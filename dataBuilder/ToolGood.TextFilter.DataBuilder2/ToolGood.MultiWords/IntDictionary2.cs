using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.TextFilter;

namespace ToolGood.MultiWords
{
    public class IntDictionary2
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

        public int[] Keys {
            get {
                return _keys;
            }
        }

        public int[] Values {
            get {
                return _values;
            }
        }

        public bool TryGetValue(int key, ref int value)
        {
            if (last == -1) {
                return false;
            }
            if (_keys[0] == key) {
                value = _values[0];
                return true;
            } else if (last == 0 || _keys[0] > key) {
                return false;
            }

            if (_keys[last] == key) {
                value = _values[last];
                return true;
            } else if (_keys[last] < key) {
                return false;
            }

            var left = 0;
            var right = last;
            while (left + 1 < right) {
                int mid = (left + right) >> 1;
                int d = _keys[mid] - key;

                if (d == 0) {
                    value = _values[mid];
                    return true;
                } else if (d > 0) {
                    right = mid;
                } else {
                    left = mid;
                }
            }
            return false;
        }

        #region Save
        public void Save(BinaryWriter bw)
        {
            bw.Write(_keys.Length);
            if (_keys.Length > 0) {
                bw.Write(_keys);
                bw.Write(_values);
            }
        }
        #endregion

        #region Load
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
