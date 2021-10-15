/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class KeywordListResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<KeywordItem> Items { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":");
            sb.Append(Code);

            if (string.IsNullOrWhiteSpace(Message) == false) {
                sb.Append(",\"message\":\"");
                JsonCommon.AddString(sb, Message);
                sb.Append("\"");
            }
            if (Total!=null) {
                sb.Append(",\"total\":");
                sb.Append(Total);
            }
            if (Items != null && Items.Count > 0) {
                sb.Append(",\"data\":[");
                for (int i = 0; i < Items.Count; i++) {
                    if (i > 0) { sb.Append(","); }
                    Items[i].Build(sb);
                }
                sb.Append("]");
            }
            sb.Append("}");
            return sb.ToString();
        }

    }
}
