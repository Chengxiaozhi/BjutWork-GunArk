using System;
namespace Gunark.Model
{
	/// <summary>
	/// user:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class group
	{
        public group()
		{}
        #region Model
        private string _group_id;
        private string _group_leader;
        private int? _available;
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_ID
        {
            set { _group_id = value; }
            get { return _group_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_LEADER
        {
            set { _group_leader = value; }
            get { return _group_leader; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AVAILABLE
        {
            set { _available = value; }
            get { return _available; }
        }
        #endregion Model

	}
}

