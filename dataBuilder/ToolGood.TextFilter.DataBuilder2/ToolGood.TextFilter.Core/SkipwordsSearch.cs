using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter
{
    public partial class SkipwordsSearch
    {
        private ushort _firstMaxChar;
        private ushort[] _dict;// 0 没有该值 1) 跳词, 2+)转成对应key

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _checkLen;
        private Int32[] _failure;


        #region FindAll
        public unsafe void FindAll(string text, HashSet<int> result)
        {
            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_checkLen[0])
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
                        var len = _pcheck[next];
                        if (len > 0) {
                            var end = i;
                            while (len > 0) {
                                var idx = end - len + 1;
                                if (_pdict[idx] == 1) { end--; } else { len--; }
                                result.Add(idx);
                            }
                        }
                    }
                    p = next;
                }
            }
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
