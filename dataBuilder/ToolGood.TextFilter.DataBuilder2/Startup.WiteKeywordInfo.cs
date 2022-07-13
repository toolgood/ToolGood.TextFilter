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
        private const string _tempKeywordPath = "temp/keywordInfo.dat";
        public void BuildKeywordInfoFile2(SqlHelper helper, TxtCache txtCache)
        {
            var fs = File.Create(_tempKeywordPath);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(txtCache.TxtKeywordInfos.Count - 1);
            for (int i = 1; i < txtCache.TxtKeywordInfos.Count; i++) {
                var txtKeywordInfo = txtCache.TxtKeywordInfos[i];
                var f = txtKeywordInfo.ToOut();
                bw.Write(f.Id);
                //bw.Write(f.TxtCustomId);
                bw.Write((ushort)f.TxtCustomTypeId);
                bw.Write(GetKeyword(txtKeywordInfo));
                if (f.RiskLevel == 4) {
                    bw.Write((byte)3);
                } else {
                    bw.Write((byte)f.RiskLevel);
                }
                bw.Write((byte)f.MatchType);
                bw.Write((byte)f.KeywordLength);
            }
            bw.Close();
            fs.Close();
        }
        public void BuildKeywordInfoFile(SqlHelper helper, TxtCache txtCache)
        {
            var fs = File.Create(_tempKeywordPath);
            BinaryWriter bw = new BinaryWriter(fs);

            var txtKeywordInfos = GetNewTxtKeywordInfo(txtCache);
            bw.Write(txtKeywordInfos.Length - 1);
            for (int i = 1; i < txtKeywordInfos.Length; i++) {
                var txtKeywordInfo = txtKeywordInfos[i];
                var f = txtKeywordInfo.ToOut();
                if (f.Id== 84457) {

                }

                bw.Write(f.Id);
                //bw.Write(f.TxtCustomId);
                bw.Write((ushort)f.TxtCustomTypeId);
                //bw.Write(GetKeyword(txtKeywordInfo));
                if (f.RiskLevel == 4) {
                    bw.Write((byte)3);
                } else {
                    bw.Write((byte)f.RiskLevel);
                }
                bw.Write((byte)f.MatchType);
                bw.Write((byte)f.KeywordLength);
            }
            bw.Close();
            fs.Close();

        }


        private TxtKeywordInfo[] GetNewTxtKeywordInfo(TxtCache txtCache)
        {
            var length = txtCache.TxtKeywordInfos.Count + (txtCache.New_Merge_keyword34_Start - txtCache.Merge_keyword34_Start);
            TxtKeywordInfo[] result = new TxtKeywordInfo[length];
            var dict = txtCache.TxtKeywordInfo_OutIndex;

            for (int i = 1; i < txtCache.TxtKeywordInfos.Count; i++) {
                var txtKeywordInfo = txtCache.TxtKeywordInfos[i];
                if (txtKeywordInfo.Id < txtCache.Keyword34_Start) {
                    result[txtKeywordInfo.Id] = txtKeywordInfo;
                } else if (txtKeywordInfo.Id >= txtCache.Merge_keyword34_Start) {
                    result[txtKeywordInfo.Id + (txtCache.New_Merge_keyword34_Start - txtCache.Merge_keyword34_Start)] = txtKeywordInfo;
                } else {
                    var ids = dict[txtKeywordInfo.Id];
                    foreach (var id in ids) {
                        if (result[id] == null) {
                            result[id] = txtKeywordInfo.Copy(id);
                        } else {
                            Merge(result[id], txtKeywordInfo);
                        }
                    }
                }
            }
            return result;
        }
        public void Merge(TxtKeywordInfo src, TxtKeywordInfo tar)
        {
            foreach (var item in tar.CustomInfos) {
                src.CustomInfos.Add(item);
            }
        }



        private string GetKeyword(TxtKeywordInfo keywordInfo)
        {
            string txt = null;
            foreach (var customInfo in keywordInfo.CustomInfos) {
                txt = customInfo.Text;
                txt = txt.Replace("||", " ");
                txt = tagRegex.Replace(txt, "$1");
                return txt;
            }
            return "";
        }


        //public int Id { get; set; }
        //public int TypeId { get; set; }
        //public byte RiskLevel { get; set; }
        //public byte MatchType { get; set; }
        //public byte KeywordLength { get; set; }
    }
}
