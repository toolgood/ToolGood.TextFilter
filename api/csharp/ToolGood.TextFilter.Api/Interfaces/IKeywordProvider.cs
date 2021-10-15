using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Keywords;

namespace ToolGood.TextFilter.Api.Interfaces
{
    public interface IKeywordProvider
    {
        /// <summary>
        /// 获取自定义敏感词列表
        /// </summary>
        /// <param name="text">敏感词 可空</param>
        /// <param name="type">敏感等级 ,0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="page">页数，默认为1</param>
        /// <param name="pageSize">每页个数，默认为20</param>
        /// <returns></returns>
        KeywordListResult GetKeywordList(string text, int? type, int? page, int? pageSize);
        /// <summary>
        /// 获取自定义敏感词列表
        /// </summary>
        /// <param name="text">敏感词 可空</param>
        /// <param name="type">敏感等级 ,0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="page">页数，默认为1</param>
        /// <param name="pageSize">每页个数，默认为20</param>
        /// <returns></returns>
        Task<KeywordListResult> GetKeywordListAsync(string text, int? type, int? page, int? pageSize);

        /// <summary>
        /// 添加自定义敏感词
        /// </summary>
        /// <param name="text">敏感词</param>
        /// <param name="type">类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="comment">备注</param>
        /// <returns></returns>
        CommonResult AddKeyword(string text, int type, string comment);

        /// <summary>
        /// 添加自定义敏感词
        /// </summary>
        /// <param name="text">敏感词</param>
        /// <param name="type">类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="comment">备注</param>
        /// <returns></returns>
        Task<CommonResult> AddKeywordAsync(string text, int type, string comment);


        /// <summary>
        /// 添加自定义敏感词
        /// </summary>
        /// <param name="id">敏感词ID</param>
        /// <param name="text">敏感词</param>
        /// <param name="type">类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="comment">备注</param>
        /// <returns></returns>
        CommonResult EditKeyword(int id, string text, int type, string comment);
        /// <summary>
        /// 添加自定义敏感词
        /// </summary>
        /// <param name="id">敏感词ID</param>
        /// <param name="text">敏感词</param>
        /// <param name="type">类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过</param>
        /// <param name="comment">备注</param>
        /// <returns></returns>
        Task<CommonResult> EditKeywordAsync(int id, string text, int type, string comment);

        /// <summary>
        /// 删除自定义敏感词
        /// </summary>
        /// <param name="id">敏感词ID</param>
        /// <returns></returns>
        CommonResult DeleteKeyword(int id);
        /// <summary>
        /// 删除自定义敏感词
        /// </summary>
        /// <param name="id">敏感词ID</param>
        /// <returns></returns>
        Task<CommonResult> DeleteKeywordAsync(int id);


    }
}
