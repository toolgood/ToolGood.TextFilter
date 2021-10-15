package toolgood.textfilter.api.Datas.Keywords;

import java.util.Date;

public class KeywordItem {
    /**
     * 自定义敏感词ID
     */
    public int id;

    /**
     * 自定义敏感词
     */
    public String text;

    /**
     * 类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
     */
    public int type;

    /**
     * 备注
     */
    public String comment;

    /**
     * 添加日期
     */
    public Date addingTime;

    /**
     * 修改日期
     */
    public Date modifyTime;
}
