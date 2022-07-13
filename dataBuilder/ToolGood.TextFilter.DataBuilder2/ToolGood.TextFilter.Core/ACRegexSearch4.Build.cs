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
    partial class ACRegexSearch4
    {
        #region SetKeywords
        public void SetKeywords(List<TxtCustomInfo> infos,ref int minKeywordIndex, Dictionary<string, int> tempIndexDict, Dictionary<int, List<int>> outIndexDict)
        {
            List<Exp> exps = new List<Exp>();
            foreach (var item in infos) {
                ((RootExp)item.CurrExp).LabelIndex = item.Id;
                exps.Add(item.CurrExp);
            }

            BuildDict(exps, out int length);

            var dict = new char[0x10000];
            for (int i = 0; i <= 0xffff; i++) {
                dict[i] = (char)_dict[i];
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

            foreach (var exp in exps) {
                exp.Reverse();
            }
            exps = null;
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

        private bool[] BuildEndKey(List<Exp> exps)
        {
            bool[] endKeys = new bool[char.MaxValue + 1];
            int layer = 1;
            for (int i = 0; i < exps.Count; i++) {
                var exp = exps[i];
                exp.Reverse();
                layer = 1;
                exp.SetFirst(endKeys, ref layer);
            }
            return endKeys;
        }
        private bool[] BuildOnlyEndKey(List<Exp> exps)
        {
            bool[] onlyEndKeys = new bool[char.MaxValue + 1];
            int layer = 1;
            for (int i = 0; i < exps.Count; i++) {
                var exp = exps[i];
                layer = 1;
                exp.SetOnlyEnd(onlyEndKeys, true, ref layer);
            }
            return onlyEndKeys;
        }


        private void BuildDict(List<Exp> exps, out int length)
        {
            Dictionary<int, CharStatistics> tempCount = new Dictionary<int, CharStatistics>();
            int layer = 1;
            int i = 0;
            foreach (var exp in exps) {
                layer = 1;
                exp.GetChars((Item1, Item2) => {
                    var single = Item1.Count == 1;
                    if (Item1.Contains('a')) {

                    }
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

                }, ref layer);
            }
            var endkeys = BuildEndKey(exps);
            foreach (var item in tempCount) {
                item.Value.IsEnd = endkeys[item.Key];
            }
            var onlyEndKeys = BuildOnlyEndKey(exps);
            foreach (var item in tempCount) {
                item.Value.IsOnlyEnd = onlyEndKeys[item.Key];
                //if (item.Value.IsOnlyEnd) {
                //    Console.Write((char)item.Key);
                //}
            }

            BuildDict(tempCount, out length);
            tempCount = null;
        }
        private void BuildDict(Dictionary<int, CharStatistics> tempCount, out int length)
        {
            Dictionary<string, int> indexDict = new Dictionary<string, int>();
            int index = 1;// 0 为没有值， 0xffff为跳词

            var temp = tempCount.Where(q => q.Value.IsEnd == false).Where(q => IsEnglishOrNumber(q.Value.Char) == false)
                               .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                             .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _azNumMinChar = (ushort)index;

            temp = tempCount.Where(q => q.Value.IsEnd == false).Where(q => IsEnglishOrNumber(q.Value.Char) == true)
                                           .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                                         .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _minEndKey = (ushort)index;

            temp = tempCount.Where(q => q.Value.IsEnd == true).Where(q => IsEnglishOrNumber(q.Value.Char) == true)
                               .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                             .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;
            _azNumMaxChar = (ushort)(index - 1);

            temp = tempCount.Where(q => q.Value.IsEnd == true).Where(q => IsEnglishOrNumber(q.Value.Char) == false)
                               //.Where(q => q.Value.IsOnlyEnd == false)
                               .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
                             .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;

            //_onlyEndKey = (ushort)index;
            //temp = tempCount.Where(q => q.Value.IsEnd == true).Where(q => IsEnglishOrNumber(q.Value.Char) == false)
            //        .Where(q => q.Value.IsOnlyEnd == true)
            //       .OrderBy(q => q.Value.MaxLayer).ThenBy(q => q.Value.MinLayer).ThenBy(q => q.Key)
            //     .Select(q => q.Value.ExpIndexs).Distinct().ToList();
            //foreach (var tmp in temp) if (indexDict.ContainsKey(tmp) == false) indexDict[tmp] = index++;


            foreach (var item in tempCount) item.Value.Index = indexDict[item.Value.ExpIndexs];
            _dict = new ushort[char.MaxValue + 1];
            foreach (var item in tempCount.OrderBy(q => q.Key)) _dict[item.Key] = (ushort)item.Value.Index;

            for (int i = 0; i < 127; i++) {
                var ch = (char)i;
                if (IsEnglishOrNumber(ch)) {
                    if (_dict[char.ToLower(ch)] == _dict[char.ToUpper(ch)]) {
                    }else if (_dict[char.ToLower(ch)] == 0) {
                        _dict[char.ToLower(ch)] = _dict[char.ToUpper(ch)];
                    } else if (_dict[char.ToUpper(ch)]==0) {
                        _dict[char.ToUpper(ch)] = _dict[char.ToLower(ch)];
                    } else {

                    }
                }
            }



            _minLayer = new ushort[index];
            _maxLayer = new ushort[index];
            foreach (var item in tempCount) {
                _minLayer[item.Value.Index] = (ushort)item.Value.MinLayer;
                _maxLayer[item.Value.Index] = (ushort)item.Value.MaxLayer;
            }

            length = index;
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
            public bool IsOnlyEnd;

            public override string ToString()
            {
                return $"{Char}|{Index}|{MaxLayer}|{ExpIndexs}";
            }
        }

        #endregion



        #endregion

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
            bw.Write(_azNumMinChar);
            bw.Write(_azNumMaxChar);
            bw.Write(_minLayer);
            bw.Write(_maxLayer);
            bw.Write(_minEndKey);
            //bw.Write(_onlyEndKey);

            bw.Write(_first);
            bw.Write(_nextIndex.Length);
            foreach (var item in _nextIndex) {
                item.Save(bw);
            }
            bw.Write(_end);


            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> ACRegexSearch ");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _azNumMinChar " + _azNumMinChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _azNumMaxChar " + _azNumMaxChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _minLayer Length " + _minLayer.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _maxLayer Length " + _maxLayer.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _minEndKey " + _minEndKey);
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
