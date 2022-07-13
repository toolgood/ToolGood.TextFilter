using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.DFAs
{
    /// <summary>
    /// 表示正则表达式的并（r|s）
    /// </summary>
    public class AlternationExp : Exp
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


        internal AlternationExp(Exp left, Exp right)
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
            ENfaState head = ENfa.NewState();
            ENfaState tail = ENfa.NewState();

            List<Exp> exps = new List<Exp>();
            Stack<Exp> stack = new Stack<Exp>();
            stack.Push(Left);
            stack.Push(Right);
            while (stack.TryPop(out Exp exp)) {
                if (exp is AlternationExp alternationExp) {
                    stack.Push(alternationExp.Left);
                    stack.Push(alternationExp.Right);
                } else {
                    exps.Add(exp);
                }
            }
            stack = null;

            foreach (var exp in exps) {
                exp.BuildENfa(nfa);
                head.Add(nfa.HeadState);
                nfa.TailState.Add(tail);
            }
            exps = null;

            nfa.HeadState = head;
            nfa.TailState = tail;

            //Left.BuildENfa(nfa);
            //head.Add(nfa.HeadState);
            //nfa.TailState.Add(tail);
            //Right.BuildENfa(nfa);
            //head.Add(nfa.HeadState);
            //nfa.TailState.Add(tail);
            //nfa.HeadState = head;
            //nfa.TailState = tail;
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
            if (Left is ConcatenationExp) {
                builder.Append("(");
                Left.ToString(builder);
                builder.Append(")");
            } else {
                Left.ToString(builder);
            }
            builder.Append('|');
            if (Right is ConcatenationExp) {
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
            layer = oldLayer;
            Right.GetChars(list, ref layer);
            layer = oldLayer;
        }
        public override void GetChars(Action<List<char>, int> action, ref int layer)
        {
            var oldLayer = layer;
            Left.GetChars(action, ref layer);
            layer = oldLayer;
            Right.GetChars(action, ref layer);
            layer = oldLayer;
        }

        public override void SetFirst(bool[] endKeys, ref int layer)
        {
            var oldLayer = layer;
            Left.SetFirst(endKeys, ref layer);
            layer = oldLayer;
            Right.SetFirst(endKeys, ref layer);
            layer = oldLayer;
        }
        public override void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer)
        {
            var oldLayer = layer;
            Left.SetOnlyEnd(onlyEndKeys, once, ref layer);
            layer = oldLayer;
            Right.SetOnlyEnd(onlyEndKeys, once, ref layer);
            layer = oldLayer;
        }

        public override int GetCharExpCount()
        {
            return Math.Max(Left.GetCharExpCount(), Right.GetCharExpCount());
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
            Left.Reverse();
            Right.Reverse();
        }

        public override bool EqualExp(Exp exp)
        {
            if (exp is AlternationExp alternationExp) {
                if (Left.EqualExp(alternationExp.Left)) {
                    return Right.EqualExp(alternationExp.Right);
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

