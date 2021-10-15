#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/treetable.css")]
        public IActionResult __layui_css_treetable_css()
        {
            if (SetResponseHeaders("91427F13AFB444399EC35D22FBA9AA83") == false) { return StatusCode(304); }
            const string s = "G4oBYKwLeLI5qkcqR+8rS/hjrJXWCvHqF/Lyc10ucv7+bRt7Oz3iFBUMytGo0TKIjl2SHjY2lmUJ9mONYjuvJvNSCMkaKnc/dzww9+6S7/y2yG8lLYvz5oG1BViq5/MNbWLrxHD5hQQExHuv+WEEAYQOBQd/39DQQCx4gCkGQSnebD+s6SxBlh+JTfqcq6z63B6CNdi0fkcOOI/x+rAGf9ogkpArvVzyPCA2h0mK9QRufwSc23trnGJ/gDmRceXbUseh7HKe2cjF10ubtqUHXiXGKWeYZK7o0kFwPw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif