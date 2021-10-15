using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Keywords
{
    public class KeywordItem
    {
        /// <summary>
        /// 自定义敏感词ID
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        /// <summary>
        /// 自定义敏感词
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// 类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        [JsonProperty("addingTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AddingTime { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [JsonProperty("modifyTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ModifyTime { get; set; }


    }
}
