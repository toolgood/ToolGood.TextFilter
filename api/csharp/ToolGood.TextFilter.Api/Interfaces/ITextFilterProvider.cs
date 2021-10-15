using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas.Requests;
using ToolGood.TextFilter.Api.Datas.Texts;

namespace ToolGood.TextFilter.Api.Interfaces
{
    public interface ITextFilterProvider
    {
        /// <summary>
        /// 文本检测-Text格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        TextFilterResult TextFilter(TextFilterRequest request);
        /// <summary>
        /// 文本检测-Text格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<TextFilterResult> TextFilterAsync(TextFilterRequest request);
        /// <summary>
        /// 文本替换-Text格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        TextReplaceResult TextReplace(TextReplaceRequest request);
        /// <summary>
        /// 文本替换-Text格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        Task<TextReplaceResult> TextReplaceAsync(TextReplaceRequest request);



        /// <summary>
        /// 文本检测-Html格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        TextFilterResult HtmlFilter(TextFilterRequest request);
        /// <summary>
        /// 文本检测-Html格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<TextFilterResult> HtmlFilterAsync(TextFilterRequest request);
        /// <summary>
        /// 文本替换-Html格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        TextReplaceResult HtmlReplace(TextReplaceRequest request);
        /// <summary>
        /// 文本替换-Html格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        Task<TextReplaceResult> HtmlReplaceAsync(TextReplaceRequest request);

        /// <summary>
        /// 文本检测-Json格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        TextFilterResult JsonFilter(TextFilterRequest request);
        /// <summary>
        /// 文本检测-Json格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<TextFilterResult> JsonFilterAsync(TextFilterRequest request);
        /// <summary>
        /// 文本替换-Json格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        TextReplaceResult JsonReplace(TextReplaceRequest request);
        /// <summary>
        /// 文本替换-Json格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        Task<TextReplaceResult> JsonReplaceAsync(TextReplaceRequest request);

        /// <summary>
        /// 文本检测-Markdown格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        TextFilterResult MarkdownFilter(TextFilterRequest request);
        /// <summary>
        /// 文本检测-Markdown格式
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<TextFilterResult> MarkdownFilterAsync(TextFilterRequest request);
        /// <summary>
        /// 文本替换-Markdown格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        TextReplaceResult MarkdownReplace(TextReplaceRequest request);
        /// <summary>
        /// 文本替换-Markdown格式
        /// </summary>
        /// <param name="request">参数</param>

        /// <returns></returns>
        Task<TextReplaceResult> MarkdownReplaceAsync(TextReplaceRequest request);


    }

}
