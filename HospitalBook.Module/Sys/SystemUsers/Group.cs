using System;


			/******************************
			 * 类名称：Group
			 *   功能：表示一个用户组
			 *     by：Lining
			 *   日期：2004-10-14
             * Edit By: Maming 2006-4-17
			 *****************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// 表示一个用户组。不能继承此类。
	/// </summary>
	public sealed class Group
	{
		/// <summary>
		/// 创建一个用户组对象
		/// </summary>
		public Group()
		{

		}

		/// <summary>
		/// 创建一个用户组对象
		/// </summary>
		/// <param name="uniqueID">组的唯一标识</param>
		/// <param name="groupName">组名称</param>
		public Group(string uniqueID,string groupName)
		{
			this._id = uniqueID;
			this._name = groupName;
		}


		#region 字段

		/// <summary>
		/// 用户组的唯一标识
		/// </summary>
		private string _id = "";

		/// <summary>
		/// 用户组的名称
		/// </summary>
		private string _name = "";

        /// <summary>
        /// 判断该组是否是超级管理员组，100为高级管理员组
        /// </summary>
        private string _IsAdmin = "0";

        private string _ForRegUser = "";

		#endregion 结束字段


		#region 属性

		/// <summary>
		/// 获取或设置用户组的编号
		/// </summary>
		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>
		/// 获取或设置用户组的名称
		/// </summary>
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
        /// <summary>
        ///  判断该组是否是超级管理员组，100为高级管理员组
        /// </summary>
        public string Is_Admin
        {
            get
            {
                return this._IsAdmin;
            }
            set
            {
                this._IsAdmin = value;
            }
        }
        /// <summary>
        /// 该组是否是电子政务（外来用户注册组）
        /// </summary>
        public string ForRegUser
        {
            get
            {
                return this._ForRegUser;
            }
            set
            {
                this._ForRegUser = value;
            }
        }

		#endregion 结束属性
	}
}
