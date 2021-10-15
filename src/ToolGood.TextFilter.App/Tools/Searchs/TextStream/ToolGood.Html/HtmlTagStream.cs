/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Text;
using ToolGood.HtmlExtract.HtmlAgilityPack;

namespace ToolGood.TextFilter
{
    public class HtmlTagStream: ReadStreamBase
    {
        public HtmlTagStream(string source,HtmlDocument doc) : base(source)
        {
            Parse(doc);
        }

        private void Parse(HtmlDocument doc)
        {
            StringBuilder sb1 = new StringBuilder();
            var starts1 = new List<int>();
            var ends1 = new List<int>();

            HtmlUtil.BuildWordsTag(doc.DocumentNode, 0, sb1, starts1, ends1);

            TestingText = sb1.ToString().ToCharArray();
            Start = starts1.ToArray();
            End = ends1.ToArray();
        }


    }
}
