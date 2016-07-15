using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:log
	/// </summary>
	public partial class log
	{
		public log()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_log");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_log(");
            strSql.Append("LOG_TIME,LOG_DISCRIBE,LOG_TYPE,OPREAT_USER,BULLET_NUMBER,GUN_NUMBER,ALARM_TYPE,ALARM_DISCRIBE)");
            strSql.Append(" values (");
            strSql.Append("@LOG_TIME,@LOG_DISCRIBE,@LOG_TYPE,@OPREAT_USER,@BULLET_NUMBER,@GUN_NUMBER,@ALARM_TYPE,@ALARM_DISCRIBE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@LOG_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@LOG_DISCRIBE", MySqlDbType.VarChar,255),
					new MySqlParameter("@LOG_TYPE", MySqlDbType.Int32,2),
					new MySqlParameter("@OPREAT_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_DISCRIBE", MySqlDbType.Int32,255)};
            parameters[0].Value = model.LOG_TIME;
            parameters[1].Value = model.LOG_DISCRIBE;
            parameters[2].Value = model.LOG_TYPE;
            parameters[3].Value = model.OPREAT_USER;
            parameters[4].Value = model.BULLET_NUMBER;
            parameters[5].Value = model.GUN_NUMBER;
            parameters[6].Value = model.ALARM_TYPE;
            parameters[7].Value = model.ALARM_DISCRIBE;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Update(Gunark.Model.log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_log set ");
            strSql.Append("LOG_TIME=@LOG_TIME,");
            strSql.Append("LOG_DISCRIBE=@LOG_DISCRIBE,");
            strSql.Append("LOG_TYPE=@LOG_TYPE,");
            strSql.Append("OPREAT_USER=@OPREAT_USER,");
            strSql.Append("BULLET_NUMBER=@BULLET_NUMBER,");
            strSql.Append("GUN_NUMBER=@GUN_NUMBER,");
            strSql.Append("ALARM_TYPE=@ALARM_TYPE,");
            strSql.Append("ALARM_DISCRIBE=@ALARM_DISCRIBE");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@LOG_TIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@LOG_DISCRIBE", MySqlDbType.VarChar,255),
					new MySqlParameter("@LOG_TYPE", MySqlDbType.Int32,2),
					new MySqlParameter("@OPREAT_USER", MySqlDbType.VarChar,255),
					new MySqlParameter("@BULLET_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@ALARM_DISCRIBE", MySqlDbType.Int32,255),
					new MySqlParameter("@ID", MySqlDbType.Int32,11)};
            parameters[0].Value = model.LOG_TIME;
            parameters[1].Value = model.LOG_DISCRIBE;
            parameters[2].Value = model.LOG_TYPE;
            parameters[3].Value = model.OPREAT_USER;
            parameters[4].Value = model.BULLET_NUMBER;
            parameters[5].Value = model.GUN_NUMBER;
            parameters[6].Value = model.ALARM_TYPE;
            parameters[7].Value = model.ALARM_DISCRIBE;
            parameters[8].Value = model.ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_log ");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
            parameters[0].Value = ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_log ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
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
        public Gunark.Model.log GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LOG_TIME,LOG_DISCRIBE,LOG_TYPE,OPREAT_USER,BULLET_NUMBER,GUN_NUMBER,ALARM_TYPE,ALARM_DISCRIBE from gunark_log ");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
            parameters[0].Value = ID;

            Gunark.Model.log model = new Gunark.Model.log();
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
        public Gunark.Model.log DataRowToModel(DataRow row)
        {
            Gunark.Model.log model = new Gunark.Model.log();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["LOG_TIME"] != null)
                {
                    model.LOG_TIME = row["LOG_TIME"].ToString();
                }
                if (row["LOG_DISCRIBE"] != null)
                {
                    model.LOG_DISCRIBE = row["LOG_DISCRIBE"].ToString();
                }
                if (row["LOG_TYPE"] != null && row["LOG_TYPE"].ToString() != "")
                {
                    model.LOG_TYPE = int.Parse(row["LOG_TYPE"].ToString());
                }
                if (row["OPREAT_USER"] != null)
                {
                    model.OPREAT_USER = row["OPREAT_USER"].ToString();
                }
                if (row["BULLET_NUMBER"] != null)
                {
                    model.BULLET_NUMBER = row["BULLET_NUMBER"].ToString();
                }
                if (row["GUN_NUMBER"] != null)
                {
                    model.GUN_NUMBER = row["GUN_NUMBER"].ToString();
                }
                if (row["ALARM_TYPE"] != null)
                {
                    model.ALARM_TYPE = row["ALARM_TYPE"].ToString();
                }
                if (row["ALARM_DISCRIBE"] != null && row["ALARM_DISCRIBE"].ToString() != "")
                {
                    model.ALARM_DISCRIBE = int.Parse(row["ALARM_DISCRIBE"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LOG_TIME,LOG_DISCRIBE,LOG_TYPE,OPREAT_USER,BULLET_NUMBER,GUN_NUMBER,ALARM_TYPE,ALARM_DISCRIBE ");
            strSql.Append(" FROM gunark_log ");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM gunark_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_log T ");
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
            parameters[0].Value = "gunark_log";
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

