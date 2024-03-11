/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.Results;

namespace ToolGood.TextFilter
{
    public class ContactSearch2 : IContactSearch
    {
        private int[] _dict;  
        private IntDictionary2[] _nextIndex;
        private byte[] _intervals; 
        private byte[] _maxNextIntervals; 
        private int[] _resultIndexs;  
        private int[] _typeIndexs;
        private int[] _textIndexs;

        public int[] GetContactDict()
        {
            return _dict;
        }

        public List<ContactResult> FindAll(List<TempWordsResultItem> txt)
        {
            TempMultiWords root = new TempMultiWords();// 根
            List<TempMultiWords> tempResult = new List<TempMultiWords>();
            TempMultiWords newsRoot = new TempMultiWords();// 根

            bool find = false;
            int idx = 0;
            for (int i = 0; i < txt.Count; i++) {
                var item = txt[i];
                var t = _dict[item.SingleIndex];// 映射字
                if (t == 0) { continue; }

                TempMultiWords news = newsRoot;// 初始 默认根

                var temp = root;// 初始 默认根
                while (temp != null) {
                    // 获取 下一个
                    if (temp.Item != null && temp.Item.End >= item.Start) { temp = temp.After; continue; }

                    // 最后节点 反推
                    if (_nextIndex[temp.Ptr].TryGetValue(t, ref idx)) {
                        var interval = _intervals[idx];// 多组字词 的 间隔
                        if (interval == 0 || item.NplIndex - temp.NplIndex <= interval) { 
                            var tmp = Append(temp, idx, idx, item);// 生成
                            if (tmp.ResultIndex != 0 /*&& temp.Item != null*/) {// 有值
                                tempResult.Add(tmp);
                                // 判断 无值
                                if (_nextIndex[tmp.Ptr].HasNoneKey()) { temp = temp.After; continue; }
                            }
                            news.After = tmp;// 下一个节点
                            news = tmp;// 设置下一个节点
                            find = true; // 设置 find 状态
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
            if (tempResult.Count == 0) { return new List<ContactResult>(); }


            List<ContactResult> tempResults = new List<ContactResult>(tempResult.Count);
            #region Build Result

            var max = 0;
            for (int i = 0; i < tempResult.Count; i++) {
                var item = tempResult[i];
                List<TempWordsResultItem> items = new List<TempWordsResultItem>();
                var temp = item;
                // 从右到左
                while (temp.Ptr != 0 /*!= root*/) {
                    if (temp.Item != null) {
                        items.Add(temp.Item);
                    }
                    temp = temp.Parent;
                }
                var type = _typeIndexs[item.ResultIndex];
                var textIndex = items.Count - 1 - _textIndexs[item.ResultIndex]; //联系方式字符串索引  反推
                var contact = items[textIndex]; //获取 联系方式字符串

                tempResults.Add(new ContactResult(type, contact.Start, contact.End));
                if (contact.End > max) { max = contact.End; }
            }
            tempResult = null;
            #endregion
            if (tempResults.Count <= 1) { return tempResults; }

            TextSplit_Contact textSplit = new TextSplit_Contact(max + 1); // 联系方式 分割
            foreach (var item in tempResults) {
                textSplit.AddWords(item);
            }
            textSplit.Calculation();
            var results = textSplit.GetIllegalWords(); // 获取 敏感词
            textSplit = null;

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
            length = br.ReadInt32();
            _intervals = br.ReadBytes(length);

            length = br.ReadInt32();
            _maxNextIntervals = br.ReadBytes(length);

            _resultIndexs = br.ReadIntArray();
            _typeIndexs = br.ReadIntArray();
            _textIndexs = br.ReadIntArray();
        }


    }
}
