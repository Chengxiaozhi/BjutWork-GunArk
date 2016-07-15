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
	public partial class user
	{
		public user()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string USER_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_user");
            strSql.Append(" where USER_ID=@USER_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = USER_ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_user(");
            strSql.Append("USER_ID,USER_POLICENUMB,USER_REALNAME,USER_NAME,USER_PWD,USER_STATE,USER_ADDRESS,USER_OFFICETELEP,USER_MOBILTELEP,USER_SEX,USER_EMAIL,USER_POSTCODE,USER_PRIVIEGES,USER_GUNLICENSE,USER_GUNLICENSEDATE,USER_POLICEKINDS,USER_RANK,USER_REMARK,USER_CARD,USER_JOBSTATUS,UNITINFO_CODE,USER_FINGER_ID,GROUP_ID,USER_BANNED)");
            strSql.Append(" values (");
            strSql.Append("@USER_ID,@USER_POLICENUMB,@USER_REALNAME,@USER_NAME,@USER_PWD,@USER_STATE,@USER_ADDRESS,@USER_OFFICETELEP,@USER_MOBILTELEP,@USER_SEX,@USER_EMAIL,@USER_POSTCODE,@USER_PRIVIEGES,@USER_GUNLICENSE,@USER_GUNLICENSEDATE,@USER_POLICEKINDS,@USER_RANK,@USER_REMARK,@USER_CARD,@USER_JOBSTATUS,@UNITINFO_CODE,@USER_FINGER_ID,@GROUP_ID,@USER_BANNED)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POLICENUMB", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_REALNAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PWD", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_STATE", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_ADDRESS", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_OFFICETELEP", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_MOBILTELEP", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_SEX", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_EMAIL", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POSTCODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PRIVIEGES", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_GUNLICENSE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_GUNLICENSEDATE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POLICEKINDS", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_RANK", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_CARD", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_JOBSTATUS", MySqlDbType.Int32,1),
					new MySqlParameter("@UNITINFO_CODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_FINGER_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@USER_BANNED", MySqlDbType.Int32,10)};
            parameters[0].Value = model.USER_ID;
            parameters[1].Value = model.USER_POLICENUMB;
            parameters[2].Value = model.USER_REALNAME;
            parameters[3].Value = model.USER_NAME;
            parameters[4].Value = model.USER_PWD;
            parameters[5].Value = model.USER_STATE;
            parameters[6].Value = model.USER_ADDRESS;
            parameters[7].Value = model.USER_OFFICETELEP;
            parameters[8].Value = model.USER_MOBILTELEP;
            parameters[9].Value = model.USER_SEX;
            parameters[10].Value = model.USER_EMAIL;
            parameters[11].Value = model.USER_POSTCODE;
            parameters[12].Value = model.USER_PRIVIEGES;
            parameters[13].Value = model.USER_GUNLICENSE;
            parameters[14].Value = model.USER_GUNLICENSEDATE;
            parameters[15].Value = model.USER_POLICEKINDS;
            parameters[16].Value = model.USER_RANK;
            parameters[17].Value = model.USER_REMARK;
            parameters[18].Value = model.USER_CARD;
            parameters[19].Value = model.USER_JOBSTATUS;
            parameters[20].Value = model.UNITINFO_CODE;
            parameters[21].Value = model.USER_FINGER_ID;
            parameters[22].Value = model.GROUP_ID;
            parameters[23].Value = model.USER_BANNED;

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
        public bool Update(Gunark.Model.user model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_user set ");
            strSql.Append("USER_POLICENUMB=@USER_POLICENUMB,");
            strSql.Append("USER_REALNAME=@USER_REALNAME,");
            strSql.Append("USER_NAME=@USER_NAME,");
            strSql.Append("USER_PWD=@USER_PWD,");
            strSql.Append("USER_STATE=@USER_STATE,");
            strSql.Append("USER_ADDRESS=@USER_ADDRESS,");
            strSql.Append("USER_OFFICETELEP=@USER_OFFICETELEP,");
            strSql.Append("USER_MOBILTELEP=@USER_MOBILTELEP,");
            strSql.Append("USER_SEX=@USER_SEX,");
            strSql.Append("USER_EMAIL=@USER_EMAIL,");
            strSql.Append("USER_POSTCODE=@USER_POSTCODE,");
            strSql.Append("USER_PRIVIEGES=@USER_PRIVIEGES,");
            strSql.Append("USER_GUNLICENSE=@USER_GUNLICENSE,");
            strSql.Append("USER_GUNLICENSEDATE=@USER_GUNLICENSEDATE,");
            strSql.Append("USER_POLICEKINDS=@USER_POLICEKINDS,");
            strSql.Append("USER_RANK=@USER_RANK,");
            strSql.Append("USER_REMARK=@USER_REMARK,");
            strSql.Append("USER_CARD=@USER_CARD,");
            strSql.Append("USER_JOBSTATUS=@USER_JOBSTATUS,");
            strSql.Append("UNITINFO_CODE=@UNITINFO_CODE,");
            strSql.Append("USER_FINGER_ID=@USER_FINGER_ID,");
            strSql.Append("GROUP_ID=@GROUP_ID,");
            strSql.Append("USER_BANNED=@USER_BANNED");
            strSql.Append(" where USER_ID=@USER_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_POLICENUMB", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_REALNAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PWD", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_STATE", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_ADDRESS", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_OFFICETELEP", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_MOBILTELEP", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_SEX", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_EMAIL", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POSTCODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_PRIVIEGES", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_GUNLICENSE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_GUNLICENSEDATE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_POLICEKINDS", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_RANK", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_CARD", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_JOBSTATUS", MySqlDbType.Int32,1),
					new MySqlParameter("@UNITINFO_CODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@USER_FINGER_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GROUP_ID", MySqlDbType.VarChar,50),
					new MySqlParameter("@USER_BANNED", MySqlDbType.Int32,10),
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.USER_POLICENUMB;
            parameters[1].Value = model.USER_REALNAME;
            parameters[2].Value = model.USER_NAME;
            parameters[3].Value = model.USER_PWD;
            parameters[4].Value = model.USER_STATE;
            parameters[5].Value = model.USER_ADDRESS;
            parameters[6].Value = model.USER_OFFICETELEP;
            parameters[7].Value = model.USER_MOBILTELEP;
            parameters[8].Value = model.USER_SEX;
            parameters[9].Value = model.USER_EMAIL;
            parameters[10].Value = model.USER_POSTCODE;
            parameters[11].Value = model.USER_PRIVIEGES;
            parameters[12].Value = model.USER_GUNLICENSE;
            parameters[13].Value = model.USER_GUNLICENSEDATE;
            parameters[14].Value = model.USER_POLICEKINDS;
            parameters[15].Value = model.USER_RANK;
            parameters[16].Value = model.USER_REMARK;
            parameters[17].Value = model.USER_CARD;
            parameters[18].Value = model.USER_JOBSTATUS;
            parameters[19].Value = model.UNITINFO_CODE;
            parameters[20].Value = model.USER_FINGER_ID;
            parameters[21].Value = model.GROUP_ID;
            parameters[22].Value = model.USER_BANNED;
            parameters[23].Value = model.USER_ID;

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
        public bool Delete(string USER_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_user ");
            strSql.Append(" where USER_ID=@USER_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = USER_ID;

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
        public bool DeleteList(string USER_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_user ");
            strSql.Append(" where USER_ID in (" + USER_IDlist + ")  ");
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
        public Gunark.Model.user GetModel(string USER_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select USER_ID,USER_POLICENUMB,USER_REALNAME,USER_NAME,USER_PWD,USER_STATE,USER_ADDRESS,USER_OFFICETELEP,USER_MOBILTELEP,USER_SEX,USER_EMAIL,USER_POSTCODE,USER_PRIVIEGES,USER_GUNLICENSE,USER_GUNLICENSEDATE,USER_POLICEKINDS,USER_RANK,USER_REMARK,USER_CARD,USER_JOBSTATUS,UNITINFO_CODE,USER_FINGER_ID,GROUP_ID,USER_BANNED from gunark_user ");
            strSql.Append(" where USER_ID=@USER_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = USER_ID;

            Gunark.Model.user model = new Gunark.Model.user();
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
        /// 得到一个对象实体（根据姓名）
        /// </summary>
        public Gunark.Model.user GetModelByName(string USER_REALNAME)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select USER_ID,USER_POLICENUMB,USER_REALNAME,USER_NAME,USER_PWD,USER_STATE,USER_ADDRESS,USER_OFFICETELEP,USER_MOBILTELEP,USER_SEX,USER_EMAIL,USER_POSTCODE,USER_PRIVIEGES,USER_GUNLICENSE,USER_GUNLICENSEDATE,USER_POLICEKINDS,USER_RANK,USER_REMARK,USER_CARD,USER_JOBSTATUS,UNITINFO_CODE,USER_FINGER_ID,GROUP_ID,USER_BANNED from gunark_user ");
            strSql.Append(" where USER_REALNAME=@USER_REALNAME ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@USER_REALNAME", MySqlDbType.VarChar,255)			};
            parameters[0].Value = USER_REALNAME;

            Gunark.Model.user model = new Gunark.Model.user();
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
        public Gunark.Model.user DataRowToModel(DataRow row)
        {
            Gunark.Model.user model = new Gunark.Model.user();
            if (row != null)
            {
                if (row["USER_ID"] != null)
                {
                    model.USER_ID = row["USER_ID"].ToString();
                }
                if (row["USER_POLICENUMB"] != null)
                {
                    model.USER_POLICENUMB = row["USER_POLICENUMB"].ToString();
                }
                if (row["USER_REALNAME"] != null)
                {
                    model.USER_REALNAME = row["USER_REALNAME"].ToString();
                }
                if (row["USER_NAME"] != null)
                {
                    model.USER_NAME = row["USER_NAME"].ToString();
                }
                if (row["USER_PWD"] != null)
                {
                    model.USER_PWD = row["USER_PWD"].ToString();
                }
                if (row["USER_STATE"] != null && row["USER_STATE"].ToString() != "")
                {
                    model.USER_STATE = int.Parse(row["USER_STATE"].ToString());
                }
                if (row["USER_ADDRESS"] != null)
                {
                    model.USER_ADDRESS = row["USER_ADDRESS"].ToString();
                }
                if (row["USER_OFFICETELEP"] != null)
                {
                    model.USER_OFFICETELEP = row["USER_OFFICETELEP"].ToString();
                }
                if (row["USER_MOBILTELEP"] != null)
                {
                    model.USER_MOBILTELEP = row["USER_MOBILTELEP"].ToString();
                }
                if (row["USER_SEX"] != null && row["USER_SEX"].ToString() != "")
                {
                    model.USER_SEX = int.Parse(row["USER_SEX"].ToString());
                }
                if (row["USER_EMAIL"] != null)
                {
                    model.USER_EMAIL = row["USER_EMAIL"].ToString();
                }
                if (row["USER_POSTCODE"] != null)
                {
                    model.USER_POSTCODE = row["USER_POSTCODE"].ToString();
                }
                if (row["USER_PRIVIEGES"] != null && row["USER_PRIVIEGES"].ToString() != "")
                {
                    model.USER_PRIVIEGES = int.Parse(row["USER_PRIVIEGES"].ToString());
                }
                if (row["USER_GUNLICENSE"] != null)
                {
                    model.USER_GUNLICENSE = row["USER_GUNLICENSE"].ToString();
                }
                if (row["USER_GUNLICENSEDATE"] != null)
                {
                    model.USER_GUNLICENSEDATE = row["USER_GUNLICENSEDATE"].ToString();
                }
                if (row["USER_POLICEKINDS"] != null)
                {
                    model.USER_POLICEKINDS = row["USER_POLICEKINDS"].ToString();
                }
                if (row["USER_RANK"] != null)
                {
                    model.USER_RANK = row["USER_RANK"].ToString();
                }
                if (row["USER_REMARK"] != null)
                {
                    model.USER_REMARK = row["USER_REMARK"].ToString();
                }
                if (row["USER_CARD"] != null)
                {
                    model.USER_CARD = row["USER_CARD"].ToString();
                }
                if (row["USER_JOBSTATUS"] != null && row["USER_JOBSTATUS"].ToString() != "")
                {
                    model.USER_JOBSTATUS = int.Parse(row["USER_JOBSTATUS"].ToString());
                }
                if (row["UNITINFO_CODE"] != null)
                {
                    model.UNITINFO_CODE = row["UNITINFO_CODE"].ToString();
                }
                if (row["USER_FINGER_ID"] != null)
                {
                    model.USER_FINGER_ID = row["USER_FINGER_ID"].ToString();
                }
                if (row["GROUP_ID"] != null)
                {
                    model.GROUP_ID = row["GROUP_ID"].ToString();
                }
                if (row["USER_BANNED"] != null && row["USER_BANNED"].ToString() != "")
                {
                    model.USER_BANNED = int.Parse(row["USER_BANNED"].ToString());
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
            strSql.Append("select USER_ID,USER_POLICENUMB,USER_REALNAME,USER_NAME,USER_PWD,USER_STATE,USER_ADDRESS,USER_OFFICETELEP,USER_MOBILTELEP,USER_SEX,USER_EMAIL,USER_POSTCODE,USER_PRIVIEGES,USER_GUNLICENSE,USER_GUNLICENSEDATE,USER_POLICEKINDS,USER_RANK,USER_REMARK,USER_CARD,USER_JOBSTATUS,UNITINFO_CODE,USER_FINGER_ID,GROUP_ID,USER_BANNED ");
            strSql.Append(" FROM gunark_user ");
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
            strSql.Append("select count(1) FROM gunark_user ");
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
                strSql.Append("order by T.USER_ID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_user T ");
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
            parameters[0].Value = "gunark_user";
            parameters[1].Value = "USER_ID";
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

