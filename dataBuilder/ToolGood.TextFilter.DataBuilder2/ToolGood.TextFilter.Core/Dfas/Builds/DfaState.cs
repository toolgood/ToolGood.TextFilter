using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.DFAs
{
    public class DfaState
    {
        public List<char> NextChars;
        public List<DfaState> NextStates;
        public int Index;
        public int[] LabelIndex;

        public DfaState(int index)
        {
            Index = index;
            NextChars = new List<char>();
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

