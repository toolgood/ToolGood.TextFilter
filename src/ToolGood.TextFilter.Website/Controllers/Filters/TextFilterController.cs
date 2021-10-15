/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Controllers
{
    [IgnoreAntiforgeryToken]
    public class TextFilterController : Controller
    {

        #region TextFilter HtmlFilter JsonFilter MarkdownFilter
        [Route("/api/text-filter")]
        public async Task<IActionResult> TextFilter(TextFilterRequest request)
        {
            TextFilterResult result = new TextFilterResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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
                #endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                var temp = TextFilterApplication.FindAll(request.Txt, skipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }
        [Route("/api/html-filter")]
        public async Task<IActionResult> HtmlFilter(TextFilterRequest request)
        {
            TextFilterResult result = new TextFilterResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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
                #endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                var temp = HtmlFilterApplication.FindAll(request.Txt, skipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }
        [Route("/api/json-filter")]
        public async Task<IActionResult> JsonFilter(TextFilterRequest request)
        {
            TextFilterResult result = new TextFilterResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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
                #endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                var temp = JsonFilterApplication.FindAll(request.Txt, skipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }
        [Route("/api/markdown-filter")]
        public async Task<IActionResult> MarkdownFilter(TextFilterRequest request)
        {
            TextFilterResult result = new TextFilterResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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
                #endregion
                bool skipBidi = false;
                if (string.IsNullOrWhiteSpace(request.SkipBidi) == false) {
                    if (Boolean.TryParse(request.SkipBidi, out bool b)) {
                        skipBidi = b;
                    } else {
                        skipBidi = request.SkipBidi == "1";
                    }
                }

                var temp = MarkdownFilterApplication.FindAll(request.Txt, skipBidi);
                TextFilterCommon.SetTextFilterResult(result, temp, request);
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


        #region TextReplace HtmlReplace JsonReplace MarkdownReplace
        [Route("/api/text-replace")]
        public async Task<IActionResult> TextReplace(TextReplaceRequest request)
        {
            TextReplaceResult result = new TextReplaceResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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

                var temp = TextFilterApplication.Replace(request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true, request.ContactReplace ?? true, skipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }
        [Route("/api/html-replace")]
        public async Task<IActionResult> HtmlReplace(TextReplaceRequest request)
        {
            TextReplaceResult result = new TextReplaceResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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

                var temp = HtmlFilterApplication.Replace(request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true, request.ContactReplace ?? true, skipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }
        [Route("/api/json-replace")]
        public async Task<IActionResult> JsonReplace(TextReplaceRequest request)
        {
            TextReplaceResult result = new TextReplaceResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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

                var temp = JsonFilterApplication.Replace(request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true, request.ContactReplace ?? true, skipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, request);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            var str = result.ToString();
            result = null;
            return Content(str, "application/json");
            //return Content(result.ToString(), "application/json");
        }
        [Route("/api/markdown-replace")]
        public async Task<IActionResult> MarkdownReplace(TextReplaceRequest request)
        {
            TextReplaceResult result = new TextReplaceResult();
            try {
                #region Check
                if (SysApplication.IsRegister() == false) {
                    result.Code = 1;
                    result.Message = "error: software not registered.";
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

                var temp = MarkdownFilterApplication.Replace(request.Txt, request.ReplaceChar[0], request.ReviewReplace ?? true, request.ContactReplace ?? true, skipBidi);
                TextFilterCommon.SetTextReplaceResult(result, temp, request);
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
