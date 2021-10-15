package toolgood.textfilter.api.Interfaces;

import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceRequest;
import toolgood.textfilter.api.Datas.Texts.TextFilterResult;
import toolgood.textfilter.api.Datas.Texts.TextReplaceResult;

public interface ITextFilterProvider {
    /**
     * 文本检测-Text格式
     * 
     * @param request 参数
     * @return
     */
    TextFilterResult TextFilter(TextFilterRequest request);

    /**
     * 文本替换-Text格式
     * 
     * @param request 参数
     * @return
     */
    TextReplaceResult TextReplace(TextReplaceRequest request);

    /**
     * 文本检测-Html格式
     * 
     * @param request 参数
     * @return
     */
    TextFilterResult HtmlFilter(TextFilterRequest request);

    /**
     * 文本替换-Html格式
     * 
     * @param request 参数
     * @return
     */
    TextReplaceResult HtmlReplace(TextReplaceRequest request);

    /**
     * 文本检测-Json格式
     * 
     * @param request 参数
     * @return
     */
    TextFilterResult JsonFilter(TextFilterRequest request);

    /**
     * 文本替换-Json格式
     * 
     * @param request 参数
     * @return
     */
    TextReplaceResult JsonReplace(TextReplaceRequest request);

    /**
     * 文本检测-Markdown格式
     * 
     * @param request 参数
     * @return
     */
    TextFilterResult MarkdownFilter(TextFilterRequest request);

    /**
     * 文本替换-Markdown格式
     * 
     * @param request 参数
     * @return
     */
    TextReplaceResult MarkdownReplace(TextReplaceRequest request);

}
