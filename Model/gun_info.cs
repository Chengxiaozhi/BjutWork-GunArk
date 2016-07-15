using System;
namespace Gunark.Model
{
	/// <summary>
	/// gun_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class gun_info
	{
		public gun_info()
		{}
		#region Model
		private string _gun_info_id;
		private string _gunark_id;
		private string _unit_id;
		private string _gun_number;
		private string _gun_type;
		private string _gun_status;
		private string _gun_bullet_location;
		private string _loss_description;
		private string _remark;
		private DateTime? _in_time;
		private DateTime? _out_time;
		private int? _syn_flag;
		/// <summary>
		/// 
		/// </summary>
		public string GUN_INFO_ID
		{
			set{ _gun_info_id=value;}
			get{return _gun_info_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUNARK_ID
		{
			set{ _gunark_id=value;}
			get{return _gunark_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UNIT_ID
		{
			set{ _unit_id=value;}
			get{return _unit_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_NUMBER
		{
			set{ _gun_number=value;}
			get{return _gun_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_TYPE
		{
			set{ _gun_type=value;}
			get{return _gun_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_STATUS
		{
			set{ _gun_status=value;}
			get{return _gun_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_BULLET_LOCATION
		{
			set{ _gun_bullet_location=value;}
			get{return _gun_bullet_location;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOSS_DESCRIPTION
		{
			set{ _loss_description=value;}
			get{return _loss_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REMARK
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? IN_TIME
		{
			set{ _in_time=value;}
			get{return _in_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OUT_TIME
		{
			set{ _out_time=value;}
			get{return _out_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SYN_FLAG
		{
			set{ _syn_flag=value;}
			get{return _syn_flag;}
		}
		#endregion Model

	}
}

