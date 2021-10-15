using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Texts
{
    public class TextReplaceResult
    {
        /// <summary>
        /// 返回码：0) 成功，1) 失败
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 返回码详情描述
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        /// <summary>
        /// 请求标识
        /// </summary>
        [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestId { get; set; }

        /// <summary>
        /// 风险级别：        PASS：正常内容，建议直接放行        REVIEW：可疑内容，建议人工审核        REJECT：违规内容，建议直接拦截
        /// </summary>
        [JsonProperty("riskLevel", NullValueHandling = NullValueHandling.Ignore)]
        public string RiskLevel { get; set; }

        /// <summary>
        /// 替换后的文本
        /// </summary>
        [JsonProperty("resultText", NullValueHandling = NullValueHandling.Ignore)]
        public string ResultText { get; set; }

        /// <summary>
        /// 风险详情，当reviewReplace为false时会出现, 详见 details
        /// </summary>
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public List<TextFilterDetailItem> Details { get; set; }

    }
}
