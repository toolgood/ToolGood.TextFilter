using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.DFAs;

namespace ToolGood.TextFilter.DataBuilder2
{
    public static class ExpHelper
    {
        private const char HIGH_SURROGATE_START = '\uD800';
        private const char HIGH_SURROGATE_END = '\uDBFF';
        private const char LOW_SURROGATE_START = '\uDC00';
        private const char LOW_SURROGATE_END = '\uDFFF';
        private static RegexParser regexParser = new RegexParser();
        public static RootExp BuildExp(string exp, int lableIndex)
        {
            return regexParser.ParseRegex(exp, lableIndex);
        }


        public static Exp BuildReplaceExp(Exp exp, Dictionary<string, List<string>> dict, Dictionary<string, Exp> temp = null
            , HashSet<string> singleSkips = null)
        {
            var list = exp.GetChars();
            return BuildReplaceExp(list, dict, temp, singleSkips);
        }


        private static Exp BuildReplaceExp(List<string> chs, Dictionary<string, List<string>> txtDict, Dictionary<string, Exp> temp
            , HashSet<string> singleSkips = null)
        {
            if (chs.Contains("a")) {
            }
            HashSet<string> charList = new HashSet<string>();
            foreach (var ch in chs) {
                if (txtDict.TryGetValue(ch, out List<string> list)) {
                    foreach (var item in list) {
                        if (singleSkips == null) {
                            charList.Add(item);
                        } else if (singleSkips.Contains(item) == false) {
                            charList.Add(item);
                        }
                    }
                }
            }



            if (charList.Count == 0) { return null; }
            foreach (var ch in chs) { charList.Add(ch); }
            var key = string.Join("|", charList.OrderBy(q => q).ToList());
            if (temp != null && temp.TryGetValue(key, out Exp e)) {
                return e;
            }

            List<string> singleCharList = new List<string>();
            List<string> multiCharList = new List<string>();
            foreach (var s in charList) {
                if (s.Length == 1) {
                    singleCharList.Add(s);
                } else {
                    multiCharList.Add(s);
                }
            }

            Exp result;
            if (multiCharList.Count > 0 && singleCharList.Count == 0) {
                string str = BuildMultiChar(multiCharList);
                result = regexParser.ParseRegex(str, 0);
            } else if (multiCharList.Count > 0) {
                string str = BuildMultiChar(multiCharList);
                var right = regexParser.ParseRegex(str, 0);
                var singleString = BuildString(singleCharList);
                if (singleString.Length > 1) {
                    singleString = "[" + singleString + "]";
                }
                var left = new CharExp(singleString);
                result = new AlternationExp(left, right);
            } else {
                var singleString = BuildString(singleCharList);
                if (singleString.Length > 1) {
                    singleString = "[" + singleString + "]";
                }
                result = new CharExp(singleString);
            }
            if (temp != null) {
                temp[key] = result;
            }
            return result;
        }
        private static string BuildMultiChar(List<string> srcMultiCharList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var multiChar in srcMultiCharList) {
                sb.Append("|");
                foreach (var c in multiChar) {
                    var ch = c.ToString();
                    HashSet<string> charList = new HashSet<string>();

                    charList.Add(ch);
                    List<string> singleCharList = new List<string>();
                    List<string> multiCharList = new List<string>();
                    foreach (var s in charList) {
                        if (s.Length == 1) {
                            singleCharList.Add(s);
                        } else {
                            multiCharList.Add(s);
                        }
                    }

                    if (multiCharList.Count > 0) {
                        sb.Append("(");
                    }
                    if (singleCharList.Count > 1) {
                        sb.Append("[");
                        foreach (var s in singleCharList) { sb.Append(s); }
                        sb.Append("]");
                    } else {
                        sb.Append(ch);
                    }
                    if (multiCharList.Count > 0) {
                        foreach (var s in multiCharList) {
                            sb.Append("|");
                            sb.Append(s);
                        }
                        sb.Append(")");
                    }
                }
            }
            sb.Remove(0, 1);
            return sb.ToString();
        }

        public static Exp SimplifyExp(Exp src)
        {
            if (src is RootExp root) {
                root.InnerExp = SimplifyExp(root.InnerExp);
                return root;
            } else if (src is RepeatExp repeatExp) {
                repeatExp.InnerExp = SimplifyExp(repeatExp.InnerExp);
                return repeatExp;
            } else if (src is AlternationExp alternationExp) {
                var left = SimplifyExp(alternationExp.Left);
                var right = SimplifyExp(alternationExp.Right);
                if (left is CharExp leftChar) {
                    if (right is CharExp rightChar) {
                        HashSet<string> singleCharList = new HashSet<string>();
                        var chs1 = leftChar.GetChars();
                        var chs2 = rightChar.GetChars();
                        foreach (var ch in chs1) { foreach (var c in ch) { singleCharList.Add(c.ToString()); } }
                        foreach (var ch in chs2) { foreach (var c in ch) { singleCharList.Add(c.ToString()); } }
                        var singleString = BuildString(singleCharList);
                        singleString = "[" + singleString + "]";
                        return new CharExp(singleString);
                    }
                }
                alternationExp.Left = left;
                alternationExp.Right = right;
                return alternationExp;
            } else if (src is ConcatenationExp concatenationExp) {
                concatenationExp.Left = SimplifyExp(concatenationExp.Left);
                concatenationExp.Right = SimplifyExp(concatenationExp.Right);
                return concatenationExp;
            }
            return src;
        }


        public static List<Exp> FindExp(Exp src)
        {
            List<Exp> result = new List<Exp>();
            FindExp(src, result);
            return result;
        }
        private static void FindExp(Exp src, List<Exp> result)
        {
            if (src is RootExp rootExp) {
                FindExp(rootExp.InnerExp, result);
            } else if (src is RepeatExp repeatExp) {
                FindExp(repeatExp.InnerExp, result);
            } else if (src is AlternationExp alternationExp) {
                FindExp(alternationExp.Left, result);
                FindExp(alternationExp.Right, result);
            } else if (src is ConcatenationExp concatenationExp) {
                // 多字节 繁体字
                if (concatenationExp.Left is CharExp leftCharExp) {
                    if (leftCharExp.IsOneAndInRange(HIGH_SURROGATE_START, HIGH_SURROGATE_END)) {
                        if (concatenationExp.Right is CharExp rightCharExp) {
                            if (rightCharExp.IsOneAndInRange(LOW_SURROGATE_START, LOW_SURROGATE_END)) {
                                result.Add(src);
                                return;
                            }
                        }
                    }
                }
                FindExp(concatenationExp.Left, result);
                FindExp(concatenationExp.Right, result);
            } else {
                result.Add(src);
            }
        }


        public static Exp ReplaceExpSelf(Exp src, Dictionary<Exp, Exp> normalExpDict = null)
        {
            if (normalExpDict != null && normalExpDict.TryGetValue(src, out Exp exp)) {
                return exp;
            }
            if (src is RootExp root) {
                root.InnerExp = ReplaceExpSelf(root.InnerExp, normalExpDict);
                return root;
            } else if (src is RepeatExp repeatExp) {
                repeatExp.InnerExp = ReplaceExpSelf(repeatExp.InnerExp, normalExpDict);
                return repeatExp;
            } else if (src is AlternationExp alternationExp) {
                alternationExp.Left = ReplaceExpSelf(alternationExp.Left, normalExpDict);
                alternationExp.Right = ReplaceExpSelf(alternationExp.Right, normalExpDict);
                return alternationExp;
            } else if (src is ConcatenationExp concatenationExp) {
                concatenationExp.Left = ReplaceExpSelf(concatenationExp.Left, normalExpDict);
                concatenationExp.Right = ReplaceExpSelf(concatenationExp.Right, normalExpDict);
                return concatenationExp;
            }
            return src;
        }

        public static Exp ReplaceExpSelf(Exp src, Dictionary<Exp, Exp> normalExpDict, HashSet<string> skips)
        {
            if (normalExpDict != null && normalExpDict.TryGetValue(src, out Exp exp)) {
                return exp;
            }
            if (src is RootExp root) {
                root.InnerExp = ReplaceExpSelf(root.InnerExp, normalExpDict, skips);
                return root;
            } else if (src is RepeatExp repeatExp) {
                repeatExp.InnerExp = ReplaceExpSelf(repeatExp.InnerExp, normalExpDict, skips);
                return repeatExp;
            } else if (src is AlternationExp alternationExp) {
                if (alternationExp.Left.IsOnlyChars()) {
                    if (skips.Contains(alternationExp.Left.ToString()) == false) {
                        alternationExp.Left = ReplaceExpSelf(alternationExp.Left, normalExpDict, skips);
                    }
                } else {
                    alternationExp.Left = ReplaceExpSelf(alternationExp.Left, normalExpDict, skips);
                }
                if (alternationExp.Right.IsOnlyChars()) {
                    if (skips.Contains(alternationExp.Right.ToString()) == false) {
                        alternationExp.Right = ReplaceExpSelf(alternationExp.Right, normalExpDict, skips);
                    }
                } else {
                    alternationExp.Right = ReplaceExpSelf(alternationExp.Right, normalExpDict, skips);
                }
                return alternationExp;
            } else if (src is ConcatenationExp concatenationExp) {
                concatenationExp.Left = ReplaceExpSelf(concatenationExp.Left, normalExpDict, skips);
                concatenationExp.Right = ReplaceExpSelf(concatenationExp.Right, normalExpDict, skips);
                return concatenationExp;
            }
            return src;
        }

        public static Exp BuildRepeatWords(Exp src)
        {
            if (src is RootExp rootExp) {
                rootExp.InnerExp = BuildRepeatWords(rootExp.InnerExp);
                return rootExp;
            } else if (src is RepeatExp repeatExp) {
                var inner = BuildRepeatWords(repeatExp.InnerExp);
                if (inner is RepeatExp repeat) {
                    return new RepeatExp(inner, Math.Min(repeat.MinTimes, repeatExp.MinTimes), Math.Max(repeatExp.MaxTimes, repeat.MaxTimes));
                }
                repeatExp.InnerExp = inner;
                return repeatExp;
            } else if (src is AlternationExp alternationExp) {
                alternationExp.Left = BuildRepeatWords(alternationExp.Left);
                alternationExp.Right = BuildRepeatWords(alternationExp.Right);
                return alternationExp;
            } else if (src is ConcatenationExp concatenationExp) {
                if (concatenationExp.Left is CharExp leftCharExp) {
                    if (leftCharExp.IsOneAndInRange(HIGH_SURROGATE_START, HIGH_SURROGATE_END)) {
                        if (concatenationExp.Right is CharExp rightCharExp) {
                            if (rightCharExp.IsOneAndInRange(LOW_SURROGATE_START, LOW_SURROGATE_END)) {
                                return new RepeatExp(new ConcatenationExp(leftCharExp, rightCharExp), 1, int.MaxValue);
                            }
                        }
                    }
                }
                concatenationExp.Left = BuildRepeatWords(concatenationExp.Left);
                concatenationExp.Right = BuildRepeatWords(concatenationExp.Right);
                return concatenationExp;
            }
            return new RepeatExp(src, 1, int.MaxValue);
        }

        private static string BuildString(HashSet<string> singleCharList)
        {
            var list = singleCharList.ToList();
            return BuildString(list);
        }
        private static string BuildString(List<string> list)
        {
            return string.Join("", list);
        }
         

    }
}
