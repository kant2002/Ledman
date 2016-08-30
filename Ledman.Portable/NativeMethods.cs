namespace Ledman
{
    using System.Runtime.InteropServices;

    public static class NativeMethods
    {
        public const string DllName = "LED_Setup_Interface.dll";

        /// <summary>
        /// 接收卡Hub板最大端口数
        /// </summary>
        public const int MAX_RECEIVER_HUB_PORT_AMNT = 16;

        /// <summary>
        /// 一个Hub板端口下最大模组数
        /// </summary>
        public const int MAX_RECV_HUB_PORT_MODL_AMNT = 16;

        public struct tagReceiverIdInfo
        {
            /// <summary>
            /// 发送卡索引号，第一张发送卡索引号为0，第二张为1，以此类推。
            /// </summary>
            public byte bySenderIndex;

            /// <summary>
            /// 发送卡端口索引号，A端口为0，B端口为1.
            /// </summary>
            public byte bySenderPortIndex;

            /// <summary>
            /// Hub卡端口索引号，没有使用Hub卡时为0，有使用Hub卡时第一个端口为0，第二个为1，以此类推。
            /// </summary>
            public byte byHubPortIndex;
        }

        public struct tagReceiverBadPanels
        {
            /// <summary>
            /// 不正常模组数
            /// </summary>
            public byte byBadPanelCount;

            /// <summary>
            /// 不正常模组的行位置（注：默认都是从屏体的正面看）
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byBadPanelRow;

            /// <summary>
            /// 不正常模组的列位置
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byBadPanelCol;

            /// <summary>
            /// 接收卡的行位置
            /// </summary>
            public ushort wReceiverRow;

            /// <summary>
            /// 接收卡的列位置
            /// </summary>
            public ushort wReceiverCol;
        }

        public struct tagSenderVersionInfo
        {
            /// <summary>
            /// 发送卡CPU主版本
            /// </summary>
            public byte bySenderCpuMainVer;

            /// <summary>
            /// 发送卡CPU子版本
            /// </summary>
            public byte bySenderCpuSubVer;

            /// <summary>
            /// 发送卡FPGA主版本
            /// </summary>
            public byte bySenderFpgaMainVer;

            /// <summary>
            /// 发送卡FPGA子版本
            /// </summary>
            public byte bySenderFpgaSubVer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagSenderBrightness
        {
            /// <summary>
            /// 发送卡总体亮度
            /// </summary>
            public byte byTotalBright;

            /// <summary>
            /// 发送卡红亮度
            /// </summary>
            public byte byRedBright;

            /// <summary>
            /// 发送卡绿亮度
            /// </summary>
            public byte byGreenBright;

            /// <summary>
            /// 发送卡蓝亮度
            /// </summary>
            public byte byBlueBright;
        }

        public struct tagSenderPortRect
        {
            /// <summary>
            /// 发送卡端口输出X坐标偏移
            /// </summary>
            public ushort wLeft;

            /// <summary>
            /// 发送卡端口输出Y坐标偏移
            /// </summary>
            public ushort wTop;

            /// <summary>
            /// 发送卡端口输出宽度
            /// </summary>
            public ushort wWidth;

            /// <summary>
            /// 发送卡端口输出高度
            /// </summary>
            public ushort wHeight;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct tagSenderExProp
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string wcSenderIp;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string wcSenderNickName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public string wcSenderMac;
        }

        public struct tagApiBigWord
        {
            public byte hi;

            public byte lo;

            public ushort GetWord()
            {
                return (ushort)(this.hi << 8 + this.lo);
            }
        }

        public struct tagApiBigShort
        {
            public byte hi;

            public byte lo;

            public short GetShort()
            {
                if ((hi & 0x80) != 0)
                {
                    var temp = (hi & 0x7F) << 8 + lo;
                    return (short)-temp;
                }

                return (short)(hi << 8 + lo);
            }
        }

        public struct tagReceiverMonitorData
        {
            /// <summary>
            /// 数据部分长度128字节
            /// Data0-1：PCB版本号, byPcbMain主版本号，byPcbSub子版本号.
            /// </summary>
            public byte byPcbMain;

            public byte byPcbSub;

            /// <summary>
            /// Data2-3：CPU版本号, byCpuMain主版本号,byCpuSub子版本号.
            /// </summary>
            public byte byCpuMain;

            public byte byCpuSub;

            /// <summary>
            /// Data4-5：设备ID号
            /// </summary>
            public tagApiBigWord bwDeviceId;

            /// <summary>
            /// Data6：	升级代码有效标志 或 驱动芯片类型
            /// </summary>
            public byte byFlagOrChipl;

            /// <summary>
            /// Data7：	接收卡地址模式
            /// </summary>
            public byte byAddressMode;

            /// <summary>
            /// Data8-9  电源电压(单位mv)
            /// </summary>
            public tagApiBigWord bwPowerVolt;

            /// <summary>
            /// Data0xA-B
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byReserved;

            /// <summary>
            /// Data0xC 环境亮度标志 0: 环境亮度无效, 1: 环境亮度有效， 2: 亮度溢出
            /// </summary>
            public byte byEnviBritFlag;

            /// <summary>
            /// Data0xD-E 环境亮度，等于0时无效,单位lux【(bwEnviBritValue.GetWord()*nBritCfct)/100】,nBritCfct：亮度衰减系数
            /// </summary>
            public tagApiBigWord bwEnviBritValue;

            /// <summary>
            /// Data0xF  箱体湿度
            /// </summary>
            public byte byHumidity;

            /// <summary>
            /// Data0x10-0x11：		FPGA版本号,byFpgaMain主版本， byFpgaSub子版本.
            /// </summary>
            public byte byFpgaMain;

            public byte byFpgaSub;

            /// <summary>
            /// Data0x12-0x13:		箱体宽度
            /// </summary>
            public tagApiBigWord bwWidth;

            /// <summary>
            /// Data0x14-0x15:		箱体高度
            /// </summary>
            public tagApiBigWord bwHeight;

            /// <summary>
            /// Data0x16-0x17:
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byReserved2;

            /// <summary>
            /// Data0x18-19 箱体温度,单位℃（bnCabinetTemperature.GetShort()/10.0f)
            /// </summary>
            public tagApiBigShort bnCabinetTemperature;

            /// <summary>
            /// Data0x1A-1B 模组平均温度,单位℃（bnCabinetTemperature.GetShort()/10.0f)
            /// </summary>
            public tagApiBigShort bnPaneAvgTemperature;

            /// <summary>
            /// Data0x1C-1D 
            /// </summary>
            public tagApiBigWord bwBadPackage;

            /// <summary>
            /// Data0x1E-1F 
            /// </summary>
            public tagApiBigWord bwPixelChipCount;

            /// <summary>
            /// Data0x20-2F 面板状态,	Bit1：模组温度传感器有效标志，0：无效，1：有效 ;  Bit0：模组I2C存储器有效标志，0：无效，1：有效
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byPaneStates;

            /// <summary>
            /// Data0x30-4f 面板温度,单位℃（bnCabinetTemperature.GetShort()/10.0f)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I2)]
            public tagApiBigShort[] bnPaneTemeratures;

            /// <summary>
            /// Data0x50-6f  像素检测坏点数(16路)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I2)]
            public tagApiBigShort[] bwBadPixelCount;

            /// <summary>
            /// Data0x70-7F 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byReserved4;

            /// <summary>
            /// 模组存储器有效?
            /// </summary>
            /// <param name="byIndex"></param>
            /// <returns></returns>
            public bool IsPaneValid(byte byIndex)
            {
                return byIndex < 16
                    ? (byPaneStates[byIndex] & 0x01) != 0
                    : false;
            }

            public bool IsPaneTemperatureValid(byte byIndex)
            {
                return byIndex < 16
                    ? (byPaneStates[byIndex] & 0x02) != 0
                    : false;
            }
        }

        /// <summary>
        /// 查询接收卡返回数据追加部分
        /// </summary>
        public struct tagReceiverMonitorData_ExPart
        {
            /// <summary>
            /// Data0x80-8F				数据线对应模组数量以及排线状态。bit0~3：模组数量； bit7:1表示都好的，0表示有坏的；
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byModuleNumberCableStatus;

            /// <summary>
            /// Data90-AF					模组电压(实际电压=bwModuleVolt*32/1000.0)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I2)]
            public tagApiBigWord[] bwModuleVolt;

            /// <summary>
            /// DataB0-CF					模组温度。低字节：数据线下最高温度；高字节：数据线下平均温度；
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I2)]
            public tagApiBigWord[] bwModuleTemperature;

            public bool IsAllCableOk(byte byIndex)
            {
                return byIndex < 16 ? (byModuleNumberCableStatus[byIndex] & 0x80) != 0 : false;
            }

            public byte GetModuleNumber(byte byIndex)
            {
                return (byte)(byIndex < 16
                    ? (byModuleNumberCableStatus[byIndex] & 0xF)
                    : 0);
            }

            public ushort GetAvgVolt(byte byIndex)
            {
                return (ushort)(byIndex < 16 ? ((bwModuleVolt[byIndex].hi)) : 0);
            }

            public ushort GetLowVolt(byte byIndex)
            {
                return (ushort)(byIndex < 16 ? ((bwModuleVolt[byIndex].lo)) : 0);
            }

            public sbyte GetAvgTemp(byte byIndex)
            {
                return (sbyte)(byIndex < 16 ? ((bwModuleTemperature[byIndex].hi)) : 0);
            }

            public sbyte GetLowTemp(byte byIndex)
            {
                return (sbyte)(byIndex < 16 ? ((bwModuleTemperature[byIndex].lo)) : 0);
            }
        }

        public struct tagRecvModlInfo
        {
            /// <summary>
            /// 模组地址 [BIT7-4] = Row address 0~15, [BIT3-0] = Column address 0~13
            /// </summary>
            public byte byModuleAddr;

            /// <summary>
            /// 连线状态	[BIT2] = 0 B data line broke
            /// 　　				 1 good
            ///		　	[BIT1] = 0 G data line broke
            ///			  　　	 1 good
            ///			[BIT0] = 0 R data line broke
            ///					 1 good			
            /// </summary>
            public byte byCableStatus;

            /// <summary>
            /// 模组电压(实际电压=byModuleVoltage*32/1000.0)
            /// </summary>
            public byte byModuleVoltage;

            /// <summary>
            /// 模组温度
            /// </summary>
            public sbyte sbyModuleTemperature;

            short GetModlVoltAsShort()
            {
                return byModuleVoltage;
            }

            sbyte GetModlTemperature()
            {
                return sbyModuleTemperature;
            }

            byte GetModlAddrRow()
            {
                return (byte)(byModuleAddr >> 4);
            }

            byte GetModlAddrCol()
            {
                return (byte)(byModuleAddr & 0xF);
            }

            bool IsCableOk()
            {
                return ((byCableStatus & 0x1) != 0
                    && (byCableStatus & 0x2) != 0
                    && (byCableStatus & 0x4) != 0);
            }
        }

        public struct tagReceiverModuleDetailInfo
        {
            public int bQueryResultOk;
            public ushort nRealModuleAmount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RECEIVER_HUB_PORT_AMNT * MAX_RECV_HUB_PORT_MODL_AMNT, ArraySubType = UnmanagedType.I4)]
            public tagRecvModlInfo[] stReceiverModlInfo;

            public bool GetReceiverModlInfo(byte byRowAddr/*0~15*/, byte byColAddr/*0~13*/, ref tagRecvModlInfo rReceiverModlInfo)
            {
                bool bResult = false;
                if (nRealModuleAmount > MAX_RECEIVER_HUB_PORT_AMNT * MAX_RECV_HUB_PORT_MODL_AMNT)
                {
                    nRealModuleAmount = MAX_RECEIVER_HUB_PORT_AMNT * MAX_RECV_HUB_PORT_MODL_AMNT;
                }

                for (int i = 0; i < nRealModuleAmount; i++)
                {
                    if ((stReceiverModlInfo[i].byModuleAddr >> 4) == byRowAddr
                        && (stReceiverModlInfo[i].byModuleAddr & 0xF) == byColAddr)
                    {
                        rReceiverModlInfo = stReceiverModlInfo[i];
                        bResult = true;
                        break;
                    }
                }

                return bResult;
            }
        }

        /// <summary>
        /// 重新连接发送卡
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool ReConnectSender();

        /// <summary>
        /// 获取发送卡连接使用的网络端口号
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern ushort GetSenderNetCommPort();

        /// <summary>
        /// 设置发送卡连接使用的网络端口号，需与发送卡匹配，默认为8399
        /// </summary>
        /// <param name="wNetCommPort"></param>
        [DllImport(DllName)]
        public static extern void SetSenderNetCommPort(ushort wNetCommPort = 8399);

        /// <summary>
        /// 根据IP地址范围连接发送卡，返回连接上的发送卡数
        /// </summary>
        /// <param name="byIpAddrSub1"></param>
        /// <param name="byIpAddrSub2"></param>
        /// <param name="byIpAddrSub3"></param>
        /// <param name="byIpAddr4_Start"></param>
        /// <param name="byIpAddr4_End"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int ConnectSenderByIp(byte byIpAddrSub1, byte byIpAddrSub2, byte byIpAddrSub3, byte byIpAddr4_Start, byte byIpAddr4_End);

        /// <summary>
        /// 清除已连接上的所有发送卡
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool ClearSenders();

        /// <summary>
        /// 获取已连接发送卡数量
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern ushort GetConnectSenderCount();

        /// <summary>
        /// 重新搜索接收卡
        /// </summary>
        [DllImport(DllName)]
        public static extern void ReSearchReceivers();

        /// <summary>
        /// 获取搜索到的接收卡数量,如果从未连接上发送卡并执行ReSearchReceivers（）则返回-1
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int GetFoundReceiverCount();

        /// <summary>
        /// 获取指定发送卡端口下，连接正常的接收卡数量；
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="byPortIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int GetConnectingReceiverCount(ushort wSenderId, byte byPortIndex);

        /// <summary>
        /// 判断是否正在搜索接收卡
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsSearchingReceiver();

        /// <summary>
        /// 停止搜索接收卡
        /// </summary>
        [DllImport(DllName)]
        public static extern void StopSearchReceiver();

        /// <summary>
        /// 获取指定接收卡监控返回的数据
        /// </summary>
        /// <param name="pReceiverMonitorData"></param>
        /// <param name="wReceiverConnIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverMonitorData(out tagReceiverMonitorData pReceiverMonitorData, ushort wReceiverConnIndex);

        /// <summary>
        /// 获取指定接收卡监控返回的数据
        /// </summary>
        /// <param name="pReceiverMonitorData"></param>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wReceiverIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverMonitorData_ById(out tagReceiverMonitorData pReceiverMonitorData, ushort wSenderId, byte bySenderPortIndex, ushort wReceiverIndex);

        /// <summary>
        /// 获取指定接收卡监控返回的额外数据（模组汇总信息）
        /// </summary>
        /// <param name="pReceiverMonitorData_ExPart"></param>
        /// <param name="wReceiverConnIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverMonitorData_ExPart(out tagReceiverMonitorData_ExPart pReceiverMonitorData_ExPart, ushort wReceiverConnIndex);

        /// <summary>
        /// 获取指定接收卡监控返回的额外数据（模组汇总信息）
        /// </summary>
        /// <param name="pReceiverMonitorData_ExPart"></param>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wReceiverIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverMonitorData_ExPart_ById(out tagReceiverMonitorData_ExPart pReceiverMonitorData_ExPart, ushort wSenderId, byte bySenderPortIndex, ushort wReceiverIndex);

        /// <summary>
        /// 查询指定接收卡模组详细信息
        /// </summary>
        /// <param name="pReceiverModuleDetailInfo"></param>
        /// <param name="wReceiverConnIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool QueryReceiverModuleDetailInfo(out tagReceiverModuleDetailInfo pReceiverModuleDetailInfo, ushort wReceiverConnIndex);

        /// <summary>
        /// 查询指定接收卡模组详细信息
        /// </summary>
        /// <param name="pReceiverModuleDetailInfo"></param>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wReceiverIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool QueryReceiverModuleDetailInfo_ById(out tagReceiverModuleDetailInfo pReceiverModuleDetailInfo, ushort wSenderId, byte bySenderPortIndex, ushort wReceiverIndex);

        /// <summary>
        /// 获取指定接收卡Id信息
        /// </summary>
        /// <param name="pRecvIdInfo"></param>
        /// <param name="wReceiverConnIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverIdInfo(out tagReceiverIdInfo pRecvIdInfo, ushort wReceiverConnIndex);

        /// <summary>
        /// 获取发送卡ID
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern ushort GetSenderId(byte bySenderIndex);

        [DllImport(DllName)]
        public static extern bool GetSenderExProp(byte bySenderIndex, out tagSenderExProp pSenderExProp);

        [DllImport(DllName)]
        public static extern bool GetSenderExProp_ById(ushort wSenderId, out tagSenderExProp pSenderExProp);

        /// <summary>
        /// 设置显示屏起始坐标
        /// </summary>
        /// <param name="wLeft"></param>
        /// <param name="wTop"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetDisplayOrigin(ushort wLeft, ushort wTop);

        /// <summary>
        /// 设置发送卡起始坐标
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="wLeft"></param>
        /// <param name="wTop"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderOrigin(byte bySenderIndex, ushort wLeft, ushort wTop);

        /// <summary>
        /// 设置发送卡起始坐标
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="wLeft"></param>
        /// <param name="wTop"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderOrigin_ById(ushort wSenderId, ushort wLeft, ushort wTop);

        /// <summary>
        /// 设置发送卡端口坐标偏移
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wLeft"></param>
        /// <param name="wTop"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortOffset(byte bySenderIndex, byte bySenderPortIndex, ushort wLeft, ushort wTop);

        /// <summary>
        /// 设置发送卡端口坐标偏移
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wLeft"></param>
        /// <param name="wTop"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortOffset_ById(ushort wSenderId, byte bySenderPortIndex, ushort wLeft, ushort wTop);

        /// <summary>
        /// 设置发送卡端口输出宽高
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wWidth"></param>
        /// <param name="wHeight"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortSize(byte bySenderIndex, byte bySenderPortIndex, ushort wWidth, ushort wHeight);

        /// <summary>
        /// 设置发送卡端口输出宽高
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wWidth"></param>
        /// <param name="wHeight"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortSize_ById(ushort wSenderId, byte bySenderPortIndex, ushort wWidth, ushort wHeight);

        /// <summary>
        /// 设置显示屏总体亮度
        /// </summary>
        /// <param name="byBrightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetDisplayBrightness(byte byBrightness);

        /// <summary>
        /// 设置发送卡总体亮度
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="byBrightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderBrightness(byte bySenderIndex, byte byBrightness);

        /// <summary>
        /// 设置发送卡总体亮度
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="byBrightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderBrightness_ById(ushort wSenderId, byte byBrightness);

        /// <summary>
        /// 设置显示屏红、绿、蓝亮度
        /// </summary>
        /// <param name="byRed"></param>
        /// <param name="byGreen"></param>
        /// <param name="byBlue"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetDisplayColor(byte byRed, byte byGreen, byte byBlue);

        /// <summary>
        /// 设置发送卡红、绿、蓝亮度
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="byRed"></param>
        /// <param name="byGreen"></param>
        /// <param name="byBlue"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetSenderColor(byte bySenderIndex, byte byRed, byte byGreen, byte byBlue);

        /// <summary>
        /// 设置发送卡红、绿、蓝亮度
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="byRed"></param>
        /// <param name="byGreen"></param>
        /// <param name="byBlue"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetSenderColor_ById(ushort wSenderId, byte byRed, byte byGreen, byte byBlue);

        /// <summary>
        /// 开关显示屏
        /// </summary>
        /// <param name="bOn"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetDisplayState(bool bOn);

        /// <summary>
        /// 检查发送卡是否在连接状态
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsSenderConnect(byte bySenderIndex);

        /// <summary>
        /// 检查发送卡是否在连接状态
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsSenderConnect_ById(ushort wSenderId);

        /// <summary>
        /// 检查发送卡DVI输入是否正常
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsSenderDviInputOk(byte bySenderIndex);

        /// <summary>
        /// 检查发送卡DVI输入是否正常
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsSenderDviInputOk_ById(ushort wSenderId);

        /// <summary>
        /// 获取指定发送卡输入分辨率
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="rHorResolution"></param>
        /// <param name="rVerResolution"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderDviInput(byte bySenderIndex, out ushort rHorResolution, out ushort rVerResolution);

        /// <summary>
        /// 获取指定发送卡输入分辨率
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="rHorResolution"></param>
        /// <param name="rVerResolution"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderDviInput_ById(ushort wSenderId, out ushort rHorResolution, out ushort rVerResolution);

        /// <summary>
        /// 获取指定发送卡版本信息
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="pSenderVersionInfo"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderVersionInfo(byte bySenderIndex, out tagSenderVersionInfo pSenderVersionInfo);

        /// <summary>
        /// 获取指定发送卡版本信息
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="pSenderVersionInfo"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderVersionInfo_ById(ushort wSenderId, out tagSenderVersionInfo pSenderVersionInfo);

        /// <summary>
        /// 获取指定发送卡亮度信息
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="pSenderBrightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderBrightness(byte bySenderIndex, out tagSenderBrightness pSenderBrightness);

        /// <summary>
        /// 获取指定发送卡亮度信息
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="pSenderBrightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderBrightness_ById(ushort wSenderId, out tagSenderBrightness pSenderBrightness);

        /// <summary>
        /// 获取发送卡端口输出区域
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="pSenderPortRect"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderPortRect(byte bySenderIndex, byte bySenderPortIndex, out tagSenderPortRect pSenderPortRect);

        /// <summary>
        /// 获取发送卡端口输出区域
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="pSenderPortRect"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderPortRect_ById(ushort wSenderId, byte bySenderPortIndex, out tagSenderPortRect pSenderPortRect);

        /// <summary>
        /// 加载显示屏设置文件
        /// </summary>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        [DllImport(DllName, CharSet = CharSet.Unicode)]
        public static extern bool LoadDisplaySetFile(string pFileName);

        /// <summary>
        /// 获取指定接收卡不正常模组信息
        /// </summary>
        /// <param name="wReceiverConnIndex"></param>
        /// <param name="pReceiverBadPanels"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverBadPanels(ushort wReceiverConnIndex, out tagReceiverBadPanels pReceiverBadPanels);

        /// <summary>
        /// 获取指定接收卡不正常模组信息
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bySenderPortIndex"></param>
        /// <param name="wReceiverIndex"></param>
        /// <param name="pReceiverBadPanels"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetReceiverBadPanels_ById(ushort wSenderId, byte bySenderPortIndex, ushort wReceiverIndex, out tagReceiverBadPanels pReceiverBadPanels);

        /// <summary>
        /// 开关接收卡监控
        /// </summary>
        /// <param name="bOn"></param>
        [DllImport(DllName)]
        public static extern void SetReceiverMonitor(bool bOn);

        /// <summary>
        /// 获取接收卡采集到的环境亮度
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int GetEnvironmentBright();

        /// <summary>
        /// 设置亮度衰减系数（当采集到的亮度与实际环境亮度不一致时，可通过设置此参数达到校正）
        /// </summary>
        /// <param name="wLumAtteCoef"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetLumAttenuationCoefficient(ushort wLumAtteCoef);

        /// <summary>
        /// 获取当前亮度衰减系数
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern ushort GetLumAttenuationCoefficient();

        /// <summary>
        /// 检查接收卡是否处在监控状态
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool IsReceiverMonitorOn();

        /// <summary>
        /// 重启接收卡, nSenderId = -1时，重启所有发送卡下的接收卡
        /// </summary>
        /// <param name="nSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool ReStartReceivers(int nSenderId = -1);

        /// <summary>
        /// 设置指定发送卡显示源，nSenderId = -1时设置所有发送卡, bySource = 0： 切换当前选择的源,同步->异步，异步->同步　1： 同步优先模式　2： 强制异步模式　3： 强制同步模式　　
        /// 返回设置发送卡的数量
        /// </summary>
        /// <param name="bySource"></param>
        /// <param name="nSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern int SetDisplaySource(byte bySource = 1, int nSenderId = -1);

        /// <summary>
        /// 获取指定发送卡显示源，nSenderId = -1时获取第一张发送卡显示源。 返回 0：异步， 1：同步
        /// </summary>
        /// <param name="nSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern byte GetDisplaySource(int nSenderId = -1);

        /// <summary>
        /// 设置发送卡端口开关状态
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bPortA_On"></param>
        /// <param name="bPortB_On"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortState(ushort wSenderId, bool bPortA_On = true, bool bPortB_On = true);

        /// <summary>
        /// 获取发送卡端口开关状态
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="bPortA_On"></param>
        /// <param name="bPortB_On"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderPortState(ushort wSenderId, ref bool bPortA_On, ref bool bPortB_On);

        /// <summary>
        /// 设置发送卡端口亮度
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="stPortA_Brightness"></param>
        /// <param name="stPortB_Brightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SetSenderPortBright(ushort wSenderId, tagSenderBrightness stPortA_Brightness, tagSenderBrightness stPortB_Brightness);

        /// <summary>
        /// 获取发送卡端口亮度
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <param name="stPortA_Brightness"></param>
        /// <param name="stPortB_Brightness"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool GetSenderPortBright(ushort wSenderId, out tagSenderBrightness stPortA_Brightness, out tagSenderBrightness stPortB_Brightness);

        /// <summary>
        /// 保存发送卡设置数据
        /// </summary>
        /// <param name="bySenderIndex"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SaveSetsToSender(byte bySenderIndex);

        /// <summary>
        /// 保存发送卡设置数据
        /// </summary>
        /// <param name="wSenderId"></param>
        /// <returns></returns>
        [DllImport(DllName)]
        public static extern bool SaveSetsToSender_ById(ushort wSenderId);
    }
}
