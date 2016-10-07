using System;


			/*********************************
			 * 类名称：UserRight
			 *   功能：表示用户对某个模块当中
			 *	       的某个功能的权限
			 *     by：Lining
			 *   日期：2004-10-16
             * Edit By Maming 2006-3-31
			 ********************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// 表示用户对某个模块当中的某个功能的权限。不能继承此类。
	/// </summary>
	public sealed class UserRight
	{
		/// <summary>
		/// 创建权限对象
		/// </summary>
		public UserRight()
		{
		}

		/// <summary>
		/// 创建权限对象
		/// </summary>
		/// <param name="moduleID">模块编号</param>
		/// <param name="functionID">功能编号</param>
		public UserRight(int moduleID,int functionID)
		{
			this._moduleID = moduleID;
			this._functionID = functionID;
		}


		#region 字段

		/// <summary>
		/// 模块编号
		/// </summary>
		private int _moduleID = 0;

		/// <summary>
		/// 功能编号
		/// </summary>
		private int _functionID = 0;

		/// <summary>
		/// 是否拥有权限
		/// </summary>
		private bool _possessRight = true;

        /// <summary>
        /// 对叶子模块的修改权限，针对列表信息、档案管理、审批管理、静态页面
        /// </summary>
        private bool _editRight = false;

        /// <summary>
        /// 对叶子模块的添加权限，针对列表信息、档案管理、审批管理
        /// </summary>
        private bool _addRight = false;
        /// <summary>
        /// 对叶子模块的删除权限，针对列表信息、档案管理、审批管理
        /// </summary>
        private bool _deleteRight = false;
        /// <summary>
        /// 对叶子模块的维护权限，针对业务数据
        /// </summary>
        private bool _manageRight = false;

		#endregion 结束字段


		#region 属性

		/// <summary>
		/// 获取或设置模块编号
		/// </summary>
		public int ModuleID
		{
			get
			{
				return this._moduleID;
			}
			set
			{
				this._moduleID = value;
			}
		}

		/// <summary>
		/// 获取或设置功能编号
		/// </summary>
		public int FunctionID
		{
			get
			{
				return this._functionID;
			}
			set
			{
				this._functionID = value;
			}
		}

		/// <summary>
		/// 获取或设置是否拥有权限
		/// </summary>
		public bool PossessRight
		{
			get
			{
				return this._possessRight;
			}
			set
			{
				this._possessRight = value;
			}
		}

        /// <summary>
        /// 对叶子模块的修改权限，针对列表信息、档案管理、审批管理、静态页面
        /// </summary>
        public bool EditRight
        {
            get { return _editRight; }
            set { _editRight = value; }
        }
        /// <summary>
        /// 对叶子模块的添加权限，针对列表信息、档案管理、审批管理
        /// </summary>
        public bool AddRight
        {
            get { return _addRight; }
            set { _addRight = value; }
        }
        /// <summary>
        /// 对叶子模块的删除权限，针对列表信息、档案管理、审批管理
        /// </summary>
        public bool DeleteRight
        {
            get { return _deleteRight; }
            set { _deleteRight = value; }
        }
        /// <summary>
        /// 对叶子模块的维护权限，针对业务数据
        /// </summary>
        public bool ManageRight
        {
            get { return _manageRight; }
            set { _manageRight = value; }
        }

		#endregion 结束属性

        /// <summary>
        /// 用户对叶子模块的具体操作功能
        /// </summary>
        public enum UserRightFunction
        {
            /// <summary>
            /// 对叶子模块的添加权限，针对列表信息、档案管理、审批管理
            /// </summary>
            Add,
            /// <summary>
            /// 对叶子模块的修改权限，针对列表信息、档案管理、审批管理、静态页面
            /// </summary>
            Edit,
            /// <summary>
            /// 对叶子模块的删除权限，针对列表信息、档案管理、审批管理
            /// </summary>
            Delete,
            /// <summary>
            /// 对叶子模块的维护权限，针对业务数据
            /// </summary>
            Manage
        }
	}
}
