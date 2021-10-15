#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/laytpl.js")]
        public IActionResult __layui_lay_modules_laytpl_js()
        {
            if (SetResponseHeaders("7610091AD2E7B657105773811C5FFA70") == false) { return StatusCode(304); }
            const string s = "G2sHIBwJNq7w8dR0Vqpm/01FraQH7nGC3iGRPnNcxsquFBKUQEIxoOg6h9JZfv7+6ULOQjEZG4EgEJ4E+/+NuTqTulZWV9vpVOwfqqFAFus0HhUPldJ5AMfsxMHfNp8ekqckDC8H3zi+tG/su+THl19JW8BggBjTjnbqH/zd8prh79V/lkgynJYkTe7kn4m/S41qz//rOJgr/TPhpj7k8PGr/M0HFRzWoSAdHKPoxGZyU7ovxe0G1vydFDEi8lBuk/M6JXG5EZBYTk5/PvO8dowZKHI2APRGwZ6DiD8qpY2qbnc6u07nZ5dMDoZu927V2KWnHN12cb8/d6smM9bav6oHIDOzmnlN+Ty5ihsdYAmKnBebKsdmnN0hgzT49qPOK9W8PWxcPzdaz40PXZNRc8aIsXBi/wg4H/Yn8k8mZjJFKkoAyEADjx1DcsTIUZ7Ne8H34WW/Dj9Euvj/GD5wILuTEFkiWGUHHx+fJ/92jglvCoPOEseK0sqv/F+MPJahnZzXPBcHkV71zn4dxMVsbtd7mdusdnJO0yYUk2fGsdv+BdikPC8Ditm9yQvx7GSs0UOsplEFA8QFhEfNj9rdlcyLd1NHZ2fune1jyd629NjexIeYmjMME78oYEEB1nhdQkyQJXtJjq3D3dp52Pz+sYq2JqcKsZutdYd8SgcJ1BscxSqgUwFBYHc4xB56F6he7Vnpn9CWTjW1F9b4CEKbueCuMKAk+/DfMbI9W/Y7BW3FzXNYIVMm+A/fxIzKutwNRqCitP0vjkmCrARRMFplVkGuil1CRFzC3fECOZDqlax8RjQtNHvSnJ/XV/T4SIehBI0o4ej3iGhfq9ddala2TZOBokjsSdifGjCz22+92yMzBG92K5JAGW9UbRLASKay/fbiWX9pp1ZMvPRE/h/kK2HMiRKvgT2x10se2lc6aWl1b+JDBx/nF+RhjQ9seEEx1iOdSuDU4LgIeD7najZdrr0wc+bPntFOfiAta9uny4bQw464yHf/aqE9pY+5AjEsVx/EjoOiVy6ZSr/VYxdGpVu5cw78XeHx5Wr/4fUYKHWUdX6diNn0vwEBGEuKZCHFH/jIx3TVJ36ZgDKztu9eoqffiZGn9ih4YV/aZ/BII/RRCyA5ipQB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif