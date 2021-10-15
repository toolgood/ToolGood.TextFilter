/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class TextFilterContactItem
    {
        /// <summary>
        /// 联系方式类型 0)手机号  1)qq号  2)微信号  3) 微博号  4)微信号公众号   
        /// </summary>
        [JsonProperty("contactType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactType { get; set; }
        /// <summary>
        /// 联系方式串
        /// </summary>
        [JsonProperty("contactString", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactString { get; set; }
        /// <summary>
        /// 联系方式串位置
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public string Position { get; set; }


        public void Build(StringBuilder sb)
        {
            sb.Append("{\"contactType\":\"");
            JsonCommon.AddString(sb, ContactType);
            sb.Append("\",\"contactString\":\"");
            JsonCommon.AddString(sb, ContactString);
            sb.Append("\",\"position\":\"");
            JsonCommon.AddString(sb, Position);
            sb.Append("\"}");
        }
    }
}
