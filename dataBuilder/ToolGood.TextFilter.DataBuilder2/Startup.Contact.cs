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

        private const string _tempContactTxtPath = "temp/Contact.txt";
        private const string _tempContactPath = "temp/Contact.dat";

        public void BuildContactKeyword(SqlHelper helper, TxtCache txtCache, List<TxtCustomInfo> contactKeywords)
        {
            WriteContactKeyword(contactKeywords, txtCache);

            Dictionary<string, int> indexDict = new Dictionary<string, int>();
            var typeIndex = new List<int>() { 0 };
            var textIndex = new List<int>() { 0 };
            foreach (var item in contactKeywords) {
                var str = $"{item.TxtContact.TxtContactTypeId}-{item.TxtContact.TextIndex}";
                if (indexDict.TryGetValue(str, out int index)) {
                    item.ContactId = index;
                } else {
                    item.ContactId = typeIndex.Count;
                    indexDict[str] = typeIndex.Count;
                    typeIndex.Add(item.TxtContact.TxtContactTypeId);
                    textIndex.Add(item.TxtContact.TextIndex);
                }
            }

            List<Exp> exps = new List<Exp>();
            foreach (var contactKeyword in contactKeywords) {
                var exp = BuildContactExp(contactKeyword, txtCache);
                exps.Add(exp);
            }

            //var length = multiKeywords.Min(q => q.Id);
            var length = txtCache.New_Merge_keyword34_Start;
            var contactSearch = new ContactSearch();
            contactSearch.BuildDict(exps, length);
            var nodes = contactSearch.BuildMultiWordsTreeNode(exps);
            foreach (var multiKeyword in contactKeywords) {
                var keys = GetSubIds_Once(multiKeyword, txtCache);
                if (keys != null) {
                    contactSearch.SetIntervalWrods(nodes, keys, multiKeyword.IntervalWrods);
                }
            }
            contactSearch.CreateData(nodes);
            contactSearch.SetIndexs(typeIndex, textIndex);
            contactSearch.Save(_tempContactPath);

            contactSearch = null;

        }
        private Exp BuildContactExp(TxtCustomInfo contactKeyword, TxtCache txtCache)
        {
            var ids = contactKeyword.GetSubIds(txtCache);
            Exp exp = null;
            foreach (var id in ids) {
                if (exp == null) {
                    exp = new CharExp(id);
                } else {
                    Exp exp2 = new CharExp(id);
                    exp = new ConcatenationExp(exp, exp2);
                }
            }
            exp.LabelIndex = contactKeyword.ContactId;
            return exp;
        }

        private void WriteContactKeyword(List<TxtCustomInfo> multiKeywords, TxtCache txtCache)
        {
            List<string> list = new List<string>();
            foreach (var item in multiKeywords) {
                var subIds = new List<string>();

                var ids = item.GetSubIds(txtCache);
                foreach (var item2 in ids) {
                    subIds.Add($"{{{string.Join(",", item2)}}}");
                }
                list.Add($"{item.Id + (txtCache.New_Merge_keyword34_Start - txtCache.Merge_keyword34_Start)} {item.TxtContact.Text} {string.Join(",", subIds)}");
            }
            File.WriteAllLines(_tempContactTxtPath, list);
        }








    }
}
