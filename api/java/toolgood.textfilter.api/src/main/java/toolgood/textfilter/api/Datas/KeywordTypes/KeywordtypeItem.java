package toolgood.textfilter.api.Datas.KeywordTypes;

public class KeywordtypeItem {

    /**
     * 敏感词类型ID
     */
    public String typeId;

    /**
     * 上级类型ID
     */
    public String parentId;

    /**
     * 敏感词类型CODE
     */
    public String code;

    /**
     * 敏感词类型名
     */
    public String name;

    /**
     * 内置触线类型1：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 1
     */
    public int level_1_UseType;

    /**
     * 内置危险类型2：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
     */
    public int level_2_UseType;

    /**
     * 内置违规类型3：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
     */
    public int level_3_UseType;

    /**
     * 是否启用指定日期
     */
    public Boolean useTime;

    /**
     * 开始日期：格式 MM-dd
     */
    public String startTime;
    /**
     * 结束日期：格式 MM-dd
     */
    public String endTime;

}
