/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.IO;

namespace ToolGood.TextFilter
{
    public interface ISkipwordsSearch
    {
        void Load(BinaryReader br);

        unsafe bool FindAll(char* _ptext, int length, bool* result);

    }

    public class SkipwordsSearch : ISkipwordsSearch
    {
        private ushort _firstMaxChar;
        private ushort[] _dict;

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _checkLen;
        private Int32[] _failure;

        #region unsafe FindAll

        public unsafe bool FindAll(char* _ptext, int length, bool* result)
        {
            var _find = false;
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_checkLen[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];

                    var t = _pdict[t1];
                    if (t <= _firstMaxChar) { p = t; continue; }
                    if (t == 0xffff) { continue; }

                    var next = _pnext[p] + t;
                    if (_pkey[next] == t) {
                        var len = _pcheck[next];
                        if (len > 0) {
                            _find = true;
                            var idx = i;
                            while (len != 0) {
                                if (_pdict[_ptext[idx]] != 0xffff) { len--; }
                                result[idx] = true;
                                idx--;
                            }
                        }
                        p = next;
                    } else {
                        while (p != 0) {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) {
                                var len = _pcheck[next];
                                if (len > 0) {
                                    _find = true;
                                    var idx = i;
                                    while (len != 0) {
                                        if (_pdict[_ptext[idx]] != 0xffff) { len--; }
                                        result[idx] = true;
                                        idx--;
                                    }
                                }
                                p = next;
                                break;
                            }
                        }
                    }


                }
            }
            return _find;
        }

        #endregion

        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
            _key = br.ReadUshortArray();
            _next = br.ReadIntArray();
            _checkLen = br.ReadIntArray();
            _failure = br.ReadIntArray();
        }
        #endregion

    }
}
