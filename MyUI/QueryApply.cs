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
    public partial class QueryApply : Form
    {
        //调用WebServie接口工具
        WebService.gunServices webService = SingleWebService.getWebService();
        private string task_type = "";
        private Bll.gun_info gun_info_bll = new Gunark.BLL.gun_info();
        private Bll.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
        private Bll.task_info task_info_bll = new Gunark.BLL.task_info();
        private Bll.task_info_detail task_info_details_bll = new Gunark.BLL.task_info_detail();
        private Bll.position_info gun_position_info_bll = new Gunark.BLL.position_info();
        private Bll.user user_bll = new Gunark.BLL.user();
        private Bll.group group_bll = new Gunark.BLL.group();
        private Bll.gbg gbg_bll = new Gunark.BLL.gbg();
        private List<string> gun_number = new List<string>();
        private List<string> gun_info_id = new List<string>();
        private List<string> magazine_number = new List<string>();
        //private List<int> apply_bullet_qty = new List<int>();
        private Dictionary<string, int> apply_bullet_qty = new Dictionary<string, int>();
        private List<string> magazine_info_id = new List<string>();
        private List<string> gun_position_info_id = new List<string>();
        private List<string> bullet_model = new List<string>();
        public QueryApply(string type)
        {
            this.task_type = type;
            InitializeComponent();
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }

        private void QueryApply_Load(object sender, EventArgs e)
        {
            if (task_type.Equals("emergency"))
            {
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.comboBox1.Visible = false;
                this.comboBox2.Visible = false;
            }
            Display();
            //调用语音（请选择任务,可配置）
            if (bool.Parse(ConfigurationManager.AppSettings["set_sound"]))
                new SoundPlayer(Properties.Resources._27).Play();
        }
        /// <summary>
        /// 显示枪弹
        /// </summary>
        private void Display()
        {
            /*----------------------------------------------------------------
             *按照班组显示枪弹（显示验指纹的执勤民警那一组的枪弹） 
             *    1）找到user对象
             *    2）通过user对象找到组对象
             *    3）查找枪、弹、组表，找出本组枪弹
             *    4）显示枪、弹信息
             * 
            **----------------------------------------------------------------*/
            string userid = PubFlag.policeNum;
            Model.user user = user_bll.GetModel(userid);
            List<Model.gbg> gbg_list = gbg_bll.GetModelList("GROUP_ID = '"+user.GROUP_ID+"'");
            //枪的信息
            this.listView1.View = View.LargeIcon;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.BeginUpdate();

            for (int i = 0; i < gbg_list.Count; i++)
            {
                try
                {
                    ListViewItem lvi = new ListViewItem();
                    string pos_num = gbg_list[i].GUN_LOCATION.ToString();
                    lvi.ImageIndex = 1;
                    // 枪号找对应枪型
                    if (!"3".Equals(gun_position_info_bll.GetModelByGunPosNum(pos_num).GUN_POSITION_STATUS))
                        continue;
                    lvi.Text = gbg_list[i].GUN_LOCATION + "\n" + gun_position_info_bll.GetModelByGunPosNum(pos_num).GUN_TYPE;
                    
                    this.listView1.Items.Add(lvi);
                }
                catch { continue; }
            }

            this.listView1.EndUpdate();

            //弹的信息
            this.listView2.View = View.LargeIcon;

            this.listView2.LargeImageList = this.imageList1;

            this.listView2.BeginUpdate();

            for (int i = 0; i < gbg_list.Count; i++)
            {
                try
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = 3;
                    // 弹仓号
                    if (gbg_list[i].BULLET_LOCATION != 0)
                        lvi.Text = gbg_list[i].BULLET_LOCATION.ToString() + "\n" + getBulletType(magazine_info_bll.GetModelByMagazineNum(gbg_list[i].BULLET_LOCATION.ToString()).BULLET_MODEL);
                    else
                        lvi.Text = (i + 1) + "\n" + getBulletType(magazine_info_bll.GetModelByMagazineNum((i + 1).ToString()).BULLET_MODEL);
                    this.listView2.Items.Add(lvi);
                }
                catch { continue; }
            }

            this.listView2.EndUpdate();
        }
        /// <summary>
        /// 获取子弹类型
        /// </summary>
        /// <param name="bullet_model"></param>
        /// <returns></returns>
        private string getBulletType(string bullet_model)
        {
            string bullet_type = "";
            switch (bullet_model)
            {
                case "1":
                    bullet_type = "64式子弹";
                    break;
                case "2":
                    bullet_type = "51式子弹";
                    break;
                case "3":
                    bullet_type = "51式空爆弹";
                    break;
                case "4":
                    bullet_type = "97式动能弹";
                    break;
                case "5":
                    bullet_type = "97式杀伤弹";
                    break;
                default:
                    break;
            }
            return bullet_type;
        }
        /// <summary>
        /// 选择枪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                var item = info.Item as ListViewItem;
                string gun_pos_num = item.Text.Substring(0, item.Text.IndexOf("\n"));
                if (item.ImageIndex == 0)
                {
                    item.ImageIndex = 1;
                    gun_position_info_id.Remove(gun_position_info_bll.GetModelByGunPosNum(gun_pos_num).GUN_POSITION_INFO_ID);
                    gun_info_id.Remove(gun_position_info_bll.GetModelByGunPosNum(gun_pos_num).GUN_INFO_ID);
                    gun_number.Remove(gun_pos_num);
                }
                else
                {
                    item.ImageIndex = 0;
                    gun_position_info_id.Add(gun_position_info_bll.GetModelByGunPosNum(gun_pos_num).GUN_POSITION_INFO_ID);
                    gun_info_id.Add(gun_position_info_bll.GetModelByGunPosNum(gun_pos_num).GUN_INFO_ID);
                    gun_number.Add(gun_pos_num);
                }
            }
        }
        /// <summary>
        /// 选择弹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView2.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                var item = info.Item as ListViewItem;
                string mag_num = item.Text.Substring(0,item.Text.IndexOf("\n"));
                if (item.ImageIndex == 2)
                {
                    item.ImageIndex = 3;
                    magazine_number.Remove(mag_num);
                    string mii = magazine_info_bll.GetModelByMagazineNum(mag_num).MAGAZINE_INFO_ID;
                    magazine_info_id.Remove(mii);
                    apply_bullet_qty.Remove(mii);
                }
                else
                {
                    
                    item.ImageIndex = 2;
                    magazine_number.Add(mag_num);
                    Model.magazine_info mi = magazine_info_bll.GetModelByMagazineNum(mag_num);
                    string mii = mi.MAGAZINE_INFO_ID;
                    magazine_info_id.Add(mii);
                    bullet_model.Add(mi.BULLET_MODEL);
                    //输入子弹数量
                    inputBulletQty ib = new inputBulletQty(mii);
                    ib.ShowDialog();
                    if(!apply_bullet_qty.ContainsKey(mii))
                        apply_bullet_qty.Add(mii, ib.BulletQty);
                }
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int[] applyQty = new int[apply_bullet_qty.Count];
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
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].ImageIndex == 0)
                {
                    count++;
                    break;
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
                        //任务申请时间
                        task_info.TASK_APPLY_TIME = string.Format("{0:yyyy-MM-dd HH:mm:ss.f}", DateTime.Now);
                        //任务审批人
                        task_info.TASK_APPROVAL_USERID = PubFlag.bossLeaderNum;
                        //将同步状态设为未同步(0:未同步，1：已同步)
                        task_info.taskFlagType = "0";
                        //如果是快速取枪,调用webservice接口，将任务信息上传
                        
                        task_info_bll.Add(task_info);
                        //生成任务详情信息(枪)
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
                                task_info_details.FLAG_TYPE = 1;

                                task_info_details_bll.Add(task_info_details);
                            }
                            catch  {  }
                        }
                        //生成任务详情信息（弹）
                        for (int i = 0; i < magazine_number.Count; i++)
                        {
                            try
                            {
                                Model.task_info_detail task_info_details = new Gunark.Model.task_info_detail();
                                //任务详情ID
                                task_info_details.task_Detail_ID = System.Guid.NewGuid().ToString();
                                //任务ID
                                task_info_details.TASK_ID = task_info.task_ID;
                                //机构ID
                                task_info_details.UNIT_ID = task_info.unitId;
                                //枪柜ID
                                task_info_details.GUNARK_ID = task_info.gunarkId;
                                //弹仓ID
                                task_info_details.MAGAZINE_INFO_ID = magazine_info_id[i];
                                //子弹类型
                                task_info_details.BULLET_TYPE = magazine_info_bll.GetModel(magazine_info_id[i]).BULLET_MODEL;
                                //取弹数量
                                int qty = 0;
                                apply_bullet_qty.TryGetValue(task_info_details.MAGAZINE_INFO_ID,out qty);
                                task_info_details.apply_Bullet_Qty = qty;
                                applyQty[i] = qty;

                                if (task_info.task_BigType == 8)
                                {
                                    task_info_details.FLAG_TYPE = 0;
                                    //用这个字段标识紧急取枪需要调用接口情况，（get：调“未置枪接口”；return：调“置枪接口”）
                                    task_info_details.BULLET_ID = "get";
                                }

                                task_info_details_bll.Add(task_info_details);
                            }
                            catch { }
                        }

                        if (task_type.Equals("quick"))
                        {
                            //插入日志
                            Utils.AddLog.add(1, "快速取枪弹", gun_number.Count.ToString(), magazine_number.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "5", "3", "2", "取弹", gun_info_id, gun_position_info_id, magazine_info_id, task_info_details_bll.GetModelList("TASK_ID = '" + task_info.TASK_ID + "'"));
                            //调用webservice接口，上传快速取枪任务
                            //....
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setgunInUse(gun_info_id[i]);
                                webService.setGunNotOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_position_info_id[i]);
                            }
                            //调用webservice接口，更改子弹库存量
                            for (int i = 0; i < magazine_info_id.Count; i++)
                            {
                                webService.setMagazineStock(magazine_info_id[i], (int)magazine_info_bll.GetModel(magazine_info_id[i]).STOCK_QTY);
                            }
                        }
                        else
                        {
                            //插入日志
                            Utils.AddLog.add(1, "紧急取枪弹", gun_number.Count.ToString(), magazine_number.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "5", "3", "2", "取弹", gun_info_id, gun_position_info_id, magazine_info_id, task_info_details_bll.GetModelList("TASK_ID = '" + task_info.TASK_ID + "'"));
                            //调用webservice接口，上传紧急取枪任务
                            //....
                        }
                        this.Close();
                    }
                }
            }
        }
    }
}
