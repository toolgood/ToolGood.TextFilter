/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToolGood.TextFilter.Application;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["isRegister"] = SysApplication.IsRegister() ? "1" : "0"; // 是否注册
            ViewData["serviceEnd"] = SysApplication.ServiceEnd()?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""; // 服务结束日期

#if image
            bool hasImage = true; ;
#else
            bool hasImage = false;
#endif
#if browser
            bool hasBrowser = true; ;
#else
            bool hasBrowser = false; ;
#endif
#if Async
            bool hasGrpc = true; ;
#else
            bool hasGrpc = false; ;
#endif
 
            ViewData["hasImage"] = hasImage;
            ViewData["hasBrowser"] = hasBrowser;
            ViewData["hasGrpc"] = hasGrpc;

            base.OnActionExecuting(context);
        }

        #region 首页
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 敏感词管理
        [HttpGet("/keywords")]
        public IActionResult Keywords()
        {
            return View();
        }

        [HttpGet("/keywordsEdit")]
        public IActionResult KeywordsEdit(int id = 0)
        {
            DbKeyword keyword = null;
            if (id > 0) {
                keyword = KeywordApplication.GetKeyword(id);
            }
            if (keyword == null) {
                keyword = new DbKeyword();
            }
            ViewData["keyword"] = keyword;
            return View();
        }

        [HttpGet("/KeywordTypes")]
        public IActionResult KeywordTypes()
        {
            return View();
        }

        [HttpGet("/KeywordTypeEdit")]
        public IActionResult KeywordTypeEdit(int id)
        {
            if (id <= 0) { return Redirect("/KeywordTypes"); }

            var keyowrd = KeywordApplication.GetKeywordType(id);
            if (keyowrd == null) {
                return Redirect("/KeywordTypes");
            }
            ViewData["keyword"] = keyowrd;

            return View();
        }


        [HttpGet("/Sys")]
        public IActionResult Sys()
        {
            ViewData["TextFilterNoticeUrl"] = SysApplication.GetTextFilterNoticeUrl();
            ViewData["TextReplaceNoticeUrl"] = SysApplication.GetTextReplaceNoticeUrl();
            ViewData["ImageFilterNoticeUrl"] = SysApplication.GetImageFilterNoticeUrl();
            ViewData["ImageClassifyNoticeUrl"] = SysApplication.GetImageClassifyNoticeUrl();
            ViewData["ImageTempPath"] = SysApplication.GetImageTempPath();

            ViewData["Skipword"] = SysApplication.GetSkipword();

            return View();
        }

        #endregion

        #region 接口文档

        [HttpGet("/apiDoc")]
        public IActionResult ApiDoc()
        {
            return View();
        }
        #endregion

        #region 产品信息
        [HttpGet("/about")]
        public IActionResult About()
        {
            ViewData["version"] = SysApplication.GetVersion(); // 版本号
            ViewData["machineCode"] = SysApplication.GetMachineCode(); // 机器码
            ViewData["isRegister"] = SysApplication.IsRegister() ? "已注册" : "未注册"; // 是否注册
            var end = SysApplication.ServiceEnd();
            if (end != null && end.Value < DateTime.Now) {
                ViewData["isRegister"] = "已过期";
            }
            ViewData["register"] = SysApplication.GetRegister(); // 注册人
            ViewData["serviceStart"] = SysApplication.ServiceStart()?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""; // 服务开始日期
            ViewData["serviceEnd"] = SysApplication.ServiceEnd()?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""; // 服务结束日期
            ViewData["licenceTxt"] = SysApplication.GetLicenceTxt();

            return View();
        }
        #endregion

        #region 服务条款
        [HttpGet("/legal")]
        public IActionResult Legal()
        {
            return View();
        }
        #endregion


    }
}
