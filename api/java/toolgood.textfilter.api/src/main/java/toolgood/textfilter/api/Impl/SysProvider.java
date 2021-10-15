package toolgood.textfilter.api.Impl;

import java.util.Hashtable;
import java.util.Map;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import toolgood.textfilter.api.TextFilterConfig;
import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Sys.SysInfo;
import toolgood.textfilter.api.Interfaces.ISysProvider;

public class SysProvider extends ProviderBase implements ISysProvider {

    public SysProvider(TextFilterConfig textFilterConfig) {
        super(textFilterConfig);
    }

    private final String UpdateSystemUrl = "/api/sys-update";
    private final String RefreshUrl = "/api/sys-refresh";
    private final String InfoUrl = "/api/sys-info";
    private final String InitDataUrl = "/api/sys-init-Data";
    private final String GCCollectUrl = "/api/sys-GC-Collect";

    @Override
    public CommonResult UpdateSystem(String textFilterNoticeUrl, String textReplaceNoticeUrl, String skipword) {
        Map<String, Object> dictionary = new Hashtable<String, Object>();
        dictionary.put("textFilterNoticeUrl", textFilterNoticeUrl);
        dictionary.put("textReplaceNoticeUrl", textReplaceNoticeUrl);
        dictionary.put("skipword", skipword);
        String content = doPost(UpdateSystemUrl, dictionary);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult Refresh() {
        String content = doGet(RefreshUrl);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public SysInfo Info() {
        String content = doGet(InfoUrl);

        Gson gson = new GsonBuilder().create();
        SysInfo result = gson.fromJson(content, SysInfo.class);
        return result;
    }

    @Override
    public CommonResult InitData() {
        String content = doGet(InitDataUrl);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

    @Override
    public CommonResult GCCollect() {
        String content = doGet(GCCollectUrl);

        Gson gson = new GsonBuilder().create();
        CommonResult result = gson.fromJson(content, CommonResult.class);
        return result;
    }

}
