package toolgood.textfilter.api.Impl;

import java.util.Hashtable;
import java.util.Map;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.KeywordTypes.KeywordtypeListResult;
import toolgood.textfilter.api.Interfaces.IKeywordTypeProvider;

public class KeywordTypeProvider extends ProviderBase implements IKeywordTypeProvider {

    public KeywordTypeProvider(TextFilterConfig textFilterConfig) {
        super(textFilterConfig);
    }

    private final String GetListUrl = "/api/get-keywordtype-list";
    private final String SetKeywordTypeUrl = "/api/set-keywordtype";

    @Override
    public KeywordtypeListResult GetList() {
        String content = doGet(GetListUrl);

        Gson gson = new GsonBuilder().create();
        KeywordtypeListResult result = gson.fromJson(content, KeywordtypeListResult.class);
        return result;
    }

    @Override
    public CommonResult SetKeywordType(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType,
            Boolean useTime, String startTime, String endTime) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("typeId", typeId);
        dictionary.put("level_1_UseType", level_1_UseType);
        dictionary.put("level_2_UseType", level_2_UseType);
        dictionary.put("level_3_UseType", level_3_UseType);
        dictionary.put("useTime", useTime);
        dictionary.put("startTime", startTime);
        dictionary.put("endTime", endTime);
        String content = doPost(SetKeywordTypeUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

}
