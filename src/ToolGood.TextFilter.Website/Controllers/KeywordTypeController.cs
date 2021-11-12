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
    public class KeywordTypeController : Controller
    {
        [Route("/api/get-keywordtype-list")]
        public IActionResult GetList()
        {
            KeywordtypeListResult result = new KeywordtypeListResult();
            try {
                #region Check
                if (SysApplication.LoadTextDataError()) {
                    result.Code = 1;
                    result.Message = "error: Load data error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                var list = KeywordApplication.GetKeywordTypeList();
                result.Items = new List<KeywordtypeItem>();
                foreach (var item in list) {
                    result.Items.Add(new KeywordtypeItem() {
                        Code = item.Code,
                        TypeId = item.TypeId.ToString(),
                        ParentId = item.ParentId.ToString(),
                        Name = item.Name,
                        Level_1_UseType = item.Level_1_UseType ?? (int)IllegalWordsRiskLevel.Review,
                        Level_2_UseType = item.Level_2_UseType ?? (int)IllegalWordsRiskLevel.Reject,
                        Level_3_UseType = item.Level_3_UseType ?? (int)IllegalWordsRiskLevel.Reject,
                        UseTime = item.UseTime,
                        StartTime = item.StartTime?.ToString("MM-dd") ?? "",
                        EndTime = item.EndTime?.ToString("MM-dd") ?? "",
                    });
                }
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }

        [Route("/api/set-keywordtype")]
        public async Task<IActionResult> SetKeywordType(KeywordTypeEditRequest request)
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
                    request = JsonConvert.DeserializeObject<KeywordTypeEditRequest>(txt);
                }
                if (request == null || request.TypeId <= 0) {
                    result.Code = 1;
                    result.Message = "error: TypeId is null.";
                    return Content(result.ToString(), "application/json");
                }
                if (request.Level_1_UseType < 0 && request.Level_1_UseType > 3) {
                    result.Code = 1;
                    result.Message = "error: Level_1_UseType is error.";
                    return Content(result.ToString(), "application/json");
                }
                if (request.Level_2_UseType < 0 && request.Level_2_UseType > 3) {
                    result.Code = 1;
                    result.Message = "error: Level_2_UseType is error.";
                    return Content(result.ToString(), "application/json");
                }
                if (request.Level_3_UseType < 0 && request.Level_3_UseType > 3) {
                    result.Code = 1;
                    result.Message = "error: Level_3_UseType is error.";
                    return Content(result.ToString(), "application/json");
                }
                #endregion

                KeywordApplication.SetKeywordType(request.TypeId, request.Level_1_UseType, request.Level_2_UseType, request.Level_3_UseType
                    , request.UseTime, request.StartTime, request.EndTime);
            } catch (Exception ex) {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return Content(result.ToString(), "application/json");
        }


    }
}
