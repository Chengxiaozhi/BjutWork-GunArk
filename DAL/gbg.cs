using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:user
	/// </summary>
	public partial class gbg
	{
        public gbg()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GGGBID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_gbg");
            strSql.Append(" where GGGBID=@GGGBID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GGGBID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GGGBID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.gbg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_gbg(");
            strSql.Append("GGGBID,GUNARK_ID,GUN_LOCATION,BULLET_LOCATION,GROUP_ID)");
            strSql.Append(" values (");
            strSql.Append("@GGGBID,@GUNARK_ID,@GUN_LOCATION,@BULLET_LOCATION,@GROUP_ID)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GGGBID", MySqlDbType.VarChar,50),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_LOCATION", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_LOCATION", MySqlDbType.Int32,10),
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.GGGBID;
            parameters[1].Value = model.GUNARK_ID;
            parameters[2].Value = model.GUN_LOCATION;
            parameters[3].Value = model.BULLET_LOCATION;
            parameters[4].Value = model.GROUP_ID;

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
        public bool Update(Gunark.Model.gbg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_gbg set ");
            strSql.Append("GUNARK_ID=@GUNARK_ID,");
            strSql.Append("GUN_LOCATION=@GUN_LOCATION,");
            strSql.Append("BULLET_LOCATION=@BULLET_LOCATION,");
            strSql.Append("GROUP_ID=@GROUP_ID");
            strSql.Append(" where GGGBID=@GGGBID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUN_LOCATION", MySqlDbType.Int32,10),
					new MySqlParameter("@BULLET_LOCATION", MySqlDbType.Int32,10),
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GGGBID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.GUNARK_ID;
            parameters[1].Value = model.GUN_LOCATION;
            parameters[2].Value = model.BULLET_LOCATION;
            parameters[3].Value = model.GROUP_ID;
            parameters[4].Value = model.GGGBID;

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
        public bool Delete(string GGGBID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_gbg ");
            strSql.Append(" where GGGBID=@GGGBID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GGGBID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GGGBID;

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
        public bool DeleteList(string GGGBIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_gbg ");
            strSql.Append(" where GGGBID in (" + GGGBIDlist + ")  ");
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
        public Gunark.Model.gbg GetModel(string GGGBID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GGGBID,GUNARK_ID,GUN_LOCATION,BULLET_LOCATION,GROUP_ID from gunark_gbg ");
            strSql.Append(" where GGGBID=@GGGBID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GGGBID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GGGBID;

            Gunark.Model.gbg model = new Gunark.Model.gbg();
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
        /// 得到一个对象实体（根据枪柜id和枪位号）
        /// </summary>
        public Gunark.Model.gbg GetModelByGunPos(string gunarkId, string gunLocation)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GGGBID,GUNARK_ID,GUN_LOCATION,BULLET_LOCATION,GROUP_ID from gunark_gbg ");
            strSql.Append(" where GUNARK_ID=@GUNARK_ID and GUN_LOCATION=@GUN_LOCATION ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
                    new MySqlParameter("@GUN_LOCATION", MySqlDbType.Int32,10)
                                          };
            parameters[0].Value = gunarkId;
            parameters[1].Value = gunLocation;

            Gunark.Model.gbg model = new Gunark.Model.gbg();
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
        public Gunark.Model.gbg DataRowToModel(DataRow row)
        {
            Gunark.Model.gbg model = new Gunark.Model.gbg();
            if (row != null)
            {
                if (row["GGGBID"] != null)
                {
                    model.GGGBID = row["GGGBID"].ToString();
                }
                if (row["GUNARK_ID"] != null)
                {
                    model.GUNARK_ID = row["GUNARK_ID"].ToString();
                }
                if (row["GUN_LOCATION"] != null && row["GUN_LOCATION"].ToString() != "")
                {
                    model.GUN_LOCATION = int.Parse(row["GUN_LOCATION"].ToString());
                }
                if (row["BULLET_LOCATION"] != null && row["BULLET_LOCATION"].ToString() != "")
                {
                    model.BULLET_LOCATION = int.Parse(row["BULLET_LOCATION"].ToString());
                }
                if (row["GROUP_ID"] != null)
                {
                    model.GROUP_ID = row["GROUP_ID"].ToString();
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
            strSql.Append("select GGGBID,GUNARK_ID,GUN_LOCATION,BULLET_LOCATION,GROUP_ID ");
            strSql.Append(" FROM gunark_gbg ");
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
            strSql.Append("select count(1) FROM gunark_gbg ");
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
                strSql.Append("order by T.GGGBID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_gbg T ");
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
            parameters[0].Value = "gunark_gbg";
            parameters[1].Value = "GGGBID";
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

