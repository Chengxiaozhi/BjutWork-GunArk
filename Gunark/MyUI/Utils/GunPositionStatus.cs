using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyUI.Utils
{
    public class GunPositionStatus
    {
        /// <summary>
        /// 插入mark
        /// </summary>
        /// <param name="mark"></param>
        public static void Insert_Mark(string mark)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["mark"].Value = mark;
            cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// 插入enable
        /// </summary>
        /// <param name="enable"></param>
        public static void Insert_Enable(string enable)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["enable"].Value = enable;
            cfa.Save(); ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        ///获取mark
        /// </summary>
        /// <returns></returns>
        public static string Get_Mark()
        {
            return ConfigurationManager.AppSettings["mark"];
        }
        /// <summary>
        /// 获取enable
        /// </summary>
        /// <returns></returns>
        public static string Get_Enable()
        {
            return ConfigurationManager.AppSettings["enable"];
        }
    }
}
