using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module.Utils
{
    public class ExceptionType
    {
        /// <summary>
        /// 1成功
        /// </summary>
        public const int Success = 1;

        /// <summary>
        /// 0失败
        /// </summary>
        public const int Error = 0;

        /// <summary>
        /// -102系统异常
        /// </summary>
        public const int SystemError = -102;


        /// <summary>
        /// -107无效的参数信息
        /// </summary>
        public const int ArgumentError = -107;

        /// <summary>
        /// -111未授权
        /// </summary>
        public const int NotAuthorize = -111;

        /// <summary>
        /// -105用户未登录
        /// </summary>
        public const int NotLogin = -105;
    }
}