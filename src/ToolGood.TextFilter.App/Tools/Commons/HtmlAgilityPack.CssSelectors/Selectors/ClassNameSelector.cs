using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.HtmlExtract.HtmlAgilityPack.Selectors
{
    internal class ClassNameSelector : CssSelector
    {
        public override string Token => ".";

        protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
        {
            foreach (var node in currentNodes)
            {
                if (node.GetClassList().Any(c => c.Equals(Selector, StringComparison.OrdinalIgnoreCase)))
                    yield return node;
            }
        }
    }
}
