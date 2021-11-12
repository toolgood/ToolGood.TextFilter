/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.Results;

namespace ToolGood.TextFilter
{
    public class TextSplit_Contact
    {
        private readonly int _len;
        private readonly int _end;
        private readonly ContactResult[] MinWords;
        private readonly List<ContactResult>[] NextWords;

        public TextSplit_Contact(int textLength)
        {
            _end = textLength;
            _len = textLength + 1;
            MinWords = new ContactResult[_len];
            NextWords = new List<ContactResult>[_len];
        }

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

        public unsafe void Calculation()
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
                            var endCount = count + 1;

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
                        MinLength[i + 1] = minLength;
                        MaxCount[i + 1] = MaxCount[i] + 1;
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
            var end = _end;
            while (end != 0) {
                var words = MinWords[end];
                if (words.IsSet == false) {
                    end--;
                } else {
                    temp.Push(words);
                    end = words.Start;
                }
            }
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
