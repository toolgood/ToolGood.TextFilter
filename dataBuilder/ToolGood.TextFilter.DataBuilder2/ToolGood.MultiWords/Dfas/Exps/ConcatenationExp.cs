using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;

namespace ToolGood.MultiWords.Dfas.Exps
{
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

        public override void GetSrcChars(List<List<int>> list)
        {
            Left.GetSrcChars(list);
            Right.GetSrcChars(list);
        }
 
        public override void Reverse()
        {
            var temp = Left;
            Left = Right;
            Right = temp;

            Left.Reverse();
            Right.Reverse();
        }

    }
}
