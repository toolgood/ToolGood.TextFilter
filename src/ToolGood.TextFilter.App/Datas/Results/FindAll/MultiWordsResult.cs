/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Text;

namespace ToolGood.TextFilter.App.Datas.Results
{
    public struct MultiWordsResult 
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int Index;

        public int TypeId;

        public string Code;

        public MultiWordsResultItem[] Items;


        public MultiWordsResult(int index,int typeId, string code,MultiWordsResultItem[] items)
        {
            Index = index;
            TypeId = typeId;
            Code = code;
            Items = items;
        }

        public string GetHashSet()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Items) {
                sb.Append(item.Start);
                sb.Append('-');
                sb.Append(item.End);
                sb.Append(',');
            }
            return sb.ToString();
        }

    }




}
