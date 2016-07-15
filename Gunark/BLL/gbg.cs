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
	public partial class gbg
	{
        private readonly Gunark.DAL.gbg dal = new Gunark.DAL.gbg();
        public gbg()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GGGBID)
        {
            return dal.Exists(GGGBID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Gunark.Model.gbg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Gunark.Model.gbg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string GGGBID)
        {

            return dal.Delete(GGGBID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GGGBIDlist)
        {
            return dal.DeleteList(GGGBIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Gunark.Model.gbg GetModel(string GGGBID)
        {

            return dal.GetModel(GGGBID);
        }
        /// <summary>
        /// 得到一个对象实体(根据枪柜id和枪位号)
        /// </summary>
        public Gunark.Model.gbg GetModelByGunPos(string gunarkId,string gunLocation)
        {

            return dal.GetModelByGunPos(gunarkId, gunLocation);
        }

        
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Gunark.Model.gbg GetModelByCache(string GGGBID)
        {

            string CacheKey = "gbgModel-" + GGGBID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GGGBID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Gunark.Model.gbg)objModel;
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
        public List<Gunark.Model.gbg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Gunark.Model.gbg> DataTableToList(DataTable dt)
        {
            List<Gunark.Model.gbg> modelList = new List<Gunark.Model.gbg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Gunark.Model.gbg model;
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

