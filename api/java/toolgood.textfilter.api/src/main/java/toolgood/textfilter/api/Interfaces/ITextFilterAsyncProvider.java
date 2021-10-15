package toolgood.textfilter.api.Interfaces;

import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Requests.TextFilterAsyncRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceAsyncRequest;

public interface ITextFilterAsyncProvider {

        /**
         * 文本检测-Text格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult TextFilter(TextFilterAsyncRequest request);

        /**
         * 文本替换-Text格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult TextReplace(TextReplaceAsyncRequest request);

        /**
         * 文本检测-Html格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult HtmlFilter(TextFilterAsyncRequest request);

        /**
         * 文本替换-Html格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult HtmlReplace(TextReplaceAsyncRequest request);

        /**
         * 文本检测-Json格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult JsonFilter(TextFilterAsyncRequest request);

        /**
         * 文本替换-Json格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult JsonReplace(TextReplaceAsyncRequest request);

        /**
         * 文本检测-Markdown格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult MarkdownFilter(TextFilterAsyncRequest request);

        /**
         * 文本替换-Markdown格式-异步
         * 
         * @param request 参数
         * @return
         */
        CommonResult MarkdownReplace(TextReplaceAsyncRequest request);

}
