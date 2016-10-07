using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Module.Models;
using Module.Utils;

namespace HospitalBookWebSite.Home
{
    public partial class EditPoint : HospitalBook.WebSite.Home.ManagePageBase
    {
        public int moduleId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["ModuleID"] != null)
                {
                    moduleId = Convert.ToInt32(Request["ModuleID"]);
                }
                ShowDetail(moduleId);
            }
        }
        private void ShowDetail(int moduleId)
        {
            Sys_Module unit = Sys_Module.Single((object)moduleId);
            Sys_Point point = Sys_Point.FirstOrDefault(@"where ModulelId=@0", moduleId);
            lblPointId.Text = point.Id.ToString();
            lblContent.Text = unit.MODULE_NAME;
            this.txtContent.Value = point.Content;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Sys_Point point = Sys_Point.Single((object)Convert.ToInt32(lblPointId.Text));
            
               point. Content = this.txtContent.Value;

               point.UpdateDateTime = DateTime.Now;
            
           int r =  point.Update();
           if (r > 0)
           {
               MessageBox.Show(Page, "更新成功");
           }
        }
    }
}