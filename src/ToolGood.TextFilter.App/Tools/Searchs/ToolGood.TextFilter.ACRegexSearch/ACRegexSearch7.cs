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
        private ushort _azNumMinChar;   // 最小英文数字映射值
        private ushort _azNumMaxChar;   // 最大英文数字映秀值
        private ushort[] _minLayer;     // 字符所在最小层级(位置索引)，从0开始
        private ushort[] _maxLayer;     // 字符所在最大层级(位置索引)，从0开始
        private ushort _minEndKey;      // 结束字符的最小值



        public byte[] _skipIndexs;      // 跳词索引
        public ushort[][] _dicts;       // 映秀字典集
        public ISkipwordsSearch[] _skipwordsSearchs;    // 跳词检验
        public bool[] _useSkipOnce;     // 跳词计数，只计一次
        public int[][] _dictIndex;      // 跳词字典索引


        private int[] _first;           // 第一次位置
        private IntDictionary[] _nextIndex; // 用于判断下一个位置
        private int[] _end;             // 敏感词 索引


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
            List<FindItem> findItems = new List<FindItem>();// 临时结构
            HashSet<uint> set = new HashSet<uint>();// 去重标识
            HashSet<uint> skipSet = new HashSet<uint>(); // 去重标识，跳词用的

            fixed (int* _pend = &_end[0]) // 使用fixed加速
            fixed (int* _pfirst = &_first[0]) { // 使用fixed加速
                #region 第一次 
                fixed (ushort* _pdict = &_dicts[0][0])// 使用第0组映射字典 使用fixed加速
                fixed (ushort* _pminLayer = &_minLayer[0])// 使用fixed加速
                fixed (ushort* _pmaxLayer = &_maxLayer[0]) {// 使用fixed加速
                    int _min = 0;
                    int* min = &_min;
                    var len = length - 1;
                    for (int i = 0; i < length; i++) {// 从左到右匹配
                        var t1 = _ptext[i];// 获取字符
                        var t = _pdict[t1];// 获取映射
                        if (t == 0) { *min = 0; continue; }// 映射字符不存在，跳过
                        if (t == 0xffff) { continue; } // 是跳词，跳过

                        var n = *(_pminLayer + t); // 获取最小层级(位置索引)
                        if (n == 1) { // 最小层级(位置索引)为1
                            *min = 1;  // 最小值赋值为1
                            if (t >= _minEndKey && CheckNextChar(_ptext, len, t, i, _pdict)) {//★ 检测下一字符，防误判英文数字
                                Find(_ptext, t, i, set, skipSet, result, _pdict, _pend, _pfirst, findItems);// 进入第二次查寻
                            }
                        } else if (*min != 0) {// 最小值不为0
                            if (*(_pmaxLayer + t) <= *min) { *min = 0; continue; }// 最大层级(位置索引)小于等于最小值 ，跳过
                            *min = n; // 更新最小值
                            if (t >= _minEndKey && CheckNextChar(_ptext, len, t, i, _pdict)) { //★ 检测下一字符，防误判英文数字 
                                Find(_ptext, t, i, set, skipSet, result, _pdict, _pend, _pfirst, findItems);// 进入第二次查寻
                            }
                        }
                    }
                }
                #endregion
                if (findItems.Count == 0) { findItems = null; set = null; return; } //暂存表数据为0，返回
                HashSet<int> indexSet = new HashSet<int>();
                for (int i = 0; i < findItems.Count; i++) { indexSet.Add(findItems[i].SkipIndex); }//获取多组词跳词搜索

                foreach (var index in indexSet) {
                    var skip2 = new bool[length];// 分配内存
                    fixed (bool* skip = &skip2[0]) // 使用 fixed 加速
                    fixed (ushort* _pdict = &_dicts[index][0]) { // 使用 fixed 加速

                        if (_skipwordsSearchs[index].FindAll(_ptext, length, skip)) { // 获取跳词位置标识
                            if (_useSkipOnce[index] == false) { // 是否使用跳词计数
                                for (int i = 0; i < findItems.Count; i++) {  // 循环 暂存表
                                    var item = findItems[i];
                                    if (item.SkipIndex == index) { //判断是否是跳词
                                        Find_3(_ptext, item.End, index, skip, skipSet, result, _pend, _pfirst); // 匹配文本
                                    }
                                }
                            } else {
                                for (int i = 0; i < findItems.Count; i++) {// 循环 暂存表
                                    var item = findItems[i];
                                    if (item.SkipIndex == index) { //判断是否是跳词
                                        Find_4(_ptext, item.End, index, skip, skipSet, result, _pend, _pfirst);// 匹配文本
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
            // 进行第一遍匹配，
            var type = Find_0(_ptext, tc, _pfirst[tc], end, set, result, _pdict, _pend);
            foreach (var index in _dictIndex[type]) { // 获取  跳词搜索列表
                var skipwordsSearch = _skipwordsSearchs[index];
                if (skipwordsSearch == null) {
                    // 跳词搜索不存在，跳词搜索主要针对多词跳词
                    if (_useSkipOnce[index] == false) {
                        // 不使用跳词计数，去匹配
                        Find_1(_ptext, end, index, skipSet, result, _pend, _pfirst);
                    } else {
                        // 使用跳词计数，去匹配
                        Find_2(_ptext, end, index, skipSet, result, _pend, _pfirst);
                    }
                } else {
                    findItems.Add(new FindItem(end, index));// 将记录暂存在findItems
                }
            }
        }

        private unsafe byte Find_0(char* _ptext, ushort tc, int first, int end, HashSet<uint> set, List<TempWordsResultItem> result
            , ushort* _pdict, int* _pend)
        {
            var next = first;
            var index = _pend[next];
            if (index != 0 && CheckPreChar(_ptext, tc, end, _pdict)) { // ★ 检测上一字符，防误判英文数字
                AddToResult(end, end, index, set, result); // 添加到 result列表
            }

            byte resultType = 0; // 返回类型，每一位标记为1时，代表要进行跳词搜索
            for (int j = end - 1; j >= 0; j--) { // ★ 开始为 end-1
                var t1 = _ptext[j]; // 获取字符
                resultType = (byte)(resultType | _skipIndexs[t1]); //  异或，获取跳词搜索类型
                tc = _pdict[t1]; // 获取映射符
                if (tc == 0xffff) { continue; } // 跳过跳词
                // 出现敏感词内不存在的词 或  无法获取下一字符，返回跳词搜索类型
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return resultType; }

                index = _pend[next];
                if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) { // ★ 检测上一字符，防误判英文数字
                    AddToResult(j, end, index, set, result); // 添加到 result列表
                }
            }
            return resultType; // 返回跳词搜索类型
        }
        private unsafe void Find_1(char* _ptext, int end, int index, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index]; // 获取映射字典
            var tc = _pdict[_ptext[end]];  // 获取未端列表
            if (tc >= 0xfffe) { return; }  // ★ 判断 是否为跳词，如果是跳词，返回
            var next = _pfirst[tc];

            bool skip = false; // 跳词标记
            for (int j = end - 1; j >= 0; j--) { // ★ 开始为 end-1
                var t1 = _ptext[j]; // 获取字符
                tc = _pdict[t1]; // 获取映射符
                if (tc == 0xffff) { skip = true; continue; } // 跳词标记为true
                // 出现敏感词内不存在的词 或  无法获取下一字符，返回跳词搜索类型
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }

                if (skip) { //★ 有跳词，防重复
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) { // ★ 检测上一字符，防误判英文数字
                        AddToResult(j, end, index, skipSet, result); // 添加到 result列表
                    }
                }
            }
        }
        private unsafe void Find_2(char* _ptext, int end, int index, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];         // 获取映射字典
            var tc = _pdict[_ptext[end]];       // 获取未端列表
            if (tc >= 0xfffe) { return; }       // ★ 判断 是否为跳词，如果是跳词，返回
            var next = _pfirst[tc];

            bool skip = false; // 跳词标记
            var len = -1; // 计数变量，-1为初始值，1为可跳过一个，0为计数用完
            for (int j = end - 1; j >= 0; j--) {
                var t1 = _ptext[j]; // 获取字符
                tc = _pdict[t1];  // 获取映射符
                if (tc == 0xffff) { skip = true; continue; } // 跳词标记为true
                if (tc == 0xfffe) { // 判断 记数跳词
                    if (len == -1) { len = 1; skip = true; continue; }
                    if (len == 0) { return; } // 跳词计数用完 ，返回
                    len--; // 跳词计数-1
                    skip = true; // 跳词标记为true
                    continue;
                }
                // 出现敏感词内不存在的词 或  无法获取下一字符，返回跳词搜索类型
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; } // 重置计数跳词

                if (skip) {  //★ 有跳词，防重复
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) { // ★ 检测上一字符，防误判英文数字 
                        AddToResult(j, end, index, skipSet, result); // 添加到 result列表
                    }
                }
            }
        }

        private unsafe void Find_3(char* _ptext, int end, int index, bool* skips, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index];         // 获取映射字典
            var tc = _pdict[_ptext[end]];       // 获取未端列表
            if (tc >= 0xfffe) { return; }       // ★ 判断 是否为跳词，如果是跳词，返回
            var next = _pfirst[tc];

            bool skip = false;  // 跳词标记
            for (int j = end - 1; j >= 0; j--) { // ★ 开始为 end-1
                if (skips[j]) { skip = true; continue; } // 获取映射符
                tc = _pdict[_ptext[j]]; // 获取映射符
                if (tc == 0xffff) { skip = true; continue; }   // 跳词标记为true
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }// 出现敏感词内不存在的词 或  无法获取下一字符，返回跳词搜索类型

                if (skip) { //★ 有跳词，防重复
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) {// ★ 检测上一字符，防误判英文数字 
                        AddToResult(j, end, index, skipSet, result);// 添加到 result列表
                    }
                }
            }
        }
        private unsafe void Find_4(char* _ptext, int end, int index, bool* skips, HashSet<uint> skipSet, List<TempWordsResultItem> result
            , int* _pend, int* _pfirst)
        {
            var _pdict = _dicts[index]; // 获取映射字典
            var tc = _pdict[_ptext[end]];// 获取未端列表
            if (tc >= 0xfffe) { return; }// ★ 判断 是否为跳词，如果是跳词，返回
            var next = _pfirst[tc];

            bool skip = false;// 跳词标记
            var len = -1;// 计数变量，-1为初始值，1为可跳过一个，0为计数用完
            for (int j = end - 1; j >= 0; j--) {
                if (skips[j]) { skip = true; continue; }// 跳词
                tc = _pdict[_ptext[j]]; // 获取映射符
                if (tc == 0xffff) { skip = true; continue; } // 跳词标记为true
                if (tc == 0xfffe) { // 判断 记数跳词
                    if (len == -1) { len = 1; skip = true; continue; } 
                    if (len == 0) { return; } // 跳词计数用完 ，返回
                    len--;// 跳词计数-1
                    skip = true;// 跳词标记为true
                    continue;
                }
                // 出现敏感词内不存在的词 或  无法获取下一字符，返回跳词搜索类型
                if (tc == 0 || _nextIndex[next].TryGetValue(tc, ref next) == false) { return; }
                if (len != -1) { len = -1; } // 重置计数跳词

                if (skip) {//★ 有跳词，防重复
                    index = _pend[next];
                    if (index != 0 && CheckPreChar(_ptext, tc, j, _pdict)) { // ★ 检测上一字符，防误判英文数字 
                        AddToResult(j, end, index, skipSet, result); // 添加到 result列表
                    }
                }
            }
        }

        #region TryGet CheckNextChar CheckPreChar AddToResult
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddToResult(int start_key, int end_key, int index, HashSet<uint> set, List<TempWordsResultItem> result)
        {
            //曾试过HashSet<ulong>做为重复过滤集，发现在大文本时性能下降过快，总体性能比不过 HashSet<uint> 。
            uint u = ((uint)start_key << 10) | ((uint)end_key & 0x3ff); // 特征码，
            if (set.Add(u)) {// 添加到 HashSet，防重复
                var r = new TempWordsResultItem(start_key, end_key, GetMatchKeyword(index));
                result.Add(r); // 添加到结果集
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
