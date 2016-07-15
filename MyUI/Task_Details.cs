using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using BLL = Gunark.BLL;
using Model = Gunark.Model;
using MyUI.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
namespace MyUI
{
    public partial class Task_Details : Form
    {

        //调用WebServie接口工具
        WebService.gunServices webService = SingleWebService.getWebService();
        //控制柜子
        //Communication c = new Communication();
        
        //任务对象
        Model.task_info task_info = new Gunark.Model.task_info();
        //实例化业务逻辑层对象
        BLL.task_info_detail bll = new BLL.task_info_detail();
        BLL.magazine_info magazine_info_bll = new Gunark.BLL.magazine_info();
        /// <summary>
        /// //gun_pos_id集合
        /// </summary>
        List<string> gun_pos_id = new List<string>();
        /// <summary>
        /// gun_info_id
        /// </summary>
        List<string> gun_info_id = new List<string>();
        /// <summary>
        /// //gun_num集合
        /// </summary>
        List<string> gun_number = new List<string>();
        /// <summary>
        ///magazine_id集合
        /// </summary>
        List<string> magazine_id = new List<string>();
        /// <summary>
        ///magazine_number集合
        /// </summary>
        List<string> magazine_number = new List<string>();
        //任务详情集合
        List<Model.task_info_detail> list = new List<Gunark.Model.task_info_detail>();
        //枪弹分开显示
        List<Model.task_info_detail> list_gun = new List<Gunark.Model.task_info_detail>();
        List<Model.task_info_detail> list_bullet = new List<Gunark.Model.task_info_detail>();
        private bool _isUsual = false;
        public Task_Details()
        {
            InitializeComponent();
           
        }

        public Task_Details(Model.task_info task)
        {
            InitializeComponent();
            this.task_info = task;
            //设置皮肤
            skinEngine1.SkinFile = Application.StartupPath + @"\RealOne.ssk";
        }

        

        private void Task_Details_Load(object sender, EventArgs e)
        {
            
            //禁止datagridview自动创建列
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            //显示任务
            if (task_info.task_BigType == 13 || task_info.task_BigType == 8)
                Display_quick();
            else
                Display();
        }
        /// <summary>
        /// 显示任务
        /// </summary>
        private void Display()
        {
            list = bll.GetModelList("TASK_ID ='" + task_info.TASK_ID + "'");
            //gun_id、magazine_id赋值
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].MAGAZINE_INFO_ID.Equals(""))
                {
                    list_bullet.Add(list[i]);
                    magazine_id.Add(list[i].MAGAZINE_INFO_ID);
                    magazine_number.Add(new BLL.magazine_info().GetModel(list[i].MAGAZINE_INFO_ID).MAGAZINE_NUMBER);
                }
                else if(!list[i].GUN_INFO_ID.Equals("null"))
                {
                    list_gun.Add(list[i]);
                    gun_pos_id.Add(list[i].GUN_POSITION_INFO_ID);
                    gun_info_id.Add(list[i].GUN_INFO_ID);
                    gun_number.Add(new BLL.position_info().GetModel(list[i].GUN_POSITION_INFO_ID).GUN_POSITION_NUMBER);
                }
            }
            //datagridview绑定数据源
            dataGridView1.DataSource = list_gun;
            dataGridView2.DataSource = list_bullet;
        }
        /// <summary>
        /// 显示任务(快速取枪)
        /// </summary>
        private void Display_quick()
        {
            list = bll.GetModelList("TASK_ID ='" + task_info.TASK_ID + "'");
            ////gun_id、magazine_id赋值
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].MAGAZINE_INFO_ID.Equals(""))
                {
                    list_bullet.Add(list[i]);
                    magazine_id.Add(list[i].MAGAZINE_INFO_ID);
                    magazine_number.Add(new BLL.magazine_info().GetModel(list[i].MAGAZINE_INFO_ID).MAGAZINE_NUMBER);
                }
                if(!list[i].GUN_INFO_ID.Equals(""))
                {
                    list_gun.Add(list[i]);
                    gun_pos_id.Add(list[i].GUN_POSITION_INFO_ID);
                    gun_info_id.Add(list[i].GUN_INFO_ID);
                    gun_number.Add(new BLL.position_info().GetModel(list[i].GUN_POSITION_INFO_ID).GUN_POSITION_NUMBER);
                }
            }
            //datagridview绑定数据源
            dataGridView1.DataSource = list_gun;
            dataGridView2.DataSource = list_bullet;
        }
        
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (task_info.task_BigType == 3 && task_info.task_Status == "3")
            {
                Verify v = new Verify();
                v.Flag = 1;
                v.ShowDialog();
                //如果身份验证通过
                if (v.Result)
                {
                    //向串口发送指令
                    backgroundWorker2.RunWorkerAsync();
                    this.button1.Enabled = false;
                }
            }
            else
            {
                Verify v = new Verify();
                v.Flag = 5;
                v.ShowDialog();
                //如果身份验证通过
                if (v.Result)
                {
                    //向串口发送指令
                    backgroundWorker2.RunWorkerAsync();
                    this.button1.Enabled = false;
                    this.button2.Enabled = false;
                }
            }
        }
        
        /// <summary>
        /// 发指令后台进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //调用控制柜子公共类，返回值标识是否正常打开枪柜门
            bool control_status = Utils.ControlGunark.control(task_info, ListToByte.List2Byte(gun_number), ListToByte.List2Byte(magazine_number));
            //判断是否正常打开柜门，如果未正常打开，重新执行该任务
            if (!control_status)
            {
                MessageBox.Show(this,"超时未开门，请重新做任务", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                this.button1.Enabled = true;
                this.button2.Enabled = true;
            }
            else
            {
                this.Close();
                //上传任务信息为“执行中”（调用webService接口）
                //webService.setDoingTask(task_info.TASK_ID, DateTime.Now,true,GetUserByFingerId.getUser(PubFlag.verifyUserId_1).USER_POLICENUMB, GetUserByFingerId.getUser(PubFlag.verifyUserId_2).USER_POLICENUMB);
                //检测对比取枪弹状态,返回值为true表示无异常枪位
                _isUsual = Utils.CheckGun.Check(gun_number);
                //如果没有异常枪位
                if (_isUsual)
                {
                    //更改数据库任务，枪支，枪位，子弹信息，并插入操作日志
                    switch (task_info.TASK_BIGTYPE)
                    {
                        //枪弹入柜
                        case 2:
                            Utils.AddLog.add(1, "枪弹入柜", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setGunInStore(gun_info_id[i]);
                                webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                            }
                            //调用webservice接口，更改子弹库存量
                            for (int i = 0; i < magazine_id.Count; i++)
                            {
                                webService.setMagazineStock(magazine_id[i], (int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //申请枪弹
                        case 3:
                            if (task_info.TASK_STATUS == "3")
                            {
                                Utils.AddLog.add(1,"领取枪弹" ,list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "5", "3", "2", "取弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setgunInUse(gun_info_id[i]);
                                    webService.setGunNotOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                }
                                //调用webservice接口，更改子弹库存量
                                for (int i = 0; i < magazine_id.Count; i++)
                                {
                                    webService.setMagazineStock(magazine_id[i], (int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                                }
                                //调用WebService接口，更改任务状态为执行中
                                webService.setDoingTask(task_info.task_ID,DateTime.Now,true,PubFlag.policeNum,PubFlag.dutyLeaderNum);
                            }
                            else if (task_info.TASK_STATUS == "5")
                            {
                                Utils.AddLog.add(1,"归还枪弹", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setGunInStore(gun_info_id[i]);
                                    webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                }
                                //调用webservice接口，更改子弹库存量
                                for (int i = 0; i < magazine_id.Count; i++)
                                {
                                    webService.setMagazineStock(magazine_id[i], (int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                                }
                                //调用WebService接口，更改任务状态为已完成
                                webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            else if (task_info.TASK_STATUS == "7")
                            {
                                Utils.AddLog.add(1,"超期归还", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setGunInStore(gun_info_id[i]);
                                    webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                }
                                //调用webservice接口，更改子弹库存量
                                for (int i = 0; i < magazine_id.Count; i++)
                                {
                                    webService.setMagazineStock(magazine_id[i], (int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                                }
                                //调用WebService接口，更改任务状态为已完成
                                webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            break;
                        //枪支封存
                        case 4:
                            Utils.AddLog.add(1, "枪支封存", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "8", "5", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setGunBeSeal(gun_info_id[i]);
                                webService.setGunPositionSeal(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //枪支报废
                        case 5:
                            Utils.AddLog.add(1, "枪支报废", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "5", "0", "取弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setGunBeScrap(gun_info_id[i]);
                                //webService.setGunPosNotOnGunark(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                webService.setGunPosNotOnGunark(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //枪支保养
                        case 6:
                            if (task_info.TASK_STATUS == "3")
                            {
                                Utils.AddLog.add(1, "枪支保养", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "5", "3", "2", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setgunInUse(gun_info_id[i]);
                                    webService.setGunNotOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                }
                                //调用WebService接口，更改任务状态为执行中
                                webService.setDoingTask(task_info.task_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            else if (task_info.TASK_STATUS == "5")
                            {
                                Utils.AddLog.add(1, "归还枪弹(保养)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setGunInStore(gun_info_id[i]);
                                    webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                    webService.setMaintainconfig(task_info.gunarkId);
                                }
                                //调用WebService接口，更改任务状态为已完成
                                webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            break;
                        //枪支调拨
                        case 7:
                            Utils.AddLog.add(1,"枪支调拨", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "9", "0", "取弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setGunBeAllot(gun_info_id[i]);
                                webService.setGunPosNotOnGunark(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                            }
                            //调用webservice接口，更改子弹库存量
                            for(int i = 0;i < magazine_id.Count; i++)
                            {
                                Console.WriteLine((int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                                webService.setMagazineStock(magazine_id[i],(int)magazine_info_bll.GetModel(magazine_id[i]).STOCK_QTY);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum,PubFlag.dutyLeaderNum);
                            break;
                        //紧急取枪
                        case 8:
                            if (task_info.TASK_STATUS == "5")
                            {
                                Utils.AddLog.add(1, "归还枪弹(紧急)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //更新调用接口参数
                                foreach (Model.task_info_detail item in list)
                                {
                                    item.FLAG_TYPE = 2;
                                    bll.Update(item);
                                }
                            }
                            else if (task_info.TASK_STATUS == "7")
                            {
                                Utils.AddLog.add(1, "超期归还(紧急取枪)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //更新调用接口参数
                                foreach (Model.task_info_detail item in list)
                                {
                                    item.FLAG_TYPE = 2;
                                    bll.Update(item);
                                }
                            }
                            break;
                        //枪弹检查（取出枪支）
                        case 9:
                            if (task_info.TASK_STATUS == "3")
                            {
                                Utils.AddLog.add(1,"枪弹检查", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "5", "3", "2", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //枪支点验不开枪锁，所以不更新数据库状态
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setgunInUse(gun_info_id[i]);
                                    webService.setGunNotOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                }
                                //调用WebService接口，更改任务状态为已完成
                                webService.setDoingTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            else if (task_info.TASK_STATUS == "5")
                            {
                                Utils.AddLog.add(1, "归还枪弹(检查)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //调用WebService接口，更改枪位状态
                                for (int i = 0; i < gun_number.Count; i++)
                                {
                                    webService.setGunInStore(gun_info_id[i]);
                                    webService.setGunOnPosition(task_info.UNIT_ID, task_info.GUNARK_ID, gun_pos_id[i]);
                                    
                                }
                                for (int i = 0; i < magazine_id.Count; i++)
                                {
                                    webService.setMagazineInfoCheck(magazine_id[i]);
                                }
                                //调用WebService接口，更改任务状态为已完成
                                webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            }
                            break;
                        //枪支解封
                        case 10:
                            Utils.AddLog.add(1, "枪支解封", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "1", "3", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setGunInStore(gun_info_id[i]);
                                webService.setGunOnPosition(task_info.unitId, task_info.gunarkId, gun_pos_id[i]);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //枪支寄存11
                        case 11:
                            Utils.AddLog.add(1, "枪支寄存", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "10", "1", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setgunLuggage(gun_info_id[i]);
                                webService.setgunpositionLuggage(gun_pos_id[i]);
                                //webService.setGunOnPosition(task_info.unitId, task_info.gunarkId, gun_info_id[i]);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //枪支解存12
                        case 12:
                            Utils.AddLog.add(1, "枪支解存", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "11", "2", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            for (int i = 0; i < gun_number.Count; i++)
                            {
                                webService.setgunExport(gun_info_id[i]);
                                webService.setGunPosNotOnGunark(task_info.unitId, task_info.gunarkId, gun_pos_id[i]);
                            }
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        //快速取枪
                        case 13:
                            if (task_info.TASK_STATUS == "5")
                            {
                                Utils.AddLog.add(1,"归还枪弹(快速取枪)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //更新调用接口参数
                                foreach (Model.task_info_detail item in list)
                                {
                                    item.FLAG_TYPE = 2;
                                    bll.Update(item);
                                }
                            }
                            else if (task_info.TASK_STATUS == "7")
                            {
                                Utils.AddLog.add(1, "超期归还(快速取枪)", list_gun.Count.ToString(), list_bullet.Count.ToString());
                                //更新本地数据库任务状态,建立执行中任务
                                UpdateTaskInfo.update(task_info, "6", "1", "3", "还弹", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                                //更新调用接口参数
                                foreach (Model.task_info_detail item in list)
                                {
                                    item.FLAG_TYPE = 2;
                                    bll.Update(item);
                                }
                            }
                            break;
                        //枪支点验
                        case 14:
                            Utils.AddLog.add(1, "枪支点验", list_gun.Count.ToString(), list_bullet.Count.ToString());
                            //更新本地数据库任务状态,建立执行中任务
                            UpdateTaskInfo.update(task_info, "6", "1", "3", "", gun_info_id, gun_pos_id, magazine_id, list_bullet);
                            //调用WebService接口，更改枪位状态
                            webService.setgunarkcheckingSuccess(task_info.gunarkId,task_info.unitId);
                            //调用WebService接口，更改任务状态为已完成
                            webService.setFinishTask(task_info.TASK_ID, DateTime.Now, true, PubFlag.policeNum, PubFlag.dutyLeaderNum);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 后台进程执行完毕，判断是否有异常枪位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //如果有异常枪位
            if(!_isUsual)
            {
                //右下角升起提示框，提示有异常任务
                PopForm.Instance().Show();
                //提示开门继续执行任务【？？？】（初步定为发开门指令继续执行任务）
                //comm.send_message(PubFlag.com_gun, Communication.open);
                //记录操作日志
                switch (task_info.TASK_BIGTYPE)
                {
                    case 3:
                        Utils.AddLog.add(1,"未正常取枪", list_gun.Count.ToString(), list_bullet.Count.ToString());
                        //调用WebService接口，异常任务报警
                        webService.setAlarm(task_info.GUNARK_ID, task_info.UNIT_ID, "未正常取枪", DateTime.Now, true, "异常任务");
                        break;
                    case 5:
                        Utils.AddLog.add(1,"未正常还枪", list_gun.Count.ToString(), list_bullet.Count.ToString());
                        //调用WebService接口，异常任务报警
                        webService.setAlarm(task_info.GUNARK_ID, task_info.UNIT_ID, "未正常还枪", DateTime.Now, true, "异常任务");
                        break;
                    case 2:
                        Utils.AddLog.add(1,"未正常入柜", list_gun.Count.ToString(), list_bullet.Count.ToString());
                        //调用WebService接口，异常任务报警
                        webService.setAlarm(task_info.GUNARK_ID, task_info.UNIT_ID, "未正常入柜", DateTime.Now, true, "异常任务");
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 释放辅助线程资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //两个datagridview同步滚动
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;  
            dataGridView2.HorizontalScrollingOffset = dataGridView1.HorizontalScrollingOffset;  
        }
        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView2.FirstDisplayedScrollingRowIndex;  
            dataGridView1.HorizontalScrollingOffset = dataGridView2.HorizontalScrollingOffset;
        }
        #region 
        /// <summary>
        /// datagridview1格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.Value.ToString() != "" && e.Value.ToString() != "null")
                    e.Value = new BLL.user().GetModel(e.Value.ToString()).USER_REALNAME;
                else
                    e.Value = "无";
            }
            if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString() != "null")
                    e.Value = new BLL.position_info().GetModel(e.Value.ToString()).GUN_POSITION_NUMBER;
            }
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString() != "null")
                    e.Value = new BLL.gun_info().GetModel(e.Value.ToString()).GUN_TYPE;
            }
        }
        /// <summary>
        /// datagridview2格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.Value.ToString() != "null")
                    e.Value = new BLL.magazine_info().GetModel(e.Value.ToString()).MAGAZINE_NUMBER;
            }
            if (e.ColumnIndex == 1)
            {
                if (e.Value.ToString() != "null")
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
        #endregion
        #region 输入还弹数量
        public DataGridViewTextBoxEditingControl CellEdit = null; // 声明 一个 CellEdit
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control; // 赋值                
            CellEdit.SelectAll();                 
            CellEdit.KeyPress += Cells_KeyPress; // 绑定到事件
        }
         // 自定义事件             
        private void Cells_KeyPress(object sender, KeyPressEventArgs e)             
        {
            
            if (dataGridView2.CurrentCellAddress.X == 2) // 判断当前列是不是要控制的列 我是控制的索引值为2的  列(即第三列)                 
            {                     
                if ((Convert.ToInt32(e.KeyChar) < 48 || Convert.ToInt32(e.KeyChar) > 57) && Convert.ToInt32(e.KeyChar) != 46 && Convert.ToInt32(e.KeyChar) != 8 && Convert.ToInt32(e.KeyChar) != 13)                     
                {                         
                    e.Handled = true;  // 输入非法就屏蔽                    
                }                    
                else                   
                {                         
                    if ((Convert.ToInt32(e.KeyChar) == 46))                        
                    {                             
                        e.Handled = true;                        
                    }                    
                }                
            }            
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["bullet_qty"].Index)                
            {
                dataGridView2.Rows[e.RowIndex].ErrorText = "";                    
                int NewVal=0;                     
                if (!int.TryParse (e.FormattedValue.ToString (),out NewVal ) || NewVal <0)                    
                {                         
                    e.Cancel=true;
                    return;
                }
            } 
        }
        int apply_bullet_qty = 0;
        Process myProcess;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCellAddress.X == 2) // 判断当前列是不是要控制的列 我是控制的索引值为2的  列(即第三列)                 
            {
                myProcess = Process.Start("osk.exe");
            }
            try
            {
                apply_bullet_qty = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch { }
        }
        
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                myProcess.CloseMainWindow();
            }
            catch { }
            if (int.Parse(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > apply_bullet_qty)
            {
                MessageBox.Show("还弹数量大于取弹数量！");
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = apply_bullet_qty;
                apply_bullet_qty = 0;
            }
        }
        #endregion

        
    }
}
