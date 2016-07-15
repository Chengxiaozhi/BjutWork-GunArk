using System;
namespace Gunark.Model
{
	/// <summary>
	/// fingerprint:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class fingerprint
	{
		public fingerprint()
		{}
        #region Model
        private string _user_fingerprint_id;
        private string _user_policenumb;
        private string _user_name;
        private string _user_pwd;
        private string _finger_number;
        private string _unit_id;
        private byte[] _user_fingerprint;
        private int _id;
        private string _user_type;
        private int? _is_update;
        private int? _user_ban;
        private int? _user_privieges;
        /// <summary>
        /// 
        /// </summary>
        public string USER_FINGERPRINT_ID
        {
            set { _user_fingerprint_id = value; }
            get { return _user_fingerprint_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_POLICENUMB
        {
            set { _user_policenumb = value; }
            get { return _user_policenumb; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_PWD
        {
            set { _user_pwd = value; }
            get { return _user_pwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FINGER_NUMBER
        {
            set { _finger_number = value; }
            get { return _finger_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIT_ID
        {
            set { _unit_id = value; }
            get { return _unit_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] USER_FINGERPRINT
        {
            set { _user_fingerprint = value; }
            get { return _user_fingerprint; }
        }
        /// <summary>
        /// auto_increment
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_TYPE
        {
            set { _user_type = value; }
            get { return _user_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IS_UPDATE
        {
            set { _is_update = value; }
            get { return _is_update; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? USER_BAN
        {
            set { _user_ban = value; }
            get { return _user_ban; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? USER_PRIVIEGES
        {
            set { _user_privieges = value; }
            get { return _user_privieges; }
        }
        #endregion Model
	}
}

