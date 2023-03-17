/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
namespace ToolGood.TextFilter
{
    /// <summary>
    /// 违规词匹配类型
    /// </summary>
    public enum IllegalWordsMatchType //: byte
    {
        /// <summary>
        /// 普通匹配
        /// </summary>
        PartMatch = 0,
        /// <summary>
        /// 匹配句子开始
        /// </summary>
        MatchTextStart = 1,
        /// <summary>
        /// 匹配整句话
        /// </summary>
        MatchText = 2,
        /// <summary>
        /// 匹配句子结尾
        /// </summary>
        MatchTextEnd = 3,
        /// <summary>
        /// 匹配句子开始 或 结尾
        /// </summary>
        MatchTextStartOrEnd = 4,

    }

}
