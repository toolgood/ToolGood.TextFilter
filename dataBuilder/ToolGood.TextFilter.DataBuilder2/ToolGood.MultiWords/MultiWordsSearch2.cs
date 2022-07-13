using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter.Core;

namespace ToolGood.MultiWords
{
    partial class MultiWordsSearch2
    {
        private int[] _dict;  // 0 跳过 
        private int[] _first;
        private IntDictionary2[] _nextIndex;
        private byte[] _intervals; // 当前间隔
        private byte[] _maxNextIntervals; // 下一个最大间隔
        private int[] _resultIndexs;  // 结果

        class TempMultiWords
        {
            public int Ptr;    // 检索位置
            public int Index;  // 字节位置
            public int MaxNextIndex; // 最大下一个位置

            public int ResultIndex; //
            public TempMultiWords Parent;// 上一个节点

            public TempMultiWords Before;// 上一个
            public TempMultiWords After;// 下一个

            public void Remove()
            {
                if (Before == null) { return; }
                if (After == null) {
                    Before.After = null;
                } else {
                    Before.After = After;
                }
                Before = null;
                After = null;
            }
            public void ClearAll()
            {
                var temp = After;
                while (temp != null) {
                    var after = temp.After;
                    temp.Before = null;
                    temp.After = null;
                    temp = after;
                }
            }

        }


        public List<MultiIllegalWordsResult> Find(List<int> txt, List<int> indexs)
        {
            TempMultiWords root = new TempMultiWords();
            TempMultiWords last = root;
            List<TempMultiWords> tempResult = new List<TempMultiWords>();

            for (int i = 0; i < txt.Count; i++) {
                var c = txt[i];
                var t = _dict[c];
                if (t == 0) { continue; }

                TempMultiWords newsRoot = null;
                TempMultiWords news = null;

                var temp = root;
                while (temp != null) {
                    var ptr = temp.Ptr;
                    int idx = 0;
                    if (_nextIndex[ptr].TryGetValue(t,ref idx)) {
                        var interval = _intervals[idx];
                        if (interval == 0) { // 0为无限
                            news = Append(newsRoot, news, temp, idx, i, _maxNextIntervals[idx]);
                            news.ResultIndex = _resultIndexs[idx];
                            if (news.ResultIndex > 0) {
                                tempResult.Add(news);
                            }
                        } else {
                            if (indexs[idx] - temp.Index > interval) {
                                news = Append(newsRoot, news, temp, idx, i, _maxNextIntervals[idx]);
                                news.ResultIndex = _resultIndexs[idx];
                                if (news.ResultIndex > 0) {
                                    tempResult.Add(news);
                                }
                            }
                        }
                    }
                    temp = temp.After;
                }

                #region 排除无效节点
                temp = root.After;
                var index2 = indexs[i];
                while (temp != null) {
                    if (index2 >= temp.MaxNextIndex) {
                        var temp2 = temp.After;
                        temp.Remove();
                        temp = temp2;
                    } else {
                        temp = temp.After;
                    }
                }
                #endregion

                #region 将新节点 添加到最后
                if (newsRoot != null) {
                    last.After = newsRoot;
                    last = news;
                }
                #endregion
            }
            last = null;
            root.ClearAll();

            List<MultiIllegalWordsResult> results = new List<MultiIllegalWordsResult>();
            foreach (var item in tempResult) {
                MultiIllegalWordsResult illegalWords = new MultiIllegalWordsResult();
                illegalWords.ResultIndex = item.ResultIndex;
                Stack<int> stack = new Stack<int>();
                var temp = item;
                while (temp != root) {
                    if (temp.Index > 0) {
                        stack.Push(temp.Index);
                    }
                    temp = temp.Parent;
                }
                while (stack.TryPop(out int idx)) {
                    illegalWords.KeywordIndexs.Add(idx);
                }
                results.Add(illegalWords);
            }
            tempResult = null;
            return results;
        }

        private TempMultiWords Append(TempMultiWords newsRoot, TempMultiWords news, TempMultiWords parent, int ptr, int index, int maxNextInterval)
        {
            TempMultiWords temp = new TempMultiWords();
            temp.Ptr = ptr;
            temp.Index = index;
            temp.MaxNextIndex = index + maxNextInterval;
            temp.Parent = parent;

            if (newsRoot == null) {
                newsRoot = temp;
                return temp;
            }
            news.After = temp;
            return temp;
        }






    }
}
