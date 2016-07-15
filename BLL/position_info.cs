using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Gunark.Model;
namespace Gunark.BLL
{
	/// <summary>
	/// position_info
	/// </summary>
	public partial class position_info
	{
		private readonly Gunark.DAL.position_info dal=new Gunark.DAL.position_info();
		public position_info()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GUN_POSITION_INFO_ID)
		{
			return dal.Exists(GUN_POSITION_INFO_ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Gunark.Model.position_info model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Gunark.Model.position_info model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string GUN_POSITION_INFO_ID)
		{
			
			return dal.Delete(GUN_POSITION_INFO_ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GUN_POSITION_INFO_IDlist )
		{
			return dal.DeleteList(GUN_POSITION_INFO_IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Gunark.Model.position_info GetModel(string GUN_POSITION_INFO_ID)
		{
			return dal.GetModel(GUN_POSITION_INFO_ID);
		}
        /// <summary>
        /// 得到一个对象实体(根据枪支id)
        /// </summary>
        public Gunark.Model.position_info GetModelByGunInfo(string GUN_INFO_ID)
        {
            return dal.GetModelByGunInfo(GUN_INFO_ID);
        }

        /// <summary>
        /// 得到一个对象实体（根据枪位号）
        /// </summary>
        public Gunark.Model.position_info GetModelByGunPosNum(string GUN_POSITION_NUMBER)
        {
            return dal.GetModelByGunPosNum(GUN_POSITION_NUMBER);
        }

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Gunark.Model.position_info GetModelByCache(string GUN_POSITION_INFO_ID)
		{
			
			string CacheKey = "position_infoModel-" + GUN_POSITION_INFO_ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GUN_POSITION_INFO_ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Gunark.Model.position_info)objModel;
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
		public List<Gunark.Model.position_info> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Gunark.Model.position_info> DataTableToList(DataTable dt)
		{
			List<Gunark.Model.position_info> modelList = new List<Gunark.Model.position_info>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Gunark.Model.position_info model;
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

