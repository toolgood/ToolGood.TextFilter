using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolGood.DFAs
{
    /// <summary>
    /// 表示字符类的正则表达式, 字符类（单个字符）
    /// </summary>
    public class CharExp : Exp
    {
        /// <summary>
        /// 源文
        /// </summary>
        public string Source;
        public bool IsActionFind;

        internal CharExp(string charClass)
        {
            Source = charClass;
        }


        #region ToString
        public override string ToString()
        {
            return Source;
        }
        protected internal override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.Append(Source);
        }

        #endregion

        #region BuildENfa
        internal override void BuildENfa(ENfa nfa)
        {
            nfa.HeadState = ENfa.NewState();
            nfa.TailState = ENfa.NewState();
            nfa.HeadState.Add(nfa.TailState, Source);
        }
        #endregion

        #region GetSourceChars

        //private List<char> _SourceChars;
        public List<char> GetSourceChars()
        {
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
        private List<char> Reversal(List<char> list)
        {
            bool[] all = new bool[char.MaxValue + 1];
            foreach (var item in list) {
                all[item] = true;
            }
            List<char> newlist = new List<char>();
            for (int i = 0; i <= char.MaxValue; i++) {
                if (all[i] == false) {
                    newlist.Add((char)i);
                }
            }
            return newlist;
        }

        #endregion

        #region GetChars SetTarChars
        public override void GetChars(List<string> list)
        {
            var chs = GetSourceChars();
            foreach (var ch in chs) {
                list.Add(ch.ToString());
            }
            //list.Add(new string(chs.ToArray()));
        }

        public override void GetChars(List<Tuple<string, int>> list, ref int layer)
        {
            var chs = GetSourceChars();
            list.Add(Tuple.Create(new string(chs.ToArray()), layer));
            layer++;
        }
        public override void GetChars(Action<List<char>, int> action, ref int layer)
        {
            if (IsActionFind == false) {
                var chs = GetSourceChars();
                action(chs, layer);
                IsActionFind = true;
            }
            layer++;
        }

        public override void SetFirst(bool[] endKeys, ref int layer)
        {
            if (layer == 1) {
                var chs = GetSourceChars();
                foreach (var ch in chs) {
                    endKeys[ch] = true;
                }
            }
            layer++;
        }
        public override void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer)
        {
            if (layer == 1 && once) {
                var chs = GetSourceChars();
                foreach (var ch in chs) {
                    onlyEndKeys[ch] = true;
                }
            }
            layer++;
        }


        public override int GetCharExpCount()
        {
            return 1;
        }

        #endregion

        public bool IsOneAndInRange(int min, int max)
        {
            var chars = GetSourceChars();
            if (chars.Count != 1) { return false; }

            foreach (var c in chars) {
                if (min >= c && max <= c) {
                    return true;
                }
            }
            return false;
        }

        public override bool HasRepeat()
        {
            return false;
        }

        public override void Reverse()
        {
        }

        public override bool EqualExp(Exp exp)
        {
            if (exp is CharExp charExp) {
                return Source == charExp.Source;
            }
            return false;
        }

        public override bool IsOnlyChars()
        {
            return Source.Length == 1;
        }

        public override bool HasInfinite()
        {
            return false;
        }
        public override void SetActionFindFalse()
        {
            IsActionFind = false;
        }
    }
}

