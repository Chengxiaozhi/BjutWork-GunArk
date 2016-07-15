using System;
using System.Collections.Generic;
using System.Text;
using Model = Gunark.Model;
using Bll = Gunark.BLL;

namespace MyUI.Utils
{
    class DownloadPrinter
    {
        
        public static void download(Model.fingerprint fingerprint)
        {
            //实例化指纹验证对象
            Printer p = PrinterInstance.getInstance().getPrinter();
            Bll.fingerprint fingerPrint_bll = new Gunark.BLL.fingerprint();
            //p.OpenPort();
            int userId = fingerprint.ID;
            string print = System.Text.Encoding.Default.GetString(fingerprint.USER_FINGERPRINT);
            string[] pr = print.Split(' ');

            byte[] finger = new byte[1600];
            for (int i = 0; i < pr.Length - 1; i++)
            {
                finger[i] = Convert.ToByte(pr[i], 16);
            }

            //System.Windows.Forms.MessageBox.Show(fingerByte.Length.ToString());

            //下载用户
            bool isDownloadUser = false;
            List<Model.fingerprint> list = fingerPrint_bll.GetModelList("USER_POLICENUMB = "+fingerprint.USER_POLICENUMB);
            foreach(Model.fingerprint fi in list)
            {
                if (fi.IS_UPDATE == 1)
                {
                    isDownloadUser = true;
                    break;
                }
            }
            if (!isDownloadUser)
            {
                p.downloadUser((byte)userId);
                //while (true)
                //{
                //    if (p.Downuserbool)
                //    {
                //        fingerprint.IS_UPDATE = 1;
                //        fingerPrint_bll.Update(fingerprint);
                //        p.Downuserbool = false;
                //        System.Windows.Forms.MessageBox.Show("用户下载成功");
                //        break;
                //    }
                //}
            }
            //下载指纹
            p.downloadfinger(finger, (byte)userId);
            //while (true)
            //{
            //    if (p.Downprinterbool)
            //    {
            //        System.Windows.Forms.MessageBox.Show("指纹下载成功！");
            //        break;
            //    }
            //}
        }
    }
}
