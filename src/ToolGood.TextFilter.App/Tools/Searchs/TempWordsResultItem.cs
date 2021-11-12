/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    public class TempWordsResultItem 
    {
        internal TempWordsResultItem(int start, int end, FenciKeywordInfo keyInfo)
        {
            End = end;
            Start = start;
            Count = keyInfo.Count;
            EmotionalColor = keyInfo.EmotionalColor;
            IsFenci = true;
        }

        internal TempWordsResultItem(int start, int end, KeywordInfo keyInfo)
        {
            End = end;
            Start = start;
            SingleIndex = keyInfo.Id;
            SrcRiskLevel = keyInfo.GetRiskLevel();
            MatchType = keyInfo.GetMatchType();
            TypeId = keyInfo.TypeId;
            Count = 1;
        }


        public int NplIndex;

        /// <summary>
        /// 违规词类型 原敏感词分险等级
        /// </summary>
        public IllegalWordsSrcRiskLevel? SrcRiskLevel;

        /// <summary>
        /// 敏感词分险等级
        /// </summary>
        public IllegalWordsRiskLevel? RiskLevel;

        /// <summary>
        /// 匹配类型
        /// </summary>
        public IllegalWordsMatchType? MatchType;

        public bool IsFenci;

        /// <summary>
        /// 索引---单组索引
        /// </summary>
        public int SingleIndex;

        /// <summary>
        /// 自定义ID
        /// </summary>
        public int DiyIndex;

        /// <summary>
        /// 
        /// </summary>
        public int TypeId;

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start;

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End;

        /// <summary>
        /// 词频，附助
        /// </summary>
        public int Count;

        /// <summary>
        /// 0 未标注
        /// 1-7 程度 4，3，2，0.5，-0.3 和-0.5 -1。
        /// 10-19 褒义
        /// 20-29 贬义
        /// </summary>
        public sbyte EmotionalColor;

        #region GetEmotionScore  CalcEmotionScore
        ///// <summary>
        /// 0 未标注
        /// 1-7 程度 4，3，2，0.5，-0.3 和-0.5 -1。
        /// 10-19 褒义
        /// 20-29 贬义
        ///// </summary>
        private static double[] _score = new double[]
        {
            1,4,3,2,0.5,-0.3,-0.5,-1,0,0,
            1.0,1.1,1.2,1.3,1.4,1.5,1.6,1.7,1.8,1.9,
            -1.0,-1.1,-1.2,-1.3,-1.4,-1.5,-1.6,-1.7,-1.8,-1.9,
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetEmotionScore()
        {
            return _score[EmotionalColor];
        }

        #endregion

        public bool ContainsRange(TempWordsResultItem item)
        {
            if (this.Start <= item.Start) {
                if (this.End >= item.End) {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"[{Start}-{End}]";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetPostion()
        {
            return (((uint)Start) << 10) | ((uint)End & 0x3ff);
        }

    }
}
