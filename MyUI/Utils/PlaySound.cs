using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using System.Configuration;

namespace MyUI.Utils
{
    public class PlaySound
    {
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="stream">文件流对象</param>
        public static void paly(System.IO.Stream stream)
        {
            //如果语音功能已开启
            if(bool.Parse(ConfigurationManager.AppSettings["set_sound"]))
                new SoundPlayer(stream).PlaySync();
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        /// <param name="path"></param>
        public static void paly(string path)
        {
            //如果语音功能已开启
            if (bool.Parse(ConfigurationManager.AppSettings["set_sound"]))
                new SoundPlayer(path).PlaySync();
        }
    }
}
