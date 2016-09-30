using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Utils
{
    /// <summary>
    /// MessageBox 的摘要说明
    /// </summary>
    public class MessageBox
    {
        public MessageBox()
        {
            //
            //DONE: 在此处添加构造函数逻辑
            //
        }



        /// <summary>
        /// 客户端提示框
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="page">当前页Page对象</param>
        /// <param name="clientkey">客户端注册ID</param>
        public static void Show(string message, System.Web.UI.Page page, string clientkey)
        {
            string clientscript = "<script>";
            clientscript += "alert(\"" + message + "\");";
            clientscript += "</script>";
            page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), clientkey, clientscript);
        }

        /// <summary>
        /// 客户端提示框
        /// </summary>
        /// <param name="message">信息框内容</param>
        /// <param name="behindcontent">在弹出信息框后的脚本处理内容</param>
        /// <param name="page">当前页Page对象</param>
        /// <param name="clientkey">客户端注册ID</param>
        public static void Show(string message, string behindcontent, System.Web.UI.Page page, string clientkey)
        {
            string clientscript = "<script>";
            clientscript += "alert('" + message + "');";
            clientscript += behindcontent;
            clientscript += "</script>";
            page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), clientkey, clientscript);
        }

    }
}
