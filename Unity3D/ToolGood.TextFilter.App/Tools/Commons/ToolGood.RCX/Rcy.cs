/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Text;

namespace ToolGood.RcxCrypto
{
    /// <summary>
    /// RCY algorithm
    /// Author : Lin ZhiJun
    /// NickName : ToolGood
    /// Email : toolgood@qq.com
    /// 
    /// See https://github.com/toolgood/RCY
    /// </summary>
    public static class Rcy
    {
        private const int keyLen = 256;

        public static void Encrypt(byte[] data, string pass, int start = 0)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(pass)) throw new ArgumentNullException("pass");

            encrypt(data, Encoding.UTF8.GetBytes(pass), start);
        }

        private unsafe static void encrypt(byte[] data, byte[] pass, int start)
        {
            byte[] mBox = GetKey(pass, keyLen);
            int i = 0, j = 0;
            var length = data.Length;

            fixed (byte* _mBox = &mBox[0])
            fixed (byte* _data = &data[0]) {
                for (Int64 offset = start; offset < length; offset++) {
                    i = (++i) & 0xFF;
                    j = (j + *(_mBox + i)) & 0xFF;

                    byte a = *(_data + offset);
                    byte c = (byte)(a ^ *(_mBox + ((*(_mBox + i) & *(_mBox + j)))));
                    *(_data + offset) = c;

                    j = (j + a + c);
                }
            }
        }
        private static unsafe byte[] GetKey(byte[] pass, int kLen)
        {
            byte[] mBox = new byte[kLen];
            fixed (byte* _mBox = &mBox[0]) {
                for (Int64 i = 0; i < kLen; i++) {
                    *(_mBox + i) = (byte)i;
                }
                Int64 j = 0;
                int lengh = pass.Length;
                fixed (byte* _pass = &pass[0]) {
                    for (Int64 i = 0; i < kLen; i++) {
                        j = (j + *(_mBox + i) + *(_pass + (i % lengh))) % kLen;
                        byte temp = *(_mBox + i);
                        *(_mBox + i) = *(_mBox + j);
                        *(_mBox + j) = temp;
                    }
                }
            }
            return mBox;
        }

    }
}
