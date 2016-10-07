using System;
using SNS.Library.Database;


			/****************************************
			 * 类名称：GroupDAOImplSQLServer
			 *   功能：管理用户组的数据访问对象的
			 *		   SQLServer版本
			 *     by：Lining
			 *   日期：2004-11-15
			 *   备注：继承抽象类IGroupDAO
			 * Edit BY Maming 2005-12-9
			 ***************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// 管理用户组的数据访问对象的SQLServer版本。实现接口IGroupDAO。
	/// </summary>
	internal class GroupDAOImplSQLServer : GroupDAO
	{
		/// <summary>
		/// 创建管理用户组的数据访问对象。
		/// </summary>
		public GroupDAOImplSQLServer()
		{
		}

		/// <summary>
		/// 添加用户组
		/// </summary>
		/// <param name="groupName">组名称</param>
		/// <returns>组编号</returns>
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
