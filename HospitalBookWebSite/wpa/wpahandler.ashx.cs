using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HospitalBook.Module;
using Module.Models;
using Module.Utils;

namespace HospitalBookWebSite.wpa
{
    /// <summary>
    /// wpahandler 的摘要说明
    /// </summary>
    public class wpahandler : BaseHandler
    {
        
        public wpahandler()
        {
            base.ValidCode = ConfigurationManager.AppSettings["ValidCode"].ToString();
            dictAction.Add("regist", regist);
            dictAction.Add("login", login);
            dictAction.Add("getpassword", getpassword);
            dictAction.Add("submitscore", submitscore);
            dictAction.Add("showscore", showscore);
        }

        /// <summary>
        /// 获取得分
        /// </summary>
        private void showscore()
        {
            string type = RequestKeeper.GetFormString(Request["type"]);
            string where ="";
            if(type.ToLower()=="day")
            {
                where =" where ShortDate = "+DateTime.Now.ToString("yyyyMMdd");
            }

            List<UserScore> list = UserScore.Query("SELECT  TOP 100 UserName ,                            SUM(Score) AS 'Score'    FROM    UserScore                    "+where+"  GROUP BY UserName   ORDER BY Score DESC ").ToList();

                Response.Write(BaseCommon.ObjectToJson(list));
           
        }

        /// <summary>
        /// 添加得分
        /// </summary>
        private void submitscore()
        {
            int score = RequestKeeper.GetFormInt(Request["score"]);
            string username = RequestKeeper.GetFormString(Request["name"]);
            UserScore us = new UserScore() { 
                 Score=score
                 , UserName= username
                 , ShortDate=Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"))
                 , CreateDateTime=DateTime.Now
            };
            object r =us.Insert();
            if(Convert.ToInt32(r)>0)
            {
                Response.Write( BaseCommon.ObjectToJson(new ReturnJsonType() { code = 1, message = "成功" }));
            }else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "失败" }));
            }
        }

        /// <summary>
        /// code值为:
        /// -117: 非指定省份用户.
        /// -107: 参数错误.
        /// -105: 用户未登录.
        /// -104: 非移动用户.
        /// -102: 系统错误.
        /// -101: 活动未开始.
        /// -100: 活动已结束.
        /// </summary>
        /// <returns></returns>
        private bool IsReady(string token,long mobile)
        {

            if (token == BaseCommon.MD5(mobile.ToString(), ValidCode))
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "秘钥错误" }));
                return false;
            }
            return true;
            
        }
        //找回密码
        private void getpassword()
        {
            string token = RequestKeeper.GetFormString(Request["token"]);
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string emailInput = RequestKeeper.GetFormString(Request["email"]);
            
            //判断token是否正确
            if (!IsReady(token,mobile))
            {
                return;
            }
            User user = User.SingleOrDefault(@"where Mobile=@0 and Email=@1",mobile,emailInput);
            if (user != null)
            {
                //用邮箱和手机号来发送邮件密码
                string EmailServer = ConfigurationManager.AppSettings["EmailServer"].ToString();
                string Email = ConfigurationManager.AppSettings["Email"].ToString();
                string EmailUser = ConfigurationManager.AppSettings["EmailUser"].ToString();
                string EmailPwd = ConfigurationManager.AppSettings["EmailPwd"].ToString();
                string EmailPort = ConfigurationManager.AppSettings["EmailPort"].ToString();
                string EmailFromUserName = ConfigurationManager.AppSettings["EmailFromUserName"].ToString();
                string EmailSubject = ConfigurationManager.AppSettings["EmailSubject"].ToString();
                string EmailBody = ConfigurationManager.AppSettings["EmailBody"].ToString();

                Module.Utils.LiveEmail email = new Module.Utils.LiveEmail(EmailServer, Email, EmailUser, EmailPwd, Convert.ToInt32(EmailPort));

                email.FromEmailUserName = EmailFromUserName;// "系统邮件";
                email.ToEmailAddress = emailInput;//"xx@163.com";
                email.ToEmailUserName = "";//"测试";

                email.emailSubject = EmailSubject;// "主题";

                string htmlBody = string.Format(EmailBody, user.PassWord);
                email.emailBody = htmlBody + "</br></br></br></br>" + Guid.NewGuid().ToString();
                string r = email.SendEmail();

                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 1, message = "发送邮件成功" }));
                return;
            }else
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "用户名或者邮箱错误" }));
                return;
            }
        }
        //登录
        private void login()
        {
            //method	值填写login
            //mobile	用户手机号
            //password	密码
            //token	密钥，
            string token = RequestKeeper.GetFormString(Request["token"]);
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string password = RequestKeeper.GetFormString(Request["password"]);
            int  bookid = RequestKeeper.GetFormInt(Request["bookid"]);

            //判断token是否正确
            if (!IsReady(token, mobile))
            {
                return;
            }
            User user = User.SingleOrDefault(@"where Mobile=@0 and PassWord=@1",mobile,password);
            if(user!=null)
            { 
                //记录登录日志（包括登录时间、书籍ID、手机号）
                UserLoginLog userLoginLog = new UserLoginLog() {
                    BookId = bookid
                    , CreateDatetime=DateTime.Now
                    , Mobile=mobile
                };
                userLoginLog.Insert();
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 1, message = "成功登录" }));
                return;
            }else
            {
                //找不到 那么去日志里看 是否存在记录 如果存在说明被顶替掉了，返回不同的提示
                UserRegistLog userlog = UserRegistLog.SingleOrDefault(@"where Mobile=@0 and PassWord=@1", mobile, password);
                if (userlog != null)
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "注册码已被其他人占用，请重新注册" }));
                    return;
                }
                else
                {
                    Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "用户名不存在或者用户名密码错误" }));
                    return;
                }
            }

        }

        //注册
        private void regist()
        {
            
            //method	值填写regist
            //mobile	用户手机号
            //password	密码
            //email	邮箱
            //registcode	注册码
            //token	密钥，规则：token=MD5(‘用户手机号+key‘) 
            string token = RequestKeeper.GetFormString(Request["token"]);
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string password = RequestKeeper.GetFormString(Request["password"]);
            string email = RequestKeeper.GetFormString(Request["email"]);
            string registcode = RequestKeeper.GetFormString(Request["registcode"]).ToUpper().Trim();

            //判断token是否正确
            if (!IsReady(token, mobile))
            {
                return;
            }

            //判断手机号是否已注册
            User user = User.SingleOrDefault(@"where Mobile=@0",mobile);
            if(user!=null)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "该手机号码已被注册" }));
                return;
            }
            //判断注册码是否合法
            Sys_RegistCode srcodeHave = Sys_RegistCode.SingleOrDefault(@"where RegistCode=@0", registcode);
            if(srcodeHave==null)
            {
                Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType() { code = 0, message = "注册码不存在" }));
                return;
            }

            #region //1.如果注册码已经被其他用户注册过，那用户还可以用这个注册码？
            //可以
            //然后 把原来使用这个注册码的用户删除，记录日志。
            //下次原用户登陆时候提示查无此用户，或者提示注册码已被重新注册？

            //2.假如可以用注册码 那么被占用注册码用户还能登陆么？
            //不能

            //然后需要重新注册
            #endregion
            User userFromRegistCode = User.SingleOrDefault(@"where RegistCode=@0", registcode);
            ReturnJsonType r = new ReturnJsonType();
            if(userFromRegistCode!=null)
            {
                #region  //已被占用  把被占用的这条记录移动到UserRegistLog表，然后删除此条记录，然后进行新用户插入操作
                var db = CoreDB.GetInstance();
                try
                {
                    db.BeginTransaction();
                    UserRegistLog userlog = new UserRegistLog()
                    {
                         Mobile=userFromRegistCode.Mobile
                         , CreateDateTime=DateTime.Now
                         , Email=userFromRegistCode.Email
                         , NickName=userFromRegistCode.NickName
                         , RegistCode=userFromRegistCode.RegistCode
                         , PassWord =userFromRegistCode.PassWord
                         , BookId =srcodeHave.BookId
                    };
                    db.Insert(userlog);
                    db.Delete(userFromRegistCode);
                    User userNew = new User() { 
                         Mobile=mobile
                         , PassWord=password
                         , CreateDateTime=DateTime.Now
                         , RegistCode=registcode
                         , NickName=mobile.ToString()
                         , Email =email
                         , BookId=srcodeHave.BookId
                    };
                    db.Insert(userNew);
                    db.CompleteTransaction();
                    r.code = 1;
                    r.message = "注册成功";
                }catch(Exception ex)
                {
                    r.code = 0;
                    r.message = "注册失败";
                   
                    db.AbortTransaction();

                }
                finally
                {
                    db.CloseSharedConnection();
                }
                Response.Write(BaseCommon.ObjectToJson(r));
                return;
                #endregion

            }
            else
            {
                #region //未被占用  插入数据库  然后更新注册码为已被占用

                var db = CoreDB.GetInstance();
                try
                {
                    db.BeginTransaction();
                    
                    User userNew = new User()
                    {
                        Mobile = mobile
                        ,
                        PassWord = password
                        ,
                        CreateDateTime = DateTime.Now
                        ,
                        RegistCode = registcode
                        ,
                        NickName = mobile.ToString()
                        ,
                        Email = email
                        ,
                        BookId = srcodeHave.BookId
                    };
                    db.Insert(userNew);
                    Sys_RegistCode srcode = db.SingleOrDefault<Sys_RegistCode>(@"where RegistCode=@0",registcode);
                    if(srcode!=null)
                    {
                        srcode.IsEnable = 0;
                        db.Update(srcode);
                    }
                    db.CompleteTransaction();
                    r.code = 1;
                    r.message = "注册成功";
                }
                catch (Exception ex)
                {
                    r.code = 0;
                    r.message = "注册失败";

                    db.AbortTransaction();

                }
                finally
                {
                    db.CloseSharedConnection();
                }
                Response.Write(BaseCommon.ObjectToJson(r));
                return;
                #endregion
            }

        }

    }
}