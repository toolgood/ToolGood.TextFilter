/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    class TempMultiWords
    {
        public int Ptr;    
        public int NplIndex;  
        public int MaxNextIndex; 

        public int ResultIndex; 
        public TempMultiWords Parent;
        public TempWordsResultItem Item;

        public TempMultiWords After;
 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearAll()
        {
            var temp = After;
            while (temp != null) {
                var after = temp.After;
                temp.After = null;
                temp = after;
            }
        }

    }

}
