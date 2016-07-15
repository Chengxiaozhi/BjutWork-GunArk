using System;
namespace Gunark.Model
{
	/// <summary>
	/// gun_records:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class gun_records
	{
		public gun_records()
		{}
		#region Model
		private string _gunark_record_id;
		private string _record_bullet_type;
		private int? _record_gun_unm;
		private DateTime? _record_lend_time;
		private DateTime? _record_return_time;
		/// <summary>
		/// 
		/// </summary>
		public string GUNARK_RECORD_ID
		{
			set{ _gunark_record_id=value;}
			get{return _gunark_record_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RECORD_BULLET_TYPE
		{
			set{ _record_bullet_type=value;}
			get{return _record_bullet_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RECORD_GUN_UNM
		{
			set{ _record_gun_unm=value;}
			get{return _record_gun_unm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RECORD_LEND_TIME
		{
			set{ _record_lend_time=value;}
			get{return _record_lend_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RECORD_RETURN_TIME
		{
			set{ _record_return_time=value;}
			get{return _record_return_time;}
		}
		#endregion Model

	}
}

