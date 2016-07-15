using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using Bll = Gunark.BLL;

namespace MyUI.Utils
{
    public class InputFinger
    {
        public static void input(Model.fingerprint fingerprint, byte[] finger)
        {
            //实例化指纹验证对象
            Printer p = PrinterInstance.getInstance().getPrinter();
            Bll.fingerprint fingerPrint_bll = new Gunark.BLL.fingerprint();
            //p.OpenPort();
            int userId = fingerprint.ID;
            //下载用户
            //System.Windows.Forms.MessageBox.Show("开始下载用户");
            p.downloadUser((byte)userId);
            while (true)
            {
                if (p.Downuserbool)
                {
                    p.Downuserbool = false;
                    System.Windows.Forms.MessageBox.Show("用户下载成功");
                    break;
                }
                if (!p.DownuserFail)
                {
                    System.Windows.Forms.MessageBox.Show("用户下载失败");
                    p.DownuserFail = true;
                    break;
                }
            }
            //System.Threading.Thread.Sleep(5000);
            //if (p.GetDownuserbool())
            //{
            //    p.Downuserbool = false;
            //    System.Windows.Forms.MessageBox.Show("用户下载成功");
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("用户下载失败");
            //}
            //下载指纹
            //System.Windows.Forms.MessageBox.Show("开始下载用户");
            p.downloadfinger(finger, (byte)userId);
            while (true)
            {
                if (p.Downprinterbool)
                {
                    p.Downprinterbool = false;
                    System.Windows.Forms.MessageBox.Show("指纹录入成功！");
                    break;
                }
                if(!p.DownprinterFail)
                {
                    p.DownprinterFail = true;
                    System.Windows.Forms.MessageBox.Show("指纹录入失败");
                    break;
                }
            }

            
        }
    }
}
