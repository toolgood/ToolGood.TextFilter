using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Sys;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Impl
{
    public class SysProvider : ProviderBase, ISysProvider
    {
        public SysProvider() : base(TextFilterConfig.Instance) { }
        public SysProvider(TextFilterConfig textFilterConfig) : base(textFilterConfig) { }

        private const string UpdateSystemUrl = "/api/sys-update";
        private const string RefreshUrl = "/api/sys-refresh";
        private const string InfoUrl = "/api/sys-info";
        private const string UpdateLicenceUrl = "/api/sys-Update-Licence";
        private const string InitDataUrl = "/api/sys-init-Data";
        private const string GCCollectUrl = "/api/sys-GC-Collect";


        public CommonResult UpdateSystem(string textFilterNoticeUrl, string textReplaceNoticeUrl,  string skipword)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["textFilterNoticeUrl"] = textFilterNoticeUrl;
            dict["textReplaceNoticeUrl"] = textReplaceNoticeUrl;
            dict["skipword"] = skipword;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(UpdateSystemUrl, json);
        }
        public Task<CommonResult> UpdateSystemAsync(string textFilterNoticeUrl, string textReplaceNoticeUrl, string skipword)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["textFilterNoticeUrl"] = textFilterNoticeUrl;
            dict["textReplaceNoticeUrl"] = textReplaceNoticeUrl;
            dict["skipword"] = skipword;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(UpdateSystemUrl, json);
        }

        public SysInfo Info()
        {
            return GetContent<SysInfo>(InfoUrl);

        }
        public Task<SysInfo> InfoAsync()
        {
            return GetContentAsync<SysInfo>(InfoUrl);
        }

        public CommonResult UpdateLicence(string lic)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["Licence"] = lic;
            var json = JsonConvert.SerializeObject(dict);

            return PostContent<CommonResult>(UpdateLicenceUrl, json);
        }
        public Task<CommonResult> UpdateLicenceAsync(string lic)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["Licence"] = lic;
            var json = JsonConvert.SerializeObject(dict);

            return PostContentAsync<CommonResult>(UpdateLicenceUrl, json);
        }


        public CommonResult Refresh()
        {
            return GetContent<CommonResult>(RefreshUrl);
        }
        public Task<CommonResult> RefreshAsync()
        {
            return GetContentAsync<CommonResult>(RefreshUrl);
        }

        public CommonResult InitData()
        {
            return GetContent<CommonResult>(InitDataUrl);
        }
        public Task<CommonResult> InitDataAsync()
        {
            return GetContentAsync<CommonResult>(InitDataUrl);
        }

        public CommonResult GCCollect()
        {
            return GetContent<CommonResult>(GCCollectUrl);
        }
        public Task<CommonResult> GCCollectAsync()
        {
            return GetContentAsync<CommonResult>(GCCollectUrl);
        }



    }
}
