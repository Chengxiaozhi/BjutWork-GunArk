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
	public partial class group
	{
        public group()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GROUP_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_group");
            strSql.Append(" where GROUP_ID=@GROUP_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GROUP_ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.group model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_group(");
            strSql.Append("GROUP_ID,GROUP_LEADER,AVAILABLE)");
            strSql.Append(" values (");
            strSql.Append("@GROUP_ID,@GROUP_LEADER,@AVAILABLE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@GROUP_LEADER", MySqlDbType.VarChar,255),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Int32,10)};
            parameters[0].Value = model.GROUP_ID;
            parameters[1].Value = model.GROUP_LEADER;
            parameters[2].Value = model.AVAILABLE;

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
        public bool Update(Gunark.Model.group model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_group set ");
            strSql.Append("GROUP_LEADER=@GROUP_LEADER,");
            strSql.Append("AVAILABLE=@AVAILABLE");
            strSql.Append(" where GROUP_ID=@GROUP_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GROUP_LEADER", MySqlDbType.VarChar,255),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Int32,10),
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.GROUP_LEADER;
            parameters[1].Value = model.AVAILABLE;
            parameters[2].Value = model.GROUP_ID;

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
        public bool Delete(string GROUP_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_group ");
            strSql.Append(" where GROUP_ID=@GROUP_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GROUP_ID;

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
        public bool DeleteList(string GROUP_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_group ");
            strSql.Append(" where GROUP_ID in (" + GROUP_IDlist + ")  ");
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
        public Gunark.Model.group GetModel(string GROUP_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GROUP_ID,GROUP_LEADER,AVAILABLE from gunark_group ");
            strSql.Append(" where GROUP_ID=@GROUP_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = GROUP_ID;

            Gunark.Model.group model = new Gunark.Model.group();
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
        public Gunark.Model.group DataRowToModel(DataRow row)
        {
            Gunark.Model.group model = new Gunark.Model.group();
            if (row != null)
            {
                if (row["GROUP_ID"] != null)
                {
                    model.GROUP_ID = row["GROUP_ID"].ToString();
                }
                if (row["GROUP_LEADER"] != null)
                {
                    model.GROUP_LEADER = row["GROUP_LEADER"].ToString();
                }
                if (row["AVAILABLE"] != null && row["AVAILABLE"].ToString() != "")
                {
                    model.AVAILABLE = int.Parse(row["AVAILABLE"].ToString());
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
            strSql.Append("select GROUP_ID,GROUP_LEADER,AVAILABLE ");
            strSql.Append(" FROM gunark_group ");
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
            strSql.Append("select count(1) FROM gunark_group ");
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
                strSql.Append("order by T.GROUP_ID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_group T ");
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
            parameters[0].Value = "gunark_group";
            parameters[1].Value = "GROUP_ID";
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

