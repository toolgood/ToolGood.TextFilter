using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{

    /// <summary>
    /// 跳词
    /// </summary>
    [Table("TxtSkipChar")]
    public class DbTxtSkipChar
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string SkipWords { get; set; }

        public string ExcludeWords { get; set; }

        /// <summary>
        /// 跳过词，0）不计数跳词，1）计数跳词，计数为1
        /// </summary>
        public int SkipWordsCount { get; set; }

        public string Comment { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
