using System;
using System.Collections.Generic;
using System.Text;
using BLL = Gunark.BLL;
using Model = Gunark.Model;
using System.Configuration;

namespace MyUI.Utils
{
    public class SynInfoFormService
    {
        /// <summary>
        /// 同步枪柜信息
        /// </summary>
        /// <param name="guanrk_ip"></param>
        public static void syn_gunark_info(string guanrk_ip)
        {
            BLL.gunark gunark_bll = new Gunark.BLL.gunark();
            WebService.gunServices webService = SingleWebService.getWebService();
            WebService.gunArk gunark_syn = webService.getGunarkInfo(guanrk_ip);
            
            if (gunark_syn != null)
            {
                Model.gunark gunark = new Gunark.Model.gunark();

                gunark.GUNARK_ID = gunark_syn.gunarkId;
                gunark.GUNARK_IP = gunark_syn.gunarkIp;
                gunark.GUNARK_ENTERTIME = gunark_syn.enterTime.ToString();
                gunark.GUNARK_GATEWAY = gunark_syn.gunarkGateway;
                gunark.GUNARK_SUBNET = gunark_syn.gunarkSubnet;
                gunark.GUNARK_TYPE = gunark_syn.gunarkType;
                gunark.GUNARK_NAME = gunark_syn.gunarkName;
                gunark.GUNARK_NUMOFGUN = gunark_syn.numOfGun;
                gunark.GUNARK_NUMOFBULLETWAREHOUSE = gunark_syn.numOfBulletWarehouse;
                gunark.UNITINFO_CODE = gunark_syn.unitInfo.unitInfoCode;
                gunark.GUNARK_STATUS = gunark_syn.gunarkStatus;

                if (!gunark_bll.Exists(gunark_syn.gunarkId))
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["gunark_id"].Value = gunark.GUNARK_ID;
                    cfa.AppSettings.Settings["unit_id"].Value = gunark.UNITINFO_CODE;
                    cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("appSettings");

                    gunark_bll.Add(gunark);
                    //使能弹柜
                    Communication comm = CommunicationInstance.getInstance().getCommunication();
                    comm.enableBullet();
                }
                else
                {
                    gunark_bll.Update(gunark);
                }
            }
        }
        /// <summary>
        /// 同步用户信息
        /// </summary>
        /// <returns>WebService.user[]</returns>
        public static WebService.user[] getUserInfo()
        {
            WebService.user[] userList = null;
            WebService.gunServices webService = SingleWebService.getWebService();
            try
            {
                userList = webService.getUser(ConfigurationManager.AppSettings["unit_id"]);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            return userList;
        }
        /// <summary>
        /// 同步任务信息
        /// </summary>
        public static void syn_Task_Info()
        {
            //同步枪柜信息(以后要改成通过guark_ip获取)
            //syn_gunark_info(ConfigurationManager.AppSettings["gunark_ip"]);
            syn_gunark_info("TEST");
            WebService.gunServices webService = SingleWebService.getWebService();
            BLL.task_info task_info_bll = new Gunark.BLL.task_info();

            WebService.taskInfo[] task_info_list = webService.getTaskInfo(ConfigurationManager.AppSettings["unit_id"], ConfigurationManager.AppSettings["gunark_id"]);

            for (int i = 0; i < task_info_list.Length; i++)
            {
                if (!task_info_bll.Exists(task_info_list[i].task_ID) && task_info_list[i].task_BigType != 1)
                {
                    Model.task_info task_info = new Gunark.Model.task_info();
                    //为对象设置属性
                    task_info.gunarkId = task_info_list[i].gunArk.gunarkId;
                    task_info.unitId = task_info_list[i].unitInfo.unitInfoCode;
                    task_info.TASK_APPLY_USERID = task_info_list[i].task_Apply_User.userPoliceNumb ;
                    task_info.task_Plan_BeginTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", task_info_list[i].task_Plan_BeginTime);
                    task_info.task_Plan_FinishTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", task_info_list[i].task_Plan_FinishTime);
                    task_info.TASK_STATUS = task_info_list[i].task_Status.ToString();
                    task_info.TASK_BIGTYPE = Convert.ToInt32(task_info_list[i].task_BigType.ToString());
                    ClassValueCopier.Copy(task_info, task_info_list[i]);
                   
                    task_info_bll.Add(task_info);

                    syn_Task_Details(task_info_list[i].task_ID,task_info_list[i].unitInfo.unitInfoCode,task_info_list[i].gunArk.gunarkId);
                }
            }
            
        }
        /// <summary>
        /// 同步任务详情
        /// </summary>
        public static void syn_Task_Details(string task_id,string unit_id,string gunark_id)
        {
            BLL.task_info_detail task_info_detail_bll = new Gunark.BLL.task_info_detail();
            WebService.gunServices webService = SingleWebService.getWebService();
            WebService.taskInfoDetail[] task_info_details_list = webService.getTaskDetail(task_id);
            //标识是否有枪的信息
            bool gunIsNull = true;
            //标识是否有弹的信息
            bool bulletIsNull = true;

            for (int i = 0; i < task_info_details_list.Length; i++)
            {
                if (!task_info_detail_bll.Exists(task_info_details_list[i].task_Detail_ID))
                {
                    Model.task_info_detail task_info_detail = new Gunark.Model.task_info_detail();
                    //为对象设置属性
                    task_info_detail.TASK_ID = task_id;
                    task_info_detail.UNIT_ID = task_info_details_list[i].unitInfo.unitInfoCode;
                    task_info_detail.GUNARK_ID = task_info_details_list[i].gunArk.gunarkId;
                    if (task_info_details_list[i].gunInfo != null)
                    {
                        task_info_detail.GUN_INFO_ID = task_info_details_list[i].gunInfo.gun_Info_Id;
                        gunIsNull = false;
                    }

                    if (task_info_details_list[i].gunPositionInfo != null)
                    {
                        task_info_detail.GUN_POSITION_INFO_ID = task_info_details_list[i].gunPositionInfo.gun_Position_Info_Id;
                    }

                    if (task_info_details_list[i].magazineInfo != null)
                    {
                        task_info_detail.MAGAZINE_INFO_ID = task_info_details_list[i].magazineInfo.magazine_Info_ID;
                        bulletIsNull = false;
                    }

                    if (task_info_details_list[i].gun_Duty_User != null)
                        task_info_detail.GUN_DUTY_USER = task_info_details_list[i].gun_Duty_User.userPoliceNumb;

                    task_info_detail.task_Detail_ID = task_info_details_list[i].task_Detail_ID;
                    task_info_detail.apply_Bullet_Qty = task_info_details_list[i].apply_Bullet_Qty;
                    task_info_detail.BULLET_TYPE = task_info_details_list[i].bullet_Model;
                    task_info_detail_bll.Add(task_info_detail);
                }
            }
            if (!gunIsNull)
            {   //同步枪支信息
                syn_Gun_Info(unit_id, gunark_id);
                //同步枪位信息
                syn_Gun_Position_Info(unit_id, gunark_id);
            }
            if (!bulletIsNull)
                syn_Magazine_Info(unit_id,gunark_id);
        }
        /// <summary>
        /// 同步枪支信息
        /// </summary>
        /// <param name="unit_id"></param>
        /// <param name="gunark_id"></param>
        public static void syn_Gun_Info(string unit_id, string gunark_id)
        {
            BLL.gun_info gun_info_bll = new Gunark.BLL.gun_info();
            WebService.gunServices webService = SingleWebService.getWebService();
            WebService.gunInfo[] gun_info_list = webService.getGunInfo(unit_id, gunark_id);

            for (int i = 0; i < gun_info_list.Length; i++)
            {
                if (!gun_info_bll.Exists(gun_info_list[i].gun_Info_Id))
                {
                    Model.gun_info gun_info = new Gunark.Model.gun_info();
                    gun_info.GUN_INFO_ID = gun_info_list[i].gun_Info_Id;
                    gun_info.GUNARK_ID = gun_info_list[i].gunArk.gunarkId;
                    gun_info.UNIT_ID = gun_info_list[i].unitInfo.unitInfoCode;
                    gun_info.GUN_NUMBER = gun_info_list[i].gun_Bullet_Number;
                    gun_info.GUN_TYPE = gun_info_list[i].gun_Bullet_Name;
                    gun_info.GUN_STATUS = gun_info_list[i].gun_Bullet_Status.ToString();
                    gun_info.GUN_BULLET_LOCATION = gun_info_list[i].gun_Bullet_Location;
                    gun_info.LOSS_DESCRIPTION = gun_info_list[i].loss_Description;
                    gun_info.REMARK = gun_info_list[i].remark;
                    gun_info.SYN_FLAG = gun_info_list[i].syn_Flag;

                    gun_info_bll.Add(gun_info);
                }
            }
        }
        /// <summary>
        /// 同步枪位信息
        /// </summary>
        /// <param name="unit_id"></param>
        /// <param name="gunark_id"></param>
        public static void syn_Gun_Position_Info(string unit_id, string gunark_id)
        {
            BLL.position_info gun_position_info_bll = new Gunark.BLL.position_info();
            WebService.gunServices webService = SingleWebService.getWebService();
            WebService.gunPositionInfo[] gun_position_info_list = webService.getGunPositionInfo(unit_id, gunark_id);

            for (int i = 0; i < gun_position_info_list.Length; i++)
            {
                if (!gun_position_info_bll.Exists(gun_position_info_list[i].gun_Position_Info_Id))
                {
                    Model.position_info gun_position_info = new Gunark.Model.position_info();
                    gun_position_info.GUN_POSITION_INFO_ID = gun_position_info_list[i].gun_Position_Info_Id;
                    gun_position_info.UNIT_ID = gun_position_info_list[i].unitInfo.unitInfoCode;
                    gun_position_info.GUNARK_ID = gun_position_info_list[i].gunArk.gunarkId;
                    gun_position_info.GUN_POSITION_NUMBER = gun_position_info_list[i].gun_Position_Number;
                    gun_position_info.GUN_POSITION_STATUS = gun_position_info_list[i].gun_Position_Status.ToString(); ;
                    gun_position_info.GUN_INFO_ID = gun_position_info_list[i].gun_Info_ID;
                    gun_position_info.GUN_BULLET_NUMBER = gun_position_info_list[i].gun_Bullet_Number;
                    gun_position_info.GUN_TYPE = gun_position_info_list[i].gun_Bullet_Name;

                    gun_position_info_bll.Add(gun_position_info);
                }
            }
        }
        /// <summary>
        /// 同步子弹信息
        /// </summary>
        /// <param name="unit_id"></param>
        /// <param name="gunark_id"></param>
        public static void syn_Magazine_Info(string unit_id, string gunark_id)
        {
            BLL.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
            WebService.gunServices webService = SingleWebService.getWebService();
            WebService.magazineInfo[] magazine_info_list = webService.getMagazineInfo(unit_id, gunark_id);

            for (int i = 0; i < magazine_info_list.Length; i++)
            {
                if (!magazine_info_bll.Exists(magazine_info_list[i].magazine_Info_ID))
                {
                    Model.magazine_info magazine_info = new Gunark.Model.magazine_info();
                    magazine_info.MAGAZINE_INFO_ID = magazine_info_list[i].magazine_Info_ID;
                    magazine_info.GUNARK_ID = magazine_info_list[i].gunArk.gunarkId;
                    magazine_info.UNIT_ID = magazine_info_list[i].unitInfo.unitInfoCode;
                    magazine_info.MAGAZINE_NUMBER = magazine_info_list[i].magazine_Number;
                    magazine_info.STOCK_QTY = magazine_info_list[i].stock_Qty;
                    magazine_info.MAGAZINE_NAME = magazine_info_list[i].magazine_Name;
                    magazine_info.CAPACITY_QTY = magazine_info_list[i].capacity_Qty;
                    magazine_info.BULLET_MODEL = magazine_info_list[i].bullet_Model;
                    magazine_info.MAGAZINE_STATUS = magazine_info_list[i].magazine_Status;
                    magazine_info.SYN_FLAG = magazine_info_list[i].syn_Flag;

                    magazine_info_bll.Add(magazine_info);
                }
            }
        }
    }
}
