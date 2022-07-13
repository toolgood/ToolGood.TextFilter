using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.DFAs;
using ToolGood.TextFilter.DataBuilder2;

namespace ToolGood.TextFilter.Core
{
    partial class ACTextFilterSearch
    {
        #region SetKeywords
        public void SetKeywords(List<TxtCustomInfo> infos,ref int minKeywordIndex, Dictionary<string, int> tempIndexDict, Dictionary<int, List<int>> outIndexDict)
        {
            List<Exp> exps = new List<Exp>();
            //foreach (var item in infos) {
            //    if (item.Text.StartsWith("t")) {
            //        ((RootExp)item.CurrExp).LabelIndex = item.Id;
            //        exps.Add(item.CurrExp);
            //    }
            //}
            //foreach (var item in infos) {
            //    if (item.Text.StartsWith("t")==false) {
            //        ((RootExp)item.CurrExp).LabelIndex = item.Id;
            //        exps.Add(item.CurrExp);
            //    }
            //}

            foreach (var item in infos) {
                item.CurrExp.SetActionFindFalse();
                ((RootExp)item.CurrExp).LabelIndex = item.Id;
                exps.Add(item.CurrExp);
            }
            BuildDict(exps, out int length);

            var dict = new char[0x10000];
            for (int i = 0; i <= 0xffff; i++) {
                dict[i] = (char)_dict[i];
                if (_dict[i]==4004) {

                } 
            }
            // 第一步 定位
            {
                var nfa = Dfa.BuildENfa(exps);
                var dfa = Dfa.BuildDfa2(nfa, dict);
                nfa = null;
                GC.Collect();

                var nodes2 = BuildNodes(dfa.Items);
                dfa.Items.Clear();
                dfa.Items = null;
                dfa = null;
                GC.Collect();

                _maxLength = AcrTrieNode.SetFailure(nodes2);
                var root = nodes2[0];
                foreach (var node in nodes2) {
                    node.SetSimplifyFailure(root);
                }
                BuildArray(nodes2, length);
                nodes2 = null;
                GC.Collect();
            }
            // 第二步 反向定位
            {
                foreach (var exp in exps) {
                    exp.Reverse();
                }
                var nfa = Dfa.BuildENfa(exps);
                var dfa = Dfa.BuildDfa(nfa, dict);
                nfa = null;
                GC.Collect();

                var nodes2 = BuildNodes(dfa.Items);
                dfa.Items.Clear();
                dfa.Items = null;
                dfa = null;
                GC.Collect();

                GetResultList(nodes2,ref minKeywordIndex, tempIndexDict, outIndexDict);
                BuildBackwardArray(nodes2, length);
                nodes2 = null;
            }
        }

        private void GetResultList(List<AcrTrieNode> list,ref int idx, Dictionary<string, int> dict, Dictionary<int, List<int>> result)
        {
            foreach (var item in list) {
                if (item.Results.Count > 0) {
                    item.OldResults = item.Results.ToList();
                    var str = ToString(item.Results);
                    if (dict.TryGetValue(str, out int index)) {
                        item.Results.Clear();
                        item.Results.Add(index);
                    } else {
                        if (idx==6643) {

                        }
                        dict[str] = idx;
                        foreach (var r in item.Results) {
                            List<int> rs;
                            if (result.TryGetValue(r, out rs) == false) {
                                rs = new List<int>();
                                result[r] = rs;
                            }
                            rs.Add(idx);
                        }
                        item.Results.Clear();
                        item.Results.Add(idx);
                        idx++;
                    }
                }
            }
        }
        private string ToString(List<int> result)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in result.OrderBy(q => q)) {
                stringBuilder.Append((char)(item >> 16));
                stringBuilder.Append((char)(item & 0xffff));
            }
            return stringBuilder.ToString();
        }

        private List<AcrTrieNode> BuildNodes(List<DfaState> dfaStates)
        {
            List<AcrTrieNode> list = new List<AcrTrieNode>();
            for (int i = 0; i < dfaStates.Count; i++) {
                list.Add(new AcrTrieNode());
            }
            for (int i = 0; i < dfaStates.Count; i++) {
                var dfa = dfaStates[i];
                var node = list[i];
                node.Index = i;
                node.Results = dfa.LabelIndex.ToList();
                if (node.Results.Count > 0) {
                    node.End = true;
                }

                for (int j = 0; j < dfa.NextStates.Count; j++) {
                    node.Add(dfa.NextChars[j], list[dfa.NextStates[j].Index]);
                }
            }
            foreach (var node in list) {
                node.SetNewChar();
            }
            return list;
        }

        public AcrTrieNode GetTrieNode(List<AcrTrieNode> list, string str)
        {
            var node = list[0];
            foreach (var ch in str) {
                node = node.Nodes[(char)_dict[ch]];
                //var idx = node.NodeChars.IndexOf((char)_dict[ch]);
                //node = node.NodeValues[idx];
            }
            return node;
        }

        #region BuildDict

        private void BuildDict(List<Exp> exps, out int length)
        {
            Dictionary<int, CharStatistics> tempCount = new Dictionary<int, CharStatistics>();
            int layer = 1;
            int i = 0;
            foreach (var exp in exps) {
                layer = 1;
                exp.GetChars((Item1, Item2) => {
                    var single = Item1.Count == 1;
                    foreach (var ch in Item1) {
                        CharStatistics cs;
                        if (tempCount.TryGetValue(ch, out cs) == false) {
                            cs = new CharStatistics();
                            cs.Char = ch;
                            cs.MinLayer = Item2;
                            cs.ExpIndexs = "";
                            tempCount[ch] = cs;
                        }
                        if (cs.IsSingle) {
                        } else if (single) {
                            cs.IsSingle = true;
                            cs.ExpIndexs = "|||" + ch;
                        } else {
                            cs.ExpIndexs += (char)(i & 0xFFFF);
                        }

                        if (cs.MaxLayer < Item2) cs.MaxLayer = Item2;
                        if (cs.MinLayer > Item2) cs.MinLayer = Item2;
                    }
                    i++;
                    #region 简化
                    if (i % 1000 == 0) {
                        Dictionary<string, int> indexCount = new Dictionary<string, int>();
                        foreach (var item in tempCount) {
                            if (item.Value.IsSingle) { continue; }
                            if (indexCount.ContainsKey(item.Value.ExpIndexs)) {
                                indexCount[item.Value.ExpIndexs]++;
                            } else {
                                indexCount[item.Value.ExpIndexs] = 1;
                            }
                        }
                        var index = 0;
                        Dictionary<string, int> indexDict = new Dictionary<string, int>();
                        foreach (var item in indexCount) {
                            if (item.Value > 1) {
                                indexDict[item.Key] = index++;
                            }
                        }
                        indexCount = null;
                        foreach (var item in tempCount) {
                            if (item.Value.IsSingle) { continue; }
                            if (indexDict.TryGetValue(item.Value.ExpIndexs, out int idx)) {
                                item.Value.ExpIndexs = idx + "|||";
                            } else {
                                item.Value.IsSingle = true;
                                item.Value.ExpIndexs = "|||" + item.Key;
                            }
                        }
                        indexDict = null;
                    }
                    #endregion
                }, ref layer);
            }
            var endkeys = BuildEndKey(exps);
            foreach (var item in tempCount) {
                item.Value.IsEnd = endkeys[item.Key];
            }
            BuildDict(tempCount, out length);
            tempCount = null;
        }

        private void BuildDict(Dictionary<int, CharStatistics> tempCount, out int length)
        {
            Dictionary<string, int> indexDict = new Dictionary<string, int>();
            int index = 1;// 0 为没有值， 0xffff为跳词

            var temp = tempCount.Where(q => q.Value.MinLayer == 1 && q.Value.MaxLayer == 1).Where(q => IsEnglishOrNumber(q.Value.Char) == false)
                        .Where(q => q.Value.IsEnd == false)
                        .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                        .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _azNumMinChar = (ushort)index;

            temp = tempCount.Where(q => q.Value.MinLayer == 1 && q.Value.MaxLayer == 1).Where(q => IsEnglishOrNumber(q.Value.Char) == true)
                     .Where(q => q.Value.IsEnd == false)
                     .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                     .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _firstMaxChar = (ushort)(index - 1);

            temp = tempCount.Where(q => IsEnglishOrNumber(q.Value.Char) == true)
                     .OrderBy(q => q.Value.MinLayer).ThenBy(q => q.Value.MaxLayer).ThenBy(q => q.Key)
                     .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _azNumMaxChar = (ushort)(index - 1);

            temp = tempCount
                      .OrderBy(q => q.Value.MinLayer).ThenBy(q => q.Value.MaxLayer).ThenBy(q => q.Key)
                      .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;


            foreach (var item in tempCount) item.Value.Index = indexDict[item.Value.ExpIndexs];
            _dict = new ushort[char.MaxValue + 1];

            foreach (var item in tempCount.OrderBy(q => q.Key)) _dict[item.Key] = (ushort)item.Value.Index;

            for (int i = 0; i < 127; i++) {
                var ch = (char)i;
                if (IsEnglishOrNumber(ch)) {
                    if (_dict[char.ToLower(ch)] == _dict[char.ToUpper(ch)]) {
                    } else if (_dict[char.ToLower(ch)] == 0) {
                        _dict[char.ToLower(ch)] = _dict[char.ToUpper(ch)];
                    } else if (_dict[char.ToUpper(ch)] == 0) {
                        _dict[char.ToUpper(ch)] = _dict[char.ToLower(ch)];
                    } else {

                    }
                }
            }

            length = index;
        }

        private bool[] BuildEndKey(List<Exp> exps)
        {
            bool[] endKeys = new bool[char.MaxValue + 1];
            int layer = 1;
            for (int i = 0; i < exps.Count; i++) {
                var exp = exps[i];
                exp.Reverse();
                layer = 1;
                exp.SetFirst(endKeys, ref layer);
                exp.Reverse();
            }
            return endKeys;
        }

        private bool IsEnglishOrNumber(char c)
        {
            if (c >= '0' && c <= '9') return true;
            if (c >= 'A' && c <= 'Z') return true;
            if (c >= 'a' && c <= 'z') return true;
            return false;
        }

        class CharStatistics
        {
            public char Char;
            public string ExpIndexs;
            public int MaxLayer;
            public int MinLayer;
            public int Index;
            public bool IsEnd;
            public bool IsSingle;

            public override string ToString()
            {
                return $"{Char}|{Index}|{MaxLayer}|{ExpIndexs}";
            }
        }

        #endregion

        #endregion
        private void BuildArray(List<AcrTrieNode> nodes, Int32 length)
        {
            var nextIndexs = new List<Dictionary<ushort, int>>(nodes.Count);
            var failure = new List<int>(nodes.Count);
            var check = new List<bool>(nodes.Count);

            for (int i = 0; i < nodes.Count; i++) {
                var dict = new Dictionary<ushort, int>();
                var node = nodes[i];

                foreach (var item in node.Nodes) {
                    dict[item.Key] = item.Value.Index;
                }
                nextIndexs.Add(dict);
                failure.Add(node.SimplifyFailure.Index);
                check.Add(node.End);
            }
            var first = new int[length];
            foreach (var item in nodes[0].Nodes) {
                first[item.Key] = item.Value.Index;
            }

            var _nextIndex = new IntDictionary[nextIndexs.Count];
            for (int i = 0; i < nextIndexs.Count; i++) {
                IntDictionary dictionary = new IntDictionary(nextIndexs[i]);
                _nextIndex[i] = dictionary;
            }

            _first_first = first;
            _nextIndex_first = _nextIndex;
            _failure = failure.ToArray();
            _check = check.ToArray();
        }



        #region BuildBackwardArray
        private void BuildBackwardArray(List<AcrTrieNode> nodes, Int32 length)
        {
            var nextIndexs = new List<Dictionary<ushort, int>>();
            var end = new List<int>() { };
            for (int i = 0; i < nodes.Count; i++) {
                var dict = new Dictionary<ushort, int>();
                var node = nodes[i];

                if (i > 0) {
                    foreach (var item in node.Nodes) {
                        dict[item.Key] = item.Value.Index;
                    }
                    //for (int j = 0; j < node.NodeChars.Count; j++) {
                    //    var key = node.NodeChars[j];
                    //    var value = node.NodeValues[j];
                    //    dict[key] = value.Index;
                    //}
                }
                if (node.Results.Count > 0) {
                    end.Add(node.Results.First());
                } else {
                    end.Add(0);
                }
                //foreach (var item in node.Results) {
                //    resultIndex.Add(item);
                //}
                //end.Add(resultIndex.Count);
                nextIndexs.Add(dict);
            }
            var first = new int[Char.MaxValue + 1];

            foreach (var item in nodes[0].Nodes) {
                first[item.Key] = item.Value.Index;
            }

            //for (int j = 0; j < nodes[0].NodeChars.Count; j++) {
            //    var key = nodes[0].NodeChars[j];
            //    var value = nodes[0].NodeValues[j];
            //    first[key] = value.Index;
            //}

            _first = first;
            _nextIndex = new IntDictionary[nextIndexs.Count];
            for (int i = 0; i < nextIndexs.Count; i++) {
                IntDictionary dictionary = new IntDictionary(nextIndexs[i]);
                //dictionary.SetDictionary(nextIndexs[i]);
                _nextIndex[i] = dictionary;
            }
            _end = end.ToArray();
        }

        #endregion

        #region Save
        internal virtual void Save(BinaryWriter bw)
        {
            bw.Write(_firstMaxChar);
            bw.Write(_maxLength);
            bw.Write(_first_first);
            bw.Write(_nextIndex_first.Length);
            foreach (var item in _nextIndex_first) {
                item.Save(bw);
            }
            bw.Write(_failure);
            bw.Write(_check);

            bw.Write(_azNumMinChar);
            bw.Write(_azNumMaxChar);
            bw.Write(_first);
            bw.Write(_nextIndex.Length);
            foreach (var item in _nextIndex) {
                item.Save(bw);
            }
            bw.Write(_end);


            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> ACTextFilterSearch ");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _firstMaxChar " + _firstMaxChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _dict Length " + _dict.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _nextIndex_first Length " + _nextIndex_first.Length);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _azNumMinChar " + _azNumMinChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _azNumMaxChar " + _azNumMaxChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _first Length " + _first.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _nextIndex Length " + _nextIndex.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _end Length " + _end.Length);
        }


        public void SaveDict(string filePath)
        {
            var fs = File.Open(filePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(_dict);

            bw.Close();
            fs.Close();
        }


        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            var fs = File.Open(filePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            Save(bw);

            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 保存到Stream
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);
            Save(bw);
            bw.Close();
        }

        #endregion

    }
}
