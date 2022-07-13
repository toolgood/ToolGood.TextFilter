using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToolGood.DFAs
{
    public class RegexParser
    {
        private const char HIGH_SURROGATE_START = '\uD800';
        private const char HIGH_SURROGATE_END = '\uDBFF';
        private const char LOW_SURROGATE_START = '\uDC00';
        private const char LOW_SURROGATE_END = '\uDFFF';
        public RootExp ParseRegex(string text, int lableIndex)
        {
            text = Regex.Replace(text, "[\x00-\x1F\x7f]", "");
            text = Regex.Replace(text, "[\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000]", "");
            //text = Regex.Replace(text, "[\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF]", "");
            text = Regex.Replace(text, "[\u180E\u200b-\u200D\u2028-\u2029\u2060\uFEFF]", "");
            text = Regex.Replace(text, " ", "");

            var str = new char[] {
                '\u200E', '\u200F', '\u061C','\u202A'
                , '\u202B', '\u202D', '\u202E', '\u202C'
                //,'\u2066','\u2067','\u2068','\u2069'
            };

            var line = SplitRegex(text);
            line = line.GroupByOr(true);
            var exp = line.Build();
            ((RootExp)exp).LabelIndex = lableIndex;
            return (RootExp)exp;
        }

        public RegexTextNode SplitRegex(string text)
        {
            RegexTextNode root = new RegexTextNode();
            SplitRegex(text, 0, 0, root.Nodes);
            return root;
        }
        private int SplitRegex(string text, int start, int layer, List<RegexTextNode> nodes)
        {
            while (start < text.Length) {
                var ch = text[start++];
                if (ch == '(') {
                    var black = new RegexTextNode();
                    var end = SplitRegex(text, start, layer + 1, black.Nodes);
                    var node = new RegexTextNode() { Nodes = new List<RegexTextNode>() { new RegexTextNode(ch) } };
                    node.Nodes.Add(black);
                    node.Nodes.Add(new RegexTextNode(text[end - 1]));
                    nodes.Add(node);
                    start = end;
                } else if (ch == ')') {
                    //nodes.Add(new TextNode(ch));
                    return start;
                } else if (ch == '[') {
                    var node = new RegexTextNode() { Nodes = new List<RegexTextNode>() { new RegexTextNode(ch) } };
                    var end = SplitRegexOptional(text, start, node.Nodes);
                    nodes.Add(node);
                    start = end;
                } else if (ch == '\\') {
                    var next = text[start];
                    if (next == 'u') {
                        nodes.Add(new RegexTextNode(ch, next, text[start + 1], text[start + 2], text[start + 3], text[start + 4]));
                        start += 5;
                    } else if (next == 'x') {
                        nodes.Add(new RegexTextNode(ch, next, text[start + 1], text[start + 2]));
                        start += 3;
                    } else {
                        nodes.Add(new RegexTextNode(ch, next));
                        start++;
                    }
                } else if (ch == '{') {
                    var str = "{";
                    ch = text[start++];
                    while (ch != '}') {
                        str += ch;
                        ch = text[start++];
                    }
                    str += "}";
                    nodes.Last().AppendText = str;
                } else if (ch == '?' || ch == '*' || ch == '+') {
                    nodes.Last().AppendText = ch.ToString();
                } else {
                    if (ch >= HIGH_SURROGATE_START && ch <= HIGH_SURROGATE_END
                        && text[start] >= LOW_SURROGATE_START && text[start] <= LOW_SURROGATE_END
                        ) {
                        var node = new RegexTextNode();
                        //node.Nodes.Add(new RegexTextNode('('));
                        node.Nodes.Add(new RegexTextNode(ch));
                        node.Nodes.Add(new RegexTextNode(text[start]));
                        //node.Nodes.Add(new RegexTextNode(')'));
                        nodes.Add(node);
                        start++;
                    } else {
                        nodes.Add(new RegexTextNode(ch));
                    }
                }
            }
            if (layer > 0) {
                throw new ArgumentException("text括号没有关闭");
            }
            return start;
        }
        private int SplitRegexOptional(string text, int start, List<RegexTextNode> nodes)
        {
            while (start < text.Length) {
                var ch = text[start++];
                if (ch == ']') {
                    nodes.Add(new RegexTextNode(ch));
                    return start;
                } else if (ch == '\\') {
                    var next = text[start];
                    if (next == 'u') {
                        nodes.Add(new RegexTextNode(ch, next, text[start + 1], text[start + 2], text[start + 3], text[start + 4]));
                        start += 5;
                    } else if (next == 'x') {
                        nodes.Add(new RegexTextNode(ch, next, text[start + 1], text[start + 2]));
                        start += 3;
                    } else {
                        nodes.Add(new RegexTextNode(ch, next));
                        start++;
                    }
                } else if (ch == '(') {
                    nodes.Add(new RegexTextNode('\\', ch));
                } else if (ch == ')') {
                    nodes.Add(new RegexTextNode('\\', ch));
                } else if (ch == '|') {
                    nodes.Add(new RegexTextNode('\\', ch));
                } else {
                    nodes.Add(new RegexTextNode(ch));
                }
            }
            throw new ArgumentException("text没有关闭");
        }

    }
}

