/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    /// <summary>
    /// TempMultiWords采用双向链表结构
    /// </summary>
    class TempMultiWords
    {
        public int Ptr;                 // 指针
        public int NplIndex;            // NPL索引
        public int MaxNextIndex;        // 最大下个NPL索引
        public int ResultIndex;         // 结果索引
        public TempMultiWords Parent;   // 上个节点
        public TempWordsResultItem Item;    // 多组敏感词部分
        public TempMultiWords After;    // 暂存下个节点



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
