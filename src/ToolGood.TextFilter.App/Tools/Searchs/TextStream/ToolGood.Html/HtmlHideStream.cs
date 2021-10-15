/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Text;
using ToolGood.Css;
using ToolGood.HtmlExtract.HtmlAgilityPack;

namespace ToolGood.TextFilter
{
    class HtmlHideStream : ReadStreamBase
    {
        public HtmlHideStream(string source, HtmlDocument doc) : base(source)
        {
            Parse(doc);
        }

        private void Parse(HtmlDocument doc)
        {
            SetClassHide(doc);
            CssParser parser = new CssParser();

            StringBuilder sb1 = new StringBuilder();
            var starts1 = new List<int>();
            var ends1 = new List<int>();

            HtmlUtil.BuildHideWordsInfo(doc.DocumentNode, parser,IsHide, sb1, starts1, ends1);

            TestingText = sb1.ToString().ToCharArray();
            Start = starts1.ToArray();
            End = ends1.ToArray();

        }

        private bool IsHide(Dictionary<string, string> attrs)
        {
            string val;
            if (attrs.TryGetValue("display", out val)) { if (val == "none") { return true; } }
            if (attrs.TryGetValue("visibility", out val)) { if (val == "hidden") { return true; } }
            if (attrs.TryGetValue("opacity", out val)) { if (val == "0") { return true; } }
            if (attrs.TryGetValue("clip-path", out val)) { if (val == "polygon(0px 0px,0px 0px,0px 0px,0px 0px)") { return true; } }
            if (attrs.TryGetValue("height", out val)) { if (val == "0") { return true; } }
            if (attrs.TryGetValue("-moz-transform", out val)) { if (val == "scale(0)") { return true; } }
            if (attrs.TryGetValue("-webkit-transform", out val)) { if (val == "scale(0)") { return true; } }
            if (attrs.TryGetValue("-o-transform", out val)) { if (val == "scale(0)") { return true; } }
            if (attrs.TryGetValue("transform", out val)) { if (val == "scale(0)" || val == "scaleY(0)") { return true; } }
            if (attrs.TryGetValue("position", out val)) {
                if (val == "absolute" || val == "relative") {
                    string pos;
                    if (attrs.TryGetValue("left", out pos)) {
                        if (pos.StartsWith("-")) { return true; }
                    }
                    if (attrs.TryGetValue("top", out pos)) {
                        if (pos.StartsWith("-")) { return true; }
                    }
                }
            }
            return false;
        }


        private void SetClassHide(HtmlDocument doc)
        {
            var nodes = HtmlUtil.GetStyleNodes(doc.DocumentNode);
            CssDocument cssDocument = new CssDocument();
            foreach (var node in nodes) {
                cssDocument.Load(node.InnerHtml);
            }
            List<string> hideClassNames = new List<string>();

            cssDocument.GetCssNames(hideClassNames, IsHide);

            if (hideClassNames.Count>0) {
                var attr = doc.CreateAttribute("_toolgood_hidden_", "true");
                foreach (var item in hideClassNames) {
                    var nds = doc.QuerySelectorAll(item);
                    foreach (var nd in nds) {
                        nd.Attributes.Add(attr);
                    }
                }
            }
        }



    }
}
