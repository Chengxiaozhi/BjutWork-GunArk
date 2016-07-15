using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyUI.Utils
{
    public class SingleWebService
    {
        private volatile static WebService.gunServices _instance = null;
        private static readonly object lockHelper = new object();
        private SingleWebService() { }
        /// <summary>
        /// 获取WebService单例
        /// </summary>
        /// <returns></returns>
        public static WebService.gunServices getWebService()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new MyUI.WebService.gunServices();
                        _instance.Url = "http://" + ConfigurationManager.AppSettings["service_ip"] + ":8080/ssh/services/gunServices?wsdl";
                        //_instance.Url = "http://172.18.1.132:8080/ssh/services/gunServices?wsdl";
                    }
                }
            }
            return _instance;  
        }
    }
}
