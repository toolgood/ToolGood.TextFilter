package toolgood.textfilter.api.Datas.Requests;

import toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest;

public class TextFilterRequest {
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

    public TextFilterRequest() {
        SkipBidi = false;
    }

    /**
     * 
     * @param txt          需要检测的文本
     * @param skipBidi     是否跳过 Bidi 字符
     * @param onlyPosition 是否只显示位置，不显示匹配文字
     */
    public TextFilterRequest(String txt, boolean skipBidi, boolean onlyPosition) {
        Txt = txt;
        SkipBidi = skipBidi;
        OnlyPosition = onlyPosition;
    }

    public TextFindAllGrpcRequest ToGrpcRequest() {
        return TextFindAllGrpcRequest.newBuilder().setTxt(Txt).setSkipBidi(SkipBidi).setOnlyPosition(OnlyPosition)
                .build();
    }

}
