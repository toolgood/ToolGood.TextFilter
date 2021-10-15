/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Models;

namespace ToolGood.TextFilter.Controllers
{
    [IgnoreAntiforgeryToken]
    public class KeywordsController : Controller
    {
        [Route("/api/get-keyword-list")]
        public async Task<IActionResult> GetKeywordList(KeywordListRequest request)
        {
            KeywordListResult result = new KeywordListResult();
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
                    request = JsonConvert.DeserializeObject<KeywordListRequest>(txt);
                }
                if (request == null) {
                    result.Code = 1;
                    result.Message = "error: content is null.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                int total = 0;
                var list = KeywordApplication.GetKeywordList(ref total, request.Type, request.Text, request.Page, request.PageSize);

                result.Total = total;
                result.Items = new List<KeywordItem>();
                foreach (var item in list) {
                    result.Items.Add(new KeywordItem() {
                        Id = item.Id,
                        Type = item.Type,
                        Text = item.Text,
                        Comment = item.Comment,
                        AddingTime = item.AddingTime,
                        ModifyTime = item.ModifyTime,
                    });
                }
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        [Route("/api/add-keyword")]
        public async Task<IActionResult> AddKeyword(KeywordAddRequest request)
        {
            CommonResult result = new CommonResult();
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
                    request = JsonConvert.DeserializeObject<KeywordAddRequest>(txt);
                }
                if (request == null || string.IsNullOrEmpty(request.Text)) {
                    result.Code = 1;
                    result.Message = "error: text is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (request.Type < 0 && request.Type > 3) {
                    result.Code = 1;
                    result.Message = "error: type is error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion


                KeywordApplication.AddKeyword(request.Text, request.Type, request.Comment);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        [Route("/api/edit-keyword")]
        public async Task<IActionResult> EditKeyword(KeywordEditRequest request)
        {
            CommonResult result = new CommonResult();
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
                    request = JsonConvert.DeserializeObject<KeywordEditRequest>(txt);
                }
                if (request == null || string.IsNullOrEmpty(request.Text)) {
                    result.Code = 1;
                    result.Message = "error: text is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (request.Type < 0 && request.Type > 3) {
                    result.Code = 1;
                    result.Message = "error: type is error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion


                KeywordApplication.SetKeyword(request.Id, request.Text, request.Type, request.Comment);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        [Route("/api/delete-keyword")]
        public async Task<IActionResult> DeleteKeyword(KeywordDeleteRequest request)
        {
            CommonResult result = new CommonResult();
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
                    request = JsonConvert.DeserializeObject<KeywordDeleteRequest>(txt);
                }
                if (request == null || request.Id <= 0) {
                    result.Code = 1;
                    result.Message = "error: id is null.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                KeywordApplication.DeleteKeyword(request.Id);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }


    }
}
