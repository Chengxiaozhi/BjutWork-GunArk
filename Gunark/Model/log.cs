using System;
namespace Gunark.Model
{
	/// <summary>
	/// log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class log
	{
		public log()
		{}
        #region Model
        private int _id;
        private string _log_time;
        private string _log_discribe;
        private int? _log_type;
        private string _opreat_user;
        private string _bullet_number;
        private string _gun_number;
        private string _alarm_type;
        private int? _alarm_discribe;
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
        public string LOG_TIME
        {
            set { _log_time = value; }
            get { return _log_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LOG_DISCRIBE
        {
            set { _log_discribe = value; }
            get { return _log_discribe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LOG_TYPE
        {
            set { _log_type = value; }
            get { return _log_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OPREAT_USER
        {
            set { _opreat_user = value; }
            get { return _opreat_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BULLET_NUMBER
        {
            set { _bullet_number = value; }
            get { return _bullet_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GUN_NUMBER
        {
            set { _gun_number = value; }
            get { return _gun_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ALARM_TYPE
        {
            set { _alarm_type = value; }
            get { return _alarm_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ALARM_DISCRIBE
        {
            set { _alarm_discribe = value; }
            get { return _alarm_discribe; }
        }
        #endregion Model

	}
}

