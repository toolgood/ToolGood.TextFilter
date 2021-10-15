/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class CommonResult
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":");
            sb.Append(Code);

            if (string.IsNullOrWhiteSpace(Message)==false) {
                sb.Append(",\"message\":\"");
                JsonCommon.AddString(sb,Message);
                sb.Append("\"");
            }
            if (string.IsNullOrWhiteSpace(RequestId) == false) {
                sb.Append(",\"requestId\":\"");
                JsonCommon.AddString(sb, RequestId);
                sb.Append("\"");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
