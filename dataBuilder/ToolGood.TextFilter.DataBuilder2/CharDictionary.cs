using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.DataBuilder2
{
    public class CharDictionary
    {
        private char[] _keys;
        private char[] _values;
        private int last = -1;

        public CharDictionary()
        {

        }
        public CharDictionary(Dictionary<char, char> dict)
        {
            _keys = dict.Keys.OrderBy(q => (int)q).ToArray();
            _values = new char[_keys.Length];

            for (int i = 0; i < _keys.Length; i++) {
                _values[i] = dict[_keys[i]];
            }
            last = _keys.Length - 1;
        }

        public char GetTranslate(char key)
        {
            if (last == -1) { return key; }
            if (_keys[0] == key) { return _values[0]; }
            if (_keys[0] > key) { return key; }
            if (_keys[last] < key) { return key; }
            if (_keys[last] == key) { return _values[last]; }

            var left = 0;
            var right = last;

            while (left <= right) {
                int mid = (left + right) >> 1;
                int d = _keys[mid] - key;
                if (d == 0) {
                    return _values[mid];
                } else if (d > 0) {
                    right = mid - 1;
                } else {
                    left = mid + 1;
                }
            }
            return key;
        }


        public string GetTranslate(string src)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in src) {
                stringBuilder.Append(GetTranslate(item));
            }
            return stringBuilder.ToString();
        }

        public int GetLength()
        {
            return last + 1;
        }

        public char GetKey(int index)
        {
            return _keys[index];
        }

        public char GetValue(int index)
        {
            return _values[index];
        }

        public static CharDictionary Load(BinaryReader br)
        {
            CharDictionary charDictionary = new CharDictionary();
            var len = br.ReadInt32();
            charDictionary._keys = br.ReadChars(len);
            charDictionary._values = br.ReadChars(len);
            charDictionary.last = len - 1;
            return charDictionary;
        }

    }

}
