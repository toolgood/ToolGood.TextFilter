using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.DFAs
{
    /// <summary>
    /// 正则表达式重复（可以重复上限至下限之间的任意次数）
    /// </summary>
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

        #region ToString
        /// <summary>
        /// 返回当前对象的字符串表示形式。
        /// </summary>
        /// <returns>当前对象的字符串表示形式。</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ToString(builder);
            return builder.ToString();
        }
        protected internal override void ToString(StringBuilder builder)
        {
            if (InnerExp is AlternationExp || InnerExp is ConcatenationExp) {
                builder.Append("(");
                InnerExp.ToString(builder);
                builder.Append(")");
            } else {
                InnerExp.ToString(builder);
            }
            bool skip = false;
            if (MaxTimes == int.MaxValue) {
                if (MinTimes == 0) {
                    builder.Append("*");
                    skip = true;
                } else if (MinTimes == 1) {
                    builder.Append("+");
                    skip = true;
                }
            } else if (MinTimes == 0 && MaxTimes == 1) {
                builder.Append("?");
                skip = true;
            }
            if (!skip) {
                builder.Append("{");
                builder.Append(MinTimes);
                if (MinTimes != MaxTimes) {
                    builder.Append(',');
                    if (MaxTimes != int.MaxValue) {
                        builder.Append(MaxTimes);
                    }
                }
                builder.Append("}");
            }
        }

        #endregion

        #region GetChars SetTarChars
        public override void GetChars(List<string> list)
        {
            InnerExp.GetChars(list);
        }
        public override void GetChars(List<Tuple<string, int>> list, ref int layer)
        {
            InnerExp.GetChars(list, ref layer);
        }
        public override void GetChars(Action<List<char>, int> action, ref int layer)
        {
            InnerExp.GetChars(action, ref layer);
        }

        public override void SetFirst(bool[] endKeys, ref int layer)
        {
            InnerExp.SetFirst(endKeys, ref layer);
        }
        public override void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer)
        {
            InnerExp.SetOnlyEnd(onlyEndKeys, once, ref layer);
        }
        public override int GetCharExpCount()
        {
            return InnerExp.GetCharExpCount();
        }

        #endregion


        public override bool HasRepeat()
        {
            return true;
        }


        public override void Reverse()
        {
            InnerExp.Reverse();
        }

        public override bool EqualExp(Exp exp)
        {
            if (exp is RepeatExp repeatExp) {
                if (MinTimes == repeatExp.MinTimes) {
                    if (MaxTimes == repeatExp.MaxTimes) {
                        return InnerExp.EqualExp(repeatExp.InnerExp);
                    }
                }
            }
            return false;
        }

        public override bool IsOnlyChars()
        {
            return false;
        }

        public override bool HasInfinite()
        {
            return true;
            //return MaxTimes == int.MaxValue;
        }
        public override void SetActionFindFalse()
        {
            InnerExp.SetActionFindFalse();
        }
    }
}

