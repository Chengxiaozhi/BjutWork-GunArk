using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Bll = Gunark.BLL;
using Model = Gunark.Model;
using System.Media;

namespace MyUI.Utils
{
    public class FingerVerify
    {
        //表示指纹类型
        string role = "";
        //控制循环
        private bool _continue = true;

        public bool _continue1
        {
            get { return _continue; }
            set { _continue = value; }
        }
        //实例化指纹业务逻辑层对象
        private Bll.fingerprint finger_bll = new Bll.fingerprint();
        //实例化指纹验证对象
        Printer p = PrinterInstance.getInstance().getPrinter();
        public FingerVerify(string _role)
        {
            this.role = _role;
        }
        /// <summary>
        /// 指纹验证
        /// </summary>
        /// <param name="p"></param>
        public bool verifyFinger()
        {
            //p.OpenPort();
            p.cleanResult();
            p.checkUser();
            //p.ClosePort();
            //等待指纹机返回结果
            while(_continue)
            {
                if (p.GetResultFg().Equals(""))
                    continue;
                string id = p.GetResultFg();
                Model.fingerprint fingerPrint = finger_bll.GetModel(Convert.ToInt32(id));
                //MessageBox.Show(id.ToString());
                switch (role)
                {
                    case "用枪警员":
                        if (fingerPrint != null)
                        {
                            if (fingerPrint.USER_PRIVIEGES == 1)
                            {
                                PubFlag.policeNum = fingerPrint.USER_POLICENUMB;
                                return true;
                            }
                        }
                        break;
                    case "当班领导":
                        if (fingerPrint != null)
                        {
                            if (fingerPrint.USER_PRIVIEGES == 5)
                            {
                                PubFlag.dutyLeaderNum = fingerPrint.USER_POLICENUMB;
                                return true;
                            }
                        }
                        break;
                    case "主管领导":
                        if (fingerPrint != null)
                        {
                            if (fingerPrint.USER_PRIVIEGES == 6)
                            {
                                PubFlag.bossLeaderNum = fingerPrint.USER_POLICENUMB;
                                return true;
                            }
                        }
                        break;
                    case "枪柜管理员":
                        if (fingerPrint != null)
                        {
                            if (fingerPrint.USER_PRIVIEGES == 3 || fingerPrint.USER_PRIVIEGES == 4)
                            {
                                PubFlag.gunarkAdminNum = fingerPrint.USER_POLICENUMB;
                                return true;
                            }
                           
                        }
                        break;
                    default:
                        break;
                }
                //指纹仪中没有当前指纹信息
                if (p.GetResultFg().Equals("wrong"))
                {
                    System.Windows.Forms.MessageBox.Show("指纹不存在！","提示");
                }
                break;
            }
            return false;
        }
    }
}
