/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;

namespace ToolGood.TextFilter.Datas
{
    public class LicenceInfo 
    {
        public string MachineCode;
        public string Phone;
        public DateTime RegisterTime;
        public DateTime ServiceEnd;
        public Dictionary<string, string> PasswordDictionary;

        public bool ImageLicence;
        public bool BrowserLicence;
        public bool GrpcLicence;

        public string LicenceTxt;


        public void ClearTemp()
        {
            PasswordDictionary = null;
        }

    }
}
