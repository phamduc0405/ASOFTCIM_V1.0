﻿using A_SOFT.CMM.INIT;
using ASOFTCIM.Helper;
using ASOFTCIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AComm.TCPIP;
using A_SOFT.Ctl.SecGem;

namespace ASOFTCIM
{
    public partial class ACIM
    {
        public void SendS6F201( LABELINFORMATIONREQUEST label)
        {
            try
            {
                SysPacket packet = new SysPacket(_cim.Conn);
                packet.Stream = 6;
                packet.Function = 201;
                packet.Command = Command.UserData;
                packet.DeviceId = EqpData.DeviceId;
                packet.SystemByte = EqpData.TransactionSys;
                packet.addItem(DataType.List, 3);
                packet.addItem(DataType.Ascii, EqpData.EQINFORMATION.EQPID);
                packet.addItem(DataType.Ascii, "");
                packet.addItem(DataType.List, 4);
                {
                    packet.addItem(DataType.Ascii, label.CELLID);
                    packet.addItem(DataType.Ascii, label.PRODUCTID);
                    packet.addItem(DataType.Ascii, label.OPTIONINFO);
                    packet.addItem(DataType.Ascii, label.LABELID);
                }
                packet.Send2Sys();
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
