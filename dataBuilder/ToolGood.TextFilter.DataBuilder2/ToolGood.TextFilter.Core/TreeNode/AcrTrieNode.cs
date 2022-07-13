using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.DFAs;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// AhoCorasickRegexSearch 的 TrieNode
    /// </summary>
    public class AcrTrieNode
    {
        public int Index;
        public bool End;
        public List<int> Results = new List<int>();
        public Dictionary<char, AcrTrieNode> Nodes = new Dictionary<char, AcrTrieNode>();

        //public List<char> NodeChars = new List<char>();
        //public List<AcrTrieNode> NodeValues = new List<AcrTrieNode>();

        public Int32 Next;
        public Int32 minflag = Int32.MaxValue;
        public Int32 maxflag = -1;

        public List<int> OldResults;




        public void Add(char c, AcrTrieNode node)
        {
            Nodes[c] = node;
            //NodeChars.Add(c);
            //NodeValues.Add(node);
        }
        public int GetDensity()
        {
            if (maxflag == 0) return 0;
            var count = Nodes.Count;
            //var count = NodeChars.Count;
            var size = maxflag - minflag + 1;
            return (int)(Math.Log(Math.Sqrt(size / (double)count), Math.E) * 7);
        }

        public void SetNewChar()
        {
            foreach (var c in Nodes.Keys) {
                if (minflag > c) { minflag = c; }
                if (maxflag < c) { maxflag = c; }
            }
            //foreach (var c in NodeChars) {
            //    if (minflag > c) { minflag = c; }
            //    if (maxflag < c) { maxflag = c; }
            //}
        }
        public int Maxflag()
        {
            return maxflag;
        }
        public bool IsRankOne()
        {
            return minflag == maxflag;
        }
        public void Rank(ref Int32 start, bool[] seats, bool[] seats2, int[] has)
        {
            if (maxflag == -1) { return; }
            //var keys = NodeChars;
            var keys = Nodes.Keys.ToList();

            int[] moves = new int[keys.Count - 1];
            for (int i = 1; i < keys.Count; i++) { moves[i - 1] = maxflag - keys[i]; }

            while (has[start] != 0) { start++; }
            //var s = start < (Int32)minflag ? (Int32)minflag : start;
            var next = start - minflag;
            if (next < 0) { next = 0; }
            var e = next + maxflag;
            while (e < has.Length) {
                if (seats2[e] == false && seats[next] == false) {
                    var isok = true;
                    for (int i = 0; i < keys.Count; i++) {
                        var position = next + keys[i];
                        if (has[position] != 0) {
                            for (int j = 0; j < moves.Length; j++) {
                                seats2[position + moves[j]] = true;
                            }
                            isok = false;
                            break;
                        }
                    }
                    if (isok) {
                        SetSeats(next, seats, has);
                        start += keys.Count / 4;
                        Array.Clear(seats2, start, e + maxflag - start + 1);
                        return;
                    }
                }
                next++;
                e++;
            }
            throw new Exception("");
        }

        public void SetSeats(Int32 next, bool[] seats, int[] has)
        {
            Next = next;
            seats[next] = true;

            foreach (var item in Nodes) {
                var position = next + item.Key;
                has[position] = item.Value.Index;
            }

            //for (int i = 0; i < NodeChars.Count; i++) {
            //    var position = next + NodeChars[i];
            //    has[position] = NodeValues[i].Index;
            //}
        }




        #region MyRegion
        public AcrTrieNode Parent;
        public AcrTrieNode Failure;
        public AcrTrieNode SimplifyFailure;

        public static int SetFailure(List<AcrTrieNode> list)
        {
            foreach (var item in list) {
                foreach (var (ch, val) in item.Nodes) {
                    if (val.Parent == null) {
                        val.Parent = item;
                    } else if (val.Parent == item) {
                    } else {
                    }
                }
            }

            var _root = list[0];
            HashSet<Tuple<char, int>> nodes = new HashSet<Tuple<char, int>>();
            foreach (var (_, value) in _root.Nodes) {
                value.Failure = _root;
                foreach (var (k, v) in value.Nodes) {
                    nodes.Add(Tuple.Create(k, v.Index));
                }
            }
            var length = 1;
            while (nodes.Count != 0) {
                HashSet<Tuple<char, int>> newNodes = new HashSet<Tuple<char, int>>();
                length++;
                foreach (var (c, index) in nodes) {
                    var nd = list[index];
                    var r = nd.Parent.Failure;

                    while (r != null && !r.Nodes.ContainsKey(c)) r = r.Failure;
                    if (r == null)
                        nd.Failure = _root;
                    else {
                        nd.Failure = r.Nodes[c];
                        if (nd.Failure.End) { nd.End = true; }
                    }
                    foreach (var (k, v) in nd.Nodes) {
                        newNodes.Add(Tuple.Create(k, v.Index));
                    }
                }
                nodes = newNodes;
            }
            _root.Failure = _root;
            return length;
        }

        public void SetSimplifyFailure(AcrTrieNode root)
        {
            if (Nodes == null || Nodes.Count == 0) { SimplifyFailure = Failure; return; }
            var fail = Failure;
            var keys = Nodes.Keys.ToArray();

            do {
                var tryKeys = fail.Nodes.Keys.ToList();
                foreach (var key in tryKeys) {
                    if (keys.Contains(key) == false) {
                        break;
                    }
                }
                if (fail.Failure == root) { break; }
                fail = fail.Failure;
            } while (true);

            SimplifyFailure = fail;
        }

        #endregion


    }
}
