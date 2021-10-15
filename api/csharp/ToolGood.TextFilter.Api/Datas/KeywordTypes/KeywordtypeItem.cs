using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.KeywordTypes
{
    public class KeywordtypeItem
    {
        /// <summary>
        /// 敏感词类型ID
        /// </summary>
        [JsonProperty("typeId", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeId { get; set; }
        /// <summary>
        /// 上级类型ID
        /// </summary>
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }

        /// <summary>
        /// 敏感词类型CODE
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// 敏感词类型名
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 内置触线类型1：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 1
        /// </summary>
        [JsonProperty("level_1_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_1_UseType { get; set; }

        /// <summary>
        /// 内置危险类型2：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
        /// </summary>
        [JsonProperty("level_2_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_2_UseType { get; set; }

        /// <summary>
        /// 内置违规类型3：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
        /// </summary>
        [JsonProperty("level_3_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_3_UseType { get; set; }

        [JsonProperty("useTime", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseTime { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public string StartTime { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public string EndTime { get; set; }

    }
}
