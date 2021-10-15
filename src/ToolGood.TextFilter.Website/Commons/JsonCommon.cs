/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;

namespace ToolGood.TextFilter.Website.Commons
{
    public static class JsonCommon
    {
        public static void AddString(StringBuilder sb, string text)
        {
            if (text == null) {
                return;
            }
            for (int i = 0; i < text.Length; i++) {
                var ch = text[i];
                if (ch == '\\') {
                    sb.Append(@"\\");
                } else if (ch == '\"') {
                    sb.Append(@"\""");
                } else if (ch == '\r') {
                    sb.Append(@"\r");
                } else if (ch == '\n') {
                    sb.Append(@"\n");
                } else if (ch == '\t') {
                    sb.Append(@"\t");
                } else if (ch == '\b') {
                    sb.Append(@"\b");
                } else if (ch == '\f') {
                    sb.Append(@"\f");
                } else if (ch == '\v') {
                    sb.Append(@"\v");
                } else {
                    sb.Append(ch);
                }
            }
        }

    }
}
