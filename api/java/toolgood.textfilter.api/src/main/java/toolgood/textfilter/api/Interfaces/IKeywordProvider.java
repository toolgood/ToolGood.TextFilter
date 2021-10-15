package toolgood.textfilter.api.Interfaces;

import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Keywords.KeywordListResult;

public interface IKeywordProvider {

    /**
     * 获取自定义敏感词列表
     * 
     * @param text     敏感词 可空
     * @param type     敏感等级 ,0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
     * @param page     页数，默认为1
     * @param pageSize 每页个数，默认为20
     * @return
     */
    KeywordListResult GetKeywordList(String text, Integer type, Integer page, Integer pageSize);

    /**
     * 添加自定义敏感词
     * 
     * @param text    敏感词
     * @param type    类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
     * @param comment 备注
     * @return
     */
    CommonResult AddKeyword(String text, int type, String comment);

    /**
     * 编辑自定义敏感词
     * 
     * @param id      敏感词ID
     * @param text    敏感词
     * @param type    类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
     * @param comment 备注
     * @return
     */
    CommonResult EditKeyword(int id, String text, int type, String comment);

    /**
     * 删除自定义敏感词
     * 
     * @param id 敏感词ID
     * @return
     */
    CommonResult DeleteKeyword(int id);

}
