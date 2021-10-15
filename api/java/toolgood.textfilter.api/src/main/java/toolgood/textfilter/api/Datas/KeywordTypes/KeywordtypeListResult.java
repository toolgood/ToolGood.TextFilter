package toolgood.textfilter.api.Datas.KeywordTypes;

import java.util.List;

public class KeywordtypeListResult {
    /**
     * 返回码：0) 成功，1) 失败
     */
    public int code;
    /**
     * 返回码详情描述
     */
    public String message;

    /**
     * 敏感词类型列表
     */
    public List<KeywordtypeItem> data;
}
