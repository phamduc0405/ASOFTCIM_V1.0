﻿using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class INTERLOCKCONFIRMREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;
                

List<IWordModel> word = bit.LstWord;
                List<INTERLOCKMESS> interlock = new List<INTERLOCKMESS>();
                INTERLOCKMESS mess = new INTERLOCKMESS();
                mess.INTERLOCKID = word.FirstOrDefault(x => x.Item == "OPCALLID").GetValue(eq.PLC);
                mess.MESSAGE = word.FirstOrDefault(x => x.Item == "MESSAGE").GetValue(eq.PLC);
                interlock.Add(mess);
                eq.SendS6F11_502( interlock, "502");
                bit.SetPCValue = true;
            }
            catch (Exception ex)
            {
                var debug = string.Format("Class:{0} Method:{1} exception occurred. Message is <{2}>.", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                LogTxt.Add(LogTxt.Type.Exception, debug);
            }
            
        }
    }
}
