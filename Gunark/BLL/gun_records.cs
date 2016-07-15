using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Gunark.Model;
namespace Gunark.BLL
{
	/// <summary>
	/// gun_records
	/// </summary>
	public partial class gun_records
	{
		private readonly Gunark.DAL.gun_records dal=new Gunark.DAL.gun_records();
		public gun_records()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GUNARK_RECORD_ID)
		{
			return dal.Exists(GUNARK_RECORD_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.gun_records model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Gunark.Model.gun_records model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string GUNARK_RECORD_ID)
		{
			
			return dal.Delete(GUNARK_RECORD_ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GUNARK_RECORD_IDlist )
		{
			return dal.DeleteList(GUNARK_RECORD_IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Gunark.Model.gun_records GetModel(string GUNARK_RECORD_ID)
		{
			
			return dal.GetModel(GUNARK_RECORD_ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Gunark.Model.gun_records GetModelByCache(string GUNARK_RECORD_ID)
		{
			
			string CacheKey = "gun_recordsModel-" + GUNARK_RECORD_ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GUNARK_RECORD_ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Gunark.Model.gun_records)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Gunark.Model.gun_records> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Gunark.Model.gun_records> DataTableToList(DataTable dt)
		{
			List<Gunark.Model.gun_records> modelList = new List<Gunark.Model.gun_records>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Gunark.Model.gun_records model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

