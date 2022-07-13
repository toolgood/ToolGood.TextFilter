using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtTranslateType")]
    public class DbTxtTranslateType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

    }
}
