using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas.Texts;
using ToolGood.TextFilter.Api.Interfaces;
using Newtonsoft.Json;
using ToolGood.TextFilter.Api.Datas.Requests;

namespace ToolGood.TextFilter.Api.Impl
{
    public class TextFilterProvider : ProviderBase, ITextFilterProvider
    {
        public TextFilterProvider() : base(TextFilterConfig.Instance) { }
        public TextFilterProvider(TextFilterConfig textFilterConfig) : base(textFilterConfig) { }

        private const string TextFilterUrl = "/api/text-filter";
        private const string TextReplaceUrl = "/api/text-replace";
        private const string HtmlFilterUrl = "/api/html-filter";
        private const string HtmlReplaceUrl = "/api/html-replace";

        private const string JsonFilterUrl = "/api/json-filter";
        private const string JsonReplaceUrl = "/api/json-replace";
        private const string MarkdownFilterUrl = "/api/markdown-filter";
        private const string MarkdownReplaceUrl = "/api/markdown-replace";

        #region TextFilter TextReplace
        public TextFilterResult TextFilter(TextFilterRequest request)
        {
            return Filter(TextFilterUrl, request);
        }

        public Task<TextFilterResult> TextFilterAsync(TextFilterRequest request)
        {
            return FilterAsync(TextFilterUrl, request);
        }
        public TextReplaceResult TextReplace(TextReplaceRequest request)
        {
            return Replace(TextReplaceUrl, request);
        }

        public Task<TextReplaceResult> TextReplaceAsync(TextReplaceRequest request)
        {
            return ReplaceAsync(TextReplaceUrl, request);
        }

        #endregion

        #region HtmlFilter HtmlReplace


        public TextFilterResult HtmlFilter(TextFilterRequest request)
        {
            return Filter(HtmlFilterUrl, request);
        }

        public Task<TextFilterResult> HtmlFilterAsync(TextFilterRequest request)
        {
            return FilterAsync(HtmlFilterUrl, request);
        }

        public TextReplaceResult HtmlReplace(TextReplaceRequest request)
        {
            return Replace(HtmlReplaceUrl, request);
        }

        public Task<TextReplaceResult> HtmlReplaceAsync(TextReplaceRequest request)
        {
            return ReplaceAsync(HtmlReplaceUrl, request);
        }
        #endregion

        #region JsonFilter  JsonReplace
        public TextFilterResult JsonFilter(TextFilterRequest request)
        {
            return Filter(JsonFilterUrl, request);
        }

        public Task<TextFilterResult> JsonFilterAsync(TextFilterRequest request)
        {
            return FilterAsync(JsonFilterUrl, request);
        }

        public TextReplaceResult JsonReplace(TextReplaceRequest request)
        {
            return Replace(JsonReplaceUrl, request);
        }

        public Task<TextReplaceResult> JsonReplaceAsync(TextReplaceRequest request)
        {
            return ReplaceAsync(JsonReplaceUrl, request);
        }

        #endregion

        #region MarkdownFilter MarkdownReplace
        public TextFilterResult MarkdownFilter(TextFilterRequest request)
        {
            return Filter(MarkdownFilterUrl, request);
        }

        public Task<TextFilterResult> MarkdownFilterAsync(TextFilterRequest request)
        {
            return FilterAsync(MarkdownFilterUrl, request);
        }

        public TextReplaceResult MarkdownReplace(TextReplaceRequest request)
        {
            return Replace(MarkdownReplaceUrl, request);
        }

        public Task<TextReplaceResult> MarkdownReplaceAsync(TextReplaceRequest request)
        {
            return ReplaceAsync(MarkdownReplaceUrl, request);
        }


        #endregion

        private TextFilterResult Filter(string url, TextFilterRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            var json = JsonConvert.SerializeObject(dict);
            return PostContent<TextFilterResult>(url, json);
        }
        private Task<TextFilterResult> FilterAsync(string url, TextFilterRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["skipBidi"] = request.SkipBidi;
            dict["onlyPosition"] = request.OnlyPosition;

            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<TextFilterResult>(url, json);
        }
        private TextReplaceResult Replace(string url, TextReplaceRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["replaceChar"] = request.ReplaceChar;
            dict["reviewReplace"] = request.ReviewReplace;
            dict["skipBidi"] = request.SkipBidi;
            dict["contactReplace"] = request.ContactReplace;
            dict["onlyPosition"] = request.OnlyPosition;

            var json = JsonConvert.SerializeObject(dict);
            return PostContent<TextReplaceResult>(url, json);
        }
        private Task<TextReplaceResult> ReplaceAsync(string url, TextReplaceRequest request)
        {
            if (string.IsNullOrEmpty(request.Txt)) { throw new ArgumentNullException(nameof(request.Txt)); }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["txt"] = request.Txt;
            dict["replaceChar"] = request.ReplaceChar;
            dict["reviewReplace"] = request.ReviewReplace;
            dict["skipBidi"] = request.SkipBidi;
            dict["contactReplace"] = request.ContactReplace;
            dict["onlyPosition"] = request.OnlyPosition;

            var json = JsonConvert.SerializeObject(dict);
            return PostContentAsync<TextReplaceResult>(url, json);
        }







    }
}
