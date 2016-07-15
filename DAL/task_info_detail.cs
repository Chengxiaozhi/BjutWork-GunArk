using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:task_info_detail
	/// </summary>
	public partial class task_info_detail
	{
		public task_info_detail()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TASK_DETAIL_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gunark_task_info_detail");
			strSql.Append(" where TASK_DETAIL_ID=@TASK_DETAIL_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_DETAIL_ID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = TASK_DETAIL_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.task_info_detail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gunark_task_info_detail(");
			strSql.Append("TASK_DETAIL_ID,TASK_ID,UNIT_ID,GUNARK_ID,GUN_INFO_ID,GUN_POSITION_INFO_ID,BULLET_TYPE,MAGAZINE_INFO_ID,APPLY_BULLET_QTY,DEPLETION_BULLET_QTY,TAKE_GUNBULLET_TIME,RETURN_GUNBULLET_TIME,TAKE_GUNBULLET_USER,RETURN_GUNBULLET_USER,FLAG_TYPE,GUN_TYPE,GUN_DUTY_USER,BULLET_ID)");
			strSql.Append(" values (");
			strSql.Append("@TASK_DETAIL_ID,@TASK_ID,@UNIT_ID,@GUNARK_ID,@GUN_INFO_ID,@GUN_POSITION_INFO_ID,@BULLET_TYPE,@MAGAZINE_INFO_ID,@APPLY_BULLET_QTY,@DEPLETION_BULLET_QTY,@TAKE_GUNBULLET_TIME,@RETURN_GUNBULLET_TIME,@TAKE_GUNBULLET_USER,@RETURN_GUNBULLET_USER,@FLAG_TYPE,@GUN_TYPE,@GUN_DUTY_USER,@BULLET_ID)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_DETAIL_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@APPLY_BULLET_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@DEPLETION_BULLET_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@TAKE_GUNBULLET_TIME", MySqlDbType.Time),
					new MySqlParameter("@RETURN_GUNBULLET_TIME", MySqlDbType.Time),
					new MySqlParameter("@TAKE_GUNBULLET_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@FLAG_TYPE", MySqlDbType.Int32,10),
					new MySqlParameter("@GUN_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_DUTY_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.TASK_DETAIL_ID;
			parameters[1].Value = model.TASK_ID;
			parameters[2].Value = model.UNIT_ID;
			parameters[3].Value = model.GUNARK_ID;
			parameters[4].Value = model.GUN_INFO_ID;
			parameters[5].Value = model.GUN_POSITION_INFO_ID;
			parameters[6].Value = model.BULLET_TYPE;
			parameters[7].Value = model.MAGAZINE_INFO_ID;
			parameters[8].Value = model.APPLY_BULLET_QTY;
			parameters[9].Value = model.DEPLETION_BULLET_QTY;
			parameters[10].Value = model.TAKE_GUNBULLET_TIME;
			parameters[11].Value = model.RETURN_GUNBULLET_TIME;
			parameters[12].Value = model.TAKE_GUNBULLET_USER;
			parameters[13].Value = model.RETURN_GUNBULLET_USER;
			parameters[14].Value = model.FLAG_TYPE;
			parameters[15].Value = model.GUN_TYPE;
			parameters[16].Value = model.GUN_DUTY_USER;
			parameters[17].Value = model.BULLET_ID;

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
		public bool Update(Gunark.Model.task_info_detail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gunark_task_info_detail set ");
			strSql.Append("TASK_ID=@TASK_ID,");
			strSql.Append("UNIT_ID=@UNIT_ID,");
			strSql.Append("GUNARK_ID=@GUNARK_ID,");
			strSql.Append("GUN_INFO_ID=@GUN_INFO_ID,");
			strSql.Append("GUN_POSITION_INFO_ID=@GUN_POSITION_INFO_ID,");
			strSql.Append("BULLET_TYPE=@BULLET_TYPE,");
			strSql.Append("MAGAZINE_INFO_ID=@MAGAZINE_INFO_ID,");
			strSql.Append("APPLY_BULLET_QTY=@APPLY_BULLET_QTY,");
			strSql.Append("DEPLETION_BULLET_QTY=@DEPLETION_BULLET_QTY,");
			strSql.Append("TAKE_GUNBULLET_TIME=@TAKE_GUNBULLET_TIME,");
			strSql.Append("RETURN_GUNBULLET_TIME=@RETURN_GUNBULLET_TIME,");
			strSql.Append("TAKE_GUNBULLET_USER=@TAKE_GUNBULLET_USER,");
			strSql.Append("RETURN_GUNBULLET_USER=@RETURN_GUNBULLET_USER,");
			strSql.Append("FLAG_TYPE=@FLAG_TYPE,");
			strSql.Append("GUN_TYPE=@GUN_TYPE,");
			strSql.Append("GUN_DUTY_USER=@GUN_DUTY_USER,");
			strSql.Append("BULLET_ID=@BULLET_ID");
			strSql.Append(" where TASK_DETAIL_ID=@TASK_DETAIL_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@APPLY_BULLET_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@DEPLETION_BULLET_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@TAKE_GUNBULLET_TIME", MySqlDbType.Time),
					new MySqlParameter("@RETURN_GUNBULLET_TIME", MySqlDbType.Time),
					new MySqlParameter("@TAKE_GUNBULLET_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@RETURN_GUNBULLET_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@FLAG_TYPE", MySqlDbType.Int32,10),
					new MySqlParameter("@GUN_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_DUTY_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@TASK_DETAIL_ID", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.TASK_ID;
			parameters[1].Value = model.UNIT_ID;
			parameters[2].Value = model.GUNARK_ID;
			parameters[3].Value = model.GUN_INFO_ID;
			parameters[4].Value = model.GUN_POSITION_INFO_ID;
			parameters[5].Value = model.BULLET_TYPE;
			parameters[6].Value = model.MAGAZINE_INFO_ID;
			parameters[7].Value = model.APPLY_BULLET_QTY;
			parameters[8].Value = model.DEPLETION_BULLET_QTY;
			parameters[9].Value = model.TAKE_GUNBULLET_TIME;
			parameters[10].Value = model.RETURN_GUNBULLET_TIME;
			parameters[11].Value = model.TAKE_GUNBULLET_USER;
			parameters[12].Value = model.RETURN_GUNBULLET_USER;
			parameters[13].Value = model.FLAG_TYPE;
			parameters[14].Value = model.GUN_TYPE;
			parameters[15].Value = model.GUN_DUTY_USER;
			parameters[16].Value = model.BULLET_ID;
			parameters[17].Value = model.TASK_DETAIL_ID;

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
		public bool Delete(string TASK_DETAIL_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_task_info_detail ");
			strSql.Append(" where TASK_DETAIL_ID=@TASK_DETAIL_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_DETAIL_ID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = TASK_DETAIL_ID;

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
		public bool DeleteList(string TASK_DETAIL_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_task_info_detail ");
			strSql.Append(" where TASK_DETAIL_ID in ("+TASK_DETAIL_IDlist + ")  ");
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
		public Gunark.Model.task_info_detail GetModel(string TASK_DETAIL_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TASK_DETAIL_ID,TASK_ID,UNIT_ID,GUNARK_ID,GUN_INFO_ID,GUN_POSITION_INFO_ID,BULLET_TYPE,MAGAZINE_INFO_ID,APPLY_BULLET_QTY,DEPLETION_BULLET_QTY,TAKE_GUNBULLET_TIME,RETURN_GUNBULLET_TIME,TAKE_GUNBULLET_USER,RETURN_GUNBULLET_USER,FLAG_TYPE,GUN_TYPE,GUN_DUTY_USER,BULLET_ID from gunark_task_info_detail ");
			strSql.Append(" where TASK_DETAIL_ID=@TASK_DETAIL_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@TASK_DETAIL_ID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = TASK_DETAIL_ID;

			Gunark.Model.task_info_detail model=new Gunark.Model.task_info_detail();
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
		public Gunark.Model.task_info_detail DataRowToModel(DataRow row)
		{
			Gunark.Model.task_info_detail model=new Gunark.Model.task_info_detail();
			if (row != null)
			{
				if(row["TASK_DETAIL_ID"]!=null)
				{
					model.TASK_DETAIL_ID=row["TASK_DETAIL_ID"].ToString();
				}
				if(row["TASK_ID"]!=null)
				{
					model.TASK_ID=row["TASK_ID"].ToString();
				}
				if(row["UNIT_ID"]!=null)
				{
					model.UNIT_ID=row["UNIT_ID"].ToString();
				}
				if(row["GUNARK_ID"]!=null)
				{
					model.GUNARK_ID=row["GUNARK_ID"].ToString();
				}
				if(row["GUN_INFO_ID"]!=null)
				{
					model.GUN_INFO_ID=row["GUN_INFO_ID"].ToString();
				}
				if(row["GUN_POSITION_INFO_ID"]!=null)
				{
					model.GUN_POSITION_INFO_ID=row["GUN_POSITION_INFO_ID"].ToString();
				}
				if(row["BULLET_TYPE"]!=null)
				{
					model.BULLET_TYPE=row["BULLET_TYPE"].ToString();
				}
				if(row["MAGAZINE_INFO_ID"]!=null)
				{
					model.MAGAZINE_INFO_ID=row["MAGAZINE_INFO_ID"].ToString();
				}
				if(row["APPLY_BULLET_QTY"]!=null && row["APPLY_BULLET_QTY"].ToString()!="")
				{
					model.APPLY_BULLET_QTY=int.Parse(row["APPLY_BULLET_QTY"].ToString());
				}
				if(row["DEPLETION_BULLET_QTY"]!=null && row["DEPLETION_BULLET_QTY"].ToString()!="")
				{
					model.DEPLETION_BULLET_QTY=int.Parse(row["DEPLETION_BULLET_QTY"].ToString());
				}
				if(row["TAKE_GUNBULLET_TIME"]!=null && row["TAKE_GUNBULLET_TIME"].ToString()!="")
				{
					model.TAKE_GUNBULLET_TIME=DateTime.Parse(row["TAKE_GUNBULLET_TIME"].ToString());
				}
				if(row["RETURN_GUNBULLET_TIME"]!=null && row["RETURN_GUNBULLET_TIME"].ToString()!="")
				{
					model.RETURN_GUNBULLET_TIME=DateTime.Parse(row["RETURN_GUNBULLET_TIME"].ToString());
				}
				if(row["TAKE_GUNBULLET_USER"]!=null)
				{
					model.TAKE_GUNBULLET_USER=row["TAKE_GUNBULLET_USER"].ToString();
				}
				if(row["RETURN_GUNBULLET_USER"]!=null)
				{
					model.RETURN_GUNBULLET_USER=row["RETURN_GUNBULLET_USER"].ToString();
				}
				if(row["FLAG_TYPE"]!=null && row["FLAG_TYPE"].ToString()!="")
				{
					model.FLAG_TYPE=int.Parse(row["FLAG_TYPE"].ToString());
				}
				if(row["GUN_TYPE"]!=null)
				{
					model.GUN_TYPE=row["GUN_TYPE"].ToString();
				}
				if(row["GUN_DUTY_USER"]!=null)
				{
					model.GUN_DUTY_USER=row["GUN_DUTY_USER"].ToString();
				}
				if(row["BULLET_ID"]!=null)
				{
					model.BULLET_ID=row["BULLET_ID"].ToString();
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
			strSql.Append("select TASK_DETAIL_ID,TASK_ID,UNIT_ID,GUNARK_ID,GUN_INFO_ID,GUN_POSITION_INFO_ID,BULLET_TYPE,MAGAZINE_INFO_ID,APPLY_BULLET_QTY,DEPLETION_BULLET_QTY,TAKE_GUNBULLET_TIME,RETURN_GUNBULLET_TIME,TAKE_GUNBULLET_USER,RETURN_GUNBULLET_USER,FLAG_TYPE,GUN_TYPE,GUN_DUTY_USER,BULLET_ID ");
			strSql.Append(" FROM gunark_task_info_detail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetTaskList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select gti.TASK_ID, gti.TASK_STATUS, gpi.GUN_POSITION_NUMBER, gpi.GUN_TYPE, gtid.GUN_INFO_ID, gtid.GUN_POSITION_INFO_ID, gti.TASK_PLAN_BEGINTIME, gu.USER_REALNAME, cast(gti.TASK_BIGTYPE as CHAR), gu.USER_REALNAME, gpi.GUN_POSITION_NUMBER, gpi.GUN_TYPE, cast(gtid.APPLY_BULLET_QTY as CHAR)");
            strSql.Append(" from gunark_task_info gti left join gunark_task_info_detail gtid on gtid.TASK_ID = gti.TASK_ID left join gunark_position_info gpi on gpi.GUN_POSITION_INFO_ID = gtid.GUN_POSITION_INFO_ID left join gunark_magazine_info gmi on gmi.MAGAZINE_INFO_ID = gtid.MAGAZINE_INFO_ID left join gunark_user gu on gtid.GUN_DUTY_USER=gu.USER_POLICENUMB ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM gunark_task_info_detail ");
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
				strSql.Append("order by T.TASK_DETAIL_ID desc");
			}
			strSql.Append(")AS Row, T.*  from gunark_task_info_detail T ");
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
			parameters[0].Value = "gunark_task_info_detail";
			parameters[1].Value = "TASK_DETAIL_ID";
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

