/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// MultiWordsSearch4类是一个核心类，用于检索多组敏感词组
    /// </summary>
    public class MultiWordsSearch4 : IMultiWordsSearch
    {
        private int[] _dict;        // 映射表
        private IntDictionary2[] _nextIndex; // 下一个索引集
        private byte[] _intervals;  // 间隔数量集
        private byte[] _maxNextIntervals;  // 下一个最大间隔数量集
        private int[] _resultIndexs;    // 结果索引集


        public List<TempMultiWordsResult> FindAll(List<TempWordsResultItem> txt)
        {
            TempMultiWords root = new TempMultiWords();// 根
            List<TempMultiWords> tempResult = new List<TempMultiWords>(); 
            TempMultiWords newsRoot = new TempMultiWords(); // 根

            bool find = false;
            int idx = 0;
            for (int i = 0; i < txt.Count; i++) {
                var item = txt[i];
                var t = _dict[item.SingleIndex]; // 映射字
                if (t == 0) { continue; }

                TempMultiWords news = newsRoot; // 初始 默认根

                var temp = root; // 初始 默认根
                while (temp != null) {
                    // 获取 下一个
                    if (temp.Item != null && temp.Item.End >= item.Start) { temp = temp.After; continue; } 

                    // 最后节点 反推
                    if (_nextIndex[temp.Ptr].TryGetValue(t, ref idx)) {
                        var interval = _intervals[idx]; // 多组字词 的 间隔
                        if (interval == 0 || item.NplIndex - temp.NplIndex <= interval) {  
                            var tmp = Append(temp, idx, idx, item);  // 生成
                            if (tmp.ResultIndex != 0 /*&& temp.Item != null*/) { // 有值
                                tempResult.Add(tmp);
                                // 判断 无值
                                if (_nextIndex[tmp.Ptr].HasNoneKey()) { temp = temp.After; continue; } 
                            }
                            news.After = tmp; // 下一个节点
                            news = tmp;     // 设置下一个节点
                            find = true; // 设置 find 状态
                        }
                    }
                    temp = temp.After; // 获取 下一个
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
            int lastKeywordsId = -1;// 初始值，防重复，如  ABC + ABCE  与  ABC + CE
            TempMultiWordsResult lastMultiWordsResult = null;
            #region Build Result

            for (int i = 0; i < tempResult.Count; i++) {
                var item = tempResult[i];
                Stack<TempWordsResultItem> stack = new Stack<TempWordsResultItem>();
                var temp = item;
                // 从右到左
                while (temp.Ptr != 0 /*!= root*/) {
                    if (temp.Item != null) {
                        stack.Push(temp.Item);
                    }
                    temp = temp.Parent;
                }

                // 反转集合，集合从左到右
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
