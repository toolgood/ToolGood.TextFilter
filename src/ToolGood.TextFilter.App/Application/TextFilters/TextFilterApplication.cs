/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using ToolGood.TextFilter.App.Datas.Results;
using ToolGood.TextFilter.Commons;

namespace ToolGood.TextFilter.Application
{
    public static class TextFilterApplication
    {
        /// <summary>
        /// 查找所有敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IllegalWordsFindAllResult FindAll(in string text, in bool skipBidi = false)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new IllegalWordsFindAllResult(); }

            var translateSearch = MemoryCache.Instance.TranslateSearch;
            var stream = translateSearch.Replace(text, skipBidi);
            return TextFilterHelper.FindAll(stream);
        }



        /// <summary>
        /// 替换所有敏感词
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceChar"></param>
        /// <param name="reviewReplace">为true时，不会返回Review，只返回Reject或Pass</param>
        /// <returns></returns>
        public static IllegalWordsReplaceResult Replace(in string text, in char replaceChar, in bool reviewReplace, in bool contactReplace, in bool skipBidi = false)
        {
            if (string.IsNullOrWhiteSpace(text)) { return new IllegalWordsReplaceResult(); }

            var translateSearch = MemoryCache.Instance.TranslateSearch;
            var textStream = translateSearch.Replace(text, skipBidi);
            return TextFilterHelper.Replace(textStream, replaceChar, reviewReplace, contactReplace);
        }




    }


}
