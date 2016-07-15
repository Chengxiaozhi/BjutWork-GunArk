using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyUI.Utils;
using System.Configuration;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;

namespace MyUI
{
    public partial class QuickGet : Form
    {
        //调用WebServie接口工具
        WebService.gunServices webService = SingleWebService.getWebService();
        private string task_type = "";
        private Bll.gun_info gun_info_bll = new Gunark.BLL.gun_info();
        private Bll.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
        private Bll.task_info task_info_bll = new Gunark.BLL.task_info();
        private Bll.task_info_detail task_info_details_bll = new Gunark.BLL.task_info_detail();
        private Bll.position_info gun_position_info_bll = new Gunark.BLL.position_info();
        private List<string> gun_number = new List<string>();
        private List<string> gun_info_id = new List<string>();
        private List<string> magazine_number = new List<string>();
        private List<int> apply_bullet_qty = new List<int>();
        private List<string> magazine_info_id = new List<string>();
        private List<string> gun_position_info_id = new List<string>();
        public QuickGet(string type)
        {
            this.task_type = type;
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }
        

        private void QuickGet_Load(object sender, EventArgs e)
        {
            if (task_type.Equals("emergency"))
            {
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.comboBox1.Visible = false;
                this.comboBox2.Visible = false;
            }
            //显示所有枪弹
            //Display();
            backgroundWorker1.RunWorkerAsync();
            //调用语音（请选择任务,可配置）
            if (bool.Parse(ConfigurationManager.AppSettings["set_sound"]))
                new SoundPlayer(Properties.Resources._27).Play();
            if (!task_type.Equals("quick"))
            {
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
            }
        }
       
        /// <summary>
        /// 显示所有枪弹
        /// </summary>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //禁止自动创建列
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;

            List<Model.position_info> gun_position_info_list = new List<Gunark.Model.position_info>();
            gun_position_info_list = gun_position_info_bll.GetModelList("GUN_POSITION_STATUS = '3'");
            dataGridView1.DataSource = gun_position_info_list;
            dataGridView1.ClearSelection();
            //根据枪弹绑定向子弹数组中添加元素
            //...
            List<Model.magazine_info> magazine_info_list = new List<Gunark.Model.magazine_info>();
            for (int i = 0; i < gun_position_info_list.Count; i++)
            {
                magazine_info_list.Add(magazine_info_bll.GetModelByMagazineNum(gun_position_info_list[i].GUN_POSITION_NUMBER));
            }
            //magazine_info_list = magazine_info_bll.GetAllList();
            dataGridView2.DataSource = magazine_info_list;
            dataGridView2.ClearSelection();
        }
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("") && task_type.Equals("quick"))
            {
                MessageBox.Show("请选择任务类型");
                return;
            }
            if (comboBox2.Text.Equals("") && task_type.Equals("quick"))
            {
                MessageBox.Show("请选择任务时间");
                return;
            }
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
                MessageBox.Show("请选择枪支！", "提示");
                return;
            }
            //身份验证界面
            Verify v = new Verify();
            v.Flag = 3;
            v.ShowDialog();
            //如果身份验证通过
            if (v.Result)
            {
                if (MessageBox.Show(this, "您确定要执行该任务么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString() == "Yes")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        {
                            //枪支信息
                            gun_number.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            gun_info_id.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            gun_position_info_id.Add(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            ////子弹信息
                            //magazine_number.Add(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            //magazine_info_id.Add(dataGridView2.Rows[i].Cells[4].Value.ToString());
                            //try
                            //{
                            //    apply_bullet_qty.Add(Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString()));
                            //}
                            //catch (NullReferenceException n)
                            //{
                            //    MessageBox.Show("第【" + (i + 1) + "】行没有填写取弹数量！如果只取枪不取弹，请填写【0】！");
                            //    return;
                            //}
                            //if (Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value.ToString()) == 0)
                            //{
                            //    magazine_number.RemoveAt(i);
                            //    magazine_info_id.RemoveAt(i);
                            //}
                        }
                    }
                    Model.task_info task_info = new Gunark.Model.task_info();
                    //机构ID
                    task_info.UNIT_ID = ConfigurationManager.AppSettings["unit_id"];
                    //枪柜ID
                    task_info.gunarkId = ConfigurationManager.AppSettings["gunark_id"];
                    //任务类型（13代表快速取枪,8代表紧急取枪）
                    if (task_type.Equals("quick"))
                    {
                        task_info.TASK_BIGTYPE = 13;
                        task_info.task_SmallType = "快速取枪";
                        //调用快速取枪接口
                        //...将任务id替换为调用接口返回的id值
                    }
                    else
                    {
                        task_info.TASK_BIGTYPE = 8;
                        task_info.task_SmallType = "紧急取枪";
                    }
                    //任务状态
                    task_info.task_Status = "3";
                    this.button1.Enabled = false;
                    this.button2.Enabled = false;
                    //调用控制柜子公共类，返回值标识是否正常打开枪柜门
                    bool control_status = Utils.ControlGunark.control(task_info, ListToByte.List2Byte(gun_number), ListToByte.List2Byte(magazine_number));
                    //判断是否正常打开柜门，如果未正常打开，重新执行该任务
                    if (!control_status)
                    {
                        MessageBox.Show(this, "超时未开门，请重新做任务", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        //生成任务信息
                        //32位GUID
                        task_info.TASK_ID = System.Guid.NewGuid().ToString();
                        //任务开始时间
                        task_info.task_Plan_BeginTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now);
                        switch (comboBox2.Text)
                        {
                            case "4小时":
                                task_info.task_Plan_FinishTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now.AddHours(+4d));
                                break;
                            case "8小时":
                                task_info.task_Plan_FinishTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now.AddHours(+8d));
                                break;
                            case "12小时":
                                task_info.task_Plan_FinishTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now.AddHours(+12d));
                                break;
                            default:
                                //紧急任务（默认执勤时间为24小时）
                                task_info.task_Plan_FinishTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now.AddHours(+24d));
                                break;
                        }
                        
                        //任务申请人
                        task_info.TASK_APPLY_USERID = PubFlag.policeNum;
                        //任务审批人
                        task_info.TASK_APPROVAL_USERID = PubFlag.bossLeaderNum;
                        task_info_bll.Add(task_info);
                        //生成任务详情信息
                        for (int i = 0; i < gun_number.Count; i++)
                        {
                            Model.task_info_detail task_info_details = new Gunark.Model.task_info_detail();
                            try
                            {
                                //任务详情ID
                                task_info_details.task_Detail_ID = System.Guid.NewGuid().ToString();
                                //任务ID
                                task_info_details.TASK_ID = task_info.task_ID;
                                //机构ID
                                task_info_details.UNIT_ID = task_info.unitId;
                                //枪柜ID
                                task_info_details.GUNARK_ID = task_info.gunarkId;
                                //枪支ID
                                task_info_details.GUN_INFO_ID = gun_info_id[i];
                                //枪位ID
                                task_info_details._gun_position_info_id = gun_position_info_bll.GetModelByGunInfo(task_info_details.GUN_INFO_ID).GUN_POSITION_INFO_ID;
                                //用枪人
                                task_info_details.GUN_DUTY_USER = PubFlag.policeNum;
                                //如果有子弹
                                if (magazine_number.Count > 0)
                                {
                                    //弹仓ID
                                    task_info_details.MAGAZINE_INFO_ID = magazine_info_id[i];
                                    //子弹类型
                                    task_info_details.BULLET_TYPE = magazine_info_bll.GetModel(magazine_info_id[i]).BULLET_MODEL;
                                    //取弹数量
                                    task_info_details.apply_Bullet_Qty = apply_bullet_qty[i];
                                }
                                task_info_details.FLAG_TYPE = 1;
                                task_info_details_bll.Add(task_info_details);
                            }
                            catch (ArgumentOutOfRangeException a) { task_info_details_bll.Add(task_info_details); }
                        }
                        if (task_type.Equals("quick"))
                        {
                            //插入日志
                            Utils.AddLog.add(1, "快速取枪弹", gun_number.Count.ToString(), magazine_number.Count.ToString());
                        }
                        else
                        {
                            //插入日志
                            Utils.AddLog.add(1, "紧急取枪弹", gun_number.Count.ToString(), magazine_number.Count.ToString());
                            //webService.setUrgentGunBulletTaskNew(task_info.task_ID,);
                        }
                        //更新本地数据库任务状态,建立执行中任务
                        UpdateTaskInfo.update(task_info, "5", "3", "2", "取弹", gun_info_id, gun_position_info_id, magazine_info_id, task_info_details_bll.GetModelList("TASK_ID = '"+task_info.TASK_ID+"'"));
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region datagridview1
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    if (e.Value.ToString() != "null")
            //    {
            //        if (object.Equals(e.Value, "1"))
            //        {
            //            e.Value = "64式子弹";
            //        }
            //        else if (object.Equals(e.Value, "2"))
            //        {
            //            e.Value = "51式子弹";
            //        }
            //        else if (object.Equals(e.Value, "3"))
            //        {
            //            e.Value = "51式空爆弹";
            //        }
            //        else if (object.Equals(e.Value, "4"))
            //        {
            //            e.Value = "97式动能弹";
            //        }
            //        else if (object.Equals(e.Value, "5"))
            //        {
            //            e.Value = "97式杀伤弹";
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                {
                    try
                    {
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = SystemColors.Highlight;
                        dataGridView2.Rows[i].DefaultCellStyle.ForeColor = SystemColors.Window;
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2.Message);
                    }
                }
                else
                {
                    try
                    {
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = SystemColors.Window;
                        dataGridView2.Rows[i].DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.Message);
                    }
                }
            }
        }
        #endregion datagridview1

        private void QuickGet_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundWorker1.Dispose();
        }

        
    }
}
