/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;

namespace ToolGood.TextFilter.App.Datas.Results
{
    public class IllegalWordsReplaceResult 
    {
        public IllegalWordsRiskLevel RiskLevel;

        public string Result;

        /// <summary>
        /// 建议复审 敏感词
        /// </summary>
        public List<SingleWordsResult> ReviewSingleItems;

        /// <summary>
        /// 建议复审 多组敏感词
        /// </summary>
        public List<MultiWordsResult> ReviewMultiItems;

        public IllegalWordsReplaceResult()
        {
            RiskLevel = IllegalWordsRiskLevel.Pass;
        }

    }
}
