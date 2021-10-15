#if Async
using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Requests;

namespace ToolGood.TextFilter.Api.Interfaces
{
    public interface ITextFilterAsyncProvider
    {
        /// <summary>
        /// 文本检测-Text格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult TextFilter(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本检测-Text格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> TextFilterAsync(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本替换-Text格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult TextReplace(TextReplaceAsyncRequest request);
        /// <summary>
        /// 文本替换-Text格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> TextReplaceAsync(TextReplaceAsyncRequest request);

        /// <summary>
        /// 文本检测-Html格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult HtmlFilter(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本检测-Html格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> HtmlFilterAsync(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本替换-Html格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult HtmlReplace(TextReplaceAsyncRequest request);
        /// <summary>
        /// 文本替换-Html格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> HtmlReplaceAsync(TextReplaceAsyncRequest request);

        /// <summary>
        /// 文本检测-Json格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult JsonFilter(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本检测-Json格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> JsonFilterAsync(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本替换-Json格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult JsonReplace(TextReplaceAsyncRequest request);
        /// <summary>
        /// 文本替换-Json格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> JsonReplaceAsync(TextReplaceAsyncRequest request);

        /// <summary>
        /// 文本检测-Markdown格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult MarkdownFilter(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本检测-Markdown格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> MarkdownFilterAsync(TextFilterAsyncRequest request);
        /// <summary>
        /// 文本替换-Markdown格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        CommonResult MarkdownReplace(TextReplaceAsyncRequest request);
        /// <summary>
        /// 文本替换-Markdown格式-异步
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        Task<CommonResult> MarkdownReplaceAsync(TextReplaceAsyncRequest request);




    }

}

#endif