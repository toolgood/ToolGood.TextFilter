#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/code.css")]
        public IActionResult __layui_css_modules_code_css()
        {
            if (SetResponseHeaders("0D60FE38D6C99184C6B3AD6582727CB8") == false) { return StatusCode(304); }
            const string s = "G28EACwS7BgVo2JzTmj9Ql5+rsvP2d5/upCzqJiMjUAQ/vDk2/eFbXZKGvI75u1m6aEgZtkUZv7/3zvgD4hvjT+xtSzwO2rbsE+dAAMKvFDSqaJ69kMaTYgBm/XZs5/ZowjsYcbQBGTLUSIlg+cfBoN8DpYOttburfqLp53FybG9sHJci1LTe9xOZortVfW/48iwH8PrQwF/4ov0103CgSysqiIrLCGKz+dTaRjImuFyKKzyfqFSkdufSJ8/umQ1rkw5Q0aWXBC8iyie/RWMFkatT4RS1xYsaedw1jxUmjNzQFKTqX534y4rTc+bM707/zpnTRk4JfycLs16whEBmOV45DmN2qucRPdpwFT4HmiqeCojquO2dZZrDGoperp+Z+C8l8UNcNKTEtorRh/EwY34fUsQMy84BlRJLoFN4VomxaZ1H8mJUGrvICWp1EiOnkf0/Uqutd41TGqbvHSmcYpXyEHwIjfsiVEdc7b4snSJN8sdb7YZz5tn7nRVrydeAlIRROFBcs1BGBA4BFupOC4=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif