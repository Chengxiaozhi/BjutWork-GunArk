using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class AlcoholInstance
    {
        private volatile static Alcohol _instance = null;
        private static readonly object lockHelper = new object();
        public AlcoholInstance() { }
        /// <summary>
        /// 获取WebService单例
        /// </summary>
        /// <returns></returns>
        public static Alcohol getAlcoholInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new Alcohol();
                    }
                }
            }
            return _instance;  
        }
    }
}
