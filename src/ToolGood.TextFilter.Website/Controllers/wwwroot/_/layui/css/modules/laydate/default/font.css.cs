#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/laydate/default/font.css")]
        public IActionResult __layui_css_modules_laydate_default_font_css()
        {
            if (SetResponseHeaders("92A203AC640798EC7DA04C24F15FB53F") == false) { return StatusCode(304); }
            const string s = "GzECILwMbwzoezhUmp7bRfgJ08B6ftSKyMj6hbz8XJefs73/dCFnUTEZG4Eg/OHJt+8J4gKUKiAvedJkfrYtdcTv2Yh8S6aopEJIW6XsdM1NEpWpm6Khpvhjtc7HBIzXxnAsgUAKuFjSOU18PTwUilGgyZ4dLPuK0Mi9Gzi6SPVrQVCdQ5oFKx+qXzztLE6O7YU1xTEncXY77UxMYG2snx0H7nobXh8K+BPRZBcjP2HiPVaYe3LfSgFHRfLrwnrfm9BApVXA+eVIjFsOEYuNkObK/SnJD0SggnPBwSEw/Mu5xl5aD0XW+QE0tYzO5yglGEpq9dG2NRg7fJrRjy5fdIssGkRRzbMLEgBLYJ1COPw6pqQGSiuMYwU9vyTDZog3ZBUcw+MD43rBIBsQCZQf";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif