using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using BLL = Gunark.BLL;
using Model = Gunark.Model;
using System.Threading;
namespace MyUI.Utils
{
    public class ControlGunark
    {
        /// <summary>
        /// 控制柜子公共类
        /// </summary>
        /// <param name="task_info">任务对象</param>
        /// <param name="gun_number">枪位号集合</param>
        /// <param name="magazine_number">弹仓号集合</param>
        /// <returns></returns>
        public static bool control(Model.task_info task_info, byte[] gun_number, byte[] magazine_number)
        {
            Communication comm = CommunicationInstance.getInstance().getCommunication();
            
            //WebService接口调用工具
            WebService.gunServices webService = SingleWebService.getWebService();
            if (gun_number.Length > 0 || task_info.task_BigType == 14)
            {
                //标识发完开门指令后多少秒还未开门
                int count = 0;
                //开枪柜门(枪支封存解封不开柜门)
                if (task_info.task_BigType != 4 && task_info.task_BigType != 10)
                    comm.send_message(Port.Get_ComGun(), Communication.open);
                //枪支封存
                if (task_info.task_BigType == 4)
                {
                    PlaySound.paly(Properties.Resources._29);
                    return true;
                }
                //枪支解封
                if (task_info.task_BigType == 10)
                {
                    PlaySound.paly(Properties.Resources._30);
                    return true;
                }
                //语音提示
                PlaySound.paly(Properties.Resources._9);
                //如果柜门已经打开，再发解锁枪位指令
                while (PubFlag.gunark_control_isAile)
                {
                    //将此循环设置为1秒执行一次，判断25秒之后还未将门打开,此时柜门已重新锁上
                    
                    //查询枪柜门状态
                    bool gunDoorStatus = SearchDoorStatus.searchGunStatus();
                    System.Threading.Thread.Sleep(1000);
                    count++;
                    if (count > 20)
                        return false;
                    //判断枪柜门是否已经打开
                    if (gunDoorStatus)
                    {
                        GunarkDoorStatus gds = new GunarkDoorStatus(task_info, gun_number, magazine_number);
                        gds.pictureBox1.Image = Properties.Resources.gun_open;
                        gds.pictureBox2.Image = Properties.Resources.openDoor;
                        gds.Flag = 1;
                        //gds.openBackgroundWorker();
                        gds.ShowDialog();
                        break;
                    }
                }
                ////判断是否已经关闭枪柜门，检测关闭枪柜门之后再开启弹柜门
                //while (PubFlag.gunark_control_isAile)
                //{
                //    //查询枪柜门状态
                //    bool doorStatus = SearchDoorStatus.searchGunStatus();
                //    System.Threading.Thread.Sleep(1000);
                //    //////假设已经关闭枪柜门,测试用，以后要删除下边这行代码
                //    ////doorStatus = false;
                //    //if (!doorStatus)
                //    if (true)
                //    {
                //        GunarkDoorStatus gds = new GunarkDoorStatus(task_info, gun_number, magazine_number);
                //        gds.pictureBox1.Image = Properties.Resources.gun_close;
                //        gds.pictureBox2.Image = Properties.Resources.closeDoor;
                //        gds.Flag = 2;
                //        gds.openBackgroundWorker();
                //        gds.ShowDialog();
                //        break;
                //    }
                //}
                
            }
            //结束取枪后，判断是否有子弹，若有在进行下面对弹柜的操作
            if (magazine_number.Length > 0)
            {
                //开弹柜门
                //comm.send_message(Port.Get_ComBullet(), Communication.open);
                //语音提示
                PlaySound.paly(Properties.Resources._10);
                //如果弹柜门已打开，再发弹开弹仓指令
                while (PubFlag.gunark_control_isAile)
                {
                    //查询弹柜门状态
                    //bool bulletDoorStatus = SearchDoorStatus.searchBulletStatus();
                    System.Threading.Thread.Sleep(1000);
                    //判断弹柜门是否已经打开(假设已经打开)
                    //if (bulletDoorStatus)
                    if(true)
                    {
                        GunarkDoorStatus gds = new GunarkDoorStatus(task_info, gun_number, magazine_number);
                        gds.pictureBox1.Image = Properties.Resources.bullet_open;
                        gds.pictureBox2.Image = Properties.Resources.openDoor;
                        gds.Flag = 3;
                        //gds.openBackgroundWorker();
                        gds.ShowDialog();
                        break;
                    }
                }
                /////判断弹柜门是否已经关闭
                //while (PubFlag.gunark_control_isAile)
                //{
                //    //查询弹柜门状态
                //    //bool doorStatus = SearchDoorStatus.searchBulletStatus();
                //    System.Threading.Thread.Sleep(1000);

                //    ////假设应经关闭
                //    //if (!doorStatus)
                //    if(true)
                //    {
                //        GunarkDoorStatus gds = new GunarkDoorStatus(task_info, gun_number, magazine_number);
                //        gds.pictureBox1.Image = Properties.Resources.bullet_close;
                //        gds.pictureBox2.Image = Properties.Resources.closeDoor;
                //        gds.Flag = 4;
                //        gds.openBackgroundWorker();
                //        gds.ShowDialog();
                //        break;
                //    }
                //}
            }
            return true;
        }
    }
}
