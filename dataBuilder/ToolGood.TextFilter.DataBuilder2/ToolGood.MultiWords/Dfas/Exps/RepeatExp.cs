using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;

namespace ToolGood.MultiWords.Dfas.Exps
{
    public class RepeatExp : Exp
    {
        #region 公共属性
        /// <summary>
        /// 获取重复多次的内部正则表达式。
        /// </summary>
        /// <value>重复多次的内部正则表达式。</value>
        public Exp InnerExp;
        /// <summary>
        /// 获取内部正则表达式的最少重复次数。
        /// </summary>
        /// <value>内部正则表达式的最少重复次数，这个一个大于等于零的值。</value>
        public int MinTimes;
        /// <summary>
        /// 获取内部正则表达式的最多重复次数。
        /// </summary>
        /// <value>内部正则表达式的最多重复次数，<see cref="System.Int32.MaxValue"/> 
        /// 表示不限制重复次数。</value>
        public int MaxTimes;
        #endregion

        internal RepeatExp(Exp innerExp, int minTimes, int maxTimes)
        {
            this.InnerExp = innerExp;
            this.MinTimes = minTimes;
            this.MaxTimes = maxTimes;
        }

        #region BuildENfa

        /// <summary>
        /// 根据当前的正则表达式构造 NFA。
        /// </summary>
        /// <param name="nfa">要构造的 NFA。</param>
        internal override void BuildENfa(ENfa nfa)
        {
            ENfaState head = ENfa.NewState();
            ENfaState tail = ENfa.NewState();
            ENfaState lastHead = head;
            // 如果没有上限，则需要特殊处理。
            int times = MaxTimes == int.MaxValue ? MinTimes : MaxTimes;
            if (times == 0) {
                // 至少要构造一次。
                times = 1;
            }
            for (int i = 0; i < times; i++) {
                InnerExp.BuildENfa(nfa);
                lastHead.Add(nfa.HeadState);
                if (i >= MinTimes) {
                    // 添加到最终的尾状态的转移。
                    lastHead.Add(tail);
                }
                lastHead = nfa.TailState;
            }
            // 为最后一个节点添加转移。
            lastHead.Add(tail);
            // 无上限的情况。
            if (MaxTimes == int.MaxValue) {
                // 在尾部添加一个无限循环。
                nfa.TailState.Add(nfa.HeadState);
            }
            nfa.HeadState = head;
            nfa.TailState = tail;

        }

        #endregion

        public override void GetSrcChars(List<List<int>> list)
        {
            InnerExp.GetSrcChars(list);
        }


        public override void Reverse()
        {
            InnerExp.Reverse();

        }

    }
}
