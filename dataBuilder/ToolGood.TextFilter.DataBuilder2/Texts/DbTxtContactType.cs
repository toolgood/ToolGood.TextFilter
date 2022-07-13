using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtContactType")]
    public class DbTxtContactType
    {
        public int Id { get; set; }
        /// <summary>
        /// 通用类型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
