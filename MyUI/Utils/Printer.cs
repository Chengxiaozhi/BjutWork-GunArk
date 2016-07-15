using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyUI.Utils
{
    public class Printer
    {
        private SerialPort comm = new SerialPort();
        private bool downuserbool = false;

        public bool Downuserbool
        {
            get { return downuserbool; }
            set { downuserbool = value; }
        }
        private bool downprinterbool = false;

        public bool Downprinterbool
        {
            get { return downprinterbool; }
            set { downprinterbool = value; }
        }
        private bool inputFingerbool = false;

        public bool InputFingerbool
        {
            get { return inputFingerbool; }
            set { inputFingerbool = value; }
        }
        public string data = "";
        public string ResultFg = "";
        public string buffer_fg = "";
        public bool OpenDoor = false;
        private bool inputFail = true;

        public bool InputFail
        {
            get { return inputFail; }
            set { inputFail = value; }
        }
        private bool downprinterFail = true;

        public bool DownprinterFail
        {
            get { return downprinterFail; }
            set { downprinterFail = value; }
        }
        private bool downuserFail = true;

        public bool DownuserFail
        {
            get { return downuserFail; }
            set { downuserFail = value; }
        }

        public static string state;
        public byte[] init = new byte[15];
        public byte[] delete = new byte[15];
        public byte[] downUser;
        public byte[] check = new byte[12];
        public byte[] register = new byte[12];
        public byte[] getRegFinger = new byte[1405];
        public byte[] confirmopen = new byte[17];
        public byte[] getdownfinger = new byte[1616];
        private long received_count = 0;//接收计数
        private bool Listening = false;//是否没有执行完invoke相关操作
        private bool Closing = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        private List<byte> buffer = new List<byte>(4096);//默认分配1页内存，并始终限制不允许超过
        private byte[] binary_data_1 = new byte[2000];//AA 44 05 01 02 03 04 05 EA

        private StringBuilder builder = new StringBuilder();

        public bool IsOpen    /// 端口是否已经打开
        {
            get
            {
                return comm.IsOpen;
            }
        }


        /// 检查端口名称是否存在
        /// <param name="port_name"></param>
        /// <returns></returns>
        public static bool Exists(string port_name)
        {
            foreach (string port in SerialPort.GetPortNames()) if (port == port_name) return true;
            return false;
        }

        /// </summary>
        public void DiscardBuffer()/// 丢弃来自串行驱动程序的接收和发送缓冲区的数据
        {
            comm.DiscardInBuffer();
            comm.DiscardOutBuffer();
        }

        public void OpenPort()//打开串口
        {
            if (comm.IsOpen) comm.Close();
            System.Threading.Thread.Sleep(1000);
            comm.PortName = "COM"+Port.Get_ComFinger();
            comm.BaudRate = 9600;
            comm.DataBits = 8;
            comm.StopBits = StopBits.One;
            try
            {
                comm.Open();
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                comm = new SerialPort();
                //现实异常信息给客户。
               
            }
            if (comm.IsOpen)
            {

                string name = comm.PortName;
                int baudRate = comm.BaudRate;
                state = name + "串口已经正常打开.  " + "波特率: " + baudRate;
            }
            comm.DataReceived += comm_DataReceived;
        }


        public void ClosePort()//关闭串口
        {
            if (comm.IsOpen)
            {
                Closing = true;
                while (Listening) Application.DoEvents();
                comm.Close();
            }
        }

        public void send_message(char[] bs)//下发指令
        {
            int n = bs.Length;
            comm.Write(bs, 0, n);
        }


        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
      
            if (Closing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            System.Threading.Thread.Sleep(2000);
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                comm.Read(buf, 0, n);//读取缓冲数据

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("读取缓冲数据:");
                for (int i = 0; i < buf.Length; i++)
                {
                    sb.AppendLine("buf[" + i + "]=0x" + Convert.ToString(buf[i], 16));
                }
          

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //<协议解析>
                bool data_1_catched = false;//缓存记录数据是否捕获到
                //1.缓存数据
                buffer.AddRange(buf);
                //2.完整性判断
                while (buffer.Count >= 4)//至少要包含头（2字节）+长度（1字节）+校验（1字节）
                {
                    //请不要担心使用>=，因为>=已经和>,<,=一样，是独立操作符，并不是解析成>和=2个符号
                    //2.1 查找数据头
                    if (buffer[0] == 0x1b)
                    {
                        //2.2 探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                        //前面已经限定了剩余长度>=4，那我们这里一定能访问到buffer[2]这个长度
                        int len = buffer.Count;//数据长度

                        if (buffer.Count < 12) break;//数据不够的时候什么都不做
                        byte checksum = 0;
                        for (int i = 0; i < 11; i++)//len+3表示校验之前的位置
                        {
                            checksum += buffer[i];
                        }
                        if (checksum != buffer[11]) //如果数据校验失败，丢弃这一包数据
                        {
                            buffer.RemoveRange(0, len);//从缓存中删除错误数据
                            MessageBox.Show("数据不正确");
                            continue;//继续下一次循环
                        }
                        //至此，已经被找到了一条完整数据。

                        buffer.CopyTo(0, binary_data_1, 0, len);//复制一条完整数据到具体的数据缓存
                        data_1_catched = true;
                        buffer.RemoveRange(0, len);//正确分析一条数据，从缓存中移除数据。
                    }
                    else
                    {
                        //这里是很重要的，如果数据开始不是头，则删除数据
                        buffer.RemoveAt(0);
                    }
                }

                StringBuilder sb1 = new StringBuilder();
                sb1.AppendLine("完整数据缓存:");
                for (int i = 0; i < binary_data_1.Length; i++)
                {
                    sb1.AppendLine("binary_data_1[" + i + "]=0x" + Convert.ToString(binary_data_1[i], 16));
                }
              

                //分析数据
                if (data_1_catched)
                {
                    //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了

                    if (data_1_catched)
                    {
                        byte[] ttt = binary_data_1;
                        for (int i = 0; i < ttt.Length; i++)
                        {
                            data += string.Format("{0:x2}", ttt[i]);
                        }
                    }
                    if (binary_data_1[7] == 0x50)
                    {
                        buffer_fg += string.Format("{0:D}", binary_data_1[12]);//+ string.Format("{0:D}", binary_data_1[13] * 16 * 16);
                        OpenDoor = true;
                        ResultFg = buffer_fg;
                        // MessageBox.Show(ResultFg);
                        buffer_fg = "";

                    }
                    else if (binary_data_1[7] == 0x51)
                    {
                        ResultFg += "wrong";
                        OpenDoor = true;
                        //MessageBox.Show(ResultFg);
                    }
                    else if (binary_data_1[7] == 0x05 )
                    {
                        //MessageBox.Show("指纹下载成功，哈哈哈");
                        if (binary_data_1[10] == 0x00)
                        {
                            downprinterbool = true;
                        }
                        else
                        {
                            downprinterFail = false;
                        }
                    }
                    else if (binary_data_1[7] == 0x04 )
                    {
                        if (binary_data_1[10] == 0x00)
                            downuserbool = true;
                        else
                            downuserFail = false;
                        //MessageBox.Show("用户下载成功，哈哈哈");
                    }

                    else if (binary_data_1[7] == 0x09)//获取指纹特征值
                    {
                        if (binary_data_1[10] == 0x00)
                        {
                            inputFingerbool = true;
                            InputFail = true;
                            for (int i = 12, j = 0; i < 1417; i++, j++)
                            {
                                getRegFinger[j] = binary_data_1[i];
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("error");
                            inputFail = false;
                        }
                        
                    }




                    //如果需要别的协议，只要扩展这个data_n_catched就可以了。往往我们协议多的情况下，还会包含数据编号，给来的数据进行
                    //编号，协议优化后就是： 头+编号+长度+数据+校验
                    //</协议解析>
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //builder.Length = 0;//清除字符串构造器的内容
                    //因为要访问ui资源，所以需要使用invoke方式同步ui。
                }
            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。
            }
        }



        public char convert(byte number, bool high) // 十六进制 高、低四位转为ASCII
        {

            char[] list = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            if (high)
            {
                return list[number >> 4];// 高四位
            }
            else
            {
                return list[number & 0x0f];//低四位
            }

        }




        public void fingerInit()//初始化
        {
            init[0] = 0x1b;//帧头
            init[1] = 0x46;
            init[2] = 0x44;
            init[3] = 0x41;

            init[4] = 0x01;
            init[5] = 0x00;
            init[6] = 0x00;//地址

            init[7] = 0x02;//命令号
            init[8] = 0x03;
            init[9] = 0x00;
            init[10] = 0x00;
            // package[11] = 0xec;
            init[12] = 0x01;
            init[13] = 0x01;
            init[14] = 0x02;

            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                if (i >= init.Length) { break; }
                sum += init[i];
            }
            init[11] = sum;//和校验

            //解析十进制数据，获取bit位数据 

            comm.Write(init, 0, init.Length);
        }

        public void checkUser()//验证身份
        {
            check[0] = 0x1b;//帧头
            check[1] = 0x46;
            check[2] = 0x44;
            check[3] = 0x41;

            check[4] = 0x01;
            check[5] = 0x00;
            check[6] = 0x00;//地址

            check[7] = 0x11;//命令号

            check[8] = 0x00;
            check[9] = 0x00;
            check[10] = 0x00;


            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                if (i >= check.Length) { break; }
                sum += check[i];
            }
            check[11] = sum;//和校验

            //解析十进制数据，获取bit位数据 

            comm.Write(check, 0, check.Length);
        }



        public void confirmOpen(byte[] user_ID)//确认开门
        {
            confirmopen[0] = 0x1b;//帧头
            confirmopen[1] = 0x46;
            confirmopen[2] = 0x44;
            confirmopen[3] = 0x41;

            confirmopen[4] = 0x01;
            confirmopen[5] = 0x00;
            confirmopen[6] = 0x00;//地址

            confirmopen[7] = 0x50;//命令号

            confirmopen[8] = 0x05;
            confirmopen[9] = 0x00;
            confirmopen[10] = 0x00;


            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                if (i >= confirmopen.Length) { break; }
                sum += confirmopen[i];
            }
            confirmopen[11] = sum;//和校验

            int j = 0;

            for (int i = 12; i < 14; i++)
            {
                confirmopen[i] = user_ID[j];
                j++;

            }

            confirmopen[14] = 0x00;
            confirmopen[15] = 0x00;
            byte sum1 = 0;
            for (int i = 12; i < 16; i++)
            {
                if (i >= confirmopen.Length) { break; }
                sum1 += confirmopen[i];
            }
            confirmopen[16] = sum1;

            //解析十进制数据，获取bit位数据 

            comm.Write(confirmopen, 0, confirmopen.Length);
        }

        public void deleteUser(byte[] number)//删除用户
        {


            ///   "、、x0A\x03\x00\x00\xf4\x00\x01\x01


            delete[0] = 0x1b;//帧头
            delete[1] = 0x46;
            delete[2] = 0x44;
            delete[3] = 0x41;

            delete[4] = 0x01;
            delete[5] = 0x00;
            delete[6] = 0x00;//地址

            delete[7] = 0x0A;//命令号

            delete[8] = 0x03;
            delete[9] = 0x00;
            delete[10] = 0x00;

            //delete[12] = 0x00;
            //delete[13] = 0x01;
            //delete[14] = 0x01;
            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                if (i >= delete.Length) { break; }
                sum += delete[i];
            }
            delete[11] = sum;//和校验

            delete[12] = number[0];
            delete[13] = number[1];

            byte extra = 0;
            for (int i = 12; i < 14; i++)
            {
                if (i >= delete.Length) { break; }
                extra += delete[i];
            }
            delete[14] = extra;
            comm.Write(delete, 0, delete.Length);
        }

        public void downloadUser(byte UserID)//下传拥护
        {

            //("\x1b\x46\x44\x41\x01\x00\x00\x09\x00\x00\x00\xf0")
            ///   "、、x0A\x03\x00\x00\xf4\x00\x01\x01
            //byte[] package = new byte[27];

            //package[0] = 0x1b;//帧头
            //package[1] = 0x46;
            //package[2] = 0x44;
            //package[3] = 0x41;

            //package[4] = 0x01;
            //package[5] = 0x00;
            //package[6] = 0x00;//地址

            //package[7] = 0x04;//命令号

            //package[8] = 0x0f;
            //package[9] = 0x00;
            //package[10] = 0x00;

            //package[12] = UserID;
            //package[13] = 0x00;//UserID

            //package[14] = 0x00;
            //package[15] = 0x00;
            //package[16] = 0x00;
            //package[17] = 0x00;
            //package[18] = 0x00;
            //package[19] = 0x00;
            //package[20] = 0x00;
            //package[21] = 0x00;
            //package[22] = 0x00;
            //package[23] = 0x00;
            //package[24] = 0x00;
            //package[25] = 0x01;


            //byte sum = 0;
            //for (int i = 0; i < 11; i++)
            //{
            //    if (i >= package.Length) { break; }
            //    sum += package[i];
            //}
            //package[11] = sum;//和校验

            //byte sum1 = 0;
            //for (int inter = 12; inter < 26; inter++)
            //    for (int i = 0; i < 11; i++)
            //    {
            //        if (i >= package.Length) { break; }
            //        sum1 += package[i];
            //    }
            //package[26] = sum1;

            //downUser = package;

            ////解析十进制数据，获取bit位数据 
            //downuserbool = false;
            //comm.Write(package, 0, package.Length);
            byte[] package = new byte[28];

            package[0] = 0x1b;//帧头'a'
            package[1] = 0x46;
            package[2] = 0x44;
            package[3] = 0x41;

            package[4] = 0x01;
            package[5] = 0x00;
            package[6] = 0x00;//地址

            package[7] = 0x04;//命令号

            package[8] = 0x10;
            package[9] = 0x00;
            package[10] = 0x00;

            package[12] = UserID;
            package[13] = 0x00;//UserID

            package[14] = 0x00;
            package[15] = 0x00;
            package[16] = 0x00;
            package[17] = 0x00;
            package[18] = 0x00;
            package[19] = 0x00;
            package[20] = 0x00;
            package[21] = 0x00;
            package[22] = 0x00;
            package[23] = 0x00;
            package[24] = 0x00;
            package[25] = 0x01;
            package[26] = 0x00;
            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                //if (i >= package.Length) { break; }
                sum += package[i];
            }
            package[11] = sum;//和校验

            byte sum1 = 0;
            for (int inter = 12; inter < 27; inter++)
            //for (int i = 0; i < 11; i++)
            {
                //if (i >= package.Length) { break; }
                sum1 += package[inter];
            }
            package[27] = sum1;

            downUser = package;

            //解析十进制数据，获取bit位数据 
            downuserbool = false;
            comm.Write(package, 0, package.Length);
        }

        public void downloadfinger(byte[] printer, byte UserID)
        {

            //("\x1b\x46\x44\x41\x01\x00\x00\x09\x00\x00\x00\xf0")
            ///   "、、x0A\x03\x00\x00\xf4\x00\x01\x01
            //byte[] package = new byte[1616];

            //package[0] = 0x1b;//帧头
            //package[1] = 0x46;
            //package[2] = 0x44;
            //package[3] = 0x41;

            //package[4] = 0x01;
            //package[5] = 0x00;
            //package[6] = 0x00;//地址

            //package[7] = 0x05;//命令号

            //package[8] = 0x44;
            //package[9] = 0x06;
            //package[10] = 0x00;

            //package[12] = UserID;
            //package[13] = 0x00;//UserID

            //package[14] = 0x03;

            //// package[15] -package[1614]



            //byte sum = 0;
            //for (int i = 0; i < 11; i++)
            //{
            //    if (i >= package.Length) { break; }
            //    sum += package[i];
            //}
            //package[11] = sum;//和校验

            ////byte[] printer = StrToByte(finger);
            //int j = 0;
            //for (int i = 15; i < 1615; i++)
            //{
            //    package[i] = printer[j];
            //    j = j + 1;
            //}
            //byte sum1 = 0;
            //for (int inter = 12; inter < 1615; inter++)
            //    for (int i = 0; i < 11; i++)
            //    {
            //        if (i >= package.Length) { break; }
            //        sum1 += package[i];
            //    }
            //package[1615] = sum1;

            //getdownfinger = package;
            ////解析十进制数据，获取bit位数据 
            //downprinterbool = false;

            //comm.Write(package, 0, package.Length);
            byte[] package = new byte[1420];

            package[0] = 0x1b;//帧头
            package[1] = 0x46;
            package[2] = 0x44;
            package[3] = 0x41;

            package[4] = 0x01;
            package[5] = 0x00;
            package[6] = 0x00;//地址

            package[7] = 0x05;//命令号

            package[8] = 0x80;
            package[9] = 0x05;
            package[10] = 0x00;

            package[12] = UserID;

            package[13] = 0x00;//UserID

            package[14] = 0x03;

            // package[15] -package[1614]



            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                //if (i >= package.Length) { break; }
                sum += package[i];

            }

            package[11] = Convert.ToByte(sum % 0x100);//和校验

            //byte[] printer = StrToByte(finger);
            int j = 0;
            for (int i = 15; i < 1419; i++)
            {
                package[i] = printer[j];
                j = j + 1;
            }

            byte sum1 = 0;
            for (int inter = 12; inter < 1419; inter++)
            //for (int i = 0; i < 11; i++)
            {
                //if (i >= package.Length) { break; }
                sum1 += package[inter];
            }
            package[1419] = Convert.ToByte(sum1 % 0x100);


            getdownfinger = package;
            //解析十进制数据，获取bit位数据 
            downprinterbool = false;

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 20; i++)
            //{
            //    sb.AppendLine("package[" + i + "]= 0x" + Convert.ToString(package[i], 16) + ";");
            //}


            comm.Write(package, 0, package.Length);

        }

        public void registerPrinter()
        {
            register[0] = 0x1b;//帧头
            register[1] = 0x46;
            register[2] = 0x44;
            register[3] = 0x41;

            register[4] = 0x01;
            register[5] = 0x00;
            register[6] = 0x00;//地址

            register[7] = 0x09;//命令号
            register[8] = 0x00;
            register[9] = 0x00;
            register[10] = 0x00;
            // package[11] = 0xec;

            byte sum = 0;
            for (int i = 0; i < 11; i++)
            {
                if (i >= register.Length) { break; }
                sum += register[i];
            }
            register[11] = sum;//和校验

            //解析十进制数据，获取bit位数据 
            //MessageBox.Show(System.Text.Encoding.Default.GetString(register));
            comm.Write(register, 0, register.Length);

        }


        public byte[] StrToByte(string finger)
        {
            byte[] fingerByte = new byte[1600];
            string[] s = finger.Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                //if (!String.IsNullOrEmpty(s[i]))
                fingerByte[i] = Convert.ToByte(s[i], 16);
            }


            return fingerByte;
        }
        public byte[] getRegisterFinger()//返回指纹特征值
        {
            return this.getRegFinger;
        }
        public string getData()
        {
            return this.data;
        }
        public byte[] getCheck()
        {
            return this.check;
        }
        public byte[] getDownfinger()
        {
            return this.getdownfinger;
        }
        public byte[] getInit()
        {
            return this.init;
        }
        public byte[] getDelete()
        {
            return this.delete;
        }
        public byte[] getDownuser()
        {
            return this.downUser;
        }

        public byte[] Getconfirmopen()
        {
            return this.confirmopen;
        }
        public string GetResultFg()
        {
            return this.ResultFg;

        }
        public void cleanResult()
        {
            this.ResultFg = "";
        }
        public bool GetOpenDoor()
        {
            return this.OpenDoor;
        }
        public bool GetDownuserbool()
        {
            return this.downuserbool;
        }

        public bool GetInputUserBool()
        {
            return this.inputFingerbool;
        }

        public bool GetDownprinterbool()
        {
            return this.downprinterbool;
        }
    }
}
