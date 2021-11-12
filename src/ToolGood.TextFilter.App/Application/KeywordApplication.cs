/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using ToolGood.TextFilter.Commons;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Application
{
    public static class KeywordApplication
    {
        #region KeywordType
        public static List<DbKeywordType> GetKeywordTypeList()
        {
            var infos = MemoryCache.Instance.KeywordTypeInfos;
            List<DbKeywordType> result = new List<DbKeywordType>();
            Dictionary<int, DbKeywordType> temp = new Dictionary<int, DbKeywordType>();
            foreach (var info in infos) {
                //if (info == null) { continue; }
                if (info.Id == 0) { continue; }
                var val = new DbKeywordType(info);
                result.Add(val);
                temp.Add(info.Id, val);
            }

            using (var helper = ConfigUtil.GetSqlHelper()) {
                var list = helper.Select<DbKeywordType>();
                foreach (var item in list) {
                    if (temp.TryGetValue(item.TypeId, out DbKeywordType type)) {
                        type.TypeId = type.TypeId;
                        type.ParentId = type.ParentId;
                        type.Level_1_UseType = item.Level_1_UseType;
                        type.Level_2_UseType = item.Level_2_UseType;
                        type.Level_3_UseType = item.Level_3_UseType;
                        type.UseTime = item.UseTime;
                        type.StartTime = item.StartTime;
                        type.EndTime = item.EndTime;

                        type.AddingTime = item.AddingTime;
                        type.ModifyTime = item.ModifyTime;
                    }
                }
            }
            return result;
        }

        public static DbKeywordType GetKeywordType(int id)
        {
            var infos = MemoryCache.Instance.KeywordTypeInfos;

            foreach (var info in infos) {
                //if (info == null) { continue; }
                if (info.Id == 0) { continue; }
                if (info.Id != id) { continue; }
                DbKeywordType restlt = new DbKeywordType(info);

                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var keywordType = helper.FirstOrDefault<DbKeywordType>("select * from KeywordType where TypeId=@0", id);
                    if (keywordType != null) {
                        //restlt.Id = keywordType.Id;
                        //restlt.TypeId = keywordType.TypeId;
                        //restlt.ParentId = keywordType.ParentId;
                        //restlt.Code = keywordType.Code;

                        restlt.Level_1_UseType = keywordType.Level_1_UseType;
                        restlt.Level_2_UseType = keywordType.Level_2_UseType;
                        restlt.Level_3_UseType = keywordType.Level_3_UseType;
                        restlt.UseTime = keywordType.UseTime;
                        restlt.StartTime = keywordType.StartTime;
                        restlt.EndTime = keywordType.EndTime;

                        restlt.AddingTime = keywordType.AddingTime;
                        restlt.ModifyTime = keywordType.ModifyTime;
                    }
                    return restlt;
                }
            }
            return null;
        }

        public static void SetKeywordType(int typeId, byte Level_1_UseType, byte Level_2_UseType, byte Level_3_UseType
            , bool useTime, string startTime, string endTime)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    DateTime? startTime_1 = null;
                    DateTime? endTime_1 = null;
                    if (string.IsNullOrEmpty(startTime) == false) {
                        startTime_1 = DateTime.Parse("2021-" + startTime);
                    }
                    if (string.IsNullOrEmpty(endTime) == false) {
                        endTime_1 = DateTime.Parse("2021-" + endTime);
                    }
                    if (startTime_1 == null && endTime_1 == null) {
                        useTime = false;
                    }

                    var count = helper.Update<DbKeywordType>(@"update KeywordType 
set Level_1_UseType=@1,Level_2_UseType=@2,Level_3_UseType=@3,  ModifyTime=@4,UseTime=@5,startTime=@6,endTime=@7
where typeId=@0",
                          typeId, Level_1_UseType, Level_2_UseType, Level_3_UseType, DateTime.Now
                         , useTime, startTime_1, endTime_1
                          );
                    if (count == 0) {
                        DbKeywordType type = new DbKeywordType();
                        type.TypeId = typeId;
                        type.Level_1_UseType = Level_1_UseType;
                        type.Level_2_UseType = Level_2_UseType;
                        type.Level_3_UseType = Level_3_UseType;
                        type.UseTime = useTime;
                        type.StartTime = startTime_1;
                        type.EndTime = endTime_1;

                        type.AddingTime = DateTime.Now;
                        helper.Insert(type);
                    }

                }
            }
        }
        #endregion


    }
}
