using System;
namespace Gunark.Model
{
	/// <summary>
	/// alarm_records:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class alarm_records
	{
		public alarm_records()
		{}
		#region Model
		private int _alarm_record_id;
		private string _gunark_id;
		private string _unit_id;
		private string _alarm_type;
		private DateTime? _alarm_begin_time;
		private string _alarm_description;
		private DateTime? _alarm_handle_time;
		private string _alarm_handle_userid;
		private string _alarm_handle_type;
		private int? _alarm_atate;
		private int? _task_id;
		private DateTime? _alarm_finish_time;
		private int? _alarm_count;
		/// <summary>
		/// 
		/// </summary>
		public int ALARM_RECORD_ID
		{
			set{ _alarm_record_id=value;}
			get{return _alarm_record_id;}
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
		public string ALARM_TYPE
		{
			set{ _alarm_type=value;}
			get{return _alarm_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ALARM_BEGIN_TIME
		{
			set{ _alarm_begin_time=value;}
			get{return _alarm_begin_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ALARM_DESCRIPTION
		{
			set{ _alarm_description=value;}
			get{return _alarm_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ALARM_HANDLE_TIME
		{
			set{ _alarm_handle_time=value;}
			get{return _alarm_handle_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ALARM_HANDLE_USERID
		{
			set{ _alarm_handle_userid=value;}
			get{return _alarm_handle_userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ALARM_HANDLE_TYPE
		{
			set{ _alarm_handle_type=value;}
			get{return _alarm_handle_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ALARM_ATATE
		{
			set{ _alarm_atate=value;}
			get{return _alarm_atate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TASK_ID
		{
			set{ _task_id=value;}
			get{return _task_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ALARM_FINISH_TIME
		{
			set{ _alarm_finish_time=value;}
			get{return _alarm_finish_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ALARM_COUNT
		{
			set{ _alarm_count=value;}
			get{return _alarm_count;}
		}
		#endregion Model

	}
}

