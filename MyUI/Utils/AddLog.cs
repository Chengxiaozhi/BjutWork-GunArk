using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using BLL = Gunark.BLL;
namespace MyUI.Utils
{
    public class AddLog
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log_type">日志类型</param>
        /// <param name="gun_number">枪支数</param>
        /// <param name="bullet_number">弹药数</param>
        public static void add(int log_type,string log_discribe,string gun_number,string bullet_number)
        {
            BLL.user user_bll = new Gunark.BLL.user();
            Model.log log = new Gunark.Model.log();
            log.LOG_TIME = System.DateTime.Now.ToLongDateString();
            log.LOG_TYPE = log_type;
            log.LOG_DISCRIBE = log_discribe;
            //if (!"".Equals(PubFlag.bossLeaderNum))
            //    log.OPREAT_USER = user_bll.GetModel(PubFlag.policeNum).userRealName + "+" + user_bll.GetModel(PubFlag.dutyLeaderNum).userRealName + "+" + PubFlag.bossLeaderNum;
            //else
            //    log.OPREAT_USER = user_bll.GetModel(PubFlag.policeNum).userRealName + "+" + user_bll.GetModel(PubFlag.dutyLeaderNum).userRealName;
            log.GUN_NUMBER = gun_number;
            log.BULLET_NUMBER = bullet_number;
            new BLL.log().Add(log);
        }
    }
}
