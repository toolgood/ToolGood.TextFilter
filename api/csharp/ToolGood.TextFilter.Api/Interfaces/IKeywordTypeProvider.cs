using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.KeywordTypes;

namespace ToolGood.TextFilter.Api.Interfaces
{
    public interface IKeywordTypeProvider
    {
        /// <summary>
        /// 获取敏感词类型列表 
        /// </summary>
        /// <returns></returns>
        KeywordtypeListResult GetList();
        /// <summary>
        /// 获取敏感词类型列表
        /// </summary>
        /// <returns></returns>
        Task<KeywordtypeListResult> GetListAsync();

        /// <summary>
        /// 设置敏感词类型
        /// </summary>
        /// <param name="typeId">敏感词类型ID</param>
        /// <param name="level_1_UseType">内置触线类型1：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 1</param>
        /// <param name="level_2_UseType">内置危险类型2：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0</param>
        /// <param name="level_3_UseType">内置违规类型3：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0</param>
        /// <returns></returns>
        CommonResult SetKeywordType(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType, bool useTime = false, string startTime = "", string endTime = "");
        /// <summary>
        /// 设置敏感词类型
        /// </summary>
        /// <param name="typeId">敏感词类型ID</param>
        /// <param name="level_1_UseType">内置触线类型1：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 1</param>
        /// <param name="level_2_UseType">内置危险类型2：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0</param>
        /// <param name="level_3_UseType">内置违规类型3：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过, 默认 0</param>
        /// <returns></returns>
        Task<CommonResult> SetKeywordTypeAsync(int typeId, int level_1_UseType, int level_2_UseType, int level_3_UseType, bool useTime = false, string startTime = "", string endTime = "");

 

    }
}
