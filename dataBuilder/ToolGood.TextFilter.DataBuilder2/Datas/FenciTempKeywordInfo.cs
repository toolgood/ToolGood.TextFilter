using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.DFAs;
using ToolGood.TextFilter.DataBuilder2.Enums;

namespace ToolGood.TextFilter.DataBuilder2.Datas
{
    public class FenciTempKeywordInfo
    {
        public int NewId { get; set; }
        public int Id { get; set; }

        public string Keyword { get; set; }
 
        /// <summary>
        /// 词频
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 0 未标注
        /// 1-7 程度 4，3，2，0.5，-0.3 和-0.5 -1。
        /// 10-19 褒义
        /// 20-29 贬义
        /// 
        /// </summary>
        public byte EmotionalColor { get; set; }


        public override string ToString()
        {
            return Keyword;
        }


        public string ToHashSet()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Keyword.Length.ToString());
            sb.Append(",");
            sb.Append(Count.ToString());
            sb.Append(",");
            sb.Append(EmotionalColor.ToString());
            return sb.ToString();
        }
    }

}
