using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;

namespace ToolGood.MultiWords.Dfas.Exps
{
    public class CharExp : Exp
    {
        public List<int> SrcChars;

        public CharExp(List<int> chars)
        {
            SrcChars = chars;
        }


        #region BuildENfa
        internal override void BuildENfa(ENfa nfa)
        {
            nfa.HeadState = ENfa.NewState();
            nfa.TailState = ENfa.NewState();
            nfa.HeadState.Add(nfa.TailState, SrcChars);
        }
        #endregion

        public override void GetSrcChars(List<List<int>> list)
        {
            list.Add(SrcChars);
        }

        public override void Reverse()
        {
        }


    }
}
