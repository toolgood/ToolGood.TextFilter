using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtSkipWords")]
    public class DbTxtSkipWords
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string SkipWords { get; set; }

        public string Comment { get; set; }


        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }


    }
}
