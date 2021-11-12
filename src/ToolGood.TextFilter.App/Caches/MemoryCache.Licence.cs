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
        }


    }
}
