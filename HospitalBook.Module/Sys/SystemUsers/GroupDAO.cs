using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SNS.Library.Database;


			/*************************************
			 * 类名称：GroupDAO
			 *   功能：管理用户组的数据访问对象的
			 *		   抽象类
			 *     by：Lining
			 *   日期：2004-11-15
			 * Edit BY Maming 2005-12-9
             * Edit By Maming 2006-3-8
             * Edit By Maming 2006-5-26
			 ************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// 管理用户组的数据访问对象接口
	/// </summary>
	public abstract class GroupDAO
	{
		#region 方法

		/// <summary>
		/// 添加用户组
		/// </summary>
		/// <param name="groupName">组名称</param>
		/// <returns>组编号</returns>
		public abstract string Add(string groupName);

		/// <summary>
		/// 查询所有的组
		/// </summary>
		/// <param name="userMaxLevel">用户所属的最高级别</param>
		/// <returns>组对象集合</returns>
		public ArrayList FindAll(int userMaxLevel)
		{
			string strSql = "SELECT GROUP_ID,GROUP_NAME FROM FU_Group WHERE IS_ADMIN<=" + userMaxLevel;
			DataTable dtGroups = DatabaseFactory.ExecuteQuery(strSql);

			if(dtGroups.Rows.Count > 0)
			{
				ArrayList groups = new ArrayList(dtGroups.Rows.Count);
				foreach(DataRow drGroup in dtGroups.Rows)
				{
					Group group = new Group();
					group.ID = drGroup[0].ToString();
					group.Name = drGroup[1].ToString();
					groups.Add(group);
				}
				return groups;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 删除用户组
		/// </summary>
		/// <param name="groupID">组编号</param>
		public void Delete(string groupID)
		{
			string strSql = "DELETE FROM FU_Group WHERE GROUP_ID=" + groupID;
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// 重命名指定的用户组
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <param name="newName">新的组名称</param>
		public void Rename(string groupID,string newName)
		{
			string strSql = "UPDATE FU_Group SET GROUP_NAME='" + newName + "' WHERE GROUP_ID=" +
				groupID;
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// 按照组编号查询
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <returns>组对象</returns>
		public Group FindByID(string groupID)
		{
			string strSql = "SELECT GROUP_NAME,Is_Admin,ForRegUser FROM FU_Group WHERE GROUP_ID=" + groupID;
			DataTable dtGroup = DatabaseFactory.ExecuteQuery(strSql);

			if(dtGroup.Rows.Count > 0)
			{
				Group group = new Group();
				group.ID = groupID;
				group.Name = dtGroup.Rows[0][0].ToString();
                group.Is_Admin = dtGroup.Rows[0][1].ToString();
                group.ForRegUser = dtGroup.Rows[0][2].ToString();
				return group;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 分配权限 Edit By Maming 2006-3-8 、2006-3-31
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <param name="rights">用户权限集合</param>
		public void AssignRight(string groupID,ArrayList rights)
        {
            #region
            IDatabase database = DatabaseFactory.CreateObject();

            try
            {
                //添加新的权限
                string strInsertSql = "INSERT INTO FU_User_Right([User_ID],Module_ID,Function_ID,[Edit],[Add],[Delete],[Manage]) VALUES(" 
                                        + groupID + ",@ModuleID,@FunctionID,@Edit,@Add,@Delete,@Manage)";
                string strDeleteSql = "DELETE FROM FU_User_Right WHERE User_ID=" + groupID + " AND Module_ID=" +
                    "@ModuleID AND Function_ID=@FunctionID";

                database.BeginTransaction();
                for (int i = 0; i < rights.Count; i++)
                {
                    UserRight userRight = (UserRight)rights[i];

                    database.Parameters.Clear();

                    SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int);
                    parameterModuleID.Value = userRight.ModuleID;
                    database.Parameters.Add(parameterModuleID);

                    SqlParameter parameterFunctionID = new SqlParameter("@FunctionID", SqlDbType.Int);
                    parameterFunctionID.Value = userRight.FunctionID;
                    database.Parameters.Add(parameterFunctionID);

                    SqlParameter parameterEdit = new SqlParameter("@Edit", SqlDbType.Int);
                    parameterEdit.Value = Convert.ToInt32(userRight.EditRight);
                    database.Parameters.Add(parameterEdit);

                    SqlParameter parameterAdd = new SqlParameter("@Add", SqlDbType.Int);
                    parameterAdd.Value = Convert.ToInt32(userRight.AddRight);
                    database.Parameters.Add(parameterAdd);

                    SqlParameter parameterDelete = new SqlParameter("@Delete", SqlDbType.Int);
                    parameterDelete.Value = Convert.ToInt32(userRight.DeleteRight);
                    database.Parameters.Add(parameterDelete);

                    SqlParameter parameterManage = new SqlParameter("@Manage", SqlDbType.Int);
                    parameterManage.Value = Convert.ToInt32(userRight.ManageRight);
                    database.Parameters.Add(parameterManage);
                    //先删除以前的权限
                    database.ExecuteNonQuery(strDeleteSql);

                    //添加新的权限
                    if (userRight.PossessRight)
                    {
                        database.ExecuteNonQuery(strInsertSql);
                    }
                    database.Parameters.Clear();
                }

                //提交事务
                database.Commit();
            }
            catch (Exception exc)
            {
                database.Rollback();
                throw exc;
            }
            finally
            {
                database.Close();
            }

            #endregion
        }

		/// <summary>
		/// 判断指定的组是否对指定的模块有权限
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <param name="moduleID">模块编号</param>
		/// <returns>bool--有权限；false--无权限</returns>
		public bool CheckRight(string groupID,int moduleID)
		{
			string strSql = "SELECT COUNT(*) FROM FU_User_Right WHERE User_ID=" + groupID +
				" AND Module_ID=" + moduleID;
			return (Convert.ToInt32(DatabaseFactory.ExecuteScalar(strSql)) > 0);
		}

		/// <summary>
		/// 得到指定用户组的权限的XML表现形式
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <returns>指定用户组的权限的XML表现形式</returns>
		public string GetRightXml(string groupID)
		{
			IDatabase database = DatabaseFactory.CreateObject();

			try
			{
				string strSql = "SELECT MODULE_ID,FUNCTION_ID FROM FU_User_Right WHERE USER_ID=" + groupID;
				DataTable dtRights = new DataTable("Right");
				database.ExecuteQuery(strSql,dtRights);

				DataSet dsRights = new DataSet("Rights");
				dsRights.Tables.Add(dtRights);
				return dsRights.GetXml();
			}
			finally
			{
				database.Close();
			}
		}



		/// <summary>
		/// 分配组成员
		/// </summary>
		/// <param name="groupID">组编号</param>
		/// <param name="membersID">所属成员的编号集合</param>
		public void AssignMembers(string groupID,string[] membersID)
		{
			IDatabase database = DatabaseFactory.CreateObject();

			try
			{
				string strSql = "DELETE FROM FU_Group_USER WHERE GROUP_ID=" + groupID;
				database.BeginTransaction();
				database.ExecuteNonQuery(strSql);

				strSql = "INSERT INTO FU_Group_USER VALUES(" + groupID + ",@MemberID)";
				foreach(string strMemberID in membersID)
				{
					if(strMemberID.Trim() != "")
					{
						SqlParameter parameterUserID = new SqlParameter("@MemberID",SqlDbType.Int);
						parameterUserID.Value = strMemberID;
						database.Parameters.Add(parameterUserID);
						database.ExecuteNonQuery(strSql);
						database.Parameters.Clear();
					}
				}
				database.Commit();
			}
			catch(Exception exc)
			{
				database.Rollback();
				throw exc;
			}
			finally
			{
				database.Close();
			}
		}



		#region New Method By Maming  2005-12-9

		/// <summary>
		/// 查询所有组信息 新写方法 By Maming 2005-10-17
		/// </summary>
		/// <returns>含有组信息的DataTable</returns>
		public DataTable FindAll()
		{
			return FindAllByLevel(-1);
		}

		/// <summary>
		/// 只查询比指定的组级别低和等于指定级别的组信息  新写方法 By Maming 2005-10-17
		/// </summary>
		/// <param name="groupLevel">组级别</param>
		/// <param name="userID">用户ID</param>
		/// <returns>含有组信息的DataTable</returns>
		public DataTable FindAllByLevel(int groupLevel)
		{
			string strSql = "SELECT G.Group_ID as ID,G.Group_Name as Name,Group_Level,Is_Admin FROM FU_Group G";
			if(groupLevel > -1)
			{
				strSql += " Where Group_Name <> 'developer'  and Group_Level>=" + groupLevel;
			}
			strSql += " ORDER BY Group_Level";
			return DatabaseFactory.ExecuteQuery(strSql);
		}

		/// <summary>
		/// 把组的级别提高一级  By Maming 2005-10-17
		/// </summary>
		/// <param name="iGroupID">组的GroupID</param>
		public void UpGroupLevel(int iGroupID)
		{
			this.GroupLevelChange(iGroupID,"UP");
		}

		/// <summary>
		/// 把组的级别降低一级  By Maming 2005-10-17
		/// </summary>
		/// <param name="iGroupID">组的GroupID</param>
		public void DownGroupLevel(int iGroupID)
		{
			this.GroupLevelChange(iGroupID,"DOWN");
		}

		/// <summary>
		/// 根据不同的动作（UP，DOWN）改变组的级别  By Maming 2005-10-17 
		/// 向上移动（up）：取上一条Group_Level ==3 本条更新为 3 ，上一条更新为 3+1
		/// 向下移动（down）：取本条Group_Level ==3 本条Group_Level==3+1，把本条原来Group_Level==3复给下一条
		/// </summary>
		/// <param name="iGroupID">组的GroupID</param>
		/// <param name="strAction">动作方式</param>
		private void GroupLevelChange(int iGroupID,string strAction)
		{
			string strSql = "SELECT  * FROM FU_Group ORDER BY Group_Level";
			DataTable dtUP = DatabaseFactory.ExecuteQuery(strSql);
			int iFlag = 0;
			if(dtUP.Rows.Count>=2)
			{
				for(int i=0;i<dtUP.Rows.Count;i++)
				{
					if(iGroupID.ToString() ==dtUP.Rows[i]["Group_ID"].ToString())
					{
						iFlag = i;
						break;
					}
				}
			}
			switch(strAction)
			{
				case "UP": 
					//本条GroupID
					int GroupIDThis = int.Parse(dtUP.Rows[iFlag]["Group_ID"].ToString());
					//上条GroupID
					int GroupIDTop  =  int.Parse(dtUP.Rows[iFlag-1]["Group_ID"].ToString());
					//上条GroupLevel
					int iTopLevel =int.Parse(dtUP.Rows[iFlag-1]["Group_Level"].ToString());
					using(IDatabase database = DatabaseFactory.CreateObject())
					{	
						try
						{
							//本条Group_Level更新为上一条的Level值
							string strUpdate = "UPDATE FU_Group SET Group_Level ="+iTopLevel+" WHERE Group_ID ="+GroupIDThis+" ";
							strUpdate +="UPDATE FU_Group SET Group_Level ="+(iTopLevel+1)+" WHERE Group_ID ="+GroupIDTop+"";
							database.BeginTransaction();
							database.ExecuteNonQuery(strUpdate);
							database.Commit();
						}
						catch
						{
							database.Rollback();
						}
						finally
						{
							database.Close();
						}
					}
					break;
				case "DOWN":
					//本条GroupID
					int GroupIDThisDOWN = int.Parse(dtUP.Rows[iFlag]["Group_ID"].ToString());
					//下条GroupID
					int GroupIDNext  =  int.Parse(dtUP.Rows[iFlag+1]["Group_ID"].ToString());
					//本条GroupLevel
					int iThisLevel =int.Parse(dtUP.Rows[iFlag]["Group_Level"].ToString());
					using(IDatabase database = DatabaseFactory.CreateObject())
					{	
						try
						{
							//本条Group_Level更新为本条的Level值+1
							string strUpdate = "UPDATE FU_Group SET Group_Level ="+iThisLevel+" WHERE Group_ID ="+GroupIDNext+" ";
							strUpdate +="UPDATE FU_Group SET Group_Level ="+(iThisLevel+1)+" WHERE Group_ID ="+GroupIDThisDOWN+"";
							database.BeginTransaction();
							database.ExecuteNonQuery(strUpdate);
							database.Commit();
						}
						catch
						{
							database.Rollback();
						}
						finally
						{
							database.Close();
						}
					}
					break;
			}
		}

		/// <summary>
		/// 判断用户组中存在不存成员 new Method By maming 2005-11-10
		/// </summary>
		/// <param name="groupID">组ID</param>
		/// <returns>true-存在成员不能删除，  false--不存在可以删除</returns>
		public bool CheckContainUsers(string groupID)
		{
			string strSql = "select Count(*) from FU_Group_User where Group_id=" + groupID;
			object obj = DatabaseFactory.ExecuteScalar(strSql);
			if(int.Parse(obj.ToString()) >0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		#endregion 结束新添加方法 By Maming

        #region New Method By Maming 2006-3-8

        /// <summary>
        /// 判断指定的组是否对指定的模块有具体的操作权限
        /// </summary>
        /// <param name="groupID">组编号</param>
        /// <param name="moduleID">模块编号</param>
        /// <returns>bool--有权限；false--无权限</returns>
        public bool IsHaveCheckUserOpModuleRight(string groupID, string moduleID,UserRight.UserRightFunction userRightFunction)
        {
            string strSql = "SELECT [" + userRightFunction.ToString() + "] FROM FU_User_Right WHERE User_ID=" + groupID +
                " AND Module_ID=" + moduleID;
            object o = DatabaseFactory.ExecuteScalar(strSql);
            if (o != null)
            {
                 return (Convert.ToInt32(o) > 0);
            }
            else
            {
                return false;
            }
           
        }

        /// <summary>
        /// 通过UserID查找该用户属于哪个组
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string FindGroupIDByUserID(string UserID)
        {
            string strSql = "select Group_ID from FU_Group_User where User_ID="+UserID;
            object o = DatabaseFactory.ExecuteScalar(strSql);
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #endregion 结束方法
    }
}
