package toolgood.textfilter.api.Datas.Texts;

import java.util.List;

public class TextFilterResult {

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
     * 风险类别：Char：非正常字符 Politics：涉政文本 Terrorism：涉恐文本 Porn：涉黄文本 Gamble：涉赌文本 Drug：涉毒文本
     * Contraband：非法交易 Abuse：辱骂文本 Other：推广引诱诈骗 Custom：自定义敏感词
     */
    public String riskCode;

    /**
     * 情感值，>0 正面，<0 负向 ，riskLevel为REVIEW（可疑内容），会出现此值
     */
    public Float sentimentScore;

    /**
     * 风险详情, 详见 details
     */
    public List<TextFilterDetailItem> details;

    /**
     * 联系方式详情, 详见 contacts
     */
    public List<TextFilterContactItem> contacts;
}
