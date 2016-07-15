using System;
namespace Gunark.Model
{
	/// <summary>
	/// task_info_detail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class task_info_detail
	{
		public task_info_detail()
		{}
		#region Model
		public string task_Detail_ID;
		public string _task_id;
		public string _unit_id;
		public string _gunark_id;
		public string _gun_info_id;
		public string _gun_position_info_id;
        public string bullet_Model { set; get; }
		public string _magazine_info_id;
        public int apply_Bullet_Qty { set; get; }
		public int? _depletion_bullet_qty;
		public DateTime? _take_gunbullet_time;
		public DateTime? _return_gunbullet_time;
		public string _take_gunbullet_user;
		public string _return_gunbullet_user;
		public int? _flag_type;
		public string _gun_type;
		public string _gun_duty_user;
		public string _bullet_id;
		/// <summary>
		/// 
		/// </summary>
		public string TASK_DETAIL_ID
		{
			set{ task_Detail_ID=value;}
			get{return task_Detail_ID;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TASK_ID
		{
			set{ _task_id=value;}
			get{return _task_id;}
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
		public string GUN_INFO_ID
		{
			set{ _gun_info_id=value;}
			get{return _gun_info_id;}
		}
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
		public string BULLET_TYPE
		{
			set{ bullet_Model=value;}
			get{return bullet_Model;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MAGAZINE_INFO_ID
		{
			set{ _magazine_info_id=value;}
			get{return _magazine_info_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int APPLY_BULLET_QTY
		{
			set{ apply_Bullet_Qty=value;}
			get{return apply_Bullet_Qty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DEPLETION_BULLET_QTY
		{
			set{ _depletion_bullet_qty=value;}
			get{return _depletion_bullet_qty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TAKE_GUNBULLET_TIME
		{
			set{ _take_gunbullet_time=value;}
			get{return _take_gunbullet_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RETURN_GUNBULLET_TIME
		{
			set{ _return_gunbullet_time=value;}
			get{return _return_gunbullet_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TAKE_GUNBULLET_USER
		{
			set{ _take_gunbullet_user=value;}
			get{return _take_gunbullet_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RETURN_GUNBULLET_USER
		{
			set{ _return_gunbullet_user=value;}
			get{return _return_gunbullet_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FLAG_TYPE
		{
			set{ _flag_type=value;}
			get{return _flag_type;}
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
		public string GUN_DUTY_USER
		{
			set{ _gun_duty_user=value;}
			get{return _gun_duty_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BULLET_ID
		{
			set{ _bullet_id=value;}
			get{return _bullet_id;}
		}
		#endregion Model

	}
}

