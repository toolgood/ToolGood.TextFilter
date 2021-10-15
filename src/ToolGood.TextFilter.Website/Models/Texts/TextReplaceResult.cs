/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class TextReplaceResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// 异步返回
        /// </summary>
        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }


        [JsonProperty("riskLevel", NullValueHandling = NullValueHandling.Ignore)]
        public string RiskLevel { get; set; }


        [JsonProperty("resultText", NullValueHandling = NullValueHandling.Ignore)]
        public string ResultText { get; set; }


        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public List<TextFilterDetailItem> Details { get; set; }


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
            if (string.IsNullOrWhiteSpace(RequestId) == false) {
                sb.Append(",\"requestId\":\"");
                JsonCommon.AddString(sb, RequestId);
                sb.Append("\"");
            }
            if (string.IsNullOrWhiteSpace(RiskLevel) == false) {
                sb.Append(",\"riskLevel\":\"");
                JsonCommon.AddString(sb, RiskLevel);
                sb.Append("\"");
            }
            if (string.IsNullOrWhiteSpace(ResultText) == false) {
                sb.Append(",\"resultText\":\"");
                JsonCommon.AddString(sb, ResultText);
                sb.Append("\"");
            }
            if (Details != null && Details.Count > 0) {
                sb.Append(",\"details\":[");
                for (int i = 0; i < Details.Count; i++) {
                    if (i > 0) { sb.Append(","); }
                    Details[i].Build(sb);
                }
                sb.Append("]");
            }
            sb.Append("}");
            return sb.ToString();
        }



    }

}
