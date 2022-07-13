using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolGood.DFAs
{
    public class Dfa
    {
        public List<DfaState> Items = new List<DfaState>();
        public DfaState NewState()
        {
            DfaState state = new DfaState(Items.Count);
            Items.Add(state);
            return state;
        }


        #region BuildNfa

        public static ENfa BuildENfa(List<Exp> exps)
        {
            ENfa.ItemIndex = 1;
            ENfa topEnfa = new ENfa();
            topEnfa.HeadState = ENfa.NewState();
            topEnfa.TailState = ENfa.NewState();
            for (int i = 0; i < exps.Count; i++) {
                var exp = exps[i];
                ENfa enfa = new ENfa();
                exp.BuildENfa(enfa);
                enfa.TailState.LabelIndex = ((RootExp)exp).LabelIndex;

                topEnfa.HeadState.Add(enfa.HeadState);
                enfa.TailState.Add(enfa.TailState);
            }
            return topEnfa;
        }

        /// <summary>
        /// 根据当前的 NFA 构造 DFA，采用子集构造法。
        /// treeNode
        /// </summary>
        /// <param name="headCnt">头节点的个数。</param>
        public static Dfa BuildDfa2(ENfa eNfa, char[] _dict)
        {
            Dfa dfa = new Dfa();

            Dictionary<int, HashSet<ENfaState>> stateMap = new Dictionary<int, HashSet<ENfaState>>();
            Dictionary<string, int> nfaStateMap = new Dictionary<string, int>();
            Stack<DfaState> stack = new Stack<DfaState>();

            {
                // 添加头节点。
                DfaState head = dfa.NewState();
                head.LabelIndex = new int[0];
                HashSet<ENfaState> headStates = EpsilonClosure(Enumerable.Repeat(eNfa.HeadState, 1));
                stateMap.Add(head.Index, headStates);
                stack.Push(head);
            }
            var idx = 0;

            while (stack.TryPop(out DfaState state)) {
                idx++;

                HashSet<ENfaState> stateSet = stateMap[state.Index];
                var dict = MoveAll(stateSet, _dict);
                var keys = dict.Keys.OrderBy(q => q).ToList();

                foreach (var i in keys) {
                    HashSet<ENfaState> set = dict[i];
                    set = EpsilonClosure(set);
                    var keycount = GetString(set);
                    DfaState newState;
                    if (nfaStateMap.TryGetValue(keycount, out int index)) {
                        newState = dfa.Items[index];
                    } else {
                        // 添加新状态.
                        newState = dfa.NewState();
                        stateMap.Add(newState.Index, set);
                        nfaStateMap.Add(keycount, newState.Index);

                        newState.LabelIndex = set.Where(s => s.LabelIndex != 0).Select(q => q.LabelIndex).OrderBy(s => s).ToArray();
                        stack.Push(newState);
                    }
                    // 添加 DFA 的转移。
                    state.NextChars.Add((char)i);
                    state.NextStates.Add(newState);
                }
                nfaStateMap.Clear();

                keys = null;
                dict = null;
                stateMap.Remove(state.Index);
                stateSet = null;
            }
            return dfa;
        }
        /// <summary>
        /// 根据当前的 NFA 构造 DFA，采用子集构造法。
        /// </summary>
        /// <param name="headCnt">头节点的个数。</param>
        public static Dfa BuildDfa(ENfa eNfa, char[] _dict)
        {
            Dfa dfa = new Dfa();

            Dictionary<int, HashSet<ENfaState>> stateMap = new Dictionary<int, HashSet<ENfaState>>();
            Dictionary<string, int> nfaStateMap = new Dictionary<string, int>();
            Stack<DfaState> stack = new Stack<DfaState>();

            {
                // 添加头节点。
                DfaState head = dfa.NewState();
                head.LabelIndex = new int[0];
                HashSet<ENfaState> headStates = EpsilonClosure(Enumerable.Repeat(eNfa.HeadState, 1));
                stateMap.Add(head.Index, headStates);
                stack.Push(head);
            }
            var idx = 0;

            while (stack.TryPop(out DfaState state)) {
                idx++;

                HashSet<ENfaState> stateSet = stateMap[state.Index];
                var dict = MoveAll(stateSet, _dict);
                var keys = dict.Keys.OrderBy(q => q).ToList();

                foreach (var i in keys) {
                    HashSet<ENfaState> set = dict[i];
                    set = EpsilonClosure(set);
                    var keycount = GetString(set);
                    DfaState newState;
                    if (nfaStateMap.TryGetValue(keycount, out int index)) {
                        newState = dfa.Items[index];
                    } else {
                        // 添加新状态.
                        newState = dfa.NewState();
                        stateMap.Add(newState.Index, set);
                        nfaStateMap.Add(keycount, newState.Index);

                        newState.LabelIndex = set.Where(s => s.LabelIndex != 0).Select(q => q.LabelIndex).OrderBy(s => s).ToArray();
                        stack.Push(newState);
                    }
                    // 添加 DFA 的转移。
                    state.NextChars.Add((char)i);
                    state.NextStates.Add(newState);
                }

                keys = null;
                dict = null;
                stateMap.Remove(state.Index);
                stateSet = null;
            }
            return dfa;
        }



        private static string GetString(HashSet<ENfaState> set)
        {
            bool two = false;
            StringBuilder sb = new StringBuilder();
            foreach (var s in set.OrderBy(q => q.Index)) {
                var item = s.Index;
                if (item <= char.MaxValue) {
                    sb.Append((char)item);
                } else {
                    if (two == false) {
                        sb.Append((char)0);
                        two = true;
                    }
                    sb.Append((char)(item >> 16));
                    sb.Append((char)(item & 0xFFFF));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回指定 NFA 状态集合的 ϵ 闭包。 
        /// </summary>
        /// <param name="states">要获取 ϵ 闭包的 NFA 状态集合。</param>
        /// <returns>得到的 ϵ 闭包。</returns>
        private static HashSet<ENfaState> EpsilonClosure(IEnumerable<ENfaState> states)
        {
            HashSet<ENfaState> set = new HashSet<ENfaState>();
            Stack<ENfaState> stack = new Stack<ENfaState>(states);
            while (stack.TryPop(out ENfaState state)) {
                set.Add(state);
                if (state.EpsilonTransitions == null) { continue; }
                // 这里只需遍历 ϵ 转移。
                int cnt = state.EpsilonTransitions.Count;
                for (int i = 0; i < cnt; i++) {
                    ENfaState target = state.EpsilonTransitions[i];
                    if (set.Add(target)) {
                        stack.Push(target);
                    }
                }
            }
            return set;
        }

        public static Dictionary<char, HashSet<ENfaState>> MoveAll(IEnumerable<ENfaState> states, char[] _dict)
        {
            Dictionary<char, HashSet<ENfaState>> result = new Dictionary<char, HashSet<ENfaState>>();

            foreach (ENfaState state in states) {
                if (state.CharClassTransition == null) {
                    continue;
                }
                foreach (var ch in state.CharClassTransition) {
                    var key = _dict[ch];
                    if (key == 0) {

                    }
                    HashSet<ENfaState> nfaStates;
                    if (result.TryGetValue(key, out nfaStates) == false) {
                        nfaStates = new HashSet<ENfaState>();
                        result.Add(key, nfaStates);
                    }
                    nfaStates.Add(state.CharClassTarget);
                }
            }
            return result;
        }

        #endregion


    }
}

