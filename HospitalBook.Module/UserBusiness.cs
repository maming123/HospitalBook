using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;
using Module.Utils;
using PetaPoco;

namespace HospitalBook.Module
{
    public  class UserBusiness:BaseBusiness
    {
        public static PageList<List<User>> GetUserList(long mobile, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"select * from [User] where 1=1 ");
            if(mobile>0)
            {
                strSql += string.Format(@" and Mobile={0}",mobile);
            }
            var db = CoreDB.GetInstance();
            Page<User> pagelist = db.Page<User>(pageIndex, pageSize, strSql);

            PageList<List<User>> pList = new PageList<List<User>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }
        public static PageList<List<UserRegistLog>> GetUserLogList(long mobile, int pageIndex, int pageSize)
        {
            string strSql = string.Format(@"select * from [UserRegistLog] where 1=1 ");
            if (mobile > 0)
            {
                strSql += string.Format(@" and Mobile={0}", mobile);
            }
            var db = CoreDB.GetInstance();
            Page<UserRegistLog> pagelist = db.Page<UserRegistLog>(pageIndex, pageSize, strSql);

            PageList<List<UserRegistLog>> pList = new PageList<List<UserRegistLog>>((int)pagelist.CurrentPage, (int)pagelist.ItemsPerPage, (int)pagelist.TotalItems);
            pList.Source = pagelist.Items.ToList();
            return pList;
        }
    }
}
