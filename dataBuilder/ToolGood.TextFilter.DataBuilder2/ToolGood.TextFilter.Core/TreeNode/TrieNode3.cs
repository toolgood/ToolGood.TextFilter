using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.TextFilter
{
    public class TrieNode3
    {
        public int Index;
        public int Layer;
        public bool End;
        public char Char;
        public List<int> Results;
        public Dictionary<char, TrieNode3> m_values;
        public TrieNode3 Failure;
        public TrieNode3 Parent;
        public Dictionary<char, TrieNode3> dfa_values;


        public int Ptr = -1;
        public Int32 Next;
        public Int32 minflag = Int32.MaxValue;
        public Int32 maxflag = 0;
        public int NewChar;
        public Dictionary<int, TrieNode3> new_values;
        public TrieNode3 SimplifyFailure;


        public TrieNode3()
        {
            m_values = new Dictionary<char, TrieNode3>();
            dfa_values = new Dictionary<char, TrieNode3>();
            Results = new List<int>();
        }

        public bool ContainsKey(char c)
        {
            return m_values.ContainsKey(c);
        }

        public TrieNode3 Add(char c)
        {
            TrieNode3 node;
            if (m_values.TryGetValue(c, out node)) {
                return node;
            }
            if (minflag > c) { minflag = c; }
            if (maxflag < c) { maxflag = c; }
            node = new TrieNode3();
            node.Parent = this;
            node.Char = c;
            m_values[c] = node;
            return node;
        }

        public TrieNode3 Clone()
        {
            var c = this.Char;
            TrieNode3 node;
            if (m_values.TryGetValue(c, out node)) {
                return node;
            }
            node = new TrieNode3();
            node.Parent = this;
            node.Char = c;
            node.minflag = minflag;
            node.maxflag = maxflag;
            foreach (var item in m_values) {
                node.m_values.Add(item.Key, item.Value);
            }
            foreach (var item in Results) {
                node.Results.Add(item);
            }

            if (minflag > c) { minflag = c; }
            if (maxflag < c) { maxflag = c; }
            m_values[c] = node;
            return node;
        }


        public void SetResults(int index)
        {
            if (End == false) {
                End = true;
            }
            Results.Add(index);
        }

        public void SetNewTrieNode(ushort[] dict)
        {
            NewChar = dict[Char];
            new_values = new Dictionary<int, TrieNode3>();
            foreach (var item in m_values) {
                var c = dict[item.Key];
                if (minflag > c) { minflag = c; }
                if (maxflag < c) { maxflag = c; }
                new_values[c] = item.Value;
            }
            //m_values = null;
        }



        public void SetSimplifyFailure(TrieNode3 root)
        {
            if (new_values == null || new_values.Count == 0) { SimplifyFailure = Failure; return; }
            var fail = Failure;
            var keys = new_values.Keys.ToArray();

            do {
                var tryKeys = fail.new_values.Keys.ToList();
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

        public int GetDensity()
        {
            if (maxflag == 0) return 0;
            var count = new_values.Count;
            var size = maxflag - minflag + 1;
            return (int)(Math.Log(Math.Sqrt(size / (double)count), Math.E) * 7);
            //return (int) (Math.Log(Math.Sqrt(size / (double) count)) / 0.5);
            //return (int) (Math.Log((size / (double) count),Math.E) / 0.5);
            //return (int) (Math.Log(size / (double) count) / 0.5);
            //return (int) Math.Sqrt(size / (double) count);
        }
        public int Maxflag()
        {
            return maxflag;
        }
        public bool IsRankOne()
        {
            return minflag == maxflag;
        }

        public int Rank2(ref Int32 start, bool[] seats, int[] has)
        {
            if (maxflag == 0) return 0;

            var keys = new_values.Select(q => (Int32)q.Key).OrderBy(q => q).ToList();
            while (has[start] != 0) { start++; }

            var next = start;

            while (next < has.Length) {
                if (seats[next] == false) {
                    var isok = true;
                    for (int i = 0; i < keys.Count; i++) {
                        var position = next + keys[i];
                        if (has[position] != 0) {
                            isok = false;
                            break;
                        }
                    }
                    if (isok) {
                        SetSeats(next, seats, has);
                        start += keys.Count / 8;
                        return next;
                    }
                }
                next++;
            }
            throw new Exception("");
        }


        public int Rank(ref Int32 start, bool[] seats, bool[] seats2, int[] has)
        {
            if (maxflag == 0) return 0;
            //if (minflag == maxflag) {
            //    RankOne(ref oneStart, seats, has);
            //    return 0;
            //}
            var keys = new_values.Select(q => (Int32)q.Key).OrderByDescending(q => q).ToList();
            var length = keys.Count - 1;
            int[] moves = new int[keys.Count - 1];
            for (int i = 1; i < keys.Count; i++) {
                moves[i - 1] = maxflag - keys[i];
            }

            while (has[start] != 0) { start++; }
            var s = start < (Int32)minflag ? (Int32)minflag : start;
            var next = s - minflag;
            var e = next + maxflag;
            while (e < has.Length) {
                if (seats2[e] == false && seats[next] == false) {
                    var isok = true;
                    for (int i = 0; i < keys.Count; i++) {
                        var position = next + keys[i];
                        if (has[position] > 0) {
                            for (int j = 0; j < length; j++) {
                                seats2[position + moves[j]] = true;
                            }
                            isok = false;
                            break;
                        }
                    }
                    if (isok) {
                        SetSeats(next, seats, has);
                        start += keys.Count / 2;
                        Array.Clear(seats2, start, e + maxflag - start + 1);
                        return next;
                    }
                }
                next++;
                e++;
            }
            throw new Exception("");
        }

        public void RankOne(ref int start, bool[] seats, int[] has)
        {
            while (has[start] != 0) { start++; }
            var s = start < (Int32)minflag ? (Int32)minflag : start;

            for (Int32 i = s; i < has.Length; i++) {
                if (has[i] == 0) {
                    var next = i - (Int32)minflag;
                    if (seats[next]) continue;
                    if (next == 2000) {

                    }
                    SetSeats(next, seats, has);
                    break;
                }
            }
            start++;
        }


        private void SetSeats(Int32 next, bool[] seats, int[] has)
        {
            Next = next;
            seats[next] = true;
            //Ptr = next;

            foreach (var item in new_values) {
                var position = next + item.Key;
                has[position] = item.Value.Index;
            }

        }
    }

}

