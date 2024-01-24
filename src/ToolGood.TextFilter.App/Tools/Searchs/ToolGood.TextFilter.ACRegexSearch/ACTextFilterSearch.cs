/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// 没用到的类
    /// </summary>
    public class ACTextFilterSearch : IACRegexSearch
    {
        public byte[] _skipIndexs;
        public ushort[][] _dicts;
        public ISkipwordsSearch[] _skipwordsSearchs;
        public bool[] _useSkipOnce;
        public int[][] _dictIndex;

        private ushort _firstMaxChar;
        private int _maxLength;
        private int[] _first_first;
        private IntDictionary[] _nextIndex_first;
        private int[] _failure;
        private bool[] _check;

        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private int[] _first;
        private IntDictionary[] _nextIndex;
        private int[] _end;

        #region SetDict
        public void SetDict(byte[] skipIndexs, ushort[][] dicts, ISkipwordsSearch[] skipwordsSearchs, bool[] useSkipOnce)
        {
            _skipIndexs = skipIndexs;
            _dicts = dicts;
            _skipwordsSearchs = skipwordsSearchs;
            _useSkipOnce = useSkipOnce;

            var max = (int)Math.Pow(2, dicts.Length - 1);
            int[][] list = new int[max][];

            for (int type = 0; type < max; type++) {
                List<int> ls = new List<int>();
                for (int j = 0; j < _dicts.Length - 1; j++) {
                    var indexFlag = 1 << j;
                    if ((type & indexFlag) == indexFlag) {
                        ls.Add(j + 1);
                    }
                }
                list[type] = ls.ToArray();
            }
            _dictIndex = list;
        }

        #endregion

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

        public unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result)
        {
            HashSet<uint> set = new HashSet<uint>();
            fixed (int* _pfailure = &_failure[0])
            fixed (bool* _pcheck = &_check[0])
            fixed (int* _pend = &_end[0]) {
                //var type = 
                FindAll_0(_ptext, length, set, result, _pfailure, _pcheck, _pend);
                for (int index = 1; index < _dicts.Length; index++) {
                    var skipwordsSearch = _skipwordsSearchs[index];
                    if (skipwordsSearch == null) {
                        if (_useSkipOnce[index] == false) {
                            FindAll_1(_ptext, length, set, result, index, _pfailure, _pcheck, _pend);
                        } else {
                            FindAll_2(_ptext, length, set, result, index, _pfailure, _pcheck, _pend);
                        }
                    } else {
                        var skip2 = new bool[length];
                        fixed (bool* skip = &skip2[0]) {
                            if (_skipwordsSearchs[index].FindAll(_ptext, length, skip)) {
                                if (_useSkipOnce[index] == false) {
                                    FindAll_3(_ptext, length, set, result, index, skip, _pfailure, _pcheck, _pend);
                                } else {
                                    FindAll_4(_ptext, length, set, result, index, skip, _pfailure, _pcheck, _pend);
                                }
                            }
                        }
                    }
                }

            }
            set = null;
        }

        #region FindAll_0 FindAll_1 FindAll_2 FindAll_3 FindAll_4
        // 第一遍 查询
        public unsafe void FindAll_0(char* _ptext, in int length, HashSet<uint> set, List<TempWordsResultItem> result
            , int* _pfailure, bool* _pcheck, int* _pend)
        {
            fixed (ushort* _pdict = &_dicts[0][0])
            fixed (byte* _pskipIndexs = &_skipIndexs[0]) {
                var p = 0;
                var len = length - 1;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t == 0xffff) { continue; }
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                            Find_0(_ptext, t, i, set, result, _pdict, _pend);
                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                    Find_0(_ptext, t, i, set, result, _pdict, _pend);
                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }
            }
        }
        public unsafe void FindAll_1(char* _ptext, in int length, HashSet<uint> set, List<TempWordsResultItem> result, int index
            , int* _pfailure, bool* _pcheck, int* _pend)
        {
            fixed (ushort* _pdict = &_dicts[index][0]) {
                var p = 0;
                var len = length - 1;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t >= 0xfffe) { continue; }
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                            Find_1(_ptext, t, i, set, result, _pdict, _pend);
                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (p != 0 && _pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                    Find_1(_ptext, t, i, set, result, _pdict, _pend);
                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }

            }
        }
        public unsafe void FindAll_2(char* _ptext, in int length, HashSet<uint> set, List<TempWordsResultItem> result, int index
            , int* _pfailure, bool* _pcheck, int* _pend)
        {
            fixed (ushort* _pdict = &_dicts[index][0]) {
                var p = 0;
                var len = length - 1;
                for (int i = 0; i < length; i++) {
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t >= 0xfffe) { continue; }
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                            Find_2(_ptext, t, i, set, result, _pdict, _pend);
                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (p != 0 && _pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                    Find_2(_ptext, t, i, set, result, _pdict, _pend);
                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }

            }
        }
        public unsafe void FindAll_3(char* _ptext, in int length, HashSet<uint> set, List<TempWordsResultItem> result, int index, bool* skips
             , int* _pfailure, bool* _pcheck, int* _pend)
        {
            fixed (ushort* _pdict = &_dicts[index][0]) {
                var p = 0;
                var len = length - 1;
                for (int i = 0; i < length; i++) {
                    if (skips[i]) { continue; }
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t >= 0xfffe) { continue; }
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                            Find_3(_ptext, t, i, skips, set, result, _pdict, _pend);
                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (p != 0 && _pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                    Find_3(_ptext, t, i, skips, set, result, _pdict, _pend);
                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }
            }
        }
        public unsafe void FindAll_4(char* _ptext, in int length, HashSet<uint> set, List<TempWordsResultItem> result, int index, bool* skips
             , int* _pfailure, bool* _pcheck, int* _pend)
        {
            fixed (ushort* _pdict = &_dicts[index][0]) {
                var p = 0;
                var len = length - 1;
                for (int i = 0; i < length; i++) {
                    if (skips[i]) { continue; }
                    var t1 = _ptext[i];
                    var t = _pdict[t1];
                    if (t == 0xffff) { continue; }
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                            Find_4(_ptext, t, i, skips, set, result, _pdict, _pend);
                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (p != 0 && _pcheck[next] && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                    Find_4(_ptext, t, i, skips, set, result, _pdict, _pend);
                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }
            }
        }

        #endregion


        #region Find_0  Find_1  Find_2  Find_3  Find_4
        private unsafe void Find_0(char* _ptext, ushort tc, in int end, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = _first[tc];
            var index = _pend[next];
            if (index != 0 && CheckPreChar(_ptext, tc, end, _pdict)) {
                AddToResult(end, end, index, set, result);
            }

            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
        }
        private unsafe void Find_1(char* _ptext, ushort tc, in int end, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = _first[tc];

            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                var index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
        }
        private unsafe void Find_2(char* _ptext, ushort tc, in int end, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = _first[tc];

            var len = -1;
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0xfffe) {
                    if (len == -1) { len = 1; continue; }
                    if (len == 0) { return; }
                    len--;
                    continue;
                }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; }

                var index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
        }
        private unsafe void Find_3(char* _ptext, ushort tc, in int end, bool* skips, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = _first[tc];

            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                if (skips[j]) { continue; }
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                var index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
        }
        private unsafe void Find_4(char* _ptext, ushort tc, in int end, bool* skips, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = _first[tc];

            var len = -1;
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                if (skips[j]) { continue; }
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0xfffe) {
                    if (len == -1) { len = 1; continue; }
                    if (len == 0) { return; }
                    len--;
                    continue;
                }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; }

                var index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
        }

        #endregion




        #region TryGet CheckNextChar CheckPreChar AddToResult
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddToResult(int start_key, int end_key, int index, HashSet<uint> set, List<TempWordsResultItem> result)
        {
            uint u = ((uint)start_key << 10) | ((uint)end_key & 0x3ff);
            if (set.Add(u)) {
                var r = new TempWordsResultItem(start_key, end_key, GetMatchKeyword(index));
                result.Add(r);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckPreChar(char* _ptext, ushort t, int i, ushort[] _pdict)
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
            _maxLength = br.ReadInt32();
            _first_first = br.ReadIntArray();
            var length = br.ReadInt32();
            _nextIndex_first = new IntDictionary[length];
            for (int i = 0; i < length; i++) {
                _nextIndex_first[i] = IntDictionary.Load(br);
            }
            _failure = br.ReadIntArray();
            _check = br.ReadBoolArray();

            _azNumMinChar = br.ReadUInt16();
            _azNumMaxChar = br.ReadUInt16();
            _first = br.ReadIntArray();

            length = br.ReadInt32();
            _nextIndex = new IntDictionary[length];
            for (int i = 0; i < length; i++) {
                _nextIndex[i] = IntDictionary.Load(br);
            }
            _end = br.ReadIntArray();

        }


        #endregion


    }
}
