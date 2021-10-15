using System.Linq;

namespace ToolGood.HtmlExtract.HtmlAgilityPack.PseudoClassSelectors
{
    //[PseudoClassName("last-child")]
    internal class LastChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlNode node, string parameter)
        {
            return node.ParentNode.GetChildElements().Last() == node;
        }
    }
}