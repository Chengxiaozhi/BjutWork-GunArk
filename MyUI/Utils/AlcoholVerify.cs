using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class AlcoholVerify
    {
        Alcohol alcohol = AlcoholInstance.getAlcoholInstance();
        private bool _continue = true;

        public bool _continue1
        {
            get { return _continue; }
            set { _continue = value; }
        }
        /// <summary>
        /// 酒精检验
        /// </summary>
        /// <returns></returns>
        public bool verifyAlcohol()
        {
            //清除上次的数据
            alcohol.Test_status = "";
            //验两次
            alcohol.testTwice();
            //检验结果
            string result = "";
            while (_continue)
            {
                result = alcohol.getTestStatus();
                if (result.Equals("W"))
                    continue;
                else if (result.Equals("er"))
                    return false;
                else if (result.Equals("ok"))
                    return true;
            }
            return true;
        }
    }
}
