using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class CheckMessage
    {
        public static bool check(string message)
        { 
            //验证短信具体方法
            if (message.Equals("1234"))
                return true;
            return false;
        }
    }
}
