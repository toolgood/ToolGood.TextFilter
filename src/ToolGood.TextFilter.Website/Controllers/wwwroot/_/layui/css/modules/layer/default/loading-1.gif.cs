#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/layer/default/loading-1.gif")]
        public IActionResult __layui_css_modules_layer_default_loading_1_gif()
        {
            if (SetResponseHeaders("1140BC5C7863F8E54A3C2B179E640758") == false) { return StatusCode(304); }
            const string s = "G7wC8C4L7GC87LuACCVCvPq01ecfm9fjOQUPcT/f754mrTHKduMhZpgWw7oFOpyWQS5uhsOIuoQCTCh8yKCBcToimQfRADgUu8Js7yK8ZGsZVZmemKKyZjug7W5AOx9NcN1z1A0hhMC6CwKglIrQcoxrw3GepmH1j8cTM422t4AyF/R4D1bJ//8fyP+tsLO9IH+yUl0BLAD5p6yYiFQBypRB3FE5vPmMzx/bxfTxmPuZxEmG85Y2wfttcgNCCvpqJaay+2FS2J5z2Ciu5CzLas+eev0cqqw6ZX6Dd9hfstnCdmeLMH6v02v6EJ8arre7bVJkJF2HdyT//RaQrcL9G+W7VrC8pn7DrPdvliGnxQ8XIntJgemuaGLIPKUkxarkfYCIjCXKfnGjJQAF81KCfDZ5ev+oONrGX265lSDfOp8D1TEDBNhILkApeRLh0zwfgnFDRkL8V2xtX2TMgDGSy1BIMEEVlFav5jkWd+f7ClkIYD4JJJehkBSF3gU=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif