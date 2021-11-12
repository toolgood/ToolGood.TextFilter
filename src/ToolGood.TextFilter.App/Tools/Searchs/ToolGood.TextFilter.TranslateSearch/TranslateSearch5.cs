/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;

namespace ToolGood.TextFilter
{
    public interface ITranslateSearch
    {
        void Load(BinaryReader br);

        ReadStreamBase Replace(in string text, in bool skipBidi);

        ReadStreamBase Replace(ReadStreamBase src, in bool skipBidi);

    }

    public class TranslateSearch5 : ITranslateSearch
    {

        private ushort _firstMaxChar;
        private ushort[] _dict;
        private ushort[] _bidiDict;

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _check;
        private Int32[] _failure;
        private byte[] _len;
        private string[] _keywords;


        public ReadStreamBase Replace(ReadStreamBase src, in bool skipBidi)
        {
            if (skipBidi) {
                return Replace1(src);
            }
            return Replace2(src);
        }
        public ReadStreamBase Replace(in string text, in bool skipBidi)
        {
            if (skipBidi) {
                return Replace1(text);
            }
            return Replace2(text);
        }

        private unsafe ReadStreamBase Replace1(ReadStreamBase src)
        {
            var text = src.TestingText;
            char[] sb_temp = new char[text.Length];
            int[] starts_temp = new int[text.Length];
            int[] ends_temp = new int[text.Length];
            int idx = 0;

            fixed (char* sb = &sb_temp[0])
            fixed (Int32* starts = &starts_temp[0])
            fixed (Int32* ends = &ends_temp[0])

            fixed (char* _ptext = &text[0])
            fixed (ushort* _pdict = &_bidiDict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                var length = text.Length;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t == 1) { p = 0; continue; }
                    if (t <= _firstMaxChar) {
                        p = t;
                        sb[idx] = t1;
                        starts[idx] = src.Start[i];
                        ends[idx] = src.End[i];
                        idx++;
                        continue;
                    }

                    var next = _pnext[p] + t;
                    bool find = _pkey[next] == t;
                    if (find == false && p != 0) {
                        do {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) { find = true; break; }
                            if (p == 0) { next = 0; break; }
                        } while (true);
                    }

                    if (find) {
                        var index = _pcheck[next];
                        if (index != 0) {
                            var len = _len[index];
                            var key = _keywords[index];
                            if (len == 2 && key.Length == 2) {
                                sb[idx - 1] = key[0];
                                sb[idx] = key[1];
                            } else if (len == 2) { 
                                sb[idx - 1] = key[0];
                                ends[idx - 1] = src.End[i];
                                continue;
                            } else { 
                                sb[idx] = key[0];
                            }
                        } else {
                            sb[idx] = t1;
                        }
                        p = next;
                    } else {
                        sb[idx] = t1;
                    }
                    starts[idx] = src.Start[i];
                    ends[idx] = src.End[i];
                    idx++;
                }
            }
            TextStream stream = new TextStream(src.Source);
            stream.TestingText = new char[idx];
            Array.Copy(sb_temp, stream.TestingText, idx);
            stream.Start = new int[idx];
            Array.Copy(starts_temp, stream.Start, idx);
            stream.End = new int[idx];
            Array.Copy(ends_temp, stream.End, idx);

            sb_temp = null;
            starts_temp = null;
            ends_temp = null;
            return stream;
        }

        private unsafe ReadStreamBase Replace2(ReadStreamBase src)
        {
            var text = src.TestingText;
            char[] sb_temp = new char[text.Length];
            int[] starts_temp = new int[text.Length];
            int[] ends_temp = new int[text.Length];
            int idx = 0;

            fixed (char* sb = &sb_temp[0])
            fixed (Int32* starts = &starts_temp[0])
            fixed (Int32* ends = &ends_temp[0])

            fixed (char* _ptext = &text[0])
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                var length = text.Length;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t <= _firstMaxChar) {
                        p = t;
                        sb[idx] = t1;
                        starts[idx] = src.Start[i];
                        ends[idx] = src.End[i];
                        idx++;
                        continue;
                    }

                    var next = _pnext[p] + t;
                    bool find = _pkey[next] == t;
                    if (find == false && p != 0) {
                        do {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) { find = true; break; }
                            if (p == 0) { next = 0; break; }
                        } while (true);
                    }

                    if (find) {
                        var index = _pcheck[next];
                        if (index != 0) {
                            var len = _len[index];
                            var key = _keywords[index];
                            if (len == 2 && key.Length == 2) {
                                sb[idx - 1] = key[0];
                                sb[idx] = key[1];
                            } else if (len == 2) { 
                                sb[idx - 1] = key[0];
                                ends[idx - 1] = src.End[i];
                                continue;
                            } else { 
                                sb[idx] = key[0];
                            }
                        } else {
                            sb[idx] = t1;
                        }
                        p = next;
                    } else {
                        sb[idx] = t1;
                    }
                    starts[idx] = src.Start[i];
                    ends[idx] = src.End[i];
                    idx++;
                }
            }
            TextStream stream = new TextStream(src.Source);
            stream.TestingText = new char[idx];
            Array.Copy(sb_temp, stream.TestingText, idx);
            stream.Start = new int[idx];
            Array.Copy(starts_temp, stream.Start, idx);
            stream.End = new int[idx];
            Array.Copy(ends_temp, stream.End, idx);
            sb_temp = null;
            starts_temp = null;
            ends_temp = null;
            return stream;
        }

        private unsafe ReadStreamBase Replace1(in string text)
        {
            char[] sb_temp = new char[text.Length];
            int[] starts_temp = new int[text.Length];
            int[] ends_temp = new int[text.Length];
            int idx = 0;

            var chs = text.ToCharArray();
            fixed (char* sb = &sb_temp[0])
            fixed (Int32* starts = &starts_temp[0])
            fixed (Int32* ends = &ends_temp[0])

            fixed (char* _ptext = &chs[0])
            fixed (ushort* _pdict = &_bidiDict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                var length = text.Length;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];

                    var t = _pdict[t1];
                    if (t == 1) { p = 0; continue; }
                    if (t <= _firstMaxChar) {
                        p = t;
                        sb[idx] = t1;
                        starts[idx] = i;
                        ends[idx] = i;
                        idx++;
                        continue;
                    }

                    var next = _pnext[p] + t;

                    bool find = _pkey[next] == t;
                    if (find == false && p != 0) {
                        do {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) { find = true; break; }
                            if (p == 0) { next = 0; break; }
                        } while (true);
                    }

                    if (find) {
                        var index = _pcheck[next];
                        if (index != 0) {
                            var len = _len[index];
                            var key = _keywords[index];
                            if (len == 2 && key.Length == 2) {
                                sb[idx - 1] = key[0];
                                sb[idx] = key[1];
                            } else if (len == 2) { 
                                sb[idx - 1] = key[0];
                                ends[idx - 1] = i;
                                continue;
                            } else { 
                                sb[idx] = key[0];
                            }
                        } else {
                            sb[idx] = t1;
                        }
                        p = next;
                    } else {
                        sb[idx] = t1;
                    }
                    starts[idx] = i;
                    ends[idx] = i;
                    idx++;

                }
            }

            TextStream stream = new TextStream(chs);
            stream.TestingText = new char[idx];
            Array.Copy(sb_temp, stream.TestingText, idx);
            stream.Start = new int[idx];
            Array.Copy(starts_temp, stream.Start, idx);
            stream.End = new int[idx];
            Array.Copy(ends_temp, stream.End, idx);
            sb_temp = null;
            starts_temp = null;
            ends_temp = null;
            return stream;

        }


        private unsafe ReadStreamBase Replace2(in string text)
        {
            char[] sb_temp = new char[text.Length];
            int[] starts_temp = new int[text.Length];
            int[] ends_temp = new int[text.Length];
            int idx = 0;

            var chs = text.ToCharArray();
            fixed (char* sb = &sb_temp[0])
            fixed (Int32* starts = &starts_temp[0])
            fixed (Int32* ends = &ends_temp[0])

            fixed (char* _ptext = &chs[0])
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                var length = text.Length;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t <= _firstMaxChar) {
                        p = t;
                        sb[idx] = t1;
                        starts[idx] = i;
                        ends[idx] = i;
                        idx++;
                        continue;
                    }

                    var next = _pnext[p] + t;

                    bool find = _pkey[next] == t;
                    if (find == false && p != 0) {
                        do {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) { find = true; break; }
                            if (p == 0) { next = 0; break; }
                        } while (true);
                    }

                    if (find) {
                        var index = _pcheck[next];
                        if (index != 0) {
                            var len = _len[index];
                            var key = _keywords[index];
                            if (len == 2 && key.Length == 2) {
                                sb[idx - 1] = key[0];
                                sb[idx] = key[1];
                            } else if (len == 2) { 
                                sb[idx - 1] = key[0];
                                ends[idx - 1] = i;
                                continue;
                            } else { 
                                sb[idx] = key[0];
                            }
                        } else {
                            sb[idx] = t1;
                        }
                        p = next;
                    } else {
                        sb[idx] = t1;
                    }
                    starts[idx] = i;
                    ends[idx] = i;
                    idx++;
                }
            }
            TextStream stream = new TextStream(chs);
            stream.TestingText = new char[idx];
            Array.Copy(sb_temp, stream.TestingText, idx);
            stream.Start = new int[idx];
            Array.Copy(starts_temp, stream.Start, idx);
            stream.End = new int[idx];
            Array.Copy(ends_temp, stream.End, idx);
            sb_temp = null;
            starts_temp = null;
            ends_temp = null;
            return stream;

        }


        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
            _bidiDict = br.ReadUshortArray();
            _key = br.ReadUshortArray();
            _next = br.ReadIntArray();
            _check = br.ReadIntArray();
            _failure = br.ReadIntArray();
            var len = br.ReadInt32();
            _len = br.ReadBytes(len);
            len = br.ReadInt32();
            _keywords = new string[len];
            for (int i = 0; i < len; i++) {
                _keywords[i] = br.ReadString();
            }


        }
        #endregion

    }
}
