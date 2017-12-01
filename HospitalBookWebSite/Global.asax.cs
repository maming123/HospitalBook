using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HospitalBookWebSite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            // Code that runs when an unhandled error occurs
            //First write Error information to DB

            //Then Clear Session
            try
            {
                Exception exc = Server.GetLastError();
                if (exc.InnerException != null)
                    exc = exc.InnerException;

                SNS.Library.Tools.WebTools.WriteLog(exc.Message + exc.Source + exc.StackTrace, SNS.Library.Logs.LogType.Error);

               // string loginStr = string.Format(@"<a href='/home/account/login.aspx'>登录</a>");
               // Response.Write(exc.Message + exc.Source + exc.StackTrace + loginStr);

            }
            catch (Exception ex)
            {
                SNS.Library.Tools.WebTools.WriteLog(ex.Message + ex.Source + ex.StackTrace, SNS.Library.Logs.LogType.Error);

                //string loginStr = string.Format(@"<a href='/view/account/login.aspx'>登录</a>");
                //Response.Write(ex.Message + ex.Source + ex.StackTrace + loginStr);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}