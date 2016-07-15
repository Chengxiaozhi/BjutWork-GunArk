using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace MyUI.Utils
{
    public class SettingIp
    {
        #region 设置本机IP地址
        /// <summary>
        /// 设置本机IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="submask"></param>
        /// <param name="getway"></param>
        public static void SetIPAddress(string ip, string subNet, string gatWay)
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!(bool)mo["IPEnabled"])
                    continue;
                //设置ip地址和子网掩码
                inPar = mo.GetMethodParameters("EnableStatic");
                inPar["IPAddress"] = new string[] { ip };// 1.备用 2.IP
                if(!subNet.Equals(""))
                    inPar["SubnetMask"] = new string[] { subNet };
                outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                //设置网关地址
                if (!gatWay.Equals(""))
                {
                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = new string[] { gatWay }; // 1.网关;2.备用网关
                    outPar = mo.InvokeMethod("SetGateways", inPar, null);
                }
                ////设置DNS
                //inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                //string dns1 = numericUpDown16.Value.ToString() + "." + numericUpDown15.Value.ToString() + "." + numericUpDown14.Value.ToString() + "." + numericUpDown13.Value.ToString();
                //string dns2 = numericUpDown20.Value.ToString() + "." + numericUpDown19.Value.ToString() + "." + numericUpDown18.Value.ToString() + "." + numericUpDown17.Value.ToString();
                //inPar["DNSServerSearchOrder"] = new string[] { dns1, dns2 }; // 1.DNS 2.备用DNS
                //outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                break;
            }

            System.Windows.Forms.MessageBox.Show("修改成功！！！");

        }
        #endregion 设置本机IP地址
    }
}
