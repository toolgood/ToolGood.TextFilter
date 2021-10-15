using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ToolGood.HtmlExtract.HtmlAgilityPack.Selectors;

namespace ToolGood.HtmlExtract.HtmlAgilityPack
{
    public abstract class CssSelector
    {
        #region Constructor

        protected CssSelector()
        {
            SubSelectors = new List<CssSelector>();
        }
        #endregion

        #region Properties
        private static readonly CssSelector[] Selectors = FindSelectors();
        public abstract string Token { get; }
        //protected virtual bool IsSubSelector => false;
        public virtual bool AllowTraverse => true;

        public IList<CssSelector> SubSelectors { get; set; }
        public string Selector { get; set; }
        #endregion

        #region Methods
        protected internal abstract IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes);

        public IEnumerable<HtmlNode> Filter(IEnumerable<HtmlNode> currentNodes)
        {
            var nodes = currentNodes;
            IEnumerable<HtmlNode> rt = FilterCore(nodes).Distinct();

            if (SubSelectors.Count == 0)
                return rt;

            foreach (var selector in SubSelectors)
                rt = selector.FilterCore(rt);

            return rt;
        }


        public static IList<CssSelector> Parse(string cssSelector)
        {
            var tokens = Tokenizer.GetTokens(cssSelector);

            return tokens.Select(ParseSelector).ToList();
        }

        private static CssSelector ParseSelector(Token token)
        {
            var selector = char.IsLetter(token.Filter[0])
                ? Selectors.First(i => i is TagNameSelector)
                : Selectors.Where(s => s.Token.Length > 0).FirstOrDefault(s => token.Filter.StartsWith(s.Token));

            if (selector == null)
                throw new InvalidOperationException($"Invalid token : {token.Filter}.");

            var selectorType = selector.GetType();
            var rt = (CssSelector)Activator.CreateInstance(selectorType);

            string filter = token.Filter.Substring(selector.Token.Length);
            rt.SubSelectors = token.SubTokens.Select(ParseSelector).ToList();

            rt.Selector = filter;
            return rt;
        }

        private static CssSelector[] FindSelectors()
        {
            var defaultAsm = typeof(CssSelector).GetTypeInfo().Assembly;
            Func<Type, bool> typeQuery = type => type.GetTypeInfo().IsSubclassOf(typeof(CssSelector)) && !type.GetTypeInfo().IsAbstract;

            var defaultTypes = defaultAsm.GetTypes().Where(typeQuery);
            var types = defaultAsm.GetTypes().Where(typeQuery);
            types = defaultTypes.Concat(types);

            var rt = types.Select(Activator.CreateInstance).Cast<CssSelector>().ToArray();
            return rt;
        }

        #endregion
    }
}