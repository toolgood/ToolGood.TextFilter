using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.DFAs;
using ToolGood.TextFilter.DataBuilder2.Enums;

namespace ToolGood.TextFilter.DataBuilder2.Datas
{
    public class TempKeywordInfo
    {
 
        public bool IsTxtCommon { get; set; }

        public int TxtId { get; set; }
        public int TxtTypeId { get; set; }

        public int IsRepeatWords { get; set; }

        public int TxtFilterTypeId { get; set; }
        /// <summary>
        /// 类型，0）单组专属名词,1）动词，2）名词前缀，3）名词，4）名词后缀，5）多组敏感词
        /// </summary>
        public int TextPart { get; set; }



        public int Id { get; set; }
        public string FullKeyword { get; set; }
        public string Keyword { get; set; }

        /// <summary>
        /// 当前风险
        /// </summary>
        public Exp CurrExp { get; set; }

        /// <summary>
        /// 类型
        /// 1-3位 多词组 间隔 0-15 ，0）无限, 1-7) n*5
        /// 4-5位 风险等级
        /// 6-8位 匹配类型
        /// </summary>
        public byte KeywordType { get; set; }


        #region 多词组 间隔
        public int GetMultiWordsInterval()
        {
            return (KeywordType & 0b_0000_0111) * 5;
        }
        public void SetMultiWordsInterval(int interval)
        {
            var t = interval / 5;
            if (t > 7) { t = 7; }
            if (t < 0) { t = 0; }
            KeywordType = (byte)((KeywordType & 0b_1111_1000) | t);
        }
        #endregion

        #region 风险等级
        public EmRiskLevel GetRiskLevel()
        {
            return (EmRiskLevel)((KeywordType & 0b_0001_1000) >> 3);
        }
        public void SetRiskLevel(EmRiskLevel riskLevel)
        {
            var t = (int)riskLevel << 3;
            KeywordType = (byte)((KeywordType & 0b_1110_0111) | t);
        }
        #endregion

        #region 匹配类型
        public EmMatchType GetMatchType()
        {
            return (EmMatchType)(KeywordType >> 5);
        }
        public void SetMatchType(EmMatchType matchType)
        {
            var t = (int)matchType << 5;
            KeywordType = (byte)((KeywordType & 0b_0001_1111) | t);
        }
        #endregion


        public override string ToString()
        {
            return Keyword;
        }
    }

}
