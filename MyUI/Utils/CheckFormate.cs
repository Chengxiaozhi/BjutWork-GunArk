using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Globalization;

namespace MyUI.Utils
{
    public class CheckFormate
    {
        /// <summary>
        /// 判断是否是正确的ip地址
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static bool IsIpaddress(string ipaddress)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ipaddress, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 判断是否是正确的时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool Istime(string time)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(time, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$"); 
        }
    }
}
