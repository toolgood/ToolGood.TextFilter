/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.IO;
using System.IO.Compression;

namespace ToolGood.TextFilter.App.Commons
{
    /// <summary>
    /// 压缩
    /// </summary>
    public static class CompressionUtil
    {
        public static MemoryStream GzipDecompress(byte[] data, int start)
        {
            var resultStream = new MemoryStream();
            using (MemoryStream stream = new MemoryStream(data, start, data.Length - start)) {
                using (GZipStream zStream = new GZipStream(stream, CompressionMode.Decompress)) {
                    zStream.CopyTo(resultStream);
                }
            }
            resultStream.Position = 0;
            return resultStream;
        }
 


    }
}
