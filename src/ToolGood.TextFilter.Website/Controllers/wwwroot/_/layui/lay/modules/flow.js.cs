#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/flow.js")]
        public IActionResult __layui_lay_modules_flow_js()
        {
            if (SetResponseHeaders("C977D078490C1F26FC75D55448311C87") == false) { return StatusCode(304); }
            const string s = "GyYIAJwJdswJ1GFxR43+n2rut0w2ycP/n76xUprRl1O3ZLstVadDES7gCHA7NGxbWpafv79tY2+PwyFuDXTUaNRoSWSRNDU2KxYasXHUwDtiljyJ2DyRN29P45YsuaalJKZjMN+BWCP0FbddXE+vJteTbXNOdn54rJ6pF5NPN48LguF1rG5osVd9g785ncX5d7Z/PojpODZKrN6+P7t/MjXcf6EZq1Wy+IPCFXwo4Kc0y+KqDFoFqQPEc67bBVX6aHB4L1IwkxAH20ahhWSNHHHiIVk4MkcaeFbZR4nhym1pnN1NzvLmg49NNFRoZoQGvfJ9RC0fnMzzJ0ZXXxa2nmmrcFYreQ1wAWGZijry1FPLArscnbmkRAcyFLTtwLHLebN/4ghOma3ZISVvg2Jr9iFYMKoP4facVeYdkI7s1O4252cl3fIUhA1vUvTCuonDnJ2iHUYbdsq4u5yFd8HuiPf7oqAtp79ESop/ypzdidSwqFobTf2H4n9VKp7qy4LOE2Q5wkx9MGDQyrRz79JSt/bWop4VTSGqL4umPpG+s4da2I06fUkdywmVHvK4vpxTQ7MVLNIBcyMNc7XDLYraKiuD54sGLpmHnH1YRA8ra+JJ2ODSZuVtBNJLG9SeB90x+6wgZQAcyajSzh1PSx1ZMOgFo08w96PMTtm7VcrT/4CiONMBRwIDSMlEvAPx9IH9lJsuto4v9jl3ORvAEckpGz42l7Pd/UQpIXCrtiQz3GuQdqlInkJMHpnWR7uB5b5kRKTbFewzx0HLCfJDNsdlcJDVlVk3fg+oeym9fyn0qKdeyvRdhcCyrdyzE0GJJwbertw/ssOHy+hFDUi2UOKCbRQtIX67eA5cuml6hrTCA4r3UJ3mdj5UfJQS0FNMY/SoLBHFKVpctuNouXjYfO8NNBiuoPOR1/aK/My41jqtLQtCmrOYD2Nr/X0hrBTnE54t/jFq2RfcDrOa0/6t1v3/O/ioot/PLWmKwo+AS7YXUdB9zV7KvuJWyqlTTYwDiDC0QsMIFZq1Zh6iJfcKdW5o0Bp/5AFOhAFPmV/QoU7ZRwEvdwx5Mz9Sx8/lVykTRNRdUYxSG0iYSNIX37JZQcrZohYNa6Db78xNsSVPgQ6et0lZj+IILZLDHhxtUXfsP6DzA3guta/42cM0d+WLAm0Hodqre6SDjOQPokZGOK6HQFukjj0d6jN2CnSjn/zukAApYM5gTXk57U1X4MfxpmpJ0It7k8ORnpU4IgWelkhp1A5Et/VHQe/VOGxxRA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif