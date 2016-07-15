using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
using System.Collections;
using System.Configuration;
using MyUI.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace MyUI
{
    public partial class GunarkManagement : Form
    {
        WebService.gunServices webService = SingleWebService.getWebService();
        MyService.MyServiceService service = new MyUI.MyService.MyServiceService();
        public GunarkManagement()
        {
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }
        private void GunarkManagement_Load(object sender, EventArgs e)
        {
            settingConfig_layout();
        }
        #region 页面切换控制
        /// <summary>
        /// 主页面切换控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name)
	        {
                case "Maintain":
                    Maintain_Layout();
                    break;
                case "Query":
                    gunark_info_Layout();
                    break;
                case "FingerManagement":
                    input_finger_layout();
                    break;
		        default:
                    break;
	        }
        }
        /// <summary>
        /// 子页面切换控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedTab.Name)
            {
                case "setting_config":
                    settingConfig_layout();
                    break;
                case "setting_founction":
                    settingFouncction_layout();
                    break;
                case "setting_com":
                    settingCom_Layout();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Query页面切换控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (query.SelectedTab.Name)
            {
                case "opreat_info":
                    opreat_info_Layout();
                    break;
                case "data_syn":
                    user_syn_Layout();
                    break;
                case "task_log":
                    task_log_Layout();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 指纹管理页面切换控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl4.SelectedTab.Name)
            {
                case "download_finger":
                    download_finger_layout();
                    break;
                case "upload_finger":
                    upload_finger_layout();
                    break;
                case "delete_finger":
                    delete_finger_layout();
                    break;
                default:
                    break;
            }
        }
        #endregion 页面切换控制
        #region 参数配置子页面
            #region 枪弹柜参数配置
            /// <summary>
            /// 调用软键盘输入服务器IP
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void service_ip_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start("osk.exe");
                //clear();
                //TextBox t = (TextBox)sender;
                //MyControls.KeyBorad k = new MyControls.KeyBorad(t);
                //k.Location = new Point(t.Location.X + 200, t.Location.Y + 120);

                ////使控件可移动
                //ControlMoveResize c = new ControlMoveResize(k, this);
                //this.Controls.Add(k);
                ////置于顶层
                //k.BringToFront();
            }
            /// <summary>
            /// 调用软键盘输入枪柜IP
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void gunark_ip_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start("osk.exe");
                //clear();
                //TextBox t = (TextBox)sender;
                //MyControls.KeyBorad k = new MyControls.KeyBorad(t);
                //k.Location = new Point(t.Location.X + 200, t.Location.Y);

                ////使控件可移动
                //ControlMoveResize c = new ControlMoveResize(k, this);
                //this.Controls.Add(k);
                ////置于顶层
                //k.BringToFront();
            }
            /// <summary>
            /// 枪弹柜参数配置预览项目
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void settingConfig_layout()
            {
                ////服务器IP
                //this.service_ip.Text = ConfigurationManager.AppSettings["service_ip"];
                ////枪柜IP
                //this.gunarkIp.Text = ConfigurationManager.AppSettings["gunark_ip"];
            }
            /// <summary>
            /// 设置服务器IP
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void set_service_ip_Click(object sender, EventArgs e)
            {
                if (Utils.CheckFormate.IsIpaddress(service_ip.Text.Replace(" ","")))
                {
                    if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {
                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings["service_ip"].Value = service_ip.Text.Replace(" ","");
                        cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                }
                else
                {
                    MessageBox.Show(this, "您输入的IP地址格式不正确！正确格式为【192.168.1.1】", "提示");
                }
            }
            /// <summary>
            /// 设置枪柜IP
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void set_gunark_ip_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    string gunark_ip = gunarkIp.Text.Replace(" ","");

                    if (Utils.CheckFormate.IsIpaddress(gunark_ip))
                    {
                        Utils.SettingIp.SetIPAddress(gunark_ip, "", "");

                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings["gunark_ip"].Value = gunark_ip;
                        cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    else
                    {
                        MessageBox.Show(this, "您输入的IP地址格式不正确！正确格式为【192.168.1.1】", "提示");
                    }
                }

            }
            #endregion 枪弹柜参数配置
            #region 功能配置
            /// <summary>
            /// 功能配置
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void FounctionSetting_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["set_sound"].Value = Sound.Checked.ToString();
                    cfa.AppSettings.Settings["set_drink"].Value = Drink.Checked.ToString();
                    cfa.AppSettings.Settings["set_message"].Value = Message.Checked.ToString();
                    cfa.AppSettings.Settings["set_reStart"].Value = Resatrt.Checked.ToString();
                    cfa.AppSettings.Settings["set_onOff"].Value = OnOff.Checked.ToString();
                    cfa.AppSettings.Settings["set_getGunByCard"].Value = get_gun_byCard.Checked.ToString();
                    cfa.AppSettings.Settings["set_bltAutoCount"].Value = blt_auto_count.Checked.ToString();
                    cfa.AppSettings.Settings["set_gun_bullet"].Value = gun_bullet.Checked.ToString();
                    cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("appSettings");
                    MessageBox.Show(this, "修改成功！！！");
                }
            }

            private void settingFouncction_layout()
            {
                Sound.Checked = bool.Parse(ConfigurationManager.AppSettings["set_sound"]);
                Drink.Checked = bool.Parse(ConfigurationManager.AppSettings["set_drink"]);
                Message.Checked = bool.Parse(ConfigurationManager.AppSettings["set_message"]);
                Resatrt.Checked = bool.Parse(ConfigurationManager.AppSettings["set_reStart"]);
                OnOff.Checked = bool.Parse(ConfigurationManager.AppSettings["set_onOff"]);
                get_gun_byCard.Checked = bool.Parse(ConfigurationManager.AppSettings["set_getGunByCard"]);
                blt_auto_count.Checked = bool.Parse(ConfigurationManager.AppSettings["set_bltAutoCount"]);
            }
            #endregion 功能配置
            #region 端口配置
            /// <summary>
            /// 端口配置
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void settingCom_Layout()
            {
                com_gun.Text = ConfigurationManager.AppSettings["com_gun"];
                com_bullet.Text = ConfigurationManager.AppSettings["com_bullet"];
                com_finger.Text = ConfigurationManager.AppSettings["com_finger"];
                com_drink.Text = ConfigurationManager.AppSettings["com_drink"];
                com_message.Text = ConfigurationManager.AppSettings["com_message"];
            }

            private void ComSetting_Click(object sender, EventArgs e)
            {
                //判断选择的串口号是否有相同的
                List<string> list = new List<string>() { com_gun.Text, com_bullet.Text, com_finger.Text, com_drink.Text, com_message.Text };
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (list[i].Equals(list[j]) && i != j)
                        {
                            MessageBox.Show(this, "串口号不能重复！！！");
                            return;
                        }
                    }
                }
                if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["com_gun"].Value = com_gun.Text;
                    cfa.AppSettings.Settings["com_bullet"].Value = com_bullet.Text;
                    cfa.AppSettings.Settings["com_drink"].Value = com_drink.Text;
                    cfa.AppSettings.Settings["com_message"].Value = com_message.Text;
                    cfa.AppSettings.Settings["com_finger"].Value = com_finger.Text;
                    cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.RefreshSection("appSettings");
                    MessageBox.Show(this, "修改成功！！！");
                }
            }
            #endregion 端口配置
        #endregion 参数配置子页面
        #region 信息查询子页面
            #region 历史操作信息
        /// <summary>
        /// 历史操作信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opreat_info_Layout()
        {
            //禁止datagridview自动创建列和行
            this.dataGridView5.AutoGenerateColumns = false;
            queryByType.SelectedIndex = 0;
            List<Model.log> log_list = new Bll.log().GetModelList("LOG_TYPE = 1");
            dataGridView5.DataSource = log_list;
        }
        /// <summary>
        /// 按类型查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryByType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (queryByType.SelectedItem.ToString().Equals("全选"))
            {
                List<Model.log> log_list = new Bll.log().GetModelList("LOG_TYPE = 1");
                dataGridView5.DataSource = log_list;
            }
            else
            {
                List<Model.log> log_list = new Bll.log().GetModelList("LOG_TYPE = 1 and LOG_DISCRIBE = '" + queryByType.SelectedItem.ToString() + "'");
                dataGridView5.DataSource = log_list;
            }
        }
        /// <summary>
        /// 按日期查询日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void end_ValueChanged(object sender, EventArgs e)
        {
            string start_time = start.Text;
            string end_time = end.Text;
            if (queryByType.SelectedItem.ToString().Equals("全选"))
            {
                List<Model.log> log_list = new Bll.log().GetModelList("LOG_TYPE = 1 and LOG_TIME between '" + start_time + "' and '" + end_time + "'");
                dataGridView5.DataSource = log_list;
            }
            else
            {
                List<Model.log> log_list = new Bll.log().GetModelList("LOG_TYPE = 1 and LOG_TIME between '" + start_time + "' and '" + end_time + "' and LOG_DISCRIBE = '" + queryByType.SelectedItem.ToString() + "'");
                dataGridView5.DataSource = log_list;
            }
        }
        #endregion 历史操作信息
            #region 枪弹信息查询
        /// <summary>
        /// 按枪支编号查询枪支信息(调出软键盘)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_gun_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(t.Location.X, t.Location.Y + 120);
            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }
        /// <summary>
        /// 符合条件的枪支信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_gun_TextChanged(object sender, EventArgs e)
        {
            //禁止自动创建列
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.Columns[1].Visible = true;
            dataGridView4.Columns[2].Visible = true;
            dataGridView4.Columns[3].Visible = false;
            dataGridView4.Columns[4].Visible = false;
            dataGridView4.Columns[5].Visible = false;
            List<Model.gun_info> list_gun = new List<Gunark.Model.gun_info>();
            list_gun = new Bll.gun_info().GetModelList("GUN_NUMBER like '%" + query_gun.Text + "%'");
            dataGridView4.DataSource = list_gun;
        }
        /// <summary>
        /// 按弹仓号查询子弹信息（调出软键盘）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_bullet_Click(object sender, EventArgs e)
        {
            clear();
            TextBox t = (TextBox)sender;
            MyControls.KeyBorad k = new MyControls.KeyBorad(t);
            k.Location = new Point(t.Location.X, t.Location.Y + 120);

            //使控件可移动
            ControlMoveResize c = new ControlMoveResize(k, this);
            this.Controls.Add(k);
            //置于顶层
            k.BringToFront();
        }
        /// <summary>
        /// 符合条件的子弹信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void query_bullet_TextChanged(object sender, EventArgs e)
        {
            //禁止自动创建列
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.Columns[1].Visible = false;
            dataGridView4.Columns[2].Visible = false;
            dataGridView4.Columns[3].Visible = true;
            dataGridView4.Columns[4].Visible = true;
            dataGridView4.Columns[5].Visible = true;
            List<Model.magazine_info> list_bullet = new List<Gunark.Model.magazine_info>();
            list_bullet = new Bll.magazine_info().GetModelList("MAGAZINE_NUMBER like '%" + query_bullet.Text + "%'");
            dataGridView4.DataSource = list_bullet;
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
        /// <summary>
        /// 枪弹信息表格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.Value != null)
                    e.Value = new Bll.gunark().GetModel(e.Value.ToString()).GUNARK_NAME;
            }
            if (e.ColumnIndex == 4)
            {
                if (e.Value != null)
                {
                    if (object.Equals(e.Value, "1"))
                    {
                        e.Value = "64式子弹";
                    }
                    else if (object.Equals(e.Value, "2"))
                    {
                        e.Value = "51式子弹";
                    }
                    else if (object.Equals(e.Value, "3"))
                    {
                        e.Value = "51式空爆弹";
                    }
                    else if (object.Equals(e.Value, "4"))
                    {
                        e.Value = "97式动能弹";
                    }
                    else if (object.Equals(e.Value, "5"))
                    {
                        e.Value = "97式杀伤弹";
                    }
                }
            }
        }
        #endregion 枪弹信息查询
            #region 枪柜信息
        /// <summary>
        /// 枪弹柜信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gunark_info_Layout()
        {
            //查询枪弹柜实时情况
        }
        #endregion 枪柜信息
            #region 人员信息同步
            WebService.user[] user_list = null;
            /// <summary>
            /// 人员信息同步
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void user_syn_Layout()
            {
                //禁止datagridview自动创建列和行
                this.dataGridView3.AutoGenerateColumns = false;
                //从服务器获取用户信息
                if (PubFlag.online)
                {
                    user_list = Utils.SynInfoFormService.getUserInfo();
                    ArrayList al = new ArrayList();
                    Bll.user user_bll = new Gunark.BLL.user();
                    if (user_list != null)
                    {
                        for (int i = 0; i < user_list.Length; i++)
                        {
                            al.Add(user_list[i]);
                            if (user_bll.Exists(user_list[i].userPoliceNumb))
                                al.RemoveAt(al.Count - 1);
                        }
                        user_list = (WebService.user[])al.ToArray(typeof(WebService.user));
                        dataGridView3.DataSource = user_list;
                    }
                }
                else {
                    MessageBox.Show("同步用户信息失败，请检查与远程服务器联网状态！","提示");
                }
                
            }
            
            /// <summary>
            /// 人员信息表格式化
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.ColumnIndex == 3 && e.Value != null)
                {
                    if (e.Value.Equals(0))
                    {
                        e.Value = "男";
                    }
                    else
                    {
                        e.Value = "女";
                    }
                }
            }

            /// <summary>
            /// 全选、反选
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void checkBox1_CheckedChanged(object sender, EventArgs e)
            {
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    if (dataGridView3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView3.Rows.Count; i++)
                        {
                            dataGridView3.Rows[i].Cells[0].Value = true;
                        }
                    }
                }
                else
                {
                    if (dataGridView3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView3.Rows.Count; i++)
                        {
                            if ((bool)dataGridView3.Rows[i].Cells[0].EditedFormattedValue)
                            {
                                dataGridView3.Rows[i].Cells[0].Value = false;
                            }
                            else
                                dataGridView3.Rows[i].Cells[0].Value = true;
                        }
                    }
                }
            }
            /// <summary>
            /// 单击选中
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                int index = 0;
                //MessageBox.Show(e.RowIndex.ToString());
                if (e.RowIndex == -1)
                {
                    index = 0;
                }
                else
                {
                    index = e.RowIndex;
                }

                if (dataGridView3.Rows.Count > 0)
                {
                    DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView3.Rows[index].Cells[0];
                    if (check.Value == null || false.Equals(check.Value))
                        check.Value = true;
                    else
                        check.Value = false;
                }
            }
            /// <summary>
            /// 立即同步
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button1_Click(object sender, EventArgs e)
            {
                int count = 0;
                //获取选中的行数
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    if ((bool)dataGridView3.Rows[i].Cells[0].EditedFormattedValue)
                    {
                        count++;
                    }
                }
                if (count <= 0)
                {
                    MessageBox.Show("请选择人员！", "提示");
                    return;
                }
                if (MessageBox.Show(this, "您确定要同步么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    ArrayList al = new ArrayList();
                    Bll.user user_bll = new Gunark.BLL.user();
                    for (int i = 0; i < dataGridView3.RowCount; i++)
                    {
                        al.Add(user_list[i]);
                        if ((bool)dataGridView3.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            Model.user user = new Model.user();

                            user.UNITINFO_CODE = user_list[i].unitInfo.unitInfoCode;

                            Utils.ClassValueCopier.Copy(user, user_list[i]);

                            if (!user_bll.Exists(dataGridView3.Rows[i].Cells[1].Value.ToString()))
                                user_bll.Add(user);

                            al.RemoveAt(al.Count - 1);
                        }
                    }
                    user_list = (WebService.user[])al.ToArray(typeof(WebService.user));
                    dataGridView3.DataSource = user_list;
                }

            }
            #endregion 人员信息同步
            #region 任务日志
            Bll.task_info task_info_bll = new Gunark.BLL.task_info();
            /// <summary>
            /// 任务日志
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void task_log_Layout()
            {
                //禁止datagridview自动创建列和行
                this.dataGridView8.AutoGenerateColumns = false;
                this.task_status_combox.SelectedIndex = 0;
                List<Model.task_info> task_info_list = task_info_bll.GetModelList("");
                dataGridView8.DataSource = task_info_list;
            }
            private void task_status_combox_SelectedIndexChanged(object sender, EventArgs e)
            {
                
                if (task_status_combox.SelectedItem.ToString().Equals("全选"))
                {
                    List<Model.task_info> task_info_list = task_info_bll.GetModelList("");
                    dataGridView8.DataSource = task_info_list;
                }
                else if (task_status_combox.SelectedItem.ToString().Equals("未执行任务"))
                {
                    List<Model.task_info> task_info_list = task_info_bll.GetModelList("TASK_STATUS = 3");
                    dataGridView8.DataSource = task_info_list;
                }
                else if (task_status_combox.SelectedItem.ToString().Equals("执行中任务"))
                {
                    List<Model.task_info> task_info_list = task_info_bll.GetModelList("TASK_STATUS = 5");
                    dataGridView8.DataSource = task_info_list;
                }
                else
                {
                    List<Model.task_info> task_info_list = task_info_bll.GetModelList("TASK_STATUS =  6");
                    dataGridView8.DataSource = task_info_list;
                }
            }
            private void dataGridView8_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.ColumnIndex == 0)
                {
                    if (object.Equals(e.Value, 1))
                    {
                        e.Value = "申请弹仓";
                    }
                    else if (object.Equals(e.Value, 2))
                    {
                        e.Value = "枪弹入柜";
                    }
                    else if (object.Equals(e.Value, 3))
                    {
                        e.Value = "申请枪弹";
                    }
                    else if (object.Equals(e.Value, 4))
                    {
                        e.Value = "枪支封存";
                    }
                    else if (object.Equals(e.Value, 5))
                    {
                        e.Value = "枪支报废";
                    }
                    else if (object.Equals(e.Value, 6))
                    {
                        e.Value = "枪支保养";
                    }
                    else if (object.Equals(e.Value, 7))
                    {
                        e.Value = "枪支调拨";
                    }
                    else if (object.Equals(e.Value, 8))
                    {
                        e.Value = "紧急取枪弹";
                    }
                    else if (object.Equals(e.Value, 9))
                    {
                        e.Value = "枪弹检查";
                    }
                    else if (object.Equals(e.Value, 10))
                    {
                        e.Value = "枪支解封";
                    }
                    else if (object.Equals(e.Value, 11))
                    {
                        e.Value = "枪支寄存";
                    }
                    else if (object.Equals(e.Value, 12))
                    {
                        e.Value = "枪支解存";
                    }
                    else if (object.Equals(e.Value, 13))
                    {
                        e.Value = "快速取枪弹";
                    }
                    else if (object.Equals(e.Value, 14))
                    {
                        e.Value = "枪支点验";
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    if (object.Equals(e.Value, "1"))
                    {
                        e.Value = "申请";
                    }
                    else if (object.Equals(e.Value, "2"))
                    {
                        e.Value = "审核";
                    }
                    else if (object.Equals(e.Value, "3"))
                    {
                        e.Value = "审批完成";
                    }
                    else if (object.Equals(e.Value, "4"))
                    {
                        e.Value = "拒绝";
                    }
                    else if (object.Equals(e.Value, "5"))
                    {
                        e.Value = "进行中";
                    }
                    else if (object.Equals(e.Value, "6"))
                    {
                        e.Value = "完成";
                    }
                    else if (object.Equals(e.Value, "7"))
                    {
                        e.Value = "超期未还";
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    if(!"".Equals(e.Value.ToString()))
                        e.Value = user_bll.GetModel(e.Value.ToString()).USER_REALNAME;
                }
            }
            #endregion 任务日志
        #endregion 信息查询子页面
        #region 枪柜维护子页面
            #region 预加载项
            /// <summary>
            /// 加载项
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Maintain_Layout()
            {
                //获取自动重启时间
                ReBootTime.Text = ConfigurationManager.AppSettings["reBoot_time"];
                //获取软件版本号和修改日期
                CurrentSoftVersion.Text = Utils.GetVersion.getSoftVersion();
                ModifyTime.Text = ConfigurationManager.AppSettings["updateTime"];
                try
                {
                    NewSoftVersion.Text = service.getVersion();
                }
                catch (Exception e)
                {
                    MessageBox.Show("请检查联网情况");
                }
            }
            #endregion 预加载项
            #region 设置重启时间
            /// <summary>
            /// 设置重启时间
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ReBoot_Click(object sender, EventArgs e)
            {
                if (Utils.CheckFormate.Istime(ReBootTime.Text.Replace(" ","")))
                {
                    if (MessageBox.Show(this, "您确定要修改么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {
                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings["reBoot_time"].Value = ReBootTime.Text.Replace(" ","");
                        cfa.Save(); 
                        ConfigurationManager.RefreshSection("appSettings");
                        MessageBox.Show(this, "修改成功！！！");
                    }
                }
                else
                {
                    MessageBox.Show(this, "输入的时间格式不正确！正确格式为【hh:MM:ss】");
                }
            }
            /// <summary>
            /// 设置重启时间（调用软键盘）
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ReBootTime_Click(object sender, EventArgs e)
            {
                System.Diagnostics.Process.Start("osk.exe");
                //clear();
                //TextBox t = (TextBox)sender;
                //MyControls.KeyBorad k = new MyControls.KeyBorad(t);
                //k.Location = new Point(t.Location.X + 200, t.Location.Y + 100);

                ////使控件可移动
                //ControlMoveResize c = new ControlMoveResize(k, this);
                //this.Controls.Add(k);
                ////置于顶层
                //k.BringToFront();
            }
            /// <summary>
            /// 重启时间格式控制
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ReBootTime_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
            {
                MessageBox.Show(this, "您输入的时间格式不正确！！！", "提示");
            }
            #endregion 设置重启时间
            #region 系统关机
            /// <summary>
            /// 系统关机
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ShutDown_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show(this, "您确定要关机么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Utils.ExitWindows.Shutdown();
                }
            }
            #endregion 系统关机
            #region 软件升级
            /// <summary>
            /// 软件升级
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void SoftUpdate_Click(object sender, EventArgs e)
            {
                
                //判断是否需要升级
                if (CurrentSoftVersion.Text.CompareTo(NewSoftVersion.Text) == 0)
                {
                    MessageBox.Show("当前为最新版本！");
                }
                else if (CurrentSoftVersion.Text.CompareTo(NewSoftVersion.Text) == -1)
                {
                    if (MessageBox.Show(this, "您确定要更新么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                    {
                        if (Directory.Exists(@"C:\gunark\" + DateTime.Now.ToShortDateString().Replace("/", "")) == false)
                            Directory.CreateDirectory(@"C:\gunark\" + DateTime.Now.ToShortDateString().Replace("/", ""));
                        string[] fileName = service.getFileName();

                        for (int i = 0; i < fileName.Length; i++)
                        {
                            FileStream stream = new FileStream(@"C:\gunark\" + DateTime.Now.ToShortDateString().Replace("/", "") + "\\" + fileName[i] + "", FileMode.Create, FileAccess.Write, FileShare.None);
                            byte[] bs = service.downloadFile(fileName[i]);
                            stream.Write(bs, 0, bs.Length);
                            stream.Flush();
                            stream.Close();
                        }
                       
                        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        cfa.AppSettings.Settings["updateTime"].Value = DateTime.Now.ToLocalTime().ToString();
                        cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("appSettings");
                        MessageBox.Show("更新完成！！");

                        //更新完成后自启动新程序
                        Application.ExitThread();
                        Thread thtmp = new Thread(new ParameterizedThreadStart(run));
                        //object appName = Application.ExecutablePath;
                        object appName = @"C:\testFile\down\" + DateTime.Now.ToShortDateString().Replace("/", "") + "\\" + "Gunark.exe";
                        Thread.Sleep(1);
                        thtmp.Start(appName);
                    }
                }
            }
            private void run(Object obj)
            {
                Process ps = new Process();
                ps.StartInfo.FileName = obj.ToString();
                ps.Start();
            }
            #endregion 软件升级
        #endregion 枪柜维护子页面
        #region 指纹管理子页面
            #region 属性
            List<Model.user> finger_user_list = null;
            List<Model.fingerprint> fingerPrint_list = null;
            Bll.fingerprint fingerPrint_bll = new Gunark.BLL.fingerprint();
            Bll.user user_bll = new Gunark.BLL.user();
            #endregion 属性
            #region 预加载项
            /// <summary>
            /// 加载全部用户
            /// </summary>
            private void download_finger_layout()
            {
                dataGridView7.AutoGenerateColumns = false;
                finger_user_list = user_bll.GetModelList("");
                dataGridView7.DataSource = finger_user_list;
            }
            /// <summary>
            /// 加载全部未上传指纹信息
            /// </summary>
            private void upload_finger_layout()
            {
                dataGridView1.AutoGenerateColumns = false;

                fingerPrint_list = fingerPrint_bll.GetModelList("USER_TYPE is NULL");
                dataGridView1.DataSource = fingerPrint_list;
            }
            /// <summary>
            /// 加载全部指纹信息
            /// </summary>
            private void delete_finger_layout()
            {
                dataGridView2.AutoGenerateColumns = false;

                fingerPrint_list = fingerPrint_bll.GetAllList();
                dataGridView2.DataSource = fingerPrint_list;
            }
            #endregion 预加载项
            #region 录入指纹
            /// <summary>
            /// 录入指纹(获取用户组和指纹号)
            /// </summary>
            private void input_finger_layout()
            {
                comboBox2.SelectedIndex = 0;
                finger_user_list = user_bll.GetModelList("");
                List<string> userNameList = new List<string>();
                foreach (Model.user u in finger_user_list)
                {
                    userNameList.Add(u.USER_REALNAME);
                }
                comboBox1.DataSource = userNameList;
            }
            /// <summary>
            /// 调用下位机录入指纹
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button3_Click_1(object sender, EventArgs e)
            {
                string userPoliceNumb = user_bll.GetModelByName(comboBox1.Text).USER_POLICENUMB;
                string user_id = user_bll.GetModelByName(comboBox1.Text).USER_ID;
                string fingerNum = comboBox2.Text;
                Model.fingerprint fingerPrint = new Gunark.Model.fingerprint();
                if (fingerPrint_bll.Exists(userPoliceNumb, fingerNum))
                {
                    fingerPrint = fingerPrint_bll.GetModelByUser(user_id, fingerNum);
                    MessageBox.Show("已存在指纹");
                }
                else
                {
                    Model.user user = user_bll.GetModel(userPoliceNumb);
                    fingerPrint.USER_POLICENUMB = userPoliceNumb;
                    fingerPrint.FINGER_NUMBER = fingerNum;
                    fingerPrint.USER_FINGERPRINT_ID = user.USER_ID;
                    fingerPrint.USER_NAME = user.USER_NAME;
                    fingerPrint.USER_PWD = user.USER_PWD;
                    fingerPrint.UNIT_ID = user.UNITINFO_CODE;
                    fingerPrint.USER_PRIVIEGES = user.USER_PRIVIEGES;
                    Printer p = PrinterInstance.getInstance().getPrinter();
                    p.registerPrinter();
                    while (true)
                    {
                        if (p.InputFingerbool)
                        {
                            fingerPrint.USER_FINGERPRINT = p.getRegisterFinger();
                            fingerPrint_bll.Add(fingerPrint);
                            fingerPrint = fingerPrint_bll.GetModelByUser(user_id, fingerNum);
                            //MessageBox.Show(System.Text.Encoding.Default.GetString(p.getRegisterFinger()));
                            InputFinger.input(fingerPrint, p.getRegisterFinger());
                            p.InputFingerbool = false;
                            break;
                        }
                        if (!p.InputFail)
                        {
                            MessageBox.Show("请重新录入");
                            p.InputFail = true;
                            break;
                        }
                    }
                }
            }
            #endregion 录入指纹
            #region 下载指纹
            /// <summary>
            /// 下载指纹
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button2_Click(object sender, EventArgs e)
            {
                int count = 0;
                //获取选中的行数
                for (int i = 0; i < dataGridView7.RowCount; i++)
                {
                    if ((bool)dataGridView7.Rows[i].Cells[0].EditedFormattedValue)
                    {
                        count++;
                    }
                }
                if (count <= 0)
                {
                    MessageBox.Show("请选择人员！", "提示");
                    return;
                }
                if (MessageBox.Show(this, "您确定要下载么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    Bll.user user_bll = new Gunark.BLL.user();
                    
                    for (int i = 0; i < dataGridView7.RowCount; i++)
                    {
                        if ((bool)dataGridView7.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            string userPoliceNum = dataGridView7.Rows[i].Cells[1].Value.ToString();
                            WebService.fingerUser[] fingerPrint_list = webService.getFingerUserInfo(userPoliceNum, user_bll.GetModel(userPoliceNum).UNITINFO_CODE);

                            for(int j = 0;j<fingerPrint_list.Length;j++)
                            {
                                if (!fingerPrint_bll.ExistsByFingerPrintId(fingerPrint_list[j].fingerPrintId))
                                {
                                    Model.fingerprint fingerPrint = new Gunark.Model.fingerprint();

                                    fingerPrint.USER_FINGERPRINT_ID = fingerPrint_list[j].fingerPrintId;
                                    fingerPrint.USER_POLICENUMB = fingerPrint_list[j].userPoliceNumb;
                                    fingerPrint.USER_NAME = fingerPrint_list[j].userName;
                                    fingerPrint.FINGER_NUMBER = fingerPrint_list[j].fingerNumber.ToString();
                                    fingerPrint.UNIT_ID = fingerPrint_list[j].unitCode;
                                    fingerPrint.USER_FINGERPRINT = fingerPrint_list[j].userFingerPrint;
                                    fingerPrint.IS_UPDATE = 0;
                                    fingerPrint_bll.Add(fingerPrint);

                                    DownloadPrinter.download(fingerPrint);
                                }
                            }
                        }
                    }
                    for (int i = dataGridView7.Rows.Count-1; i >= 0;i--)
                    {
                        if ((bool)dataGridView7.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            finger_user_list.RemoveAt(i);
                        }
                    }
                    dataGridView7.DataSource = null;
                    dataGridView7.DataSource = finger_user_list;
                }
            }
            #endregion 下载指纹
            #region 上传指纹
            /// <summary>
            /// 指纹上传
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button4_Click(object sender, EventArgs e)
            {
                int count = 0;
                //获取选中的行数
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                    {
                        count++;
                    }
                }
                if (count <= 0)
                {
                    MessageBox.Show("请选择人员！", "提示");
                    return;
                }
                if (MessageBox.Show(this, "您确定要上传么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            //上传接口待完成
                            
                            fingerPrint_list[i].USER_TYPE = "isUpload";
                            fingerPrint_bll.Update(fingerPrint_list[i]);
                        }
                    }
                    MessageBox.Show("上传指纹接口待开发！");
                    for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            fingerPrint_list.RemoveAt(i);
                        }
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = fingerPrint_list;
                }
            }
            #endregion 上传指纹
            #region 删除指纹
            /// <summary>
            /// 删除指纹
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void button5_Click(object sender, EventArgs e)
            {
                int count = 0;
                //获取选中的行数
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    if ((bool)dataGridView2.Rows[i].Cells[0].EditedFormattedValue)
                    {
                        count++;
                    }
                }
                if (count <= 0)
                {
                    MessageBox.Show("请选择人员！", "提示");
                    return;
                }
                if (MessageBox.Show(this, "您确定要删除么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {

                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if ((bool)dataGridView2.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            //上传接口待完成
                            
                            fingerPrint_bll.Delete(fingerPrint_list[i].ID);
                        }
                    }
                    MessageBox.Show("删除服务器指纹接口、删除指纹机中的指纹，待开发！");
                    for (int i = dataGridView2.Rows.Count - 1; i >= 0; i--)
                    {
                        if ((bool)dataGridView2.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            fingerPrint_list.RemoveAt(i);
                        }
                    }
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = fingerPrint_list;
                }
            }
            #endregion 删除指纹
            #region 整行选中
        /// <summary>
        /// 整行选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            //MessageBox.Show(e.RowIndex.ToString());
            if (e.RowIndex == -1)
            {
                index = 0;
            }
            else
            {
                index = e.RowIndex;
            }

            if (dataGridView7.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView7.Rows[index].Cells[0];
                if (check.Value == null || false.Equals(check.Value))
                    check.Value = true;
                else
                    check.Value = false;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            //MessageBox.Show(e.RowIndex.ToString());
            if (e.RowIndex == -1)
            {
                index = 0;
            }
            else
            {
                index = e.RowIndex;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView1.Rows[index].Cells[0];
                if (check.Value == null || false.Equals(check.Value))
                    check.Value = true;
                else
                    check.Value = false;
            }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;
            //MessageBox.Show(e.RowIndex.ToString());
            if (e.RowIndex == -1)
            {
                index = 0;
            }
            else
            {
                index = e.RowIndex;
            }

            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewCheckBoxCell check = (DataGridViewCheckBoxCell)dataGridView2.Rows[index].Cells[0];
                if (check.Value == null || false.Equals(check.Value))
                    check.Value = true;
                else
                    check.Value = false;
            }
        }
        #endregion 整行选中
            #region 全选/反选
        /// <summary>
        /// 全选/反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (dataGridView7.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView7.Rows.Count; i++)
                    {
                        dataGridView7.Rows[i].Cells[0].Value = true;
                    }
                }
            }
            else
            {
                if (dataGridView7.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView7.Rows.Count; i++)
                    {
                        if ((bool)dataGridView7.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            dataGridView7.Rows[i].Cells[0].Value = false;
                        }
                        else
                            dataGridView7.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }
            else
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = false;
                        }
                        else
                            dataGridView1.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        dataGridView2.Rows[i].Cells[0].Value = true;
                    }
                }
            }
            else
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if ((bool)dataGridView2.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            dataGridView2.Rows[i].Cells[0].Value = false;
                        }
                        else
                            dataGridView2.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }
        #endregion 全选/反选
        #endregion 指纹管理子页面
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
