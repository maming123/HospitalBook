using System;


			/*************************************
			 * �����ƣ�GroupDAOFactory
			 *   ���ܣ�����ά���û�������ݷ���
			 *         ����Ĺ���
			 *     by��Lining
			 *   ���ڣ�2004-11-15
			 *   ��ע�����ܼ̳д���
			 *************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// ����ά���û�������ݷ��ʶ���Ĺ��������ܼ̳д��ࡣ
	/// </summary>
	public class GroupDAOFactory
	{
		/// <summary>
		/// ����ά���û�������ݷ��ʶ���
		/// </summary>
		/// <returns></returns>
		public static GroupDAO CreateObject()
		{
			return new GroupDAOImplSQLServer();
		}
	}
}
