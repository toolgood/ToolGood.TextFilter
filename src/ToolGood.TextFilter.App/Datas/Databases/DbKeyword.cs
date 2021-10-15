/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("Keyword")]
    public class DbKeyword 
    {
        [Column("Id")]
        public int Id { get; set; }


        [Column("Text")]
        public string Text { get; set; }

        [Column("Type")]
        public byte Type { get; set; }

        [Column("Comment")]
        public string Comment { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; }

        [Column("AddingTime")]
        public DateTime AddingTime { get; set; }

        [Column("ModifyTime")]
        public DateTime? ModifyTime { get; set; }

    }
}
