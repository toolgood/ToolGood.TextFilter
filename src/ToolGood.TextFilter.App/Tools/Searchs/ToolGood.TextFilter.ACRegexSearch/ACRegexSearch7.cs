/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter
{
    public unsafe class ACRegexSearch7 : IACRegexSearch
    {
        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private ushort[] _minLayer;
        private ushort[] _maxLayer;
        private ushort _minEndKey;


        public byte[] _skipIndexs;
        public ushort[][] _dicts;
        public ISkipwordsSearch[] _skipwordsSearchs;
        public bool[] _useSkipOnce;
        public int[][] _dictIndex;

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

        public struct FindItem
        {
            public int End;
            public int SkipIndex;
            public FindItem(int end, int skipIndex) { End = end; SkipIndex = skipIndex; }
        }

        public unsafe void FindAll(char* _ptext, in int length, List<TempWordsResultItem> result)
        {
            List<FindItem> findItems = new List<FindItem>();
            HashSet<uint> set = new HashSet<uint>();
            HashSet<uint> skipSet = new HashSet<uint>();

            fixed (int* _pend = &_end[0])
            fixed (int* _pfirst = &_first[0]) {
                #region 第一次 
                fixed (ushort* _pdict = &_dicts[0][0])
                fixed (ushort* _pminLayer = &_minLayer[0])
                fixed (ushort* _pmaxLayer = &_maxLayer[0]) {
                    int _min = 0;
                    int* min = &_min;
                    var len = length - 1;
                    for (int i = 0; i < length; i++) {
                        //if (i==23) {

                        //}
                        var t1 = _ptext[i];
                        var t = _pdict[t1];
                        if (t == 0) { *min = 0; continue; }
                        if (t == 0xffff) { continue; }//跳词

                        var n = *(_pminLayer + t);
                        if (n == 1) {
                            *min = 1;
                            if (t >= _minEndKey && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                Find(_ptext, t, i, set, skipSet, result, _pdict, _pend, _pfirst, findItems);
                            }
                        } else if (*min != 0) {
                            if (*(_pmaxLayer + t) <= *min) { *min = 0; continue; }// 当前最小layer为min时，下一个字符最小layer应为min+1
                            *min = n;
                            if (t >= _minEndKey && CheckNextChar(_ptext, len, t, i, _pdict)) {
                                Find(_ptext, t, i, set, skipSet, result, _pdict, _pend, _pfirst, findItems);
                            }
                        }
                    }
                }
                #endregion
                if (findItems.Count == 0) { findItems = null; set = null; return; }
                HashSet<int> indexSet = new HashSet<int>();
                for (int i = 0; i < findItems.Count; i++) { indexSet.Add(findItems[i].SkipIndex); }

                foreach (var index in indexSet) {
                    var skip2 = new bool[length];
                    fixed (bool* skip = &skip2[0])
                    fixed (ushort* _pdict = &_dicts[index][0]) {

                        if (_skipwordsSearchs[index].FindAll(_ptext, length, skip)) {
                            if (_useSkipOnce[index] == false) {
                                for (int i = 0; i < findItems.Count; i++) {
                                    var item = findItems[i];
                                    if (item.SkipIndex == index) {
                                        Find_3(_ptext, item.End, index, skip, skipSet, result, _pend, _pfirst);
                                    }
                                }
                            } else {
                                for (int i = 0; i < findItems.Count; i++) {
                                    var item = findItems[i];
                                    if (item.SkipIndex == index) {
                                        Find_4(_ptext, item.End, index, skip, skipSet, result, _pend, _pfirst);
                                    }
                                }
                            }
                        }
                    }
                }
                indexSet = null;
            }
            findItems = null;
            set = null;
            skipSet = null;
        }

        private unsafe void Find(char* _ptext, ushort tc, int end, HashSet<uint> set, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend, int* _pfirst, List<FindItem> findItems)
        {
            var type = Find_0(_ptext, tc, _pfirst[tc], end, set, result, _pdict, _pend);
            foreach (var index in _dictIndex[type]) {
                var skipwordsSearch = _skipwordsSearchs[index];
                if (skipwordsSearch == null) {
                    if (_useSkipOnce[index] == false) {
                        Find_1(_ptext, end, index, skipSet, result, _pend, _pfirst);
                    } else {
                        Find_2(_ptext, end, index, skipSet, result, _pend, _pfirst);
                    }
                } else {
                    findItems.Add(new FindItem(end, index));
                }
            }
        }

        private unsafe byte Find_0(char* _ptext, ushort tc, int first, int end, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = first;
            var index = _pend[next];
            if (index != 0 && CheckPreChar(_ptext, tc, end, _pdict)) {
                AddToResult(end, end, index, set, result);
            }

            byte resultType = 0;
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                resultType = (byte)(resultType | _skipIndexs[t1]);
                tc = _pdict[t1];
                if (tc == 0xffff) { continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return resultType; }

                index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                    AddToResult(j, end, index, set, result);
                }
            }
            return resultType;
        }
        private unsafe void Find_1(char* _ptext, int end, int index, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];
            var tc = _pdict[_ptext[end]];
            if (tc >= 0xfffe) { return; }
            var next = _pfirst[tc];

            bool skip = false;
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                tc = _pdict[t1];
                if (tc == 0xffff) { skip = true; continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                if (skip) {
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                        AddToResult(j, end, index, skipSet, result);
                    }
                }
                //if (index != 0 && skip && CheckPreChar(_ptext, tc, j, _pdict)) {
                //    AddToResult(j, end, index, skipSet, result);
                //}
            }
        }
        private unsafe void Find_2(char* _ptext, int end, int index, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];
            var tc = _pdict[_ptext[end]];
            if (tc >= 0xfffe) { return; }
            var next = _pfirst[tc];

            bool skip = false;
            var len = -1;
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j];
                tc = _pdict[t1];
                if (tc == 0xffff) { skip = true; continue; }
                if (tc == 0xfffe) {
                    if (len == -1) { len = 1; skip = true; continue; }
                    if (len == 0) { return; }
                    len--;
                    skip = true;
                    continue;
                }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; }

                if (skip) {
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                        AddToResult(j, end, index, skipSet, result);
                    }
                }
                //index = _pend[next];
                //if (index != 0 && skip && CheckPreChar(_ptext, tc, j, _pdict)) {
                //    AddToResult(j, end, index, skipSet, result);
                //}
            }
        }

        private unsafe void Find_3(char* _ptext, int end, int index, bool* skips, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];
            var tc = _pdict[_ptext[end]];
            if (tc >= 0xfffe) { return; }
            var next = _pfirst[tc];

            bool skip = false;
            for (int j = end - 1; j >= 0; j--) {
                if (skips[j]) { skip = true; continue; }
                tc = _pdict[_ptext[j]];
                if (tc == 0xffff) { skip = true; continue; }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                if (skip) {
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                        AddToResult(j, end, index, skipSet, result);
                    }
                }
                //index = _pend[next];
                //if (index != 0 && skip && CheckPreChar(_ptext, tc, j, _pdict)) {
                //    AddToResult(j, end, index, skipSet, result);
                //}
            }
        }
        private unsafe void Find_4(char* _ptext, int end, int index, bool* skips, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];
            var tc = _pdict[_ptext[end]];
            if (tc >= 0xfffe) { return; }
            var next = _pfirst[tc];

            bool skip = false;
            var len = -1;
            for (int j = end - 1; j >= 0; j--) {
                if (skips[j]) { skip = true; continue; }
                tc = _pdict[_ptext[j]];
                if (tc == 0xffff) { skip = true; continue; }
                if (tc == 0xfffe) {
                    if (len == -1) { len = 1; skip = true; continue; }
                    if (len == 0) { return; }
                    len--;
                    skip = true;
                    continue;
                }
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; }

                if (skip) {
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {
                        AddToResult(j, end, index, skipSet, result);
                    }
                }
                //index = _pend[next];
                //if (index != 0 && skip && CheckPreChar(_ptext, tc, j, _pdict)) {
                //    AddToResult(j, end, index, skipSet, result);
                //}
            }
        }

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
            //非数字与英文
            if (t < _azNumMinChar || t > _azNumMaxChar || i == length) { return true; }

            var tt = _pdict[_ptext[i + 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckPreChar(char* _ptext, ushort t, int i, ushort* _pdict)
        {
            //非数字与英文
            if (t < _azNumMinChar || t > _azNumMaxChar || i == 0) { return true; }

            var tt = _pdict[_ptext[i - 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe bool CheckPreChar(char* _ptext, ushort t, int i, ushort[] _pdict)
        {
            //非数字与英文
            if (t < _azNumMinChar || t > _azNumMaxChar || i == 0) { return true; }

            var tt = _pdict[_ptext[i - 1]];
            if (tt < _azNumMinChar || tt > _azNumMaxChar) { return true; }
            return false;
        }

        #endregion


        #region Load
        public void Load(BinaryReader br)
        {
            _azNumMinChar = br.ReadUInt16();
            _azNumMaxChar = br.ReadUInt16();
            _minLayer = br.ReadUshortArray();
            _maxLayer = br.ReadUshortArray();
            _minEndKey = br.ReadUInt16();

            _first = br.ReadIntArray();

            var length = br.ReadInt32();
            _nextIndex = new IntDictionary[length];
            for (int i = 0; i < length; i++) {
                _nextIndex[i] = IntDictionary.Load(br);
            }
            _end = br.ReadIntArray();

        }

        #endregion


    }
}
