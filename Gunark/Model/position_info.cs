using System;
namespace Gunark.Model
{
	/// <summary>
	/// position_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class position_info
	{
		public position_info()
		{}
		#region Model
		private string _gun_position_info_id;
		private string _unit_id;
		private string _gunark_id;
		private string _gun_position_number;
		private string _gun_position_status;
		private string _gun_info_id;
		private string _gun_bullet_number;
		private string _gun_type;
		/// <summary>
		/// 
		/// </summary>
		public string GUN_POSITION_INFO_ID
		{
			set{ _gun_position_info_id=value;}
			get{return _gun_position_info_id;}
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
		public string GUNARK_ID
		{
			set{ _gunark_id=value;}
			get{return _gunark_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_POSITION_NUMBER
		{
			set{ _gun_position_number=value;}
			get{return _gun_position_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_POSITION_STATUS
		{
			set{ _gun_position_status=value;}
			get{return _gun_position_status;}
		}
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
		public string GUN_BULLET_NUMBER
		{
			set{ _gun_bullet_number=value;}
			get{return _gun_bullet_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUN_TYPE
		{
			set{ _gun_type=value;}
			get{return _gun_type;}
		}
		#endregion Model

	}
}

