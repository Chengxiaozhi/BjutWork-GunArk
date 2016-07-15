using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Net.NetworkInformation;
using MyUI.Utils;
using System.Configuration;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
namespace MyUI
{
    public partial class Main : Form
    {
        /// <summary>
        /// 控制指纹实例
        /// </summary>
        Printer p = PrinterInstance.getInstance().getPrinter();
        /// <summary>
        /// 控制枪弹柜实例
        /// </summary>
        Communication comm = CommunicationInstance.getInstance().getCommunication();
        /// <summary>
        /// 获取酒精检测实例
        /// </summary>
        Alcohol alcohol = AlcoholInstance.getAlcoholInstance();
        /// <summary>
        /// 用于检测与远程服务器联网状态
        /// </summary>
        Ping ping = new Ping();
        PingReply pingReply = null;
        /// <summary>
        /// 枪柜状态
        /// </summary>
        private char[] alarmCC = null;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //显示系统当前时间
            SYS_TIME.Start();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
            
            //跨进程调用控件
            Control.CheckForIllegalCrossThreadCalls = false;
            //开启panel双缓冲  
            panel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(panel1, true, null);
            //读取配置文件，判断程序是否是第一次启动
            bool isFirstRun = bool.Parse(ConfigurationManager.AppSettings["first_run"]);
            if (isFirstRun)
            {
                SettingIP st = new SettingIP();
                st.ShowDialog();
            }
            //检测与远程服务器以及枪柜的联网状态
            WebService_Status.RunWorkerAsync();
            //查询柜子状态
            gunark_status.Start();
            //打开枪串口
            comm.OpenPort_gun("COM"+Port.Get_ComGun());
            //打开弹串口
            comm.OpenPort_bullet("COM"+Port.Get_ComBullet());
            //打开指纹串口
            p.OpenPort();
            //打开酒精串口
            alcohol.OpenPort("COM"+Port.Get_ComDrink());
        }
        /// <summary>
        /// 领取枪弹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void get_gun_Click(object sender, EventArgs e)
        {
            //同步任务信息
            //Utils.SynInfoFormService.syn_Task_Info();
            gunark_status.Stop();
            PubFlag.task_type = "领取枪弹";
            Select_Task st = new Select_Task();
            st.ShowDialog();
            gunark_status.Start();
        }
        /// <summary>
        /// 归还枪弹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void return_gun_Click(object sender, EventArgs e)
        {
            //同步任务信息
            //Utils.SynInfoFormService.syn_Task_Info();
            gunark_status.Stop();
            PubFlag.task_type = "归还枪弹";
            Select_Task st = new Select_Task();
            st.ShowDialog();
            gunark_status.Start();
        }
        /// <summary>
        /// 其他任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void another_task_Click(object sender, EventArgs e)
        {
            //同步任务信息
            //Utils.SynInfoFormService.syn_Task_Info();
            gunark_status.Stop();
            PubFlag.task_type = "其他任务";
            Select_Task st = new Select_Task();
            st.ShowDialog();
            gunark_status.Start();
        }
        /// <summary>
        /// 枪弹柜管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gunark_management_Click(object sender, EventArgs e)
        {
            gunark_status.Stop();
            Verify v = new Verify();
            v.Flag = 4;
            v.ShowDialog();
            if (v.Result)
            {
                GunarkManagement gm = new GunarkManagement();
                gm.ShowDialog();
            }
            gunark_status.Start();
        }
        /// <summary>
        /// 快速申领
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quick_get_gun_Click(object sender, EventArgs e)
        {
            gunark_status.Stop();
            Verify v = new Verify();
            v.Flag = 2;
            v.ShowDialog();
            if (v.Result)
            {
                QueryApply qa = new QueryApply("quick");
                qa.ShowDialog();
            }
            gunark_status.Start();
        }
        /// <summary>
        /// 紧急取枪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emergency_Click(object sender, EventArgs e)
        {
            ////右下角升起提示框，提示有异常任务
            //PopForm.Instance().Show();
            gunark_status.Stop();
            Verify v = new Verify();
            v.Flag = 2;
            v.ShowDialog();
            if (v.Result)
            {
                QueryApply qa = new QueryApply("emergency");
                qa.ShowDialog();
            }
            gunark_status.Start();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
        #region 检测系统时间
        /// <summary>
        /// 检测系统时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SYS_TIME_Tick(object sender, EventArgs e)
        {
            SYS_TIME.Interval = 1000;
            TimeSpan ts = new TimeSpan(Environment.TickCount * TimeSpan.TicksPerMillisecond);
            string TempStr = string.Format("系统已经运行：{0:d2}天 {1:d2}时 {2:d2}分 {3:d2}秒", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            DateTime dt = DateTime.Now;
            string TempStr2 = DateTime.Now.ToString("【yyyy/MM/dd HH:mm:ss】");
            this.time.Text = TempStr2;
            if (DateTime.Now.ToString("HH:mm:ss").Equals(ConfigurationManager.AppSettings["reBoot_time"]) && bool.Parse(ConfigurationManager.AppSettings["set_reStart"]))
            {
                if (MessageBox.Show(this, "您确定要重启么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Utils.ExitWindows.Reboot();
                }
            }
        }
        #endregion 检测系统时间
        #region 查询柜子状态
        /// <summary>
        /// 查询柜子状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gunark_status_Tick(object sender, EventArgs e)
        {
            try
            {
                char[] data1 = { '\x02', '0', 'A', '0', '0', '2', '0', 'D', 'A', 'T', 'A', '6', '3', '\x03', '\x0D' };
                comm.send_message(Port.Get_ComGun(), data1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            alarmCC = comm.getAlarmCC();

            if (alarmCC[6] == '1' || alarmCC[5] == '1' || alarmCC[4] == '1' || alarmCC[3] == '1')
            {
                this.alert.ForeColor = Color.Red;
                this.alert.Text = "【正在报警】";
            }
            else
            {
                this.alert.ForeColor = Color.Black;
                this.alert.Text = "【安全】";
            }
                
            if (alarmCC[0] == '1')
                this.power.Text = "【市电工作】";
            else
                this.power.Text = "【电池工作】";

            string temperature = "【" + comm.getTEMP() + "°C】";
            this.degree.Text = temperature;
        }
        #endregion 查询柜子状态
        #region 检测远程服务器连接状态
        /// <summary>
        /// 检测远程服务器连接状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Web_Service_Status_DoWork(object sender, DoWorkEventArgs e)
        {
            //检测联网状态
            while (true)
            {
                pingReply = ping.Send(ConfigurationManager.AppSettings["service_ip"]);
                if (pingReply.Status == IPStatus.Success)
                {
                    PubFlag.online = true;
                    service_status.Image = Properties.Resources.connected;
                    service_status.Text = "【已联网】";
                    get_gun.Enabled = true;
                    //return_gun.Enabled = true;
                    another_task.Enabled = true;
                    //通知者，联网状态下调webservice接口
                    CallWebService.call();
                }
                else
                {
                    PubFlag.online = false;
                    service_status.Image = Properties.Resources.unconnected;
                    service_status.Text = "【未联网】";
                    get_gun.Enabled = false;
                    //return_gun.Enabled = false;
                    another_task.Enabled = false;
                }
                //每隔五秒检测一次网络
                System.Threading.Thread.Sleep(5000);
            }
        }
        #endregion 检测远程服务器连接状态

        private void button1_Click(object sender, EventArgs e)
        {
            SynInfo.getSynInfo();
        }
    }
}
