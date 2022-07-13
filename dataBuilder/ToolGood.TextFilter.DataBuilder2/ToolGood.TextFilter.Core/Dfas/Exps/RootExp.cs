using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.DFAs
{
    public class RootExp : Exp
    {
        public int LabelIndex;

        public Exp InnerExp;


        internal RootExp(Exp innerExp)
        {
            this.InnerExp = innerExp;
        }


        internal RootExp()
        {

        }

        public override int GetCharExpCount()
        {
            return InnerExp.GetCharExpCount();
        }

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

        public override bool HasRepeat()
        {
            return InnerExp.HasRepeat();
        }

        public override void Reverse()
        {
            InnerExp.Reverse();
        }

        public override void SetFirst(bool[] endKeys, ref int layer)
        {
            InnerExp.SetFirst(endKeys, ref layer);

        }
        public override void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer)
        {
            InnerExp.SetOnlyEnd(onlyEndKeys, once, ref layer);
        }


        internal override void BuildENfa(ENfa nfa)
        {
            InnerExp.BuildENfa(nfa);
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToString(stringBuilder);
            return stringBuilder.ToString();
        }

        protected internal override void ToString(StringBuilder stringBuilder)
        {
            InnerExp.ToString(stringBuilder);
        }

        public override bool EqualExp(Exp exp)
        {
            if (exp is RootExp rootExp) {
                return this.InnerExp.EqualExp(rootExp.InnerExp);
            }
            return false;
        }

        public override bool IsOnlyChars()
        {
            return this.InnerExp.IsOnlyChars();
        }

        public override bool HasInfinite()
        {
            return InnerExp.HasInfinite();
        }
        public override void SetActionFindFalse()
        {
            InnerExp.SetActionFindFalse();
        }


    }
}
