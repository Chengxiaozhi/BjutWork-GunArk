using System;
using System.Collections.Generic;
using System.Text;
namespace MyUI.Utils
{
    public class CheckGun
    {
        /// <summary>
        /// 检测对比枪位状态
        /// </summary>
        /// <param name="gun_number">取枪（还枪）集合</param>
        /// <returns></returns>
        public static bool Check(List<string> gun_number)
        {
            //这个集合用于存放异常枪支的枪位号
            List<string> error_gun_number = new List<string>();
            //inner集合为枪柜中的实时在位枪支，判断在位枪支中若存在应取出的枪支，则加入异常枪位集合
            for (int i = 0; i < gun_number.Count; i++)
            {
                if (PubFlag.inner.Contains(gun_number[i]))
                {
                    error_gun_number.Add(gun_number[i]);
                }
            }
            //如果异常枪位集合中有元素，则枪柜中的枪位是异常状态
            if (error_gun_number.Count > 0)
                return false;
            return true;
        }
    }
}
