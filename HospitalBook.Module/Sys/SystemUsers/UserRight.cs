using System;


			/*********************************
			 * �����ƣ�UserRight
			 *   ���ܣ���ʾ�û���ĳ��ģ�鵱��
			 *	       ��ĳ�����ܵ�Ȩ��
			 *     by��Lining
			 *   ���ڣ�2004-10-16
             * Edit By Maming 2006-3-31
			 ********************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// ��ʾ�û���ĳ��ģ�鵱�е�ĳ�����ܵ�Ȩ�ޡ����ܼ̳д��ࡣ
	/// </summary>
	public sealed class UserRight
	{
		/// <summary>
		/// ����Ȩ�޶���
		/// </summary>
		public UserRight()
		{
		}

		/// <summary>
		/// ����Ȩ�޶���
		/// </summary>
		/// <param name="moduleID">ģ����</param>
		/// <param name="functionID">���ܱ��</param>
		public UserRight(int moduleID,int functionID)
		{
			this._moduleID = moduleID;
			this._functionID = functionID;
		}


		#region �ֶ�

		/// <summary>
		/// ģ����
		/// </summary>
		private int _moduleID = 0;

		/// <summary>
		/// ���ܱ��
		/// </summary>
		private int _functionID = 0;

		/// <summary>
		/// �Ƿ�ӵ��Ȩ��
		/// </summary>
		private bool _possessRight = true;

        /// <summary>
        /// ��Ҷ��ģ����޸�Ȩ�ޣ�����б���Ϣ��������������������̬ҳ��
        /// </summary>
        private bool _editRight = false;

        /// <summary>
        /// ��Ҷ��ģ������Ȩ�ޣ�����б���Ϣ������������������
        /// </summary>
        private bool _addRight = false;
        /// <summary>
        /// ��Ҷ��ģ���ɾ��Ȩ�ޣ�����б���Ϣ������������������
        /// </summary>
        private bool _deleteRight = false;
        /// <summary>
        /// ��Ҷ��ģ���ά��Ȩ�ޣ����ҵ������
        /// </summary>
        private bool _manageRight = false;

		#endregion �����ֶ�


		#region ����

		/// <summary>
		/// ��ȡ������ģ����
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
		/// ��ȡ�����ù��ܱ��
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
		/// ��ȡ�������Ƿ�ӵ��Ȩ��
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
        /// ��Ҷ��ģ����޸�Ȩ�ޣ�����б���Ϣ��������������������̬ҳ��
        /// </summary>
        public bool EditRight
        {
            get { return _editRight; }
            set { _editRight = value; }
        }
        /// <summary>
        /// ��Ҷ��ģ������Ȩ�ޣ�����б���Ϣ������������������
        /// </summary>
        public bool AddRight
        {
            get { return _addRight; }
            set { _addRight = value; }
        }
        /// <summary>
        /// ��Ҷ��ģ���ɾ��Ȩ�ޣ�����б���Ϣ������������������
        /// </summary>
        public bool DeleteRight
        {
            get { return _deleteRight; }
            set { _deleteRight = value; }
        }
        /// <summary>
        /// ��Ҷ��ģ���ά��Ȩ�ޣ����ҵ������
        /// </summary>
        public bool ManageRight
        {
            get { return _manageRight; }
            set { _manageRight = value; }
        }

		#endregion ��������

        /// <summary>
        /// �û���Ҷ��ģ��ľ����������
        /// </summary>
        public enum UserRightFunction
        {
            /// <summary>
            /// ��Ҷ��ģ������Ȩ�ޣ�����б���Ϣ������������������
            /// </summary>
            Add,
            /// <summary>
            /// ��Ҷ��ģ����޸�Ȩ�ޣ�����б���Ϣ��������������������̬ҳ��
            /// </summary>
            Edit,
            /// <summary>
            /// ��Ҷ��ģ���ɾ��Ȩ�ޣ�����б���Ϣ������������������
            /// </summary>
            Delete,
            /// <summary>
            /// ��Ҷ��ģ���ά��Ȩ�ޣ����ҵ������
            /// </summary>
            Manage
        }
	}
}
