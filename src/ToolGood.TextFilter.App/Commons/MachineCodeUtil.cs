/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Diagnostics;
#if Win
using System.Management;
# endif
using System.Text;
using ToolGood.Bedrock;

namespace ToolGood.TextFilter.Commons
{
    public static class MachineCodeUtil
    {
        private static string _code = null;

        public static string GetMachineCode()
        {
            if (_code == null) {
                try {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("ToolGood.TextFilter");
                    stringBuilder.Append(Environment.OSVersion.Platform.ToString());
                    stringBuilder.Append(Environment.Is64BitOperatingSystem.ToString());
                    stringBuilder.Append(Environment.Is64BitProcess.ToString());

                    stringBuilder.Append(GetMainBoardSn());
                    stringBuilder.Append(GetCpuID());

                    _code = HashUtil.GetSha1String(stringBuilder.ToString());
                } catch (Exception) {
                    _code = "";
                }
            }
            return _code;
        }


        public static string GetMainBoardSn()
        {
            string strbNumber = string.Empty;
#if Win
            if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                using (ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_BaseBoard")) {
                    foreach (ManagementObject mo in mos.Get()) {
                        strbNumber = mo["SerialNumber"].ToString();
                        break;
                    }
                }
            }
#endif

            return strbNumber;
        }

        public static string GetCpuID()
        {
            string cpuInfo = "";
#if Win
            if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                using (ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Processor")) {
                    foreach (ManagementObject mo in mos.Get()) {
                        cpuInfo = mo["ProcessorId"].ToString();
                        break;
                    }
                }
                return cpuInfo;
            }
#endif


            if (Environment.OSVersion.Platform == PlatformID.Unix) {
                cpuInfo = executeLinuxCmd("cat", "/sys/class/dmi/id/product_uuid");
            }

            return cpuInfo;
        }

        private static string executeLinuxCmd(string cmd, string args)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(cmd, args) {
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                RedirectStandardError = false,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            var output = process.StandardOutput;
            var txt = output.ReadToEnd();
            process.WaitForExit();

            return txt;
        }


    }
}
