using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Core;
using ToolGood.TextFilter.DataBuilder2.Datas;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        static Regex tagAllRegex = new Regex(@"^\{([\u3400-\u9fffa-zA-Z][\u3400-\u9fff0-9a-zA-Z\-_]*)\}$", RegexOptions.Compiled);
        const string _tempKeyword_012 = "temp/tempKeyword_012.dat";
        const string _tempKeyword_012_txt = "temp/tempKeyword_012.txt";

        public void BuildKeyword_012(SqlHelper helper, TxtCache txtCache, TxtCommonCache txtCommonCache)
        {
            var ids = helper.Select<int>("select Id from TxtCustomType where Name Not like '%单字%'");

            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i <= 2; i++) {
                var customs = helper.Select<DbTxtCustom>("where RiskLevel =@0 and TxtCustomTypeId in (" + string.Join(",", ids) + ")", i);
                foreach (var custom in customs) {
                    if (custom.Text.Contains("||")) { continue; }
                    if (tagAllRegex.IsMatch(custom.Text)) {
                        var txts = txtCommonCache.GetTxtCommon(custom.Text);
                        foreach (var txt in txts) {
                            var id = txtCache.TryAddTextString(custom, 0, txt);
                            if (dict.ContainsKey(txt) == false) {
                                dict[txt] = id;
                            }
                        }
                    } else {
                        var id = txtCache.TryAddTextString(custom, 0, custom.Text);
                        if (dict.ContainsKey(custom.Text) == false) {
                            dict[custom.Text] = id;
                        }
                    }
                }
            }
            List<FenciTempKeywordInfo> FenciKeywordInfos = new List<FenciTempKeywordInfo>();
            foreach (var item in dict) {
                FenciKeywordInfos.Add(new FenciTempKeywordInfo() {
                    Count = 1,
                    Keyword = item.Key,
                    Id = item.Value,
                    NewId = item.Value
                });
            }
            dict = null;




            var translateDict = CreateTranslateDict(helper);
            var skips = GetSkipwords(helper, 0);

            FenciSearch fenciSearch = new FenciSearch();
            fenciSearch.SetKeywords(FenciKeywordInfos, translateDict, skips);
            fenciSearch.Save(_tempKeyword_012);
            fenciSearch = null;

            WriteKeyword_012_Txt(FenciKeywordInfos);

            FenciKeywordInfos = null;
        }

        private void WriteKeyword_012_Txt(List<FenciTempKeywordInfo> FenciKeywordInfos)
        {
            List<string> result = new List<string>();
            foreach (var info in FenciKeywordInfos) {
                result.Add($"{info.Id} {info.Keyword} {info.Count} {info.EmotionalColor}");
            }
            File.WriteAllLines(_tempKeyword_012_txt, result);
        }




    }
}
