using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFID_WorkStation.Common
{
    /// <summary>
    /// 标签信息类
    /// </summary>
    public class TagInfo
    {
        public byte[] UID = new byte[8];
        public int Ant;//
        public byte DSFID;

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
    }
}
