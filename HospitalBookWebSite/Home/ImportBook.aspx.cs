using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Module.Models;
using Module.Utils;

namespace HospitalBookWebSite.Home
{
    public partial class ImportBook : HospitalBook.WebSite.Home.ManagePageBase
    {
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
            foreach(Sys_Module sys in list)
            {
                this.ddlBook.Items.Add(new ListItem(sys.MODULE_NAME, sys.MODULE_ID.ToString()));
            }
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            this.btnImportDB.Enabled = false;

            #region 测试的xml文件
//            string str = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
//<root>
//  <section title=""第一部分 基础医学综合"" id="""" parentid="""">
//    <part title=""第一篇　生 物 化 学"" id="""" parentid="""">
//      <unit title=""第一单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//      <unit title=""第二单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//    </part>
//    <part title=""第二篇　生 物 化 学"" id="""" parentid="""">
//      <unit title=""第一单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//      <unit title=""第二单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//    </part>
//    <part title=""第三篇　生 物 化 学2"" id="""" parentid="""">
//      <unit title=""第一单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//      <unit title=""第二单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//    </part>
//  </section>
//  <section title=""第二部分 基础医学综合"" id="""" parentid="""">
//    <part title=""第一篇　生 物 化 学"" id="""" parentid="""">
//      <unit title=""第一单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//      <unit title=""第二单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//    </part>
//    <part title=""第二篇　生 物 化 学"" id="""" parentid="""">
//      <unit title=""第一单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//
//]]>
//        </point>
//      </unit>
//      <unit title=""第二单元　蛋白质的结构与功能"" id="""" parentid="""">
//        <point parentid="""">
//          <![CDATA[采分点1：蛋白质的基本结构单位是氨基酸。
//采分点n
//]]>
//        </point>
//      </unit>
//    </part>
//  </section>
//</root>
//";
            #endregion
            string str = this.txtBookXML2.Text.Trim();

            int bookid =Convert.ToInt32(this.ddlBook.SelectedValue);
            List<Sys_Module> list = Sys_Module.Query("where parent_module_id=@0", bookid).ToList();
            if (list != null && list.Count > 0)
            {
                MessageBox.Show("该书籍下面已经有内容，请选择其他书籍", Page, "aa");
                return;
            }
            else
            {

                int beginId = bookid * 1000;
                Sys_Module moduleMax = CoreDB.Record<Sys_Module>.SingleOrDefault("select top 1 * from  Sys_Module order by MODULE_ID desc");
                if (moduleMax != null)
                {
                    beginId = moduleMax.MODULE_ID + 1;
                }
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    //从选择题 中的tab页面获取xml
                    xmlDoc.LoadXml(str);
                    this.txtBookXML2.Text = xmlDoc.OuterXml;
                    //<root><data name="专业实务 "><section
                    XmlNodeList nodeSectionList = xmlDoc.SelectNodes(@"root/section");
                    XmlNodeList nodePartList = xmlDoc.SelectNodes(@"root/section/part");
                    XmlNodeList nodeUnitList = xmlDoc.SelectNodes(@"root/section/part/unit");

                    for (int sectionNum = 0; sectionNum < nodeSectionList.Count; sectionNum++)
                    {
                        nodeSectionList[sectionNum].Attributes["id"].Value = Convert.ToString(beginId + sectionNum);
                        nodeSectionList[sectionNum].Attributes["parentid"].Value = bookid.ToString();
                    }

                    for (int partNum = 0; partNum < nodePartList.Count; partNum++)
                    {
                        nodePartList[partNum].Attributes["id"].Value = Convert.ToString(beginId + nodeSectionList.Count + partNum);
                        nodePartList[partNum].Attributes["parentid"].Value = nodePartList[partNum].ParentNode.Attributes["id"].Value;
                    }

                    for (int unitNum = 0; unitNum < nodeUnitList.Count; unitNum++)
                    {
                        nodeUnitList[unitNum].Attributes["id"].Value = Convert.ToString(beginId + nodeSectionList.Count + nodePartList.Count + unitNum);
                        nodeUnitList[unitNum].Attributes["parentid"].Value = nodeUnitList[unitNum].ParentNode.Attributes["id"].Value;
                        nodeUnitList[unitNum].ChildNodes[0].Attributes["parentid"].Value = nodeUnitList[unitNum].Attributes["id"].Value;
                    }
                    //$point$$content$
                    string xmlCheck = xmlDoc.OuterXml.Replace("&#xD;&#xA;","");
                    if(xmlCheck.IndexOf("$point$")>-1 )
                    {
                        this.txtBookXML2.Text = xmlCheck.Substring(xmlCheck.IndexOf("$point$"));
                        this.Label1.Text = "存在未格式化的$point$节点";
                    }else if( xmlCheck.IndexOf("$content$")>-1)
                    {
                        this.txtBookXML2.Text = xmlCheck.Substring(xmlCheck.IndexOf("$content$"));
                        this.Label1.Text = "存在未格式化的$content$节点";
                    }else
                    {
                        this.txtBookXML2.Text = xmlCheck;
                        if (nodeUnitList.Count > 0)
                        {
                            this.Label1.Text = "总共有" + nodeUnitList.Count.ToString() + "单元";
                            this.btnImportDB.Enabled = true;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    this.txtResult.Text = ex.Message + ex.Source + ex.StackTrace;
                }
            }
        }

        protected void btnImport0_Click(object sender, EventArgs e)
        {
            this.txtBookxml.Text = GetStep1(this.txtBook.Text.Trim());
        }

        #region 获取数组
        private string GetStep1(string origStr)
        {
            
            //把 内容中的\n替换掉 多行换成1行
            //整理后问道类似
            /***
             * 第一部分 基础医学综合
             *      第一篇　生 物 化 学
             *           第一单元　蛋白质的结构与功能
             *               采分点1：蛋白质的基本结构单位是氨基酸。
             *               采分点n
             *           第二单元　蛋白质的结构与功能
             *              采分点1：蛋白质的基本结构单位是氨基酸。
             *              采分点n    
             *      第二篇　生 物 化 学
             *          第一单元　蛋白质的结构与功能
             *          采分点1：蛋白质的基本结构单位是氨基酸。
             *          采分点n
             *          第二单元　蛋白质的结构与功能
             *          采分点1：蛋白质的基本结构单位是氨基酸。
             *          采分点n       
             * 第二部分 基础医学综合
             *      第一篇　生 物 化 学
             *           第一单元　蛋白质的结构与功能
             *               采分点1：蛋白质的基本结构单位是氨基酸。
             *               采分点n
             *           第二单元　蛋白质的结构与功能
             *              采分点1：蛋白质的基本结构单位是氨基酸。
             *              采分点n    
             *      第二篇　生 物 化 学
             *          第一单元　蛋白质的结构与功能
             *          采分点1：蛋白质的基本结构单位是氨基酸。
             *          采分点n
             *          第二单元　蛋白质的结构与功能
             *          采分点1：蛋白质的基本结构单位是氨基酸。
             *          采分点n       
             * **/
            StringBuilder sb = new StringBuilder();
            List<string> listOrig = new List<string>();
            byte [] barr =Encoding.UTF8.GetBytes(origStr);
            MemoryStream ms = new MemoryStream(barr);
            using (StreamReader sr = new StreamReader(ms))
            {
                string point = "";
                while (sr.Peek() > -1)
                {
                    string str = sr.ReadLine().Trim();

                    if (Regex.IsMatch(str, @"采分点", RegexOptions.Singleline))
                    {
                        point +="$content$"+ str;
                    }
                    else
                    {
                        if (Regex.IsMatch(str, @"第[\d一二三四五六七八九十]+部分", RegexOptions.Singleline))
                        {
                            str = "$section$" + str;
                            point = GetPoint(listOrig, point);
                        }
                        else if (Regex.IsMatch(str, @"第[\d一二三四五六七八九十]+篇", RegexOptions.Singleline))
                        {
                            str = "$part$" + str;
                            point = GetPoint(listOrig, point);
                        }
                        else if (Regex.IsMatch(str, @"第[\d一二三四五六七八九十]+单元", RegexOptions.Singleline))
                        {
                            str = "$unit$" + str;
                            point = GetPoint(listOrig, point);
                        }
                        listOrig.Add(str);
                    }
                    
                }
                point = GetPoint(listOrig, point);
                
            }
            ms.Close();
            ms.Dispose();
            
            listOrig.ForEach(m =>
            {
               
                sb.Append(m + "\n");
            }
            );
            return sb.ToString();
        }

        /// <summary>
        /// 获取采分点
        /// </summary>
        /// <param name="listOrig"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private string GetPoint(List<string> listOrig, string point)
        {
            if (!string.IsNullOrEmpty(point))
            {
                listOrig.Add("$point$" + point);
                point = "";
            }
            return point;
        }
        #endregion


        //把整理好的XML导入数据库
        protected void btnImportDB_Click(object sender, EventArgs e)
        {
            string strXML =this.txtBookXML2.Text.Trim();
            List<Sys_Point> listPoint =new List<Sys_Point>();
            List<Sys_Module> listModule = GetList(strXML, listPoint);

            string strSql = "";
            //用事务处理 把xml导入数据库
            try
            {
                var db = CoreDB.GetInstance();
                using (var scope = db.GetTransaction())
                {
                    strSql = "SET IDENTITY_INSERT Sys_Module ON";
                    db.Execute(strSql);
                    for (int sectionNum = 0; sectionNum < listModule.Count; sectionNum++)
                    {
                       
                        int    MODULE_ID = listModule[sectionNum].MODULE_ID;
                            
                         string   MODULE_NAME = listModule[sectionNum].MODULE_NAME;

                         int PARENT_MODULE_ID = Convert.ToInt32(listModule[sectionNum].PARENT_MODULE_ID);

                         strSql = string.Format(@"INSERT INTO [Sys_Module]
                                                       (
                                                       MODULE_ID
                                                       ,[MODULE_NAME]
                                                       ,[PARENT_MODULE_ID]
                                                        ,IS_DISPLAY
                                                       )
                                                 VALUES
                                                       ({0},'{1}',{2},1)", MODULE_ID, MODULE_NAME, PARENT_MODULE_ID);
                         db.Execute(strSql);
                    }
                    for (int i = 0; i < listPoint.Count;i++)
                    {
                        db.Insert(listPoint[i]);
                    }
                    strSql = "SET IDENTITY_INSERT Sys_Module OFF";
                    db.Execute(strSql);
                    scope.Complete();
                }
                this.txtResult.Text = "导入成功";
            }catch(Exception ex)
            {
                this.txtResult.Text = strSql+"\r\n"+ex.Message + ex.Source + ex.StackTrace;
            }

            
        }

        private List<Sys_Module> GetList(string strXML,List<Sys_Point> listPoint)
        {
            
            List<Sys_Module> list = new List<Sys_Module>();
            XmlDocument xmlDoc = new XmlDocument();
            //从选择题 中的tab页面获取xml
            xmlDoc.LoadXml(strXML.Trim());

            //<root><data name="专业实务 "><section
            XmlNodeList nodeSectionList = xmlDoc.SelectNodes(@"root/section");
            XmlNodeList nodePartList = xmlDoc.SelectNodes(@"root/section/part");
            XmlNodeList nodeUnitList = xmlDoc.SelectNodes(@"root/section/part/unit");
            for (int sectionNum = 0; sectionNum < nodeSectionList.Count; sectionNum++)
            {
                Sys_Module sys = new Sys_Module() { 
                
                    MODULE_ID = Convert.ToInt32(nodeSectionList[sectionNum].Attributes["id"].Value)
                            
                      ,   MODULE_NAME = nodeSectionList[sectionNum].Attributes["title"].Value
                        
                        ,  PARENT_MODULE_ID = Convert.ToInt32(nodeSectionList[sectionNum].Attributes["parentid"].Value)
                        , IS_DISPLAY=1
                };
                list.Add(sys);
            }

            for (int partNum = 0; partNum < nodePartList.Count; partNum++)
            {
                Sys_Module sys = new Sys_Module()
                {

                    MODULE_ID = Convert.ToInt32(nodePartList[partNum].Attributes["id"].Value)

                    ,
                    MODULE_NAME = nodePartList[partNum].Attributes["title"].Value

                    ,
                    PARENT_MODULE_ID = Convert.ToInt32(nodePartList[partNum].Attributes["parentid"].Value)
                    ,
                    IS_DISPLAY = 1
                };
                list.Add(sys);

            }

            Sys_Point pointMax = CoreDB.Record<Sys_Point>.SingleOrDefault("select top 1 * from  Sys_Point order by Id desc");
            if(pointMax==null)
            {
                pointMax = new Sys_Point() {  Id=1};
            }
            for (int unitNum = 0; unitNum < nodeUnitList.Count; unitNum++)
            {
                Sys_Module sys = new Sys_Module()
                {

                    MODULE_ID = Convert.ToInt32(nodeUnitList[unitNum].Attributes["id"].Value)

                    ,
                    MODULE_NAME = nodeUnitList[unitNum].Attributes["title"].Value

                    ,
                    PARENT_MODULE_ID = Convert.ToInt32(nodeUnitList[unitNum].Attributes["parentid"].Value)
                    ,
                    IS_DISPLAY = 1
                };
                list.Add(sys);

                Sys_Point point = new Sys_Point() {
                    ModulelId = Convert.ToInt32(nodeUnitList[unitNum].ChildNodes[0].Attributes["parentid"].Value)
                    ,
                    Id = pointMax.Id+unitNum+1
                    , CreateDateTime=DateTime.Now
                    ,
                    Content = nodeUnitList[unitNum].FirstChild.FirstChild.Value
                };
                listPoint.Add(point);
            }
            return list;
        }

        protected void btnTransfor2_Click(object sender, EventArgs e)
        {
            //$section$第一部分 基础医学综合
            //$part$第一篇　生 物 化 学
            //$unit$第一单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$unit$第二单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$part$第二篇　生 物 化 学
            //$unit$第一单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$unit$第二单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$section$第二部分 基础医学综合
            //$part$第一篇　生 物 化 学
            //$unit$第一单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$unit$第二单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$part$第二篇　生 物 化 学

            //$unit$第一单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
            //$unit$第二单元　蛋白质的结构与功能
            //$point$$content$采分点1：蛋白质的基本结构单位是氨基酸。$content$采分点n
           
           this.txtBookXML2.Text=  GetStep2(this.txtBookxml.Text.Trim());
        }

        private string GetStep2(string str)
        {

            str =str.Replace("\n", "");
            string[] arr = str.Split(new string[] { "$section$" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sbAll = new StringBuilder();
            sbAll.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root>");
            for (int i = 0; i < arr.Length; i++)
            {
                //部分
                string[] arrSection = arr[i].Split(new string[] { "$part$" }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sbSection = new StringBuilder();
                sbSection.AppendFormat(@"<section title=""{0}"" id="""" parentid="""">", arrSection[0]);
                //篇
                for (int partNum = 1; partNum < arrSection.Length; partNum++)
                { 
                    //篇
                    string[] arrPart = arrSection[partNum].Split(new string[] { "$unit$" }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sbPart = new StringBuilder();
                    sbPart.AppendFormat(@"<part title=""{0}"" id="""" parentid="""">"
                        , arrPart[0]);
                    for (int unitNum = 1; unitNum < arrPart.Length; unitNum++)
                    {
                        //单元
                        string[] arrUnit = arrPart[unitNum].Split(new string[] { "$point$" }, StringSplitOptions.RemoveEmptyEntries);
                        StringBuilder sbUnit = new StringBuilder();
                        sbUnit.AppendFormat(@"<unit title=""{0}"" id="""" parentid="""">"
                            , arrUnit[0]
                           );
                        //采分点
                        for (int pointNum = 1; pointNum < arrUnit.Length; pointNum++)
                        {
                            string[] arrPoint = arrUnit[pointNum].Split(new string[] { "$content$" }, StringSplitOptions.RemoveEmptyEntries);
                            StringBuilder sbPoint = new StringBuilder();
                            sbPoint.AppendFormat(@"<point parentid=""""><![CDATA["
                               );
                            //循环采分点
                            for (int contentNum = 0; contentNum < arrPoint.Length; contentNum++)
                            {
                                sbPoint.AppendFormat(@"<br/>{0}", arrPoint[contentNum]+"\r\n");
                            }
                            sbPoint.Append("]]></point>");
                            sbUnit.Append(sbPoint.ToString());
                        }
                        sbUnit.AppendFormat(@"</unit>");
                        sbPart.Append(sbUnit.ToString());
                    }
                    sbPart.AppendFormat(@"</part>");
                    sbSection.Append(sbPart.ToString());
                }
                sbSection.AppendFormat(@"</section>");
                sbAll.Append(sbSection.ToString());
            }
            sbAll.AppendFormat(@"</root>");
            return sbAll.ToString();
        }

        protected void btnExportXMLFromDB_Click(object sender, EventArgs e)
        {
            int bookid = Convert.ToInt32(this.ddlBook.SelectedValue);
            string bookName =this.ddlBook.SelectedItem.Text;
            List<Sys_Module> listSection = Sys_Module.Query(@" where parent_module_id=@0", bookid).ToList();
            
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", "");//xml文档的声明部分  
            document.AppendChild(declaration);  
            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);

            foreach(Sys_Module section in listSection)
            {
                XmlElement sectionXe = document.CreateElement("section");
                sectionXe.SetAttribute("title", section.MODULE_NAME);
                sectionXe.SetAttribute("id", section.MODULE_ID.ToString());
                sectionXe.SetAttribute("parentid", section.PARENT_MODULE_ID.ToString()); 
                root.AppendChild(sectionXe);
                List<Sys_Module> listPart = Sys_Module.Query(@" where parent_module_id=@0", section.MODULE_ID).ToList();
                foreach(Sys_Module part in listPart)
                {
                    XmlElement partXe = document.CreateElement("part");
                    partXe.SetAttribute("title", part.MODULE_NAME);
                    partXe.SetAttribute("id", part.MODULE_ID.ToString());
                    partXe.SetAttribute("parentid", part.PARENT_MODULE_ID.ToString());
                    partXe.SetAttribute("display", part.IS_DISPLAY.ToString());
                    sectionXe.AppendChild(partXe);
                    List<Sys_Module> listUnit = Sys_Module.Query(@" where parent_module_id=@0", part.MODULE_ID).ToList();
                    foreach(Sys_Module unit in listUnit)
                    {
                        XmlElement unitXe = document.CreateElement("unit");
                        unitXe.SetAttribute("title", unit.MODULE_NAME);
                        unitXe.SetAttribute("id", unit.MODULE_ID.ToString());
                        unitXe.SetAttribute("parentid", unit.PARENT_MODULE_ID.ToString());
                        partXe.AppendChild(unitXe);
                        List<Sys_Point> listPoint = Sys_Point.Query(@" where ModulelId=@0", unit.MODULE_ID).ToList();
                        foreach(Sys_Point point in listPoint)
                        {
                            XmlElement pointXe = document.CreateElement("point");
                            pointXe.SetAttribute("id", point.Id.ToString());
                            pointXe.SetAttribute("parentid", point.ModulelId.ToString());
                            pointXe.AppendChild(document.CreateCDataSection(point.Content));
                            unitXe.AppendChild(pointXe);
                        }
                    }
                }
            }

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(bookName+DateTime.Now.ToString("yyyyMMddHHss"), System.Text.Encoding.UTF8) + ".xml");
            Response.ContentType = "application/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "GB2312";


            Response.Write(document.OuterXml);
            Response.End();
           
        }
    }
}