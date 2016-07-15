using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:magazine_info
	/// </summary>
	public partial class magazine_info
	{
		public magazine_info()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string MAGAZINE_INFO_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_magazine_info");
            strSql.Append(" where MAGAZINE_INFO_ID=@MAGAZINE_INFO_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = MAGAZINE_INFO_ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.magazine_info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_magazine_info(");
            strSql.Append("MAGAZINE_INFO_ID,GUNARK_ID,UNIT_ID,MAGAZINE_NUMBER,STOCK_QTY,MAGAZINE_NAME,CAPACITY_QTY,SYN_FLAG,BULLET_MODEL,MAGAZINE_STATUS,BULLET_GROUP_ID)");
            strSql.Append(" values (");
            strSql.Append("@MAGAZINE_INFO_ID,@GUNARK_ID,@UNIT_ID,@MAGAZINE_NUMBER,@STOCK_QTY,@MAGAZINE_NAME,@CAPACITY_QTY,@SYN_FLAG,@BULLET_MODEL,@MAGAZINE_STATUS,@BULLET_GROUP_ID)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@STOCK_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@MAGAZINE_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@CAPACITY_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@SYN_FLAG", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_MODEL", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_GROUP_ID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.MAGAZINE_INFO_ID;
            parameters[1].Value = model.GUNARK_ID;
            parameters[2].Value = model.UNIT_ID;
            parameters[3].Value = model.MAGAZINE_NUMBER;
            parameters[4].Value = model.STOCK_QTY;
            parameters[5].Value = model.MAGAZINE_NAME;
            parameters[6].Value = model.CAPACITY_QTY;
            parameters[7].Value = model.SYN_FLAG;
            parameters[8].Value = model.BULLET_MODEL;
            parameters[9].Value = model.MAGAZINE_STATUS;
            parameters[10].Value = model.BULLET_GROUP_ID;

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
        public bool Update(Gunark.Model.magazine_info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_magazine_info set ");
            strSql.Append("GUNARK_ID=@GUNARK_ID,");
            strSql.Append("UNIT_ID=@UNIT_ID,");
            strSql.Append("MAGAZINE_NUMBER=@MAGAZINE_NUMBER,");
            strSql.Append("STOCK_QTY=@STOCK_QTY,");
            strSql.Append("MAGAZINE_NAME=@MAGAZINE_NAME,");
            strSql.Append("CAPACITY_QTY=@CAPACITY_QTY,");
            strSql.Append("SYN_FLAG=@SYN_FLAG,");
            strSql.Append("BULLET_MODEL=@BULLET_MODEL,");
            strSql.Append("MAGAZINE_STATUS=@MAGAZINE_STATUS,");
            strSql.Append("BULLET_GROUP_ID=@BULLET_GROUP_ID");
            strSql.Append(" where MAGAZINE_INFO_ID=@MAGAZINE_INFO_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNIT_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@STOCK_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@MAGAZINE_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@CAPACITY_QTY", MySqlDbType.Int32,10),
					new MySqlParameter("@SYN_FLAG", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_MODEL", MySqlDbType.VarChar,255),
					new MySqlParameter("@MAGAZINE_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_GROUP_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.GUNARK_ID;
            parameters[1].Value = model.UNIT_ID;
            parameters[2].Value = model.MAGAZINE_NUMBER;
            parameters[3].Value = model.STOCK_QTY;
            parameters[4].Value = model.MAGAZINE_NAME;
            parameters[5].Value = model.CAPACITY_QTY;
            parameters[6].Value = model.SYN_FLAG;
            parameters[7].Value = model.BULLET_MODEL;
            parameters[8].Value = model.MAGAZINE_STATUS;
            parameters[9].Value = model.BULLET_GROUP_ID;
            parameters[10].Value = model.MAGAZINE_INFO_ID;

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
        public bool Delete(string MAGAZINE_INFO_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_magazine_info ");
            strSql.Append(" where MAGAZINE_INFO_ID=@MAGAZINE_INFO_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = MAGAZINE_INFO_ID;

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
        public bool DeleteList(string MAGAZINE_INFO_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_magazine_info ");
            strSql.Append(" where MAGAZINE_INFO_ID in (" + MAGAZINE_INFO_IDlist + ")  ");
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
        public Gunark.Model.magazine_info GetModel(string MAGAZINE_INFO_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAGAZINE_INFO_ID,GUNARK_ID,UNIT_ID,MAGAZINE_NUMBER,STOCK_QTY,MAGAZINE_NAME,CAPACITY_QTY,SYN_FLAG,BULLET_MODEL,MAGAZINE_STATUS,BULLET_GROUP_ID from gunark_magazine_info ");
            strSql.Append(" where MAGAZINE_INFO_ID=@MAGAZINE_INFO_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@MAGAZINE_INFO_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = MAGAZINE_INFO_ID;

            Gunark.Model.magazine_info model = new Gunark.Model.magazine_info();
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
        /// 得到一个对象实体(根据弹仓号)
        /// </summary>
        public Gunark.Model.magazine_info GetModelByMagazineNum(string MAGAZINE_NUMBER)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAGAZINE_INFO_ID,GUNARK_ID,UNIT_ID,MAGAZINE_NUMBER,STOCK_QTY,MAGAZINE_NAME,CAPACITY_QTY,SYN_FLAG,BULLET_MODEL,MAGAZINE_STATUS,BULLET_GROUP_ID from gunark_magazine_info ");
            strSql.Append(" where MAGAZINE_NUMBER=@MAGAZINE_NUMBER ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@MAGAZINE_NUMBER", MySqlDbType.VarChar,50)			};
            parameters[0].Value = MAGAZINE_NUMBER;

            Gunark.Model.magazine_info model = new Gunark.Model.magazine_info();
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
        public Gunark.Model.magazine_info DataRowToModel(DataRow row)
        {
            Gunark.Model.magazine_info model = new Gunark.Model.magazine_info();
            if (row != null)
            {
                if (row["MAGAZINE_INFO_ID"] != null)
                {
                    model.MAGAZINE_INFO_ID = row["MAGAZINE_INFO_ID"].ToString();
                }
                if (row["GUNARK_ID"] != null)
                {
                    model.GUNARK_ID = row["GUNARK_ID"].ToString();
                }
                if (row["UNIT_ID"] != null)
                {
                    model.UNIT_ID = row["UNIT_ID"].ToString();
                }
                if (row["MAGAZINE_NUMBER"] != null)
                {
                    model.MAGAZINE_NUMBER = row["MAGAZINE_NUMBER"].ToString();
                }
                if (row["STOCK_QTY"] != null && row["STOCK_QTY"].ToString() != "")
                {
                    model.STOCK_QTY = int.Parse(row["STOCK_QTY"].ToString());
                }
                if (row["MAGAZINE_NAME"] != null)
                {
                    model.MAGAZINE_NAME = row["MAGAZINE_NAME"].ToString();
                }
                if (row["CAPACITY_QTY"] != null && row["CAPACITY_QTY"].ToString() != "")
                {
                    model.CAPACITY_QTY = int.Parse(row["CAPACITY_QTY"].ToString());
                }
                if (row["SYN_FLAG"] != null && row["SYN_FLAG"].ToString() != "")
                {
                    model.SYN_FLAG = int.Parse(row["SYN_FLAG"].ToString());
                }
                if (row["BULLET_MODEL"] != null)
                {
                    model.BULLET_MODEL = row["BULLET_MODEL"].ToString();
                }
                if (row["MAGAZINE_STATUS"] != null && row["MAGAZINE_STATUS"].ToString() != "")
                {
                    model.MAGAZINE_STATUS = int.Parse(row["MAGAZINE_STATUS"].ToString());
                }
                if (row["BULLET_GROUP_ID"] != null)
                {
                    model.BULLET_GROUP_ID = row["BULLET_GROUP_ID"].ToString();
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
            strSql.Append("select MAGAZINE_INFO_ID,GUNARK_ID,UNIT_ID,MAGAZINE_NUMBER,STOCK_QTY,MAGAZINE_NAME,CAPACITY_QTY,SYN_FLAG,BULLET_MODEL,MAGAZINE_STATUS,BULLET_GROUP_ID ");
            strSql.Append(" FROM gunark_magazine_info ");
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
            strSql.Append("select count(1) FROM gunark_magazine_info ");
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
                strSql.Append("order by T.MAGAZINE_INFO_ID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_magazine_info T ");
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
            parameters[0].Value = "gunark_magazine_info";
            parameters[1].Value = "MAGAZINE_INFO_ID";
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

