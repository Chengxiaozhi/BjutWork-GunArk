using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Gunark.Model;
namespace Gunark.BLL
{
	/// <summary>
	/// user
	/// </summary>
	public partial class user
	{
		private readonly Gunark.DAL.user dal=new Gunark.DAL.user();
		public user()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string USER_ID)
        {
            return dal.Exists(USER_ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.user model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Gunark.Model.user model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string USER_ID)
        {

            return dal.Delete(USER_ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string USER_IDlist)
        {
            return dal.DeleteList(USER_IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Gunark.Model.user GetModel(string USER_ID)
        {

            return dal.GetModel(USER_ID);
        }

        /// <summary>
        /// 得到一个对象实体(根据姓名)
        /// </summary>
        public Gunark.Model.user GetModelByName(string USER_NAME)
        {

            return dal.GetModelByName(USER_NAME);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Gunark.Model.user GetModelByCache(string USER_ID)
        {

            string CacheKey = "userModel-" + USER_ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(USER_ID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Gunark.Model.user)objModel;
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
        public List<Gunark.Model.user> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Gunark.Model.user> DataTableToList(DataTable dt)
        {
            List<Gunark.Model.user> modelList = new List<Gunark.Model.user>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Gunark.Model.user model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

