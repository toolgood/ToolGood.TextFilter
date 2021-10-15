/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using ToolGood.TextFilter.App.Datas.Results;
using ToolGood.TextFilter.Commons;

namespace ToolGood.TextFilter.Application
{
    public static partial class MarkdownFilterApplication
    {
        /// <summary>
        /// 查找所有敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IllegalWordsFindAllResult FindAll(in string txt, in bool skipBidi)
        {
            if (string.IsNullOrWhiteSpace(txt)) { return new IllegalWordsFindAllResult(); }

            // 1、全文检测一次 除标签内 有违规信息
            var translateSearch = MemoryCache.Instance.TranslateSearch;
            var stream = translateSearch.Replace(txt, skipBidi);
            var result1 = TextFilterHelper.FindAll(stream);
            stream = null;

            // 2、提取Markdown全文，检测一次
            stream = new MarkdownStream(txt);
            var textStream = translateSearch.Replace(stream, skipBidi);
            var result2 = TextFilterHelper.FindAll(textStream);
            stream = null;

            var result = TextFilterHelper.MergeFindAllResult(new[] { result1, result2 });
            result1 = null;
            result2 = null;
            return result;
        }

        /// <summary>
        /// 替换所有敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceChar"></param>
        /// <param name="reviewReplace">为true时，不会返回Review，只返回Reject或Pass</param>
        /// <returns></returns>
        public static IllegalWordsReplaceResult Replace(in string txt, in char replaceChar, in bool reviewReplace, in bool contactReplace, in bool skipBidi)
        {
            if (string.IsNullOrWhiteSpace(txt)) { return new IllegalWordsReplaceResult(); }

            var translateSearch = MemoryCache.Instance.TranslateSearch;

            // 1、全文检测一次 除标签内 有违规信息
            var stream = translateSearch.Replace(txt, skipBidi);
            var temp = TextFilterHelper.FindAll_Replace(stream, contactReplace);
            IllegalWordsReplaceResult replaceResult = new IllegalWordsReplaceResult();
            byte[] bytes = new byte[txt.Length];
            TextFilterHelper.Replace(replaceResult, stream, temp, bytes, replaceChar, reviewReplace, contactReplace);
            stream = null;
            temp = null;

            // 2、提取Markdown全文，检测一次
            stream = new MarkdownStream(txt);
            stream = translateSearch.Replace(stream, skipBidi);
            temp = TextFilterHelper.FindAll_Replace(stream, contactReplace);
            TextFilterHelper.Replace(replaceResult, stream, temp, bytes, replaceChar, reviewReplace, contactReplace);
            stream = null;
            temp = null;
 

            if (replaceResult.RiskLevel == IllegalWordsRiskLevel.Reject) {
                replaceResult.Result = TextFilterHelper.Replace(txt, bytes, replaceChar);
            }
            bytes = null;
            return replaceResult;
        }

    }


}
