using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Gunark.DAL
{
	/// <summary>
	/// 数据访问类:gunark
	/// </summary>
	public partial class gunark
	{
		public gunark()
		{}
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GUNARK_ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from gunark_gunark");
            strSql.Append(" where GUNARK_ID=@GUNARK_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUNARK_ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.gunark model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into gunark_gunark(");
            strSql.Append("GUNARK_ID,GUNARK_NUMBER,GUNARK_BIGTYPE,GUNARK_IP,GUNARK_GATEWAY,GUNARK_SUBNET,GUNARK_TYPE,GUNARK_NAME,GUNARK_SIZE,GUNARK_NUMOFGUN,GUNARK_NUMOFBULLET,GUNARK_NUMOFBULLETWAREHOUSE,GUNARK_PORT,GUNARK_LOCATION,GUNARK_PICURL,GUNARK_REMARK,GUNARK_VERIFYSTATUS,GUNARK_ISSEALUP,GUNARK_ISONLINE,GUNARK_ISPOWERON,GUNARK_ISOPEN,GUNARK_ISWARNING,GUNARK_ENTERTIME,UNITINFO_CODE,GUNARK_STATUS,GUNARK_CAMERAIP,GUNARK_VERSION,GUNARK_ALCOHOLIS,GUNARK_CODEIS,GUNARK_GUNARKIS,GUNARK_GGGBUNIQUEIS,GUNARK_ISCHECKING,GUNARK_ISOFFLINE,GUNARK_ISCARDREADING,GUNARK_EMERGENCYNUMBER)");
            strSql.Append(" values (");
            strSql.Append("@GUNARK_ID,@GUNARK_NUMBER,@GUNARK_BIGTYPE,@GUNARK_IP,@GUNARK_GATEWAY,@GUNARK_SUBNET,@GUNARK_TYPE,@GUNARK_NAME,@GUNARK_SIZE,@GUNARK_NUMOFGUN,@GUNARK_NUMOFBULLET,@GUNARK_NUMOFBULLETWAREHOUSE,@GUNARK_PORT,@GUNARK_LOCATION,@GUNARK_PICURL,@GUNARK_REMARK,@GUNARK_VERIFYSTATUS,@GUNARK_ISSEALUP,@GUNARK_ISONLINE,@GUNARK_ISPOWERON,@GUNARK_ISOPEN,@GUNARK_ISWARNING,@GUNARK_ENTERTIME,@UNITINFO_CODE,@GUNARK_STATUS,@GUNARK_CAMERAIP,@GUNARK_VERSION,@GUNARK_ALCOHOLIS,@GUNARK_CODEIS,@GUNARK_GUNARKIS,@GUNARK_GGGBUNIQUEIS,@GUNARK_ISCHECKING,@GUNARK_ISOFFLINE,@GUNARK_ISCARDREADING,@GUNARK_EMERGENCYNUMBER)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_BIGTYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_IP", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GATEWAY", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_SUBNET", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_SIZE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFGUN", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFBULLET", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFBULLETWAREHOUSE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_PORT", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_LOCATION", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_PICURL", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_VERIFYSTATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ISSEALUP", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISONLINE", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISPOWERON", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISOPEN", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISWARNING", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ENTERTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNITINFO_CODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_CAMERAIP", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_VERSION", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ALCOHOLIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_CODEIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GUNARKIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GGGBUNIQUEIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ISCHECKING", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ISOFFLINE", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ISCARDREADING", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_EMERGENCYNUMBER", MySqlDbType.Int32,10)};
            parameters[0].Value = model.GUNARK_ID;
            parameters[1].Value = model.GUNARK_NUMBER;
            parameters[2].Value = model.GUNARK_BIGTYPE;
            parameters[3].Value = model.GUNARK_IP;
            parameters[4].Value = model.GUNARK_GATEWAY;
            parameters[5].Value = model.GUNARK_SUBNET;
            parameters[6].Value = model.GUNARK_TYPE;
            parameters[7].Value = model.GUNARK_NAME;
            parameters[8].Value = model.GUNARK_SIZE;
            parameters[9].Value = model.GUNARK_NUMOFGUN;
            parameters[10].Value = model.GUNARK_NUMOFBULLET;
            parameters[11].Value = model.GUNARK_NUMOFBULLETWAREHOUSE;
            parameters[12].Value = model.GUNARK_PORT;
            parameters[13].Value = model.GUNARK_LOCATION;
            parameters[14].Value = model.GUNARK_PICURL;
            parameters[15].Value = model.GUNARK_REMARK;
            parameters[16].Value = model.GUNARK_VERIFYSTATUS;
            parameters[17].Value = model.GUNARK_ISSEALUP;
            parameters[18].Value = model.GUNARK_ISONLINE;
            parameters[19].Value = model.GUNARK_ISPOWERON;
            parameters[20].Value = model.GUNARK_ISOPEN;
            parameters[21].Value = model.GUNARK_ISWARNING;
            parameters[22].Value = model.GUNARK_ENTERTIME;
            parameters[23].Value = model.UNITINFO_CODE;
            parameters[24].Value = model.GUNARK_STATUS;
            parameters[25].Value = model.GUNARK_CAMERAIP;
            parameters[26].Value = model.GUNARK_VERSION;
            parameters[27].Value = model.GUNARK_ALCOHOLIS;
            parameters[28].Value = model.GUNARK_CODEIS;
            parameters[29].Value = model.GUNARK_GUNARKIS;
            parameters[30].Value = model.GUNARK_GGGBUNIQUEIS;
            parameters[31].Value = model.GUNARK_ISCHECKING;
            parameters[32].Value = model.GUNARK_ISOFFLINE;
            parameters[33].Value = model.GUNARK_ISCARDREADING;
            parameters[34].Value = model.GUNARK_EMERGENCYNUMBER;

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
        public bool Update(Gunark.Model.gunark model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update gunark_gunark set ");
            strSql.Append("GUNARK_NUMBER=@GUNARK_NUMBER,");
            strSql.Append("GUNARK_BIGTYPE=@GUNARK_BIGTYPE,");
            strSql.Append("GUNARK_IP=@GUNARK_IP,");
            strSql.Append("GUNARK_GATEWAY=@GUNARK_GATEWAY,");
            strSql.Append("GUNARK_SUBNET=@GUNARK_SUBNET,");
            strSql.Append("GUNARK_TYPE=@GUNARK_TYPE,");
            strSql.Append("GUNARK_NAME=@GUNARK_NAME,");
            strSql.Append("GUNARK_SIZE=@GUNARK_SIZE,");
            strSql.Append("GUNARK_NUMOFGUN=@GUNARK_NUMOFGUN,");
            strSql.Append("GUNARK_NUMOFBULLET=@GUNARK_NUMOFBULLET,");
            strSql.Append("GUNARK_NUMOFBULLETWAREHOUSE=@GUNARK_NUMOFBULLETWAREHOUSE,");
            strSql.Append("GUNARK_PORT=@GUNARK_PORT,");
            strSql.Append("GUNARK_LOCATION=@GUNARK_LOCATION,");
            strSql.Append("GUNARK_PICURL=@GUNARK_PICURL,");
            strSql.Append("GUNARK_REMARK=@GUNARK_REMARK,");
            strSql.Append("GUNARK_VERIFYSTATUS=@GUNARK_VERIFYSTATUS,");
            strSql.Append("GUNARK_ISSEALUP=@GUNARK_ISSEALUP,");
            strSql.Append("GUNARK_ISONLINE=@GUNARK_ISONLINE,");
            strSql.Append("GUNARK_ISPOWERON=@GUNARK_ISPOWERON,");
            strSql.Append("GUNARK_ISOPEN=@GUNARK_ISOPEN,");
            strSql.Append("GUNARK_ISWARNING=@GUNARK_ISWARNING,");
            strSql.Append("GUNARK_ENTERTIME=@GUNARK_ENTERTIME,");
            strSql.Append("UNITINFO_CODE=@UNITINFO_CODE,");
            strSql.Append("GUNARK_STATUS=@GUNARK_STATUS,");
            strSql.Append("GUNARK_CAMERAIP=@GUNARK_CAMERAIP,");
            strSql.Append("GUNARK_VERSION=@GUNARK_VERSION,");
            strSql.Append("GUNARK_ALCOHOLIS=@GUNARK_ALCOHOLIS,");
            strSql.Append("GUNARK_CODEIS=@GUNARK_CODEIS,");
            strSql.Append("GUNARK_GUNARKIS=@GUNARK_GUNARKIS,");
            strSql.Append("GUNARK_GGGBUNIQUEIS=@GUNARK_GGGBUNIQUEIS,");
            strSql.Append("GUNARK_ISCHECKING=@GUNARK_ISCHECKING,");
            strSql.Append("GUNARK_ISOFFLINE=@GUNARK_ISOFFLINE,");
            strSql.Append("GUNARK_ISCARDREADING=@GUNARK_ISCARDREADING,");
            strSql.Append("GUNARK_EMERGENCYNUMBER=@GUNARK_EMERGENCYNUMBER");
            strSql.Append(" where GUNARK_ID=@GUNARK_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_NUMBER", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_BIGTYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_IP", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GATEWAY", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_SUBNET", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_TYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_SIZE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFGUN", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFBULLET", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_NUMOFBULLETWAREHOUSE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_PORT", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_LOCATION", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_PICURL", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_REMARK", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_VERIFYSTATUS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ISSEALUP", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISONLINE", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISPOWERON", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISOPEN", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ISWARNING", MySqlDbType.Int32,1),
					new MySqlParameter("@GUNARK_ENTERTIME", MySqlDbType.VarChar,255),
					new MySqlParameter("@UNITINFO_CODE", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_STATUS", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_CAMERAIP", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_VERSION", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ALCOHOLIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_CODEIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GUNARKIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_GGGBUNIQUEIS", MySqlDbType.VarChar,255),
					new MySqlParameter("@GUNARK_ISCHECKING", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ISOFFLINE", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ISCARDREADING", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_EMERGENCYNUMBER", MySqlDbType.Int32,10),
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.GUNARK_NUMBER;
            parameters[1].Value = model.GUNARK_BIGTYPE;
            parameters[2].Value = model.GUNARK_IP;
            parameters[3].Value = model.GUNARK_GATEWAY;
            parameters[4].Value = model.GUNARK_SUBNET;
            parameters[5].Value = model.GUNARK_TYPE;
            parameters[6].Value = model.GUNARK_NAME;
            parameters[7].Value = model.GUNARK_SIZE;
            parameters[8].Value = model.GUNARK_NUMOFGUN;
            parameters[9].Value = model.GUNARK_NUMOFBULLET;
            parameters[10].Value = model.GUNARK_NUMOFBULLETWAREHOUSE;
            parameters[11].Value = model.GUNARK_PORT;
            parameters[12].Value = model.GUNARK_LOCATION;
            parameters[13].Value = model.GUNARK_PICURL;
            parameters[14].Value = model.GUNARK_REMARK;
            parameters[15].Value = model.GUNARK_VERIFYSTATUS;
            parameters[16].Value = model.GUNARK_ISSEALUP;
            parameters[17].Value = model.GUNARK_ISONLINE;
            parameters[18].Value = model.GUNARK_ISPOWERON;
            parameters[19].Value = model.GUNARK_ISOPEN;
            parameters[20].Value = model.GUNARK_ISWARNING;
            parameters[21].Value = model.GUNARK_ENTERTIME;
            parameters[22].Value = model.UNITINFO_CODE;
            parameters[23].Value = model.GUNARK_STATUS;
            parameters[24].Value = model.GUNARK_CAMERAIP;
            parameters[25].Value = model.GUNARK_VERSION;
            parameters[26].Value = model.GUNARK_ALCOHOLIS;
            parameters[27].Value = model.GUNARK_CODEIS;
            parameters[28].Value = model.GUNARK_GUNARKIS;
            parameters[29].Value = model.GUNARK_GGGBUNIQUEIS;
            parameters[30].Value = model.GUNARK_ISCHECKING;
            parameters[31].Value = model.GUNARK_ISOFFLINE;
            parameters[32].Value = model.GUNARK_ISCARDREADING;
            parameters[33].Value = model.GUNARK_EMERGENCYNUMBER;
            parameters[34].Value = model.GUNARK_ID;

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
        public bool Delete(string GUNARK_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_gunark ");
            strSql.Append(" where GUNARK_ID=@GUNARK_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUNARK_ID;

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
        public bool DeleteList(string GUNARK_IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from gunark_gunark ");
            strSql.Append(" where GUNARK_ID in (" + GUNARK_IDlist + ")  ");
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
        public Gunark.Model.gunark GetModel(string GUNARK_ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GUNARK_ID,GUNARK_NUMBER,GUNARK_BIGTYPE,GUNARK_IP,GUNARK_GATEWAY,GUNARK_SUBNET,GUNARK_TYPE,GUNARK_NAME,GUNARK_SIZE,GUNARK_NUMOFGUN,GUNARK_NUMOFBULLET,GUNARK_NUMOFBULLETWAREHOUSE,GUNARK_PORT,GUNARK_LOCATION,GUNARK_PICURL,GUNARK_REMARK,GUNARK_VERIFYSTATUS,GUNARK_ISSEALUP,GUNARK_ISONLINE,GUNARK_ISPOWERON,GUNARK_ISOPEN,GUNARK_ISWARNING,GUNARK_ENTERTIME,UNITINFO_CODE,GUNARK_STATUS,GUNARK_CAMERAIP,GUNARK_VERSION,GUNARK_ALCOHOLIS,GUNARK_CODEIS,GUNARK_GUNARKIS,GUNARK_GGGBUNIQUEIS,GUNARK_ISCHECKING,GUNARK_ISOFFLINE,GUNARK_ISCARDREADING,GUNARK_EMERGENCYNUMBER from gunark_gunark ");
            strSql.Append(" where GUNARK_ID=@GUNARK_ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_ID", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUNARK_ID;

            Gunark.Model.gunark model = new Gunark.Model.gunark();
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
        public Gunark.Model.gunark GetModelByIp(string GUNARK_IP)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GUNARK_ID,GUNARK_NUMBER,GUNARK_BIGTYPE,GUNARK_IP,GUNARK_GATEWAY,GUNARK_SUBNET,GUNARK_TYPE,GUNARK_NAME,GUNARK_SIZE,GUNARK_NUMOFGUN,GUNARK_NUMOFBULLET,GUNARK_NUMOFBULLETWAREHOUSE,GUNARK_PORT,GUNARK_LOCATION,GUNARK_PICURL,GUNARK_REMARK,GUNARK_VERIFYSTATUS,GUNARK_ISSEALUP,GUNARK_ISONLINE,GUNARK_ISPOWERON,GUNARK_ISOPEN,GUNARK_ISWARNING,GUNARK_ENTERTIME,UNITINFO_CODE,GUNARK_STATUS,GUNARK_CAMERAIP,GUNARK_VERSION,GUNARK_ALCOHOLIS,GUNARK_CODEIS,GUNARK_GUNARKIS,GUNARK_GGGBUNIQUEIS,GUNARK_ISCHECKING,GUNARK_ISOFFLINE,GUNARK_ISCARDREADING,GUNARK_EMERGENCYNUMBER from gunark_gunark ");
            strSql.Append(" where GUNARK_IP=@GUNARK_IP ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@GUNARK_IP", MySqlDbType.VarChar,255)			};
            parameters[0].Value = GUNARK_IP;

            Gunark.Model.gunark model = new Gunark.Model.gunark();
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
        public Gunark.Model.gunark DataRowToModel(DataRow row)
        {
            Gunark.Model.gunark model = new Gunark.Model.gunark();
            if (row != null)
            {
                if (row["GUNARK_ID"] != null)
                {
                    model.GUNARK_ID = row["GUNARK_ID"].ToString();
                }
                if (row["GUNARK_NUMBER"] != null)
                {
                    model.GUNARK_NUMBER = row["GUNARK_NUMBER"].ToString();
                }
                if (row["GUNARK_BIGTYPE"] != null)
                {
                    model.GUNARK_BIGTYPE = row["GUNARK_BIGTYPE"].ToString();
                }
                if (row["GUNARK_IP"] != null)
                {
                    model.GUNARK_IP = row["GUNARK_IP"].ToString();
                }
                if (row["GUNARK_GATEWAY"] != null)
                {
                    model.GUNARK_GATEWAY = row["GUNARK_GATEWAY"].ToString();
                }
                if (row["GUNARK_SUBNET"] != null)
                {
                    model.GUNARK_SUBNET = row["GUNARK_SUBNET"].ToString();
                }
                if (row["GUNARK_TYPE"] != null)
                {
                    model.GUNARK_TYPE = row["GUNARK_TYPE"].ToString();
                }
                if (row["GUNARK_NAME"] != null)
                {
                    model.GUNARK_NAME = row["GUNARK_NAME"].ToString();
                }
                if (row["GUNARK_SIZE"] != null)
                {
                    model.GUNARK_SIZE = row["GUNARK_SIZE"].ToString();
                }
                if (row["GUNARK_NUMOFGUN"] != null)
                {
                    model.GUNARK_NUMOFGUN = row["GUNARK_NUMOFGUN"].ToString();
                }
                if (row["GUNARK_NUMOFBULLET"] != null)
                {
                    model.GUNARK_NUMOFBULLET = row["GUNARK_NUMOFBULLET"].ToString();
                }
                if (row["GUNARK_NUMOFBULLETWAREHOUSE"] != null)
                {
                    model.GUNARK_NUMOFBULLETWAREHOUSE = row["GUNARK_NUMOFBULLETWAREHOUSE"].ToString();
                }
                if (row["GUNARK_PORT"] != null)
                {
                    model.GUNARK_PORT = row["GUNARK_PORT"].ToString();
                }
                if (row["GUNARK_LOCATION"] != null)
                {
                    model.GUNARK_LOCATION = row["GUNARK_LOCATION"].ToString();
                }
                if (row["GUNARK_PICURL"] != null)
                {
                    model.GUNARK_PICURL = row["GUNARK_PICURL"].ToString();
                }
                if (row["GUNARK_REMARK"] != null)
                {
                    model.GUNARK_REMARK = row["GUNARK_REMARK"].ToString();
                }
                if (row["GUNARK_VERIFYSTATUS"] != null)
                {
                    model.GUNARK_VERIFYSTATUS = row["GUNARK_VERIFYSTATUS"].ToString();
                }
                if (row["GUNARK_ISSEALUP"] != null && row["GUNARK_ISSEALUP"].ToString() != "")
                {
                    model.GUNARK_ISSEALUP = int.Parse(row["GUNARK_ISSEALUP"].ToString());
                }
                if (row["GUNARK_ISONLINE"] != null && row["GUNARK_ISONLINE"].ToString() != "")
                {
                    model.GUNARK_ISONLINE = int.Parse(row["GUNARK_ISONLINE"].ToString());
                }
                if (row["GUNARK_ISPOWERON"] != null && row["GUNARK_ISPOWERON"].ToString() != "")
                {
                    model.GUNARK_ISPOWERON = int.Parse(row["GUNARK_ISPOWERON"].ToString());
                }
                if (row["GUNARK_ISOPEN"] != null && row["GUNARK_ISOPEN"].ToString() != "")
                {
                    model.GUNARK_ISOPEN = int.Parse(row["GUNARK_ISOPEN"].ToString());
                }
                if (row["GUNARK_ISWARNING"] != null && row["GUNARK_ISWARNING"].ToString() != "")
                {
                    model.GUNARK_ISWARNING = int.Parse(row["GUNARK_ISWARNING"].ToString());
                }
                if (row["GUNARK_ENTERTIME"] != null)
                {
                    model.GUNARK_ENTERTIME = row["GUNARK_ENTERTIME"].ToString();
                }
                if (row["UNITINFO_CODE"] != null)
                {
                    model.UNITINFO_CODE = row["UNITINFO_CODE"].ToString();
                }
                if (row["GUNARK_STATUS"] != null && row["GUNARK_STATUS"].ToString() != "")
                {
                    model.GUNARK_STATUS = int.Parse(row["GUNARK_STATUS"].ToString());
                }
                if (row["GUNARK_CAMERAIP"] != null)
                {
                    model.GUNARK_CAMERAIP = row["GUNARK_CAMERAIP"].ToString();
                }
                if (row["GUNARK_VERSION"] != null)
                {
                    model.GUNARK_VERSION = row["GUNARK_VERSION"].ToString();
                }
                if (row["GUNARK_ALCOHOLIS"] != null)
                {
                    model.GUNARK_ALCOHOLIS = row["GUNARK_ALCOHOLIS"].ToString();
                }
                if (row["GUNARK_CODEIS"] != null)
                {
                    model.GUNARK_CODEIS = row["GUNARK_CODEIS"].ToString();
                }
                if (row["GUNARK_GUNARKIS"] != null)
                {
                    model.GUNARK_GUNARKIS = row["GUNARK_GUNARKIS"].ToString();
                }
                if (row["GUNARK_GGGBUNIQUEIS"] != null)
                {
                    model.GUNARK_GGGBUNIQUEIS = row["GUNARK_GGGBUNIQUEIS"].ToString();
                }
                if (row["GUNARK_ISCHECKING"] != null && row["GUNARK_ISCHECKING"].ToString() != "")
                {
                    model.GUNARK_ISCHECKING = int.Parse(row["GUNARK_ISCHECKING"].ToString());
                }
                if (row["GUNARK_ISOFFLINE"] != null && row["GUNARK_ISOFFLINE"].ToString() != "")
                {
                    model.GUNARK_ISOFFLINE = int.Parse(row["GUNARK_ISOFFLINE"].ToString());
                }
                if (row["GUNARK_ISCARDREADING"] != null && row["GUNARK_ISCARDREADING"].ToString() != "")
                {
                    model.GUNARK_ISCARDREADING = int.Parse(row["GUNARK_ISCARDREADING"].ToString());
                }
                if (row["GUNARK_EMERGENCYNUMBER"] != null && row["GUNARK_EMERGENCYNUMBER"].ToString() != "")
                {
                    model.GUNARK_EMERGENCYNUMBER = int.Parse(row["GUNARK_EMERGENCYNUMBER"].ToString());
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
            strSql.Append("select GUNARK_ID,GUNARK_NUMBER,GUNARK_BIGTYPE,GUNARK_IP,GUNARK_GATEWAY,GUNARK_SUBNET,GUNARK_TYPE,GUNARK_NAME,GUNARK_SIZE,GUNARK_NUMOFGUN,GUNARK_NUMOFBULLET,GUNARK_NUMOFBULLETWAREHOUSE,GUNARK_PORT,GUNARK_LOCATION,GUNARK_PICURL,GUNARK_REMARK,GUNARK_VERIFYSTATUS,GUNARK_ISSEALUP,GUNARK_ISONLINE,GUNARK_ISPOWERON,GUNARK_ISOPEN,GUNARK_ISWARNING,GUNARK_ENTERTIME,UNITINFO_CODE,GUNARK_STATUS,GUNARK_CAMERAIP,GUNARK_VERSION,GUNARK_ALCOHOLIS,GUNARK_CODEIS,GUNARK_GUNARKIS,GUNARK_GGGBUNIQUEIS,GUNARK_ISCHECKING,GUNARK_ISOFFLINE,GUNARK_ISCARDREADING,GUNARK_EMERGENCYNUMBER ");
            strSql.Append(" FROM gunark_gunark ");
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
            strSql.Append("select count(1) FROM gunark_gunark ");
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
                strSql.Append("order by T.GUNARK_ID desc");
            }
            strSql.Append(")AS Row, T.*  from gunark_gunark T ");
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
            parameters[0].Value = "gunark_gunark";
            parameters[1].Value = "GUNARK_ID";
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

