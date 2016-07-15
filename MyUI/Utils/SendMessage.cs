using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class SendMessage
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <param name="userName">申请用枪人</param>
        public static void send(string phone, string userName)
        {
            GSMModem gm = new GSMModem("COM" + Port.Get_ComMessage(), 9600);
            gm.Open();
            gm.SendMsg(phone, "申请用枪人： " + userName + " | 短信验证码为：" + new System.Random().Next(0,9999) + " | 请妥善保管！" );
            gm.Close();
        }
    }
}
