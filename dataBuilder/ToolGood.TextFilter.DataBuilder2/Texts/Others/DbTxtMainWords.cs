using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    /// <summary>
    /// 主要关键字
    /// </summary>
    [Table("TxtMainWords")]
    public class DbTxtMainWords
    {
        public int Id { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }

}
