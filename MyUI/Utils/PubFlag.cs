using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyUI.Utils
{
    /// <summary>
    /// 在这个类中存放一些全局变量
    /// </summary>
    public class PubFlag
    {
        /// <summary>
        /// 观察者模式
        /// </summary>
        public static MyObserver myObserver = new MyObserver();
        /// <summary>
        /// 是否通过酒精检测
        /// </summary>
        public static bool isPassDrink = true;
        /// <summary>
        /// 验指纹的用枪警员ID
        /// </summary>
        public static string policeNum = "40287c8655e56acc0155e56ee3fa000d";
        /// <summary>
        /// 验指纹的当班领导ID
        /// </summary>
        public static string dutyLeaderNum = "40287c8655e56acc0155e56d9545000b";
        /// <summary>
        /// 验证指纹的主管领导ID
        /// </summary>
        public static string bossLeaderNum = "40287c8655e56acc0155e56c9284000a";
        /// <summary>
        /// 验证指纹的枪柜管理员ID
        /// </summary>
        public static string gunarkAdminNum = "";
        /// <summary>
        /// 标识控制柜子的线程是否应该销毁
        /// </summary>
        public static bool gunark_control_isAile = true;
        /// <summary>
        /// 正在进行的任务类型
        /// </summary>
        public static string task_type = "";
        /// <summary>
        /// 在位枪支
        /// </summary>
        public static List<string> inner = new List<string>();
        /// <summary>
        ///检测与远程服务器联网状态
        /// </summary>
        public static bool online = false;
        /// <summary>
        /// 用枪警员指纹
        /// </summary>
        public static Dictionary<string, string> policeFinger = new Dictionary<string, string>();
        /// <summary>
        /// 当班领导指纹
        /// </summary>
        public static Dictionary<string, string> dutyLeaderFinger = new Dictionary<string, string>();
        /// <summary>
        /// 主管领导指纹
        /// </summary>
        public static Dictionary<string, string> bossLeaderFinger = new Dictionary<string, string>();
    }
}
