using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:position_info
	/// </summary>
	public partial class position_info
	{
		public position_info()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GUN_POSITION_INFO_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from gunark_position_info");
			strSql.Append(" where GUN_POSITION_INFO_ID=@GUN_POSITION_INFO_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUN_POSITION_INFO_ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.position_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into gunark_position_info(");
			strSql.Append("GUN_POSITION_INFO_ID,UNIT_ID,GUNARK_ID,GUN_POSITION_NUMBER,GUN_POSITION_STATUS,GUN_INFO_ID,GUN_BULLET_NUMBER,GUN_TYPE)");
			strSql.Append(" values (");
			strSql.Append("@GUN_POSITION_INFO_ID,@UNIT_ID,@GUNARK_ID,@GUN_POSITION_NUMBER,@GUN_POSITION_STATUS,@GUN_INFO_ID,@GUN_BULLET_NUMBER,@GUN_TYPE)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_STATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_BULLET_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_TYPE", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.GUN_POSITION_INFO_ID;
			parameters[1].Value = model.UNIT_ID;
			parameters[2].Value = model.GUNARK_ID;
			parameters[3].Value = model.GUN_POSITION_NUMBER;
			parameters[4].Value = model.GUN_POSITION_STATUS;
			parameters[5].Value = model.GUN_INFO_ID;
			parameters[6].Value = model.GUN_BULLET_NUMBER;
			parameters[7].Value = model.GUN_TYPE;

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
		public bool Update(Gunark.Model.position_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update gunark_position_info set ");
			strSql.Append("UNIT_ID=@UNIT_ID,");
			strSql.Append("GUNARK_ID=@GUNARK_ID,");
			strSql.Append("GUN_POSITION_NUMBER=@GUN_POSITION_NUMBER,");
			strSql.Append("GUN_POSITION_STATUS=@GUN_POSITION_STATUS,");
			strSql.Append("GUN_INFO_ID=@GUN_INFO_ID,");
			strSql.Append("GUN_BULLET_NUMBER=@GUN_BULLET_NUMBER,");
			strSql.Append("GUN_TYPE=@GUN_TYPE");
			strSql.Append(" where GUN_POSITION_INFO_ID=@GUN_POSITION_INFO_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_STATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_INFO_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_BULLET_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255)};
			parameters[0].Value = model.UNIT_ID;
			parameters[1].Value = model.GUNARK_ID;
			parameters[2].Value = model.GUN_POSITION_NUMBER;
			parameters[3].Value = model.GUN_POSITION_STATUS;
			parameters[4].Value = model.GUN_INFO_ID;
			parameters[5].Value = model.GUN_BULLET_NUMBER;
			parameters[6].Value = model.GUN_TYPE;
			parameters[7].Value = model.GUN_POSITION_INFO_ID;

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
		public bool Delete(string GUN_POSITION_INFO_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_position_info ");
			strSql.Append(" where GUN_POSITION_INFO_ID=@GUN_POSITION_INFO_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUN_POSITION_INFO_ID;

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
		public bool DeleteList(string GUN_POSITION_INFO_IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from gunark_position_info ");
			strSql.Append(" where GUN_POSITION_INFO_ID in ("+GUN_POSITION_INFO_IDlist + ")  ");
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
		public Gunark.Model.position_info GetModel(string GUN_POSITION_INFO_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select GUN_POSITION_INFO_ID,UNIT_ID,GUNARK_ID,GUN_POSITION_NUMBER,GUN_POSITION_STATUS,GUN_INFO_ID,GUN_BULLET_NUMBER,GUN_TYPE from gunark_position_info ");
			strSql.Append(" where GUN_POSITION_INFO_ID=@GUN_POSITION_INFO_ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_POSITION_INFO_ID", MySqlDbType.VarChar,255)			};
			parameters[0].Value = GUN_POSITION_INFO_ID;

			Gunark.Model.position_info model=new Gunark.Model.position_info();
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
        public Gunark.Model.position_info GetModelByGunInfo(string GUN_INFO_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GUN_POSITION_INFO_ID,UNIT_ID,GUNARK_ID,GUN_POSITION_NUMBER,GUN_POSITION_STATUS,GUN_INFO_ID,GUN_BULLET_NUMBER,GUN_TYPE from gunark_position_info ");
            strSql.Append(" where GUN_INFO_ID=@GUN_INFO_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_INFO_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUN_INFO_ID;

            Gunark.Model.position_info model = new Gunark.Model.position_info();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public Gunark.Model.position_info GetModelByGunPosNum(string GUN_POSITION_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GUN_POSITION_INFO_ID,UNIT_ID,GUNARK_ID,GUN_POSITION_NUMBER,GUN_POSITION_STATUS,GUN_INFO_ID,GUN_BULLET_NUMBER,GUN_TYPE from gunark_position_info ");
            strSql.Append(" where GUN_POSITION_NUMBER=@GUN_POSITION_NUMBER ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUN_POSITION_NUMBER", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUN_POSITION_NUMBER;

            Gunark.Model.position_info model = new Gunark.Model.position_info();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
		public Gunark.Model.position_info DataRowToModel(DataRow row)
		{
			Gunark.Model.position_info model=new Gunark.Model.position_info();
			if (row != null)
			{
				if(row["GUN_POSITION_INFO_ID"]!=null)
				{
					model.GUN_POSITION_INFO_ID=row["GUN_POSITION_INFO_ID"].ToString();
				}
				if(row["UNIT_ID"]!=null)
				{
					model.UNIT_ID=row["UNIT_ID"].ToString();
				}
				if(row["GUNARK_ID"]!=null)
				{
					model.GUNARK_ID=row["GUNARK_ID"].ToString();
				}
				if(row["GUN_POSITION_NUMBER"]!=null)
				{
					model.GUN_POSITION_NUMBER=row["GUN_POSITION_NUMBER"].ToString();
				}
				if(row["GUN_POSITION_STATUS"]!=null)
				{
					model.GUN_POSITION_STATUS=row["GUN_POSITION_STATUS"].ToString();
				}
				if(row["GUN_INFO_ID"]!=null)
				{
					model.GUN_INFO_ID=row["GUN_INFO_ID"].ToString();
				}
				if(row["GUN_BULLET_NUMBER"]!=null)
				{
					model.GUN_BULLET_NUMBER=row["GUN_BULLET_NUMBER"].ToString();
				}
				if(row["GUN_TYPE"]!=null)
				{
					model.GUN_TYPE=row["GUN_TYPE"].ToString();
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
			strSql.Append("select GUN_POSITION_INFO_ID,UNIT_ID,GUNARK_ID,GUN_POSITION_NUMBER,GUN_POSITION_STATUS,GUN_INFO_ID,GUN_BULLET_NUMBER,GUN_TYPE ");
			strSql.Append(" FROM gunark_position_info ");
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
			strSql.Append("select count(1) FROM gunark_position_info ");
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
				strSql.Append("order by T.GUN_POSITION_INFO_ID desc");
			}
			strSql.Append(")AS Row, T.*  from gunark_position_info T ");
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
			parameters[0].Value = "gunark_position_info";
			parameters[1].Value = "GUN_POSITION_INFO_ID";
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

