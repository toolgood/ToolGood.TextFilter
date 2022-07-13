using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    public class TxtCache
    {
        public int TextFilterStringId = 1;
        // 过滤字符串 - 过滤字符串Id
        public Dictionary<string, int> TextFilterStringDict = new Dictionary<string, int>();

        public List<TxtKeywordInfo> TxtKeywordInfos = new List<TxtKeywordInfo>();

        public Dictionary<int, List<int>> TxtKeywordInfo_OutIndex;
        //public Dictionary<int, List<int>> TxtKeywordInfo_OutIndex_Min;

        public int Keyword34_Start;
        public int Keyword34_End;
        public int Merge_keyword34_Start;
        public int New_Merge_keyword34_Start;




        public int TryAddTextString(string textFilterString)
        {
            int id;
            if (TextFilterStringDict.TryGetValue(textFilterString, out id)) {
                return id;
            }
            id = TextFilterStringId;
            TextFilterStringDict[textFilterString] = TextFilterStringId++;
            TxtKeywordInfos.Add(null);
            return id;
        }

        public int TryAddTextString(DbTxtCustom txtCustom)
        {
            var id = TryAddTextString(txtCustom.Text);
            TxtKeywordInfo txtKeywordInfo;
            if (id >= TxtKeywordInfos.Count) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id };
                TxtKeywordInfos.Add(txtKeywordInfo);
            } else if (TxtKeywordInfos[id] == null) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id };
                TxtKeywordInfos[id] = txtKeywordInfo;
            } else {
                txtKeywordInfo = TxtKeywordInfos[id];
            }
            txtKeywordInfo.CustomInfos.Add(new TxtKeywordInfo.TxtCustomInfo() {
                Text = txtCustom.Text,
                TxtCustomId = txtCustom.Id,
                RiskLevel = txtCustom.RiskLevel,
                IntervalWrods = txtCustom.IntervalWrods,
                IsRepeatWords = txtCustom.IsRepeatWords,
                MatchType = txtCustom.MatchType,
                TxtCustomTypeId = txtCustom.TxtCustomTypeId,
                IsTime = false,
            });
            return id;
        }

        public int TryAddTextString(DbTxtCustom txtCustom, int index, string txt)
        {
            var id = TryAddTextString(txt);
            TxtKeywordInfo txtKeywordInfo;
            if (id >= TxtKeywordInfos.Count) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id, KeywordLength = (byte)txt.Length };
                TxtKeywordInfos.Add(txtKeywordInfo);
            } else if (TxtKeywordInfos[id] == null) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id, KeywordLength = (byte)txt.Length };
                TxtKeywordInfos[id] = txtKeywordInfo;
            } else {
                txtKeywordInfo = TxtKeywordInfos[id];
            }

            if (index == 0) {
                txtKeywordInfo.CustomInfos.Add(new TxtKeywordInfo.TxtCustomInfo() {
                    Text = txtCustom.Text,
                    TxtCustomId = txtCustom.Id,
                    RiskLevel = txtCustom.RiskLevel,
                    IntervalWrods = txtCustom.IntervalWrods,
                    IsRepeatWords = txtCustom.IsRepeatWords,
                    MatchType = txtCustom.MatchType,
                    TxtCustomTypeId = txtCustom.TxtCustomTypeId,
                    IsTime = false,
                });
            }
            return id;
        }

        // DbTxtContact
        public int TryAddTextString(DbTxtContact txtCustom, int index, string txt)
        {
            var id = TryAddTextString(txt);
            TxtKeywordInfo txtKeywordInfo;
            if (id >= TxtKeywordInfos.Count) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id, KeywordLength = (byte)txt.Length };
                TxtKeywordInfos.Add(txtKeywordInfo);
            } else if (TxtKeywordInfos[id] == null) {
                txtKeywordInfo = new TxtKeywordInfo() { Id = id, KeywordLength = (byte)txt.Length };
                TxtKeywordInfos[id] = txtKeywordInfo;
            } else {
                txtKeywordInfo = TxtKeywordInfos[id];
            }

            //if (index == 0) {
            //    txtKeywordInfo.CustomInfos.Add(new TxtKeywordInfo.TxtCustomInfo() {
            //        Text = txtCustom.Text,
            //        TxtCustomId = txtCustom.Id,
            //        RiskLevel = txtCustom.RiskLevel,
            //        IntervalWrods = txtCustom.IntervalWrods,
            //        IsRepeatWords = txtCustom.IsRepeatWords,
            //        MatchType = txtCustom.MatchType,
            //        TxtCustomTypeId = txtCustom.TxtCustomTypeId,
            //        IsTime = false,
            //    });
            //}
            return id;
        }

    }
}
