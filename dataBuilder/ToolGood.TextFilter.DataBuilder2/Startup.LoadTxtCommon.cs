using System;
using System.Collections.Generic;
using System.Linq;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {

        private void LoadTxtCommon(SqlHelper helper, TxtCommonCache txtCommonCache)
        {
            var types = helper.Select<DbTxtCommonType>();

            foreach (var type in types) {
                if (type.Name.Length == 1) {
                    var commons = helper.Select<DbTxtCommon>("where IsDelete=0 and TxtCommonTypeId=@0", type.Id);
                    txtCommonCache.AddTxtCommon(type, commons);
                }
            }
            foreach (var type in types) {
                if (type.Name.Length > 1) {
                    var ids = GetTypes(type, types);
                    var commons = helper.Select<DbTxtCommon>($"where IsDelete=0 and TxtCommonTypeId in ({string.Join(",",ids)})");
                    txtCommonCache.AddTxtCommon(type, commons);
                }
            }
        }

        private List<int> GetTypes(DbTxtCommonType type, List<DbTxtCommonType> types)
        {
            List<int> typeIds = new List<int>();
            typeIds.Add(type.Id);

            Stack<DbTxtCommonType> stack = new Stack<DbTxtCommonType>();
            stack.Push(type);
            while (stack.TryPop(out DbTxtCommonType ty)) {
                if (string.IsNullOrEmpty(ty.SubTypes)) {
                    continue;
                }
                var subs = ty.SubTypes.Split('|', StringSplitOptions.RemoveEmptyEntries);
                foreach (var sub in subs) {
                    var t = types.Where(q => q.Name == sub).FirstOrDefault();
                    if (typeIds.Contains(t.Id) == false) {
                        typeIds.Add(t.Id);
                        stack.Push(t);
                    }
                }
            }
            return typeIds;
        }


    }
}
