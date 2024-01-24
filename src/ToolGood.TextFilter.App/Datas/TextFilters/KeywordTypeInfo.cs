/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

using System;
using System.IO;

namespace ToolGood.TextFilter.App.Datas.TextFilters
{
    public struct KeywordTypeInfo
    {
        public ushort Id;
        public ushort ParentId; // 父id
        public string Code; //编码
        public string Name;  // 名称
        public bool UseTime; // 开始时间区间
        public DateTime? StartTime; // 开始时间
        public DateTime? EndTime; // 结束时间

        static internal KeywordTypeInfo[] ReadList(BinaryReader br)
        {
            var len = br.ReadInt32();
            KeywordTypeInfo[] temps = new KeywordTypeInfo[len];
            for (int i = 0; i < len; i++) {
                KeywordTypeInfo info = new KeywordTypeInfo();
                info.Id = br.ReadUInt16();
                info.ParentId = br.ReadUInt16();
                info.Code = br.ReadString();
                info.Name = br.ReadString();
                info.UseTime = br.ReadBoolean();
                var bt = br.ReadInt64();
                if (bt != 0) {
                    info.StartTime = new DateTime(2000, 1, 1).AddSeconds(bt);
                }
                bt = br.ReadInt64();
                if (bt != 0) {
                    info.EndTime = new DateTime(2000, 1, 1).AddSeconds(bt);
                }
                temps[i] = info;
            }
            var maxId = temps[temps.Length - 1].Id;
            KeywordTypeInfo[] result = new KeywordTypeInfo[maxId + 1];
            foreach (var temp in temps) {
                result[temp.Id] = temp;
            }
            return result;
        }


    }
}
