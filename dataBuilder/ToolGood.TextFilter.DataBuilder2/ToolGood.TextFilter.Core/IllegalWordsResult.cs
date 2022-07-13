using System.Collections.Generic;
//using ToolGood.MultiWords;

namespace ToolGood.TextFilter
{
    public class IllegalWordsResult
    {
        /// <summary>
        /// 分词
        /// </summary>
        public bool SuggestParticiple;

        /// <summary>
        /// 有多组
        /// </summary>
        public bool MultiWords;

        /// <summary>
        /// 双重算法
        /// </summary>
        public bool HasBidi;


        public List<IllegalWordsResultItem> Items;

        //public List<IllegalWordsResultMultiItem> MultiItems;


        public IllegalWordsResult()
        {
            Items = new List<IllegalWordsResultItem>();
            //MultiItems = new List<IllegalWordsResultMultiItem>();
        }
    }

}
