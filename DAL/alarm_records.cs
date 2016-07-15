using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:alarm_records
	/// </summary>
	public partial class alarm_records
	{
		public alarm_records()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperMySQL.GetMaxID("ALARM_RECORD_ID", "gunark_alarm_records"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ALARM_RECORD_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gunark_alarm_records");
			strSql.Append(" where ALARM_RECORD_ID=@ALARM_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ALARM_RECORD_ID", MySqlDbType.Int32,10)			};
			parameters[0].Value = ALARM_RECORD_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.alarm_records model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gunark_alarm_records(");
			strSql.Append("ALARM_RECORD_ID,GUNARK_ID,UNIT_ID,ALARM_TYPE,ALARM_BEGIN_TIME,ALARM_DESCRIPTION,ALARM_HANDLE_TIME,ALARM_HANDLE_USERID,ALARM_HANDLE_TYPE,ALARM_ATATE,TASK_ID,ALARM_FINISH_TIME,ALARM_COUNT)");
			strSql.Append(" values (");
			strSql.Append("@ALARM_RECORD_ID,@GUNARK_ID,@UNIT_ID,@ALARM_TYPE,@ALARM_BEGIN_TIME,@ALARM_DESCRIPTION,@ALARM_HANDLE_TIME,@ALARM_HANDLE_USERID,@ALARM_HANDLE_TYPE,@ALARM_ATATE,@TASK_ID,@ALARM_FINISH_TIME,@ALARM_COUNT)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ALARM_RECORD_ID", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_BEGIN_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_DESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_HANDLE_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_HANDLE_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_HANDLE_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_ATATE", MySqlDbType.Int32,10),
					new MySqlParameter("@TASK_ID", MySqlDbType.Int32,10),
					new MySqlParameter("@ALARM_FINISH_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_COUNT", MySqlDbType.Int32,10)};
			parameters[0].Value = model.ALARM_RECORD_ID;
			parameters[1].Value = model.GUNARK_ID;
			parameters[2].Value = model.UNIT_ID;
			parameters[3].Value = model.ALARM_TYPE;
			parameters[4].Value = model.ALARM_BEGIN_TIME;
			parameters[5].Value = model.ALARM_DESCRIPTION;
			parameters[6].Value = model.ALARM_HANDLE_TIME;
			parameters[7].Value = model.ALARM_HANDLE_USERID;
			parameters[8].Value = model.ALARM_HANDLE_TYPE;
			parameters[9].Value = model.ALARM_ATATE;
			parameters[10].Value = model.TASK_ID;
			parameters[11].Value = model.ALARM_FINISH_TIME;
			parameters[12].Value = model.ALARM_COUNT;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Gunark.Model.alarm_records model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gunark_alarm_records set ");
			strSql.Append("GUNARK_ID=@GUNARK_ID,");
			strSql.Append("UNIT_ID=@UNIT_ID,");
			strSql.Append("ALARM_TYPE=@ALARM_TYPE,");
			strSql.Append("ALARM_BEGIN_TIME=@ALARM_BEGIN_TIME,");
			strSql.Append("ALARM_DESCRIPTION=@ALARM_DESCRIPTION,");
			strSql.Append("ALARM_HANDLE_TIME=@ALARM_HANDLE_TIME,");
			strSql.Append("ALARM_HANDLE_USERID=@ALARM_HANDLE_USERID,");
			strSql.Append("ALARM_HANDLE_TYPE=@ALARM_HANDLE_TYPE,");
			strSql.Append("ALARM_ATATE=@ALARM_ATATE,");
			strSql.Append("TASK_ID=@TASK_ID,");
			strSql.Append("ALARM_FINISH_TIME=@ALARM_FINISH_TIME,");
			strSql.Append("ALARM_COUNT=@ALARM_COUNT");
			strSql.Append(" where ALARM_RECORD_ID=@ALARM_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_BEGIN_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_DESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_HANDLE_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_HANDLE_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_HANDLE_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_ATATE", MySqlDbType.Int32,10),
					new MySqlParameter("@TASK_ID", MySqlDbType.Int32,10),
					new MySqlParameter("@ALARM_FINISH_TIME", MySqlDbType.Time),
					new MySqlParameter("@ALARM_COUNT", MySqlDbType.Int32,10),
					new MySqlParameter("@ALARM_RECORD_ID", MySqlDbType.Int32,10)};
			parameters[0].Value = model.GUNARK_ID;
			parameters[1].Value = model.UNIT_ID;
			parameters[2].Value = model.ALARM_TYPE;
			parameters[3].Value = model.ALARM_BEGIN_TIME;
			parameters[4].Value = model.ALARM_DESCRIPTION;
			parameters[5].Value = model.ALARM_HANDLE_TIME;
			parameters[6].Value = model.ALARM_HANDLE_USERID;
			parameters[7].Value = model.ALARM_HANDLE_TYPE;
			parameters[8].Value = model.ALARM_ATATE;
			parameters[9].Value = model.TASK_ID;
			parameters[10].Value = model.ALARM_FINISH_TIME;
			parameters[11].Value = model.ALARM_COUNT;
			parameters[12].Value = model.ALARM_RECORD_ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ALARM_RECORD_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_alarm_records ");
			strSql.Append(" where ALARM_RECORD_ID=@ALARM_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ALARM_RECORD_ID", MySqlDbType.Int32,10)			};
			parameters[0].Value = ALARM_RECORD_ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ALARM_RECORD_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_alarm_records ");
			strSql.Append(" where ALARM_RECORD_ID in ("+ALARM_RECORD_IDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Gunark.Model.alarm_records GetModel(int ALARM_RECORD_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ALARM_RECORD_ID,GUNARK_ID,UNIT_ID,ALARM_TYPE,ALARM_BEGIN_TIME,ALARM_DESCRIPTION,ALARM_HANDLE_TIME,ALARM_HANDLE_USERID,ALARM_HANDLE_TYPE,ALARM_ATATE,TASK_ID,ALARM_FINISH_TIME,ALARM_COUNT from gunark_alarm_records ");
			strSql.Append(" where ALARM_RECORD_ID=@ALARM_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ALARM_RECORD_ID", MySqlDbType.Int32,10)			};
			parameters[0].Value = ALARM_RECORD_ID;

			Gunark.Model.alarm_records model=new Gunark.Model.alarm_records();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Gunark.Model.alarm_records DataRowToModel(DataRow row)
		{
			Gunark.Model.alarm_records model=new Gunark.Model.alarm_records();
			if (row != null)
			{
				if(row["ALARM_RECORD_ID"]!=null && row["ALARM_RECORD_ID"].ToString()!="")
				{
					model.ALARM_RECORD_ID=int.Parse(row["ALARM_RECORD_ID"].ToString());
				}
				if(row["GUNARK_ID"]!=null)
				{
					model.GUNARK_ID=row["GUNARK_ID"].ToString();
				}
				if(row["UNIT_ID"]!=null)
				{
					model.UNIT_ID=row["UNIT_ID"].ToString();
				}
				if(row["ALARM_TYPE"]!=null)
				{
					model.ALARM_TYPE=row["ALARM_TYPE"].ToString();
				}
				if(row["ALARM_BEGIN_TIME"]!=null && row["ALARM_BEGIN_TIME"].ToString()!="")
				{
					model.ALARM_BEGIN_TIME=DateTime.Parse(row["ALARM_BEGIN_TIME"].ToString());
				}
				if(row["ALARM_DESCRIPTION"]!=null)
				{
					model.ALARM_DESCRIPTION=row["ALARM_DESCRIPTION"].ToString();
				}
				if(row["ALARM_HANDLE_TIME"]!=null && row["ALARM_HANDLE_TIME"].ToString()!="")
				{
					model.ALARM_HANDLE_TIME=DateTime.Parse(row["ALARM_HANDLE_TIME"].ToString());
				}
				if(row["ALARM_HANDLE_USERID"]!=null)
				{
					model.ALARM_HANDLE_USERID=row["ALARM_HANDLE_USERID"].ToString();
				}
				if(row["ALARM_HANDLE_TYPE"]!=null)
				{
					model.ALARM_HANDLE_TYPE=row["ALARM_HANDLE_TYPE"].ToString();
				}
				if(row["ALARM_ATATE"]!=null && row["ALARM_ATATE"].ToString()!="")
				{
					model.ALARM_ATATE=int.Parse(row["ALARM_ATATE"].ToString());
				}
				if(row["TASK_ID"]!=null && row["TASK_ID"].ToString()!="")
				{
					model.TASK_ID=int.Parse(row["TASK_ID"].ToString());
				}
				if(row["ALARM_FINISH_TIME"]!=null && row["ALARM_FINISH_TIME"].ToString()!="")
				{
					model.ALARM_FINISH_TIME=DateTime.Parse(row["ALARM_FINISH_TIME"].ToString());
				}
				if(row["ALARM_COUNT"]!=null && row["ALARM_COUNT"].ToString()!="")
				{
					model.ALARM_COUNT=int.Parse(row["ALARM_COUNT"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ALARM_RECORD_ID,GUNARK_ID,UNIT_ID,ALARM_TYPE,ALARM_BEGIN_TIME,ALARM_DESCRIPTION,ALARM_HANDLE_TIME,ALARM_HANDLE_USERID,ALARM_HANDLE_TYPE,ALARM_ATATE,TASK_ID,ALARM_FINISH_TIME,ALARM_COUNT ");
			strSql.Append(" FROM gunark_alarm_records ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM gunark_alarm_records ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ALARM_RECORD_ID desc");
			}
			strSql.Append(")AS Row, T.*  from gunark_alarm_records T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "gunark_alarm_records";
			parameters[1].Value = "ALARM_RECORD_ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

