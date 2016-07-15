using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;

namespace MyUI.Utils
{
    public class Alcohol
    {
        private SerialPort comm = new SerialPort();
        private string state;//显示串口的状态
        private string return_data;//显示返回的结果
        public string test_status;

        public string Test_status
        {
            get { return test_status; }
            set { test_status = value; }
        }
        private List<byte> buffer = new List<byte>();//默认分配1页内存，并始终限制不允许超过
        private byte[] once_data = new byte[50];//把收到的数据，缓存到这里进行分析
        public void OpenPort(String port)//打开串口
        {
            if (comm.IsOpen) 
                comm.Close();
            System.Threading.Thread.Sleep(1000);
            comm.PortName = port; 
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
                MessageBox.Show(ex.Message);
            }
            if (comm.IsOpen)
            {
                string name = comm.PortName;
                int baudRate = comm.BaudRate;
                state = name + "串口已经正常打开.  " + "波特率: " + baudRate;
            }
            comm.DataReceived += comm_DataReceived;
            //表示将处理 System.IO.Ports.SerialPort 对象的数据接收事件的方法。
        }

        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            bool test_result = false;//缓存记录数据是否捕获到
            bool query_result = false;

            byte[] ReDatas = new byte[comm.BytesToRead];//返回命令包
            comm.Read(ReDatas, 0, ReDatas.Length);//读取数据
            return_data = System.Text.Encoding.Default.GetString(ReDatas);//把数据转化为字符串
            buffer.AddRange(ReDatas);
            int len = length(buffer[1], buffer[2]);//数据长度
            if (buffer[0] == 0x02)//查看帧头
            {
                //异或校验，逐个字节异或得到校验码
                // '\x02', ['\x30', '\x39', '\x30', '\x32', '\x30', '\x30', '\x4f', '\x50', '\x4e', ]'\x35', '\x41', '\x03','\x0D'};
                byte checksum = 0;
                for (int i = 1; i < len + 1; i++)//len-4表示校验之前的位置
                {
                    checksum ^= buffer[i];
                }

                bool high = true;
                char first = convert(checksum, high);
                char second = convert(checksum, !high);
                //校验和检测


                if (first != buffer[len + 1] || second != buffer[len + 2]) //如果数据校验失败，丢弃这一包数据
                {
                    buffer.RemoveAt(0);//从缓存中删除错误数据
                }
                //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                else
                {
                    buffer.CopyTo(0, once_data, 0, len + 1);//复制一条完整数据到具体的数据缓存
                    buffer.RemoveRange(0, len + 1);//正确分析一条数据，从缓存中移除数据。
                    if (len == 14)
                        test_result = true;
                    else if (len == 10 || len == 11)
                        query_result = true;
                }
            }
            else
            {
                //这里是很重要的，如果数据开始不是头，则删除数据
                buffer.RemoveAt(0);
            }
            if (test_result)
            {
                test_status = Encoding.ASCII.GetString(once_data).Substring(13, 2);//得到检测结果状态码
                test_result = false;//用完把状态改回
            }
            else if (query_result)
            {
                if (len == 10)
                    test_status = "ok";
                else if(len == 11)
                    test_status = Encoding.ASCII.GetString(once_data).Substring(11, 1);//得到检测结果状态码
            }

        }
        public char convert(byte number, bool high) // byte类型转为二位ascii码，十六进制 高、低四位转为ASCII
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

        public void send_message(char[] bs)//往串口写入数据
        {
            int n = bs.Length;
            comm.Write(bs, 0, n);
        }

        public void testOnce()//检验酒精一次
        {
            //0B0020ALCO140
            char[] test1 = new char[16];
            test1[0] = '\x02';//开始符

            test1[1] = '0';
            test1[2] = 'B';//长度

            test1[3] = '0';
            test1[4] = '0';
            test1[5] = '2';
            test1[6] = '0';//地址

            test1[7] = 'A';
            test1[8] = 'L';
            test1[9] = 'C';
            test1[10] = 'O';
            test1[11] = '1';//命令号

            test1[12] = '4';
            test1[13] = '0';//校验位

            test1[14] = '\x03';//结束符
            test1[15] = '\x0D';//回车符

            comm.Write(test1, 0, test1.Length);//发送指令
        }

        public void testTwice()//检验酒精两次
        {
            //0B0020ALCO243
            char[] test2 = new char[16];
            test2[0] = '\x02';//开始符

            test2[1] = '0';
            test2[2] = 'B';//长度

            test2[3] = '0';
            test2[4] = '0';
            test2[5] = '2';
            test2[6] = '0';//地址

            test2[7] = 'A';
            test2[8] = 'L';
            test2[9] = 'C';
            test2[10] = 'O';
            test2[11] = '2';//命令号

            test2[12] = '4';
            test2[13] = '3';//校验位

            test2[14] = '\x03';//结束符
            test2[15] = '\x0D';//回车符

            comm.Write(test2, 0, test2.Length);//发送指令
        }

        public void eliminateAlarm()//消除报警
        {
            //0B0020ALCO041
            char[] eliminatealarm = new char[16];
            eliminatealarm[0] = '\x02';//开始符

            eliminatealarm[1] = '0';
            eliminatealarm[2] = 'B';//长度

            eliminatealarm[3] = '0';
            eliminatealarm[4] = '0';
            eliminatealarm[5] = '2';
            eliminatealarm[6] = '0';//地址

            eliminatealarm[7] = 'A';
            eliminatealarm[8] = 'L';
            eliminatealarm[9] = 'C';
            eliminatealarm[10] = 'O';
            eliminatealarm[11] = '0';//命令号

            eliminatealarm[12] = '4';
            eliminatealarm[13] = '1';//校验位

            eliminatealarm[14] = '\x03';//结束符
            eliminatealarm[15] = '\x0D';//回车符

            comm.Write(eliminatealarm, 0, eliminatealarm.Length);//发送指令
        }

        public void query()//查询结果
        {
            //0B0020ALCO?4E
            char[] query = new char[16];

            query[0] = '\x02';//开始符

            query[1] = '0';
            query[2] = 'B';//长度

            query[3] = '0';
            query[4] = '0';
            query[5] = '2';
            query[6] = '0';//地址

            query[7] = 'A';
            query[8] = 'L';
            query[9] = 'C';
            query[10] = 'O';
            query[11] = '?';//命令号

            query[12] = '4';
            query[13] = 'E';//校验位

            query[14] = '\x03';//结束符
            query[15] = '\x0D';//回车符

            comm.Write(query, 0, query.Length);//发送指令
        }

        public void open()//开门
        {
            //090020OPN5A
            char[] open = new char[14];

            open[0] = '\x02';//开始符

            open[1] = '0';
            open[2] = '9';//长度

            open[3] = '0';
            open[4] = '0';
            open[5] = '2';
            open[6] = '0';//地址

            open[7] = 'O';
            open[8] = 'P';
            open[9] = 'N';//命令号

            open[10] = '5';
            open[11] = 'A';//校验位

            open[12] = '\x03';//结束符
            open[13] = '\x0D';//回车符

        }

        public string getReturnData()//得到返回的完整数据
        {
            return return_data;
        }
        public string getTestStatus()//得到返回的检测结果
        {
            return test_status;
        }
    }
}
