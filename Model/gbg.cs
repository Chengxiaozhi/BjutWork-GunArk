using System;
namespace Gunark.Model
{
	/// <summary>
	/// user:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class gbg
	{
        public gbg()
		{}
        #region Model
        private string _gggbid;
        private string _gunark_id;
        private int? _gun_location;
        private int? _bullet_location;
        private string _group_id;
        /// <summary>
        /// 
        /// </summary>
        public string GGGBID
        {
            set { _gggbid = value; }
            get { return _gggbid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GUNARK_ID
        {
            set { _gunark_id = value; }
            get { return _gunark_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GUN_LOCATION
        {
            set { _gun_location = value; }
            get { return _gun_location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BULLET_LOCATION
        {
            set { _bullet_location = value; }
            get { return _bullet_location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_ID
        {
            set { _group_id = value; }
            get { return _group_id; }
        }
        #endregion Model
	}
}

