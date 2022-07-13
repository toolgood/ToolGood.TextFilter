using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.Core
{
    public partial class ACRegexSearch4
    {
        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private ushort[] _dict;// 0 没有该值 1) 跳词, 2+)转成对应key
        private ushort[] _minLayer;
        private ushort[] _maxLayer;
        private ushort _minEndKey;

        private int[] _first;
        private IntDictionary[] _nextIndex;
        private int[] _end;
        //private int[] _resultIndex;


        public unsafe void FindAll3(string text, List<IllegalWordsResultItem> result)
        {
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pminLayer = &_minLayer[0])
            fixed (ushort* _pmaxLayer = &_maxLayer[0])
            //fixed (int* _presultIndex = &_resultIndex[0]) 
                {
                var min = 0;

                for (int i = 0; i < text.Length; i++) {
                    var t = _pdict[text[i]];
                    if (t == 0) { min = 0; continue; }
                    if (t == 1) { continue; }//跳词

                    var n = *(_pminLayer + t);
                    if (n == 1) {
                        min = 1;
                        if (t >= _minEndKey && CheckNextChar(text, t, i, _pdict)) {
                            Find(text, t, result, _pdict,/* _presultIndex,*/ i);
                        }

                    } else if (min != 0) {
                        if (*(_pmaxLayer + t) <= min) { min = 0; continue; }// 当前最小layer为min时，下一个字符最小layer应为min+1
                        min = n;
                        if (t >= _minEndKey && CheckNextChar(text, t, i, _pdict)) {
                            Find(text, t, result, _pdict,/* _presultIndex,*/ i);
                        }
                    }
                }
            }
        }

        private unsafe void Find(string text, ushort tc, List<IllegalWordsResultItem> result, ushort* _pdict, /*int* _presultIndex,*/ int end)
        {
            //var next = _first[tc];
            //for (int j = _end[next]; j < _end[next + 1]; j++) {
            //    var index = _resultIndex[j];
            //    if (CheckPreChar(text, tc, end, _pdict)) {
            //        result.Add(new IllegalWordsResultItem(end, end, index));
            //    }
            //}

            //for (int j = end - 1; j >= 0; j--) {
            //    tc = _pdict[text[j]];
            //    if (tc == 0) { return; }
            //    if (tc == 1) { continue; }

            //    if (_nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
            //    for (int k = _end[next]; k < _end[next + 1]; k++) {
            //        var index = _resultIndex[j];
            //        if (CheckPreChar(text, tc, k, _pdict)) {
            //            result.Add(new IllegalWordsResultItem(k, end, index));
            //        }
            //    }
            //}
        }

        private unsafe bool CheckNextChar(string text, ushort t, int i, ushort* _pdict)
        {
            //非数字与英文
            if (t < _azNumMinChar || t > _azNumMaxChar) { return true; }
            if (i == text.Length - 1) { return true; } // 在文本最末尾的概率比 非数字非英文的概率 小，所以放在后面

            var tt = _pdict[text[i + 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        private unsafe bool CheckPreChar(string text, ushort t, int i, ushort* _pdict)
        {
            //非数字与英文
            if (t < _azNumMinChar || t > _azNumMaxChar) { return true; }
            if (i == 0) return true; // 在文本开始位置的概率比 非数字非英文的概率 小，所以放在后面

            var tt = _pdict[text[i - 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

    }
}
