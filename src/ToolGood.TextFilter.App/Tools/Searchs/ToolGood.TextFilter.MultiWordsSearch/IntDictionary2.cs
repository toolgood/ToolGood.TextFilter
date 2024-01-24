/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    /// <summary>
    ///  优化Dictionary<int,int>类型
    ///  
    /// </summary>
    public struct IntDictionary2
    {
        private int[] _keys;
        private int[] _values;
        private int last;
        public IntDictionary2(int[] keys, int[] values)
        {
            _keys = keys;
            _values = values;
            last = keys.Length - 1;
        }
        public IntDictionary2(Dictionary<int, int> dict)
        {
            var keys = dict.Select(q => q.Key).OrderBy(q => q).ToArray();
            var values = new int[keys.Length];
            for (int i = 0; i < keys.Length; i++) {
                values[i] = dict[keys[i]];
            }
            _keys = keys;
            _values = values;
            last = keys.Length - 1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasNoneKey()
        {
            return last == -1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasKey()
        {
            return last >= 0;
        }

        public bool TryGetValue(int key, ref int value)
        {
            if (last == -1) { return false; }
            if (_keys[0] == key) { value = _values[0]; return true; }
            if (_keys[0] > key) { return false; }
            if (_keys[last] < key) { return false; }
            if (_keys[last] == key) { value = _values[last]; return true; }

            var left = 1;
            var right = last - 1;
            while (left <= right) {
                int mid = (left + right) >> 1;
                int d = _keys[mid] - key;
                if (d == 0) {
                    value = _values[mid];
                    return true;
                } else if (d > 0) {
                    right = mid - 1;
                } else {
                    left = mid + 1;
                }
            }
            return false;
        }



        #region Load
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static IntDictionary2 Load(BinaryReader br)
        {
            var len = br.ReadInt32();
            if (len == 0) {
                return new IntDictionary2(new int[0], new int[0]);
            }
            var keys = br.ReadIntArray();
            var values = br.ReadIntArray();
            return new IntDictionary2(keys, values);
        }
        #endregion
    }

}
