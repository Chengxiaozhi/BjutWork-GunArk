using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:fingerprint
	/// </summary>
	public partial class fingerprint
	{
		public fingerprint()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string userPoliceNumb, string fingerNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_fingerprint");
            strSql.Append(" where USER_POLICENUMB=@USER_POLICENUMB and FINGER_NUMBER=@FINGER_NUMBER");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_POLICENUMB", MySqlDbType.String),
                    new MySqlParameter("@FINGER_NUMBER", MySqlDbType.String)
			};
            parameters[0].Value = userPoliceNumb;
            parameters[1].Value = fingerNumber;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录(通过ID判断)
        /// </summary>
        public bool ExistsById(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_fingerprint");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.String)
			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录(通过FingerPringID判断)
        /// </summary>
        public bool ExistsByFingerPrintId(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_fingerprint");
            strSql.Append(" where USER_FINGERPRINT_ID=@USER_FINGERPRINT_ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_FINGERPRINT_ID", MySqlDbType.String)
			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.fingerprint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_fingerprint(");
            strSql.Append("USER_FINGERPRINT_ID,USER_POLICENUMB,USER_NAME,USER_PWD,FINGER_NUMBER,UNIT_ID,USER_FINGERPRINT,USER_TYPE,IS_UPDATE,USER_BAN,USER_PRIVIEGES)");
            strSql.Append(" values (");
            strSql.Append("@USER_FINGERPRINT_ID,@USER_POLICENUMB,@USER_NAME,@USER_PWD,@FINGER_NUMBER,@UNIT_ID,@USER_FINGERPRINT,@USER_TYPE,@IS_UPDATE,@USER_BAN,@USER_PRIVIEGES)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_FINGERPRINT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POLICENUMB", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PWD", MySqlDbType.VarChar,255),
					new MySqlParameter("@FINGER_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_FINGERPRINT", MySqlDbType.Blob),
					new MySqlParameter("@USER_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@IS_UPDATE", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_BAN", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_PRIVIEGES", MySqlDbType.Int32,10)};
            parameters[0].Value = model.USER_FINGERPRINT_ID;
            parameters[1].Value = model.USER_POLICENUMB;
            parameters[2].Value = model.USER_NAME;
            parameters[3].Value = model.USER_PWD;
            parameters[4].Value = model.FINGER_NUMBER;
            parameters[5].Value = model.UNIT_ID;
            parameters[6].Value = model.USER_FINGERPRINT;
            parameters[7].Value = model.USER_TYPE;
            parameters[8].Value = model.IS_UPDATE;
            parameters[9].Value = model.USER_BAN;
            parameters[10].Value = model.USER_PRIVIEGES;

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
        public bool Update(Gunark.Model.fingerprint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_fingerprint set ");
            strSql.Append("USER_FINGERPRINT_ID=@USER_FINGERPRINT_ID,");
            strSql.Append("USER_POLICENUMB=@USER_POLICENUMB,");
            strSql.Append("USER_NAME=@USER_NAME,");
            strSql.Append("USER_PWD=@USER_PWD,");
            strSql.Append("FINGER_NUMBER=@FINGER_NUMBER,");
            strSql.Append("UNIT_ID=@UNIT_ID,");
            strSql.Append("USER_FINGERPRINT=@USER_FINGERPRINT,");
            strSql.Append("USER_TYPE=@USER_TYPE,");
            strSql.Append("IS_UPDATE=@IS_UPDATE,");
            strSql.Append("USER_BAN=@USER_BAN,");
            strSql.Append("USER_PRIVIEGES=@USER_PRIVIEGES");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_FINGERPRINT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POLICENUMB", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PWD", MySqlDbType.VarChar,255),
					new MySqlParameter("@FINGER_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_FINGERPRINT", MySqlDbType.Blob),
					new MySqlParameter("@USER_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@IS_UPDATE", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_BAN", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_PRIVIEGES", MySqlDbType.Int32,10),
					new MySqlParameter("@ID", MySqlDbType.Int32,10)};
            parameters[0].Value = model.USER_FINGERPRINT_ID;
            parameters[1].Value = model.USER_POLICENUMB;
            parameters[2].Value = model.USER_NAME;
            parameters[3].Value = model.USER_PWD;
            parameters[4].Value = model.FINGER_NUMBER;
            parameters[5].Value = model.UNIT_ID;
            parameters[6].Value = model.USER_FINGERPRINT;
            parameters[7].Value = model.USER_TYPE;
            parameters[8].Value = model.IS_UPDATE;
            parameters[9].Value = model.USER_BAN;
            parameters[10].Value = model.USER_PRIVIEGES;
            parameters[11].Value = model.ID;

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
            strSql.Append("delete from gunark_fingerprint ");
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
            strSql.Append("delete from gunark_fingerprint ");
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
        public Gunark.Model.fingerprint GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select USER_FINGERPRINT_ID,USER_POLICENUMB,USER_NAME,USER_PWD,FINGER_NUMBER,UNIT_ID,USER_FINGERPRINT,ID,USER_TYPE,IS_UPDATE,USER_BAN,USER_PRIVIEGES from gunark_fingerprint ");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
            parameters[0].Value = ID;

            Gunark.Model.fingerprint model = new Gunark.Model.fingerprint();
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
        public Gunark.Model.fingerprint GetModelByUser(string userID, string fingerNumber)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select USER_FINGERPRINT_ID,USER_POLICENUMB,USER_NAME,USER_PWD,FINGER_NUMBER,UNIT_ID,USER_FINGERPRINT,ID,USER_TYPE,IS_UPDATE,USER_BAN,USER_PRIVIEGES from gunark_fingerprint ");
            strSql.Append(" where USER_ID=@USER_ID and FINGER_NUMBER=@FINGER_NUMBER");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255),
                    new MySqlParameter("@FINGER_NUMBER", MySqlDbType.VarChar,255)
			};
            parameters[0].Value = userID;
            parameters[1].Value = fingerNumber;

            Gunark.Model.fingerprint model = new Gunark.Model.fingerprint();
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
        public Gunark.Model.fingerprint DataRowToModel(DataRow row)
        {
            Gunark.Model.fingerprint model = new Gunark.Model.fingerprint();
            if (row != null)
            {
                if (row["USER_FINGERPRINT_ID"] != null)
                {
                    model.USER_FINGERPRINT_ID = row["USER_FINGERPRINT_ID"].ToString();
                }
                if (row["USER_POLICENUMB"] != null)
                {
                    model.USER_POLICENUMB = row["USER_POLICENUMB"].ToString();
                }
                if (row["USER_NAME"] != null)
                {
                    model.USER_NAME = row["USER_NAME"].ToString();
                }
                if (row["USER_PWD"] != null)
                {
                    model.USER_PWD = row["USER_PWD"].ToString();
                }
                if (row["FINGER_NUMBER"] != null)
                {
                    model.FINGER_NUMBER = row["FINGER_NUMBER"].ToString();
                }
                if (row["UNIT_ID"] != null)
                {
                    model.UNIT_ID = row["UNIT_ID"].ToString();
                }
                if (row["USER_FINGERPRINT"] != null && row["USER_FINGERPRINT"].ToString() != "")
                {
                    model.USER_FINGERPRINT = (byte[])row["USER_FINGERPRINT"];
                }
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["USER_TYPE"] != null)
                {
                    model.USER_TYPE = row["USER_TYPE"].ToString();
                }
                if (row["IS_UPDATE"] != null && row["IS_UPDATE"].ToString() != "")
                {
                    model.IS_UPDATE = int.Parse(row["IS_UPDATE"].ToString());
                }
                if (row["USER_BAN"] != null && row["USER_BAN"].ToString() != "")
                {
                    model.USER_BAN = int.Parse(row["USER_BAN"].ToString());
                }
                if (row["USER_PRIVIEGES"] != null && row["USER_PRIVIEGES"].ToString() != "")
                {
                    model.USER_PRIVIEGES = int.Parse(row["USER_PRIVIEGES"].ToString());
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
            strSql.Append("select USER_FINGERPRINT_ID,USER_POLICENUMB,USER_NAME,USER_PWD,FINGER_NUMBER,UNIT_ID,USER_FINGERPRINT,ID,USER_TYPE,IS_UPDATE,USER_BAN,USER_PRIVIEGES ");
            strSql.Append(" FROM gunark_fingerprint ");
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
            strSql.Append("select count(1) FROM gunark_fingerprint ");
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
            strSql.Append(")AS Row, T.*  from gunark_fingerprint T ");
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
            parameters[0].Value = "gunark_fingerprint";
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

