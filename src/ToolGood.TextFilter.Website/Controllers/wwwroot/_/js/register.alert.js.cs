#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/register.alert.js")]
        public IActionResult __js_register_alert_js()
        {
            if (SetResponseHeaders("75854DFEDBC82A39EEB6EE8BF937E001") == false) { return StatusCode(304); }
            const string s = "G8AAACwObCc0EGHgjdK7bH2g4pludL/pgfFNO9vgvJDrTjFBVF5CIUFc5nRywNx2oKDtrcCS2DKIWuAmy2adkI5zOEnn9fpxXvismDsCXqebIEN0MEbuQHDSp/CJ3OZdiYIjGbsPe0heHeG44ltMCOnB9DvnIQZgETnuyyaH3MW6WbcRJI4gfQDpDS13Bi3UxQ8V97VnOg==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif