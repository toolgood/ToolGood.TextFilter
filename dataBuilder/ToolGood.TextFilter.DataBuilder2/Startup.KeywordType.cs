using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        private const string _tempKeywordTypePath = "temp/KeywordType.dat";


        public void BuildKeywordType(SqlHelper helper)
        {
            var types = helper.Select<DbTxtCustomType>("where IsDelete=0 and IsFrozen=0");
     
            var fs = File.Open(_tempKeywordTypePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(types.Count);
            foreach (var item in types) {
                bw.Write((ushort)item.Id);
                bw.Write((ushort)item.ParentId);
                bw.Write(item.NameEn);
                bw.Write(item.Name);
                bw.Write(item.IsCheckTime);
                if (string.IsNullOrEmpty(item.StartTime)) {
                    bw.Write((long)0);
                } else {
                    var dt = (DateTime.Parse("2021-" + item.StartTime)- new DateTime(2000, 1, 1)).TotalSeconds;
                    bw.Write((long)dt);
                }
                if (string.IsNullOrEmpty(item.EndTime)) {
                    bw.Write((long)0);
                } else {
                    var dt = (DateTime.Parse("2021-" + item.EndTime) - new DateTime(2000, 1, 1)).TotalSeconds;
                    bw.Write((long)dt);
                }
            }
            bw.Close();
            fs.Close();
        }
    }
}
