using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using MyUI.Utils;
using BLL = Gunark.BLL;
using Model = Gunark.Model;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
namespace MyUI
{
    public partial class Select_Task : Form
    {
        
        //实例化业务逻辑层对象
        Gunark.BLL.task_info task_info_bll = new Gunark.BLL.task_info();
        BLL.user user_bll = new Gunark.BLL.user();
        public Select_Task()
        {
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }
        
        private void Select_Task_Load(object sender, EventArgs e)
        {
            
            //禁止datagridview自动创建列和行
            this.dataGridView1.AutoGenerateColumns = false;
            //通过webservice接口从服务器同步任务
            //...
            //显示任务,根据条件查询任务
            Display();
            //如果没有任务，将“选择”按钮置为不可用状态
            if (this.dataGridView1.Rows.Count == 0)
                this.button1.Enabled = false;
            //调用语音（请选择任务,可配置）
            if (bool.Parse(ConfigurationManager.AppSettings["set_sound"]))
                new SoundPlayer(Properties.Resources._17).Play();
        }
        /// <summary>
        /// 显示任务
        /// </summary>
        private void Display()
        {
            List<Model.task_info> list = new List<Gunark.Model.task_info>();
            //查询满足条件的结果集
            switch (PubFlag.task_type)
            {
                case "领取枪弹":
                    list = task_info_bll.GetModelList("TASK_BIGTYPE='3'and TASK_STATUS='3' and ('" + string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now) + "' between TASK_PLAN_BEGINTIME and TASK_PLAN_FINISHTIME)");
                    dataGridView1.DataSource = list;
                    break;
                case "归还枪弹":
                    if(PubFlag.online)
                        list = task_info_bll.GetModelList("(TASK_BIGTYPE='3' or TASK_BIGTYPE='6' or TASK_BIGTYPE='13' or TASK_BIGTYPE='8' or TASK_BIGTYPE='9') and (TASK_STATUS='5' or TASK_STATUS='7')");
                    else
                        list = task_info_bll.GetModelList("(TASK_BIGTYPE='13' or TASK_BIGTYPE='8') and (TASK_STATUS='5' or TASK_STATUS='7')");
                    //判断任务状态是否为超期未还
                    for (int i = 0; i < list.Count; i++)
                    {
                        if ((string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now).CompareTo(list[i].TASK_PLAN_FINISHTIME) == 1))
                        {
                            Model.task_info task_info = new BLL.task_info().GetModel(list[i].TASK_ID);
                            list[i].TASK_STATUS = "7";
                            task_info.TASK_STATUS = "7";
                            new BLL.task_info().Update(task_info);
                        }
                    }
                    dataGridView1.DataSource = list;
                    break;
                case "其他任务":
                    list = task_info_bll.GetModelList("(TASK_BIGTYPE='2'or TASK_BIGTYPE='4' or TASK_BIGTYPE='5' or TASK_BIGTYPE='6' or TASK_BIGTYPE='7' or TASK_BIGTYPE='9' or TASK_BIGTYPE='10' or TASK_BIGTYPE='11' or TASK_BIGTYPE='12' or TASK_BIGTYPE='14') and TASK_STATUS='3' and ('" + string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now) + "' between TASK_PLAN_BEGINTIME and TASK_PLAN_FINISHTIME)");
                    dataGridView1.DataSource = list;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 获取选中的任务ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string task_id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            Task_Details td = new Task_Details(new BLL.task_info().GetModel(task_id));
            td.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// dataGridView1格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString() != "" && e.Value.ToString() != "null")
                    if (user_bll.Exists(e.Value.ToString()))
                        e.Value = user_bll.GetModel(e.Value.ToString()).USER_REALNAME;
            }
            if (e.ColumnIndex == 3 /*绑定数据源中列的序号*/)
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
            if (e.ColumnIndex == 1 /*绑定数据源中列的序号*/)
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
        }
    }
}
