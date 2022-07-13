using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords;
using ToolGood.MultiWords.Dfas.Exps;
using ToolGood.ReadyGo3;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        private const string _tempMultiwordSrearchPath = "temp/multiwordSrearch.dat";
        private const string _tempMultiwordSrearchPath_txt = "temp/multiwordSrearch.txt";

        public void BuildMultiKeyword(SqlHelper helper, TxtCache txtCache, List<TxtCustomInfo> multiKeywords)
        {
            WriteMultiKeyword(multiKeywords, txtCache);

            List<Exp> exps = new List<Exp>();
            foreach (var multiKeyword in multiKeywords) {
                var exp = BuildMutliExp(multiKeyword, txtCache);
                if (exp != null) {
                    exps.Add(exp);
                }
            }

            var length = txtCache.New_Merge_keyword34_Start;
            var multiWordsSearch = new MultiWordsSearch2();
            multiWordsSearch.BuildDict(exps, length);
            var nodes = multiWordsSearch.BuildMultiWordsTreeNode(exps);
            foreach (var multiKeyword in multiKeywords) {
                var keys = GetSubIds_Once(multiKeyword, txtCache);
                if (keys != null) {
                    multiWordsSearch.SetIntervalWrods(nodes, keys, multiKeyword.IntervalWrods);
                }
            }
            multiWordsSearch.CreateData(nodes);
            multiWordsSearch.Save(_tempMultiwordSrearchPath);

            multiWordsSearch = null;

        }

        private void WriteMultiKeyword(List<TxtCustomInfo> multiKeywords, TxtCache txtCache)
        {
            List<string> list = new List<string>();
            foreach (var item in multiKeywords) {
                var subIds = new List<string>();

                var ids = item.GetSubIds(txtCache);
                foreach (var item2 in ids) {
                    subIds.Add($"{{{string.Join(",", item2)}}}");
                }
                list.Add($"{item.Id + (txtCache.New_Merge_keyword34_Start - txtCache.Merge_keyword34_Start)} {item.TxtCustom.Text} {string.Join(",", subIds)}");
            }
            File.WriteAllLines(_tempMultiwordSrearchPath_txt, list);
        }




        private List<int> GetSubIds_Once(TxtCustomInfo customInfo, TxtCache txtCache)
        {
            List<int> result = new List<int>();

            foreach (var item in customInfo.GetSubIds(txtCache)) {
                if (item.Count == 0) { return null; }
                result.Add(item.First());
            }
            return result;
        }

        private Exp BuildMutliExp(TxtCustomInfo mutliMergeInfo, TxtCache txtCache)
        {
            var ids = mutliMergeInfo.GetSubIds(txtCache);
            Exp exp = null;
            foreach (var id in ids) {
                if (id.Count == 0) { return null; }
                if (exp == null) {
                    exp = new CharExp(id);
                } else {
                    Exp exp2 = new CharExp(id);
                    exp = new ConcatenationExp(exp, exp2);
                }
            }
            exp.LabelIndex = mutliMergeInfo.Id + (txtCache.New_Merge_keyword34_Start - txtCache.Merge_keyword34_Start);
            return exp;
        }


    }
}
