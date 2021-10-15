/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Security.Cryptography;

namespace ToolGood.Bedrock
{
    /// <summary>
    /// HASH
    /// </summary>
    public static class HashUtil
    {
        #region MD5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetMd5String(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(buffer);
            md5.Dispose();
            return BitConverter.ToString(retVal).Replace("-", "");
        }
  
        #endregion

        #region SHA1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetSha1String(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            var buffer = System.Text.Encoding.UTF8.GetBytes(text);
            return GetSha1String(buffer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetSha1String(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");

            System.Security.Cryptography.SHA1CryptoServiceProvider osha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] retVal = osha1.ComputeHash(buffer);
            osha1.Dispose();
            return BitConverter.ToString(retVal).Replace("-", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
 
        #endregion

        #region SHA512
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetSha512String(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            var buffer = System.Text.Encoding.UTF8.GetBytes(text);
            return GetSha512String(buffer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetSha512String(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");

            SHA512 sha512 = new SHA512Managed();
            byte[] retVal = sha512.ComputeHash(buffer); //计算指定Stream 对象的哈希值
            sha512.Dispose();
            return BitConverter.ToString(retVal).Replace("-", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        #endregion

    }
}
