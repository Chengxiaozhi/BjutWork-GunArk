using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL = Gunark.BLL;
using Model = Gunark.Model;
using MyUI.Utils;
using System.Threading;
using System.Runtime.InteropServices;

namespace MyUI
{
    public partial class GunarkDoorStatus : Form
    {
        private Model.task_info task_info;
        private byte[] gun_number;
        private byte[] magazine_number;
        private bool _isClose = true;
        private bool _isContinue = true;
        private int flag = 0;
        Communication comm = CommunicationInstance.getInstance().getCommunication();
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        //WebService接口调用工具
        WebService.gunServices webService = SingleWebService.getWebService();
        public GunarkDoorStatus()
        {
            InitializeComponent();
        }
        public GunarkDoorStatus(Model.task_info _task_info, byte[] _gun_number, byte[] _magazine_number)
        {
            task_info = _task_info;
            gun_number = _gun_number;
            magazine_number = _magazine_number;
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }
        
        private void GunarkDoorStatus_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        public void openBackgroundWorker()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (flag == 1)
            {
                PlaySound.paly(Properties.Resources._13);
                switch (task_info.TASK_BIGTYPE)
                {
                    //入库
                    case 2:
                        PlaySound.paly(Properties.Resources._22);
                        //发使能枪位指令（使能枪位）
                        comm.enable(Port.Get_ComGun(), gun_number);
                        System.Threading.Thread.Sleep(1000);
                        ////发标记枪位指令（解锁枪位）
                        comm.gunarkXuantong(Port.Get_ComGun(), gun_number);
                        break;
                    //取枪还枪
                    case 3:
                        if (task_info.TASK_STATUS == "3")
                        {
                            PlaySound.paly(Properties.Resources._18);
                            //发取枪指令（解锁枪位）
                            comm.open_gun(Port.Get_ComGun(), gun_number);
                        }
                        else if (task_info.TASK_STATUS == "5" || task_info.TASK_STATUS == "7")
                        {
                            PlaySound.paly(Properties.Resources._20);
                            //发还枪指令（解锁枪位）
                            comm.return_gun(Port.Get_ComGun(), gun_number);
                        }
                        break;
                    //报废
                    case 5:
                        PlaySound.paly(Properties.Resources._18);
                        //发标记枪位指令（标记枪位）
                        comm.open_gun(Port.Get_ComGun(), gun_number);
                        //发取消枪位使能指令（取消枪位使能）
                        comm.cancel_EB(Port.Get_ComGun(), gun_number);
                        break;
                    //保养
                    case 6:
                        PlaySound.paly(Properties.Resources._18);
                        //发取枪指令（解锁枪位）
                        comm.open_gun(Port.Get_ComGun(), gun_number);
                        break;
                    //调拨
                    case 7:
                        PlaySound.paly(Properties.Resources._18);
                        //发标记枪位指令（标记枪位）
                        comm.open_gun(Port.Get_ComGun(), gun_number);
                        //发取消枪位使能指令（取消枪位使能）
                        comm.cancel_EB(Port.Get_ComGun(), gun_number);
                        break;
                    //紧急取枪
                    case 8:
                        if (task_info.TASK_STATUS == "3")
                        {
                            PlaySound.paly(Properties.Resources._18);
                            //发取枪指令（解锁枪位）
                            comm.open_gun(Port.Get_ComGun(), gun_number);
                        }
                        else if (task_info.TASK_STATUS == "5" || task_info.TASK_STATUS == "7")
                        {
                            PlaySound.paly(Properties.Resources._20);
                            //发还枪指令（解锁枪位）
                            comm.return_gun(Port.Get_ComGun(), gun_number);
                        }
                        break;
                    //枪弹检查
                    case 9:
                        if (task_info.TASK_STATUS == "3")
                        {
                            PlaySound.paly(Properties.Resources._18);
                            //发取枪指令（解锁枪位）
                            comm.open_gun(Port.Get_ComGun(), gun_number);
                        }
                        else if (task_info.TASK_STATUS == "5" || task_info.TASK_STATUS == "7")
                        {
                            PlaySound.paly(Properties.Resources._20);
                            //发还枪指令（解锁枪位）
                            comm.return_gun(Port.Get_ComGun(), gun_number);
                        }
                        break;
                    //快速申领
                    case 13:
                        if (task_info.TASK_STATUS == "3")
                        {
                            PlaySound.paly(Properties.Resources._18);
                            //发取枪指令（解锁枪位）
                            comm.open_gun(Port.Get_ComGun(), gun_number);
                        }
                        else if (task_info.TASK_STATUS == "5" || task_info.TASK_STATUS == "7")
                        {
                            PlaySound.paly(Properties.Resources._20);
                            //发还枪指令（解锁枪位）
                            comm.return_gun(Port.Get_ComGun(), gun_number);
                        }
                        break;
                    //点验（不开枪锁）
                    case 14:
                        PlaySound.paly(Properties.Resources._31);
                        break;
                    default:
                        break;
                }
                
                //上传柜门开门状态（webService接口）
                webService.uploadOpenMessage(task_info.UNIT_ID, task_info.GUNARK_ID, PubFlag.policeNum, PubFlag.dutyLeaderNum, DateTime.Now, true, 1);

                //判断是否已经关闭枪柜门，检测关闭枪柜门之后再开启弹柜门
                while (_isContinue)
                {
                    //查询枪柜门状态
                    bool gunDoorStatus = SearchDoorStatus.searchGunStatus();
                    System.Threading.Thread.Sleep(1000);
                    //////假设已经关闭枪柜门,测试用，以后要删除下边这行代码
                    ////doorStatus = false;
                    //if (!gunDoorStatus)
                    if (true)
                    {
                        PlaySound.paly(Properties.Resources._12);
                        this.pictureBox1.Image = Properties.Resources.gun_close;
                        this.pictureBox2.Image = Properties.Resources.closeDoor;
                        break;
                    }
                }
                //上传枪柜门状态为关门(webService接口)
                webService.closeDoor(task_info.UNIT_ID, task_info.GUNARK_ID);

            }
            else if (flag == 3)
            {
                PlaySound.paly(Properties.Resources._15);
                switch (task_info.TASK_BIGTYPE)
                {
                    //枪弹入柜
                    case 2:
                        ////弹开相应弹仓
                        //comm.open_Bullet(magazine_number);
                        PlaySound.paly(Properties.Resources._23);
                        break;
                    default:
                        //弹开相应弹仓
                        PlaySound.paly(Properties.Resources._19);
                        //comm.open_Bullet(magazine_number);
                        break;
                }
               
                //调用WebService接口，上传弹柜门状态【1表示枪柜门，2表示弹柜门？】
                webService.uploadOpenMessage(task_info.UNIT_ID, task_info.GUNARK_ID, PubFlag.policeNum, PubFlag.dutyLeaderNum, DateTime.Now, true, 1);

                ///判断弹柜门是否已经关闭
                while (_isContinue)
                {
                    //查询弹柜门状态
                    //bool bulletDoorStatus = SearchDoorStatus.searchBulletStatus();
                    System.Threading.Thread.Sleep(1000);

                    ////假设应经关闭
                    //if (!bulletDoorStatus)
                    if (true)
                    {
                        PlaySound.paly(Properties.Resources._14);
                        this.pictureBox1.Image = Properties.Resources.bullet_close;
                        this.pictureBox2.Image = Properties.Resources.closeDoor;
                        break;
                    }
                }

                //上传弹柜门状态为关门(webService接口)
                webService.closeDoor(task_info.UNIT_ID, task_info.GUNARK_ID);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _isClose = false;
            _isContinue = false;
            this.Close();
        }

        private void GunarkDoorStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _isClose;
        }
    }
}
