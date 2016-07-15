using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyUI.Utils
{
    public class SynInfo
    {
        public static void getSynInfo()
        {
            //WebService接口调用工具
            WebService.gunServices webService = SingleWebService.getWebService();
            try
            {
                WebService.synInfo[] syn_info_list = webService.getSynInfo(ConfigurationManager.AppSettings["gunark_ip"]);


                for (int i = 0; i < syn_info_list.Length; i++)
                {
                    try
                    {
                        //System.Windows.Forms.MessageBox.Show(syn_info_list[i].gunArk.gunarkId);   
                        Analysis.analysis(syn_info_list[i]);
                        webService.setSynInfoSuccess(syn_info_list[i].syn_Id);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }
            catch (Exception e1) { System.Windows.Forms.MessageBox.Show("请检查网络连接！"); }
        }
    }
}
