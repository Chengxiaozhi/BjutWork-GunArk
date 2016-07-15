using System;
namespace Gunark.Model
{
	/// <summary>
	/// task_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class task_info
	{
		public task_info()
		{}
		#region Model
        public string task_ID { set; get; }
        public string gunarkId { set; get; }
        public string unitId { set; get; }
        public string task_Status { set; get; }
        public int? task_BigType { set; get; }
        public string task_SmallType { set; get; }
        public string taskProperty { set; get; }
        public string task_Plan_BeginTime { set; get; }
        public string task_Plan_FinishTime { set; get; }
        public string taskApplyUserId { set; get; }
        public string taskApplyTime { set; get; }
        public string taskApplyRemark { set; get; }
        public string taskCheckUserId { set; get; }
        public string taskCheckTime { set; get; }
        public string taskCheckRemark { set; get; }
        public string taskApprovalUserId { set; get; }
        public string taskApprovalTime { set; get; }
        public string taskApprovalRemark { set; get; }
        public string taskRealReginTime { set; get; }
        public string taskRealFinishTime { set; get; }
        public string taskAfterwardsHandle { set; get; }
        public string taskAfterwardsDescription { set; get; }
        public string taskAfterwardsTime { set; get; }
        public string taskFlagType { set; get; }
        public string takeGunBulletAdmin1 { set; get; }
        public string returnGunBulletAdmin1 { set; get; }
        public string takeGunBulletAdmin2 { set; get; }
        public string returnGunBulletAdmin2 { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_ID
        {
            set { task_ID = value; }
            get { return task_ID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GUNARK_ID
        {
            set { gunarkId = value; }
            get { return gunarkId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIT_ID
        {
            set { unitId = value; }
            get { return unitId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_STATUS
        {
            set { task_Status = value; }
            get { return task_Status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TASK_BIGTYPE
        {
            set { task_BigType = value; }
            get { return task_BigType; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_SMALLTYPE
        {
            set { task_SmallType = value; }
            get { return task_SmallType; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_PROPERTY
        {
            set { taskProperty = value; }
            get { return taskProperty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_PLAN_BEGINTIME
        {
            set { task_Plan_BeginTime = value; }
            get { return task_Plan_BeginTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_PLAN_FINISHTIME
        {
            set { task_Plan_FinishTime = value; }
            get { return task_Plan_FinishTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPLY_USERID
        {
            set { taskApplyUserId = value; }
            get { return taskApplyUserId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPLY_TIME
        {
            set { taskApplyTime = value; }
            get { return taskApplyTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPLY_REMARK
        {
            set { taskApplyRemark = value; }
            get { return taskApplyRemark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_CHECK_USERID
        {
            set { taskCheckUserId = value; }
            get { return taskCheckUserId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_CHECK_TIME
        {
            set { taskCheckTime = value; }
            get { return taskCheckTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_CHECK_REMARK
        {
            set { taskCheckRemark = value; }
            get { return taskCheckRemark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPROVAL_USERID
        {
            set { taskApprovalUserId = value; }
            get { return taskApprovalUserId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPROVAL_TIME
        {
            set { taskApprovalTime = value; }
            get { return taskApprovalTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_APPROVAL_REMARK
        {
            set { taskApprovalRemark = value; }
            get { return taskApprovalRemark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_REAL_BEGINTIME
        {
            set { taskRealReginTime = value; }
            get { return taskRealReginTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_REAL_FINISHTIME
        {
            set { taskRealFinishTime = value; }
            get { return taskRealFinishTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_AFTERWARDS_HANDLE
        {
            set { taskAfterwardsHandle = value; }
            get { return taskAfterwardsHandle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_AFTERWARDS_DESCRIPTION
        {
            set { taskAfterwardsDescription = value; }
            get { return taskAfterwardsDescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_AFTERWARDS_TIME
        {
            set { taskAfterwardsTime = value; }
            get { return taskAfterwardsTime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_FLAG_TYPE
        {
            set { taskFlagType = value; }
            get { return taskFlagType; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TAKE_GUNBULLET_ADMIN1
        {
            set { takeGunBulletAdmin1 = value; }
            get { return takeGunBulletAdmin1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RETURN_GUNBULLET_ADMIN1
        {
            set { returnGunBulletAdmin1 = value; }
            get { return returnGunBulletAdmin1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TAKE_GUNBULLET_ADMIN2
        {
            set { takeGunBulletAdmin2 = value; }
            get { return takeGunBulletAdmin2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RETURN_GUNBULLET_ADMIN2
        {
            set { returnGunBulletAdmin2 = value; }
            get { return returnGunBulletAdmin2; }
        }
		#endregion Model

	}
}

