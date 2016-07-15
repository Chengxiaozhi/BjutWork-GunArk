using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:gun_records
	/// </summary>
	public partial class gun_records
	{
		public gun_records()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GUNARK_RECORD_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gun_records");
			strSql.Append(" where GUNARK_RECORD_ID=@GUNARK_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_RECORD_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUNARK_RECORD_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.gun_records model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gun_records(");
			strSql.Append("GUNARK_RECORD_ID,RECORD_BULLET_TYPE,RECORD_GUN_UNM,RECORD_LEND_TIME,RECORD_RETURN_TIME)");
			strSql.Append(" values (");
			strSql.Append("@GUNARK_RECORD_ID,@RECORD_BULLET_TYPE,@RECORD_GUN_UNM,@RECORD_LEND_TIME,@RECORD_RETURN_TIME)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_RECORD_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@RECORD_BULLET_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@RECORD_GUN_UNM", MySqlDbType.Int32,10),
					new MySqlParameter("@RECORD_LEND_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@RECORD_RETURN_TIME", MySqlDbType.DateTime)};
			parameters[0].Value = model.GUNARK_RECORD_ID;
			parameters[1].Value = model.RECORD_BULLET_TYPE;
			parameters[2].Value = model.RECORD_GUN_UNM;
			parameters[3].Value = model.RECORD_LEND_TIME;
			parameters[4].Value = model.RECORD_RETURN_TIME;

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
		public bool Update(Gunark.Model.gun_records model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gun_records set ");
			strSql.Append("RECORD_BULLET_TYPE=@RECORD_BULLET_TYPE,");
			strSql.Append("RECORD_GUN_UNM=@RECORD_GUN_UNM,");
			strSql.Append("RECORD_LEND_TIME=@RECORD_LEND_TIME,");
			strSql.Append("RECORD_RETURN_TIME=@RECORD_RETURN_TIME");
			strSql.Append(" where GUNARK_RECORD_ID=@GUNARK_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RECORD_BULLET_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@RECORD_GUN_UNM", MySqlDbType.Int32,10),
					new MySqlParameter("@RECORD_LEND_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@RECORD_RETURN_TIME", MySqlDbType.DateTime),
					new MySqlParameter("@GUNARK_RECORD_ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.RECORD_BULLET_TYPE;
			parameters[1].Value = model.RECORD_GUN_UNM;
			parameters[2].Value = model.RECORD_LEND_TIME;
			parameters[3].Value = model.RECORD_RETURN_TIME;
			parameters[4].Value = model.GUNARK_RECORD_ID;

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
		public bool Delete(string GUNARK_RECORD_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gun_records ");
			strSql.Append(" where GUNARK_RECORD_ID=@GUNARK_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_RECORD_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUNARK_RECORD_ID;

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
		public bool DeleteList(string GUNARK_RECORD_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gun_records ");
			strSql.Append(" where GUNARK_RECORD_ID in ("+GUNARK_RECORD_IDlist + ")  ");
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
		public Gunark.Model.gun_records GetModel(string GUNARK_RECORD_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GUNARK_RECORD_ID,RECORD_BULLET_TYPE,RECORD_GUN_UNM,RECORD_LEND_TIME,RECORD_RETURN_TIME from gun_records ");
			strSql.Append(" where GUNARK_RECORD_ID=@GUNARK_RECORD_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_RECORD_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUNARK_RECORD_ID;

			Gunark.Model.gun_records model=new Gunark.Model.gun_records();
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
		public Gunark.Model.gun_records DataRowToModel(DataRow row)
		{
			Gunark.Model.gun_records model=new Gunark.Model.gun_records();
			if (row != null)
			{
				if(row["GUNARK_RECORD_ID"]!=null)
				{
					model.GUNARK_RECORD_ID=row["GUNARK_RECORD_ID"].ToString();
				}
				if(row["RECORD_BULLET_TYPE"]!=null)
				{
					model.RECORD_BULLET_TYPE=row["RECORD_BULLET_TYPE"].ToString();
				}
				if(row["RECORD_GUN_UNM"]!=null && row["RECORD_GUN_UNM"].ToString()!="")
				{
					model.RECORD_GUN_UNM=int.Parse(row["RECORD_GUN_UNM"].ToString());
				}
				if(row["RECORD_LEND_TIME"]!=null && row["RECORD_LEND_TIME"].ToString()!="")
				{
					model.RECORD_LEND_TIME=DateTime.Parse(row["RECORD_LEND_TIME"].ToString());
				}
				if(row["RECORD_RETURN_TIME"]!=null && row["RECORD_RETURN_TIME"].ToString()!="")
				{
					model.RECORD_RETURN_TIME=DateTime.Parse(row["RECORD_RETURN_TIME"].ToString());
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
			strSql.Append("select GUNARK_RECORD_ID,RECORD_BULLET_TYPE,RECORD_GUN_UNM,RECORD_LEND_TIME,RECORD_RETURN_TIME ");
			strSql.Append(" FROM gun_records ");
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
			strSql.Append("select count(1) FROM gun_records ");
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
				strSql.Append("order by T.GUNARK_RECORD_ID desc");
			}
			strSql.Append(")AS Row, T.*  from gun_records T ");
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
			parameters[0].Value = "gun_records";
			parameters[1].Value = "GUNARK_RECORD_ID";
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

