/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;
using Newtonsoft.Json;
using ToolGood.TextFilter.Website.Commons;

namespace ToolGood.TextFilter.Models
{
    public class KeywordtypeItem
    {

        [JsonProperty("typeId", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeId { get; set; }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }


        [JsonProperty("level_1_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_1_UseType { get; set; }

        [JsonProperty("level_2_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_2_UseType { get; set; }

        [JsonProperty("level_3_UseType", NullValueHandling = NullValueHandling.Ignore)]
        public int Level_3_UseType { get; set; }

        [JsonProperty("useTime", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseTime { get; set; }

        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public string StartTime { get; set; }

        [JsonProperty("endTime", NullValueHandling = NullValueHandling.Ignore)]
        public string EndTime { get; set; }


        public void Build(StringBuilder sb)
        {
            sb.Append("{\"typeId\":\"");
            sb.Append(TypeId);
            sb.Append("\",\"parentId\":\"");
            sb.Append(ParentId);
            sb.Append("\",\"code\":\"");
            JsonCommon.AddString(sb, Code);
            sb.Append("\",\"name\":\"");
            JsonCommon.AddString(sb, Name);
            sb.Append("\",\"level_1_UseType\":");
            sb.Append(Level_1_UseType);
            sb.Append(",\"level_2_UseType\":");
            sb.Append(Level_2_UseType);
            sb.Append(",\"level_3_UseType\":");
            sb.Append(Level_3_UseType);
            sb.Append(",\"useTime\":");
            sb.Append(UseTime?"true":"false");
            sb.Append(",\"startTime\":\"");
            sb.Append(StartTime);
            sb.Append("\",\"endTime\":\"");
            sb.Append(EndTime);
            sb.Append("\"}");
        }
    }


}
