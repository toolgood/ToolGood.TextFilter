/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml;

namespace ToolGood.Bedrock
{
    /// <summary>
    /// RSA 加解密
    /// </summary>
    public partial class RsaUtil
    {
        #region 私钥加密 公钥解密
 
        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] PublicDecrypt(string publicKey, byte[] bytes)
        {
            using (RsaEncryption rsa = new RsaEncryption()) {
                rsa.LoadPublicFromXml(publicKey);
                return rsa.PublicDecryption(bytes);
            }
        }

        #endregion 私钥加密 公钥解密

        #region 密钥解析
 
        private static void LoadPublicKey(RSA rsa, string key)
        {
            RSAParameters parameters;
            //if (key.StartsWith("<")) {
                parameters = LoadXmlString(key);
            //} else {
            //    parameters = FromPem(key, out bool _);
            //}
            rsa.ImportParameters(parameters);
        }

        private static RSAParameters LoadXmlString(string xmlString)
        {
            RSAParameters parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue")) {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes) {
                    switch (node.Name) {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            } else {
                throw new Exception("Invalid XML RSA key.");
            }
            return parameters;
        }
 
        #endregion

        class RsaEncryption : IDisposable
        {
            private BigInteger Exponent;
            private BigInteger Modulus;
            private RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            private bool isPublicKeyLoaded = false;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void LoadPublicFromXml(string publicString)
            {
                LoadPublicKey(rsa, publicString);

                RSAParameters rsaParams = rsa.ExportParameters(false);
                Modulus = FromBytes(rsaParams.Modulus);
                Exponent = FromBytes(rsaParams.Exponent);
                isPublicKeyLoaded = true;
            }

            public byte[] PublicDecryption(byte[] data)
            {
                if (!isPublicKeyLoaded) throw new CryptographicException("Public Key must be loaded before using the Public Encryption method!");

                int len = (rsa.KeySize / 8) + 1;
                if (data.Length > len) {
                    using (var ms = new MemoryStream()) {
                        MemoryStream msInput = new MemoryStream(data);
                        byte[] buffer = new byte[len];
                        int readLen = msInput.Read(buffer, 0, len);

                        while (readLen > 0) {
                            while (buffer[readLen - 1] == 0) { readLen--; }
                            byte[] dataToEnc = new byte[readLen];
                            Array.Copy(buffer, 0, dataToEnc, 0, readLen);

                            var bytes = PublicDecryption2(dataToEnc);
                            ms.Write(bytes, 0, bytes.Length);

                            readLen = msInput.Read(buffer, 0, len);
                        }
                        msInput.Close();
                        return ms.ToArray();
                    }
                }
                return PublicDecryption2(data);
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public byte[] PublicDecryption2(byte[] data)
            {
                return BigInteger.ModPow(FromBytes(data), Exponent, Modulus).ToByteArray().Reverse().ToArray();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static BigInteger FromBytes(byte[] beBytes)
            {
                return new BigInteger(beBytes.Reverse().Concat(new byte[] { 0 }).ToArray());
            }

            public void Dispose()
            {
                rsa.Clear();
            }
        }

    }
}
