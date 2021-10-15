/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ToolGood.TextFilter
{
    public class JsonStream : ReadStreamBase
    {
        private int Pointer;
        private int Index;
        private char lastChar = (char)0;
        private int p = 0;
        private int sIndex = 0;

        public JsonStream(string source) : base(source)
        {
            Parse();
        }
 

        #region Parse
        private void Parse()
        {
            StringBuilder sb = new StringBuilder();
            var starts = new List<int>();
            var ends = new List<int>();

            char ch;
            while (TryGetChar(out ch)) {
                switch (ch) {
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case ',':
                    case ':': AddStopChar(sb, starts, ends); break;
                    case '"':
                    case '\'': ParseString(ch, sb, starts, ends); break;
                    default: AddOneChar(ch, sb, starts, ends); break;
                }
            }
            if (p != 0) {
                sb.Append(lastChar);
                starts.Add(sIndex);
                ends.Add(Index - 1);
            }

            TestingText = sb.ToString().ToCharArray();
            Start = starts.ToArray();
            End = ends.ToArray();
        }
        private void ParseString(char stringChar, StringBuilder sb, List<int> starts, List<int> ends)
        {
            char ch;
            while (TryGetChar(out ch)) {
                if (ch == stringChar) {
                    AddStopChar(sb, starts, ends);
                    return;
                }
                AddOneChar(ch, sb, starts, ends);
            }
        }
        private void AddOneChar(char ch, StringBuilder sb, List<int> starts, List<int> ends)
        {
            //int startIndex = Index;

            if (ch == '\\') {
                if (TryGetChar(out ch)) {
                    switch (ch) {
                        case 'u': ch = GetEscapedString(); break;
                        case 'X':
                        case 'x': ch = GetAsciiString(); break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7': ch = GetOctalString(ch); break;
                        case 'b': ch = '\b'; break;
                        case 'f': ch = '\f'; break;
                        case 'n': ch = '\n'; break;
                        case 'r': ch = '\r'; break;
                        case 't': ch = '\t'; break;
                        case 'v': ch = '\v'; break;
                        default: break;
                    }
                }
            }
            sb.Append(ch);
            starts.Add(sIndex);
            ends.Add(Index - 1);

            //if (p > 0) {
            //    p = ToSimplifiedSearch.AppendChar(ch, lastChar, sb, p);
            //    if (p == -1) {
            //        starts.Add(sIndex);
            //        ends.Add(Index - 1);
            //    } else if (p == 0) {
            //        starts.Add(sIndex);
            //        ends.Add(startIndex - 1);
            //    }
            //}
            //if (p == 0) {
            //    p = ToSimplifiedSearch.AppendChar(ch, sb);
            //    if (p <= 0) {
            //        starts.Add(startIndex);
            //        ends.Add(Index - 1);
            //    } else {
            //        sIndex = startIndex;
            //    }
            //}
            lastChar = ch;
            if (p == -1) { p = 0; }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddStopChar(StringBuilder sb, List<int> starts, List<int> ends)
        {
            p = 0;
            if (lastChar != (char)0) {
                lastChar = (char)0;
                sb.Append((char)0);
                starts.Add(Index);
                ends.Add(Index);
            }
        }
        #endregion

        #region Common
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetChar(out char c)
        {
            if (Index >= Source.Length) {
                c = (char)0;
                return false;
            }
            c = Source[Pointer++];
            Index++;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char GetEscapedString()
        {
            int tempNum = 0;
            var c = Source[Pointer];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 12);
                c = Source[Pointer + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += (tempNum << 8);
                    c = Source[Pointer + 2];
                    if (TryCharToNumber(c, ref tempNum)) {
                        num += (tempNum << 4);
                        c = Source[Pointer + 3];
                        if (TryCharToNumber(c, ref tempNum)) {
                            num += tempNum;
                            Pointer = Pointer + 4;
                            Index = Index + 4;
                            return (char)num;
                        }
                    }
                }
            }
            return 'u';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char GetAsciiString()
        {
            int tempNum = 0;
            var c = Source[Pointer];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 4);
                c = Source[Pointer + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += tempNum;
                    Pointer = Pointer + 2;
                    Index = Index + 2;
                    return (char)num;
                }
            }
            return 'x';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char GetOctalString(char ch)
        {
            int num = ch - '0';
            var c = Source[Pointer];
            if (c >= '0' && c <= '7') {
                num = (num << 3) + c - '0';
                c = Source[Pointer + 1];
                if (c >= '0' && c <= '7') {
                    num = (num << 3) + c - '0';
                    Pointer = Pointer + 2;
                    Index = Index + 2;
                    return (char)num;
                }
                Pointer = Pointer + 1;
                Index = Index + 1;
                return (char)num;
            }
            return (char)num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryCharToNumber(char x, ref int num)
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



    }
}
