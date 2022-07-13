using System;
using System.Text;
using ToolGood.TextFilter.DataBuilder2.Datas;
using ToolGood.TextFilter.DataBuilder2.Enums;

namespace ToolGood.TextFilter
{

    public class IllegalWordsResultItem
    {
 
        internal IllegalWordsResultItem(int start, int end, int index)
        {
            End = end;
            Start = start;
            Index = index;
            Count = 1;
        }

        internal IllegalWordsResultItem(int start, int end, TempKeywordInfo keyInfo)
        {
            End = end;
            Start = start;

            KeywordInfo = keyInfo;
            //TypeCode = keyInfo.Code;
            RiskLevel = keyInfo.GetRiskLevel();
            MatchType = keyInfo.GetMatchType();
            //Count = keyInfo.Count;
            //MultiWords = keyInfo.GetIsMultiWords();
        }
        internal IllegalWordsResultItem(int start, int end, int index, TempKeywordInfo keyInfo)
        {
            End = end;
            Start = start;
            Index = index;

            KeywordInfo = keyInfo;
            //TypeCode = keyInfo.Code;
            RiskLevel = keyInfo.GetRiskLevel();
            MatchType = keyInfo.GetMatchType();
            //MultiWords = keyInfo.GetIsMultiWords();
        }

        internal IllegalWordsResultItem(string keyword, int start, int end, int index)
        {
            Keyword = keyword;
            End = end;
            Start = start;
            Index = index;
        }
        //internal IllegalWordsResultItem(string keyword, int start, int end, int index, MiniKeywordInfo keyInfo)
        //{
        //    Keyword = keyword;
        //    End = end;
        //    Start = start;
        //    Index = index;

        //    KeywordInfo = keyInfo;
        //    TypeCode = keyInfo.Code;
        //    UseType = keyInfo.UseType;
        //    MatchType = keyInfo.MatchType;
        //    Count = keyInfo.Count;
        //    MultiWords = keyInfo.MultiWords;
        //}



        #region 附助
        /// <summary>
        /// 类型CODE
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 使用类型，附助
        /// </summary>
        public EmRiskLevel RiskLevel { get; set; }

        /// <summary>
        /// 匹配类型
        /// </summary>
        public EmMatchType MatchType { get; set; }

        /// <summary>
        /// 词频，附助
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public byte KeywordType { get; set; }

        /// <summary>
        /// 多词组
        /// </summary>
        public bool MultiWords { get; set; }

        #endregion


        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start { get; private set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; private set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; private set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; private set; }

        public TempKeywordInfo KeywordInfo { get; private set; }





        //public void Merge(IllegalWordsResultItem item)
        //{
        //    if (string.IsNullOrEmpty(Keyword)) {
        //        Keyword = item.Keyword;
        //    }
        //    KeywordInfo = item.KeywordInfo;
        //    TypeCode = item.KeywordInfo.Code;
        //    UseType = item.KeywordInfo.UseType;
        //    MatchType = item.KeywordInfo.MatchType;
        //    Count = item.KeywordInfo.Count;
        //    MultiWords = item.KeywordInfo.MultiWords;
        //}

        public override string ToString()
        {
            return Start.ToString() + "|" + Keyword;
        }
    }

}
