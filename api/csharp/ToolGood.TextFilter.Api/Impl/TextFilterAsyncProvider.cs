#if Async
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Requests;
using ToolGood.TextFilter.Api.Interfaces;

namespace ToolGood.TextFilter.Api.Impl
{
    public class TextFilterAsyncProvider : ProviderBase, ITextFilterAsyncProvider
    {
        public TextFilterAsyncProvider() : base(TextFilterConfig.Instance) { }
        public TextFilterAsyncProvider(TextFilterConfig textFilterConfig) : base(textFilterConfig) { }

        private const string TextFilterUrl = "/api/async/text-filter";
        private const string TextReplaceUrl = "/api/async/text-replace";
        private const string HtmlFilterUrl = "/api/async/html-filter";
        private const string HtmlReplaceUrl = "/api/async/html-replace";

        private const string JsonFilterUrl = "/api/async/json-filter";
        private const string JsonReplaceUrl = "/api/async/json-replace";
        private const string MarkdownFilterUrl = "/api/async/markdown-filter";
        private const string MarkdownReplaceUrl = "/api/async/markdown-replace";

        #region TextFilter TextReplace
        public CommonResult TextFilter(TextFilterAsyncRequest request)
        {
            return Filter(TextFilterUrl, request);
        }
        public Task<CommonResult> TextFilterAsync(TextFilterAsyncRequest request)
        {
            return FilterAsync(TextFilterUrl, request);
        }
        public CommonResult TextReplace(TextReplaceAsyncRequest request)
        {
            return Replace(TextReplaceUrl, request);
        }
        public Task<CommonResult> TextReplaceAsync(TextReplaceAsyncRequest request)
        {
            return ReplaceAsync(TextReplaceUrl, request);
        }
        #endregion

        #region HtmlFilter HtmlReplace
        public CommonResult HtmlFilter(TextFilterAsyncRequest request)
        {
            return Filter(HtmlFilterUrl, request);
        }
        public Task<CommonResult> HtmlFilterAsync(TextFilterAsyncRequest request)
        {
            return FilterAsync(HtmlFilterUrl, request);
        }
        public CommonResult HtmlReplace(TextReplaceAsyncRequest request)
        {
            return Replace(HtmlReplaceUrl, request);
        }
        public Task<CommonResult> HtmlReplaceAsync(TextReplaceAsyncRequest request)
        {
            return ReplaceAsync(HtmlReplaceUrl, request);
        }

        #endregion

        #region JsonFilter  JsonReplace
        public CommonResult JsonFilter(TextFilterAsyncRequest request)
        {
            return Filter(JsonFilterUrl, request);
        }

        public Task<CommonResult> JsonFilterAsync(TextFilterAsyncRequest request)
        {
            return FilterAsync(JsonFilterUrl, request);
        }

        public CommonResult JsonReplace(TextReplaceAsyncRequest request)
        {
            return Replace(JsonReplaceUrl, request);
        }

        public Task<CommonResult> JsonReplaceAsync(TextReplaceAsyncRequest request)
        {
            return ReplaceAsync(JsonReplaceUrl, request);
        }
        #endregion

        #region MarkdownFilter MarkdownReplace

        public CommonResult MarkdownFilter(TextFilterAsyncRequest request)
        {
            return Filter(MarkdownFilterUrl, request);
        }

        public Task<CommonResult> MarkdownFilterAsync(TextFilterAsyncRequest request)
        {
            return FilterAsync(MarkdownFilterUrl, request);
        }

        public CommonResult MarkdownReplace(TextReplaceAsyncRequest request)
        {
            return Replace(MarkdownReplaceUrl, request);
        }

        public Task<CommonResult> MarkdownReplaceAsync(TextReplaceAsyncRequest request)
        {
            return ReplaceAsync(MarkdownReplaceUrl, request);
        }
        #endregion

        private CommonResult Filter(string url, TextFilterAsyncRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            dict["url"] = request.Url;
            dict["requestId"] = request.RequestId;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(url, json);
        }
        private Task<CommonResult> FilterAsync(string url, TextFilterAsyncRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            dict["url"] = request.Url;
            dict["requestId"] = request.RequestId;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(url, json);
        }
        private CommonResult Replace(string url, TextReplaceAsyncRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["replaceChar"] = request.ReplaceChar;
            dict["reviewReplace"] = request.ReviewReplace;
            dict["contactReplace"] = request.ContactReplace;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            dict["url"] = request.Url;
            dict["requestId"] = request.RequestId;
            var json = JsonConvert.SerializeObject(dict);
            return PostContent<CommonResult>(url, json);
        }
        private Task<CommonResult> ReplaceAsync(string url, TextReplaceAsyncRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["replaceChar"] = request.ReplaceChar;
            dict["reviewReplace"] = request.ReviewReplace;
            dict["contactReplace"] = request.ContactReplace;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            dict["url"] = request.Url;
            dict["requestId"] = request.RequestId;
            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<CommonResult>(url, json);
        }




 

    }
}

#endif