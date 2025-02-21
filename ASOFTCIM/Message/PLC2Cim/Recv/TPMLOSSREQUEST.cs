﻿using A_SOFT.PLC;
using A_SOFT.Ctl.Mitsu.Model;
using A_SOFT.CMM.INIT;
using ASOFTCIM.Data;using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASOFTCIM.Message.PLC2Cim.Recv
{
    public class TPMLOSSREQUEST
    {
        public void Excute(ACIM eq, object body)
        {

            try
            {
                BitModel bit = (BitModel)body;

                

List<IWordModel> word = bit.LstWord;
                LOSSCODEREPORT loss = new LOSSCODEREPORT();
                loss.LOSSCODE.LOSSCODE = word.FirstOrDefault(x => x.Item == "LOSSCODE").GetValue(eq.PLC);
                loss.LOSSCODE.LOSSDESCRIPTION = word.FirstOrDefault(x => x.Item == "LOSSDESCRIPTION").GetValue(eq.PLC);

                eq.SendS6F11_606( loss);
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
