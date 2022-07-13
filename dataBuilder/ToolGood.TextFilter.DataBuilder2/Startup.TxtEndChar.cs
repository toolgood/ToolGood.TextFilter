using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    public partial class Startup
    {
        private const string _tempTxtEndChar = "temp/endChar.dat";
        private void BuildTxtEndChar(SqlHelper helper)
        {
            var chs = helper.Select<DbTxtEndChar>("where IsDelete=0");

            HashSet<char> set = new HashSet<char>();
            foreach (var item in chs) {
                GetChar(item.EndChar, 0, item.EndChar.Length, set);
                //set.Add(item.EndChar[0]);
            }

            var fs = File.Open(_tempTxtEndChar, FileMode.Create);
            var bw = new BinaryWriter(fs);


            bw.Write(set.Count);
            foreach (var item in set) {
                bw.Write(item);
            }
            bw.Close();
            fs.Close();
        }

        private void GetChar(string str, int start, int end, HashSet<char> result)
        {
            var index = start;
            while (index < end) {
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


        //private char GetOctalString(char ch, string str, ref int index)
        //{
        //    int num = ch - '0';
        //    var c = str[index];
        //    if (c >= '0' && c <= '7') {
        //        num = (num << 3) + c - '0';
        //        c = str[index + 1];
        //        if (c >= '0' && c <= '7') {
        //            num = (num << 3) + c - '0';
        //            index = index + 2;
        //            return (char)num;
        //        }
        //        index = index + 1;
        //        return (char)num;
        //    }
        //    return (char)num;
        //}
        //private char GetEscapedString(string str, ref int index)
        //{
        //    int tempNum = 0;
        //    var c = str[index];
        //    if (TryCharToNumber(c, ref tempNum)) {
        //        var num = (tempNum << 12);
        //        c = str[index + 1];
        //        if (TryCharToNumber(c, ref tempNum)) {
        //            num += (tempNum << 8);
        //            c = str[index + 2];
        //            if (TryCharToNumber(c, ref tempNum)) {
        //                num += (tempNum << 4);
        //                c = str[index + 3];
        //                if (TryCharToNumber(c, ref tempNum)) {
        //                    num += tempNum;
        //                    index = index + 4;
        //                    return (char)num;
        //                }
        //            }
        //        }
        //    }
        //    return 'u';
        //}
        //private char GetAsciiString(string str, ref int index)
        //{
        //    int tempNum = 0;
        //    var c = str[index];
        //    if (TryCharToNumber(c, ref tempNum)) {
        //        var num = (tempNum << 4);
        //        c = str[index + 1];
        //        if (TryCharToNumber(c, ref tempNum)) {
        //            num += tempNum;
        //            index = index + 2;
        //            return (char)num;
        //        }
        //    }
        //    return 'x';
        //}
        //private bool TryCharToNumber(char x, ref int num)
        //{
        //    if ('0' <= x && x <= '9') {
        //        num = x - '0';
        //        return true;
        //    }
        //    if ('a' <= x && x <= 'f') {
        //        num = x - 'a' + 10;
        //        return true;
        //    }
        //    if ('A' <= x && x <= 'F') {
        //        num = x - 'A' + 10;
        //        return true;
        //    }
        //    return false;
        //}
    }
}
