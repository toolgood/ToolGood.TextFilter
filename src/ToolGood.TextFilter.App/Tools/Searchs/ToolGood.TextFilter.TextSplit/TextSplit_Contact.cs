/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.Results;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// 联系方式文本分割
    /// </summary>
    public class TextSplit_Contact
    {
        private readonly int _len;  //  MinWords长度
        private readonly int _end;  //  文本长度，
        private readonly ContactResult[] MinWords;// 最小长度字符
        private readonly List<ContactResult>[] NextWords; // 下一个词

        public TextSplit_Contact(int textLength)
        {
            _end = textLength;
            _len = textLength + 1;
            MinWords = new ContactResult[_len];
            NextWords = new List<ContactResult>[_len];
        }
        /// <summary>
        /// 添加字词
        /// </summary>
        /// <param name="context"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddWords(ContactResult context)
        {
            var nextWords = NextWords[context.Start];
            if (nextWords == null) {
                nextWords = new List<ContactResult>();
                NextWords[context.Start] = nextWords;
            }
            nextWords.Add(context);
        }

        /// <summary>
        /// 计算
        /// </summary>
        public unsafe void Calculation()
        {
            int[] _MinLength = new int[_len];//暂存最短长度
            int[] _MaxCount = new int[_len];// 暂存最大权重

            fixed (int* MinLength = &_MinLength[0])// 使用fixed 加速
            fixed (int* MaxCount = &_MaxCount[0]) {// 使用fixed 加速
                MinLength[0] = 1;// 初始长度
                for (int i = 0; i <= _end; i++) {
                    var minLength = MinLength[i];// 开始循环
                    if (minLength == 0) { continue; }// 最小长度为0时跳过
                    minLength++;// 最小长度+1
                    var nextWords = NextWords[i];// 下一个字符
                    if (nextWords != null) { // 字符不为空
                        var count = MaxCount[i];// 获取最大权重
                        for (int j = 0; j < nextWords.Count; j++) {//开始循环
                            var next = nextWords[j];// 下一个字符位置

                            var endCharIndex = next.End + 1;// 结束位置
                            var endMinLength = MinLength[endCharIndex];// 获取结束最小长度
                            var endCount = count + 1;// 获取结束位置的权重

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
                    if (i < _end && MinLength[i + 1] == 0) {    // 下一个位置长度为0
                        MinLength[i + 1] = minLength;           // 设置最小长度
                        MaxCount[i + 1] = MaxCount[i] + 1;      // 设置权重
                    }

                }
            }
            _MinLength = null;
            _MaxCount = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<ContactResult> GetIllegalWords()
        {
            Stack<ContactResult> temp = new Stack<ContactResult>();
            var end = _end;// 获取最后索引
            while (end != 0) {  // 从右到左
                var words = MinWords[end];// 获取当前位置
                if (words.IsSet == false) { // 未设置
                    end--;// 索引-1，
                } else {
                    temp.Push(words);
                    end = words.Start;// 获取开始伴置，即上一个位置的结束位置
                }
            }
            // 反转集合，集合从左到右
            var len = temp.Count;
            List<ContactResult> result = new List<ContactResult>(len);
            for (int i = 0; i < len; i++) {
                result.Add(temp.Pop());
            }
            temp = null;
            return result;
        }


    }
}
