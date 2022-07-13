using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    /// <summary>
    /// 分词
    /// </summary>
    [Table("TxtFenci")]
    public class DbTxtFenci
    {
        public int Id { get; set; }


        public string Text { get; set; }

        /// <summary>
        /// 词频
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 0 中性/未标注 1 普通结束 2 程度结束 
        /// 10-19 褒义
        /// 20-29 贬义
        /// 30-39 程度  4，3，2，0.5，-0.3 和-0.5 -1。
        /// </summary>
        public byte EmotionalColor { get; set; }


        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
