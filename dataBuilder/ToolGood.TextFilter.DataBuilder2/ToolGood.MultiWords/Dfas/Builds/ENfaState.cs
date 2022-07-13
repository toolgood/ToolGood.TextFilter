using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.MultiWords.Dfas.Builds
{
    public class ENfaState
    {
        /// <summary>
        /// 字符类的转移对应的字符类列表。
        /// </summary>
        public List<int> CharClassTransition = new List<int>();

        /// <summary>
        /// ϵ 转移的集合。
        /// </summary>
        public List<ENfaState> EpsilonTransitions = new List<ENfaState>();
        /// <summary>
        /// 获取字符类转移的目标状态。
        /// </summary>
        public ENfaState CharClassTarget { get; set; }

        #region 生成时用的
        /// <summary>
        /// 获取当前状态的索引。
        /// </summary>
        public int Index { get; private set; }
        /// <summary>
        /// 获取或设置当前状态的符号索引。
        /// </summary>
        public int LabelIndex { get; set; }

        #endregion

        /// <summary>
        /// 初始化 <see cref="ENfaState"/> 类的新实例。
        /// </summary>
        /// <param name="nfa">包含状态的 NFA。</param>
        /// <param name="index">状态的索引。</param>
        public ENfaState(int index)
        {
            Index = index;
        }

        /// <summary>
        /// 添加一个到特定状态的转移。
        /// </summary>
        /// <param name="state">要转移到的状态。</param>
        /// <param name="ch">转移的字符。</param>
        public void Add(ENfaState state, List<int> ches)
        {
            foreach (var ch in ches) {
                if (CharClassTransition.Contains(ch) == false) {
                    CharClassTransition.Add(ch);
                }
            }
            CharClassTransition = CharClassTransition.OrderBy(q => q).ToList();
            CharClassTarget = state;
        }
 
        /// <summary>
        /// 添加一个到特定状态的 ϵ 转移。
        /// </summary>
        /// <param name="state">要转移到的状态。</param>
        public void Add(ENfaState state)
        {
            EpsilonTransitions.Add(state);
        }

        public override bool Equals(object obj)
        {
            if (obj is ENfaState state) {
                return state.Index == this.Index;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Index;
        }
    }
}
