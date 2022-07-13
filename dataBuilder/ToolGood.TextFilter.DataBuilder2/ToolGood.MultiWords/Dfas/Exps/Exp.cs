using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.MultiWords.Dfas.Builds;

namespace ToolGood.MultiWords.Dfas.Exps
{
    public abstract class Exp
    {
        /// <summary>
        /// 标签
        /// </summary>
        public int LabelIndex;

        #region BuildENfa
        /// <summary>
        /// 根据当前的正则表达式构造 NFA。
        /// </summary>
        /// <param name="nfa">要构造的 NFA。</param>
        internal abstract void BuildENfa(ENfa nfa);
        #endregion

        public List<List<int>> GetSrcChars()
        {
            List<List<int>> list = new List<List<int>>();
            this.GetSrcChars(list);
            return list;
        }
        public abstract void GetSrcChars(List<List<int>> list);


        public abstract void Reverse();



    }
}
