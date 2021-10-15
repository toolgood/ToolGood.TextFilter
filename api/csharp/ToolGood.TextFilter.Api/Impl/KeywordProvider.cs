using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Keywords;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Impl
{
    public class KeywordProvider : ProviderBase, IKeywordProvider
    {
        public KeywordProvider() : base(TextFilterConfig.Instance) { }
        public KeywordProvider(TextFilterConfig textFilterConfig) : base(textFilterConfig) { }

        private const string GetKeywordListUrl = "/api/get-keyword-list";
        private const string AddKeywordUrl = "/api/add-keyword";
        private const string EditKeywordUrl = "/api/edit-keyword";
        private const string DeleteKeywordUrl = "/api/delete-keyword";

        public KeywordListResult GetKeywordList(string text, int? type, int? page, int? pageSize)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["text"] = text;
            dict["type"] = type;
            dict["page"] = page;
            dict["pageSize"] = pageSize;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<KeywordListResult>(GetKeywordListUrl, json);

        }
        public Task<KeywordListResult> GetKeywordListAsync(string text, int? type, int? page, int? pageSize)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["text"] = text;
            dict["type"] = type;
            dict["page"] = page;
            dict["pageSize"] = pageSize;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<KeywordListResult>(GetKeywordListUrl, json);
        }


        public CommonResult AddKeyword(string text, int type, string comment)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["text"] = text;
            dict["type"] = type;
            dict["comment"] = comment;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(AddKeywordUrl, json);
        }
        public Task<CommonResult> AddKeywordAsync(string text, int type, string comment)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["text"] = text;
            dict["type"] = type;
            dict["comment"] = comment;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(AddKeywordUrl, json);
        }


        public CommonResult EditKeyword(int id, string text, int type, string comment)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["id"] = id;
            dict["text"] = text;
            dict["type"] = type;
            dict["comment"] = comment;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(EditKeywordUrl, json);
        }
        public Task<CommonResult> EditKeywordAsync(int id, string text, int type, string comment)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["id"] = id;
            dict["text"] = text;
            dict["type"] = type;
            dict["comment"] = comment;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(EditKeywordUrl, json);
        }


        public CommonResult DeleteKeyword(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["id"] = id;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(DeleteKeywordUrl, json);
        }
        public Task<CommonResult> DeleteKeywordAsync(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["id"] = id;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(DeleteKeywordUrl, json);
        }


    }
}
