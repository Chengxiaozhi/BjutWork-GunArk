using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace MyUI.Utils
{
    public class ListToByte
    {
        /// <summary>
        /// 将list转为byte数组
        /// </summary>
        /// <param name="list">需要转换的list集合</param>
        /// <returns></returns>
        public static Byte[] List2Byte(List<string> list)
        {
            byte[] bytes = new byte[list.Count];
            for(int i=0;i<list.Count;i++)
            {
                bytes[i] = (byte)int.Parse(list[i]);
            }
            return bytes;
        }
    }
}
