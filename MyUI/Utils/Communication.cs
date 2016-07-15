using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace MyUI.Utils
{
    public class Communication
    {
        public string data;
        private string gun_Data;
        private string dataValue;
        private char[] alarmCC = new char[8];
        private string temperature;
        private char numberHeader = '\x02';//数据头
        private char[] numberAddress = { '0', '0', '2', '0' };//数据地址
        private char[] numberFooter = { '\x03', '\x0D' };//数据尾
        private char[] numberCheck = { '0', '0' }; //校验和
        private char[] gunarkMark = { 'M', 'A', 'R', 'K' };//抢位标记
        private char[] lenGunMark = { '2', 'A' };
        private char[] gun_status = new char[32];
        private char[] store = new char[47];
        private char[] guanQM = new char[47];
        private char[] gunarkInit = new char[47];
        private char[] ret_gun;
        private char[] get_gun;
        private char[] gun_enable;
        private string checkbattery;
        public static string state;
        public static bool view = false;
        private SerialPort com_gun = new SerialPort();
        private SerialPort com_bullet = new SerialPort();
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private bool Listening = false;//是否没有执行完invoke相关操作
        private bool Closing = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        private List<byte> buffer = new List<byte>(4096);//默认分配1页内存，并始终限制不允许超过
        private byte[] binary_data_1 = new byte[300];//AA 44 05 01 02 03 04 05 EA
        public static char[] open = { '\x02', '\x30', '\x39', '\x30', '\x30', '\x32', '\x30', '\x4f', '\x50', '\x4e', '\x35', '\x41', '\x03', '\x0D' };//开门
        public static char[] close = { '\x02', '\x30', '\x39', '\x30', '\x32', '\x30', '\x30', '\x43', '\x4f', '\x4c', '\x34', '\x42', '\x03', '\x0D' };//关门
        public static char[] stata = { '\x02', '0', 'B', '0', '0', '2', '0', 'S', 'T', 'A', 'T', 'E', '2', '7', '\x03', '\x0D' };// 查询状态

        //DatabaseTx DB = new DatabaseTx();

        //fanhui 380020ST 00,01,E0,3F,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF,FF03
        public static byte[] fingerprint_INIT = { 0x1b, 0x46, 0x44, 0x41, 0x01, 0x00, 0x00, 0x02, 0x03, 0x00, 0x00, 0xec, 0x01, 0x01, 0x02 }; //指纹仪初始化


        //public bool IsOpen(int port)   /// 端口是否已经打开
        //{
        //    if (port == PubFlag.com_gun)
        //    {
        //        return comm1.IsOpen;
        //    }
        //    else 
        //    {
        //        return comm2.IsOpen;
        //    }
       
        //}

        /// 检查端口名称是否存在
        /// <param name="port_name"></param>
        /// <returns></returns>
        public static bool Exists(string port_name)
        {
            foreach (string port in SerialPort.GetPortNames()) 
                if (port == port_name) 
                    return true;
            return false;
        }

        /// </summary>
        public void DiscardBuffer(SerialPort comm)/// 丢弃来自串行驱动程序的接收和发送缓冲区的数据
        {
            comm.DiscardInBuffer();
            comm.DiscardOutBuffer();
        }

        public void OpenPort_gun(String portname)//打开串口
        {

            com_gun.PortName = portname;
            com_gun.BaudRate = 9600;
            com_gun.DataBits = 8;
            com_gun.Parity = Parity.Even;
            com_gun.StopBits = StopBits.One;
            try
            {
                com_gun.Open();
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                com_gun = new SerialPort();
                //现实异常信息给客户。
                MessageBox.Show(ex.Message);
            }
            //if (comm1.IsOpen)
            //{

            //    string name = comm1.PortName;
            //    int baudRate = comm1.BaudRate;
            //    state = name + "串口已经正常打开.  " + "波特率: " + baudRate;
            //    Console.WriteLine(state);
            //}
            com_gun.DataReceived += comm_DataReceived;
        }

        public void OpenPort_bullet(String portname)//打开串口
        {
            com_bullet.PortName = portname;
            com_bullet.BaudRate = 9600;
            com_bullet.DataBits = 8;
            com_bullet.Parity = Parity.Even;
            com_bullet.StopBits = StopBits.One;
            try
            {
                com_bullet.Open();
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                com_bullet = new SerialPort();
                //现实异常信息给客户。
                MessageBox.Show(ex.Message);
            }
            //if (comm1.IsOpen)
            //{

            //    string name = comm1.PortName;
            //    int baudRate = comm1.BaudRate;
            //    state = name + "串口已经正常打开.  " + "波特率: " + baudRate;
            //    Console.WriteLine(state);
            //}
            com_bullet.DataReceived += comm_DataReceived2;
        }
        
        public void ClosePort(SerialPort comm)//关闭串口
        {
            if (comm.IsOpen)
            {
                Closing = true;
                while (Listening) Application.DoEvents();
                comm.Close();
            }
        }

        public void send_message(int seriport, char[] bs)//下发指令
        {

            int n = bs.Length;

            if (seriport == Port.Get_ComGun())
            {
                com_gun.Write(bs, 0, n);
            }
            else if(seriport == Port.Get_ComBullet())
            {
                com_bullet.Write(bs, 0, n);
            }
        }
        //开枪位，用于待取枪支的指示，被标记的枪支在开柜后，将有一个LED指示器指示待取枪支的位置，枪支正常取出后，LED灯将消失
        public void open_gun(int port, byte[] gunarkNb)
        {
            char[] package = new char[47];
            char[] mark2 = null;
            // 100200MARK01000017
            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'M';//data
            package[8] = 'A';
            package[9] = 'R';
            package[10] = 'K';

            package[11] = '0';
            package[12] = '0';
            package[13] = '0';
            package[14] = '0';
            package[15] = '0';
            package[16] = '0';
            package[17] = '0';
            package[18] = '0';
            package[19] = '0';
            package[20] = '0';
            package[21] = '0';
            package[22] = '0';
            package[23] = '0';
            package[24] = '0';
            package[25] = '0';
            package[26] = '0';
            package[27] = '0';
            package[28] = '0';
            package[29] = '0';
            package[30] = '0';
            package[31] = '0';
            package[32] = '0';
            package[33] = '0';
            package[34] = '0';
            package[35] = '0';
            package[36] = '0';
            package[37] = '0';//the end of data
            package[38] = '0';
            package[39] = '0';
            package[40] = '0';
            package[41] = '0';
            package[42] = '0';
            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';

            if (port == Port.Get_ComGun())
            {
                mark2 = GunPositionStatus.Get_Mark().ToCharArray();//改
            }
            else
            {
                //mark2 = GunPositionStatus.Get_Mark().ToCharArray();//改
                //mark2 = DB.SelectDB_BULLET();
            }
            int inter1 = 11;
            for (int inter = 0; inter < mark2.Length; inter++)
            {
                package[inter1] = mark2[inter];
                inter1++;
            }

            byte[] numberTohex = new byte[400];//获取数组传入的枪柜使能选通消息
            //byte [] gunarkNb = { 1, 2, 3, 4 }
            for (int i = 0; i < gunarkNb.Length; i++)
            {
                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];
                //把选通的枪位号在八位二进制中代表的十进制数存入相应的位置
                //128--64--32--16--8--4--2--1分别对应八个枪位
                //gunarkENa()就是把枪位号转化为相应的128--64--32--16--8--4--2--1
                //然后把对应的值存到枪位号对应的数组里，数组下标比枪位号小1。
                numberTohex[number - 1] = gunarkENa(number);
            }
            bool high = true;
            for (int i = 11, j = 0; i < 43; i += 2, j += 8)
            {
                byte[] numberTohex8 = { numberTohex[j], numberTohex[j + 1], numberTohex[j + 2], numberTohex[j + 3], numberTohex[j + 4], numberTohex[j + 5], numberTohex[j + 6], numberTohex[j + 7] };
                byte gun_number8 = numberOr(numberTohex8);//通过求或运算，把置枪的位置（标记为1的地方）集合到一起
                byte pre11 = (byte)length2(package[i], package[i + 1]);//不知何意
                gun_number8 = (byte)(gun_number8 + pre11);
                package[i] = convert(gun_number8, high);// 十六进制高四位转为ASCII
                package[i + 1] = convert(gun_number8, !high);// 十六进制低四位转为ASCII
            }


            byte checksum = 0;
            int len = length2(package[1], package[2]);
            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);


            get_gun = package;
            int n = package.Length;
            if (port == Port.Get_ComGun())
            {
                com_gun.Write(package, 0, n);
                char[] mark_flag = new char[32];//用于存放32位标志位
                Array.Copy(package, 11, mark_flag, 0, 32);//把指令中的32位数据位赋值到目标数组
                string mark_Flag = new string(mark_flag);//转化为字符串
                GunPositionStatus.Insert_Mark(mark_Flag);//保存到配置文件
            }
            else
            {
                com_bullet.Write(package, 0, n);
            }
           
        }

        public void return_gun(int port,byte[] gunarkNb)//还枪位
        {
            char[] package = new char[47];
            char[] mark2 = new char[32];
            // 100200MARK01000017
            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'M';//data
            package[8] = 'A';
            package[9] = 'R';
            package[10] = 'K';

            package[11] = '1';
            package[12] = '1';
            package[13] = '1';
            package[14] = '1';
            package[15] = '1';
            package[16] = '1';
            package[17] = '1';
            package[18] = '1';
            package[19] = '1';
            package[20] = '1';
            package[21] = '1';
            package[22] = '1';
            package[23] = '1';
            package[24] = '1';
            package[25] = '1';
            package[26] = '1';
            package[27] = '1';
            package[28] = '1';
            package[29] = '1';
            package[30] = '1';
            package[31] = '1';
            package[32] = '1';
            package[33] = '1';
            package[34] = '1';
            package[35] = '1';
            package[36] = '1';
            package[37] = '1';//the end of data
            package[38] = '1';
            package[39] = '1';
            package[40] = '1';
            package[41] = '1';
            package[42] = '1';

            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';

            if (port == Port.Get_ComGun())
            {
                mark2 = GunPositionStatus.Get_Mark().ToCharArray();
            }
            else
            {
                //mark2 = DB.SelectDB_BULLET();
            }
           
            int inter1 = 11;
            for (int inter = 0; inter < mark2.Length; inter++)
            {

                package[inter1] = mark2[inter];
                inter1++;


            }

            byte[] numberTohex = new byte[400];

            for (int i = 0; i < gunarkNb.Length; i++)
            {


                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];

                numberTohex[number - 1] = gunarkENa(number);
            }

            bool high = true;
            for (int i = 11, j = 0; i < 43; i += 2, j += 8)
            {
                byte[] numberTohex8 = { numberTohex[j], numberTohex[j + 1], numberTohex[j + 2], numberTohex[j + 3], numberTohex[j + 4], numberTohex[j + 5], numberTohex[j + 6], numberTohex[j + 7] };
                byte gun_number8 = numberOr(numberTohex8);//通过求或运算，把置枪的位置（标记为1的地方）集合到一起
                byte pre11 = (byte)length2(package[i], package[i + 1]);//不知何意
                gun_number8 = (byte)(pre11 - gun_number8);
                package[i] = convert(gun_number8, high);// 十六进制高四位转为ASCII
                package[i + 1] = convert(gun_number8, !high);// 十六进制低四位转为ASCII
            }



            byte checksum = 0;
            int len = length2(package[1], package[2]);
            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }


            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);


            ret_gun = package;
            int n = package.Length;
            if (port == Port.Get_ComGun())
            {
                com_gun.Write(package, 0, n);
                char[] mark_flag = new char[32];//用于存放32位标志位
                Array.Copy(package, 11, mark_flag, 0, 32);//把指令中的32位数据位赋值到目标数组
                string mark_Flag = new string(mark_flag);//转化为字符串
                GunPositionStatus.Insert_Mark(mark_Flag);//保存到配置文件
            }
            else
            {
                com_bullet.Write(package, 0, n);
            }
            
        }

        public void enableBullet()//弹柜使能(未测试)
        {
            char[] package = new char[47];

            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'E';//data
            package[8] = 'N';
            package[9] = 'A';
            package[10] = 'B';

            package[11] = 'F';
            package[12] = 'F';
            package[13] = 'F';
            package[14] = 'F';
            package[15] = 'F';
            package[16] = 'F';
            package[17] = 'F';
            package[18] = 'F';
            package[19] = 'F';
            package[20] = 'F';
            package[21] = 'F';
            package[22] = 'F';
            package[23] = 'F';
            package[24] = 'F';
            package[25] = 'F';
            package[26] = 'F';
            package[27] = 'F';
            package[28] = 'F';
            package[29] = 'F';
            package[30] = 'F';
            package[31] = 'F';
            package[32] = 'F';
            package[33] = 'F';
            package[34] = 'F';
            package[35] = 'F';
            package[36] = 'F';
            package[37] = 'F';//the end of data
            package[38] = 'F';
            package[39] = 'F';
            package[40] = 'F';
            package[41] = 'F';
            package[42] = 'F';

            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';


            byte checksum = 0;
            int len = length2(package[1], package[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;


            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);
            //MessageBox.Show(new String(package));
            int n = package.Length;
            com_bullet.Write(package, 0, n);
            
        }

       /// <summary>
       ///  开弹仓
       /// </summary>
       /// <param name="gunarkNb"></param>
        public void open_Bullet( byte[] gunarkNb)//暂时不用
        {
            char[] package = new char[47];

            // 100200MARK01000017
            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'M';//data
            package[8] = 'A';
            package[9] = 'R';
            package[10] = 'K';

            package[11] = '0';
            package[12] = '0';
            package[13] = '0';
            package[14] = '0';
            package[15] = '0';
            package[16] = '0';
            package[17] = '0';
            package[18] = '0';
            package[19] = '0';
            package[20] = '0';
            package[21] = '0';
            package[22] = '0';
            package[23] = '0';
            package[24] = '0';
            package[25] = '0';
            package[26] = '0';
            package[27] = '0';
            package[28] = '0';
            package[29] = '0';
            package[30] = '0';
            package[31] = '0';
            package[32] = '0';
            package[33] = '0';
            package[34] = '0';
            package[35] = '0';
            package[36] = '0';
            package[37] = '0';//the end of data
            package[38] = '0';
            package[39] = '0';
            package[40] = '0';
            package[41] = '0';
            package[42] = '0';
            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';



            byte[] numberTohex = new byte[400];

            for (int i = 0; i < gunarkNb.Length; i++)
            {


                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];

                numberTohex[number - 1] = gunarkENa(number);
            }


            bool high = true;
            for (int i = 11, j = 0; i < 43; i += 2, j += 8)
            {
                byte[] numberTohex8 = { numberTohex[j], numberTohex[1 + j], numberTohex[j + 2], numberTohex[j + 3], numberTohex[j + 4], numberTohex[j + 5], numberTohex[j + 6], numberTohex[j + 7] };
                byte gun_number8 = numberOr(numberTohex8);//通过求或运算，把置枪的位置（标记为1的地方）集合到一起
                byte pre11 = (byte)length2(package[i], package[i + 1]);//不知何意
                gun_number8 = (byte)(gun_number8 + pre11);
                package[i] = convert(gun_number8, high);// 十六进制高四位转为ASCII
                package[i + 1] = convert(gun_number8, !high);// 十六进制低四位转为ASCII
            }

            byte checksum = 0;
            int len = length2(package[1], package[2]);
            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);


            get_gun = package;
            int n = package.Length;
            //MessageBox.Show(new String(package));
            com_bullet.Write(package, 0, n);
            
        }
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e) //
        {
            if (Closing) return; //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = com_gun.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                com_gun.Read(buf, 0, n);//读取缓冲数据

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //<协议解析>
                bool data_1_catched = false;//缓存记录数据是否捕获到
                bool data_2_catched = false;
                bool data_3_catched = false;
                bool data_4_catched = false;
                bool data_5_catched = false;
                bool data_6_catched = false;
                bool data_7_catched = false;
                //1.缓存数据
                buffer.AddRange(buf);
                //2.完整性判断
                while (buffer.Count >= 3)//至少要包含头（1字节）+长度（1字节）+校验（1字节）
                {
                    //请不要担心使用>=，因为>=已经和>,<,=一样，是独立操作符，并不是解析成>和=2个符号
                    //2.1 查找数据头
                    if (buffer[0] == 0x02)//控制板判断使用
                    {
                        //2.2 探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                        //前面已经限定了剩余长度>=4，那我们这里一定能访问到buffer[2]这个长度
                        //(buffer(1) - 48)*16 + (buffer[2]-55) 
                        //int len = (buffer[1] - 48) * 16 + (buffer[2] - 55);//数据长度
                        int len = length(buffer[1], buffer[2]);//数据长度
                        //数据完整判断第一步，长度是否足够
                        //len是数据段长度,4个字节是while行注释的3部分长度
                        if (buffer.Count < len + 4) break;//数据不够的时候什么都不做
                        //这里确保数据长度足够，数据头标志找到，我们开始计算校验
                        //2.3 校验数据，确认数据正确
                        //异或校验，逐个字节异或得到校验码
                        // '\x02', ['\x30', '\x39', '\x30', '\x32', '\x30', '\x30', '\x4f', '\x50', '\x4e', ]'\x35', '\x41', '\x03','\x0D'};
                        byte checksum = 0;
                        for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
                        {
                            if (i >= buffer.Count) { break; }
                            checksum ^= buffer[i];
                        }

                        //char[] convert = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
                        bool high = true;
                        char first = convert(checksum, high);
                        char second = convert(checksum, !high);
                        //校验和检测
                        //first != buffer[11] && second != buffer[12]

                        if (first != buffer[len + 1] && second != buffer[len + 2]) //如果数据校验失败，丢弃这一包数据
                        {
                            buffer.RemoveRange(0, len + 1);//从缓存中删除错误数据
                            continue;//继续下一次循环
                        }
                        //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                        //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                        //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                        //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                        buffer.CopyTo(0, binary_data_1, 0, len + 1);//复制一条完整数据到具体的数据缓存

                        if (len == 10)
                            data_1_catched = true;
                        else if (len == 26)
                            data_2_catched = true;//确定要保存和显示的有效位数
                        else if (len == 13)
                            data_3_catched = true;
                        else if (len == 56)
                            data_5_catched = true;
                        else if (len == 42)
                            data_6_catched = true;

                        buffer.RemoveRange(0, len + 1);//正确分析一条数据，从缓存中移除数据。
                    }

                    
                    else
                    {
                        //这里是很重要的，如果数据开始不是头，则删除数据
                        buffer.RemoveAt(0);
                    }
                }
                //分析数据

                //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了

                if (data_1_catched)
                {
                   

                }
                if (data_2_catched)
                {
                    //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了




                  
                }

                if (data_3_catched)
                {
                   


                    string Battery = Encoding.ASCII.GetString(binary_data_1).Substring(12, 1);
                    checkbattery = Battery;
                    Battery = "";

                }
                if (data_4_catched)
                {
                    

                }
                if (data_5_catched)
                {
                    if (binary_data_1[7] == 'd')
                    {
                        data = Encoding.ASCII.GetString(binary_data_1);
                        gun_Data = data;
                        string temp = Encoding.ASCII.GetString(binary_data_1).Substring(41, 2);

                        char[] cc = Encoding.ASCII.GetString(binary_data_1).Substring(38, 2).ToCharArray();

                        int cc1 = length2(cc[0], cc[1]);
                        string cc2 = Convert.ToString(cc1, 2).PadLeft(8, '0');
                        alarmCC = cc2.ToCharArray();
                        //System.Windows.Forms.MessageBox.Show(cc2);
                        temperature = temp;
                        temp = "";
                        //MessageBox.Show("temperature:" + temperature);
                        data = dataValue;
                        dataValue = "";
                    }
                    else if(binary_data_1[7] == 'S')
                    {
                        string gun_24 = Encoding.ASCII.GetString(binary_data_1).Substring(10, 8);//这里只截取了8位
                        string[] gun_po = gun_24.Split(',');
                        string Gun_Position = string.Concat(gun_po);
                        int temp = Convert.ToInt32(Gun_Position, 16);
                        string gun_positon = Convert.ToString(temp, 2).PadLeft(32, '0');//得到枪位的序列，类型为字符串



                    }
                    
                    
                }
                if (data_6_catched)
                {
                    
                    string wode = Encoding.ASCII.GetString(binary_data_1);
                    MessageBox.Show(wode);
                }
                //如果需要别的协议，只要扩展这个data_n_catched就可以了。往往我们协议多的情况下，还会包含数据编号，给来的数据进行
                //编号，协议优化后就是： 头+编号+长度+数据+校验
                //</协议解析>
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //builder.Clear();//清除字符串构造器的内容
                //因为要访问ui资源，所以需要使用invoke方式同步ui。



            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。

            }

            // return data;
        }
        /// <summary>
        /// 监测COM2bullet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comm_DataReceived2(object sender, SerialDataReceivedEventArgs e) //
        {
            if (Closing) return; //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。
                int n = com_bullet.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                com_bullet.Read(buf, 0, n);//读取缓冲数据

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //<协议解析>
                bool data_1_catched = false;//缓存记录数据是否捕获到
                bool data_2_catched = false;
                bool data_3_catched = false;
                bool data_4_catched = false;
                bool data_5_catched = false;
                bool data_6_catched = false;
                //1.缓存数据
                buffer.AddRange(buf);
                //2.完整性判断
                while (buffer.Count >= 3)//至少要包含头（1字节）+长度（1字节）+校验（1字节）
                {
                    //请不要担心使用>=，因为>=已经和>,<,=一样，是独立操作符，并不是解析成>和=2个符号
                    //2.1 查找数据头
                    if (buffer[0] == 0x02)//控制板判断使用
                    {
                        //2.2 探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                        //前面已经限定了剩余长度>=4，那我们这里一定能访问到buffer[2]这个长度
                        //(buffer(1) - 48)*16 + (buffer[2]-55) 
                        //int len = (buffer[1] - 48) * 16 + (buffer[2] - 55);//数据长度
                        int len = length(buffer[1], buffer[2]);//数据长度
                        //数据完整判断第一步，长度是否足够
                        //len是数据段长度,4个字节是while行注释的3部分长度
                        if (buffer.Count < len + 4) break;//数据不够的时候什么都不做
                        //这里确保数据长度足够，数据头标志找到，我们开始计算校验
                        //2.3 校验数据，确认数据正确
                        //异或校验，逐个字节异或得到校验码
                        // '\x02', ['\x30', '\x39', '\x30', '\x32', '\x30', '\x30', '\x4f', '\x50', '\x4e', ]'\x35', '\x41', '\x03','\x0D'};
                        byte checksum = 0;
                        for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
                        {
                            if (i >= buffer.Count) { break; }
                            checksum ^= buffer[i];
                        }

                        //char[] convert = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
                        bool high = true;
                        char first = convert(checksum, high);
                        char second = convert(checksum, !high);
                        //校验和检测
                        //first != buffer[11] && second != buffer[12]

                        if (first != buffer[len + 1] && second != buffer[len + 2]) //如果数据校验失败，丢弃这一包数据
                        {
                            buffer.RemoveRange(0, len + 1);//从缓存中删除错误数据
                            continue;//继续下一次循环
                        }
                        //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                        //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                        //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                        //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                        buffer.CopyTo(0, binary_data_1, 0, len + 1);//复制一条完整数据到具体的数据缓存

                        if (len == 10)
                            data_1_catched = true;
                        else if (len == 26)
                            data_2_catched = true;//确定要保存和显示的有效位数
                        else if (len == 13)
                            data_3_catched = true;
                        else if (len == 56)
                            data_5_catched = true;
                        else if (len == 42)
                            data_6_catched = true;

                        buffer.RemoveRange(0, len + 1);//正确分析一条数据，从缓存中移除数据。
                    }

                    
                    else
                    {
                        //这里是很重要的，如果数据开始不是头，则删除数据
                        buffer.RemoveAt(0);
                    }
                }
                //分析数据

                //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了

                if (data_1_catched)
                {
                    //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了
                }
                if (data_2_catched)
                {
                    //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了
                }

                if (data_3_catched)
                {
                    string Battery = Encoding.ASCII.GetString(binary_data_1).Substring(12, 1);
                    checkbattery = Battery;
                    Battery = "";

                }
                if (data_4_catched)
                {

                }
                if (data_5_catched)
                {
                    
                    data = Encoding.ASCII.GetString(binary_data_1);
                    gun_Data = data;
                    string temp = Encoding.ASCII.GetString(binary_data_1).Substring(41, 2);

                    char[] cc = Encoding.ASCII.GetString(binary_data_1).Substring(38, 2).ToCharArray();

                    int cc1 = length2(cc[0], cc[1]);
                    string cc2 = Convert.ToString(cc1, 2).PadLeft(8,'0');
                    alarmCC = cc2.ToCharArray();//10000000

                    temperature = temp;
                    temp = "";
                    //MessageBox.Show("temperature:" + temperature);
                    data = dataValue;
                    dataValue = "";
                }
                if (data_6_catched)
                {
                    
                    string wode = Encoding.ASCII.GetString(binary_data_1);
                    MessageBox.Show(wode);
                }
                //如果需要别的协议，只要扩展这个data_n_catched就可以了。往往我们协议多的情况下，还会包含数据编号，给来的数据进行
                //编号，协议优化后就是： 头+编号+长度+数据+校验
                //</协议解析>
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //builder.Clear();//清除字符串构造器的内容
                //因为要访问ui资源，所以需要使用invoke方式同步ui。



            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。

            }

            // return data;
        }
        //// 格式转换

        /// 转换十六进制字符串到字节数组
        /// <param name="msg">待转换字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] HexToByte(string msg)// 16进制转字符串
        {
            msg = msg.Replace(" ", "");//移除空格

            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }


        public void enable(int port, byte[] gunarkNb)//枪柜选通,使能枪位
        {
            char[] package = new char[47];
            char[] mark2 = new char[32];

            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'E';//data
            package[8] = 'N';
            package[9] = 'A';
            package[10] = 'B';

            package[11] = '0';
            package[12] = '0';
            package[13] = '0';
            package[14] = '0';
            package[15] = '0';
            package[16] = '0';
            package[17] = '0';
            package[18] = '0';
            package[19] = '0';
            package[20] = '0';
            package[21] = '0';
            package[22] = '0';
            package[23] = '0';
            package[24] = '0';
            package[25] = '0';
            package[26] = '0';
            package[27] = '0';
            package[28] = '0';
            package[29] = '0';
            package[30] = '0';
            package[31] = '0';
            package[32] = '0';
            package[33] = '0';
            package[34] = '0';
            package[35] = '0';
            package[36] = '0';
            package[37] = '0';//the end of data
            package[38] = '0';
            package[39] = '0';
            package[40] = '0';
            package[41] = '0';
            package[42] = '0';

            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';


            if (port == Port.Get_ComGun())//获取数据库中枪柜使能选通信息
            {
                mark2 = GunPositionStatus.Get_Enable().ToCharArray();//改
            }
            else
            {
                //mark2 = GunPositionStatus.Get_Mark().ToCharArray();//改
            }
            int inter1 = 11;
            for (int inter = 0; inter < mark2.Length; inter++)
            {
                package[inter1] = mark2[inter];
                inter1++;
            }


            byte[] numberTohex = new byte[400];//获取数组传入的枪柜使能选通消息
            //byte [] gunarkNb = { 1, 2, 3, 4 }
            for (int i = 0; i < gunarkNb.Length; i++)
            {
                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];
                //把选通的枪位号在八位二进制中代表的十进制数存入相应的位置
                //128--64--32--16--8--4--2--1分别对应八个枪位
                //gunarkENa()就是把枪位号转化为相应的128--64--32--16--8--4--2--1
                //然后把对应的值存到枪位号对应的数组里，数组下标比枪位号小1。
                numberTohex[number - 1] = gunarkENa(number);

            }

            bool high = true;
            for (int i = 11, j = 0; i < 43; i += 2, j += 8)
            {
                byte[] numberTohex8 = { numberTohex[j], numberTohex[j + 1], numberTohex[j + 2], numberTohex[j + 3], numberTohex[j + 4], numberTohex[j + 5], numberTohex[j + 6], numberTohex[j + 7] };
                byte gun_number8 = numberOr(numberTohex8);//通过求或运算，把置枪的位置（标记为1的地方）集合到一起
                byte pre11 = (byte)length2(package[i], package[i + 1]);//不知何意
                gun_number8 = (byte)(gun_number8 + pre11);
                package[i] = convert(gun_number8, high);// 十六进制高四位转为ASCII
                package[i + 1] = convert(gun_number8, !high);// 十六进制低四位转为ASCII
            }




            byte checksum = 0;
            int len = length2(package[1], package[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);


            gun_enable = package;
            int n = package.Length;
            if (port == Port.Get_ComGun())
            {
                com_gun.Write(package, 0, n);
                char[] mark_flag = new char[32];//用于存放32位标志位
                Array.Copy(package, 11, mark_flag, 0, 32);//把指令中的32位数据位赋值到目标数组
                string mark_Flag = new string(mark_flag);//转化为字符串
                GunPositionStatus.Insert_Enable(mark_Flag);//保存到配置文件
            }
            else
            {
                com_bullet.Write(package, 0, n);
            
            }
            
        }


        //枪弹选通
        public void gunarkXuantong(int port, byte[] gunarkNb)
        {
            char[] mark2 = new char[32];
            store[0] = numberHeader;
            store[1] = lenGunMark[0];
            store[2] = lenGunMark[1];
            int ad = 3;
            for (int i = 0; i < 4; i++)
            {
                store[ad] = numberAddress[i];
                ad++;
            }
            int MK = 7;
            for (int i = 0; i < 4; i++)
            {

                store[MK] = gunarkMark[i];
                MK++;
            }

            int st = 11;
            for (int i = 0; i < 32; i++)
            {
                store[st] = gun_status[i] = 'F';
                st++;
            }

            if (port == Port.Get_ComGun())
            {
                mark2 = GunPositionStatus.Get_Mark().ToCharArray();
            }
            else
            {
                //mark2 = DB.SelectDB_BULLET();//都是0
            }
            int inter1 = 11;
            for (int inter = 0; inter < mark2.Length; inter++)
            {
                store[inter1] = mark2[inter];
                inter1++;
            }

            byte[] numberTohex = new byte[400];
            for (int i = 0; i < gunarkNb.Length; i++)
            {
                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];
                numberTohex[number - 1] = gunarkXT(number);//不知何意
            }

            byte[] numberTohex8 = { numberTohex[0], numberTohex[1], numberTohex[2], numberTohex[3], numberTohex[4], numberTohex[5], numberTohex[6], numberTohex[7] };
            byte gun_number8 = numberAnd(numberTohex8);
            byte pre11 = (byte)length2(store[11], store[12]);
            gun_number8 = (byte)(gun_number8 & pre11);
            bool high = true;
            store[11] = convert(gun_number8, high);

            store[12] = convert(gun_number8, !high);

            byte[] numberTohex16 = { numberTohex[8], numberTohex[9], numberTohex[10], numberTohex[11], numberTohex[12], numberTohex[13], numberTohex[14], numberTohex[15] };
            byte gun_number16 = numberAnd(numberTohex16);
            byte pre13 = (byte)length2(store[13], store[14]);
            gun_number16 = (byte)(gun_number16 & pre13);
            store[13] = convert(gun_number16, high);

            store[14] = convert(gun_number16, !high);

            byte[] numberTohex24 = { numberTohex[16], numberTohex[17], numberTohex[18], numberTohex[19], numberTohex[20], numberTohex[21], numberTohex[22], numberTohex[23] };
            byte gun_number24 = numberAnd(numberTohex24);
            byte pre15 = (byte)length2(store[15], store[16]);
            gun_number24 = (byte)(gun_number24 & pre15);

            store[15] = convert(gun_number24, high);

            store[16] = convert(gun_number24, !high);

            byte[] numberTohex32 = { numberTohex[24], numberTohex[25], numberTohex[26], numberTohex[27], numberTohex[28], numberTohex[29], numberTohex[30], numberTohex[31] };
            byte gun_number32 = numberAnd(numberTohex32);
            byte pre17 = (byte)length2(store[17], store[18]);
            gun_number32 = (byte)(gun_number32 & pre17);
            store[17] = convert(gun_number32, high);

            store[18] = convert(gun_number32, !high);

            byte[] numberTohex40 = { numberTohex[32], numberTohex[33], numberTohex[34], numberTohex[35], numberTohex[36], numberTohex[37], numberTohex[38], numberTohex[39] };
            byte gun_number40 = numberAnd(numberTohex40);

            byte pre19 = (byte)length2(store[19], store[20]);
            gun_number40 = (byte)(gun_number40 & pre19);
            store[19] = convert(gun_number40, high);

            store[20] = convert(gun_number40, !high);

            byte[] numberTohex48 = { numberTohex[40], numberTohex[41], numberTohex[42], numberTohex[43], numberTohex[44], numberTohex[45], numberTohex[46], numberTohex[47] };
            byte gun_number48 = numberAnd(numberTohex48);


            byte pre21 = (byte)length2(store[21], store[22]);
            gun_number48 = (byte)(gun_number48 & pre21);


            store[21] = convert(gun_number48, high);

            store[22] = convert(gun_number48, !high);

            byte[] numberTohex56 = { numberTohex[48], numberTohex[49], numberTohex[50], numberTohex[51], numberTohex[52], numberTohex[53], numberTohex[54], numberTohex[55] };
            byte gun_number56 = numberAnd(numberTohex56);

            byte pre23 = (byte)length2(store[23], store[23]);
            gun_number56 = (byte)(gun_number56 & pre23);
            store[23] = convert(gun_number56, high);

            store[24] = convert(gun_number56, !high);



            byte[] numberTohex64 = { numberTohex[56], numberTohex[57], numberTohex[58], numberTohex[59], numberTohex[60], numberTohex[61], numberTohex[62], numberTohex[63] };
            byte gun_number64 = numberAnd(numberTohex64);


            byte pre25 = (byte)length2(store[25], store[26]);
            gun_number64 = (byte)(gun_number64 & pre25);
            store[25] = convert(gun_number64, high);

            store[26] = convert(gun_number64, !high);

            byte[] numberTohex72 = { numberTohex[64], numberTohex[65], numberTohex[66], numberTohex[67], numberTohex[68], numberTohex[69], numberTohex[70], numberTohex[71] };
            byte gun_number72 = numberAnd(numberTohex72);


            byte pre27 = (byte)length2(store[27], store[28]);
            gun_number72 = (byte)(gun_number72 & pre27);
            store[27] = convert(gun_number72, high);

            store[28] = convert(gun_number72, !high);

            byte[] numberTohex80 = { numberTohex[72], numberTohex[73], numberTohex[74], numberTohex[75], numberTohex[76], numberTohex[77], numberTohex[78], numberTohex[79] };
            byte gun_number80 = numberAnd(numberTohex80);

            byte pre29 = (byte)length2(store[29], store[30]);
            gun_number80 = (byte)(gun_number80 & pre29);

            store[29] = convert(gun_number80, high);

            store[30] = convert(gun_number80, !high);

            byte[] numberTohex88 = { numberTohex[80], numberTohex[81], numberTohex[82], numberTohex[83], numberTohex[84], numberTohex[85], numberTohex[86], numberTohex[87] };
            byte gun_number88 = numberAnd(numberTohex88);

            byte pre31 = (byte)length2(store[31], store[32]);
            gun_number88 = (byte)(gun_number88 & pre31);

            store[31] = convert(gun_number88, high);

            store[32] = convert(gun_number88, !high);

            byte[] numberTohex96 = { numberTohex[88], numberTohex[89], numberTohex[90], numberTohex[91], numberTohex[92], numberTohex[93], numberTohex[94], numberTohex[95] };
            byte gun_number96 = numberAnd(numberTohex96);

            byte pre33 = (byte)length2(store[33], store[34]);
            gun_number96 = (byte)(gun_number96 & pre33);

            store[33] = convert(gun_number96, high);

            store[34] = convert(gun_number96, !high);

            byte[] numberTohex104 = { numberTohex[96], numberTohex[97], numberTohex[98], numberTohex[99], numberTohex[100], numberTohex[101], numberTohex[102], numberTohex[103] };
            byte gun_number104 = numberAnd(numberTohex104);

            byte pre35 = (byte)length2(store[35], store[36]);
            gun_number104 = (byte)(gun_number104 & pre35);

            store[35] = convert(gun_number104, high);

            store[36] = convert(gun_number104, !high);

            byte[] numberTohex112 = { numberTohex[104], numberTohex[105], numberTohex[106], numberTohex[107], numberTohex[108], numberTohex[109], numberTohex[110], numberTohex[111] };
            byte gun_number112 = numberAnd(numberTohex112);

            byte pre37 = (byte)length2(store[37], store[38]);
            gun_number112 = (byte)(gun_number112 & pre37);

            store[37] = convert(gun_number112, high);

            store[38] = convert(gun_number112, !high);

            byte[] numberTohex120 = { numberTohex[112], numberTohex[113], numberTohex[114], numberTohex[115], numberTohex[116], numberTohex[117], numberTohex[118], numberTohex[119] };
            byte gun_number120 = numberAnd(numberTohex120);

            byte pre39 = (byte)length2(store[39], store[40]);
            gun_number120 = (byte)(gun_number120 & pre39);

            store[39] = convert(gun_number120, high);

            store[40] = convert(gun_number120, !high);

            byte[] numberTohex128 = { numberTohex[120], numberTohex[121], numberTohex[122], numberTohex[123], numberTohex[124], numberTohex[125], numberTohex[126], numberTohex[127] };
            byte gun_number128 = numberAnd(numberTohex128);


            byte pre41 = (byte)length2(store[41], store[42]);
            gun_number128 = (byte)(gun_number128 & pre41);

            store[41] = convert(gun_number128, high);

            store[42] = convert(gun_number128, !high);


            byte checksum = 0;
            int len = length2(store[1], store[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= store.Length) { break; }
                checksum ^= (byte)store[i];
            }
            bool checksum_high = true;

            store[43] = convert(checksum, checksum_high);
            store[44] = convert(checksum, !checksum_high);
            store[45] = '\x03';
            store[46] = '\x0D';

            int n = store.Length;
            if (port == Port.Get_ComGun())
            {
                com_gun.Write(store, 0, n);
                char[] mark_flag = new char[32];//用于存放32位标志位
                Array.Copy(store, 11, mark_flag, 0, 32);//把指令中的32位数据位赋值到目标数组
                string mark_Flag = new string(mark_flag);//转化为字符串
                GunPositionStatus.Insert_Mark(mark_Flag);//保存到配置文件
            }
            else 
            {
                com_bullet.Write(store, 0, n);
            
            }
        }

        public byte gunarkXT(byte number)
        {
            byte gun_number = 0;
            byte gun_n = 0;
            if (number > 0 && number < 9)
            {
                gun_number = CircularRightShift(0x7f, (byte)(number - 1));



                //(byte)(0x80 >> (number - 1));

            }
            else if (number > 8 && number < 17)
            {
                gun_n = (byte)(number - 8);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));


                //(byte)(0x80 >> (gun_n - 1));

            }
            else if (number > 16 && number < 25)
            {
                gun_n = (byte)(number - 16);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 24 && number < 33)
            {
                gun_n = (byte)(number - 24);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 32 && number < 41)
            {
                gun_n = (byte)(number - 32);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 40 && number < 49)
            {
                gun_n = (byte)(number - 40);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 48 && number < 57)
            {
                gun_n = (byte)(number - 48);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }


            else if (number > 56 && number < 65)
            {
                gun_n = (byte)(number - 56);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 64 && number < 73)
            {
                gun_n = (byte)(number - 64);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 72 && number < 81)
            {
                gun_n = (byte)(number - 72);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 80 && number < 89)
            {
                gun_n = (byte)(number - 80);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 88 && number < 97)
            {
                gun_n = (byte)(number - 88);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 96 && number < 105)
            {
                gun_n = (byte)(number - 96);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 104 && number < 113)
            {
                gun_n = (byte)(number - 104);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 112 && number < 121)
            {
                gun_n = (byte)(number - 112);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }
            else if (number > 120 && number < 129)
            {
                gun_n = (byte)(number - 120);
                gun_number = CircularRightShift(0x7f, (byte)(gun_n - 1));

            }

            return gun_number;

        }


        public void gunarkQM() // 枪位初始化,把mark标志位都置位为1，被标记
        {
            guanQM[0] = numberHeader;
            guanQM[1] = lenGunMark[0];
            guanQM[2] = lenGunMark[1];
            int ad = 3;
            for (int i = 0; i < 4; i++)
            {
                guanQM[ad] = numberAddress[i];
                ad++;
            }
            int MK = 7;
            for (int i = 0; i < 4; i++)
            {

                guanQM[MK] = gunarkMark[i];
                MK++;
            }

            int st = 11;
            for (int i = 0; i < 32; i++)
            {

                guanQM[st] = gun_status[i] = 'F';
                st++;
            }

            byte checksum = 0;
            int len = length2(guanQM[1], guanQM[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= guanQM.Length) { break; }
                checksum ^= (byte)guanQM[i];
            }
            bool checksum_high = true;

            guanQM[43] = convert(checksum, checksum_high);
            guanQM[44] = convert(checksum, !checksum_high);
            guanQM[45] = '\x03';
            guanQM[46] = '\x0D';

            int n = guanQM.Length;

            com_gun.Write(guanQM, 0, n);
        }
        public void enable_init()//枪柜初始化，将enab标志位置位为0
        {
            char[] package = new char[47];


            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'E';//data
            package[8] = 'N';
            package[9] = 'A';
            package[10] = 'B';

            for (int i = 11; i < 43; i++)
            {
                package[i] = '0';
            }

            byte checksum = 0;
            int len = length2(package[1], package[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);

            package[45] = '\x03';
            package[46] = '\x0D';

            gunarkInit = package;
            int n = gunarkInit.Length;
            com_gun.Write(gunarkInit, 0, n);
        }


        public void checkBattery(int port)//供电状态检测
        {
            checkbattery = "\x02" + "090020MOD4D" + "\x03\x0D";
            if (port == 1)
            {
                com_gun.Write(checkbattery);
            }
            else
            {
                com_gun.Write(checkbattery);
            }

        }


        /// 转换字节数组到十六进制字符串
        /// <param name="comByte">待转换字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            }

            return builder.ToString().ToUpper();
        }
        public char convert(byte number, bool high) // 十六进制 高、低四位转为ASCII
        {

            char[] list = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            if (high)
            {
                return list[number >> 4];// 高四位
            }
            else
            {
                return list[number & 0x0f];//低四位
            }

        }
       
        public byte convert_byte(byte number, bool high) // 返回十六进制 高、低四位
        {
            if (high)
            {
                return (byte)(number >> 4);// 高四位;// 高四位
            }
            else
            {
                return (byte)(number & 0x0f);//低四位
            }

        }



        public int length(byte high, byte low)//指令长度计算函数
        {
            int a = 0, b = 0;
            if (high < 58)
            {
                a = high - 48;
            }
            else
            {
                a = high - 55;
            }
            if (low < 58)
            {
                b = low - 48;
            }
            else
            {
                b = low - 55;
            }
            return a * 16 + b;
        }

        public int length2(char high, char low)
        {
            int a = 0, b = 0;
            if (high < 58)
            {
                a = high - 48;
            }
            else
            {
                a = high - 55;
            }
            if (low < 58)
            {
                b = low - 48;
            }
            else
            {
                b = low - 55;
            }
            return a * 16 + b;
        }

        public string getData()
        {
            return this.data;
        }

        public byte CircularRightShift(byte a, byte n)
        {
            return (byte)(a >> n | a << (8 - n));
        }
        /// <summary>
        ///把输入的枪位号变为在八位二进制数中的相应值，若为把7号枪位选通10000000>>6位变为00000010变为2
        /// </summary>
        /// <param name="number"></param>
        /// <returns>gun_number</returns>
        public byte gunarkENa(byte number)
        {
            byte gun_number = 0;
            while (number > 8)
            {
                number -= 8;
            }
            gun_number = (byte)(0x80 >> (number - 1));//0x80代表10000000
            return gun_number;
        }

        public byte numberOr(byte[] number)//求或
        {
            byte NumberOr = 0;
            for (int i = 0; i < number.Length; i++)
            {
                NumberOr |= (byte)(number[i]);
            }
            return NumberOr;
        }
        public byte numberAnd(byte[] number)//求与
        {
            byte NumberAnd = 0xff;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] != 0)
                {
                    NumberAnd &= (byte)(number[i]);

                }
            }
            return NumberAnd;
        }

        public char[] getStore()
        {
            return this.store;
        }
        public char[] getGuanQM()
        {
            return this.guanQM;
        }
        public void clearData()
        {
            this.data = "";
        }
        public char[] getGun_enable()
        {
            return this.gun_enable;

        }
        public char[] getGet_gun()
        {
            return this.get_gun;
        }
        public char[] getRet_gun()
        {
            return this.ret_gun;
        }
        public string getTEMP()
        {
            return this.temperature;

        }
        public char[] getGunarkInit()
        {
            return this.gunarkInit;
        }
        public string getCheckBT()
        {
            return this.checkbattery;
        }

        private char[] cancel_enable;


        public void cancel_EB(int port, byte[] gunarkNb)//枪柜选通使能抢位
        {
            char[] package = new char[47];
            char[] mark2 = new char[32];

            package[0] = '\x02';//帧头
            package[1] = '2';//len
            package[2] = 'A';

            package[3] = '0';//address
            package[4] = '0';
            package[5] = '2';
            package[6] = '0';

            package[7] = 'E';//data
            package[8] = 'N';
            package[9] = 'A';
            package[10] = 'B';

            package[11] = '0';
            package[12] = '0';
            package[13] = '0';
            package[14] = '0';
            package[15] = '0';
            package[16] = '0';
            package[17] = '0';
            package[18] = '0';
            package[19] = '0';
            package[20] = '0';
            package[21] = '0';
            package[22] = '0';
            package[23] = '0';
            package[24] = '0';
            package[25] = '0';
            package[26] = '0';
            package[27] = '0';
            package[28] = '0';
            package[29] = '0';
            package[30] = '0';
            package[31] = '0';
            package[32] = '0';
            package[33] = '0';
            package[34] = '0';
            package[35] = '0';
            package[36] = '0';
            package[37] = '0';//the end of data
            package[38] = '0';
            package[39] = '0';
            package[40] = '0';
            package[41] = '0';
            package[42] = '0';

            package[43] = '0';
            package[44] = '0';//checksum

            package[45] = '\x03';
            package[46] = '\x0D';


            if (port == Port.Get_ComGun())
            {
                mark2 = GunPositionStatus.Get_Enable().ToCharArray();
            }
            else
            {
                //mark2 = DB.SelectDB_Enabe_Bullet();
            }
            int inter1 = 11;
            for (int inter = 0; inter < mark2.Length; inter++)
            {
                package[inter1] = mark2[inter];
                inter1++;
            }


            byte[] numberTohex = new byte[400];

            for (int i = 0; i < gunarkNb.Length; i++)
            {

                if (gunarkNb[i] == 0)
                    break;
                byte number = gunarkNb[i];

                numberTohex[number - 1] = gunarkENa(number);
            }
            //////////////
            bool high = true;
            for (int i = 11, j = 0; i < 43; i += 2, j += 8)
            {
                byte[] numberTohex8 = { numberTohex[j], numberTohex[j + 1], numberTohex[j + 2], numberTohex[j + 3], numberTohex[j + 4], numberTohex[j + 5], numberTohex[j + 6], numberTohex[j + 7] };
                byte gun_number8 = numberOr(numberTohex8);//通过求或运算，把置枪的位置（标记为1的地方）集合到一起
                byte pre11 = (byte)length2(package[i], package[i + 1]);//不知何意
                gun_number8 = (byte)(pre11 - gun_number8);
                package[i] = convert(gun_number8, high);// 十六进制高四位转为ASCII
                package[i + 1] = convert(gun_number8, !high);// 十六进制低四位转为ASCII
            }


            byte checksum = 0;
            int len = length2(package[1], package[2]);

            for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
            {
                if (i >= package.Length) { break; }
                checksum ^= (byte)package[i];
            }
            bool checksum_high = true;

            package[43] = convert(checksum, checksum_high);
            package[44] = convert(checksum, !checksum_high);


            cancel_enable = package;
            int n = package.Length;
            if (port == Port.Get_ComGun())
            {
                com_gun.Write(package, 0, n);
                char[] mark_flag = new char[32];//用于存放32位标志位
                Array.Copy(package, 11, mark_flag, 0, 32);//把指令中的32位数据位赋值到目标数组
                string mark_Flag = new string(mark_flag);//转化为字符串
                GunPositionStatus.Insert_Enable(mark_Flag);//保存到配置文件
            }
            else
            {
                com_bullet.Write(package, 0, n);
            }
            


            
        }


        public char[] getCancel_enable()
        {
            return this.cancel_enable;

        }

        public char[] getAlarmCC()//
        {
            return this.alarmCC;
        }
        public string getGun_Data()//得到异常信息
        {
            return this.gun_Data;
        }

    }
}
