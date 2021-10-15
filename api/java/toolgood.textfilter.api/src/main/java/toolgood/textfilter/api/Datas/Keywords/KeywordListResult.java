package toolgood.textfilter.api.Datas.Keywords;

import java.util.List;

public class KeywordListResult {
    /**
     * 返回码：0) 成功，1) 失败
     */
    public int code;
    /**
     * 返回码详情描述
     */
    public String message;

    /**
     * 总个数
     */
    public Integer total;

    /**
     * 自定义敏感词列表
     */
    public List<KeywordItem> data;
}
