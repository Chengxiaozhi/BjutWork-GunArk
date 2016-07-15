using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using BLL = Gunark.BLL;

namespace MyUI.Utils
{
    public class UpdateTaskInfo
    {
        /// <summary>
        /// 更改任务、枪柜、枪位、子弹数量状态
        /// </summary>
        /// <param name="task_info">任务对象</param>
        /// <param name="task_status">任务状态</param>
        /// <param name="gun_status">枪支状态</param>
        /// <param name="gun_pos_status">枪位状态</param>
        /// <param name="gun_info_id">枪支id集合</param>
        /// <param name="gun_pos_id">枪位id集合</param>
        /// <param name="magazine_id">子弹id集合</param>
        /// <param name="list_bullet">子弹任务详情集合</param>
        public static void update(Model.task_info task_info, string task_status, string gun_status, string gun_pos_status,string bullet_opreat, List<string> gun_info_id, List<string> gun_pos_id, List<string> magazine_id, List<Model.task_info_detail> list_bullet)
        {
            task_info.TASK_STATUS = task_status;
            new BLL.task_info().Update(task_info);
            //更新枪支状态“3”表示未置枪，“1”表示置枪
            for (int i = 0; i < gun_info_id.Count; i++)
            {
                Model.gun_info gun_info = new BLL.gun_info().GetModel(gun_info_id[i]);
                if (task_info.task_BigType == 2)
                {
                    gun_info.IN_TIME = DateTime.Now;
                }
                else if (task_info.task_BigType == 5 || task_info.task_BigType == 7 || task_info.task_BigType == 12)
                {
                    gun_info.OUT_TIME = DateTime.Now;
                }
                gun_info.GUN_STATUS = gun_status;
                new BLL.gun_info().Update(gun_info);
            }
            //更新枪柜状态，“2”表示未置枪，“3”表示置枪,"8"表示已封存，”7“表示已出库
            for (int i = 0; i < gun_pos_id.Count; i++)
            {
                Model.position_info gun_pos_info = new BLL.position_info().GetModel(gun_pos_id[i]);
                gun_pos_info.GUN_POSITION_STATUS = gun_pos_status;
                new BLL.position_info().Update(gun_pos_info);
            }
            //更新现存子弹数量，库存量-取弹数量
            if (bullet_opreat == "取弹")
            {
                for (int i = 0; i < magazine_id.Count; i++)
                {
                    Model.magazine_info mag_info = new BLL.magazine_info().GetModel(magazine_id[i]);
                    mag_info.STOCK_QTY = mag_info.STOCK_QTY - list_bullet[i].APPLY_BULLET_QTY;
                    new BLL.magazine_info().Update(mag_info);
                }
            }
            else if (bullet_opreat == "还弹")
            {
                for (int i = 0; i < magazine_id.Count; i++)
                {
                    Model.magazine_info mag_info = new BLL.magazine_info().GetModel(magazine_id[i]);
                    mag_info.STOCK_QTY = mag_info.STOCK_QTY + list_bullet[i].APPLY_BULLET_QTY;
                    new BLL.magazine_info().Update(mag_info);
                }
            }
            
        }
    }
}
