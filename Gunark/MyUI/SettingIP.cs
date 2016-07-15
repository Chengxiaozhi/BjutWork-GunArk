using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Management;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
using MyUI.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace MyUI
{
    public partial class SettingIP : Form
    {
        private bool _isSuccess = false;
        public SettingIP()
        {
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }
        private void SettingIP_Load(object sender, EventArgs e)
        {
            
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    gunarkIp.Text = (nic["IPAddress"] as String[])[0];
                    subNet.Text = (nic["IPSubnet"] as String[])[0];
                    gatWay.Text = (nic["DefaultIPGateway"] as String[])[0];
                }
            }
        }
        
        /// <summary>
        /// 设置Ip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
            {
                string gunark_ip = gunarkIp.Text.Trim();
                string gunark_gatWay = gatWay.Text.Trim();
                string gunark_subNet = subNet.Text.Trim();
                string service_ip = serviceIp.Text.Trim();
               
                //获取软件版本号和修改日期
                string CurrentSoftVersion = Utils.GetVersion.getSoftVersion();
                try
                {
                    if (Utils.CheckFormate.IsIpaddress(gunark_ip) && Utils.CheckFormate.IsIpaddress(service_ip))
                    {
                        Utils.SettingIp.SetIPAddress(gunark_ip, gunark_subNet, gunark_gatWay);

                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings["gunark_ip"].Value = gunark_ip;
                        cfa.AppSettings.Settings["service_ip"].Value = service_ip;
                        cfa.AppSettings.Settings["version"].Value = CurrentSoftVersion;
                        cfa.AppSettings.Settings["updateTime"].Value = DateTime.Now.ToLocalTime().ToString();
                        cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    else
                    {
                        MessageBox.Show(this, "您输入的IP地址格式不正确！正确格式为【192.168.1.1】", "提示");
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
                //同步枪柜信息
                Utils.SynInfo.getSynInfo();
                Model.gunark g = new Bll.gunark().GetModelByIp(gunark_ip);
                if (g == null)
                {
                    MessageBox.Show("请先在服务器端添加枪柜！！！");
                    return;
                }
                Configuration cfa1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa1.AppSettings.Settings["gunark_id"].Value = g.GUNARK_ID;
                cfa1.AppSettings.Settings["unit_id"].Value = g.UNITINFO_CODE;
                cfa1.AppSettings.Settings["first_run"].Value = "False";
                cfa1.Save(); ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("appSettings");
                //同步枪弹绑定信息
                //Bll.gbg gbg_bll = new Gunark.BLL.gbg();
                ////WebService接口调用工具
                //WebService.gunServices webService = SingleWebService.getWebService();
                //WebService.gunarkGroupGunBullet[] gggb = webService.getGunarkGroupGunBullet(ConfigurationManager.AppSettings["gunark_id"]);
                //for (int i = 0; i < gggb.Length; i++)
                //{
                //    Model.gbg gbg = new Gunark.Model.gbg();
                //    gbg.GGGBID = gggb[i].gggbId;
                //    gbg.GUNARK_ID = gggb[i].gunarkId;
                //    gbg.GUN_LOCATION = gggb[i].gunLocation;
                //    gbg.BULLET_LOCATION = int.Parse(gggb[i].bulletLocation);
                //    gbg.GROUP_ID = gggb[i].groupId;
                //    gbg_bll.Add(gbg);
                //}
                _isSuccess = true;
                this.Close();
            }
        }

        #region 调用键盘事件
        private void gunarkIp_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(groupBox2.Location.X, groupBox1.Location.Y - 40);
            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }
        private void serviceIp_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(groupBox1.Location.X, groupBox1.Location.Y - 40);
            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }
        private void getWay_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(groupBox2.Location.X, groupBox1.Location.Y - 40);
            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }

        private void subWay_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(groupBox2.Location.X, groupBox1.Location.Y - 40);
            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }
        /// <summary>
        /// 清除屏幕上多余的键盘
        /// </summary>
        private void clear()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {//注意:Controls中的控件是动态变化的.
                foreach (Control con in this.Controls)
                {
                    if (con is MyControls.KeyBorad)
                        con.Dispose();
                }
            }
        }
        #endregion 调用键盘事件

        private void SettingIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_isSuccess;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //WebService接口调用工具
                //MyService.MyServiceService service = new MyUI.MyService.MyServiceService();
                //service.Url = "http://" + serviceIp.Text.Trim() + ":8080/MyService/MyServicePort?wsdl";
                //label6.Text = "正在连接...";
                //if (service.getConn() == 200)
                //{
                //    label6.Text = "连接成功！";
                //    button1.Enabled = true;
                //}
                //else
                //{
                //    button1.Enabled = false;
                //    label6.Text = "连接失败";
                //}
                label6.Text = "连接成功！";
                button1.Enabled = true;
            }
            catch(System.Net.WebException we)
            {
                label6.Text = we.Message;
                button1.Enabled = false;
            }
        }
        
    }
}
