package toolgood.textfilter.api.Datas.Requests;

import toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest;

public class TextReplaceAsyncRequest {
    /**
     * 需要检测的文本
     */
    public String Txt;

    /**
     * 是否跳过 Bidi 字符，默认 false
     */
    public boolean SkipBidi;
    /**
     * 替换符号, 默认 星号
     */
    public char ReplaceChar;
    /**
     * 人工审核替换，默认true
     */
    public boolean ReviewReplace;
    /**
     * 联系字符串替换，默认true
     */
    public boolean ContactReplace;
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

    public TextReplaceAsyncRequest() {
        ReviewReplace = true;
        ContactReplace = true;
        SkipBidi = false;
        ReplaceChar = '*';
    }

    /**
     * 
     * @param txt            需要检测的文本
     * @param replaceChar    替换符号, 默认 星号
     * @param reviewReplace  人工审核替换，默认true
     * @param contactReplace 联系字符串替换，默认true
     * @param skipBidi       是否跳过 Bidi 字符
     * @param onlyPosition   只显示位置，不显示匹配文字
     * @param requestId      请求标识，为空时会自动生成
     * @param url            异步回调地址 可空
     */
    public TextReplaceAsyncRequest(String txt, char replaceChar, boolean reviewReplace, boolean contactReplace,
            boolean skipBidi, boolean onlyPosition, String requestId, String url) {
        Txt = txt;
        SkipBidi = skipBidi;
        ReplaceChar = replaceChar;
        ReviewReplace = reviewReplace;
        ContactReplace = contactReplace;
        OnlyPosition = onlyPosition;
        RequestId = requestId;
        Url = url;
    }

    public TextReplaceAsyncGrpcRequest ToGrpcRequest() {
        return TextReplaceAsyncGrpcRequest.newBuilder().setTxt(Txt).setSkipBidi(SkipBidi)
                .setReviewReplace(ReviewReplace).setReplaceChar(ReplaceChar).setContactReplace(ContactReplace)
                .setOnlyPosition(OnlyPosition).setUrl(Url).setRequestId(RequestId).build();

    }

}
