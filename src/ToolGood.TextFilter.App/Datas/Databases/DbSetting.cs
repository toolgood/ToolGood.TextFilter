/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("Setting")]
    public class DbSetting 
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Key")]
        public string Key { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        [Column("Comment")]
        public string Comment { get; set; }

        [Column("ModifyTime")]
        public DateTime? ModifyTime { get; set; }
 
    }
}
