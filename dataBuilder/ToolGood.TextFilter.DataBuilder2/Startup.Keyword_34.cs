using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolGood.DFAs;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Core;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        static Regex tagRegex = new Regex(@"\{([\u3400-\u9fffa-zA-Z][\u3400-\u9fff0-9a-zA-Z\-_]*)\}", RegexOptions.Compiled);
        static Regex tagRegexOnce = new Regex(@"\{([\u3400-\u9fffa-zA-Z])\}", RegexOptions.Compiled);
        static Regex tagRegexMore = new Regex(@"\{([\u3400-\u9fffa-zA-Z][\u3400-\u9fff0-9a-zA-Z\-_]+)\}", RegexOptions.Compiled);
        static Regex tagRegexMore_Full = new Regex(@"^\{([\u3400-\u9fffa-zA-Z][\u3400-\u9fff0-9a-zA-Z\-_]+)\}$", RegexOptions.Compiled);


        const string _acRegexSearchPath = @"temp/acRegexSearch.dat";
        const string _acRegexSearchDictPath = @"temp/acRegexSearchDict.dat";
        const string _acRegexSearchPath_txt = @"temp/acRegexSearch.txt";

        const string _bigACTextFilterSearchPath = @"temp/bigACTextFilterSearch.dat";
        const string _bigACTextFilterSearchDictPath = @"temp/bigACTextFilterSearchDict.dat";

        const string _bigAcRegexSearchPath = @"temp/bigAcRegexSearch.dat";
        const string _bigAcRegexSearchDictPath = @"temp/bigAcRegexSearchDict.dat";


        public (List<TxtCustomInfo>, List<TxtCustomInfo>) BuildKeyword_34(SqlHelper helper, TxtCache txtCache, TxtCommonCache txtCommonCache)
        {
            txtCache.TextFilterStringDict.Clear();
            var minKeywordIndex = txtCache.TextFilterStringId;
            txtCache.Keyword34_Start = txtCache.TextFilterStringId;
            var ids = helper.Select<int>("select Id from TxtCustomType where IsFrozen=0 ");
            var customs = helper.Select<DbTxtCustom>("where RiskLevel in (3,4) and TxtCustomTypeId in (" + string.Join(",", ids) + ")");

            List<TxtCustomInfo> tempCustomInfos = new List<TxtCustomInfo>();
            List<TxtCustomInfo> mergeCustomInfos = new List<TxtCustomInfo>();
            List<TxtCustomInfo> contactInfos = new List<TxtCustomInfo>();
            Dictionary<string, TxtCustomInfo> tempDict = new Dictionary<string, TxtCustomInfo>();
            List<TxtCustomInfo> customInfos_34 = new List<TxtCustomInfo>();

            // 添加 单独敏感词
            AddToSingleCustomInfo(txtCache, txtCommonCache, customs, tempCustomInfos, tempDict);
            foreach (var item in tempCustomInfos) { customInfos_34.Add(item); }
            // 添加 012 敏感词
            AddToMutil_012(helper, txtCache, txtCommonCache, tempCustomInfos, mergeCustomInfos, tempDict);
            // 添加 多组敏感词
            AddToMutilCustomInfo(txtCache, txtCommonCache, customs, tempCustomInfos, mergeCustomInfos, tempDict);
            // 添加 联系敏感词
            AddToContactInfo(helper, txtCache, txtCommonCache, tempCustomInfos, contactInfos, tempDict);

            txtCache.Keyword34_End = txtCache.TextFilterStringId - 1;
            txtCache.Merge_keyword34_Start = txtCache.TextFilterStringId;
            foreach (var custom in mergeCustomInfos) {
                var id = txtCache.TryAddTextString(custom.TxtCustom);
                custom.Id = id;
            }
            mergeCustomInfos = mergeCustomInfos.OrderBy(q => q.Id).ToList();

            customs = null;
            tempDict.Clear();
            tempDict = null;

            var dict = BuildTxtExtend(helper);
            Dictionary<string, Exp> temp = new Dictionary<string, Exp>();

            Dictionary<int, TxtCustomInfo> set = new Dictionary<int, TxtCustomInfo>();
            foreach (var item in tempCustomInfos) {
                set[item.Id] = item;
            }
            tempCustomInfos = set.Select(q => q.Value).ToList();
            set = null;


            var skips = GetSkip_SplitKeywords(helper);

            var singleChars = new HashSet<string>();
            foreach (var customInfo1 in tempCustomInfos) {
                var txt = customInfo1.Text;
                var ch = txt[0];
                if (txt.Length == 1 && ch >= 0x3400 && ch <= 0x9fff) {
                    singleChars.Add(txt);
                }
            }

            foreach (var customInfo1 in tempCustomInfos) {
                var txt = customInfo1.Text;
                txt = tagRegex.Replace(txt, new MatchEvaluator((m) => {
                    return txtCommonCache.GetCommonString(m.Value);
                }));
                var exp = ExpHelper.BuildExp(txt, customInfo1.Id);
                exp = (RootExp)ExpHelper.SimplifyExp(exp);

                if (txt.Length > 1 || (txt.Length == 1 && txt[0] < 128)) {
                    var charExps = ExpHelper.FindExp(exp);
                    Dictionary<Exp, Exp> replaceChars = new Dictionary<Exp, Exp>();
                    foreach (var charExp in charExps) {
                        var exp2 = ExpHelper.BuildReplaceExp(charExp, dict, temp, singleChars);
                        if (exp2 != null) { replaceChars[charExp] = exp2; }
                    }
                    exp = (RootExp)ExpHelper.ReplaceExpSelf(exp, replaceChars, skips);
                    replaceChars = null;
                }

                if (customInfo1.IsRepeatWords) {
                    exp = (RootExp)ExpHelper.BuildRepeatWords(exp);
                }
                customInfo1.CurrExp = exp;
            }
            temp = null;

            BuildSearch(txtCache, minKeywordIndex, tempCustomInfos, customInfos_34);


            WriteKeyword_34_Txt(tempCustomInfos, txtCache);
            tempCustomInfos = null;

            return (mergeCustomInfos, contactInfos);
        }

        private void BuildSearch(TxtCache txtCache, int minKeywordIndex, List<TxtCustomInfo> tempCustomInfos, List<TxtCustomInfo> customInfos_34)
        {
            Dictionary<string, int> tempIndexDict = new Dictionary<string, int>();
            Dictionary<int, List<int>> outIndexDict = new Dictionary<int, List<int>>();

            var idx = minKeywordIndex;
            #region 小文本  ACRegexSearch
            var acRegexSearch = new ACRegexSearch4();
            acRegexSearch.SetKeywords(tempCustomInfos, ref idx, tempIndexDict, outIndexDict);
            acRegexSearch.Save(_acRegexSearchPath);
            acRegexSearch.SaveDict(_acRegexSearchDictPath);
            acRegexSearch = null;
            GC.Collect();
            #endregion

            #region 大文本
            if (BigFile) {

                List<TxtCustomInfo> tempCustomInfos_noInfinite = new List<TxtCustomInfo>();
                List<TxtCustomInfo> tempCustomInfos_hasInfinite = new List<TxtCustomInfo>();
                foreach (var item in tempCustomInfos) {
                    item.CurrExp.SetActionFindFalse();
                    if (item.CurrExp.HasInfinite()) {
                        tempCustomInfos_hasInfinite.Add(item);
                    } else {
                        tempCustomInfos_noInfinite.Add(item);
                    }
                }
                // 无限 Exp
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>>  Big 无限 Exp ACRegexSearch4");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");

                var acRegexSearch2 = new ACRegexSearch4();
                acRegexSearch2.SetKeywords(tempCustomInfos_hasInfinite, ref idx, tempIndexDict, outIndexDict);
                acRegexSearch2.Save(_bigAcRegexSearchPath);
                acRegexSearch2.SaveDict(_bigAcRegexSearchDictPath);
                acRegexSearch2 = null;
                foreach (var item in tempCustomInfos_hasInfinite) { item.CurrExp = null; }
                tempCustomInfos_hasInfinite = null;
                GC.Collect();

                // 有限 Exp
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>>  Big 有限 Exp ACTextFilterSearch");
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> -------------------------------------------------------");
                var acTextFilterSearch = new ACTextFilterSearch();
                acTextFilterSearch.SetKeywords(tempCustomInfos_noInfinite, ref idx, tempIndexDict, outIndexDict);
                acTextFilterSearch.Save(_bigACTextFilterSearchPath);
                acTextFilterSearch.SaveDict(_bigACTextFilterSearchDictPath);
                acTextFilterSearch = null;
                foreach (var item in tempCustomInfos_noInfinite) { item.CurrExp = null; }
                tempCustomInfos_noInfinite = null;
            }

            #endregion

            tempIndexDict = null;

            var max = outIndexDict.Values.Max(q => q.Max(q => q));
            txtCache.TxtKeywordInfo_OutIndex = outIndexDict;
            txtCache.New_Merge_keyword34_Start = max + 1;

            GC.Collect();

        }

        private HashSet<string> GetSkip_SplitKeywords(SqlHelper helper)
        {
            var txtExtends = helper.Select<DbTxtExtend>("where IsDelete=0 and TxtExtendTypeId=(select Id from TxtExtendType where Name='左右拆分字')");
            HashSet<string> set = new HashSet<string>();
            foreach (var extend in txtExtends) {
                var sp = extend.TarTxt.Split('|', StringSplitOptions.RemoveEmptyEntries);
                foreach (var s in sp) {
                    set.Add(s);
                }
            }
            return set;
        }

        #region AddToMutil_012 AddToMutilCustomInfo AddToSingleCustomInfo AddToContactInfo
        private void AddToMutil_012(SqlHelper helper, TxtCache txtCache, TxtCommonCache txtCommonCache,
            List<TxtCustomInfo> tempCustomInfos, List<TxtCustomInfo> mergeCustomInfos, Dictionary<string, TxtCustomInfo> tempDict)
        {
            var ids = helper.Select<int>("select Id from TxtCustomType where Name Not like '%单字%'");

            for (int j = 0; j <= 2; j++) {
                var customs = helper.Select<DbTxtCustom>("where RiskLevel =@0 and TxtCustomTypeId in (" + string.Join(",", ids) + ")", j);
                foreach (var custom in customs) {
                    if (custom.Text.Contains("||") == false) { continue; }
                    List<string> sp = new List<string>();
                    List<int> outIntervalWrods = new List<int>();
                    if (custom.IntervalWrods == 0) {
                        custom.IntervalWrods = 20;
                    }
                    SplitAddIntervalWrods(custom.Text, custom.IntervalWrods, sp, outIntervalWrods);

                    List<List<int>> list = new List<List<int>>();

                    for (int i = 0; i < sp.Count; i++) {
                        var s = sp[i];
                        List<int> ids2 = new List<int>();
                        if (tagAllRegex.IsMatch(s)) {
                            var txts = txtCommonCache.GetTxtCommon(s);
                            foreach (var t in txts) {
                                TxtCustomInfo info;
                                if (tempDict.TryGetValue(t, out info) == false) {
                                    var id = txtCache.TryAddTextString(custom, i + 1, t);
                                    info = new TxtCustomInfo() {
                                        Text = t,
                                        Id = id,
                                    };
                                    tempCustomInfos.Add(info);
                                }
                                ids2.Add(info.Id);
                            }
                        } else {
                            TxtCustomInfo info;
                            if (tempDict.TryGetValue(s, out info) == false) {
                                var id = txtCache.TryAddTextString(custom, i + 1, s);
                                info = new TxtCustomInfo() {
                                    Text = s,
                                    Id = id,
                                };
                                tempCustomInfos.Add(info);
                            }
                            ids2.Add(info.Id);
                        }
                        list.Add(ids2);
                    }
                    TxtCustomInfo customInfo = new TxtCustomInfo() {
                        TxtCustom = custom,
                        SubIds = list,
                        IntervalWrods = outIntervalWrods,
                    };
                    mergeCustomInfos.Add(customInfo);
                    sp = null;
                }
            }
        }

        private void AddToMutilCustomInfo(TxtCache txtCache, TxtCommonCache txtCommonCache, List<DbTxtCustom> customs, List<TxtCustomInfo> tempCustomInfos,
            List<TxtCustomInfo> mergeCustomInfos, Dictionary<string, TxtCustomInfo> tempDict)
        {
            foreach (var custom in customs) {
                var find = false;
                if (custom.Text.Contains("||")) { find = true; }
                if (find == false && tagRegexMore.IsMatch(custom.Text)) {
                    if (tagRegexMore_Full.IsMatch(custom.Text) == false) {
                        find = true;
                    }
                }
                if (find == false) { continue; }

                if (custom.Text.Contains("[123456789][0123456789]")) {
                }
                if (custom.Text.Contains("{电话}")) {
                }
                if (custom.Text.Contains("{幼幼}bb")) {
                }

                List<string> sp = new List<string>();
                List<int> outIntervalWrods = new List<int>();
                if (custom.IntervalWrods == 0) {
                    custom.IntervalWrods = 10;
                }
                SplitAddIntervalWrods(custom.Text, custom.IntervalWrods, sp, outIntervalWrods);

                List<List<int>> list = new List<List<int>>();

                for (int i = 0; i < sp.Count; i++) {
                    var s = sp[i];
                    List<int> ids2 = new List<int>();
                    if (tagAllRegex.IsMatch(s)) {
                        var txts = txtCommonCache.GetTxtCommon(s);
                        foreach (var t in txts) {
                            TxtCustomInfo info;
                            if (tempDict.TryGetValue(t, out info) == false) {
                                var id = txtCache.TryAddTextString(custom, i + 1, t);
                                info = new TxtCustomInfo() {
                                    Text = t,
                                    Id = id,
                                };
                                tempCustomInfos.Add(info);
                            }
                            ids2.Add(info.Id);
                        }
                    } else {
                        TxtCustomInfo info;
                        if (tempDict.TryGetValue(s, out info) == false) {
                            var id = txtCache.TryAddTextString(custom, i + 1, s);
                            info = new TxtCustomInfo() {
                                Text = s,
                                Id = id,
                            };
                            tempCustomInfos.Add(info);
                        }
                        ids2.Add(info.Id);
                    }
                    list.Add(ids2);
                }
                if (list.Any(q => q.Count == 0)) {
                    continue;
                }

                TxtCustomInfo customInfo = new TxtCustomInfo() {
                    TxtCustom = custom,
                    SubIds = list,
                    IntervalWrods = outIntervalWrods,
                };
                mergeCustomInfos.Add(customInfo);
                sp = null;
                //txt = null;
            }
        }

        private void AddToSingleCustomInfo(TxtCache txtCache, TxtCommonCache txtCommonCache, List<DbTxtCustom> customs, List<TxtCustomInfo> tempCustomInfos, Dictionary<string, TxtCustomInfo> tempDict)
        {
            foreach (var custom in customs) {
                if (custom.Text.Contains("||")) { continue; }
                if (tagRegexMore.IsMatch(custom.Text)) {
                    if (tagRegexMore_Full.IsMatch(custom.Text) == false) {
                        continue;
                    }
                }
                if (custom.Text.Contains("[123456789][0123456789]")) {
                }
                if (custom.Text.Contains("{电话}")) {
                }
                if (custom.Text == "{你妈}") {

                }
                if (tagAllRegex.IsMatch(custom.Text)) {
                    var txts = txtCommonCache.GetTxtCommon(custom.Text);
                    foreach (var t in txts) {
                        if (tempDict.ContainsKey(t) == false) {
                            var id = txtCache.TryAddTextString(custom, 0, t);
                            TxtCustomInfo info = new TxtCustomInfo() {
                                Text = custom.Text,
                                Id = id,
                            };
                            tempCustomInfos.Add(info);
                            tempDict[t] = info;
                        }
                    }
                } else {
                    if (tempDict.ContainsKey(custom.Text) == false) {
                        var id = txtCache.TryAddTextString(custom, 0, custom.Text);
                        TxtCustomInfo info = new TxtCustomInfo() {
                            Text = custom.Text,
                            Id = id,
                            IsRepeatWords = custom.IsRepeatWords == 1,
                        };
                        tempCustomInfos.Add(info);
                        tempDict[custom.Text] = info;
                    }
                }
            }
        }

        private void AddToContactInfo(SqlHelper helper, TxtCache txtCache, TxtCommonCache txtCommonCache, List<TxtCustomInfo> tempCustomInfos,
            List<TxtCustomInfo> contactInfos, Dictionary<string, TxtCustomInfo> tempDict)
        {
            var contacts = helper.Select<DbTxtContact>("where IsDelete=0");

            foreach (var custom in contacts) {
                var find = false;
                if (custom.Text.Contains("||")) { find = true; }
                if (find == false && tagRegexMore.IsMatch(custom.Text)) {
                    if (tagRegexMore_Full.IsMatch(custom.Text) == false) {
                        find = true;
                    }
                }
                //if (find == false) { continue; }

                if (custom.Text.Contains("[123456789][0123456789]")) {
                }
                if (custom.Text.Contains("https")) {
                }

                List<string> sp = new List<string>();
                List<int> outIntervalWrods = new List<int>();
                SplitAddIntervalWrods(custom.Text, 2, sp, outIntervalWrods);

                //var txt = tagRegexMore.Replace(custom.Text, "||{$1}||");
                //var sp = txt.Split("||", StringSplitOptions.RemoveEmptyEntries);
                List<List<int>> list = new List<List<int>>();

                for (int i = 0; i < sp.Count; i++) {
                    var s = sp[i];
                    List<int> ids2 = new List<int>();
                    if (tagAllRegex.IsMatch(s)) {
                        var txts = txtCommonCache.GetTxtCommon(s);
                        foreach (var t in txts) {
                            TxtCustomInfo info;
                            if (tempDict.TryGetValue(t, out info) == false) {
                                var id = txtCache.TryAddTextString(custom, i + 1, t);
                                info = new TxtCustomInfo() {
                                    Text = t,
                                    Id = id,
                                };
                                tempCustomInfos.Add(info);
                            }
                            ids2.Add(info.Id);
                        }
                    } else {
                        TxtCustomInfo info;
                        if (tempDict.TryGetValue(s, out info) == false) {
                            var id = txtCache.TryAddTextString(custom, i + 1, s);
                            info = new TxtCustomInfo() {
                                Text = s,
                                Id = id,
                            };
                            tempCustomInfos.Add(info);
                        }
                        ids2.Add(info.Id);
                    }
                    list.Add(ids2);
                }
                TxtCustomInfo customInfo = new TxtCustomInfo() {
                    TxtContact = custom,
                    SubIds = list,
                    IntervalWrods = outIntervalWrods,
                };
                contactInfos.Add(customInfo);
                sp = null;
            }
        }
        private void SplitAddIntervalWrods(string str, int intervalWrods, List<string> keywors, List<int> outIntervalWrods)
        {
            var sp = str.Split("||");
            for (int i = 0; i < sp.Length; i++) {
                if (i > 0) {
                    outIntervalWrods.Add(intervalWrods);
                }
                var txt = sp[i];
                txt = tagRegexMore.Replace(txt, "||{$1}||");
                var sp2 = txt.Split("||", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < sp2.Length; j++) {
                    var s = sp2[j];
                    if (j > 0) {
                        outIntervalWrods.Add(1);
                    }
                    keywors.Add(s);
                }
            }
        }

        #endregion



        private void WriteKeyword_34_Txt(List<TxtCustomInfo> txtCustoms, TxtCache txtCache)
        {
            var dict = txtCache.TxtKeywordInfo_OutIndex;
            List<string> list = new List<string>();
            foreach (var item in txtCustoms) {
                if (item.Text=="bb") {

                }

                //if (dict.ContainsKey(item.Id)) {
                var ids = dict[item.Id];
                list.Add($"{string.Join(",", ids) } {item.Text} ");
                //} else {
                //    list.Add($"{item.Id} {item.Text} ");
                //}
            }
            File.WriteAllLines(_acRegexSearchPath_txt, list);
        }


        private Dictionary<string, List<string>> BuildTxtExtend(SqlHelper helper)
        {
            var extends = helper.Select<DbTxtExtend>("where IsDelete=0 and TxtExtendTypeId in (1,2,3,4)");
            return BuildTxtExtend(extends);
        }

        private Dictionary<string, List<string>> BuildTxtExtend(List<DbTxtExtend> extends)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (var extend in extends) {
                var srcs = extend.SrcTxt.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var tars = extend.TarTxt.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var src in srcs) {
                    List<string> list;
                    if (dict.TryGetValue(src, out list) == false) {
                        list = new List<string>();
                        dict[src] = list;
                    }
                    foreach (var tar in tars) {
                        if (list.Contains(tar) == false) {
                            list.Add(tar);
                        }
                    }
                }
            }
            foreach (var extend in extends) {
                var srcs = extend.SrcTxt.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var tars = extend.TarTxt.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var src in srcs) {
                    List<string> list;
                    if (dict.TryGetValue(src, out list) == false) {
                        list = new List<string>();
                        dict[src] = list;
                    }
                    foreach (var tar in tars) {
                        if (list.Contains(tar) == false) {
                            list.Add(tar);
                        }
                    }
                }
            }
            return dict;
        }


    }
}
