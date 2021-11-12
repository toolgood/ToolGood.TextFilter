/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using Microsoft.AspNetCore.Mvc;
using ToolGood.TextFilter.Application;

namespace ToolGood.TextFilter.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        #region 首页
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 敏感词管理

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

            ViewData["Skipword"] = SysApplication.GetSkipword();

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
