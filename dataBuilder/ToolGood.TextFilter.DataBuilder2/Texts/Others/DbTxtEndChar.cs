using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtEndChar")]
    public class DbTxtEndChar
    {
        public int Id { get; set; }

        public string EndChar { get; set; }
        public string Comment { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

    }
}
