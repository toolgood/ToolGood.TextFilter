using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtContact")]
    public class DbTxtContact
    {
        public int Id { get; set; }
        public int TxtContactTypeId { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public int TextIndex { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

    }
}
