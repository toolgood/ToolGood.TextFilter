/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter
{
    /// <summary>
    /// 违规词类型
    /// </summary>
    public enum IllegalWordsSrcRiskLevel : byte
    {

        Part = 255,
        /// <summary>
        /// 联系方式
        /// </summary>
        ContactPart = 254,


        /// <summary>
        /// 违规
        /// </summary>
        Violation = 3,

        /// <summary>
        ///  危险
        /// </summary>
        Dangerous = 2,

        /// <summary>
        /// 触线
        /// </summary>
        Sensitive = 1,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

    }

}
