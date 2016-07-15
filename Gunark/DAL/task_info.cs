using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:task_info
	/// </summary>
	public partial class task_info
	{
		public task_info()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TASK_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gunark_task_info");
			strSql.Append(" where TASK_ID=@TASK_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = TASK_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.task_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gunark_task_info(");
			strSql.Append("TASK_ID,GUNARK_ID,UNIT_ID,TASK_STATUS,TASK_BIGTYPE,TASK_SMALLTYPE,TASK_PROPERTY,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_APPLY_USERID,TASK_APPLY_TIME,TASK_APPLY_REMARK,TASK_CHECK_USERID,TASK_CHECK_TIME,TASK_CHECK_REMARK,TASK_APPROVAL_USERID,TASK_APPROVAL_TIME,TASK_APPROVAL_REMARK,TASK_REAL_BEGINTIME,TASK_REAL_FINISHTIME,TASK_AFTERWARDS_HANDLE,TASK_AFTERWARDS_DESCRIPTION,TASK_AFTERWARDS_TIME,TASK_FLAG_TYPE,TAKE_GUNBULLET_ADMIN1,RETURN_GUNBULLET_ADMIN1,TAKE_GUNBULLET_ADMIN2,RETURN_GUNBULLET_ADMIN2)");
			strSql.Append(" values (");
			strSql.Append("@TASK_ID,@GUNARK_ID,@UNIT_ID,@TASK_STATUS,@TASK_BIGTYPE,@TASK_SMALLTYPE,@TASK_PROPERTY,@TASK_PLAN_BEGINTIME,@TASK_PLAN_FINISHTIME,@TASK_APPLY_USERID,@TASK_APPLY_TIME,@TASK_APPLY_REMARK,@TASK_CHECK_USERID,@TASK_CHECK_TIME,@TASK_CHECK_REMARK,@TASK_APPROVAL_USERID,@TASK_APPROVAL_TIME,@TASK_APPROVAL_REMARK,@TASK_REAL_BEGINTIME,@TASK_REAL_FINISHTIME,@TASK_AFTERWARDS_HANDLE,@TASK_AFTERWARDS_DESCRIPTION,@TASK_AFTERWARDS_TIME,@TASK_FLAG_TYPE,@TAKE_GUNBULLET_ADMIN1,@RETURN_GUNBULLET_ADMIN1,@TAKE_GUNBULLET_ADMIN2,@RETURN_GUNBULLET_ADMIN2)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_STATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_BIGTYPE", MySqlDbType.Int32,10),
					new MySqlParameter("@TASK_SMALLTYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PROPERTY", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PLAN_BEGINTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PLAN_FINISHTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_REAL_BEGINTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_REAL_FINISHTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_HANDLE", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_DESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_FLAG_TYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@TAKE_GUNBULLET_ADMIN1", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_ADMIN1", MySqlDbType.VarChar,255),
					new MySqlParameter("@TAKE_GUNBULLET_ADMIN2", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_ADMIN2", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.TASK_ID;
			parameters[1].Value = model.GUNARK_ID;
			parameters[2].Value = model.UNIT_ID;
			parameters[3].Value = model.TASK_STATUS;
			parameters[4].Value = model.TASK_BIGTYPE;
			parameters[5].Value = model.TASK_SMALLTYPE;
			parameters[6].Value = model.TASK_PROPERTY;
			parameters[7].Value = model.TASK_PLAN_BEGINTIME;
			parameters[8].Value = model.TASK_PLAN_FINISHTIME;
			parameters[9].Value = model.TASK_APPLY_USERID;
			parameters[10].Value = model.TASK_APPLY_TIME;
			parameters[11].Value = model.TASK_APPLY_REMARK;
			parameters[12].Value = model.TASK_CHECK_USERID;
			parameters[13].Value = model.TASK_CHECK_TIME;
			parameters[14].Value = model.TASK_CHECK_REMARK;
			parameters[15].Value = model.TASK_APPROVAL_USERID;
			parameters[16].Value = model.TASK_APPROVAL_TIME;
			parameters[17].Value = model.TASK_APPROVAL_REMARK;
			parameters[18].Value = model.TASK_REAL_BEGINTIME;
			parameters[19].Value = model.TASK_REAL_FINISHTIME;
			parameters[20].Value = model.TASK_AFTERWARDS_HANDLE;
			parameters[21].Value = model.TASK_AFTERWARDS_DESCRIPTION;
			parameters[22].Value = model.TASK_AFTERWARDS_TIME;
			parameters[23].Value = model.TASK_FLAG_TYPE;
			parameters[24].Value = model.TAKE_GUNBULLET_ADMIN1;
			parameters[25].Value = model.RETURN_GUNBULLET_ADMIN1;
			parameters[26].Value = model.TAKE_GUNBULLET_ADMIN2;
			parameters[27].Value = model.RETURN_GUNBULLET_ADMIN2;

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
		public bool Update(Gunark.Model.task_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gunark_task_info set ");
			strSql.Append("GUNARK_ID=@GUNARK_ID,");
			strSql.Append("UNIT_ID=@UNIT_ID,");
			strSql.Append("TASK_STATUS=@TASK_STATUS,");
			strSql.Append("TASK_BIGTYPE=@TASK_BIGTYPE,");
			strSql.Append("TASK_SMALLTYPE=@TASK_SMALLTYPE,");
			strSql.Append("TASK_PROPERTY=@TASK_PROPERTY,");
			strSql.Append("TASK_PLAN_BEGINTIME=@TASK_PLAN_BEGINTIME,");
			strSql.Append("TASK_PLAN_FINISHTIME=@TASK_PLAN_FINISHTIME,");
			strSql.Append("TASK_APPLY_USERID=@TASK_APPLY_USERID,");
			strSql.Append("TASK_APPLY_TIME=@TASK_APPLY_TIME,");
			strSql.Append("TASK_APPLY_REMARK=@TASK_APPLY_REMARK,");
			strSql.Append("TASK_CHECK_USERID=@TASK_CHECK_USERID,");
			strSql.Append("TASK_CHECK_TIME=@TASK_CHECK_TIME,");
			strSql.Append("TASK_CHECK_REMARK=@TASK_CHECK_REMARK,");
			strSql.Append("TASK_APPROVAL_USERID=@TASK_APPROVAL_USERID,");
			strSql.Append("TASK_APPROVAL_TIME=@TASK_APPROVAL_TIME,");
			strSql.Append("TASK_APPROVAL_REMARK=@TASK_APPROVAL_REMARK,");
			strSql.Append("TASK_REAL_BEGINTIME=@TASK_REAL_BEGINTIME,");
			strSql.Append("TASK_REAL_FINISHTIME=@TASK_REAL_FINISHTIME,");
			strSql.Append("TASK_AFTERWARDS_HANDLE=@TASK_AFTERWARDS_HANDLE,");
			strSql.Append("TASK_AFTERWARDS_DESCRIPTION=@TASK_AFTERWARDS_DESCRIPTION,");
			strSql.Append("TASK_AFTERWARDS_TIME=@TASK_AFTERWARDS_TIME,");
			strSql.Append("TASK_FLAG_TYPE=@TASK_FLAG_TYPE,");
			strSql.Append("TAKE_GUNBULLET_ADMIN1=@TAKE_GUNBULLET_ADMIN1,");
			strSql.Append("RETURN_GUNBULLET_ADMIN1=@RETURN_GUNBULLET_ADMIN1,");
			strSql.Append("TAKE_GUNBULLET_ADMIN2=@TAKE_GUNBULLET_ADMIN2,");
			strSql.Append("RETURN_GUNBULLET_ADMIN2=@RETURN_GUNBULLET_ADMIN2");
			strSql.Append(" where TASK_ID=@TASK_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_STATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_BIGTYPE", MySqlDbType.Int32,10),
					new MySqlParameter("@TASK_SMALLTYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PROPERTY", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PLAN_BEGINTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_PLAN_FINISHTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPLY_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_CHECK_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_USERID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_APPROVAL_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_REAL_BEGINTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_REAL_FINISHTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_HANDLE", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_DESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_AFTERWARDS_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_FLAG_TYPE", MySqlDbType.VarChar,10),
					new MySqlParameter("@TAKE_GUNBULLET_ADMIN1", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_ADMIN1", MySqlDbType.VarChar,255),
					new MySqlParameter("@TAKE_GUNBULLET_ADMIN2", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_ADMIN2", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.GUNARK_ID;
			parameters[1].Value = model.UNIT_ID;
			parameters[2].Value = model.TASK_STATUS;
			parameters[3].Value = model.TASK_BIGTYPE;
			parameters[4].Value = model.TASK_SMALLTYPE;
			parameters[5].Value = model.TASK_PROPERTY;
			parameters[6].Value = model.TASK_PLAN_BEGINTIME;
			parameters[7].Value = model.TASK_PLAN_FINISHTIME;
			parameters[8].Value = model.TASK_APPLY_USERID;
			parameters[9].Value = model.TASK_APPLY_TIME;
			parameters[10].Value = model.TASK_APPLY_REMARK;
			parameters[11].Value = model.TASK_CHECK_USERID;
			parameters[12].Value = model.TASK_CHECK_TIME;
			parameters[13].Value = model.TASK_CHECK_REMARK;
			parameters[14].Value = model.TASK_APPROVAL_USERID;
			parameters[15].Value = model.TASK_APPROVAL_TIME;
			parameters[16].Value = model.TASK_APPROVAL_REMARK;
			parameters[17].Value = model.TASK_REAL_BEGINTIME;
			parameters[18].Value = model.TASK_REAL_FINISHTIME;
			parameters[19].Value = model.TASK_AFTERWARDS_HANDLE;
			parameters[20].Value = model.TASK_AFTERWARDS_DESCRIPTION;
			parameters[21].Value = model.TASK_AFTERWARDS_TIME;
			parameters[22].Value = model.TASK_FLAG_TYPE;
			parameters[23].Value = model.TAKE_GUNBULLET_ADMIN1;
			parameters[24].Value = model.RETURN_GUNBULLET_ADMIN1;
			parameters[25].Value = model.TAKE_GUNBULLET_ADMIN2;
			parameters[26].Value = model.RETURN_GUNBULLET_ADMIN2;
			parameters[27].Value = model.TASK_ID;

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
		public bool Delete(string TASK_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_task_info ");
			strSql.Append(" where TASK_ID=@TASK_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = TASK_ID;

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
		public bool DeleteList(string TASK_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_task_info ");
			strSql.Append(" where TASK_ID in ("+TASK_IDlist + ")  ");
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
		public Gunark.Model.task_info GetModel(string TASK_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TASK_ID,GUNARK_ID,UNIT_ID,TASK_STATUS,TASK_BIGTYPE,TASK_SMALLTYPE,TASK_PROPERTY,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_APPLY_USERID,TASK_APPLY_TIME,TASK_APPLY_REMARK,TASK_CHECK_USERID,TASK_CHECK_TIME,TASK_CHECK_REMARK,TASK_APPROVAL_USERID,TASK_APPROVAL_TIME,TASK_APPROVAL_REMARK,TASK_REAL_BEGINTIME,TASK_REAL_FINISHTIME,TASK_AFTERWARDS_HANDLE,TASK_AFTERWARDS_DESCRIPTION,TASK_AFTERWARDS_TIME,TASK_FLAG_TYPE,TAKE_GUNBULLET_ADMIN1,RETURN_GUNBULLET_ADMIN1,TAKE_GUNBULLET_ADMIN2,RETURN_GUNBULLET_ADMIN2 from gunark_task_info ");
			strSql.Append(" where TASK_ID=@TASK_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = TASK_ID;

			Gunark.Model.task_info model=new Gunark.Model.task_info();
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
		public Gunark.Model.task_info DataRowToModel(DataRow row)
		{
			Gunark.Model.task_info model=new Gunark.Model.task_info();
			if (row != null)
			{
				if(row["TASK_ID"]!=null)
				{
					model.TASK_ID=row["TASK_ID"].ToString();
				}
				if(row["GUNARK_ID"]!=null)
				{
					model.GUNARK_ID=row["GUNARK_ID"].ToString();
				}
				if(row["UNIT_ID"]!=null)
				{
					model.UNIT_ID=row["UNIT_ID"].ToString();
				}
				if(row["TASK_STATUS"]!=null)
				{
					model.TASK_STATUS=row["TASK_STATUS"].ToString();
				}
				if(row["TASK_BIGTYPE"]!=null && row["TASK_BIGTYPE"].ToString()!="")
				{
					model.TASK_BIGTYPE=int.Parse(row["TASK_BIGTYPE"].ToString());
				}
				if(row["TASK_SMALLTYPE"]!=null)
				{
					model.TASK_SMALLTYPE=row["TASK_SMALLTYPE"].ToString();
				}
				if(row["TASK_PROPERTY"]!=null)
				{
					model.TASK_PROPERTY=row["TASK_PROPERTY"].ToString();
				}
				if(row["TASK_PLAN_BEGINTIME"]!=null)
				{
					model.TASK_PLAN_BEGINTIME=row["TASK_PLAN_BEGINTIME"].ToString();
				}
				if(row["TASK_PLAN_FINISHTIME"]!=null)
				{
					model.TASK_PLAN_FINISHTIME=row["TASK_PLAN_FINISHTIME"].ToString();
				}
				if(row["TASK_APPLY_USERID"]!=null)
				{
					model.TASK_APPLY_USERID=row["TASK_APPLY_USERID"].ToString();
				}
				if(row["TASK_APPLY_TIME"]!=null)
				{
					model.TASK_APPLY_TIME=row["TASK_APPLY_TIME"].ToString();
				}
				if(row["TASK_APPLY_REMARK"]!=null)
				{
					model.TASK_APPLY_REMARK=row["TASK_APPLY_REMARK"].ToString();
				}
				if(row["TASK_CHECK_USERID"]!=null)
				{
					model.TASK_CHECK_USERID=row["TASK_CHECK_USERID"].ToString();
				}
				if(row["TASK_CHECK_TIME"]!=null)
				{
					model.TASK_CHECK_TIME=row["TASK_CHECK_TIME"].ToString();
				}
				if(row["TASK_CHECK_REMARK"]!=null)
				{
					model.TASK_CHECK_REMARK=row["TASK_CHECK_REMARK"].ToString();
				}
				if(row["TASK_APPROVAL_USERID"]!=null)
				{
					model.TASK_APPROVAL_USERID=row["TASK_APPROVAL_USERID"].ToString();
				}
				if(row["TASK_APPROVAL_TIME"]!=null)
				{
					model.TASK_APPROVAL_TIME=row["TASK_APPROVAL_TIME"].ToString();
				}
				if(row["TASK_APPROVAL_REMARK"]!=null)
				{
					model.TASK_APPROVAL_REMARK=row["TASK_APPROVAL_REMARK"].ToString();
				}
				if(row["TASK_REAL_BEGINTIME"]!=null)
				{
					model.TASK_REAL_BEGINTIME=row["TASK_REAL_BEGINTIME"].ToString();
				}
				if(row["TASK_REAL_FINISHTIME"]!=null)
				{
					model.TASK_REAL_FINISHTIME=row["TASK_REAL_FINISHTIME"].ToString();
				}
				if(row["TASK_AFTERWARDS_HANDLE"]!=null)
				{
					model.TASK_AFTERWARDS_HANDLE=row["TASK_AFTERWARDS_HANDLE"].ToString();
				}
				if(row["TASK_AFTERWARDS_DESCRIPTION"]!=null)
				{
					model.TASK_AFTERWARDS_DESCRIPTION=row["TASK_AFTERWARDS_DESCRIPTION"].ToString();
				}
				if(row["TASK_AFTERWARDS_TIME"]!=null)
				{
					model.TASK_AFTERWARDS_TIME=row["TASK_AFTERWARDS_TIME"].ToString();
				}
				if(row["TASK_FLAG_TYPE"]!=null)
				{
					model.TASK_FLAG_TYPE=row["TASK_FLAG_TYPE"].ToString();
				}
				if(row["TAKE_GUNBULLET_ADMIN1"]!=null)
				{
					model.TAKE_GUNBULLET_ADMIN1=row["TAKE_GUNBULLET_ADMIN1"].ToString();
				}
				if(row["RETURN_GUNBULLET_ADMIN1"]!=null)
				{
					model.RETURN_GUNBULLET_ADMIN1=row["RETURN_GUNBULLET_ADMIN1"].ToString();
				}
				if(row["TAKE_GUNBULLET_ADMIN2"]!=null)
				{
					model.TAKE_GUNBULLET_ADMIN2=row["TAKE_GUNBULLET_ADMIN2"].ToString();
				}
				if(row["RETURN_GUNBULLET_ADMIN2"]!=null)
				{
					model.RETURN_GUNBULLET_ADMIN2=row["RETURN_GUNBULLET_ADMIN2"].ToString();
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
			strSql.Append("select TASK_ID,GUNARK_ID,UNIT_ID,TASK_STATUS,TASK_BIGTYPE,TASK_SMALLTYPE,TASK_PROPERTY,TASK_PLAN_BEGINTIME,TASK_PLAN_FINISHTIME,TASK_APPLY_USERID,TASK_APPLY_TIME,TASK_APPLY_REMARK,TASK_CHECK_USERID,TASK_CHECK_TIME,TASK_CHECK_REMARK,TASK_APPROVAL_USERID,TASK_APPROVAL_TIME,TASK_APPROVAL_REMARK,TASK_REAL_BEGINTIME,TASK_REAL_FINISHTIME,TASK_AFTERWARDS_HANDLE,TASK_AFTERWARDS_DESCRIPTION,TASK_AFTERWARDS_TIME,TASK_FLAG_TYPE,TAKE_GUNBULLET_ADMIN1,RETURN_GUNBULLET_ADMIN1,TAKE_GUNBULLET_ADMIN2,RETURN_GUNBULLET_ADMIN2 ");
			strSql.Append(" FROM gunark_task_info ");
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
			strSql.Append("select count(1) FROM gunark_task_info ");
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
				strSql.Append("order by T.TASK_ID desc");
			}
			strSql.Append(")AS Row, T.*  from gunark_task_info T ");
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
			parameters[0].Value = "gunark_task_info";
			parameters[1].Value = "TASK_ID";
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

