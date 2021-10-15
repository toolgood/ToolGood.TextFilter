/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class KeywordItem
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("addingTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AddingTime { get; set; }

        [JsonProperty("modifyTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ModifyTime { get; set; }



        public void Build(StringBuilder sb)
        {
            sb.Append("{\"id\":\"");
            sb.Append(Id);
            sb.Append("\",\"text\":\"");
            JsonCommon.AddString(sb, Text);
            sb.Append("\",\"type\":\"");
            sb.Append(Type);
            if (string.IsNullOrEmpty(Comment) == false) {
                sb.Append("\",\"comment\":\"");
                JsonCommon.AddString(sb, Comment);
            }
            if (ModifyTime != null) {
                sb.Append("\",\"modifyTime\":\"");
                sb.Append(ModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            sb.Append("\",\"addingTime\":\"");
            sb.Append(AddingTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("\"}");
        }
    }
}
