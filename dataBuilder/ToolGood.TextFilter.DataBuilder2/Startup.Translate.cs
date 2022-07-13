using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Core;
using ToolGood.TextFilter.DataBuilder2.Datas;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        private const string _tempTranslatePath = "temp/translate.dat";

        private TranslateSearch BuildTranslate(SqlHelper helper)
        {
            var translates = helper.Select<DbTxtTranslate>();

            List<FenciTempKeywordInfo> keywordInfos = new List<FenciTempKeywordInfo>();
            List<string> translateList = new List<string>() { "" };
            HashSet<string> set = new HashSet<string>();

            for (int i = 0; i < translates.Count; i++) {
                var item = translates[i];
                var sp = item.SrcTxt.Trim().Split('|', StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in sp) {
                    if (s.Length > 2) {
                    }
                    if (set.Contains(s)) {
                    }
                    set.Add(s);

                    keywordInfos.Add(new FenciTempKeywordInfo() {
                        Id = i + 1,
                        Keyword = s,
                    });
                }
                translateList.Add(item.TarTxt);
            }

            var ids = helper.Select<int>("select Id from TxtCustomType where Name = '非正常字符'");
            var customs = helper.Select<DbTxtCustom>("where RiskLevel in (3,4) and TxtCustomTypeId in (" + string.Join(",", ids) + ")");
            HashSet<char> bidi = new HashSet<char>();
            foreach (var item in customs) {
                if (item.Text.Length > 1) { continue; }
                bidi.Add(item.Text[0]);
            }


            TranslateSearch fenci = new TranslateSearch();
            fenci.SetKeywords(keywordInfos, translateList);
            fenci.SetBidi(bidi);
            fenci.Save(_tempTranslatePath);

            return fenci;
        }


    }
}
