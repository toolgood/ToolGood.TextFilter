/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.IO;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter.App.Datas.TextFilters
{
    public struct KeywordInfo 
    {
        public int Id;
        public ushort TypeId; // 类型
         
        public byte RiskLevel; //风险等级
        public byte MatchType; //匹配类型
        public byte KeywordLength; //字符长度


        /// <summary>
        /// 风险等级
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IllegalWordsSrcRiskLevel GetRiskLevel()
        {
            return (IllegalWordsSrcRiskLevel)RiskLevel;
        }

        /// <summary>
        /// 匹配类型
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IllegalWordsMatchType GetMatchType()
        {
            return (IllegalWordsMatchType)MatchType;
        }


        static internal KeywordInfo[] ReadList(BinaryReader br)
        {
            var len = br.ReadInt32();
            KeywordInfo[] result = new KeywordInfo[len + 1];

            for (int i = 0; i < len; i++) {
                KeywordInfo info = new KeywordInfo();
                info.Id = br.ReadInt32();
                info.TypeId = br.ReadUInt16();
                info.RiskLevel = br.ReadByte();
                info.MatchType = br.ReadByte();
                info.KeywordLength = br.ReadByte();

                result[i + 1] = info;
            }
            return result;
        }
 
    }

}
