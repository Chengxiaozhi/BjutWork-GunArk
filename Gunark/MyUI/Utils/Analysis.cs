using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using Bll = Gunark.BLL;

namespace MyUI.Utils
{
    public class Analysis
    {
        public static void analysis(WebService.synInfo synInfo)
        {
            WebService.gunServices webService = SingleWebService.getWebService();
            #region model
            Model.gunark gunark = new Gunark.Model.gunark();
            Model.user user = new Gunark.Model.user();
            Model.group group = new Gunark.Model.group();
            Model.gbg gbg = new Gunark.Model.gbg();
            Model.fingerprint fingerPrint = new Gunark.Model.fingerprint();
            #endregion model
            #region bll
            Bll.user user_bll = new Gunark.BLL.user();
            Bll.gunark gunark_bll = new Gunark.BLL.gunark();
            Bll.group group_bll = new Gunark.BLL.group();
            Bll.gbg gbg_bll = new Gunark.BLL.gbg();
            Bll.fingerprint fingerPrint_bll = new Gunark.BLL.fingerprint();
            #endregion bll
            #region attritubes
            string[] attritubes = null;
            #endregion attritubes
            #region 同步类型
            switch (synInfo.syn_Type)
            {
                #region 枪柜
                case "枪柜信息增加"://通过
                    gunark = analysisGunark(synInfo.syn_Param);
                    gunark_bll.Add(gunark);
                    //获取枪弹绑定关系
                    attritubes = synInfo.syn_Param.Split('|');
                    WebService.gunarkGroupGunBullet[] gggb = webService.getGunarkGroupGunBullet(attritubes[0]);
                    for (int i = 0; i < gggb.Length; i++)
                    {
                        Model.gbg ggbg = new Gunark.Model.gbg();
                        gbg.GGGBID = gggb[i].gggbId;
                        gbg.GROUP_ID = gggb[i].groupId;
                        gbg.GUN_LOCATION = gggb[i].gunLocation;
                        gbg.GUNARK_ID = gggb[i].gunarkId;
                        gbg.BULLET_LOCATION = int.Parse(gggb[i].bulletLocation);
                        gbg_bll.Add(gbg);
                    }
                    break;
                case "枪柜信息修改"://未通过
                    gunark = analysisGunark(synInfo.syn_Param);
                    gunark_bll.Update(gunark);
                    break;
                case "注销"://未通过
                    attritubes = synInfo.syn_Param.Split('|');
                    gunark = gunark_bll.GetModel(attritubes[0]);
                    gunark.GUNARK_STATUS = 0;
                    gunark_bll.Update(gunark);
                    break;
                case "是否枪弹对应"://未通过
                    break;
                case "是否使用组权限"://未通过
                    break;
                #endregion 枪柜
                #region 用户（用户绑定组未通过【寄存组有问题】、新增枪柜增加用户）
                case "增加用户":
                    user = analysisUser(synInfo.syn_Param);
                    user_bll.Add(user);
                    break;
                case "更新用户":
                    user = analysisUser(synInfo.syn_Param);
                    user_bll.Update(user);
                    break;
                case "枪柜增加用户":
                    analysisUser1(synInfo.syn_Param);
                    break;
                #endregion 用户
                #region 组信息（组增加已通过、新增枪柜增加组）
                case "增加班组":
                    group = analysisGroup(synInfo.syn_Param);
                    group_bll.Add(group);
                    break;
                case "组注销":
                    attritubes = synInfo.syn_Param.Split('|');
                    group = group_bll.GetModel(attritubes[0]);
                    group_bll.Update(group);
                    break;
                case "枪柜增加组":
                    analysisGroup1(synInfo.syn_Param);
                    break;
                #endregion 组信息
                #region 枪弹组绑定关系（未测试）
                case "枪柜对应修改":
                    attritubes = synInfo.syn_Param.Split('|');
                    gbg = gbg_bll.GetModel(attritubes[0]);
                    gbg.GUN_LOCATION = int.Parse(attritubes[2]);
                    gbg.BULLET_LOCATION = int.Parse(attritubes[3]);
                    gbg_bll.Update(gbg);
                    break;
                case "枪位所属组修改":
                    attritubes = synInfo.syn_Param.Split('|');
                    gbg = gbg_bll.GetModelByGunPos(attritubes[0], attritubes[1]);
                    gbg.GROUP_ID = attritubes[2];
                    gbg_bll.Update(gbg);
                    break;
                #endregion 枪弹组绑定关系
                #region 指纹信息（未测试）
                case "指纹增加":
                    fingerPrint = analysisFingerPrint(synInfo.syn_Param);
                    fingerPrint_bll.Add(fingerPrint);
                    break;
                case "指纹修改":
                    //通过USER_ID查
                    fingerPrint = fingerPrint_bll.GetModelByUser(attritubes[1], attritubes[4]);
                    fingerPrint.USER_FINGERPRINT = StrToByte(attritubes[6]);
                    fingerPrint.USER_BAN = int.Parse(attritubes[7]);
                    fingerPrint_bll.Update(fingerPrint);
                    break;
                #endregion 指纹信息
                #region 增加任务(测试通过)
                case "增加任务":
                    addTask(synInfo.syn_Param);
                    break;
                #endregion 增加任务
                default:
                    break;
            }
            #endregion 同步类型
        }
        #region 解析枪柜串
        private static Model.gunark analysisGunark(string prams)
        {
            string[] attributes = prams.Split('|');
            Model.gunark gunark = new Gunark.Model.gunark();
            gunark.GUNARK_ID = attributes[0];
            gunark.GUNARK_NUMBER = attributes[1];
            gunark.GUNARK_BIGTYPE = attributes[2];
            gunark.GUNARK_IP = attributes[3];
            gunark.GUNARK_SUBNET = attributes[4];
            gunark.GUNARK_GATEWAY = attributes[5];
            gunark.GUNARK_TYPE = attributes[6];
            gunark.GUNARK_NAME = attributes[8];
            gunark.GUNARK_SIZE = attributes[9];
            gunark.GUNARK_NUMOFGUN = attributes[9];
            gunark.GUNARK_NUMOFBULLET = attributes[10];
            gunark.GUNARK_NUMOFBULLETWAREHOUSE = attributes[11];
            gunark.GUNARK_PORT = attributes[12];
            gunark.GUNARK_LOCATION = attributes[13];
            gunark.GUNARK_PICURL = attributes[14];
            gunark.GUNARK_REMARK = attributes[15];
            gunark.GUNARK_VERIFYSTATUS = attributes[16];
            gunark.GUNARK_ISSEALUP = int.Parse(attributes[17]);
            gunark.GUNARK_ISONLINE = int.Parse(attributes[18]);
            gunark.GUNARK_ISPOWERON = int.Parse(attributes[19]);
            gunark.GUNARK_ISOPEN = int.Parse(attributes[20]);
            gunark.GUNARK_ISWARNING = int.Parse(attributes[21]);
            gunark.GUNARK_ENTERTIME = attributes[22];
            gunark.UNITINFO_CODE = attributes[23];
            gunark.GUNARK_STATUS = int.Parse(attributes[24]);
            gunark.GUNARK_CAMERAIP = attributes[25];
            gunark.GUNARK_VERSION = attributes[26];
            gunark.GUNARK_ALCOHOLIS = attributes[27];
            gunark.GUNARK_CODEIS = attributes[28];
            gunark.GUNARK_GUNARKIS = attributes[29];
            gunark.GUNARK_GGGBUNIQUEIS = attributes[30];
            gunark.GUNARK_ISCHECKING = int.Parse(attributes[31]);
            gunark.GUNARK_ISOFFLINE = int.Parse(attributes[32]);
            gunark.GUNARK_ISCARDREADING = int.Parse(attributes[33]);
            gunark.GUNARK_EMERGENCYNUMBER = int.Parse(attributes[34]);
            return gunark;
        }
        #endregion 解析枪柜串
        #region 解析用户串
        private static Model.user analysisUser(string prams)
        {
            string[] attributes = prams.Split('|');
            Model.user user = new Gunark.Model.user();
            user.USER_POLICENUMB = attributes[0];
            user.USER_REALNAME = attributes[1];
            user.USER_NAME = attributes[2];
            if (!"1".Equals(attributes[3]))
                user.USER_PWD = attributes[3];
            else
                user.USER_PWD = new Bll.user().GetModel(attributes[22]).USER_PWD;
            user.USER_STATE = int.Parse(attributes[4]);
            user.USER_ADDRESS = attributes[5];
            user.USER_OFFICETELEP = attributes[6];
            user.USER_MOBILTELEP = attributes[7];
            user.USER_SEX = int.Parse(attributes[8]);
            user.USER_EMAIL = attributes[9];
            user.USER_POSTCODE = attributes[10];
            user.USER_PRIVIEGES = int.Parse(attributes[11]);
            user.USER_GUNLICENSE = attributes[12];
            user.USER_GUNLICENSEDATE = attributes[13];
            user.USER_JOBSTATUS = int.Parse(attributes[18]);
            user.UNITINFO_CODE = attributes[19];
            user.GROUP_ID = attributes[20];
            user.USER_BANNED = int.Parse(attributes[21]);
            user.USER_ID = attributes[22];
            return user;
        }
        private static void analysisUser1(string prams)
        {
            Bll.user user_bll = new Gunark.BLL.user();
            int j = -1;
            string[] attributes = prams.Split('|');
            int user_count = int.Parse(attributes[0].Substring(0, attributes[0].IndexOf("&")));
            for (int i = 0; i < user_count; i++)
            {
                Model.user user = new Gunark.Model.user();
                if (i == 0)
                    user.USER_POLICENUMB = attributes[++j].Substring(attributes[0].IndexOf("&") + 1);
                else
                    user.USER_POLICENUMB = attributes[++j];
                user.USER_REALNAME = attributes[++j];
                user.USER_NAME = attributes[++j];
                user.USER_PWD = attributes[++j];
                user.USER_STATE = int.Parse(attributes[++j]);
                user.USER_ADDRESS = attributes[++j];
                user.USER_OFFICETELEP = attributes[++j];
                user.USER_MOBILTELEP = attributes[++j];
                user.USER_SEX = int.Parse(attributes[++j]);
                user.USER_EMAIL = attributes[++j];
                user.USER_POSTCODE = attributes[++j];
                user.USER_PRIVIEGES = int.Parse(attributes[++j]);
                user.USER_GUNLICENSE = attributes[++j];
                user.USER_GUNLICENSEDATE = attributes[++j];
                j = j + 4;
                user.USER_JOBSTATUS = int.Parse(attributes[++j]);
                user.UNITINFO_CODE = attributes[++j];
                user.GROUP_ID = attributes[++j];
                user.USER_BANNED = int.Parse(attributes[++j]);
                user.USER_ID = attributes[++j];
                if (i != user_count - 1)
                    ++j;
                user_bll.Add(user);
            }
        }
        #endregion 解析用户串
        #region 解析组串
        private static Model.group analysisGroup(string prams)
        {
            string[] attributes = prams.Split('|');
            Model.group group = new Gunark.Model.group();
            group.GROUP_ID = attributes[0];
            group.GROUP_LEADER = attributes[1];
            group.AVAILABLE = int.Parse(attributes[2]);
            return group;
        }
        private static void analysisGroup1(string prams)
        {
            Bll.group g_bll = new Gunark.BLL.group();
            int j = -1;
            string[] attributes = prams.Split('|');
            int group_count = int.Parse(attributes[0].Substring(0,attributes[0].IndexOf("&")));
            for (int i = 0; i < group_count; i++)
            {
                Model.group group = new Gunark.Model.group();
                if(i == 0)
                    group.GROUP_ID = attributes[++j].Substring(attributes[0].IndexOf("&") + 1);
                else
                    group.GROUP_ID = attributes[++j];
                group.GROUP_LEADER = attributes[++j];
                group.AVAILABLE = int.Parse(attributes[++j]);
                ++j;
                if (i != group_count - 1)
                    ++j;
                g_bll.Add(group);
            }
        }
        #endregion 解析组串
        #region 解析指纹串
        private static Model.fingerprint analysisFingerPrint(string prams)
        {
            string[] attributes = prams.Split('|');
            Model.fingerprint fingerPrint = new Gunark.Model.fingerprint();
            fingerPrint.USER_FINGERPRINT_ID = attributes[0];
            fingerPrint.USER_POLICENUMB = attributes[1];
            fingerPrint.USER_NAME = attributes[2];
            fingerPrint.USER_PWD = attributes[3];
            fingerPrint.FINGER_NUMBER = attributes[4];
            fingerPrint.UNIT_ID = attributes[5];
            fingerPrint.USER_FINGERPRINT = StrToByte(attributes[6]);
            return fingerPrint;
        }
        #endregion 解析指纹串
        #region 解析任务串
        private static void addTask(string prams)
        {
            Bll.task_info task_info_bll = new Gunark.BLL.task_info();
            Bll.task_info_detail task_detail_info_bll = new Gunark.BLL.task_info_detail();
            Bll.position_info position_info_bll = new Gunark.BLL.position_info();
            Bll.gun_info gun_info_bll = new Gunark.BLL.gun_info();
            Bll.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
            int j = 10;

            string[] attributes = prams.Split('|');
            //任务信息
            Model.task_info task_info = new Gunark.Model.task_info();
            task_info.task_ID = attributes[0];
            task_info.GUNARK_ID = attributes[1];
            task_info.UNIT_ID = attributes[2];
            task_info.TASK_STATUS = attributes[3];
            task_info.TASK_BIGTYPE = int.Parse(attributes[4]);
            task_info.TASK_SMALLTYPE = attributes[5];
            task_info.TASK_PROPERTY = attributes[6];
            task_info.TASK_PLAN_BEGINTIME = attributes[7];
            task_info.TASK_PLAN_FINISHTIME = attributes[8];
            task_info.TASK_APPLY_USERID = attributes[9];
            task_info.TASK_APPLY_TIME = attributes[10];
            task_info_bll.Add(task_info);
            //任务详情信息
            int task_detail_info_count = int.Parse(attributes[11].Substring(1, attributes[11].LastIndexOf("&") - 1));
            for (int i = 0; i < task_detail_info_count; i++)
            {
                Model.task_info_detail task_detail_info = new Gunark.Model.task_info_detail();
                if(i == 0)
                    task_detail_info.TASK_DETAIL_ID = attributes[++j].Substring(attributes[j].LastIndexOf("&") + 1);
                else
                    task_detail_info.TASK_DETAIL_ID = attributes[++j];
                task_detail_info.TASK_ID = attributes[++j];
                task_detail_info.GUNARK_ID = attributes[++j];
                task_detail_info.UNIT_ID = attributes[++j];
                task_detail_info.GUN_INFO_ID = attributes[++j];
                task_detail_info.GUN_POSITION_INFO_ID = attributes[++j];
                task_detail_info.GUN_DUTY_USER = attributes[++j];
                task_detail_info.BULLET_TYPE = attributes[++j];
                task_detail_info.MAGAZINE_INFO_ID = attributes[++j];
                task_detail_info.APPLY_BULLET_QTY = int.Parse(attributes[++j]);
                task_detail_info.DEPLETION_BULLET_QTY = int.Parse(attributes[++j]);
                if (i != task_detail_info_count - 1)
                    ++j;
                task_detail_info_bll.Add(task_detail_info);
            }
            //枪位信息
            int gun_position_info_count = 0;
            try
            {
                 gun_position_info_count = int.Parse(attributes[j + 1].Substring(1, attributes[j + 1].LastIndexOf("&") - 1));
            }
            catch { }
            if (gun_position_info_count != 0)
            {
                for (int i = 0; i < gun_position_info_count; i++)
                {
                    Model.position_info gun_position_info = new Gunark.Model.position_info();
                    if (i == 0)
                        gun_position_info.GUN_POSITION_INFO_ID = attributes[++j].Substring(3);
                    else
                        gun_position_info.GUN_POSITION_INFO_ID = attributes[++j];
                    gun_position_info.GUNARK_ID = attributes[++j];
                    gun_position_info.UNIT_ID = attributes[++j];
                    gun_position_info.GUN_POSITION_NUMBER = attributes[++j];
                    gun_position_info.GUN_POSITION_STATUS = attributes[++j];
                    gun_position_info.GUN_INFO_ID = attributes[++j];
                    gun_position_info.GUN_BULLET_NUMBER = attributes[++j];
                    gun_position_info.GUN_TYPE = attributes[++j];
                    if (i != gun_position_info_count - 1)
                        ++j;
                    position_info_bll.Add(gun_position_info);
                }
                //枪支信息
                int gun_info_count = int.Parse(attributes[j + 1].Substring(1, attributes[j + 1].LastIndexOf("&") - 1));
                for (int i = 0; i < gun_info_count; i++)
                {
                    Model.gun_info gun_info = new Gunark.Model.gun_info();
                    if (i == 0)
                        gun_info.GUN_INFO_ID = attributes[++j].Substring(3);
                    else
                        gun_info.GUN_INFO_ID = attributes[++j];
                    gun_info.GUNARK_ID = attributes[++j];
                    gun_info.UNIT_ID = attributes[++j];
                    gun_info.GUN_NUMBER = attributes[++j];
                    gun_info.GUN_TYPE = attributes[++j];
                    gun_info.GUN_STATUS = attributes[++j];
                    gun_info.GUN_BULLET_LOCATION = attributes[++j];
                    if (i != gun_info_count - 1)
                        ++j;
                    gun_info_bll.Add(gun_info);
                }
                //弹仓信息
                int magazine_info_count = int.Parse(attributes[j + 1].Substring(1, attributes[j + 1].LastIndexOf("&") - 1));
                for (int i = 0; i < magazine_info_count; i++)
                {
                    Model.magazine_info magazine_info = new Gunark.Model.magazine_info();
                    if (i == 0)
                        magazine_info.MAGAZINE_INFO_ID = attributes[++j].Substring(3);
                    else
                        magazine_info.MAGAZINE_INFO_ID = attributes[++j];
                    magazine_info.GUNARK_ID = attributes[++j];
                    magazine_info.UNIT_ID = attributes[++j];
                    magazine_info.MAGAZINE_NUMBER = attributes[++j];
                    magazine_info.STOCK_QTY = int.Parse(attributes[++j]);
                    magazine_info.CAPACITY_QTY = int.Parse(attributes[++j]);
                    magazine_info.MAGAZINE_NAME = attributes[++j];
                    magazine_info.BULLET_MODEL = attributes[++j];
                    magazine_info.BULLET_GROUP_ID = attributes[++j];
                    ////if (i != magazine_info_count - 1)
                    ////    ++j;
                    magazine_info_bll.Add(magazine_info);
                }
            }
            else
            {
                int magazine_info_count = 0;
                //弹仓信息
                try
                {
                    magazine_info_count = int.Parse(attributes[j + 1].Substring(7, attributes[j + 1].LastIndexOf("&") - 1));
                }
                catch { }
                for (int i = 0; i < magazine_info_count; i++)
                {
                    Model.magazine_info magazine_info = new Gunark.Model.magazine_info();
                    if (i == 0)
                        magazine_info.MAGAZINE_INFO_ID = attributes[++j].Substring(9);
                    else
                        magazine_info.MAGAZINE_INFO_ID = attributes[++j];
                    magazine_info.GUNARK_ID = attributes[++j];
                    magazine_info.UNIT_ID = attributes[++j];
                    magazine_info.MAGAZINE_NUMBER = attributes[++j];
                    magazine_info.STOCK_QTY = int.Parse(attributes[++j]);
                    magazine_info.CAPACITY_QTY = int.Parse(attributes[++j]);
                    magazine_info.MAGAZINE_NAME = attributes[++j];
                    magazine_info.BULLET_MODEL = attributes[++j];
                    magazine_info.BULLET_GROUP_ID = attributes[++j];
                    ////if (i != magazine_info_count - 1)
                    ////    ++j;
                    magazine_info_bll.Add(magazine_info);
                }
            }
        }
        #endregion 解析任务串
        #region 字符串转byte数组
        public static byte[] StrToByte(string finger)
        {
            byte[] fingerByte = new byte[1404];
            string[] s = finger.Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                //if (!String.IsNullOrEmpty(s[i]))
                fingerByte[i] = Convert.ToByte(s[i], 16);
            }
            return fingerByte;
        }
        #endregion 字符串转byte数组
    }
}
