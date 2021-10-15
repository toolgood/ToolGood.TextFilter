#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.TextFilter.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/jquery.serializeJson.js")]
        public IActionResult __js_jquery_serializeJson_js()
        {
            if (SetResponseHeaders("6792BD6F8E0ECF3B7DE05A4A2BCEA95E") == false) { return StatusCode(304); }
            const string s = "G4EIACwKbGNi+BDj8sZvnTFSelhXM0ug4iHOr3BQeZPOPjgv5LpTtFTVPcskEwbzZ71bo/LQ5mHKRFIurdNsW8qQoGnLRm6TL4+3/ZK6uZgGQ1BW/6Ole9A/91NbYNerrRMsfeAlDZRowJz+jVU3NUXC8XkUbnx+Rk/oaTkT6XxocHtNFQy+agq5n6gQXWwdC9ecX+FZMXcEfE63QYbo8DEIHrBwB33KfTh49vLf4hCyEKJlJQP7iB7SV0c4ju80b7EhtOrH8jv6oX0BJiPf/C/bHLKLc7O2ESSOID0A5Q2VOwLwG+MKP7RCcGm7HVF+qvsE8VnNbwNB9XSKKEi70vorG4ZHuym66FHv7z5LZUwkyt4gw9gWj/CVzoBdKpJ6zxIBtEAxZ0uhxvYkBIoVUpWAUFS+X6cpO9/1MFzOIyAvqXpT1TIePkXZc85fc/5ZIfyzWv7Z3qy8WnBXYJztlj88F6yGiL7UGFW3ASu7y7onUFHRaE44eEhM13duSegd4wuGe1m9osI/K1KbIR9JRaGiU/u1v1xmjOTg/5Jnz6ZnOLjY2fiwyMspN+cZ78RFbtcv5har6aW0ujy5cFZKe5clyW3NRPRts9auwy4DY3IxpXwSzCZn6OW1nu5xObr899vLcGkdbbdPJDRgU0v5BMaCSjxq47HbMdEMua7E1RSauWNf83Jse1hQ10DsYS36cuQF9xBptPj4ShNIv8ZWoR2mNyuNf+tsb0tr5yXh+4ScthZZMet1FCTGPIHP1YFY+fu9nLIPOnVbdKOAYSEoctfwheIkkBIMWrm9YscDWXTKpYOr/U7I/h1sP6TAIS7jEqVYUkn0h9cOjiuOcY5jxAhOyFkAVyyXZRyeW/gDEZnNyiwe/YGScp9KBXhHru+zLIbP8uXS7DmbEMHBoFGqXPtCSiu5uTvjP6v9plWY88glsu0wYAYpGqViabwPMOMcm0UsQFphTOjsrnYFGAj3aGLI3KGjX2zh0fm+dJmdV6Pt6j171nRW0BWCiBKup6xfXGJbjSBQjZOTTc6l8DC0ppVIJZ3JwVywVx1nWfMszqTse0kL/7iJlYXvo8wpEpLuXyERJW8mX2y/17IhgDMkaQfDzJ+822Di9Z6Ywt+5ku1v7znBrYrEv/Ep6Eu2o9dtVQTvZDEwLBVnN5pOl4gKDr7jPYdZ74sB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif