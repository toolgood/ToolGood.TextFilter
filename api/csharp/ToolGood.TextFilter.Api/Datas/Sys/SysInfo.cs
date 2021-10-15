using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Datas.Sys
{
    public class SysInfo
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
        /// 名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        [JsonProperty("machineCode", NullValueHandling = NullValueHandling.Ignore)]
        public string MachineCode { get; set; }

        /// <summary>
        /// 是否注册
        /// </summary>
        [JsonProperty("isRegister", NullValueHandling = NullValueHandling.Ignore)]
        public string IsRegister { get; set; }

        /// <summary>
        /// 服务开始时间
        /// </summary>
        [JsonProperty("serviceStart", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceStart { get; set; }

        /// <summary>
        /// 服务结束时间
        /// </summary>
        [JsonProperty("serviceEnd", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceEnd { get; set; }

        /// <summary>
        /// 注册码
        /// </summary>
        [JsonProperty("licenceTxt", NullValueHandling = NullValueHandling.Ignore)]
        public string LicenceTxt { get; set; }



    }
}
