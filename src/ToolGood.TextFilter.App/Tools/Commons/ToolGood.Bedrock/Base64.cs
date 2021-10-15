/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Text;

namespace ToolGood.Bedrock
{
    /// <summary>
    /// Modified Base64 for URL applications ('base64url' encoding)
    /// 
    /// See http://tools.ietf.org/html/rfc4648
    /// For more information see http://en.wikipedia.org/wiki/Base64
    /// </summary>
    static class Base64
    {
        /// <summary>
        /// 转成 Base64String
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] input)
        {
            return Convert.ToBase64String(input);
        }
        /// <summary>
        ///  转成 byte[] 
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static byte[] FromBase64String(string base64)
        {
            return FromBase64ForUrlString(base64);
        }


        /// <summary>
        /// Modified Base64 for URL applications ('base64url' encoding)
        /// 
        /// See http://tools.ietf.org/html/rfc4648
        /// For more information see http://en.wikipedia.org/wiki/Base64
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Input byte array converted to a base64ForUrl encoded string</returns>
        public static string ToBase64ForUrlString(byte[] input)
        {
            StringBuilder result = new StringBuilder(Convert.ToBase64String(input).TrimEnd('='));
            result.Replace('+', '-');
            result.Replace('/', '_');
            return result.ToString();
        }

        private const string base64 = "===========================================+=+=/0123456789=======ABCDEFGHIJKLMNOPQRSTUVWXYZ====/=abcdefghijklmnopqrstuvwxyz=====";
        /// <summary>
        /// Modified Base64 for URL applications ('base64url' encoding)
        /// 
        /// See http://tools.ietf.org/html/rfc4648
        /// For more information see http://en.wikipedia.org/wiki/Base64
        /// </summary>
        /// <param name="base64ForUrlInput"></param>
        /// <returns>Input base64ForUrl encoded string as the original byte array</returns>
        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in base64ForUrlInput) {
                if ((int)c >= 128) continue;
                var k = base64[c];
                if (k == '=') continue;
                sb.Append(k);
            }
            var len = sb.Length;
            int padChars = (len % 4) == 0 ? 0 : (4 - (len % 4));
            if (padChars > 0) sb.Append(String.Empty.PadRight(padChars, '='));
            return Convert.FromBase64String(sb.ToString());
        }
    }
}
