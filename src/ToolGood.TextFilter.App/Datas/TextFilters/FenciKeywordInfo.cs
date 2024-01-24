/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.IO;

namespace ToolGood.TextFilter.App.Datas.TextFilters
{
    public struct FenciKeywordInfo
    {
        /// <summary>
        /// 文字长度
        /// </summary>
        public sbyte KeywordLength;

        /// <summary>
        /// 词频
        /// </summary>
        public int Count;

        /// <summary>
        /// 0 未标注
        /// 1-7 程度 4，3，2，0.5，-0.3 和-0.5 -1。
        /// 10-19 褒义
        /// 20-29 贬义
        /// </summary>
        public sbyte EmotionalColor;


        static internal FenciKeywordInfo[] ReadList(BinaryReader br)
        {
            var len = br.ReadInt32();
            FenciKeywordInfo[] result = new FenciKeywordInfo[len];

            for (int i = 0; i < len; i++) {
                FenciKeywordInfo info = new FenciKeywordInfo();
                info.KeywordLength = br.ReadSByte();
                info.Count = br.ReadInt32();
                info.EmotionalColor = br.ReadSByte();
                result[i] = info;
            }
            return result;
        }


    }
}
