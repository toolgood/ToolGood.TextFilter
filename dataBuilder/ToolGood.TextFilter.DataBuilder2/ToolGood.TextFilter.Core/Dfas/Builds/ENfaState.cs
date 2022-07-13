using System.Collections.Generic;
using System.Linq;

namespace ToolGood.DFAs
{
    public class ENfaState
    {
        /// <summary>
        /// 字符类的转移对应的字符类列表。
        /// </summary>
        public List<char> CharClassTransition { get { return GetSourceChars(); } }

        public string Source;
        /// <summary>
        /// ϵ 转移的集合。
        /// </summary>
        public List<ENfaState> EpsilonTransitions;// = new List<ENfaState>();
        /// <summary>
        /// 获取字符类转移的目标状态。
        /// </summary>
        public ENfaState CharClassTarget;

        #region 生成时用的
        /// <summary>
        /// 获取当前状态的索引。
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// 获取或设置当前状态的符号索引。
        /// </summary>
        public int LabelIndex { get; set; }

        #endregion

        /// <summary>
        /// 初始化 <see cref="ENfaState"/> 类的新实例。
        /// </summary>
        /// <param name="nfa">包含状态的 NFA。</param>
        /// <param name="index">状态的索引。</param>
        public ENfaState(int index)
        {
            Index = index;
        }

        /// <summary>
        /// 添加一个到特定状态的转移。
        /// </summary>
        /// <param name="state">要转移到的状态。</param>
        /// <param name="ch">转移的字符。</param>
        public void Add(ENfaState state, string ches)
        {
            Source = ches;
            CharClassTarget = state;
        }


        /// <summary>
        /// 添加一个到特定状态的 ϵ 转移。
        /// </summary>
        /// <param name="state">要转移到的状态。</param>
        public void Add(ENfaState state)
        {
            if (EpsilonTransitions == null) {
                EpsilonTransitions = new List<ENfaState>();
            }
            EpsilonTransitions.Add(state);
        }

        #region GetSourceChars

        private List<char> GetSourceChars()
        {
            if (string.IsNullOrEmpty(Source)) {
                return new List<char>();
            }

            List<char> chars = new List<char>();
            var str = Source;
            if (str[0] == '[') {
                GetChar(str, 1, str.Length - 1, chars);
            } else {
                GetChar(str, 0, str.Length, chars);
            }
            return chars;
        }

        private void GetChar(string str, int start, int end, List<char> result)
        {
            var index = start;
            while (index < end) {
                var ch = str[index++];
                if (ch == '\\') {
                    ch = str[index++];
                    switch (ch) {
                        case 'u': ch = GetEscapedString(str, ref index); break;
                        case 'X':
                        case 'x': ch = GetAsciiString(str, ref index); break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7': ch = GetOctalString(ch, str, ref index); break;
                        case 'b': ch = '\b'; break;
                        case 'f': ch = '\f'; break;
                        case 'n': ch = '\n'; break;
                        case 'r': ch = '\r'; break;
                        case 't': ch = '\t'; break;
                        case 'v': ch = '\v'; break;
                        default: break;
                    }
                }
                result.Add(ch);
            }
        }


        private char GetOctalString(char ch, string str, ref int index)
        {
            int num = ch - '0';
            var c = str[index];
            if (c >= '0' && c <= '7') {
                num = (num << 3) + c - '0';
                c = str[index + 1];
                if (c >= '0' && c <= '7') {
                    num = (num << 3) + c - '0';
                    index = index + 2;
                    return (char)num;
                }
                index = index + 1;
                return (char)num;
            }
            return (char)num;
        }
        private char GetEscapedString(string str, ref int index)
        {
            int tempNum = 0;
            var c = str[index];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 12);
                c = str[index + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += (tempNum << 8);
                    c = str[index + 2];
                    if (TryCharToNumber(c, ref tempNum)) {
                        num += (tempNum << 4);
                        c = str[index + 3];
                        if (TryCharToNumber(c, ref tempNum)) {
                            num += tempNum;
                            index = index + 4;
                            return (char)num;
                        }
                    }
                }
            }
            return 'u';
        }
        private char GetAsciiString(string str, ref int index)
        {
            int tempNum = 0;
            var c = str[index];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 4);
                c = str[index + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += tempNum;
                    index = index + 2;
                    return (char)num;
                }
            }
            return 'x';
        }
        private bool TryCharToNumber(char x, ref int num)
        {
            if ('0' <= x && x <= '9') {
                num = x - '0';
                return true;
            }
            if ('a' <= x && x <= 'f') {
                num = x - 'a' + 10;
                return true;
            }
            if ('A' <= x && x <= 'F') {
                num = x - 'A' + 10;
                return true;
            }
            return false;
        }


        #endregion

        /// <summary>
        /// 返回当前对象的字符串表示形式。
        /// </summary>
        /// <returns>当前对象的字符串表示形式。</returns>
        public override string ToString()
        {
            return string.Concat("State #", Index, " [", LabelIndex, "]");
        }


        public override bool Equals(object obj)
        {
            if (obj is ENfaState state) {
                return state.Index == this.Index;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Index;
        }

    }
}

