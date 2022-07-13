using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.DFAs
{
    /// <summary>
    /// 正则表达式的连接（rs）
    /// </summary>
    public class ConcatenationExp : Exp
    {
        /// <summary>
        /// 获取并联的第一个正则表达式。
        /// </summary>
        /// <value>并联的第一个正则表达式。</value>
        public Exp Left;
        /// <summary>
        /// 获取并联的第二个正则表达式。
        /// </summary>
        /// <value>并联的第二个正则表达式。</value>
        public Exp Right;

        internal ConcatenationExp(Exp left, Exp right)
        {
            this.Left = left;
            this.Right = right;
        }

        #region BuildENfa
        /// <summary>
        /// 根据当前的正则表达式构造 NFA。
        /// </summary>
        /// <param name="nfa">要构造的 NFA。</param>
        internal override void BuildENfa(ENfa nfa)
        {
            Left.BuildENfa(nfa);
            ENfaState head = nfa.HeadState;
            ENfaState tail = nfa.TailState;
            Right.BuildENfa(nfa);
            tail.Add(nfa.HeadState);
            nfa.HeadState = head;

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
            if (Left is AlternationExp) {
                builder.Append("(");
                Left.ToString(builder);
                builder.Append(")");
            } else {
                Left.ToString(builder);
            }
            if (Right is AlternationExp) {
                builder.Append("(");
                Right.ToString(builder);
                builder.Append(")");
            } else {
                Right.ToString(builder);
            }
        }

        #endregion

        #region GetChars SetTarChars
        public override void GetChars(List<string> list)
        {
            Left.GetChars(list);
            Right.GetChars(list);
        }
        public override void GetChars(List<Tuple<string, int>> list, ref int layer)
        {
            var oldLayer = layer;
            Left.GetChars(list, ref layer);
            layer++;
            if (Left is RepeatExp exp)
                if (exp.MinTimes == 0)
                    layer = oldLayer;
            Right.GetChars(list, ref layer);
        }
        public override void GetChars(Action<List<char>, int> action, ref int layer)
        {
            var oldLayer = layer;
            Left.GetChars(action, ref layer);
            layer++;
            if (Left is RepeatExp exp)
                if (exp.MinTimes == 0)
                    layer = oldLayer;
            Right.GetChars(action, ref layer);
        }

        public override void SetFirst(bool[] endKeys, ref int layer)
        {
            var oldLayer = layer;
            Left.SetFirst(endKeys, ref layer);
            layer++;
            if (Left is RepeatExp exp)
                if (exp.MinTimes == 0)
                    layer = oldLayer;
            if (layer == 1) {
                Right.SetFirst(endKeys, ref layer);
            }
        }
        public override void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer)
        {
            var oldLayer = layer;
            Left.SetOnlyEnd(onlyEndKeys, false, ref layer);
            layer++;
            if (Left is RepeatExp exp)
                if (exp.MinTimes == 0)
                    layer = oldLayer;
            if (layer == 1) {
                Right.SetOnlyEnd(onlyEndKeys, once, ref layer);
            }
        }

        public override int GetCharExpCount()
        {
            return Left.GetCharExpCount() + Right.GetCharExpCount();
        }


        #endregion

        public override bool HasRepeat()
        {
            if (Left.HasRepeat()) {
                return true;
            }
            return Right.HasRepeat();
        }

        public override void Reverse()
        {
            var temp = Left;
            Left = Right;
            Right = temp;

            Left.Reverse();
            Right.Reverse();
        }

        public override bool EqualExp(Exp exp)
        {
            if (exp is ConcatenationExp alternationExp) {
                if (Left.EqualExp(alternationExp.Left)) {
                    return Right.EqualExp(alternationExp.Right);
                }
            }
            return false;
        }

        public override bool IsOnlyChars()
        {
            if (Left.IsOnlyChars()) {
                if (Right.IsOnlyChars()) {
                    return true;
                }
            }
            return false;
        }
        public override bool HasInfinite()
        {
            if (Left.HasInfinite()) {
                return true;
            }
            return Right.HasInfinite();
        }
        public override void SetActionFindFalse()
        {
            Left.SetActionFindFalse();
            Right.SetActionFindFalse();
        }

    }
}

