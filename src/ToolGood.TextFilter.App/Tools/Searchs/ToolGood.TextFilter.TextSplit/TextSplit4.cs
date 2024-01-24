/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// 文本分割
    /// </summary>
    class TextSplit4
    {
        private readonly int _len;  //  MinWords长度
        private readonly int _end;  //  文本长度，
        private readonly WordsInfo[] MinWords; // 最小长度字符
        private readonly List<WordsInfo>[] NextWords; // 下一个词
        private static HashSet<char> _check = new HashSet<char>() { '\r', '\n', '\t', ' ', '　', '\u00A0' }; // 空白符号、换行符


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
            if (nextWords == null) { //判断当前位置 是否NULL
                nextWords = new List<WordsInfo>();// 初始化 List<WordsInfo>
                NextWords[context.Start] = nextWords;
            } else {
                for (int i = 0; i < nextWords.Count; i++) { // 循环字段
                    var first = nextWords[i];
                    if (first.End == context.End) { // 判断 之前有没有设置
                        if (context.DiyIndex > 0) { // 自定义 
                            first.Context_012 = context;
                        } else if (context.SrcRiskLevel == IllegalWordsSrcRiskLevel.Part) {//风险类型为 多组敏感词部分 
                            first.Context_34 = context;
                        } else if (context.SingleIndex > 0) {// 为单组敏感词 
                            first.Context_012 = context;
                            if (context.SingleIndex > keyword34_startIndex) { // 又是多组敏感词
                                first.Context_34 = context;
                            }
                        } else {
                            first.Count = first.Count + context.Count; // 添加单词权重
                            if (first.Context_012 == null) {
                                first.Context_012 = context;// 设置单组敏感词 
                            } else {
                                first.Context_012.EmotionalColor = context.EmotionalColor;// 设置 情感值
                            }
                        }
                        return;
                    }
                }
            }
            if (context.SrcRiskLevel == IllegalWordsSrcRiskLevel.Part) {//风险类型为 多组敏感词部分 
                nextWords.Add(new WordsInfo(context.Start, context.End, context.Count, null, context));
            } else {
                if (context.SingleIndex > keyword34_startIndex) {// 为多组敏感词
                    nextWords.Add(new WordsInfo(context.Start, context.End, context.Count, context, context));
                } else { // 为单组敏感词
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
            for (int i = 0; i < NextWords.Length; i++) {// 开始循环
                var nexts = NextWords[i]; // 获取当前NextWords
                if (nexts == null) { continue; }  // 为空，跳过
                if (nexts.Count == 1) { continue; } // 为1,跳过

                WordsInfo wordsInfo = null;  // 最短的联系方式
                int key = 0;
                for (int j = nexts.Count - 1; j >= 0; j--) { // 开始循环
                    var next = nexts[j];   // 获取 当前WordsInfo
                    int val;

                    if ((next.Context_012 != null && (val = dict[next.Context_012.SingleIndex]) > 0)
                        || (next.Context_34 != null && (val = dict[next.Context_34.SingleIndex]) > 0)
                        ) {
                        if (wordsInfo == null) { // 初始时
                            wordsInfo = next;     // 设置 最短的联系
                            key = val;   // 设置索引
                        } else if (key != val) {// 索引不同时
                            wordsInfo = next; // 设置 最短的联系
                            key = val;  // 设置索引
                        } else if (next.End < wordsInfo.End) {// 索引 相同时
                            var find = false;
                            for (int k = next.End + 1; k <= wordsInfo.End; k++) {
                                if (_check.Contains(chs[k])) { // 判断有无 空白符号、换行符
                                    find = true;
                                    break;
                                }
                            }
                            if (find) {
                                nexts.Remove(wordsInfo); // 去除 较长的联系
                                wordsInfo = next; // 保留 最短的联系
                            }
                        } else {
                            var find = false;
                            for (int k = next.Start; k <= next.End; k++) {
                                if (_check.Contains(chs[k])) { // 判断有无 空白符号、换行符
                                    find = true;
                                    break;
                                }
                            }
                            if (find) {
                                nexts.Remove(next); // 去除 较长的联系
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算长度，权重
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="_skipBitArray"></param>
        public unsafe void Calculation(in char[] txt, in bool[] _skipBitArray)
        {
            int[] _MinLength = new int[_len];//暂存最短长度
            int[] _MaxCount = new int[_len];// 暂存最大权重 

            fixed (int* MinLength = &_MinLength[0])// 使用fixed 加速
            fixed (int* MaxCount = &_MaxCount[0]) {// 使用fixed 加速
                MinLength[0] = 1; // 初始长度
                for (int i = 0; i <= _end; i++) { // 开始循环
                    var minLength = MinLength[i];// 获取当前最小长度值
                    if (minLength == 0) { continue; }   // 最小长度为0时跳过
                    minLength++;  // 最小长度+1
                    var nextWords = NextWords[i];  // 下一个字符
                    if (nextWords != null) { // 字符不为空
                        var count = MaxCount[i];   // 获取最大权重
                        for (int j = 0; j < nextWords.Count; j++) { //开始循环
                            var next = nextWords[j];  // 下一个字符位置

                            var endCharIndex = next.End + 1; // 结束位置
                            var endMinLength = MinLength[endCharIndex];// 获取结束最小长度
                            var endCount = count + next.Count; // 获取结束位置的权重

                            // endMinLength == 0        为0时，设置权重
                            // endMinLength > minLength 下一个长度大于结束长度，设置权重
                            // (endMinLength == minLength) && (MaxCount[endCharIndex] < endCount) 长度相同时，权重小时，设置权重
                            if ((endMinLength == 0) || (endMinLength > minLength)
                                || ((endMinLength == minLength) && (MaxCount[endCharIndex] < endCount))
                                ) {
                                MinLength[endCharIndex] = minLength;// 设置最小长度
                                MaxCount[endCharIndex] = endCount;  // 设置权重
                                MinWords[endCharIndex] = next;      // 设置最小位置
                            }
                        }
                    }
                    if (i < _end && MinLength[i + 1] == 0) {  // 下一个位置长度为0
                        if (_skipBitArray[txt[i]]) {   // 跳词时，
                            minLength--;       // 还原最小长度
                        }
                        MinLength[i + 1] = minLength;    // 设置最小长度
                        MaxCount[i + 1] = MaxCount[i] + 1; // 设置权重
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
            var end = _end; // 获取最后索引
            while (end != 0) {  // 从右到左
                var words = MinWords[end];  // 获取当前位置
                if (words == null) {   // 为空时
                    end--;  // 索引-1，
                } else {
                    if (words.Context_012 != null) {
                        temp.Push(words.Context_012);// 获取 单组敏感词部分
                    } else if (words.Context_34 != null) {
                        temp.Push(words.Context_34);  // 获取 多组敏感词部分
                    }
                    end = words.Start; // 获取开始伴置，即上一个位置的结束位置
                }
            }
            // 反转集合，集合从左到右
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
            for (int i = 1; i < items.Count; i++) { // 开始循环
                var start = items[i].Start;   // 开始位置
                var end = items[i].End;   // 结束位置
                for (int j = start; j <= end; j++) {   // 开始循环位置
                    var nextWords = NextWords[j];     // 下一个字符
                    if (nextWords == null) { continue; }     // 字符为空 跳过
                    for (int k = 0; k < nextWords.Count; k++) { // 开始循环位置
                        var item = nextWords[k];
                        if (item.Context_34 != null) {  // 多组敏感词不为空
                            item.Context_34.NplIndex = i;  // 设置NplIndex值
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<TempWordsResultItem> GetIllegalWords()
        {
            Stack<TempWordsResultItem> temp = new Stack<TempWordsResultItem>();
            var end = _end;  // 获取最后索引
            while (end != 0) { // 从右到左
                var words = MinWords[end];  // 获取当前位置
                if (words == null) {   // 为空时
                    end--;      // 索引-1，
                } else {
                    var context = words.Context_012; // 首先使用【单组的敏感词】
                    if (context != null) {
                        if (context.IsFenci == false) { // 排除分词
                            temp.Push(context);
                        }
                    } else {
                        if (words.Context_34 != null) { // 其次使用【多组敏感词】内的【单组的敏感词】
                            var start = words.Start;
                            for (int i = end - 1; i >= start; i--) { // 开始循环
                                var nextwords = NextWords[i];
                                if (nextwords == null) { continue; }

                                for (int j = 0; j < nextwords.Count; j++) { // 开始循环
                                    var context2 = nextwords[j].Context_012;  // 使用单组的敏感词
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
            // 反转集合，集合从左到右
            var len = temp.Count;
            List<TempWordsResultItem> result = new List<TempWordsResultItem>(len);
            for (int i = 0; i < len; i++) {
                result.Add(temp.Pop());
            }
            temp = null;
            return result;
            // 为何使用了【单组敏感词】，又使用了【多组敏感词】内的【单组的敏感词】？因为这里的【多组敏感词】是多组敏感词的部分，
            // 有些多组敏感词的部分由多个【单组敏感词】组成，有时多组敏感词的部分，在检测时无效，会掩盖有用的【单组的敏感词】。
        }
        /// <summary>
        /// 获取多组敏感词组
        /// </summary>
        /// <returns></returns>
        public List<TempWordsResultItem> GetIllegalWords2()
        {
            Stack<TempWordsResultItem> temp = new Stack<TempWordsResultItem>();
            var end = _end;// 获取最后索引
            while (end != 0) {// 从右到左
                var words = MinWords[end]; // 获取当前位置
                if (words == null) {// 为空时
                    end--;   // 索引-1，
                } else {
                    var context = words.Context_34; // 首先使用【多组敏感词】
                    if (context != null) {
                        var start = words.Start;
                        for (int i = end - 1; i >= start; i--) { // 开始循环
                            var nextwords = NextWords[i];
                            if (nextwords == null) { continue; }

                            for (int j = 0; j < nextwords.Count; j++) { // 开始循环
                                var context2 = nextwords[j].Context_34; // 使用多组敏感词
                                if (context2 != null && context2.End < end) {
                                    temp.Push(context2);
                                }
                            }
                        }
                    }
                    end = words.Start;
                }
            }
            // 反转集合，集合从左到右
            var len = temp.Count;
            List<TempWordsResultItem> result = new List<TempWordsResultItem>(len);
            for (int i = 0; i < len; i++) {
                result.Add(temp.Pop());
            }
            temp = null;
            return result;
            // 【字符串较长】的【多组敏感词部分】可能覆盖了【字符串较短】的【多组敏感词部分】
        }


        class WordsInfo  
        {
            public int Start;   // 开始位置
            public int End;     // 结束位置
            public int Count;   // 值
            public TempWordsResultItem Context_012; // 单组敏感词
            public TempWordsResultItem Context_34;  // 多组敏感词部分


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
