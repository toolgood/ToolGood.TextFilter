using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.DataBuilder2;

namespace ToolGood.TextFilter
{
    public partial class SkipwordsSearch
    {
        #region 设置关键字
        public void SetKeywords(ICollection<string> _keywords, CharDictionary dictionary, List<char> skips)
        {
            var root = new TrieNode3();
            List<List<TrieNode3>> allNodeLayers = new List<List<TrieNode3>>();

            #region 第一次建 TrieNode 
            {
                allNodeLayers.Add(new List<TrieNode3>() { root });
                foreach (var keyword in _keywords) {
                    var p = keyword;
                    var nd = root;
                    for (int j = 0; j < p.Length; j++) {
                        nd = AddNode(allNodeLayers, nd, dictionary.GetTranslate(p[j]));
                    }
                    nd.SetResults(keyword.Length);
                }
            }
            #endregion

            #region 建立 AC自动机 
            List<TrieNode3> allNode = new List<TrieNode3>();
            foreach (var trieNodes in allNodeLayers) {
                foreach (var nd in trieNodes) {
                    allNode.Add(nd);
                }
            }
            allNodeLayers = null;

            for (int i = 1; i < allNode.Count; i++) {
                var nd = allNode[i];
                nd.Index = i;
                TrieNode3 r = nd.Parent.Failure;
                char c = nd.Char;
                while (r != null && !r.m_values.ContainsKey(c)) r = r.Failure;
                if (r == null)
                    nd.Failure = root;
                else {
                    nd.Failure = r.m_values[c];
                    foreach (var result in nd.Failure.Results)
                        nd.SetResults(result);
                }
            }
            root.Failure = root;
            #endregion

            var length = BuildDict(_keywords, dictionary, skips);
            foreach (var node in allNode) {
                node.SetNewTrieNode(_dict);
                node.SetSimplifyFailure(root);
            }

            build(allNode, length);
        }

        private TrieNode3 AddNode(List<List<TrieNode3>> allNodeLayers, TrieNode3 nd, char ch)
        {
            var newNd = nd.Add(ch);
            if (newNd.Layer == 0) {
                newNd.Layer = nd.Layer + 1;
                if (allNodeLayers.Count <= newNd.Layer) {
                    allNodeLayers.Add(new List<TrieNode3>());
                }
                allNodeLayers[newNd.Layer].Add(newNd);
            }
            return newNd;
        }

        private void build(List<TrieNode3> nodes, Int32 length)
        {
            int[] has = new int[0x00FFFFFF];
            bool[] seats = new bool[0x00FFFFFF];
            bool[] seats2 = new bool[0x00FFFFFF];
            Int32 start = 0;
            Int32 oneStart = 10;

            //for (int i = 0; i < nodes.Count; i++) {
            //    var node = nodes[i];
            //    node.Rank(ref oneStart, ref start, seats, seats2, has);
            //}

            //nodes[0].Rank(ref start, seats, seats2, has);
            nodes[0].Rank2(ref start, seats, has);
            oneStart = start;
            has[0] = -1;

            var nds = nodes.Skip(1).OrderBy(q => q.Maxflag()).ThenByDescending(q => q.GetDensity())/*.OrderByDescending(q => q.GetCompaction())*/.ToList();

            foreach (var node in nds)
                if (node.IsRankOne() == false)
                    //node.Rank(ref start, seats, seats2, has);
                    node.Rank2(ref start, seats, has);
            seats2 = null;
            foreach (var node in nds)
                if (node.IsRankOne())
                    node.RankOne(ref oneStart, seats, has);

            length = nodes.Max(q => q.Next) + length;// + 1;

            _key = new ushort[length];
            _next = new Int32[length];
            _checkLen = new Int32[length];
            _failure = new int[length];

            nodes[0].Ptr = 0;
            has[0] = 0;
            for (Int32 i = 0; i < length; i++) {
                var item = nodes[has[i]];
                if (item == null) continue;
                if (item.Ptr == -1) {
                    item.Ptr = i;
                }
            }

            for (Int32 i = 0; i < length; i++) {
                var item = nodes[has[i]];
                if (item == null) continue;

                _key[i] = (ushort)item.NewChar;
                _next[i] = item.Next;
                _failure[i] = item.SimplifyFailure.Ptr;

                if (item.End) {
                    _checkLen[i] = item.Results.First();
                }
            }
            for (Int32 i = 0; i < length; i++) {
                if (_next[i] == 0 && _failure[i] > 0) {
                    _next[i] = _next[_failure[i]];
                }
            }


        }

        private int BuildDict(ICollection<string> keywords, CharDictionary dictionary, List<char> skips)
        {
            int[] dict = new int[char.MaxValue + 1];
            foreach (var item in keywords) {
                if (item.Length > 1) {
                    var key0 = dictionary.GetTranslate(item[0]);
                    if (dict[key0] == 0) {
                        dict[key0] = 1;
                    }
                    for (int i = 1; i < item.Length; i++) {
                        var keyi = dictionary.GetTranslate(item[i]);
                        dict[keyi] = -1;
                    }
                } else {
                    var key0 = dictionary.GetTranslate(item[0]);
                    dict[key0] = -1;
                }
            }

            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i <= char.MaxValue; i++) {
                if (dict[i] > 0) {
                    list.Add(i);
                } else if (dict[i] == -1) {
                    list2.Add(i);
                }
            }

            _dict = new ushort[char.MaxValue + 1];
            foreach (var item in skips) {
                _dict[(int)item] = 0xffff;
            }
            int index = 1;
            foreach (var item in list) {
                _dict[item] = (ushort)index++;
            }
            _firstMaxChar = (ushort)(index - 1);
            foreach (var item in list2) {
                _dict[item] = (ushort)index++;
            }

            var len = dictionary.GetLength();
            for (int i = 0; i < len; i++) {
                var key = dictionary.GetKey(i);
                var val = dictionary.GetValue(i);
                if (_dict[key] == 0) {
                    _dict[key] = _dict[val];
                } else if (_dict[val] == 0) {
                    _dict[val] = _dict[key];
                } else if (_dict[val] == _dict[key]) {
                } else {
                }
            }

            //var len = dictionary.GetLength();
            //for (int i = 0; i < len; i++) {
            //    var key = dictionary.GetKey(i);
            //    var val = dictionary.GetValue(i);
            //    _dict[key] = _dict[val];
            //}

            return index;
        }



        #endregion

        #region Save
        internal virtual void Save(BinaryWriter bw)
        {
            bw.Write(_firstMaxChar);
            bw.Write(_dict);
            bw.Write(_key);
            bw.Write(_next);
            bw.Write(_checkLen);
            bw.Write(_failure);

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> SkipwordsSearch ");
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _firstMaxChar " + _firstMaxChar);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _dict Length " + _dict.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _key Length " + _key.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _next Length " + _next.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _checkLen Length " + _checkLen.Length);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff") + " >>> _failure Length " + _failure.Length);

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

        #endregion


    }
}
