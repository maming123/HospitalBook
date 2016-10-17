



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `Core`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=HospitalBook;User ID=mm;Password=mm`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Module.Models
{

	public partial class CoreDB : Database
	{
		public CoreDB() 
			: base("Core")
		{
			CommonConstruct();
		}

		public CoreDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			CoreDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static CoreDB GetInstance()
        {
			//http://stackoverflow.com/questions/7052350/how-to-create-a-dal-using-petapoco/9995413#9995413
            //If you are using this in a web application then you should instantiate one PetaPoco database per request.

			/*if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else*/
				return new CoreDB();
        }

		[ThreadStatic] static CoreDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static CoreDB repo { get { return CoreDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    
	[TableName("Sys_Point")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_Point : CoreDB.Record<Sys_Point>  
    {



		[Column] public int Id { get; set; }





		[Column] public int ModulelId { get; set; }





		[Column] public string Content { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }





		[Column] public DateTime? UpdateDateTime { get; set; }



	}

    
	[TableName("Sys_AdminUser")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_AdminUser : CoreDB.Record<Sys_AdminUser>  
    {



		[Column] public int Id { get; set; }





		[Column] public string LoginUserName { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public string PassWord { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("User")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class User : CoreDB.Record<User>  
    {



		[Column] public int Id { get; set; }





		[Column] public long Mobile { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public string PassWord { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string RegistCode { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("UserRegistLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class UserRegistLog : CoreDB.Record<UserRegistLog>  
    {



		[Column] public int Id { get; set; }





		[Column] public long Mobile { get; set; }





		[Column] public string NickName { get; set; }





		[Column] public string PassWord { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string RegistCode { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("Log")]


	[PrimaryKey("log_id")]



	[ExplicitColumns]
    public partial class Log : CoreDB.Record<Log>  
    {



		[Column] public int log_id { get; set; }





		[Column] public DateTime LogDate { get; set; }





		[Column] public string descript { get; set; }





		[Column] public short logtype { get; set; }





		[Column] public string log_info { get; set; }





		[Column] public string Module_Name { get; set; }



	}

    
	[TableName("Sys_RegistCode")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class Sys_RegistCode : CoreDB.Record<Sys_RegistCode>  
    {



		[Column] public int Id { get; set; }





		[Column] public int BookId { get; set; }





		[Column] public string RegistCode { get; set; }





		[Column] public int IsEnable { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}

    
	[TableName("Sys_Module")]


	[PrimaryKey("MODULE_ID")]



	[ExplicitColumns]
    public partial class Sys_Module : CoreDB.Record<Sys_Module>  
    {



		[Column] public int MODULE_ID { get; set; }





		[Column] public string MODULE_NAME { get; set; }





		[Column] public int? PARENT_MODULE_ID { get; set; }





		[Column] public byte IS_DISPLAY { get; set; }





		[Column] public short? ORDER_ID { get; set; }





		[Column] public short? Banner_Button_Width { get; set; }





		[Column] public string Link_Path { get; set; }





		[Column] public byte Link_Type { get; set; }





		[Column] public string Link_Target { get; set; }





		[Column] public byte Create_Table { get; set; }





		[Column] public int? Template_ID { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string Business_Table { get; set; }





		[Column] public bool Is_Sys_File { get; set; }





		[Column] public bool Is_Original_Data { get; set; }





		[Column] public string Creator_Area_Code { get; set; }



	}

    
	[TableName("UserLoginLog")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class UserLoginLog : CoreDB.Record<UserLoginLog>  
    {



		[Column] public int Id { get; set; }





		[Column] public long Mobile { get; set; }





		[Column] public int BookId { get; set; }





		[Column] public DateTime CreateDatetime { get; set; }



	}

    
	[TableName("UserScore")]


	[PrimaryKey("Id")]



	[ExplicitColumns]
    public partial class UserScore : CoreDB.Record<UserScore>  
    {



		[Column] public int Id { get; set; }





		[Column] public string UserName { get; set; }





		[Column] public int Score { get; set; }





		[Column] public int ShortDate { get; set; }





		[Column] public DateTime CreateDateTime { get; set; }



	}


}



