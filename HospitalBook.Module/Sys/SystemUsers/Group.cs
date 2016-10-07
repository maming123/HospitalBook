using System;


			/******************************
			 * �����ƣ�Group
			 *   ���ܣ���ʾһ���û���
			 *     by��Lining
			 *   ���ڣ�2004-10-14
             * Edit By: Maming 2006-4-17
			 *****************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// ��ʾһ���û��顣���ܼ̳д��ࡣ
	/// </summary>
	public sealed class Group
	{
		/// <summary>
		/// ����һ���û������
		/// </summary>
		public Group()
		{

		}

		/// <summary>
		/// ����һ���û������
		/// </summary>
		/// <param name="uniqueID">���Ψһ��ʶ</param>
		/// <param name="groupName">������</param>
		public Group(string uniqueID,string groupName)
		{
			this._id = uniqueID;
			this._name = groupName;
		}


		#region �ֶ�

		/// <summary>
		/// �û����Ψһ��ʶ
		/// </summary>
		private string _id = "";

		/// <summary>
		/// �û��������
		/// </summary>
		private string _name = "";

        /// <summary>
        /// �жϸ����Ƿ��ǳ�������Ա�飬100Ϊ�߼�����Ա��
        /// </summary>
        private string _IsAdmin = "0";

        private string _ForRegUser = "";

		#endregion �����ֶ�


		#region ����

		/// <summary>
		/// ��ȡ�������û���ı��
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
		/// ��ȡ�������û��������
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
        ///  �жϸ����Ƿ��ǳ�������Ա�飬100Ϊ�߼�����Ա��
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
        /// �����Ƿ��ǵ������������û�ע���飩
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

		#endregion ��������
	}
}
