/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    public interface IKeywordsSearch 
    {
        void Set_GetMatchKeyword(Func<int, KeywordInfo> func);

        unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result);

        void Load(BinaryReader br);

    }
    class KeywordsSearch2 : IKeywordsSearch
    {
        private ushort _firstMaxChar;
        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private ushort[] _dict; 

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _check;
        private Int32[] _failure;
        private Int32[][] _guides;

        #region GetMatchKeyword Set_GetMatchKeyword
        private Func<int, KeywordInfo> _getKeyword;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KeywordInfo GetMatchKeyword(int resultIndex)
        {
            return _getKeyword(resultIndex);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set_GetMatchKeyword(Func<int, KeywordInfo> func)
        {
            _getKeyword = func;
        }
        #endregion

        #region unsafe FindAll
        public unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result)
        {
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];

                    var t = _pdict[t1];
                    if (t == 1) { continue; }
                    if (t <= _firstMaxChar) { p = t; continue; }

                    var next = _pnext[p] + t;
                    if (_pkey[next] == t) {
                        var index = _pcheck[next];
                        if (index != 0 && CheckNextChar(_ptext, length, t, i, _pdict)) {
                            var guides = _guides[index];
                            var start = i;
                            var tempLen = 1;
                            var tc = t;
                            for (int ij = 0; ij < guides.Length; ij++) {
                                var item = guides[ij];

                                var keyInfo = GetMatchKeyword(item);
                                var len = keyInfo.KeywordLength;
                                while (tempLen < len) {
                                    if ((tc = _pdict[_ptext[--start]]) != 1) { tempLen++; }
                                }
                                if (CheckPreChar(_ptext, tc, start, _pdict)) {
                                    result.Add(new TempWordsResultItem(start, i, keyInfo));
                                }
                            }
                        }
                        p = next;
                    } else {
                        while (p != 0) {
                            p = _pfailure[p];
                            next = _pnext[p] + t;
                            if (_pkey[next] == t) {
                                var index = _pcheck[next];
                                if (index != 0 && CheckNextChar(_ptext, length, t, i, _pdict)) {
                                    var guides = _guides[index];
                                    var start = i;
                                    var tempLen = 1;
                                    var tc = t;
                                    for (int ij = 0; ij < guides.Length; ij++) {
                                        var item = guides[ij];

                                        var keyInfo = GetMatchKeyword(item);
                                        var len = keyInfo.KeywordLength;
                                        while (tempLen < len) {
                                            if ((tc = _pdict[_ptext[--start]]) != 1) { tempLen++; }
                                        }
                                        if (CheckPreChar(_ptext, tc, start, _pdict)) {
                                            result.Add(new TempWordsResultItem(start, i, keyInfo));
                                        }
                                    }
                                }
                                p = next;
                                break;
                            }
                        }
                    }

                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckNextChar(char* _ptext, int length, ushort t, int i, ushort* _pdict)
        {
            if (t < _azNumMinChar || t > _azNumMaxChar || i == length) { return true; }

            var tt = _pdict[_ptext[i + 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckPreChar(char* _ptext, ushort t, int i, ushort* _pdict)
        {
            if (t < _azNumMinChar || t > _azNumMaxChar || i == 0) { return true; }

            var tt = _pdict[_ptext[i - 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        #endregion

        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _azNumMinChar = br.ReadUInt16();
            _azNumMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
            _key = br.ReadUshortArray();

            _next = br.ReadIntArray();
            _check = br.ReadIntArray();
            _failure = br.ReadIntArray();
            _guides = br.ReadIntArray2();
        }


        #endregion
    }
}
