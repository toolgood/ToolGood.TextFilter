/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter
{
    public class TempMultiWordsResult 
    {

        public int ResultIndex;

        public TempWordsResultItem[] KeywordIndexs;// = new List<TempWordsResultItem>();

        public TempMultiWordsResult(int resultIndex, TempWordsResultItem[] keywordIndexs)
        {
            ResultIndex = resultIndex;
            KeywordIndexs = keywordIndexs;
        }

        public bool ContainsRange(TempWordsResultItem[] newIndexs)
        {
            if (KeywordIndexs.Length != newIndexs.Length) { return false; }

            for (int i = 0; i < KeywordIndexs.Length; i++) {
                if (KeywordIndexs[i].ContainsRange(newIndexs[i])==false) {
                    return false;
                }
            }
            return true;
        }


    }

}
