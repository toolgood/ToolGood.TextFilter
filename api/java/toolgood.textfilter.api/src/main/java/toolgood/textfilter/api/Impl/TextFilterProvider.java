package toolgood.textfilter.api.Impl;

import java.util.Hashtable;
import java.util.Map;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.Requests.TextFilterRequest;
import toolgood.textfilter.api.Datas.Requests.TextReplaceRequest;
import toolgood.textfilter.api.Datas.Texts.TextFilterResult;
import toolgood.textfilter.api.Datas.Texts.TextReplaceResult;
import toolgood.textfilter.api.Interfaces.ITextFilterProvider;

public class TextFilterProvider extends ProviderBase implements ITextFilterProvider {

    public TextFilterProvider(TextFilterConfig textFilterConfig) {
        super(textFilterConfig);
    }

    private final String TextFilterUrl = "/api/text-filter";
    private final String TextReplaceUrl = "/api/text-replace";
    private final String HtmlFilterUrl = "/api/html-filter";
    private final String HtmlReplaceUrl = "/api/html-replace";

    private final String JsonFilterUrl = "/api/json-filter";
    private final String JsonReplaceUrl = "/api/json-replace";
    private final String MarkdownFilterUrl = "/api/markdown-filter";
    private final String MarkdownReplaceUrl = "/api/markdown-replace";

    @Override
    public TextFilterResult TextFilter(TextFilterRequest request) {
        String content = PostFilter(TextFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        TextFilterResult result = gson.fromJson(content, TextFilterResult.class);
        return result;
    }

    @Override
    public TextReplaceResult TextReplace(TextReplaceRequest request) {
        String content = PostReplace(TextReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        TextReplaceResult result = gson.fromJson(content, TextReplaceResult.class);
        return result;
    }

    @Override
    public TextFilterResult HtmlFilter(TextFilterRequest request) {
        String content = PostFilter(HtmlFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        TextFilterResult result = gson.fromJson(content, TextFilterResult.class);
        return result;
    }

    @Override
    public TextReplaceResult HtmlReplace(TextReplaceRequest request) {
        String content = PostReplace(HtmlReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        TextReplaceResult result = gson.fromJson(content, TextReplaceResult.class);
        return result;
    }

    @Override
    public TextFilterResult JsonFilter(TextFilterRequest request) {
        String content = PostFilter(JsonFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        TextFilterResult result = gson.fromJson(content, TextFilterResult.class);
        return result;
    }

    @Override
    public TextReplaceResult JsonReplace(TextReplaceRequest request) {
        String content = PostReplace(JsonReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        TextReplaceResult result = gson.fromJson(content, TextReplaceResult.class);
        return result;
    }

    @Override
    public TextFilterResult MarkdownFilter(TextFilterRequest request) {
        String content = PostFilter(MarkdownFilterUrl, request);

        Gson gson = new GsonBuilder().create();
        TextFilterResult result = gson.fromJson(content, TextFilterResult.class);
        return result;
    }

    @Override
    public TextReplaceResult MarkdownReplace(TextReplaceRequest request) {
        String content = PostReplace(MarkdownReplaceUrl, request);

        Gson gson = new GsonBuilder().create();
        TextReplaceResult result = gson.fromJson(content, TextReplaceResult.class);
        return result;
    }

    private String PostFilter(String url, TextFilterRequest request) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("txt", request.Txt);
        dictionary.put("skipBidi", request.SkipBidi);
        dictionary.put("onlyPosition", request.OnlyPosition);
        return doPost(url, dictionary);
    }

    private String PostReplace(String url, TextReplaceRequest request) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("txt", request.Txt);
        dictionary.put("replaceChar", request.ReplaceChar);
        dictionary.put("reviewReplace", request.ReviewReplace);
        dictionary.put("skipBidi", request.SkipBidi);
        dictionary.put("contactReplace", request.ContactReplace);
        dictionary.put("onlyPosition", request.OnlyPosition);

        return doPost(url, dictionary);
    }

}
