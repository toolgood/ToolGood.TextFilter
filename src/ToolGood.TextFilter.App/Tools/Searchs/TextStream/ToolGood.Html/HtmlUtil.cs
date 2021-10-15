/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ToolGood.Css;
using ToolGood.HtmlExtract.HtmlAgilityPack;

namespace ToolGood.TextFilter
{
    static class HtmlUtil
    {
        public static List<HtmlNode> GetStyleNodes(HtmlNode node)
        {
            List<HtmlNode> result = new List<HtmlNode>();
            GetStyleNodes(node, result);
            return result;
        }
        public static void GetStyleNodes(HtmlNode node, List<HtmlNode> result)
        {
            if (node.NodeType == HtmlNodeType.Comment) return;
            if (node.Name.Equals("style", StringComparison.OrdinalIgnoreCase)) {
                result.Add(node);
                return;
            }
            if (node.HasChildNodes) {
                foreach (var item in node.ChildNodes) {
                    GetStyleNodes(item, result);
                }
            }
        }

        public static void BuildWordsInfo(HtmlNode node, int position, StringBuilder sb1, List<int> starts1, List<int> ends1)
        {
            if (node.NodeType == HtmlNodeType.Comment) return;
            if (node.NodeType == HtmlNodeType.Text) {
                HtmlDecode(node.InnerText, node.InnerStartIndex + position, sb1, starts1, ends1);
                return;
            }
            if (node.Name.Equals("script", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("link", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("style", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("object", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("svg", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("br", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("hr", StringComparison.OrdinalIgnoreCase)) return;
            if (node.HasChildNodes) {
                foreach (var item in node.ChildNodes) {
                    BuildWordsInfo(item, position, sb1, starts1, ends1);
                }
            }
        }

        public static void BuildWordsTag(HtmlNode node, int position, StringBuilder sb2, List<int> starts2, List<int> ends2)
        {
            if (node.NodeType == HtmlNodeType.Comment) return;
            if (node.NodeType == HtmlNodeType.Text) return;
            if (node.Name.Equals("script", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("link", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("style", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("object", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("svg", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("br", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("hr", StringComparison.OrdinalIgnoreCase)) return;

            TryAddAttr(node, "alt", position, sb2, starts2, ends2);
            TryAddAttr(node, "value", position, sb2, starts2, ends2);
            TryAddAttr(node, "title", position, sb2, starts2, ends2);
            TryAddAttr(node, "src", position, sb2, starts2, ends2);
            TryAddAttr(node, "href", position, sb2, starts2, ends2);
            TryAddAttr(node, "placeholder", position, sb2, starts2, ends2);

            if (node.Name.Equals("meta", StringComparison.OrdinalIgnoreCase)) {
                var name = node.Attributes["name"]?.ToString();
                if (name == "keywords" || name == "description" || name == "author" || name == "generator" || name == "application-name") {
                    TryAddAttr(node, "content", position, sb2, starts2, ends2);
                }
            }
            if (node.HasChildNodes) {
                foreach (var item in node.ChildNodes) {
                    BuildWordsInfo(item, position, sb2, starts2, ends2);
                }
            }
        }

        public static void BuildHideWordsInfo(HtmlNode node, CssParser parser, Func<Dictionary<string, string>, bool> func
            , StringBuilder sb1, List<int> starts1, List<int> ends1)
        {
            if (node.NodeType == HtmlNodeType.Comment) return;
            if (node.NodeType == HtmlNodeType.Text) {
                HtmlDecode(node.InnerText, node.InnerStartIndex, sb1, starts1, ends1);
                return;
            }
            if (node.Name.Equals("script", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("link", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("style", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("object", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("svg", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("br", StringComparison.OrdinalIgnoreCase)) return;
            if (node.Name.Equals("hr", StringComparison.OrdinalIgnoreCase)) return;
            var att = node.Attributes["_toolgood_hidden_"];
            if (att != null) { return; }

            att = node.Attributes["style"];
            if (att != null) {
                var dict = parser.ParseStyle(att.Value);
                if (func(dict)) { dict = null; return; }
                dict = null;
            }

            if (node.HasChildNodes) {
                foreach (var item in node.ChildNodes) {
                    BuildHideWordsInfo(item, parser, func, sb1, starts1, ends1);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void TryAddAttr(HtmlNode node, string attrName, int position, StringBuilder sb, List<int> starts, List<int> ends)
        {
            if (node.HasAttributes == false)
                return;
            var attr = node.Attributes[attrName];
            if (attr != null) {
                sb.Append((char)0);
                starts.Add(-1);
                ends.Add(-1);
                HtmlDecode(attr.Value, attr.ValueStartIndex + position, sb, starts, ends);
            }
        }
        private static void HtmlDecode(string html, int startIndex, StringBuilder sb, List<int> starts, List<int> ends)
        {
            var index = 0;
            while (index < html.Length) {
                char[] chs;
                var sIndex = index;
                if (html[index] == '&' && HtmlDecodeSearch.FindFirstChar(html, ref index, out chs)) {
                    var eIndex = index;
                    if (chs.Length == 1) {
                        sb.Append(chs[0]);
                        starts.Add(startIndex + sIndex);
                        ends.Add(startIndex + eIndex);
                    } else {
                        sb.Append(chs[0]);
                        starts.Add(startIndex + sIndex);
                        ends.Add(startIndex + sIndex);
                        sb.Append(chs[1]);
                        starts.Add(startIndex + sIndex + 1);
                        ends.Add(startIndex + eIndex);
                    }
                } else {
                    sb.Append(html[index]);
                    starts.Add(startIndex + sIndex);
                    ends.Add(startIndex + sIndex);
                }
                index++;
            }
        }

    }
}
