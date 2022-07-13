using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;
using ToolGood.MultiWords.Dfas.Exps;
using ToolGood.TextFilter;
using ToolGood.TextFilter.Core;

namespace ToolGood.MultiWords
{
    public partial class MultiWordsSearch2
    {
        #region BuildDict
        public void BuildDict(List<Exp> exps, int length)
        {
            List<List<int>> chars = new List<List<int>>();
            foreach (var exp in exps) {
                exp.GetSrcChars(chars);
            }
            Dictionary<int, CharStatistics> tempCount = new Dictionary<int, CharStatistics>();
            for (int i = 0; i < chars.Count; i++) {
                var item = chars[i];
                var single = item.Count == 1;
                foreach (var ch in item) {
                    CharStatistics cs;
                    if (tempCount.TryGetValue(ch, out cs) == false) {
                        cs = new CharStatistics();
                        cs.Char = ch;
                        cs.ExpIndexs = "";
                        tempCount[ch] = cs;
                    }
                    if (single) {
                        cs.IsSingle = true;
                        cs.ExpIndexs = "|||" + ch;
                    } else if (cs.IsSingle) {
                    } else if (i >= 0xFFFF) {
                        var t = i + 1;
                        cs.ExpIndexs = cs.ExpIndexs + ((char)0xFFFF) + ((char)(t >> 16)) + ((char)(t & 0xFFFF));
                    } else {
                        cs.ExpIndexs = cs.ExpIndexs + (char)i;
                    }
                }
            }

            Dictionary<string, int> indexDict = new Dictionary<string, int>();
            int index = 1;// 0 为没有值
            foreach (var item in tempCount) {
                if (indexDict.ContainsKey(item.Value.ExpIndexs) == false) {
                    indexDict[item.Value.ExpIndexs] = index++;
                }
            }
            foreach (var item in tempCount) item.Value.Index = indexDict[item.Value.ExpIndexs];

            _dict = new int[length];
            foreach (var item in tempCount.OrderBy(q => q.Key)) _dict[item.Key] = (ushort)item.Value.Index;
        }

        class CharStatistics
        {
            public int Char;
            public string ExpIndexs;
            public int Index;
            public bool IsSingle;
        }
        #endregion

        #region BuildMultiWordsTreeNode
        public List<MultiWordsTreeNode> BuildMultiWordsTreeNode(List<Exp> exps)
        {
            //foreach (var exp in exps) {
            //    exp.Reverse();
            //}

            var nfa = Dfa.BuildENfa(exps);
            exps = null;
            var dfa = Dfa.BuildDfa(nfa, _dict);
            nfa = null;
            var nodes2 = BuildNodes(dfa.Items);
            dfa = null;

            return nodes2;
        }
        private List<MultiWordsTreeNode> BuildNodes(List<DfaState> dfaStates)
        {
            List<MultiWordsTreeNode> list = new List<MultiWordsTreeNode>();
            for (int i = 0; i < dfaStates.Count; i++) {
                list.Add(new MultiWordsTreeNode());
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
            return list;
        }

        #endregion

        #region SetIntervalWrods
        public void SetIntervalWrods(List<MultiWordsTreeNode> nodes, List<int> keys, List<int> intervalWrods)
        {
            var temp = nodes[0];

            for (int i = 0; i < keys.Count; i++) {
                var key = _dict[keys[i]];

                var node = temp.Nodes[key];
                if (i >= 1) {
                    var interval = intervalWrods[i - 1];
                    if (node.IntervalWrods.Contains(interval) == false) {
                        node.IntervalWrods.Add(interval);
                    }
                }
                temp = node;
            }
        }
        #endregion


        #region CreateData
        public void CreateData(List<MultiWordsTreeNode> nodes)
        {
            List<int> resultIndexs = new List<int>();
            List<byte> intervals = new List<byte>();
            List<byte> maxNextIntervals = new List<byte>();
            var nextIndexs = new List<Dictionary<int, int>>();

            for (int i = 0; i < nodes.Count; i++) {
                var nd = nodes[i];

                var dict = new Dictionary<int, int>();
                foreach (var item in nd.Nodes) {
                    var key = item.Key;
                    var value = item.Value;
                    dict[key] = value.Index;
                }
                //for (int j = 0; j < nd.Nodes.Count; j++) {
                //    var key = nd.Nodes[j].Key;
                //    var value = nd.Nodes[j];
                //    dict[key] = value.Index;
                //}
                nextIndexs.Add(dict);

                if (nd.Results.Count == 0) {
                    resultIndexs.Add(0);
                } else if (nd.Results.Count > 1) {
                    resultIndexs.Add(nd.Results[0]);
                } else {
                    resultIndexs.Add(nd.Results[0]);
                }

                if (nd.IntervalWrods.Count == 0) {
                    intervals.Add((byte)0);
                } else {
                    intervals.Add((byte)nd.IntervalWrods.Max());
                }

                var max = 0;
                foreach (var item in nd.Nodes) {
                    if (item.Value.IntervalWrods.Count > 0) {
                        var temp = item.Value.IntervalWrods.Max();
                        if (temp > max) {
                            max = temp;
                        }
                    }
                }
                maxNextIntervals.Add((byte)max);
            }

            _resultIndexs = resultIndexs.ToArray();
            _intervals = intervals.ToArray();
            _maxNextIntervals = maxNextIntervals.ToArray();


            //var first = new int[Char.MaxValue + 1];
            //for (int j = 0; j < nodes[0].Nodes.Count; j++) {
            //    var key = nodes[0].Nodes[j].Key;
            //    var value = nodes[0].Nodes[j];
            //    first[key] = value.Index;
            //}
            //_first = first;
            _nextIndex = new IntDictionary2[nextIndexs.Count];
            for (int i = 0; i < nextIndexs.Count; i++) {
                IntDictionary2 dictionary = new IntDictionary2(nextIndexs[i]);
                _nextIndex[i] = dictionary;
            }

        }
        #endregion


        #region Save
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


        internal void Save(BinaryWriter bw)
        {
            bw.Write(_dict);
            bw.Write(_nextIndex.Length);
            foreach (var item in _nextIndex) {
                item.Save(bw);
            }
            bw.Write(_resultIndexs);

            bw.Write(_intervals.Length);
            bw.Write(_intervals);

            bw.Write(_maxNextIntervals.Length);
            bw.Write(_maxNextIntervals);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> MultiWordsSearch ");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _dict Length " + _dict.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _nextIndex Length " + _nextIndex.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _indexs Length " + _resultIndexs.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _intervals Length " + _intervals.Length);
        }

        #endregion


    }
}
