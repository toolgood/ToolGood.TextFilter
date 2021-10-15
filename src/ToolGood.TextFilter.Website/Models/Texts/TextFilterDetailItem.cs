/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class TextFilterDetailItem
    {
        [JsonProperty("riskLevel", NullValueHandling = NullValueHandling.Ignore)]
        public string RiskLevel { get; set; }

        [JsonProperty("riskCode", NullValueHandling = NullValueHandling.Ignore)]
        public string RiskCode { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        public void Build(StringBuilder sb)
        {
            sb.Append("{\"riskLevel\":\"");
            sb.Append(RiskLevel ?? "");
            if (string.IsNullOrEmpty(RiskCode)==false) {
                sb.Append("\",\"riskCode\":\"");
                sb.Append(RiskCode);
            }
            if (string.IsNullOrEmpty(Text) == false) {
                sb.Append("\",\"text\":\"");
                JsonCommon.AddString(sb, Text);
            }
            if (string.IsNullOrEmpty(Position) == false) {
                sb.Append("\",\"position\":\"");
                sb.Append(Position);
            }
            sb.Append("\"}");
        }



    }
}
