using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.KeywordTypes;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Impl
{
    public class KeywordTypeProvider : ProviderBase, IKeywordTypeProvider
    {
        public KeywordTypeProvider() : base(TextFilterConfig.Instance) { }
        public KeywordTypeProvider(TextFilterConfig textFilterConfig) : base(textFilterConfig) { }


        private const string GetListUrl = "/api/get-keywordtype-list";
        private const string SetKeywordTypeUrl = "/api/set-keywordtype";

        public KeywordtypeListResult GetList()
        {
            return GetContent<KeywordtypeListResult>(GetListUrl);
        }
        public Task<KeywordtypeListResult> GetListAsync()
        {
            return GetContentAsync<KeywordtypeListResult>(GetListUrl);
        }


        public CommonResult SetKeywordType(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType
            , bool useTime = false, string startTime = "", string endTime = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["typeId"] = typeId;
            dict["level_1_UseType"] = level_1_UseType;
            dict["level_2_UseType"] = level_2_UseType;
            dict["level_3_UseType"] = level_3_UseType;
            dict["useTime"] = useTime;
            dict["startTime"] = startTime;
            dict["endTime"] = endTime;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(SetKeywordTypeUrl, json);
        }
        public Task<CommonResult> SetKeywordTypeAsync(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType
            , bool useTime = false, string startTime = "", string endTime = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["typeId"] = typeId;
            dict["level_1_UseType"] = level_1_UseType;
            dict["level_2_UseType"] = level_2_UseType;
            dict["level_3_UseType"] = level_3_UseType;
            dict["useTime"] = useTime;
            dict["startTime"] = startTime;
            dict["endTime"] = endTime;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(SetKeywordTypeUrl, json);
        }



    }
}
