using System;
using SNS.Library.Database;


			/****************************************
			 * �����ƣ�GroupDAOImplSQLServer
			 *   ���ܣ������û�������ݷ��ʶ����
			 *		   SQLServer�汾
			 *     by��Lining
			 *   ���ڣ�2004-11-15
			 *   ��ע���̳г�����IGroupDAO
			 * Edit BY Maming 2005-12-9
			 ***************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// �����û�������ݷ��ʶ����SQLServer�汾��ʵ�ֽӿ�IGroupDAO��
	/// </summary>
	internal class GroupDAOImplSQLServer : GroupDAO
	{
		/// <summary>
		/// ���������û�������ݷ��ʶ���
		/// </summary>
		public GroupDAOImplSQLServer()
		{
		}

		/// <summary>
		/// ����û���
		/// </summary>
		/// <param name="groupName">������</param>
		/// <returns>����</returns>
		public override string Add(string groupName)
		{
			#region before Edit
			/*
			string strSql = "INSERT INTO FU_Group(Group_Name) VALUES('" + groupName + "')";
			return DatabaseFactory.ExecuteInsertReturnPK(strSql,"FU_Group").ToString();
			*/
			#endregion
			string strSql = "declare @Num int select @Num = Max(Group_Level) from FU_Group "
				+"INSERT INTO FU_Group(Group_Name,Group_Level) VALUES('" + groupName + "',@Num+1)";
			return DatabaseFactory.ExecuteInsertReturnPK(strSql,"FU_Group").ToString();

		}
	}
}
