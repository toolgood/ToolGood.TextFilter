package toolgood.textfilter.api.Datas.Requests;

import toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest;

public class TextFilterAsyncRequest {
    /**
     * 需要检测的文本
     */
    public String Txt;

    /**
     * 是否跳过 Bidi 字符，默认 false
     */
    public boolean SkipBidi;

    /**
     * 只显示位置，不显示匹配文字
     */
    public boolean OnlyPosition;

    /**
     * 请求标识，为空时会自动生成
     */
    public String RequestId;

    /**
     * 异步回调地址 可空
     */
    public String Url;

    public TextFilterAsyncRequest() {
        SkipBidi = false;
    }

    /**
     * 
     * @param txt          需要检测的文本
     * @param skipBidi     是否跳过 Bidi 字符
     * @param onlyPosition 是否只显示位置，不显示匹配文字
     * @param requestId    请求标识，为空时会自动生成
     * @param url          异步回调地址 可空
     */
    public TextFilterAsyncRequest(String txt, boolean skipBidi, boolean onlyPosition, String requestId, String url) {
        Txt = txt;
        SkipBidi = skipBidi;
        OnlyPosition = onlyPosition;
        RequestId = requestId;
        Url = url;
    }

    public TextFindAllAsyncGrpcRequest ToGrpcRequest() {
        return TextFindAllAsyncGrpcRequest.newBuilder().setTxt(Txt).setSkipBidi(SkipBidi).setOnlyPosition(OnlyPosition)
                .setUrl(Url).setRequestId(RequestId).build();
    }
}
