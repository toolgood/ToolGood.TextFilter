using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;

namespace ToolGood.MultiWords.Dfas.Exps
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

        public override void GetSrcChars(List<List<int>> list)
        {
            Left.GetSrcChars(list);
            Right.GetSrcChars(list);
        }

        public override void Reverse()
        {
            Left.Reverse();
            Right.Reverse();
        }

    }
}
