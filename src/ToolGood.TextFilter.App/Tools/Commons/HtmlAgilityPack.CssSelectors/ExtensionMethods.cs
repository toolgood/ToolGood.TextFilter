using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.HtmlExtract.HtmlAgilityPack
{
    public static partial class HapCssExtensionMethods
    {
  
        public static IList<HtmlNode> QuerySelectorAll(this HtmlDocument doc, string cssSelector)
        {
            return doc.DocumentNode.QuerySelectorAll(cssSelector);
        }

        public static IList<HtmlNode> QuerySelectorAll(this HtmlNode node, string cssSelector)
        {
            return new[] { node }.QuerySelectorAll(cssSelector);
        }
        public static IList<HtmlNode> QuerySelectorAll(this IEnumerable<HtmlNode> nodes, string cssSelector)
        {
            if (cssSelector == null)
                throw new ArgumentNullException(nameof(cssSelector));

            if (cssSelector.Contains(','))
            {
                var combinedSelectors = cssSelector.Split(',');
                var rt = nodes.QuerySelectorAll(combinedSelectors[0]);
                foreach (var s in combinedSelectors.Skip(1))
                    foreach (var n in nodes.QuerySelectorAll(s))
                        if (!rt.Contains(n))
                            rt.Add(n);

                return rt;
            }

            cssSelector = cssSelector.Trim();

            var selectors = CssSelector.Parse(cssSelector);

            bool allowTraverse = true;

            for (int i = 0; i < selectors.Count; i++)
            {
                var selector = selectors[i];

                if (allowTraverse && selector.AllowTraverse)
                {
                    // If this is not the first selector then we must make filter against the child nodes of the current set of nodes
                    // since any selector that follows another selector always scopes down the nodes to the descendants of the last scope.
                    // Example: "span span" Should only resolve with span elements that are descendants of another span element.
                    // Any span elements that are not descendant of another span element shoud not be included in the output.
                    if (i > 0)
                    {
                        nodes = nodes.SelectMany(n => n.ChildNodes);
                    }

                    nodes = Traverse(nodes);
                }

                nodes = selector.Filter(nodes);
                allowTraverse = selector.AllowTraverse;
            }

            return nodes.Distinct().ToList();
        }


        private static IEnumerable<HtmlNode> Traverse(IEnumerable<HtmlNode> nodes)
        {
            foreach (var node in nodes)
                foreach (var n in Traverse(node).Where(i => i.NodeType == HtmlNodeType.Element))
                    yield return n;
        }
        private static IEnumerable<HtmlNode> Traverse(HtmlNode node)
        {
            yield return node;

            foreach (var child in node.ChildNodes)
                foreach (var n in Traverse(child))
                    yield return n;
        }

    
    }
}