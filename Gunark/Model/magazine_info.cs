using System;
namespace Gunark.Model
{
	/// <summary>
	/// magazine_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class magazine_info
	{
		public magazine_info()
		{}
        #region Model
        private string _magazine_info_id;
        private string _gunark_id;
        private string _unit_id;
        private string _magazine_number;
        private int? _stock_qty;
        private string _magazine_name;
        private int? _capacity_qty;
        private int? _syn_flag;
        private string _bullet_model;
        private int? _magazine_status;
        private string _bullet_group_id;
        /// <summary>
        /// 
        /// </summary>
        public string MAGAZINE_INFO_ID
        {
            set { _magazine_info_id = value; }
            get { return _magazine_info_id; }
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
        public string UNIT_ID
        {
            set { _unit_id = value; }
            get { return _unit_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MAGAZINE_NUMBER
        {
            set { _magazine_number = value; }
            get { return _magazine_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? STOCK_QTY
        {
            set { _stock_qty = value; }
            get { return _stock_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MAGAZINE_NAME
        {
            set { _magazine_name = value; }
            get { return _magazine_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CAPACITY_QTY
        {
            set { _capacity_qty = value; }
            get { return _capacity_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SYN_FLAG
        {
            set { _syn_flag = value; }
            get { return _syn_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BULLET_MODEL
        {
            set { _bullet_model = value; }
            get { return _bullet_model; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MAGAZINE_STATUS
        {
            set { _magazine_status = value; }
            get { return _magazine_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BULLET_GROUP_ID
        {
            set { _bullet_group_id = value; }
            get { return _bullet_group_id; }
        }
        #endregion Model

	}
}

