using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SNS.Library.Database;


			/*************************************
			 * �����ƣ�GroupDAO
			 *   ���ܣ������û�������ݷ��ʶ����
			 *		   ������
			 *     by��Lining
			 *   ���ڣ�2004-11-15
			 * Edit BY Maming 2005-12-9
             * Edit By Maming 2006-3-8
             * Edit By Maming 2006-5-26
			 ************************************/


namespace SNS.Library.SystemUsers
{
	/// <summary>
	/// �����û�������ݷ��ʶ���ӿ�
	/// </summary>
	public abstract class GroupDAO
	{
		#region ����

		/// <summary>
		/// ����û���
		/// </summary>
		/// <param name="groupName">������</param>
		/// <returns>����</returns>
		public abstract string Add(string groupName);

		/// <summary>
		/// ��ѯ���е���
		/// </summary>
		/// <param name="userMaxLevel">�û���������߼���</param>
		/// <returns>����󼯺�</returns>
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
		/// ɾ���û���
		/// </summary>
		/// <param name="groupID">����</param>
		public void Delete(string groupID)
		{
			string strSql = "DELETE FROM FU_Group WHERE GROUP_ID=" + groupID;
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// ������ָ�����û���
		/// </summary>
		/// <param name="groupID">����</param>
		/// <param name="newName">�µ�������</param>
		public void Rename(string groupID,string newName)
		{
			string strSql = "UPDATE FU_Group SET GROUP_NAME='" + newName + "' WHERE GROUP_ID=" +
				groupID;
			DatabaseFactory.ExecuteNonQuery(strSql);
		}

		/// <summary>
		/// �������Ų�ѯ
		/// </summary>
		/// <param name="groupID">����</param>
		/// <returns>�����</returns>
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
		/// ����Ȩ�� Edit By Maming 2006-3-8 ��2006-3-31
		/// </summary>
		/// <param name="groupID">����</param>
		/// <param name="rights">�û�Ȩ�޼���</param>
		public void AssignRight(string groupID,ArrayList rights)
        {
            #region
            IDatabase database = DatabaseFactory.CreateObject();

            try
            {
                //����µ�Ȩ��
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
                    //��ɾ����ǰ��Ȩ��
                    database.ExecuteNonQuery(strDeleteSql);

                    //����µ�Ȩ��
                    if (userRight.PossessRight)
                    {
                        database.ExecuteNonQuery(strInsertSql);
                    }
                    database.Parameters.Clear();
                }

                //�ύ����
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
		/// �ж�ָ�������Ƿ��ָ����ģ����Ȩ��
		/// </summary>
		/// <param name="groupID">����</param>
		/// <param name="moduleID">ģ����</param>
		/// <returns>bool--��Ȩ�ޣ�false--��Ȩ��</returns>
		public bool CheckRight(string groupID,int moduleID)
		{
			string strSql = "SELECT COUNT(*) FROM FU_User_Right WHERE User_ID=" + groupID +
				" AND Module_ID=" + moduleID;
			return (Convert.ToInt32(DatabaseFactory.ExecuteScalar(strSql)) > 0);
		}

		/// <summary>
		/// �õ�ָ���û����Ȩ�޵�XML������ʽ
		/// </summary>
		/// <param name="groupID">����</param>
		/// <returns>ָ���û����Ȩ�޵�XML������ʽ</returns>
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
		/// �������Ա
		/// </summary>
		/// <param name="groupID">����</param>
		/// <param name="membersID">������Ա�ı�ż���</param>
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
		/// ��ѯ��������Ϣ ��д���� By Maming 2005-10-17
		/// </summary>
		/// <returns>��������Ϣ��DataTable</returns>
		public DataTable FindAll()
		{
			return FindAllByLevel(-1);
		}

		/// <summary>
		/// ֻ��ѯ��ָ�����鼶��ͺ͵���ָ�����������Ϣ  ��д���� By Maming 2005-10-17
		/// </summary>
		/// <param name="groupLevel">�鼶��</param>
		/// <param name="userID">�û�ID</param>
		/// <returns>��������Ϣ��DataTable</returns>
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
		/// ����ļ������һ��  By Maming 2005-10-17
		/// </summary>
		/// <param name="iGroupID">���GroupID</param>
		public void UpGroupLevel(int iGroupID)
		{
			this.GroupLevelChange(iGroupID,"UP");
		}

		/// <summary>
		/// ����ļ��𽵵�һ��  By Maming 2005-10-17
		/// </summary>
		/// <param name="iGroupID">���GroupID</param>
		public void DownGroupLevel(int iGroupID)
		{
			this.GroupLevelChange(iGroupID,"DOWN");
		}

		/// <summary>
		/// ���ݲ�ͬ�Ķ�����UP��DOWN���ı���ļ���  By Maming 2005-10-17 
		/// �����ƶ���up����ȡ��һ��Group_Level ==3 ��������Ϊ 3 ����һ������Ϊ 3+1
		/// �����ƶ���down����ȡ����Group_Level ==3 ����Group_Level==3+1���ѱ���ԭ��Group_Level==3������һ��
		/// </summary>
		/// <param name="iGroupID">���GroupID</param>
		/// <param name="strAction">������ʽ</param>
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
					//����GroupID
					int GroupIDThis = int.Parse(dtUP.Rows[iFlag]["Group_ID"].ToString());
					//����GroupID
					int GroupIDTop  =  int.Parse(dtUP.Rows[iFlag-1]["Group_ID"].ToString());
					//����GroupLevel
					int iTopLevel =int.Parse(dtUP.Rows[iFlag-1]["Group_Level"].ToString());
					using(IDatabase database = DatabaseFactory.CreateObject())
					{	
						try
						{
							//����Group_Level����Ϊ��һ����Levelֵ
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
					//����GroupID
					int GroupIDThisDOWN = int.Parse(dtUP.Rows[iFlag]["Group_ID"].ToString());
					//����GroupID
					int GroupIDNext  =  int.Parse(dtUP.Rows[iFlag+1]["Group_ID"].ToString());
					//����GroupLevel
					int iThisLevel =int.Parse(dtUP.Rows[iFlag]["Group_Level"].ToString());
					using(IDatabase database = DatabaseFactory.CreateObject())
					{	
						try
						{
							//����Group_Level����Ϊ������Levelֵ+1
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
		/// �ж��û����д��ڲ����Ա new Method By maming 2005-11-10
		/// </summary>
		/// <param name="groupID">��ID</param>
		/// <returns>true-���ڳ�Ա����ɾ����  false--�����ڿ���ɾ��</returns>
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


		#endregion ��������ӷ��� By Maming

        #region New Method By Maming 2006-3-8

        /// <summary>
        /// �ж�ָ�������Ƿ��ָ����ģ���о���Ĳ���Ȩ��
        /// </summary>
        /// <param name="groupID">����</param>
        /// <param name="moduleID">ģ����</param>
        /// <returns>bool--��Ȩ�ޣ�false--��Ȩ��</returns>
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
        /// ͨ��UserID���Ҹ��û������ĸ���
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

        #endregion ��������
    }
}
