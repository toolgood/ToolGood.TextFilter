using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        const string _tempSkipwordsDictPath = "temp/SkipwordsDict.dat";
        const string _tempSkipwordsPath = "temp/Skipwords.dat";
        const string _tempSkipwordsTypePath = "temp/SkipwordsType.dat";


        public void BuildSkipwords(SqlHelper helper)
        {
            var types = helper.Select<DbTxtSkipType>();
            {
                var fs = File.Create(_tempSkipwordsDictPath);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(types.Count);

                foreach (var type in types) {
                    var ids = GetTypes(types, type);
                    var dict2 = GetSkipwords(helper, ids, 0);
                    bw.Write(dict2.Count);
                    foreach (var item in dict2) { bw.Write((ushort)item); }
                    var dict3 = GetSkipwords(helper, ids, 1);
                    bw.Write(dict3.Count);
                    foreach (var item in dict3) { bw.Write((ushort)item); }
                }
                bw.Close();
                fs.Close();
            }
            {
                var translateDict = CreateTranslateDict(helper);
                var fs = File.Create(_tempSkipwordsPath);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(types.Count);

                foreach (var type in types) {
                    var words = helper.Select<string>("select SkipWords from TxtSkipWords  where TypeId=@0 ", type.Id);
                    if (words.Count == 0) {
                        bw.Write((byte)0);
                    } else {
                        bw.Write((byte)1);
                        var ids = GetTypes(types, type);
                        var skips = GetSkipwords(helper, ids);
                        SkipwordsSearch skipwordsSearch = new SkipwordsSearch();
                        skipwordsSearch.SetKeywords(words, translateDict, skips);
                        skipwordsSearch.Save(bw);
                    }
                }

                bw.Close();
                fs.Close();
            }
            BuildSkipwordsType2(helper);
        }
        public void BuildSkipwordsType2(SqlHelper helper)
        {
            var types = helper.Select<DbTxtSkipType>();
            bool[] useSkipOnce = new bool[types.Count];
            byte[] skipTypes = new byte[0x10000];

            for (int i = 1; i < types.Count; i++) {
                var type = types[i];
                var ids = GetTypes(types, type);
                ids.Remove(1);
                var dict2 = GetSkipwords(helper, ids, 0);
                var dict3 = GetSkipwords(helper, ids, 1);
                useSkipOnce[i] = dict3.Count > 0;

                var words = helper.Select<string>("select SkipWords from TxtSkipWords  where TypeId=@0 ", type.Id);
           

                var skip_type = 1 << (i - 1);
                foreach (var item in dict2) {
                    skipTypes[item] = (byte)(skipTypes[item] | skip_type);
                }
                foreach (var item in dict3) {
                    skipTypes[item] = (byte)(skipTypes[item] | skip_type);
                }
                foreach (var w in words) {
                    var c = w.Reverse().First();
                    skipTypes[c] = (byte)(skipTypes[c] | skip_type);
                }
            }

            var fs = File.Create(_tempSkipwordsTypePath);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(skipTypes.Length);
            bw.Write(skipTypes);
            bw.Write(useSkipOnce);

            bw.Close();
            fs.Close();

        }

        public void BuildSkipwordsType2_2(SqlHelper helper)
        {
            var types = helper.Select<DbTxtSkipType>();
            SkipCharDict[] types_Dict = new SkipCharDict[types.Count];
            for (int i = 0; i < types.Count; i++) { types_Dict[i] = new SkipCharDict(i); }
            bool[] useSkipOnce = new bool[types.Count];

            for (int i = 0; i < types.Count; i++) {
                var type = types[i];
                var ids = GetTypes(types, type);
                var txtSkipWords = helper.Select<DbTxtSkipChar>($" where IsDelete=0 and TypeId in ({string.Join(",", ids)})");
                foreach (var item in txtSkipWords) {
                    var chs = GetSkipwords(item);
                    foreach (var ch in chs) {
                        if (item.SkipWordsCount == 0) {
                            types_Dict[i].SetSkip(ch);
                        } else if (item.SkipWordsCount == 1) {
                            types_Dict[i].SetOnceSkip(ch);
                        }
                    }
                    if (item.SkipWordsCount == 1) {
                        useSkipOnce[i] = true;
                    }
                }
                var words = helper.Select<string>("select SkipWords from TxtSkipWords  where TypeId=@0 ", type.Id);
                foreach (var w in words) {
                    var c = w.Reverse().First();
                    //var c = w.First();
                    types_Dict[i].SetPartSkip(c);
                }
            }

            for (int i = 0; i < types.Count; i++) {
                for (int j = 0; j < types.Count; j++) {
                    if (i == j) { continue; }
                    if (types_Dict[i].Contains(types_Dict[j])) {
                        types_Dict[i].ParentIndexs.Add(j);
                    }
                }
            }

            for (int i = 0; i < types.Count; i++) {
                var typeDict = types_Dict[i];
                if (typeDict.ParentIndexs.Count <= 1) { continue; }

                foreach (var item in typeDict.ParentIndexs) {
                    Stack<int> stack = new Stack<int>();
                    stack.Push(item);
                    while (stack.TryPop(out int tempIndex)) {
                        var temp = types_Dict[tempIndex];
                        typeDict.TopParentIndexs.AddRange(temp.ParentIndexs);
                        foreach (var item2 in temp.ParentIndexs) {
                            stack.Push(item2);
                        }
                    }
                }
                typeDict.ParentIndexs.RemoveAll(q => typeDict.TopParentIndexs.Contains(q));
            }

            byte[] skipTypes = new byte[0x10000];

            for (int i = 1; i < types_Dict.Length; i++) {
                var typeDict = types_Dict[i];
                List<SkipCharDict> skipChars = new List<SkipCharDict>();
                foreach (var item in typeDict.ParentIndexs) {
                    skipChars.Add(types_Dict[item]);
                }
                typeDict.SetOutDict(skipChars, skipTypes, i);
            }

            var translateDict = CreateTranslateDict(helper);
            var fs = File.Create(_tempSkipwordsTypePath);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(skipTypes.Length);
            bw.Write(skipTypes);
            bw.Write(useSkipOnce);

            bw.Close();
            fs.Close();

        }



        public class SkipCharDict
        {
            public int Index;
            public List<int> TopParentIndexs;
            public List<int> ParentIndexs;
            public SkipChar[] Chars;

            public SkipCharDict(int index)
            {
                Index = index;
                Chars = new SkipChar[0x10000];
                ParentIndexs = new List<int>();
                TopParentIndexs = new List<int>();
                for (int i = 0; i < 0x10000; i++) {
                    Chars[i] = new SkipChar() { Char = (char)i, };
                }
            }
            public void SetSkip(int ch)
            {
                Chars[ch].Skip = true;
            }
            public void SetOnceSkip(int ch)
            {
                Chars[ch].OnceSkip = true;
            }
            public void SetPartSkip(int ch)
            {
                Chars[ch].PartSkip = true;
            }
            public bool Contains(SkipCharDict dict)
            {
                for (int i = 0; i < 0x10000; i++) {
                    var ch1 = Chars[i];
                    var ch2 = dict.Chars[i];
                    if (ch1.Contains(ch2) == false) {
                        return false;
                    }
                }
                return true;
            }
            public void SetOutDict(SkipCharDict skipCharDict, byte[] dict, byte key)
            {
                for (int i = 0; i < dict.Length; i++) {
                    var skipChar = skipCharDict.Chars[i];
                    var ch = Chars[i];
                    if (skipChar.Contains(ch)) {
                        if (ch.Skip || ch.OnceSkip || ch.PartSkip) {
                            dict[i] = (byte)(dict[i] | key);
                        }
                    }
                }
            }

            public void SetOutDict(List<SkipCharDict> skipCharDicts, byte[] dict, int index)
            {
                var key = GetSkipkey(index);
                for (int i = 0; i < dict.Length; i++) {
                    var find = false;
                    var ch = Chars[i];
                    foreach (var skipCharDict in skipCharDicts) {
                        var skipChar = skipCharDict.Chars[i];
                        if (skipChar.Contains(ch) == false) {
                            find = true;
                        }
                    }
                    if (find) {
                        dict[i] = (byte)(dict[i] | key);
                    }
                }
            }
            private byte GetSkipkey(int index)
            {
                if (index == 1) return 0b1;//1
                if (index == 2) return 0b10;//2
                if (index == 3) return 0b100;//4
                if (index == 4) return 0b1000;//8
                if (index == 5) return 0b10000;//16
                if (index == 6) return 0b100000;//32
                if (index == 7) return 0b1000000;//64
                if (index == 8) return 0b10000000;//128
                return 0b00000000;
            }


        }

        public class SkipChar
        {
            public char Char;
            public bool Skip;
            public bool OnceSkip;
            public bool PartSkip;

            public bool Contains(SkipChar skipChar)
            {
                if (skipChar.GetSkipIndex() == 0) { return true; }
                if (GetSkipIndex() == skipChar.GetSkipIndex()) { return true; }
                if (Skip && OnceSkip && PartSkip) { return true; }

                if (Skip && OnceSkip && PartSkip == false) {
                    if (skipChar.Skip && skipChar.OnceSkip == false && skipChar.PartSkip == false) { return true; }
                    if (skipChar.Skip == false && skipChar.OnceSkip && skipChar.PartSkip == false) { return true; }
                }

                if (Skip && OnceSkip == false && PartSkip) {
                    if (skipChar.Skip && skipChar.OnceSkip == false && skipChar.PartSkip == false) { return true; }
                    if (skipChar.Skip == false && skipChar.OnceSkip == false && skipChar.PartSkip) { return true; }
                }

                if (Skip == false && OnceSkip && PartSkip) {
                    if (skipChar.Skip == false && skipChar.OnceSkip && skipChar.PartSkip == false) { return true; }
                    if (skipChar.Skip == false && skipChar.OnceSkip == false && skipChar.PartSkip) { return true; }
                }

                return false;
            }

            private int GetSkipIndex()
            {
                int i = 0;
                if (Skip) { i += 1; }
                if (OnceSkip) { i += 2; }
                if (PartSkip) { i += 4; }
                return i;
            }

        }


        public ushort[] ReadDict()
        {
            var fs = File.OpenRead(_acRegexSearchDictPath);
            BinaryReader br = new BinaryReader(fs);
            var dict = br.ReadUshortArray();
            br.Close();
            fs.Close();
            return dict;
        }

        public List<int> GetTypes(List<DbTxtSkipType> types, DbTxtSkipType type)
        {
            List<int> result = new List<int>();
            result.Add(type.Id);
            if (string.IsNullOrEmpty(type.SubTypes) == false) {
                var ts = type.SubTypes.Split("|", StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in types) {
                    if (ts.Contains(item.Name)) {
                        result.Add(item.Id);
                    }
                }
            }
            return result;
        }

        public List<char> GetSkipwords(SqlHelper helper, List<int> ids, int count)
        {
            var txtSkipWords = helper.Select<DbTxtSkipChar>($" where IsDelete=0 and TypeId in ({string.Join(",", ids)}) and SkipWordsCount=@0", count);
            HashSet<char> result = new HashSet<char>();
            foreach (var item in txtSkipWords) {
                var sws = GetChars(item.SkipWords);
                var ews = GetChars(item.ExcludeWords);
                sws.RemoveAll(q => ews.Contains(q));

                foreach (var ch in sws) {
                    result.Add(ch);
                }
            }
            return result.ToList();
        }
        public List<char> GetSkipwords(SqlHelper helper, List<int> ids)
        {
            var txtSkipWords = helper.Select<DbTxtSkipChar>($" where IsDelete=0 and TypeId in ({string.Join(",", ids)})");

            HashSet<char> result = new HashSet<char>();
            foreach (var item in txtSkipWords) {
                var sws = GetChars(item.SkipWords);
                var ews = GetChars(item.ExcludeWords);
                sws.RemoveAll(q => ews.Contains(q));

                foreach (var ch in sws) {
                    result.Add(ch);
                }
            }
            return result.ToList();
        }

        public List<char> GetSkipwords(DbTxtSkipChar txtSkipChar)
        {
            var sws = GetChars(txtSkipChar.SkipWords);
            var ews = GetChars(txtSkipChar.ExcludeWords);
            sws.RemoveAll(q => ews.Contains(q));
            return sws;
        }



        public List<char> GetSkipwords(SqlHelper helper, int typeId)
        {
            var txtSkipWords = helper.Select<DbTxtSkipChar>(" where IsDelete=0 and TypeId=@0", typeId);

            HashSet<char> result = new HashSet<char>();
            foreach (var item in txtSkipWords) {
                var sws = GetChars(item.SkipWords);
                var ews = GetChars(item.ExcludeWords);
                sws.RemoveAll(q => ews.Contains(q));

                foreach (var ch in sws) {
                    result.Add(ch);
                }
            }
            var words = helper.Select<string>("select SkipWords from TxtSkipWords  where TypeId=@0 ", typeId);
            words.RemoveAll(q => q.Length > 1);
            words.RemoveAll(q => q.Length == 0);
            foreach (var item in words) {
                result.Add(item[0]);
            }
            return result.ToList();

        }

        private static List<char> GetChars(string str)
        {
            if (string.IsNullOrEmpty(str)) { return new List<char>(); }

            List<char> result = new List<char>();
            if (str.Contains("\\|")) {
                result.Add('|');
                str = str.Replace("\\|", "");
            }
            if (str.Contains("\\-")) {
                result.Add('-');
                str = str.Replace("\\-", "");
            }

            var st = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in st) {
                if (s.Contains("-")) {
                    var sp = s.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    var lastChar = ' ';
                    for (int i = 0; i < sp.Length; i++) {
                        var t = sp[i];
                        List<char> list = new List<char>();
                        GetChars(t, list);
                        if (i > 0) {
                            for (int c = lastChar; c < list[0]; c++) {
                                result.Add((char)c);
                            }
                        }
                        foreach (var item in list) {
                            result.Add(item);
                        }
                        lastChar = list.Last();
                    }
                } else {
                    GetChars(s, result);
                }
            }
            return result;
        }

        private static void GetChars(string str, List<char> result)
        {
            var index = 0;
            while (index < str.Length) {
                var ch = str[index++];
                if (ch == '\\') {
                    ch = str[index++];
                    switch (ch) {
                        case 'u': ch = GetEscapedString(str, ref index); break;
                        case 'X':
                        case 'x': ch = GetAsciiString(str, ref index); break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7': ch = GetOctalString(ch, str, ref index); break;
                        case 'b': ch = '\b'; break;
                        case 'f': ch = '\f'; break;
                        case 'n': ch = '\n'; break;
                        case 'r': ch = '\r'; break;
                        case 't': ch = '\t'; break;
                        case 'v': ch = '\v'; break;
                        default: break;
                    }
                }
                result.Add(ch);
            }
        }
        private static char GetOctalString(char ch, string str, ref int index)
        {
            int num = ch - '0';
            var c = str[index];
            if (c >= '0' && c <= '7') {
                num = (num << 3) + c - '0';
                c = str[index + 1];
                if (c >= '0' && c <= '7') {
                    num = (num << 3) + c - '0';
                    index = index + 2;
                    return (char)num;
                }
                index = index + 1;
                return (char)num;
            }
            return (char)num;
        }
        private static char GetEscapedString(string str, ref int index)
        {
            int tempNum = 0;
            var c = str[index];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 12);
                c = str[index + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += (tempNum << 8);
                    c = str[index + 2];
                    if (TryCharToNumber(c, ref tempNum)) {
                        num += (tempNum << 4);
                        c = str[index + 3];
                        if (TryCharToNumber(c, ref tempNum)) {
                            num += tempNum;
                            index = index + 4;
                            return (char)num;
                        }
                    }
                }
            }
            return 'u';
        }
        private static char GetAsciiString(string str, ref int index)
        {
            int tempNum = 0;
            var c = str[index];
            if (TryCharToNumber(c, ref tempNum)) {
                var num = (tempNum << 4);
                c = str[index + 1];
                if (TryCharToNumber(c, ref tempNum)) {
                    num += tempNum;
                    index = index + 2;
                    return (char)num;
                }
            }
            return 'x';
        }
        private static bool TryCharToNumber(char x, ref int num)
        {
            if ('0' <= x && x <= '9') {
                num = x - '0';
                return true;
            }
            if ('a' <= x && x <= 'f') {
                num = x - 'a' + 10;
                return true;
            }
            if ('A' <= x && x <= 'F') {
                num = x - 'A' + 10;
                return true;
            }
            return false;
        }



    }
}
