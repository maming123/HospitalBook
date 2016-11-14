using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalBook.Module;
using Module.Models;
using Module.Utils;

namespace HospitalBookWebSite.Home.handler
{
    /// <summary>
    /// PageHandler 的摘要说明
    /// </summary>
    public class PageHandler : BaseHandler
    {
        public PageHandler()
        {
            dictAction.Add("GetUserList", GetUserList);
            dictAction.Add("GetUserLogList", GetUserLogList);
            
        }
        /// <summary>
        /// code值为:
        ///-105: 用户未登录.
        /// </summary>
        /// <returns></returns>
        private bool IsReady()
        {
            if (LoginAdminUser.GetCurrentUser() == null)
            {
                Response.Write(BaseCommon.ObjectToJson(new { code = ExceptionType.NotLogin, m = "请先登录" }));
                return false;
            }
            return true;
        }

        private void GetUserList()
        {
            if (!IsReady())
                return;

            int bookId = RequestKeeper.GetFormInt(Request["bookId"]);
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            int pageIndex = RequestKeeper.GetFormInt(Request["PageIndex"]);
            int pageSize = 20;// RequestKeeper.GetFormInt(Request["PageSize"]);

            PageList<List<User>> pList = UserBusiness.GetUserList(mobile,bookId, pageIndex, pageSize);


            Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<PageList<List<User>>>() { code = 1, m = pList }));

        }
        private void GetUserLogList()
        {
            if (!IsReady())
                return;
            
            long mobile = RequestKeeper.GetFormLong(Request["mobile"]);
            string registcode = RequestKeeper.GetFormString(Request["registcode"]);
            int pageIndex = RequestKeeper.GetFormInt(Request["PageIndex"]);
            int pageSize = 20;// RequestKeeper.GetFormInt(Request["PageSize"]);

            PageList<List<UserRegistLog>> pList = UserBusiness.GetUserLogList(mobile,registcode, pageIndex, pageSize);


            Response.Write(BaseCommon.ObjectToJson(new ReturnJsonType<PageList<List<UserRegistLog>>>() { code = 1, m = pList }));

        }
    }
}