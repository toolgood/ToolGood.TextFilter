///* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
///* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
//using System;
//using System.Collections.Generic;
//using System.Text;
//using ToolGood.Bedrock;
//using ToolGood.TextFilter.Datas;

//namespace ToolGood.TextFilter.Commons
//{
//    public static partial class LicenceUtil
//    {
//        private const string _publicStr = "<RSAKeyValue><Modulus>1ReRfHUCnhxLYZdNzh+/6laPXvkYH6Q5BApl5bW/4DKYLtKbu1vgYN0OOus72kc47oK7W7qHbpQ5fSwiKuqeXUQzgWKOf1B20hdrgObZwQeVWNeEPqatRvdT2tq+RRyXFJSXexMUTKbICbX13UtJ6R8nelCgkjwKkjjlsqIn7SunnJLCNKInyH6DLCLknGrzM2So9T1kdo2S7fcqGJ0CZzZqiwo+X8hnmpeHuM/p1MBEcsWRbB5qEAx5qCHrkRDhTAvhlFaQB2ALPJ/1ylQEU1XxLOgG/6LMLykGNOQeavu9EXt57Fv9NLqGjSew0zw5P6kzcgzvNExAfxHOQRliyKt42QG36kn2p+5pfQlMKSTQyDEkLiIu1ybGYL8ZZ/yxAdFIzEZDB6ajAm+KBk8OPSMIe5TLcp3zRcVrYekV8b7gEPeuQVWPd0IrWBFD/t0tWQQ6t0B8vYSLn/35baW8uIV6msj/1FDJ9E9EVsNAzCfoSa4ZkFZmh6bmEM4utDCYtiGKcO8Ds5xQuGuriD6bJKAPX1t2rF8IKMR2sQPoiN+jOenL/pzGfxosETNlnQGMZIfdojFcwdWpAWh3es3aIJ+V+EsCgbbJXa9kGTB3TOfHI7TdnLkOW1QXWrRVRk0Q2HjTHw6dfTFaVkag0Z1ccIh5a6wl6AYKtl9GofhDsoE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
//        public static LicenceInfo Load(string licenceTxt, string machineCode)
//        {
//            try {
//                var bytes = Base64.FromBase64ForUrlString(licenceTxt);
//                RcxCrypto.Rcy.Encrypt(bytes, machineCode);
//                bytes = RsaUtil.PublicDecrypt(_publicStr, bytes);
//                var txt = Encoding.UTF8.GetString(bytes);
//                var sp = txt.Split('#');
//                if (sp.Length == 1) { return null; }

//                var sign = HashUtil.GetSha512String(sp[0]);
//                if (string.Equals(sign, sp[1], StringComparison.OrdinalIgnoreCase) == false) { return null; }

//                var ts = sp[0].Split('|');
//                LicenceInfo info = new LicenceInfo();
//                int index = 0;
//                info.MachineCode = ts[index++];
//                info.Phone = ts[index++];
//                info.ImageLicence = ts[index++] == "1";
//                info.BrowserLicence = ts[index++] == "1";
//                info.GrpcLicence = ts[index++] == "1";

//                info.RegisterTime = DateTime.Parse(ts[index++]);
//                info.ServiceEnd = DateTime.Parse(ts[index++]);
//                info.PasswordDictionary = new Dictionary<string, string>();

//                ts = ts[index++].Split('&');
//                foreach (var item in ts) {
//                    if (string.IsNullOrEmpty(item)) { continue; }
//                    var s = item.Split('=');
//                    info.PasswordDictionary[s[0]] = s[1];
//                }

//                if (string.Equals(info.MachineCode, machineCode, StringComparison.OrdinalIgnoreCase) == false) { return null; }
//                info.LicenceTxt = licenceTxt;
//                return info;
//            } catch (Exception ex) {
//                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}>>> Load licence error:{ex.Message}");
//            }
//            return null;
//        }
//    }
//}
