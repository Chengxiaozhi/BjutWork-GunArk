using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:admin
	/// </summary>
	public partial class admin
	{
		public admin()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gunark_admin");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gunark_admin(");
			strSql.Append("ID,ADMIN_KIND,ADMIN_DUTYBEGIN,ADMIN_DUTYEND,ADMIN_STATUS,USERID,ADMIN_ID)");
			strSql.Append(" values (");
			strSql.Append("@ID,@ADMIN_KIND,@ADMIN_DUTYBEGIN,@ADMIN_DUTYEND,@ADMIN_STATUS,@USERID,@ADMIN_ID)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_KIND", MySqlDbType.VarChar,10),
					new MySqlParameter("@ADMIN_DUTYBEGIN", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_DUTYEND", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.ADMIN_KIND;
			parameters[2].Value = model.ADMIN_DUTYBEGIN;
			parameters[3].Value = model.ADMIN_DUTYEND;
			parameters[4].Value = model.ADMIN_STATUS;
			parameters[5].Value = model.USERID;
			parameters[6].Value = model.ADMIN_ID;

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
		public bool Update(Gunark.Model.admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gunark_admin set ");
			strSql.Append("ADMIN_KIND=@ADMIN_KIND,");
			strSql.Append("ADMIN_DUTYBEGIN=@ADMIN_DUTYBEGIN,");
			strSql.Append("ADMIN_DUTYEND=@ADMIN_DUTYEND,");
			strSql.Append("ADMIN_STATUS=@ADMIN_STATUS,");
			strSql.Append("USERID=@USERID,");
			strSql.Append("ADMIN_ID=@ADMIN_ID");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ADMIN_KIND", MySqlDbType.VarChar,10),
					new MySqlParameter("@ADMIN_DUTYBEGIN", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_DUTYEND", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ADMIN_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.ADMIN_KIND;
			parameters[1].Value = model.ADMIN_DUTYBEGIN;
			parameters[2].Value = model.ADMIN_DUTYEND;
			parameters[3].Value = model.ADMIN_STATUS;
			parameters[4].Value = model.USERID;
			parameters[5].Value = model.ADMIN_ID;
			parameters[6].Value = model.ID;

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
		public bool Delete(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_admin ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_admin ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Gunark.Model.admin GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,ADMIN_KIND,ADMIN_DUTYBEGIN,ADMIN_DUTYEND,ADMIN_STATUS,USERID,ADMIN_ID from gunark_admin ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = ID;

			Gunark.Model.admin model=new Gunark.Model.admin();
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
		public Gunark.Model.admin DataRowToModel(DataRow row)
		{
			Gunark.Model.admin model=new Gunark.Model.admin();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["ADMIN_KIND"]!=null)
				{
					model.ADMIN_KIND=row["ADMIN_KIND"].ToString();
				}
				if(row["ADMIN_DUTYBEGIN"]!=null)
				{
					model.ADMIN_DUTYBEGIN=row["ADMIN_DUTYBEGIN"].ToString();
				}
				if(row["ADMIN_DUTYEND"]!=null)
				{
					model.ADMIN_DUTYEND=row["ADMIN_DUTYEND"].ToString();
				}
				if(row["ADMIN_STATUS"]!=null && row["ADMIN_STATUS"].ToString()!="")
				{
					model.ADMIN_STATUS=int.Parse(row["ADMIN_STATUS"].ToString());
				}
				if(row["USERID"]!=null)
				{
					model.USERID=row["USERID"].ToString();
				}
				if(row["ADMIN_ID"]!=null)
				{
					model.ADMIN_ID=row["ADMIN_ID"].ToString();
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
			strSql.Append("select ID,ADMIN_KIND,ADMIN_DUTYBEGIN,ADMIN_DUTYEND,ADMIN_STATUS,USERID,ADMIN_ID ");
			strSql.Append(" FROM gunark_admin ");
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
			strSql.Append("select count(1) FROM gunark_admin ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from gunark_admin T ");
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
			parameters[0].Value = "gunark_admin";
			parameters[1].Value = "ID";
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

