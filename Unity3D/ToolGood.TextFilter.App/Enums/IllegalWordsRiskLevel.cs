/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
namespace ToolGood.TextFilter
{
    /// <summary>
    /// 敏感词分险等级
    /// </summary>
    public enum IllegalWordsRiskLevel : byte
    {
        /// <summary>
        /// 建议屏蔽
        /// </summary>
        Reject = 0,

        /// <summary>
        /// 建议复审
        /// </summary>
        Review = 1,

        /// <summary>
        /// 建议通过
        /// </summary>
        Pass = 2,

    }
}
