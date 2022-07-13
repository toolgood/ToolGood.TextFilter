using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ToolGood.TextFilter.DataBuilder2.Datas;

namespace ToolGood.TextFilter.Core
{
    // 分词用的
    public partial class FenciSearch
    {
        private ushort _firstMaxChar;
        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private ushort[] _dict;// 0 没有该值 1) 跳词, 2+)转成对应key

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _check;
        private Int32[] _failure;
        private Int32[][] _guides;


        #region GetMatchKeyword Set_GetMatchKeyword
        private Func<int, TempKeywordInfo> _getKeyword;
        public TempKeywordInfo GetMatchKeyword(int resultIndex)
        {
            return _getKeyword(resultIndex);
        }
        public void Set_GetMatchKeyword(Func<int, TempKeywordInfo> func)
        {
            _getKeyword = func;
        }
        #endregion

        #region FindAll
        public unsafe void FindAll(string text, List<IllegalWordsResultItem> result)
        {
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;

                for (int i = 0; i < text.Length; i++) {
                    var t1 = text[i];

                    var t = _pdict[t1];
                    if (t == 1) { continue; }//跳词   正常词句  跳词不存在的， 但 中间插 空格是可能的
                    if (t <= _firstMaxChar) { p = t; continue; }

                    var next = _pnext[p] + t;
                    bool find = _pkey[next] == t;
                    if (find == false && p != 0) {
                        int fp = p;
                        do {
                            fp = _pfailure[fp];
                            next = _pnext[fp] + t;
                            if (_pkey[next] == t) { find = true; break; }
                            if (fp == 0) { next = 0; break; }
                        } while (true);
                    }

                    if (find) {
                        var index = _pcheck[next];
                        if (index > 0) {
                            foreach (var item in _guides[index]) {
                                var keyInfo = GetMatchKeyword(item);

                                var start = i;
                                var len = keyInfo.Keyword.Length;
                                while (len > 0) {
                                    start--;
                                    if (_pdict[start] != 1) {
                                        len--;
                                    }
                                }

                                var r = new IllegalWordsResultItem(start, i, item, keyInfo);
                                result.Add(r);
                            }
                        }
                    }
                    p = next;
                }
            }
        }

        #endregion

        #region FindAll2
        public void FindAll2(string text, List<IllegalWordsResultItem> result)
        {
            var p = 0;

            var length = text.Length;
            for (int i = 0; i < length; i++) {
                var t1 = text[i];

                var t = _dict[t1];
                if (t == 1) { continue; }//跳词
                if (t <= _firstMaxChar) {
                    p = t;
                    continue;
                }
                var next = _next[p] + t;
                bool find = _key[next] == t;
                if (find == false && p != 0) {
                    find = GetNext(p, t, out next);
                }

                if (find) {
                    var index = _check[next];
                    if (index > 0) {
                        foreach (var item in _guides[index]) {
                            var keyInfo = GetMatchKeyword(item);
                            var r = new IllegalWordsResultItem(i + 1 - keyInfo.Keyword.Length, i, item, keyInfo);
                            result.Add(r);
                        }
                    }
                }
                p = next;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetNext(int p, ushort t, out int next)
        {
            int fp = p;
            do {
                fp = _failure[fp];
                next = _next[fp] + t;
                if (_key[next] == t) {
                    return true;
                }
                if (fp == 0) {
                    next = 0;
                    return false;
                }
            } while (true);
        }

        #endregion


        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
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
