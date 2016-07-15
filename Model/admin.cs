using System;
namespace Gunark.Model
{
	/// <summary>
	/// admin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class admin
	{
		public admin()
		{}
		#region Model
		private string _id;
		private string _admin_kind;
		private string _admin_dutybegin;
		private string _admin_dutyend;
		private int? _admin_status;
		private string _userid;
		private string _admin_id;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADMIN_KIND
		{
			set{ _admin_kind=value;}
			get{return _admin_kind;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADMIN_DUTYBEGIN
		{
			set{ _admin_dutybegin=value;}
			get{return _admin_dutybegin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADMIN_DUTYEND
		{
			set{ _admin_dutyend=value;}
			get{return _admin_dutyend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ADMIN_STATUS
		{
			set{ _admin_status=value;}
			get{return _admin_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USERID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ADMIN_ID
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		#endregion Model

	}
}

