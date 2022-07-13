using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.Core
{
    public partial class ACTextFilterSearch
    {
        private ushort _firstMaxChar;
        private int _maxLength;
        private ushort[] _dict;// 0 没有该值 0xffff) 跳词, 1+)转成对应key
        private int[] _first_first;
        private IntDictionary[] _nextIndex_first;
        private int[] _failure;
        private bool[] _check;

        private ushort _azNumMinChar;
        private ushort _azNumMaxChar;
        private int[] _first;
        private IntDictionary[] _nextIndex;
        private int[] _end;


        public unsafe void FindAll3(string text, List<IllegalWordsResultItem> result)
        {
            fixed (ushort* _pdict = &_dict[0])
            fixed (bool* _pcheck = &_check[0])
            fixed (Int32* _pfailure = &_failure[0]) {
                var p = 0;

                for (int i = 0; i < text.Length; i++) {
                    var t1 = text[i];
                    var t = _pdict[t1];
                    if (t <= _firstMaxChar) { p = _first_first[t]; continue; }
                    if (t == (ushort)0xffff) { continue; }//跳词   正常词句  跳词不存在的， 但 中间插 空格是可能的

                    int next = 0;
                    if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                        if (_pcheck[next]) {

                        }
                    } else if (p != 0) {
                        do {
                            p = _pfailure[p];
                            if (_nextIndex_first[p].TryGetValue(t, ref next)) {
                                if (_pcheck[next]) {

                                }
                                break;
                            }
                        } while (p != 0);
                    }
                    p = next;
                }
            }
        }






    }
}
