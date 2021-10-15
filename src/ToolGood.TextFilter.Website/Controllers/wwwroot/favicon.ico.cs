#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("favicon.ico")]
        public IActionResult favicon_ico()
        {
            if (SetResponseHeaders("5D025699F631D73059211985AA11B41A") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, " image/vnd.microsoft.icon");
        }
        private bool SetResponseHeaders(string etag)
        {
            if (Request.Headers["If-None-Match"] == etag) { return false; }
            Response.Headers["Cache-Control"] = "max-age=315360000";
            Response.Headers["Etag"] = etag;
            Response.Headers["Date"] = DateTime.Now.ToString("r");
            Response.Headers["Expires"] = DateTime.Now.AddYears(100).ToString("r");
            return true;
        }
        private byte[] UseCompressBytes(string s)
        {
            var bytes = Convert.FromBase64String(s);
            var sp = Request.Headers["Accept-Encoding"].ToString().Replace(" ", "").ToLower().Split(',');
            if (sp.Contains("br")) {
                Response.Headers["Content-Encoding"] = "br";
            } else  {
                using (MemoryStream stream = new MemoryStream(bytes)) {
                    using (BrotliStream zStream = new BrotliStream(stream, CompressionMode.Decompress)) {
                        using (var resultStream = new MemoryStream()) {
                            zStream.CopyTo(resultStream);
                            bytes = resultStream.ToArray();
                        }
                    }
                }
                if (sp.Contains("gzip")) {
                    Response.Headers["Content-Encoding"] = "gzip";
                    using (MemoryStream stream = new MemoryStream()) {
                        using (GZipStream zStream = new GZipStream(stream, CompressionMode.Compress)) {
                            zStream.Write(bytes, 0, bytes.Length);
                            zStream.Close();
                        }
                        bytes = stream.ToArray();
                    }
                }
            }
            return bytes;
        }
    }
}
#endif