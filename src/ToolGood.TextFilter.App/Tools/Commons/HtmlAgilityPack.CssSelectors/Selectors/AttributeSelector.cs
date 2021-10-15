using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.HtmlExtract.HtmlAgilityPack.Selectors
{
    internal class AttributeSelector : CssSelector
    {
        public override string Token => "[";

        protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
        {
            var filter = GetFilter();
            foreach (var node in currentNodes)
            {
                if (filter(node))
                    yield return node;
            }
        }

        private Func<HtmlNode, bool> GetFilter()
        {
            string filter = Selector.Trim('[', ']');

            int idx = filter.IndexOf('=');

            if (idx == 0)
                throw new InvalidOperationException($"Invalid selector use for attribute {Selector}.");

            if (idx < 0)
                return node => node.Attributes.Contains(filter);

            var operation = GetOperation(filter[idx - 1]);

            if (!char.IsLetterOrDigit(filter[idx - 1]))
                filter = filter.Remove(idx - 1, 1);

            string[] values = filter.Split(new[] { '=' }, 2);
            filter = values[0];
            string value = values[1];

            if (value[0] == value[value.Length - 1] && (value[0] == '"' || value[0] == '\''))
                value = value.Substring(1, value.Length - 2);

            return node => node.Attributes.Contains(filter) && operation(node.Attributes[filter].Value, value);
        }

        private Func<string, string, bool> GetOperation(char value)
        {
            if (char.IsLetterOrDigit(value))
                return (attr, v) => attr == v;

            switch (value)
            {
                case '*': return (attr, v) => attr == v || attr.Contains(v);
                case '^': return (attr, v) => attr.StartsWith(v);
                case '$': return (attr, v) => attr.EndsWith(v);
                case '~': return (attr, v) => attr.Split(' ').Contains(v);

                case '|':
                    // This operator will only match if the first full-word in the attribute value
                    // is either an exact match to the query or is an exact match immediately followed by a dash. 
                    return (attr, v) =>
                    {
                        var isMatch = false;

                        if (attr == v)
                        {
                            isMatch = true;
                        }
                        else if (attr.Length > v.Length)
                        {
                            var firstValue = attr
                                .Split(new[] { ' ' }, StringSplitOptions.None)
                                .FirstOrDefault();

                            if (firstValue?.StartsWith(v) ?? false)
                            {
                                isMatch = firstValue.Length > v.Length && firstValue[v.Length] == '-';
                            }
                        }

                        return isMatch;
                    };
            }

            throw new NotSupportedException($"Invalid selector use for attribute {Selector}.");
        }
    }
}