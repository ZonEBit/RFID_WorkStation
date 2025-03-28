using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID_WorkStation.Common
{
    /// <summary>
    /// 标签日志类
    /// </summary>
    public class TagLog
    {
        //UID
        public byte[] UID = new byte[8];
        //安全门进出时间
        public byte[] inout_time = new byte[6];
        //安全门进出方向
        public byte inout_AlarmType;

        //获取标签类型
        public string getTagType()
        {
            string uid_type = "未知";//Unknown
            if (UID[7] == 0xe0 && UID[6] == 0x04)
            {
                switch (UID[5])
                {
                    case 0x01:
                        {
                            if ((UID[4] & 0x18) == 0x10)
                            {
                                uid_type = "ICODE SLIX";
                            }
                            else if ((UID[4] & 0x18) == 0x00)
                            {
                                uid_type = "ICODE SLI";//不支持密码
                            }
                            else if ((UID[4] & 0x18) == 0x08)
                            {
                                uid_type = "ICODE SLIX2";
                            }
                        }
                        break;
                    case 0x02:
                        {
                            uid_type = "ICODE SLIX-S";
                        }
                        break;
                    case 0x03:
                        {
                            uid_type = "ICODE SLIX-L";//不支持密码
                        }
                        break;
                }
            }
            return uid_type;
        }

        public string getUID()
        {
            return String.Format("{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}", UID[0], UID[1], UID[2], UID[3], UID[4], UID[5], UID[6], UID[7]);
        }
        //获取进出
        public String getInOut()
        {
            if ((inout_AlarmType & 0x01) == 0x00)
                return "出";
            else
                return "进";
        }

        //获取报警方式
        public String getAlarmType()
        {
            switch (inout_AlarmType >> 1)
            {
                case 0x00:
                    return "AFI";
                case 0x01:
                    return "EAS";
                case 0x02:
                    return "DSFID";
                case 0x03:
                    return "EAS+AFI";
                case 0x04:
                    return "EAS+DSFID";
            }
            return "";
        }

        //获取进出时间
        public String getTime()
        {
            return String.Format("20{0:X2}/{1:X2}/{2:X2}/{3:X2}:{4:X2}:{5:X2}", inout_time[0], inout_time[1], inout_time[2], inout_time[3], inout_time[4], inout_time[5]);
        }
    }
}
