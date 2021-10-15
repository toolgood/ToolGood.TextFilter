using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Texts
{
    public class TextFilterResult
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
        /// 风险类别：Char：非正常字符 Politics：涉政文本 Terrorism：涉恐文本 Porn：涉黄文本 Gamble：涉赌文本 Drug：涉毒文本 Contraband：非法交易 Abuse：辱骂文本 Other：推广引诱诈骗 Custom：自定义敏感词
        /// </summary>
        [JsonProperty("riskCode", NullValueHandling = NullValueHandling.Ignore)]
        public string RiskCode { get; set; }

        /// <summary>
        /// 情感值，>0 正面，<0 负向 ，riskLevel为REVIEW（可疑内容），会出现此值
        /// </summary>
        [JsonProperty("sentimentScore", NullValueHandling = NullValueHandling.Ignore)]
        public double? SentimentScore { get; set; }

        /// <summary>
        /// 风险详情, 详见 details
        /// </summary>
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public List<TextFilterDetailItem> Details { get; set; }

        /// <summary>
        /// 联系方式详情, 详见 contacts
        /// </summary>
        [JsonProperty("contacts", NullValueHandling = NullValueHandling.Ignore)]
        public List<TextFilterContactItem> Contacts { get; set; }


    }
}
