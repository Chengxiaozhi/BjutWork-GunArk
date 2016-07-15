using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using Bll = Gunark.BLL;

namespace MyUI.Utils
{
    public class CallWebService
    {
        public static void call()
        {
            //调用WebServie接口工具
            WebService.gunServices webService = SingleWebService.getWebService();
            Bll.task_info_detail task_info_detail_bll = new Gunark.BLL.task_info_detail();
            Bll.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
            List<Model.task_info_detail> task_info_detail_list = new List<Gunark.Model.task_info_detail>();
            task_info_detail_list = task_info_detail_bll.GetModelList("FLAG_TYPE = 0");
            foreach (Model.task_info_detail item in task_info_detail_list)
            {
                if ("get".Equals(item.BULLET_ID))
                {
                    if (item.GUN_INFO_ID != null)
                    {
                        //修改枪支、枪位信息
                        webService.setgunInUse(item.GUN_INFO_ID);
                        webService.setGunNotOnPosition(item.UNIT_ID, item.GUNARK_ID, item.GUN_POSITION_INFO_ID);
                    }
                    else
                        //修改子弹信息
                        webService.setMagazineStock(item.MAGAZINE_INFO_ID, (int)magazine_info_bll.GetModel(item.MAGAZINE_INFO_ID).STOCK_QTY);
                }
                else if ("return".Equals(item.BULLET_ID))
                {
                    if (item.GUN_INFO_ID != null)
                    {
                        //修改枪支、枪位信息
                        webService.setGunInStore(item.GUN_INFO_ID);
                        webService.setGunOnPosition(item.UNIT_ID, item.GUNARK_ID, item.GUN_POSITION_INFO_ID);
                    }
                    else
                        //修改子弹信息
                        webService.setMagazineStock(item.MAGAZINE_INFO_ID, (int)magazine_info_bll.GetModel(item.MAGAZINE_INFO_ID).STOCK_QTY);
                }
                item.FLAG_TYPE = 1;
                task_info_detail_bll.Update(item);
            }
        }
    }
}
