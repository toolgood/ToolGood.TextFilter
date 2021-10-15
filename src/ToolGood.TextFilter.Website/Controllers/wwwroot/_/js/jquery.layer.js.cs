#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/jquery.layer.js")]
        public IActionResult __js_jquery_layer_js()
        {
            if (SetResponseHeaders("FE41AF56B2262C419A29A2956B25FA06") == false) { return StatusCode(304); }
            const string s = "G+YEQJwJtq2E7TKDxTsPcqDi+U3uF9M36WyD80KuO8VLp04r/vh0//BuTURhmTK5QpeLLick11BJUgfrTBNC/JZX0+MGIoGRjGRJy3CaW5VKdP7M6pRMWHHDXnCCMDhlwZyuOD/gWTF3BHxOt0GG6DRjwB20YDt92vgwY0X71+LgTkHfPJKhGaWH9NUNrccLAkZ5iw0hBR+W3yEI2wAMJFfsyzaH7OLcrG0Eic8TP4DyhsqdOfzIOPUPzcz72ufUjGrxkEHDjjVb+pxLL6Gag1qZ8vGzmt9FgMhEoHB7EMnXPiRg0DXUqNyh168pKcrSmvsNRziWcr4kqh7NRUhCNCGSAF2stV6VCA9gR1ydoDdxsVdeKYBx///jFUDSR6gWLdDfv7GPBpPxWPq4UpUqrwqaOH/UvIp9/DscbhbfXgj0StqcYm9gRavYVa4YFoFr+vL62yvgfyCH5IMYi5zyj81J3voI/p7A1RHRfnRJP2VvzG2TFqUczrk17pKGU6NvcjDh1heUW7fn5ujMUmtDPDkNPZ0w4Vv6vGkj4TdZhaxXGHu1ELrOARKarfnLO2SATilUEw6QlXvquDSFJNq9VgiL1HXU9RGeahoKpQBvKzBkNPLQBthQ5EZU18i9f42BYiLA+TaIUOgWwD7g3pg3DVYpIalzliiTJUD//4JBk96GOGKXIbG2x9m0yHBEa/Oz+9Kra9YCIU4QtVgesv55uDjnC1PUsg4hFLK1Vohz8PTmkA/xjpifkFJjIVBHSvejEqAfNT5EBEgQ80roQcjNpH0MQBEoiEqDuxy6MxN1u3g2aFkzQohNm8QVoJ7QXNMR";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif