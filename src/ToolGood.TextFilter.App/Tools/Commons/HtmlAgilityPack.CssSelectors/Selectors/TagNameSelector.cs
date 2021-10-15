using System;
using System.Collections.Generic;

namespace ToolGood.HtmlExtract.HtmlAgilityPack.Selectors
{
    internal class TagNameSelector : CssSelector
    {
        public override string Token => string.Empty;

        protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
        {
            foreach (var node in currentNodes)
            {
                if (node.Name.Equals(Selector, StringComparison.OrdinalIgnoreCase))
                    yield return node;
            }
        }
    }
}
