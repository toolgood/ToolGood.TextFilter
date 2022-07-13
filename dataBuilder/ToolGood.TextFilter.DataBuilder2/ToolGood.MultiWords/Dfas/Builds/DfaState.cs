using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.MultiWords.Dfas.Builds
{
    public class DfaState
    {
        public List<int> NextChars;
        public List<DfaState> NextStates;
        public int Index;
        public int[] LabelIndex;

        public DfaState(int index)
        {
            Index = index;
            NextChars = new List<int>();
            NextStates = new List<DfaState>();
        }


        public override bool Equals(object obj)
        {
            if (obj is DfaState state) {
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
