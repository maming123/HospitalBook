using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPrj
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //163的邮箱好用
            Module.Utils.LiveEmail email = new Module.Utils.LiveEmail("smtp.sina.com", "xieheapp@sina.com", "xieheapp", "", 25);

          
            
            email.FromEmailUserName = "系统邮件";
            email.ToEmailAddress = "";//"xx@163.com";
            email.ToEmailUserName = "userid";//"测试";

            email.emailSubject = "主题";
           
            string htmlBody = string.Format(@"您的密码是xxx，请用新密码登录");
            email.emailBody = htmlBody;

            string r = email.SendEmail();
        }
    }
}
