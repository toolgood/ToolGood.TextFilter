/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToolGood.TextFilter
{
    public struct IntDictionary
    {
        private ushort[] _keys;
        private int[] _values;
        private int last;
        public IntDictionary(ushort[] keys, int[] values)
        {
            _keys = keys;
            _values = values;
            last = keys.Length - 1;
        }
        public IntDictionary(Dictionary<ushort, int> dict)
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

        public bool TryGetValue(ushort key, ref int value)
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
        public static IntDictionary Load(BinaryReader br)
        {
            var len = br.ReadInt32();
            if (len == 0) {
                return new IntDictionary(new ushort[0], new int[0]);
            }
            var keys = br.ReadUshortArray();
            var values = br.ReadIntArray();
            return new IntDictionary(keys, values);
        }
        #endregion
    }
}
