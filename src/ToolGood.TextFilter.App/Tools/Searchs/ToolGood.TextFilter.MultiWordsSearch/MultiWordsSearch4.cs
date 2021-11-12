/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    public class MultiWordsSearch4 : IMultiWordsSearch
    {
        private int[] _dict;   
        private IntDictionary2[] _nextIndex;
        private byte[] _intervals; 
        private byte[] _maxNextIntervals; 
        private int[] _resultIndexs;  


        public List<TempMultiWordsResult> FindAll(List<TempWordsResultItem> txt)
        {
            TempMultiWords root = new TempMultiWords();
            List<TempMultiWords> tempResult = new List<TempMultiWords>();
            TempMultiWords newsRoot = new TempMultiWords();

            bool find = false;
            int idx = 0;
            for (int i = 0; i < txt.Count; i++) {
                var item = txt[i];
                var t = _dict[item.SingleIndex];
                if (t == 0) { continue; }

                TempMultiWords news = newsRoot;

                var temp = root;
                while (temp != null) {
                    if (temp.Item != null && temp.Item.End >= item.Start) { temp = temp.After; continue; } 

                    if (_nextIndex[temp.Ptr].TryGetValue(t, ref idx)) {
                        var interval = _intervals[idx];
                        if (interval == 0 || item.NplIndex - temp.NplIndex <= interval) { 
                            var tmp = Append(temp, idx, idx, item);
                            if (tmp.ResultIndex != 0 /*&& temp.Item != null*/) {
                                tempResult.Add(tmp);
                                if (_nextIndex[tmp.Ptr].HasNoneKey()) { temp = temp.After; continue; }
                            }
                            news.After = tmp;
                            news = tmp;
                            find = true;
                        }
                    }
                    temp = temp.After;
                }

                if (find) {
                    var parent = root;
                    temp = root.After;
                    while (temp != null) {
                        if (item.NplIndex >= temp.MaxNextIndex) {
                            parent.After = temp.After;
                        } else {
                            parent = temp;
                        }
                        temp = parent.After;
                    }

                    parent.After = newsRoot.After;
                    newsRoot.After = null;
                    find = false;
                }

            }

            newsRoot = null;
            root.ClearAll();

            List<TempMultiWordsResult> results = new List<TempMultiWordsResult>(tempResult.Count);
            int lastKeywordsId = -1;
            TempMultiWordsResult lastMultiWordsResult = null;
            #region Build Result

            for (int i = 0; i < tempResult.Count; i++) {
                var item = tempResult[i];
                Stack<TempWordsResultItem> stack = new Stack<TempWordsResultItem>();
                var temp = item;
                while (temp.Ptr != 0 /*!= root*/) {
                    if (temp.Item != null) {
                        stack.Push(temp.Item);
                    }
                    temp = temp.Parent;
                }

                var len = stack.Count;

                TempWordsResultItem[] items = new TempWordsResultItem[len];
                for (int j = 0; j < len; j++) { items[j] = stack.Pop(); }

                if (item.ResultIndex == lastKeywordsId) {
                    // 因为 item.ResultIndex 必大于0 ，当 lastKeywordsId >-1 时， lastMultiWordsResult必被赋值
                    if (lastMultiWordsResult.ContainsRange(items)) { continue; }
                }

                TempMultiWordsResult illegalWords = new TempMultiWordsResult(item.ResultIndex, items);
                results.Add(illegalWords);
                lastKeywordsId = item.ResultIndex;
                lastMultiWordsResult = illegalWords;
            }
            tempResult = null;
            #endregion
            return results;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TempMultiWords Append(in TempMultiWords parent, in int ptr, in int idx, in TempWordsResultItem item)
        {
            TempMultiWords temp = new TempMultiWords();
            temp.Ptr = ptr;
            temp.NplIndex = item.NplIndex;
            temp.Parent = parent;
            temp.Item = item;
            temp.ResultIndex = _resultIndexs[idx];
            temp.MaxNextIndex = item.NplIndex + _maxNextIntervals[idx];
            return temp;
        }




        public void Load(BinaryReader br)
        {
            _dict = br.ReadIntArray();
            var length = br.ReadInt32();
            _nextIndex = new IntDictionary2[length];
            for (int i = 0; i < length; i++) {
                _nextIndex[i] = IntDictionary2.Load(br);
            }
            _resultIndexs = br.ReadIntArray();
            length = br.ReadInt32();
            _intervals = br.ReadBytes(length);

            length = br.ReadInt32();
            _maxNextIntervals = br.ReadBytes(length);

        }

    }

}
