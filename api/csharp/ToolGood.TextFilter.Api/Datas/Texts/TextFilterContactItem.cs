using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Texts
{
    public class TextFilterContactItem
    {
        /// <summary>
        /// 联系方式类型 1) 账号，2）邮箱，3）网址，4）手机号， 5) QQ号, 6) 微信号, 7) Q群号
        /// </summary>
        [JsonProperty("contactType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactType { get; set; }
        /// <summary>
        /// 联系方式串
        /// </summary>
        [JsonProperty("contactString", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactString { get; set; }
        /// <summary>
        /// 联系方式串位置，例：1,3,5,7-12,15
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }

    }
}
