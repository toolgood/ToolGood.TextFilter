/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Commons
{
    partial class MemoryCache
    {
        /// <summary>
        /// 许可
        /// </summary>
        public LicenceInfo LicenceInfo { get; private set; }


        private void InitLicence()
        {
            LicenceInfo = new LicenceInfo() {
                MachineCode = "无",
                Phone = "评估版本",
                RegisterTime = DateTime.Parse("2021-1-1"),
                ServiceEnd = DateTime.Parse("2099-1-1"),
            };
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> BaseDirectory {AppContext.BaseDirectory}");
            //var licFile = Path.Combine(AppContext.BaseDirectory, MachineCodeUtil.GetMachineCode() + ".lic");
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> licFile {Path.GetFileName(licFile)}");
            //if (File.Exists(licFile)) {
            //    var txt = File.ReadAllText(licFile);
            //    var licence = LicenceUtil.Load(txt, MachineCodeUtil.GetMachineCode());
            //    if (licence != null) {
            //        LicenceInfo = licence;
            //    }
            //}
        }

        public bool UpdateLicence(string txt)
        {
            //var licence = LicenceUtil.Load(txt, MachineCodeUtil.GetMachineCode());
            //if (licence == null) { return false; }

            //if (LicenceInfo != null) {
            //    if (LicenceInfo.ServiceEnd > licence.ServiceEnd) {
            //        return false;
            //    }
            //}
            //var licFile = Path.Combine(AppContext.BaseDirectory, MachineCodeUtil.GetMachineCode() + ".lic");
            //File.WriteAllText(licFile, txt);

            //Init();
            return true;
        }


        private void Licence_Dispose()
        {
            LicenceInfo = null;
        }

    }
}
