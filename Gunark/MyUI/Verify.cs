using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyUI.Utils;
using Model = Gunark.Model;
using Bll = Gunark.BLL;
using System.Configuration;
/*----------------------------------------------------------------
 * Copyright (C) 2016 BJUT
 * 版权所有。 
 * 
 * 文件名： Verify.cs
 * 
 * 文件功能描述:实现多种身份验证
 *              
 * 身份验证种类：   
 *      1）取枪：验证短信->用枪警员指纹->酒精检测->当班领导指纹。flag = 1
 *      2）快速取枪、紧急取枪（用枪警员、当班领导提任务）：用枪警员指纹->酒精检测->当班领导指纹。flag = 2
 *      3）快速取枪、紧急取枪（主管领导审批）：主管领导验证指纹,（主管领导不能指纹审批时通过申请短信审批）。flag = 3；
 *      4）枪柜配置：枪柜管理员指纹。flag = 4
 *      5）其他常规任务：用枪警员指纹、当班领导指纹。flag = 5
 * 
**----------------------------------------------------------------*/
namespace MyUI
{
    public partial class Verify : Form
    {
        #region 属性
        /// <summary>
        /// 标识验证类型
        /// </summary>
        private int flag = 0;
        /// <summary>
        /// 控制while循环
        /// </summary>
        private bool _continue = true;
        /// <summary>
        /// 验证结果
        /// </summary>
        private bool result = false;
        /// <summary>
        /// 任务对象
        /// </summary>
        private Model.task_info task_info = new Gunark.Model.task_info();
        /// <summary>
        /// 用户业务逻辑实例
        /// </summary>
        private Bll.user user_bll = new Gunark.BLL.user();
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public bool _Continue
        {
            get { return _continue; }
            set { _continue = value; }
        }
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }
        public Model.task_info Task_info
        {
            get { return task_info; }
            set { task_info = value; }
        }
        #endregion 属性
        public Verify()
        {
            InitializeComponent();
        }

        private void Verify_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (flag)
            {
                case 1:
                    //如果有短信验证功能
                    if (bool.Parse(ConfigurationManager.AppSettings["set_message"]))
                    {
                        verify_message();
                    }
                    //验证用枪警员指纹
                    verify_police_finger();
                    //酒精检测
                    if (bool.Parse(ConfigurationManager.AppSettings["set_drink"]))
                    {
                        verify_alcohol();
                    }
                    //验证当班领导指纹
                    verify_leader_finger();
                    this.Close();
                    break;
                case 2:
                    //验证用枪警员指纹
                    verify_police_finger();
                    //酒精检测
                    if (bool.Parse(ConfigurationManager.AppSettings["set_drink"]))
                    {
                        verify_alcohol();
                    }
                    //验证当班领导指纹
                    verify_leader_finger();
                    this.Close();
                    break;
                case 3:
                    //主管领导指纹审批
                    verify_boss_finger();
                    this.Close();
                    break;
                case 4:
                    //枪柜管理员指纹
                    verify_gunarkAdmin();
                    this.Close();
                    break;
                case 5:
                    //用枪警员指纹
                    verify_police_finger();
                    //当班领导指纹
                    verify_leader_finger();
                    this.Close();
                    break;
                default:
                    break;
            }
        }
        #region 验证短信
        private void verify_message()
        {
            pictureBox1.Image = Properties.Resources.m3;
            pictureBox2.Image = Properties.Resources.messagepic0;
            message.Visible = true;
            //调用接口发送短信
            //SendMessage.send(user_bll.GetModel(task_info.TASK_APPROVAL_USERID).USER_MOBILTELEP, user_bll.GetModel(task_info.TASK_APPLY_USERID).USER_REALNAME);
            //验证短信
            while (_continue)
            {
                PlaySound.paly(Properties.Resources._24);
                //if (CheckMessage.check(message.Text.Trim()))
                if(true)
                {
                    pictureBox1.Image = Properties.Resources.m2;
                    pictureBox2.Image = Properties.Resources.messagepic;
                    PlaySound.paly(Properties.Resources._25);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.m1;
                    PlaySound.paly(Properties.Resources._35);
                }
                System.Threading.Thread.Sleep(2000);
            }
        }
        #endregion 验证短信
        #region 验证用枪警员指纹
        private void verify_police_finger()
        {
            pictureBox1.Image = Properties.Resources.f1;
            pictureBox2.Image = Properties.Resources.finger_check;
            message.Visible = false;
            FingerVerify fv_police = new FingerVerify("用枪警员");
            while (_continue)
            {
                //播放音效
                PlaySound.paly(Properties.Resources._4);
                //验证指纹
                //如果验证通过
                //if (fv_police.verifyFinger())
                if(true)
                {
                    pictureBox1.Image = Properties.Resources.f9;
                    //语音提示
                    PlaySound.paly(Properties.Resources._7);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.f2;
                    //语音提示
                    PlaySound.paly(Properties.Resources._33);
                }
            }
        }
        #endregion 验证指纹
        #region 酒精检测
        private void verify_alcohol()
        {
            pictureBox1.Image = Properties.Resources.a1;
            pictureBox2.Image = Properties.Resources.alcoholpic0;
            message.Visible = false;
            AlcoholVerify av = new AlcoholVerify();
            while (_continue)
            {
                //播放音效
                PlaySound.paly(Properties.Resources._1);
                //if(av.verifyAlcohol())
                if (true/*假设已通过检测*/)
                {
                    pictureBox1.Image = Properties.Resources.a2;
                    pictureBox2.Image = Properties.Resources.alcoholpic1;
                    //播放音效
                    PlaySound.paly(Properties.Resources._2);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.a3;
                    //播放音效
                    PlaySound.paly(Properties.Resources._3);
                }
            }
        }
        #endregion 酒精检测
        #region 验证当班领导指纹
        private void verify_leader_finger()
        {
            pictureBox1.Image = Properties.Resources.f3;
            pictureBox2.Image = Properties.Resources.finger_check;
            message.Visible = false;
            FingerVerify fv_dutyLeader = new FingerVerify("当班领导");
            //验证当班领导指纹具体方法
            while (_continue)
            {
                //语音提示
                PlaySound.paly(Properties.Resources._5);
                //验证指纹
                //if (fv_dutyLeader.verifyFinger())
                if (true)
                {
                    pictureBox1.Image = Properties.Resources.f9;
                    result = true;
                    //语音提示
                    PlaySound.paly(Properties.Resources._7);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.f6;
                    //语音提示
                    PlaySound.paly(Properties.Resources._34);
                }
            }
        }
        #endregion 验证当班领导指纹
        #region 主管领导验证指纹
        private void verify_boss_finger()
        {
            pictureBox1.Image = Properties.Resources.f5;
            pictureBox2.Image = Properties.Resources.finger_check;
            message.Visible = false;
            FingerVerify fv_bossLeader = new FingerVerify("主管领导");
            //验证主管领导指纹
            while (_continue)
            {
                //播放音效
                PlaySound.paly(Properties.Resources._28);
                //如果验证通过
                //if (fv_bossLeader.verifyFinger())
                if (true)
                {
                    result = true;
                    pictureBox1.Image = Properties.Resources.f9;
                    //语音提示
                    PlaySound.paly(Properties.Resources._7);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.f4;
                    //语音提示
                    PlaySound.paly(Properties.Resources._8);
                }
            }
        }
        #endregion 
        #region 枪柜管理员验证指纹
        private void verify_gunarkAdmin()
        {
            pictureBox1.Image = Properties.Resources.f7;
            pictureBox2.Image = Properties.Resources.finger_check;
            message.Visible = false;
            FingerVerify fv_gunarkAdmin = new FingerVerify("枪柜管理员");
            //验证枪柜枪员指纹
            while (_continue)
            {
                //播放音效
                PlaySound.paly(Properties.Resources._26);
                //如果验证通过(假设通过)
                //if (fv_gunarkAdmin.verifyFinger())
                if (true)
                {
                    result = true;
                    pictureBox1.Image = Properties.Resources.f9;
                    //语音提示
                    PlaySound.paly(Properties.Resources._7);
                    break;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.f8;
                    //语音提示
                    PlaySound.paly(Properties.Resources._32);
                }
            }
        }
        #endregion
        #region 验证结束后关闭验证线程
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Verify_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭循环
            _continue = false;
            backgroundWorker1.Dispose();
        }
        #endregion 验证结束后关闭验证线程

        private void message_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }
    }
}
