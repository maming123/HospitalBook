using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module.Utils;

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

        [TestMethod]
        public void TestMethod2()
        {
            //执行办法 不要删除newNum.txt和其中的内容，
            //每次执行请设置生成的数量
            int createCount = 60000;
            string[] arr = { "a", "2", "b","3", "c", "4", "d","5", "e", "6","f","7", "g","8", "h", "9",  "m", "n", "p", "s","t","u","w","x","y","z"};
            int low = 1000000000;
            int high = 2000000000;
            Random rd = new Random();

            List<string> listStrRepeat = new List<string>();

            List<string> listStr = new List<string>();
            //存储每次的增量
            List<string> listStrNew = new List<string>();
            string [] strFromtxt =System.IO.File.ReadAllLines("c:\\newNum.txt");
            listStr.AddRange(strFromtxt);
            //生成的数量
            int iCreateNumCount = listStr.Count + createCount;
            while (listStr.Count<iCreateNumCount)
            {
                int r = rd.Next(low, high);
                char[] cArr = r.ToString().ToCharArray();

                string strtmp = "";
                Random rdIndex = new Random();
                int rdI = rdIndex.Next(1, 9);
                for (int j = 1; j < cArr.Length; j++)
                {
                    int index = Convert.ToInt32(cArr[j].ToString()) + rdI;
                    if(index>=arr.Length)
                    {
                        string s = "1";
                    }
                    strtmp += arr[index];
                }
                if (!listStr.Contains(strtmp))
                {
                    char [] charArr = strtmp.ToCharArray();
                    //判断charArr不能都是数字
                    int result = 0;
                    if (!Int32.TryParse(strtmp, out result))
                    {
                        listStr.Add(strtmp);
                        listStrNew.Add(strtmp);
                    }else
                    {
                        listStrRepeat.Add(strtmp);
                    }
                }else
                {
                    listStrRepeat.Add(strtmp);
                }
            }

            System.IO.File.AppendAllLines("c:\\newNum.txt", listStrNew);
            System.IO.File.AppendAllLines("c:\\newNum" + DateTime.Now.ToString("yyyyMMdd") + ".txt", listStrNew);
            System.IO.File.AppendAllLines("c:\\newNumRepeat.txt", listStrRepeat);
        }

    }
}
