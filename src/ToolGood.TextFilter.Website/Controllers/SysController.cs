/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;

namespace ToolGood.TextFilter.Controllers
{
    [IgnoreAntiforgeryToken]
    public class SysController : Controller
    {
        [Route("/api/sys-update")]
        public async Task<IActionResult> UpdateSystem(SystemRequest request)
        {
            CommonResult result = new CommonResult();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                if (this.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase)) {
                    StreamReader streamReader = new StreamReader(Request.Body);
                    var txt = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    request = JsonConvert.DeserializeObject<SystemRequest>(txt);
                }
                if (request == null) {
                    result.Code = 1;
                    result.Message = "error: interval is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrEmpty(request.TextFilterNoticeUrl) == false && IsUrl(request.TextFilterNoticeUrl) == false) {
                    result.Code = 1;
                    result.Message = "error: textFilterNoticeUrl is error.";
                    return Content(result.ToString(), "application/json");
                }
                if (string.IsNullOrEmpty(request.TextReplaceNoticeUrl) == false && IsUrl(request.TextReplaceNoticeUrl) == false) {
                    result.Code = 1;
                    result.Message = "error: textReplaceNoticeUrl is error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                SysApplication.SetTextFilterNoticeUrl(request.TextFilterNoticeUrl);
                SysApplication.SetTextReplaceNoticeUrl(request.TextReplaceNoticeUrl);
                SysApplication.SetSkipword(request.Skipword);

            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        private static readonly Regex urlRegex = new Regex(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+ %\$#_=]*)?$", RegexOptions.Compiled);
        private bool IsUrl(string url)
        {
            return urlRegex.IsMatch(url);
        }


        [Route("/api/sys-refresh")]
        public IActionResult Refresh()
        {
            CommonResult result = new CommonResult();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                SysApplication.Refresh();
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }



        [Route("/api/sys-init-Data")]
        public IActionResult InitData()
        {
            CommonResult result = new CommonResult();
            try {
                SysApplication.Init();
                GC.Collect();
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        /// <summary>
        /// GC 回收
        /// </summary>
        /// <returns></returns>
        [Route("/api/sys-GC-Collect")]
        public IActionResult GCCollect()
        {
            CommonResult result = new CommonResult();
            try {
                GC.Collect();
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }




    }
}
