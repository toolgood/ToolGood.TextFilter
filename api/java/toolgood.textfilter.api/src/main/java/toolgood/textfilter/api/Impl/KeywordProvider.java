package toolgood.textfilter.api.Impl;

import java.util.Hashtable;
import java.util.Map;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Keywords.KeywordListResult;
import toolgood.textfilter.api.Interfaces.IKeywordProvider;

public class KeywordProvider extends ProviderBase implements IKeywordProvider {

    public KeywordProvider(TextFilterConfig textFilterConfig) {
        super(textFilterConfig);
    }

    private final String GetKeywordListUrl = "/api/get-keyword-list";
    private final String AddKeywordUrl = "/api/add-keyword";
    private final String EditKeywordUrl = "/api/edit-keyword";
    private final String DeleteKeywordUrl = "/api/delete-keyword";

    @Override
    public KeywordListResult GetKeywordList(String text, Integer type, Integer page, Integer pageSize) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("text", text);
        dictionary.put("type", type);
        dictionary.put("page", page);
        dictionary.put("pageSize", pageSize);
        String content = doPost(GetKeywordListUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        KeywordListResult result = gson.fromJson(content, KeywordListResult.class);
        return result;
    }

    @Override
    public CommonResult AddKeyword(String text, int type, String comment) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("text", text);
        dictionary.put("type", type);
        dictionary.put("comment", comment);
        String content = doPost(AddKeywordUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult EditKeyword(int id, String text, int type, String comment) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("id", id);
        dictionary.put("text", text);
        dictionary.put("type", type);
        dictionary.put("comment", comment);
        String content = doPost(EditKeywordUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult DeleteKeyword(int id) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("id", id);
        String content = doPost(DeleteKeywordUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

}
