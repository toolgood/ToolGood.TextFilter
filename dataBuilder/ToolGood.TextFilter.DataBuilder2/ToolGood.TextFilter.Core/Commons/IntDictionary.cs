using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.Core
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

        public ushort[] Keys {
            get {
                return _keys;
            }
        }

        public int[] Values {
            get {
                return _values;
            }
        }

        public bool TryGetValue(ushort key, ref int value)
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
        public bool TryGetValue2(ushort key, out int value)
        {
            if (last == -1) {
                value = 0;
                return false;
            }
            if (_keys[0] == key) {
                value = _values[0];
                return true;
            } else if (last == 0 || _keys[0] > key) {
                value = 0;
                return false;
            }

            if (_keys[last] == key) {
                value = _values[last];
                return true;
            } else if (_keys[last] < key) {
                value = 0;
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
            value = 0;
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


    public struct IntDictionary_ACTextFilter
    {
        private ushort[] _keys;
        private int[] _values;
        private int last;
        private int _failure;
        private bool _check;

        public IntDictionary_ACTextFilter(ushort[] keys, int[] values, int failure, bool end)
        {
            _keys = keys;
            _values = values;
            last = keys.Length - 1;
            _failure = failure;
            _check = end;
        }
        public IntDictionary_ACTextFilter(Dictionary<ushort, int> dict, int failure, bool end)
        {
            var keys = dict.Select(q => q.Key).OrderBy(q => q).ToArray();
            var values = new int[keys.Length];
            for (int i = 0; i < keys.Length; i++) {
                values[i] = dict[keys[i]];
            }
            _keys = keys;
            _values = values;
            last = keys.Length - 1;
            _failure = failure;
            _check = end;
        }

        public ushort[] Keys {
            get {
                return _keys;
            }
        }

        public int[] Values {
            get {
                return _values;
            }
        }

        public bool TryGetValue(ushort key, ref int value)
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

        public bool IsCheck()
        {
            return _check;
        }

        public int Failure()
        {
            return _failure;
        }

        #region Save
        public void Save(BinaryWriter bw)
        {
            bw.Write(_keys.Length);
            if (_keys.Length > 0) {
                bw.Write(_keys);
                bw.Write(_values);
            }
            bw.Write(_failure);
            bw.Write(_check);
        }
        #endregion

        #region Load
        public static IntDictionary_ACTextFilter Load(BinaryReader br)
        {
            var len = br.ReadInt32();
            if (len == 0) {
                return new IntDictionary_ACTextFilter(new ushort[0], new int[0], 0, false);
            }
            var keys = br.ReadUshortArray();
            var values = br.ReadIntArray();
            var failure = br.ReadInt32();
            var end = br.ReadBoolean();

            return new IntDictionary_ACTextFilter(keys, values, failure, end);
        }
        #endregion
    }

}
