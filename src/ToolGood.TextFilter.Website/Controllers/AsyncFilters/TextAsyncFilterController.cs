/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if Async
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;

namespace ToolGood.TextFilter.Controllers
{
    [IgnoreAntiforgeryToken]
    public class TextAsyncFilterController : Controller
    {

#region filter
        [Route("/api/async/text-filter")]
        public async Task<IActionResult> TextFilter(TextFilterRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextFilterRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.TextFilter(request.RequestId, request.Url, request.Txt, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/html-filter")]
        public async Task<IActionResult> HtmlFilter(TextFilterRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextFilterRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.HtmlFilter(request.RequestId, request.Url, request.Txt, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/json-filter")]
        public async Task<IActionResult> JsonFilter(TextFilterRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextFilterRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.JsonFilter(request.RequestId, request.Url, request.Txt, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/markdown-filter")]
        public async Task<IActionResult> MarkdownFilter(TextFilterRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextFilterRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextFilterNoticeUrl();
                }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.MarkdownFilter(request.RequestId, request.Url, request.Txt, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

#endregion

#region replace

        [Route("/api/async/text-replace")]
        public async Task<IActionResult> TextReplace(TextReplaceRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextReplaceRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                if (string.IsNullOrWhiteSpace(request.ReplaceChar)) { request.ReplaceChar = "*"; }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.TextReplace(request.RequestId, request.Url, request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true,
                     request.ContactReplace ?? true, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/html-replace")]
        public async Task<IActionResult> HtmlReplace(TextReplaceRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextReplaceRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                if (string.IsNullOrWhiteSpace(request.ReplaceChar)) { request.ReplaceChar = "*"; }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.HtmlReplace(request.RequestId, request.Url, request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true,
                     request.ContactReplace ?? true, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/json-replace")]
        public async Task<IActionResult> JsonReplace(TextReplaceRequest request)
        {
            CommonResult result = new CommonResult();
            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextReplaceRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                if (string.IsNullOrWhiteSpace(request.ReplaceChar)) { request.ReplaceChar = "*"; }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.JsonReplace(request.RequestId, request.Url, request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true, 
                    request.ContactReplace ?? true, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

        [Route("/api/async/markdown-replace")]
        public async Task<IActionResult> MarkdownReplace(TextReplaceRequest request)
        {
            CommonResult result = new CommonResult();

            try {
#region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.HasGrpcLicence() == false) {
                    result.Code = 1;
                    result.Message = "error: grpc/async not licenced.";
                    return Content(result.ToString(), "application/json");
                }
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<TextReplaceRequest>(txt);
                }
                if (request == null || string.IsNullOrWhiteSpace(request.Txt)) {
                    result.Code = 1;
                    result.Message = "error: txt is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrWhiteSpace(request.RequestId)) {
                    request.RequestId = Guid.NewGuid().ToString();
                }
                result.RequestId = request.RequestId;
                if (string.IsNullOrWhiteSpace(request.Url)) {
                    request.Url = SysApplication.GetTextReplaceNoticeUrl();
                }
                if (string.IsNullOrWhiteSpace(request.ReplaceChar)) { request.ReplaceChar = "*"; }
#endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                FilterQueueApplication.MarkdownReplace(request.RequestId, request.Url, request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true
                    , request.ContactReplace ?? true, skipBidi, request.OnlyPosition);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }

#endregion





    }
}
#endif