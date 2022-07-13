using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter
{
    partial class TranslateSearch
    {
        private ushort _firstMaxChar;
        private ushort[] _dict;// 0 没有该值 1) 跳词, 2+)转成对应key
        private ushort[] _bidiDict;// 0 没有该值 1) 跳词, 2+)转成对应key

        private ushort[] _key;
        private Int32[] _next;
        private Int32[] _check;
        private Int32[] _failure;
        private byte[] _len;
        private string[] _keywords;


        public unsafe string Replace(string text)
        {
            Dictionary<int, TempResult> tempDict = new Dictionary<int, TempResult>();

            fixed (ushort* _pdict = &_dict[0])
            fixed (ushort* _pkey = &_key[0])
            fixed (Int32* _pnext = &_next[0])
            fixed (Int32* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;
                for (int i = 0; i < text.Length; i++) {
                    var t1 = text[i];

                    var t = _pdict[t1];
                    if (t == 1) { p = 0; continue; }
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
                            index--;
                            TempResult temp = new TempResult();
                            temp.Length = _len[index];
                            temp.Keyword = _keywords[index];
                            var start = i + 1 - temp.Length;
                            tempDict[start] = temp;
                        }
                    }
                    p = next;
                }
            }
            if (tempDict.Count == 0) {
                return text;
            }
            StringBuilder stringBuilder = new StringBuilder();
            var idx = 0;
            while (idx < text.Length) {
                if (tempDict.TryGetValue(idx, out TempResult temp)) {
                    stringBuilder.Append(temp.Keyword);
                    idx += temp.Length;
                } else {
                    stringBuilder.Append(text[idx]);
                    idx++;
                }
            }
            return stringBuilder.ToString();
        }


        class TempResult
        {
            public int Length { get; set; }
            public string Keyword { get; set; }

        }

        #region Load
        public void Load(BinaryReader br)
        {
            _firstMaxChar = br.ReadUInt16();
            _dict = br.ReadUshortArray();
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
