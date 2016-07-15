using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyUI.Utils
{
    public class Port
    {
        /// <summary>
        /// 获取枪COM
        /// </summary>
        /// <returns></returns>
        public static int Get_ComGun()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["com_gun"].Substring(3, 1));
        }
        /// <summary>
        /// 获取弹COM
        /// </summary>
        /// <returns></returns>
        public static int Get_ComBullet()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["com_bullet"].Substring(3, 1));
        }
        /// <summary>
        /// 获取指纹COM
        /// </summary>
        /// <returns></returns>
        public static int Get_ComFinger()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["com_finger"].Substring(3, 1));
        }
        /// <summary>
        /// 获取短信猫COM
        /// </summary>
        /// <returns></returns>
        public static int Get_ComMessage()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["com_message"].Substring(3, 1));
        }
        /// <summary>
        /// 获取酒精COM
        /// </summary>
        /// <returns></returns>
        public static int Get_ComDrink()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["com_drink"].Substring(3, 1));
        }
    }
}
