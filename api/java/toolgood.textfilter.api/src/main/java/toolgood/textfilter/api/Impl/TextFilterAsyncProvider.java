package toolgood.textfilter.api.Impl;

import java.util.Hashtable;
import java.util.Map;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Requests.TextFilterAsyncRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceAsyncRequest;
import toolgood.textfilter.api.Interfaces.ITextFilterAsyncProvider;

public class TextFilterAsyncProvider extends ProviderBase implements ITextFilterAsyncProvider {

    public TextFilterAsyncProvider(TextFilterConfig textFilterConfig) {
        super(textFilterConfig);
    }

    private final String TextFilterUrl = "/api/async/text-filter";
    private final String TextReplaceUrl = "/api/async/text-replace";
    private final String HtmlFilterUrl = "/api/async/html-filter";
    private final String HtmlReplaceUrl = "/api/async/html-replace";

    private final String JsonFilterUrl = "/api/async/json-filter";
    private final String JsonReplaceUrl = "/api/async/json-replace";
    private final String MarkdownFilterUrl = "/api/async/markdown-filter";
    private final String MarkdownReplaceUrl = "/api/async/markdown-replace";

    @Override
    public CommonResult TextFilter(TextFilterAsyncRequest request) {
        String content = PostFilter(TextFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult TextReplace(TextReplaceAsyncRequest request) {
        String content = PostReplace(TextReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult HtmlFilter(TextFilterAsyncRequest request) {
        String content = PostFilter(HtmlFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult HtmlReplace(TextReplaceAsyncRequest request) {
        String content = PostReplace(HtmlReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult JsonFilter(TextFilterAsyncRequest request) {
        String content = PostFilter(JsonFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult JsonReplace(TextReplaceAsyncRequest request) {
        String content = PostReplace(JsonReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult MarkdownFilter(TextFilterAsyncRequest request) {
        String content = PostFilter(MarkdownFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult MarkdownReplace(TextReplaceAsyncRequest request) {
        String content = PostReplace(MarkdownReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    private String PostFilter(String url, TextFilterAsyncRequest request) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("txt", request.Txt);
        dictionary.put("skipBidi", request.SkipBidi);
        dictionary.put("onlyPosition", request.OnlyPosition);
        dictionary.put("url", request.Url);
        dictionary.put("requestId", request.RequestId);
        return doPost(url, dictionary);
    }

    private String PostReplace(String url, TextReplaceAsyncRequest request) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("txt", request.Txt);
        dictionary.put("replaceChar", request.ReplaceChar);
        dictionary.put("reviewReplace", request.ReviewReplace);
        dictionary.put("skipBidi", request.SkipBidi);
        dictionary.put("contactReplace", request.ContactReplace);
        dictionary.put("onlyPosition", request.OnlyPosition);
        dictionary.put("url", request.Url);
        dictionary.put("requestId", request.RequestId);
        return doPost(url, dictionary);
    }

}
