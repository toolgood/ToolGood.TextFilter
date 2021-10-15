/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Linq;
using ToolGood.TextFilter.App.Datas.TextFilters;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Commons
{
    public sealed class CustomKeywordType
    {
        public string Code;
        public IllegalWordsRiskLevel? RiskLevel_1;
        public IllegalWordsRiskLevel? RiskLevel_2;
        public IllegalWordsRiskLevel? RiskLevel_3;
        public bool UseTime;
        public DateTime? StartTime;
        public DateTime? EndTime;


        public static CustomKeywordType[] Build(KeywordTypeInfo[] infos, List<DbKeywordType> keywordTypes)
        {
            var len = infos.Max(q => q.Id) + 1;

            CustomKeywordType[] result = new CustomKeywordType[len];
            CustomKeywordType[] parent = new CustomKeywordType[len];
            int[] parentId = new int[len];

            for (int i = 0; i < infos.Length; i++) {
                var info = infos[i];
                //if (info == null) { continue; }
                if (info.Id == 0) { continue; }
                var type = new CustomKeywordType();
                type.Code = info.Code;
                type.UseTime = info.UseTime;
                type.StartTime = info.StartTime;
                type.EndTime = info.EndTime;
                result[info.Id] = type;
                parentId[info.Id] = info.ParentId;
            }
            for (int i = 0; i < infos.Length; i++) {
                var info = infos[i];
                //if (info == null) { continue; }
                if (info.Id == 0) { continue; }
                parent[info.Id] = result[info.ParentId];
            }

            for (int i = 0; i < keywordTypes.Count; i++) {
                var type = keywordTypes[i];
                if (type == null) { continue; }
                if (type.Level_1_UseType != null) {
                    result[type.TypeId].RiskLevel_1 = (IllegalWordsRiskLevel)type.Level_1_UseType;
                }
                if (type.Level_2_UseType != null) {
                    result[type.TypeId].RiskLevel_2 = (IllegalWordsRiskLevel)type.Level_2_UseType;
                }
                if (type.Level_3_UseType != null) {
                    result[type.TypeId].RiskLevel_3 = (IllegalWordsRiskLevel)type.Level_3_UseType;
                }
                result[type.TypeId].UseTime = type.UseTime;
                result[type.TypeId].StartTime = type.StartTime;
                result[type.TypeId].EndTime = type.EndTime;
            }
            for (int i = 0; i < len; i++) {
                var type = result[i];
                if (type != null) {
                    type.RiskLevel_1 = GetRiskLevel_1(parent, parentId, type, i);
                    type.RiskLevel_2 = GetRiskLevel_2(parent, parentId, type, i);
                    type.RiskLevel_3 = GetRiskLevel_3(parent, parentId, type, i);
                }
            }
            return result;
        }


        private static IllegalWordsRiskLevel GetRiskLevel_1(CustomKeywordType[] parent, int[] parentId, CustomKeywordType type, int postion)
        {
            var t = type;
            var p = postion;
            while (t != null) {
                if (t.RiskLevel_1 != null) {
                    return t.RiskLevel_1.Value;
                }
                t = parent[p];
                p = parentId[p];
            }
            return IllegalWordsRiskLevel.Review;
        }

        private static IllegalWordsRiskLevel GetRiskLevel_2(CustomKeywordType[] parent, int[] parentId, CustomKeywordType type, int postion)
        {
            var t = type;
            var p = postion;
            while (t != null) {
                if (t.RiskLevel_2 != null) {
                    return t.RiskLevel_2.Value;
                }
                t = parent[p];
                p = parentId[p];
            }
            return IllegalWordsRiskLevel.Reject;
        }

        private static IllegalWordsRiskLevel GetRiskLevel_3(CustomKeywordType[] parent, int[] parentId, CustomKeywordType type, int postion)
        {
            var t = type;
            var p = postion;
            while (t != null) {
                if (t.RiskLevel_3 != null) {
                    return t.RiskLevel_3.Value;
                }
                t = parent[p];
                p = parentId[p];
            }
            return IllegalWordsRiskLevel.Reject;
        }

    }


}
