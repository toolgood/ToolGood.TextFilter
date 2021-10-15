/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    class TextSplit4
    {
        private readonly int _len;
        private readonly int _end;
        private readonly WordsInfo[] MinWords;
        private readonly List<WordsInfo>[] NextWords;
        private static HashSet<char> _check = new HashSet<char>() { '\r', '\n', '\t', ' ', '　', '\u00A0' };

 
        public TextSplit4(int textLength)
        {
            _end = textLength;
            _len = textLength + 1;
            MinWords = new WordsInfo[_len];
            NextWords = new List<WordsInfo>[_len];
        }

        /// <summary>
        /// 不会替换 前一种IllegalWordsSearchResult
        /// </summary>
        /// <param name="result"></param>
        /// <param name="count"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddWords(TempWordsResultItem context, int keyword34_startIndex)
        {
            var nextWords = NextWords[context.Start];
            if (nextWords == null) {
                nextWords = new List<WordsInfo>();
                NextWords[context.Start] = nextWords;
            } else {
                for (int i = 0; i < nextWords.Count; i++) {
                    var first = nextWords[i];
                    if (first.End == context.End) {
                        if (context.DiyIndex > 0) {
                            first.Context_012 = context;
                        } else if (context.SrcRiskLevel == IllegalWordsSrcRiskLevel.Part) {
                            first.Context_34 = context;
                        } else if (context.SingleIndex > 0) {
                            first.Context_012 = context;
                            if (context.SingleIndex > keyword34_startIndex) {
                                first.Context_34 = context;
                            }
                        } else {
                            first.Count = first.Count + context.Count;
                            if (first.Context_012 == null) {
                                first.Context_012 = context;
                            } else {
                                first.Context_012.EmotionalColor = context.EmotionalColor;
                            }
                            //if (first.Context_34 != null) {
                            //    first.Context_34.IsFenci = true;
                            //}
                        }
                        return;
                    }
                }
            }
            if (context.SrcRiskLevel == IllegalWordsSrcRiskLevel.Part) {
                nextWords.Add(new WordsInfo(context.Start, context.End, context.Count, null, context));
            } else {
                if (context.SingleIndex > keyword34_startIndex) {
                    nextWords.Add(new WordsInfo(context.Start, context.End, context.Count, context, context));
                } else {
                    nextWords.Add(new WordsInfo(context.Start, context.End, context.Count, context, null));
                }
            }
        }

        /// <summary>
        /// 去除 较长的联系 方式
        /// </summary>
        /// <param name="dict"></param>
        public void RemoveMaxLengthContact(in int[] dict, in char[] chs)
        {
            for (int i = 0; i < NextWords.Length; i++) {
                var nexts = NextWords[i];
                if (nexts == null) { continue; }
                if (nexts.Count == 1) { continue; }

                WordsInfo wordsInfo = null;
                int key = 0;
                for (int j = nexts.Count - 1; j >= 0; j--) {
                    var next = nexts[j];
                    int val;

                    if ((next.Context_012 != null && (val = dict[next.Context_012.SingleIndex]) > 0)
                        || (next.Context_34 != null && (val = dict[next.Context_34.SingleIndex]) > 0)
                        ) {
                        if (wordsInfo == null) {
                            wordsInfo = next;
                            key = val;
                        } else if (key != val) {
                            wordsInfo = next;
                            key = val;
                        } else if (next.End < wordsInfo.End) {
                            var find = false;
                            for (int k = next.End + 1; k <= wordsInfo.End; k++) {
                                if (_check.Contains(chs[k])) {
                                    find = true;
                                    break;
                                }
                            }
                            if (find) {
                                nexts.Remove(wordsInfo);
                                wordsInfo = next;
                            }
                        } else {
                            var find = false;
                            for (int k = next.Start; k <= next.End; k++) {
                                if (_check.Contains(chs[k])) {
                                    find = true;
                                    break;
                                }
                            }
                            if (find) {
                                nexts.Remove(next);
                            }
                        }
                    }
                }
            }
        }

        public unsafe void Calculation(in char[] txt, in bool[] _skipBitArray)
        {
            int[] _MinLength = new int[_len];
            int[] _MaxCount = new int[_len];

            fixed (int* MinLength = &_MinLength[0])
            fixed (int* MaxCount = &_MaxCount[0]) {
                MinLength[0] = 1;
                for (int i = 0; i <= _end; i++) {
                    var minLength = MinLength[i];
                    if (minLength == 0) { continue; }
                    minLength++;
                    var nextWords = NextWords[i];
                    if (nextWords != null) {
                        var count = MaxCount[i];
                        for (int j = 0; j < nextWords.Count; j++) {
                            var next = nextWords[j];

                            var endCharIndex = next.End + 1;
                            var endMinLength = MinLength[endCharIndex];
                            var endCount = count + next.Count;

                            if ((endMinLength == 0) || (endMinLength > minLength)
                                || ((endMinLength == minLength) && (MaxCount[endCharIndex] < endCount))
                                ) {
                                MinLength[endCharIndex] = minLength;
                                MaxCount[endCharIndex] = endCount;
                                MinWords[endCharIndex] = next;
                            }
                        }
                    }
                    if (i < _end && MinLength[i + 1] == 0) {
                        if (_skipBitArray[txt[i]]) {
                            minLength--;
                        }
                        MinLength[i + 1] = minLength;
                        MaxCount[i + 1] = MaxCount[i] + 1;
                    }
                }
            }
            _MinLength = null;
            _MaxCount = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<TempWordsResultItem> GetWordsContext()
        {
            Stack<TempWordsResultItem> temp = new Stack<TempWordsResultItem>();
            var end = _end;
            while (end != 0) {
                var words = MinWords[end];
                if (words == null) {
                    end--;
                } else {
                    if (words.Context_012 != null) {
                        temp.Push(words.Context_012);
                    } else if (words.Context_34 != null) {
                        temp.Push(words.Context_34);
                    }
                    end = words.Start;
                }
            }
            List<TempWordsResultItem> result = new List<TempWordsResultItem>(temp.Count);
            TempWordsResultItem item;
            while (temp.TryPop(out item)) {
                result.Add(item);
            }
            temp = null;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetNplIndex(List<TempWordsResultItem> items)
        {
            for (int i = 1; i < items.Count; i++) {
                var start = items[i].Start;
                var end = items[i].End;
                for (int j = start; j <= end; j++) {
                    var nextWords = NextWords[j];
                    if (nextWords == null) { continue; }
                    for (int k = 0; k < nextWords.Count; k++) {
                        var item = nextWords[k];
                        if (item.Context_34 != null) {
                            item.Context_34.NplIndex = i;
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<TempWordsResultItem> GetIllegalWords()
        {
            Stack<TempWordsResultItem> temp = new Stack<TempWordsResultItem>();
            var end = _end;
            while (end != 0) {
                var words = MinWords[end];
                if (words == null) {
                    end--;
                } else {
                    var context = words.Context_012;
                    if (context != null) {
                        if (context.IsFenci == false) {
                            temp.Push(context);
                        }
                    } else {
                        if (words.Context_34 != null) {
                            var start = words.Start;
                            for (int i = end - 1; i >= start; i--) {
                                var nextwords = NextWords[i];
                                if (nextwords == null) { continue; }

                                for (int j = 0; j < nextwords.Count; j++) {
                                    var context2 = nextwords[j].Context_012;
                                    if (context2 != null && context2.SrcRiskLevel != null && context2.End < end) {
                                        temp.Push(context2);
                                    }
                                }
                            }
                        }
                    }
                    end = words.Start;
                }
            }
            var len = temp.Count;
            List<TempWordsResultItem> result = new List<TempWordsResultItem>(len);
            for (int i = 0; i < len; i++) {
                result.Add(temp.Pop());
            }
            temp = null;
            return result;
        }

        public List<TempWordsResultItem> GetIllegalWords2()
        {
            Stack<TempWordsResultItem> temp = new Stack<TempWordsResultItem>();
            var end = _end;
            while (end != 0) {
                var words = MinWords[end];
                if (words == null) {
                    end--;
                } else {
                    var context = words.Context_34;
                    if (context != null) {
                        var start = words.Start;
                        for (int i = end - 1; i >= start; i--) {
                            var nextwords = NextWords[i];
                            if (nextwords == null) { continue; }

                            for (int j = 0; j < nextwords.Count; j++) {
                                var context2 = nextwords[j].Context_34;
                                if (context2 != null && context2.End < end) {
                                    temp.Push(context2);
                                }
                            }
                        }
                    }
                    end = words.Start;
                }
            }
            var len = temp.Count;
            List<TempWordsResultItem> result = new List<TempWordsResultItem>(len);
            for (int i = 0; i < len; i++) {
                result.Add(temp.Pop());
            }
            temp = null;
            return result;
        }


        class WordsInfo  
        {
            public int Start;
            public int End;
            public int Count;
            public TempWordsResultItem Context_012;
            public TempWordsResultItem Context_34;

            public WordsInfo(int start, int end, int count, TempWordsResultItem item_012, TempWordsResultItem item_34)
            {
                Start = start;
                End = end;
                Count = count;
                Context_012 = item_012;
                Context_34 = item_34;
            }

            public override string ToString()
            {
                return $"{Start}-{End}";
            }
        }

    }
}
