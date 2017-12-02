using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Utils;
using Module.DataAccess;
using Module.Models;
using SNS.Library.Database;

namespace HospitalBookWebSite.Home
{
    public partial class RegistCodeManage : System.Web.UI.Page
    {
        private Hashtable hsBookCode = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindDDLBook();
            }
        }
        private void BindDDLBook()
        {
            List<Sys_Module> list = Sys_Module.Query("where parent_module_id=1").ToList();
            foreach (Sys_Module sys in list)
            {
                this.ddlBook.Items.Add(new ListItem(sys.MODULE_NAME, sys.MODULE_ID.ToString()));
            }
            this.ddlBook.Items.Insert(0,new ListItem("选择书籍", "0"));
        }
        protected void ddlBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Sys_RegistCode> list = Sys_RegistCode.Query(@"where BookId=@0",Convert.ToInt32(this.ddlBook.SelectedValue)).ToList();
            if (list != null && list.Count > 0)
            {
                this.lblHaveCodeNum.Text = list.Count.ToString();
            }
            else
            {
                this.lblHaveCodeNum.Text = "0";
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            
            int bookId = Convert.ToInt32(this.ddlBook.SelectedValue);
            //要生成的数量
            int inputCount = 0;
            if (!int.TryParse(txtCodeNum.Text.Trim(), out inputCount) || inputCount<=0 || bookId<=0)
            {
                MessageBox.Show(Page, "请选择书籍并填写号码数量");
                return;
            }

            List<Sys_RegistCode> list = Sys_RegistCode.Query(@"where BookId=@0 ",bookId).ToList();
            foreach(Sys_RegistCode code in list)
            {
                if(!hsBookCode.ContainsKey(code.RegistCode))
                {
                    hsBookCode.Add(code.RegistCode, null);
                }
            }
            Sys_Module sysModule = Sys_Module.SingleOrDefault((object)bookId);
            //书籍的上一级是医院ID
            int low = 10000000+list.Count;
            int high = low+inputCount+1000000;
            string strSequence = @"{0}Y{1}S{2}";
            List<int> listRandom = GenerateSequence.GetRandomSequence(low, high);
            List<Sys_RegistCode> listSequences = new List<Sys_RegistCode>();


            int count = 0;
            foreach (int r in listRandom)
            {
                string strSequenceOK = string.Format(strSequence
                        , getReplace(sysModule.PARENT_MODULE_ID.ToString())
                        , getReplace(sysModule.MODULE_ID.ToString())
                        , getReplace(r.ToString())
                        );
                Sys_RegistCode sysCode =new Sys_RegistCode(){
                    RegistCode = strSequenceOK
                         ,
                    BookId = bookId
                         ,
                    IsEnable = 0
                         ,
                    CreateDateTime = DateTime.Now
                };
                if (!hsBookCode.ContainsKey(sysCode.RegistCode))
                {
                    listSequences.Add(sysCode);
                    count++;
                }
                if (count >= inputCount)
                {
                    break;
                }
            }
            SqlBulkCopy(ConnectionStrings.Core, listSequences);
            //int count = 0;
            
            //var db = CoreDB.GetInstance();
            //using (var scope = db.GetTransaction())
            //{
            //    foreach (int r in listRandom)
            //    {
            //       string strSequenceOK = string.Format(strSequence
            //            , getReplace(sysModule.PARENT_MODULE_ID.ToString())
            //            , getReplace(sysModule.MODULE_ID.ToString())
            //            , getReplace(r.ToString())
            //            );
            //        Sys_RegistCode sysCode =new Sys_RegistCode(){
            //            RegistCode = strSequenceOK
            //             , BookId=bookId
            //             , IsEnable=1
            //             , CreateDateTime=DateTime.Now
            //        };
            //        db.Insert((object)sysCode);
            //        count++;
            //        if(count>=inputCount)
            //        {
            //            break;
            //        }
            //    }
            //    scope.Complete();
            //}

        }
        private string getReplace(string str)
        {
            return str.Replace("0", "A").Replace("1", "B");
        }

        private void SqlBulkCopy(string sqlConStr,List<Sys_RegistCode> list)
        {
            SqlConnection sqlCon = new SqlConnection(sqlConStr);
            sqlCon.Open();
            SqlTransaction sqlTran = sqlCon.BeginTransaction(); // 开始事务
            SqlBulkCopy sqlBC = new SqlBulkCopy(sqlCon, SqlBulkCopyOptions.Default, sqlTran);
            sqlBC.DestinationTableName = "Sys_RegistCode";
            sqlBC.BatchSize = 1000;

            DataTable dtSale = new DataTable();
            dtSale.Columns.Add("Id", typeof(Int32));
            dtSale.Columns.Add("BookId", typeof(Int32));
            dtSale.Columns.Add("RegistCode", typeof(String));
            dtSale.Columns.Add("IsEnable", typeof(Int32));
            dtSale.Columns.Add("CreateDateTime", typeof(DateTime));

            foreach (Sys_RegistCode code in list)
            {
                DataRow sqlRow = dtSale.NewRow();
                sqlRow["Id"] = 0;
                sqlRow["BookId"] = code.BookId;
                sqlRow["RegistCode"] = code.RegistCode;
                sqlRow["IsEnable"] = code.IsEnable;
                sqlRow["CreateDateTime"] = code.CreateDateTime;
                dtSale.Rows.Add(sqlRow);
            }
            try
            {
                sqlBC.WriteToServer(dtSale); //此处报错
                sqlTran.Commit();

            }
            catch (Exception)
            {
                sqlTran.Rollback();
                throw;
            }
            finally
            {
                sqlBC.Close();
                sqlCon.Close();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            FillData("", "2");
        }

        protected void btnQureyCodeStatus_Click(object sender, EventArgs e)
        {
            FillData(this.txtCode.Text.Trim().ToUpper(), "1");
        }

        private void FillData(string code,string queryType)
        {
            string strSql = @"select top 10 * from Sys_RegistCode where 1=1 ";
            if (queryType == "1")
            {
                if (!string.IsNullOrEmpty(code))
                {
                    strSql += string.Format(@" and RegistCode='{0}'", code);
                }
                else
                {
                    return;
                }
            }
            if(queryType=="2")
            {
                strSql += string.Format(@" and IsEnable=1");
            }
            strSql += " order by Id desc";
            List<Sys_RegistCode> list = Sys_RegistCode.Query(strSql).ToList();
            this.gvList.DataSource = list;
            this.gvList.DataBind();
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ContentUpdate")
            {
               int editId = Convert.ToInt32(e.CommandArgument);
                Sys_RegistCode srcode =Sys_RegistCode.SingleOrDefault((object)editId);
                srcode.IsEnable = 0;
                int r= srcode.Update();
                if(r>0)
                {
                    MessageBox.Show(Page, "已更新，请重新查询");
                }
            }
        }

        protected void btnExportRegistCode_Click(object sender, EventArgs e)
        {

            DataTable dt =new DataTable();

            string strSql = String.Format(@"select RegistCode as '注册码',IsEnable as '是否已用 1:未用 0：已用' from Sys_RegistCode where BookId={0}", this.ddlBook.SelectedValue);
            dt =CoreDB.GetInstance().GetDataTable(strSql);

            string fileName=String.Format(@"{0}注册码{1}",this.ddlBook.SelectedItem.Text,DateTime.Now.ToString("yyyyMMddHHmm"));
            ExportDataCommon.ExportToExcel(dt,fileName);

        }
    }
}