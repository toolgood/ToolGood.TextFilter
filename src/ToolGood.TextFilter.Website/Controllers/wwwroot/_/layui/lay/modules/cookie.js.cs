#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/cookie.js")]
        public IActionResult __layui_lay_modules_cookie_js()
        {
            if (SetResponseHeaders("7D278F6CFCCC83FEEC3C09C3F472FD63") == false) { return StatusCode(304); }
            const string s = "G/sFAKwKbGNh2EOUN4bnTrrzS0OnhkAHh5gotP1Zg/NCrpPOCDDdvt9seYyiEbMJDZc7/6tb4VaVilKuATZDb84SMYdqba2JA1M17HFYk+nZphmiKnO2ZkklKVdZI8hWir25KSA5AFCggF3rgN2DREkk5dsw569mia8c1GznZhQNhqOh0smFzDl/4Nl7zKY2Z9S01g1xjIyHEPa4VOijnGvrd4sD3t77dptBHsofSgH3AcoJf/zXtIGZC26TKVcwHW8B5M3izzU+SZzhhzmxrgj0sqeDW/5s/V8uw5bSWdm9lDlezGy0NDco0FLXGk5Nao+XMwlET70AmbvJOf0dk/x/n4RJnHOiMJdI6gglnqGgZdtySS55bqTcsZSRw+vZJli3aiZV3ie2IpDPpM5EHcXfH/gPpCQ1iBFlcnJNZIhcf3yySSa8/SbOmaHD+OZu+pEFZpdjg73fkLW54zlYTtluaXpS2636JKUMFGSeJ0xTPaM00hB/meNrWqUhMN60yII4a2zCuuTlfNNoxV4rIJhbjKSEXwX2XBq3ia9/K3slnyNuK2m/tdNtPD1pA5sX0sJvHm41aP2dp+Z61k4Lva/9kUMEyItF56JAPEEI213D/gYdsffXZsJ5sVprEU+WesLlboDu0i3hHD9h1qe5WgsBOq/55CZf/4njPH6OFi8/IOc/i9RzKxJIDxxEzyIJrYdKHyg2ypk04S+BEZAQuQLxAxkM0w2UaIxyBFJU0TWWWKNkY6BKQUbK7K2GPEkkcwwVn9EmT1607f21ww/plehJZT1pnkbSzAIz7+iOr8suNvd1Eceky+vRpUXmqhIk7nDk/lWRZAne0McJLAB139M61wLYyTmj4psGGVGd9v0/H75pzwsLAKqtOae7bG9Sws+dv0NtOdrBsZuEaAcxX6OeT6UAHOQmhLUkmofNV5RIUeXx+qbeFavxvGBkP1VoKYrwGJOJkXQq75QC";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif