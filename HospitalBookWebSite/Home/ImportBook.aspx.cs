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

namespace HospitalBookWebSite.Home
{
    public partial class ImportBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            this.btnImportDB.Enabled = false;

            #region 测试的xml文件
            string str = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<root>
  <section title=""第一部分 基础医学综合"">
    <part title=""第一篇　生物化学"" display=""1"">
      <unit title=""第一单元　蛋白质的结构与功能"">
        <point>
          <![CDATA[
采分点1：蛋白质的基本结构单位是氨基酸。
...
采分点n
]]>
        </point>
      </unit>
      <unit title=""第二单元　核酸的结构与功能"">
        <point>
          <![CDATA[
采分点1：核酸的结构与功能。
...
采分点n
]]>
        </point>
      </unit>
    </part>
    <part title=""第二篇　生理学"" display=""1"">
      <unit title=""第一单元　细胞的基本功能"">
        <point>
          <![CDATA[
采分点1：细胞的基本功能。
...
采分点n
]]>
        </point>
      </unit>
      <unit title=""第二单元　血液"">
        <point>
          <![CDATA[
采分点1：血液。
...
采分点n
]]>
        </point>
      </unit>
    </part>
  </section>
  <section title=""第二部分 医学人文综合"">
    <part title=""第一篇　医学心理学"" display=""1"">
      <unit title=""第一单元　绪论"">
        <point>
          <![CDATA[
采分点1：绪论。
...
采分点n
]]>
        </point>
      </unit>
      <unit title=""第二单元　医学心理学基础"">
        <point>
          <![CDATA[
采分点1：医学心理学基础。
...
采分点n
]]>
        </point>
      </unit>
    </part>
    <part title=""第二篇　医学伦理学"" display=""1"">
      <unit title=""第一单元　伦理学与医学伦理学"">
        <point>
          <![CDATA[
采分点1：伦理学与医学伦理学。
...
采分点n
]]>
        </point>
      </unit>
      <unit title=""第二单元　医学伦理学的基本原则与规范"">
        <point>
          <![CDATA[
采分点1：医学伦理学的基本原则与规范。
...
采分点n
]]>
        </point>
      </unit>
    </part>
  </section>
</root>

";
            #endregion

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                //从选择题 中的tab页面获取xml
                xmlDoc.LoadXml(str);
                this.txtBookXML2.Text = xmlDoc.OuterXml;
                //<root><data name="专业实务 "><section
                XmlNodeList nodeList = xmlDoc.SelectNodes(@"root/section/part/unit/point");
                if(nodeList.Count>0)
                {
                    this.Label1.Text = "总共有" + nodeList.Count.ToString()+"单元";
                    this.btnImportDB.Enabled = true;
                }
            }catch(Exception ex)
            {
                this.txtResult.Text = ex.Message + ex.Source + ex.StackTrace;
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
           int  bookid=1;
           this.txtBookXML2.Text=  GetStep2(this.txtBookxml.Text.Trim(),bookid);
        }

        private string GetStep2(string str,int bookid)
        {
            //假设1本书 1000 区间为1000
           
            int beginid=bookid * 1000;
            str =str.Replace("\n", "");
            string[] arr = str.Split(new string[] { "$section$" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sbAll = new StringBuilder();
            sbAll.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?><root>");
            for (int i = 0; i < arr.Length; i++)
            {
                //部分
                string[] arrSection = arr[i].Split(new string[] { "$part$" }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sbSection = new StringBuilder();
                sbSection.AppendFormat(@"<section title=""{0}"" id=""{1}"" parentid=""{2}"">", arrSection[0], beginid + i, bookid);
                //篇
                for (int partNum = 1; partNum < arrSection.Length; partNum++)
                {
                    //篇
                    string[] arrPart = arrSection[partNum].Split(new string[] { "$unit$" }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder sbPart = new StringBuilder();
                    sbPart.AppendFormat(@"<part title=""{0}"" id=""{1}"" parentid=""{2}"">", arrPart[0], beginid + i + partNum, beginid + i);
                    for (int unitNum = 1; unitNum < arrPart.Length; unitNum++)
                    {
                        //单元
                        string[] arrUnit = arrPart[unitNum].Split(new string[] { "$point$" }, StringSplitOptions.RemoveEmptyEntries);
                        StringBuilder sbUnit = new StringBuilder();
                        sbUnit.AppendFormat(@"<unit title=""{0}"">", arrUnit[0]);
                        //采分点
                        for (int pointNum = 1; pointNum < arrUnit.Length; pointNum++)
                        {
                            string[] arrPoint = arrUnit[pointNum].Split(new string[] { "$content$" }, StringSplitOptions.RemoveEmptyEntries);
                            StringBuilder sbPoint = new StringBuilder();
                            sbPoint.Append("<point><![CDATA[");
                            //循环采分点
                            for (int contentNum = 0; contentNum < arrPoint.Length; contentNum++)
                            {
                                sbPoint.AppendFormat(@"{0}", arrPoint[contentNum]+"\r\n");
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
    }
}