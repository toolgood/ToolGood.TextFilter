using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Texts
{
    public class TextFilterDetailItem
    {
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
        /// 命中敏感词位置，例：1,3,5,7-12,15
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// 匹配文本
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

    }
}
