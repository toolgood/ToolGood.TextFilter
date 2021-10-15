package toolgood.textfilter.api.Datas.Texts;

import java.util.List;

public class TextReplaceResult {

    /**
     * 返回码：0) 成功，1) 失败
     */
    public int code;
    /**
     * 返回码详情描述
     */
    public String message;
    /**
     * 请求标识
     */
    public String requestId;

    /**
     * 风险级别： PASS：正常内容，建议直接放行 REVIEW：可疑内容，建议人工审核 REJECT：违规内容，建议直接拦截
     */
    public String riskLevel;

    /**
     * 替换后的文本
     */
    public String resultText;

    /**
     * 风险详情，当reviewReplace为false时会出现
     */
    public List<TextFilterDetailItem> details;

}
