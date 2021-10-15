package toolgood.textfilter.api.Interfaces;

import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.KeywordTypes.KeywordtypeListResult;

public interface IKeywordTypeProvider {

    /**
     * 获取敏感词类型列表
     * 
     * @return
     */
    KeywordtypeListResult GetList();

    /**
     * 设置敏感词类型
     * 
     * @param typeId          敏感词类型ID
     * @param level_1_UseType 内置触线类型1：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 1
     * @param level_2_UseType 内置触线类型2：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
     * @param level_3_UseType 内置触线类型2：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 0
     * @param useTime         是否启用指定日期
     * @param startTime       开始日期：格式 MM-dd
     * @param endTime         结束日期：格式 MM-dd
     * @return
     */
    CommonResult SetKeywordType(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType,
            Boolean useTime, String startTime, String endTime);

}
