using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module.Models;

namespace HospitalBook.Module
{
    public class RegistCodeBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registCode"></param>
        /// <returns></returns>
        public static Sys_RegistCode SingleOrDefault(string registCode)
        {
            Sys_RegistCode model = Sys_RegistCode.SingleOrDefault(@"where RegistCode=@0", registCode);
             return model;
        }
    }
}
