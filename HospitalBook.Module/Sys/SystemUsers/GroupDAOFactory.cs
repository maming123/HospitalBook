using System;


			/*************************************
			 * 类名称：GroupDAOFactory
			 *   功能：创建维护用户组的数据访问
			 *         对象的工厂
			 *     by：Lining
			 *   日期：2004-11-15
			 *   备注：不能继承此类
			 *************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// 创建维护用户组的数据访问对象的工厂。不能继承此类。
	/// </summary>
	public class GroupDAOFactory
	{
		/// <summary>
		/// 创建维护用户组的数据访问对象
		/// </summary>
		/// <returns></returns>
		public static GroupDAO CreateObject()
		{
			return new GroupDAOImplSQLServer();
		}
	}
}
