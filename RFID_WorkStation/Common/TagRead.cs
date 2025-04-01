using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RFID_WorkStation.Common
{
    public struct RFData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] sendData;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] recvData;
    }

    public static class TagRead
    {
        public const int NOCOMM = -1;
        public const int COM = 0;
        public const int USB = 1;
        public const int NET = 2;
        public static bool isConnect = false;
        public static List<byte[]> uidList = new List<byte[]>();

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_RFIDopenCOM(string comNum, uint baudRate = 38400, byte byteSize = 8, byte parityBit = 2, byte stopBit = 0);//打开串口

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr PC_EnumCOM();//枚举串口

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_RFIDopenUSB(ushort VID = 0xFFFE, ushort PID = 0x0091);//打开USB

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_NETInit();//初始化网络

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_NETExit();//释放网络资源

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_SetNETIP(string IP);//设置目标IP

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern byte PC_GetCommType();//获取通信方式


        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_RFIDclose();//关闭设备

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_OpenDevice(ref RFData rf);//打开设备指令

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_GetDeviceInfoVersion(ref RFData rf);//获取设备信息

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_NoiseCheck(ref RFData rf, byte data);//噪音检测指令

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_Open_CloseRFPower(ref RFData rf, byte data);//开关射频

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ReadCfgBlock(ref RFData rf, byte block);//读配置块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_WriteCfgBlock(ref RFData rf, byte[] data, byte block);//写配置块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_SaveCfgBlock(ref RFData rf, byte block);//保存配置块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_HardwareControl(ref RFData rf, byte OpenType, byte HarType, byte Flag);//板上硬件控制

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_SetBuzzerState(ref RFData rf, byte state);//设置蜂鸣器状态

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_SetRelayState(ref RFData rf, byte state);//设置继电器状态

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_OpenAnt_One(ref RFData rf, byte Data);//打开指定天线口

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_InventoryTag(ref RFData rf, byte data);//盘点指令

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_NetInventoryACK(ref RFData rf, UInt16 NumFlag);//网络发送盘点响应包

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_GetOneTagInfo(ref RFData rf, byte[] UID, byte add);//获取标签信息

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ReadCardOneBlock(ref RFData rf, byte[] UID, byte add, byte block);//读取单个数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ReadCardMultBlock(ref RFData rf, byte[] UID, byte add, byte block, byte blockNum);//读多个数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_WriteCardOneBlock(ref RFData rf, byte[] UID, byte add, byte block, byte[] blockData);//写单个数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_WriteCardMultBlock(ref RFData rf, byte[] UID, byte add, byte block, byte blockNum, byte[] blockData);//写多个数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_WriteOneTagDSFID(ref RFData rf, byte[] UID, byte add, byte dsfid);//写DSFID

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_WriteOneTagAFI(ref RFData rf, byte[] UID, byte add, byte afi);//写AFI

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_EnableOneTagEAS(ref RFData rf, byte[] UID, byte add);//使能EAS

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_BanOneTagEAS(ref RFData rf, byte[] UID, byte add);//禁止EAS

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_CheckOneTagEAS(ref RFData rf, byte[] UID, byte add);//检测EAS

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_NetScanDeviceInfo(ref RFData rf);//网络搜索设备

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_Write(byte[] data, ushort len);//通用发送操作

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_Read(ref RFData rf);//通用读取操作




        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443AInventoryTag(ref RFData rf);//ISO14443A盘点标签

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443AKeyAuthentication(ref RFData rf, byte[] uid, byte block, byte KeyType, byte[] key);//ISO14443A认证密钥

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443AReadBlock(ref RFData rf, byte[] uid, byte block, byte Num, byte KeyType, byte[] key);//ISO14443A读数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443AWriteBlock(ref RFData rf, byte[] uid, byte block, byte Num, byte KeyType, byte[] key, byte[] data); //ISO14443A写数据块

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443AFormatWriteBlock(ref RFData rf, byte[] uid, byte block, byte KeyType, byte key, byte[] data);//ISO14443A操作钱包

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443ARechargeWriteBlock(ref RFData rf, byte[] uid, byte block, byte Cmd, byte KeyType, byte[] key, byte[] data);//ISO14443A操作钱包

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ISO14443ABackWriteBlock(ref RFData rf, byte[] uid, byte block, byte TAddr, byte KeyType, byte[] key);//ISO14443A备份钱包


        //安全门相关
        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_DoorTakeRecords(ref RFData rf, ushort NumFlag, byte Flagy);//安全门获取标签记录

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_GetInOutInfo(ref RFData rf);//安全门获取进出人员数量

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ClearInOutInfo(ref RFData rf, byte data);//安全门清空进出数量

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_InOutExchange(ref RFData rf);//安全门进出方向调换

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_SetSysTimer(ref RFData rf, byte[] data);//安全门设置系统时间

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_GetSysTimer(ref RFData rf);//安全门获取系统时间

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_ClearAllRecordData(ref RFData rf);//安全门清除所有记录数据

        [DllImport("PC_RFID2.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern bool PC_AllDoorAlarm(ref RFData rf, byte data);//安全门命令控制报警


        //输出发送数据
        public static void log_sendData(RFData rf)
        {
            Console.Write("发送>>");
            for (int i = 0; i <= rf.sendData[1]; i++)
            {
                Console.Write("{0:X2} ", rf.sendData[i]);
            }
            Console.WriteLine("");
        }

        //输出接收数据
        public static void log_recvData(RFData rf)
        {
            Console.Write("接收<<");
            for (int i = 0; i <= rf.recvData[1]; i++)
            {
                Console.Write("{0:X2} ", rf.recvData[i]);
            }
            Console.WriteLine("");
        }

        //输出发送和接收数据
        public static void log_sendrecvData(RFData rf)
        {
            log_sendData(rf);
            log_recvData(rf);
        }


        //枚举串口
        public static string[] enumCOM()
        {
            IntPtr temp = PC_EnumCOM();
            string res = Marshal.PtrToStringAnsi(temp);
            string[] allcom = res.Split(';');
            string[] com_list = new string[allcom.Length - 1];
            for (int i = 0; i < com_list.Length; i++)//去除allcom最后一个元素（""）
            {
                com_list[i] = allcom[i];
            }
            return com_list;
        }

        /**
        * 盘点标签
        * antNum: 要打开的天线口号(0~30，单天线盘点)，0表示不打开（多天线盘点）
        * isUploadAntNum: 是否开启了上传天线编号（只有网络盘点用到这个参数）
        * tab_list: 标签列表 
        * 返回值：执行期间无错误发生返回true,否则false
        * 这里只针对USB方式进行测试，网络和串口方式未测试
        */

        public static bool ScanTab(int antNum, bool isUploadAntNum, List<TagInfo> tab_list)
        {
            tab_list.Clear();
            RFData rf = new RFData();
            byte scanType = 0;
            if (antNum >= 1 && antNum <= 30)
            {
                //单标签盘点,打开指定天线口
                if (PC_OpenAnt_One(ref rf, (byte)antNum))
                {
                    log_sendrecvData(rf);
                    Console.WriteLine("打开天线口%d成功！\n", antNum);
                    scanType = 0x04;
                }
                else
                {
                    return false;
                }
            }
            switch (PC_GetCommType())
            {              
                case USB:             
                    {
                        if (PC_InventoryTag(ref rf, scanType))
                        {
                            log_sendData(rf);                          
                            while (rf.recvData[1] > 7)
                            {
                                log_recvData(rf);
                                //从数据包中获取DSFID
                                TagInfo tag_info = new TagInfo();
                                tag_info.DSFID = rf.recvData[6];
                                //获取天线口，设置了上传天线编号才能获取到天线口
                                if (antNum == 0)
                                {
                                    if (rf.recvData[1] == 0x11)
                                    {//通过包的长度判断是否包含天线口数据，设置了上传天线编号数据包多一个字节
                                        tag_info.Ant = rf.recvData[15];
                                    }
                                    else
                                    {
                                        tag_info.Ant = 1;
                                    }
                                }
                                else
                                {
                                    tag_info.Ant = antNum;
                                }
                                //获取uid
                                for (int i = 0; i < 8; i++)
                                {
                                    tag_info.UID[i] = rf.recvData[i + 7];
                                }
                                tab_list.Add(tag_info);
                                //继续接收,直到接收到结束包为止
                                if (!PC_Read(ref rf))
                                    break;
                            }
                            log_recvData(rf);
                        }
                        else
                        {
                            return false;
                        }
                    }
                break;
            }
            return true;
        }   

        public static bool connectByUsb()
        {
            if (PC_RFIDopenUSB())//使用默认参数VID:0xFFFE,PID:0x0091
            {
                Console.WriteLine("\n打开USB【VID:0xFFFE,PID:0x0091】成功");
                PC_NETInit();//初始化网络
                isConnect = true;
                return true;
            }
            else
            {
                Console.WriteLine("USB接口未连接设备或被占用");
                isConnect = false;              
                return false;
            }
        }

        public static bool disConnect()
        {
            if (isConnect) {
                if (PC_RFIDclose() && PC_NETExit()) {
                    isConnect = false;
                    return true;
                }
                return false;
            }
            return false;        
        }
   
        public static void readBlock(byte[] UID) 
        {
            RFData rf = new RFData();
            byte[] data = new byte[256];        

            //打开天线口命令，读写标签之前要打开标签所在的天线口
            if (PC_OpenAnt_One(ref rf, (byte)1))
            {
                log_sendrecvData(rf);
                Console.WriteLine("打开天线口1成功\n");
            }
            else
            {
                Console.WriteLine("打开天线口1失败");
                return;
            }

            //读多个数据块命令
            if (PC_ReadCardMultBlock(ref rf, UID, (byte)1, (byte)0, (byte)8))
            {
                log_sendrecvData(rf);
                if (rf.recvData[5] == 0)
                {
                    // 读取8个块(0-7)                  
                    for (int block = 0; block < 8; block++)
                    {
                        // 每个块占5字节(1字节块号+4字节数据)，从第17字节开始
                        int startIndex = 17 + (block * 5);
                        for (int k = 0; k < 4; k++)
                        {
                            data[(block * 4) + k] = rf.recvData[startIndex + k];
                        }
                    }

                    string dataStr = BitConverter.ToString(data).Replace("-", "");
                    Console.WriteLine($"{dataStr} ");

                    if (PC_GetCommType() != NET)
                    {
                        //非UDP传输方式，还有一个结束包
                        RFData rf_1 = new RFData();
                        if (PC_Read(ref rf_1))
                        {
                            log_recvData(rf_1);
                        }
                        else
                        {
                            Console.WriteLine("读数据块0~7失败");
                            return;
                        }
                    }
                    Console.WriteLine("读数据块0~7成功\n");

                }
                else
                {
                    Console.WriteLine("读数据块0~7失败");
                    return;
                }
            }
            else
            {
                Console.WriteLine("读数据块0~7失败");
                return;
            }
        }

        public static void writeOneBlock(byte[] UID) 
        {
            RFData rf = new RFData();
            byte[] data = new byte[256];

            //打开天线口命令，读写标签之前要打开标签所在的天线口
            if (PC_OpenAnt_One(ref rf, (byte)1))
            {
                log_sendrecvData(rf);
                Console.WriteLine("打开天线口1成功\n");
            }
            else
            {
                Console.WriteLine("打开天线口1失败");
                return;
            }

           
            //写单个数据块命令
            if (PC_WriteCardOneBlock(ref rf, UID, (byte)1, (byte)2, data))
            {
                log_sendrecvData(rf);
                if (rf.recvData[5] == 0)
                {
                    Console.WriteLine("写数据块2成功\n");
                }
                else
                {
                    Console.WriteLine("写数据块2失败");
                    return;
                }
            }
            else
            {
                Console.WriteLine("写数据块2失败");
                return;
            }

        }

        public static bool WriteMultiBlocksDynamic(byte[] UID, string barcode, byte startBlock = 2, int maxBlocks = 16)
        {
            // 1. 数据准备阶段
            byte[] asciiBytes = Encoding.ASCII.GetBytes(barcode);
            int totalBytes = asciiBytes.Length;

            // 计算需要的块数量(每个块4字节)
            int blocksNeeded = (int)Math.Ceiling(totalBytes / 4.0);
            blocksNeeded = Math.Min(blocksNeeded, maxBlocks); // 不超过最大限制

            Console.WriteLine($"准备写入 {blocksNeeded} 个块 (从块{startBlock}开始)");

            // 2. 准备完整写入数据(补全到4的倍数)
            byte[] writeData = new byte[blocksNeeded * 4];
            Array.Copy(asciiBytes, writeData, Math.Min(asciiBytes.Length, writeData.Length));

            // 3. 执行多块写入命令
            RFData rf = new RFData();
            if (PC_WriteCardMultBlock(ref rf, UID, (byte)1, startBlock, (byte)blocksNeeded, writeData))
            {
                log_sendrecvData(rf);
                if (rf.recvData[5] == 0)
                {
                    Console.WriteLine($"成功写入块{startBlock}~{startBlock + blocksNeeded - 1}");
                    return true;
                }
                else
                {
                    Console.WriteLine("写入失败: 卡片返回错误状态");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("写入失败: 通信错误");
                return false;
            }
        }

        public static bool readTag()
        {
            if (isConnect) // 使用默认参数VID:0xFFFE,PID:0x0091
            {            
                bool isUploadAntNum = false; // 上传天线编号
                List<TagInfo> tag_list = new List<TagInfo>();
                uidList.Clear(); // 清空

                // 盘点命令
                if (ScanTab(0, isUploadAntNum, tag_list))
                {

                    Console.WriteLine("盘点成功");
                    Console.WriteLine("标签数量: {0}\n", tag_list.Count);

                    if (tag_list.Count == 0)
                    {
                        Console.WriteLine("检测完成！");
                        return false;
                    }

                    // 所有标签的UID
                    foreach (TagInfo tag in tag_list)
                    {
                        byte[] uidCopy = new byte[8];
                        Array.Copy(tag.UID, uidCopy, 8);
                        uidList.Add(uidCopy);
                    }

                    // 打印所有UID
                    Console.WriteLine("标签UID:");
                    for (int i = 0; i < uidList.Count; i++)
                    {
                        string uidStr = BitConverter.ToString(uidList[i]).Replace("-", "");
                        Console.WriteLine($"{uidStr} ");                      
                    }

                    return true;

                }
                else
                {
                    Console.WriteLine("盘点失败");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("USB接口未连接设备或被占用");
                return false;
            }
        }

        public static bool handleTag(byte[] UID,string barcode) 
        {        
            if(WriteMultiBlocksDynamic(UID, barcode))
                return true; 
            else
            { return false; }
        }
    }
}
