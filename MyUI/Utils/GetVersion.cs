using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace MyUI.Utils
{
    public class GetVersion
    {
        /// <summary>
        /// 获取软件版本号
        /// </summary>
        /// <returns></returns>
        public static string getSoftVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            AssemblyProductAttribute asmdis = (AssemblyProductAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyProductAttribute));
            string versionStr = string.Format("{0}", fvi.ProductVersion, asmdis.Product);
            return versionStr;
        }
    }
}
