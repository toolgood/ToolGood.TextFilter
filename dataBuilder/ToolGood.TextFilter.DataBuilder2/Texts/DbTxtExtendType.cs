using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtExtendType")]
    public class DbTxtExtendType
    {
        public int Id { get; set; }

        /// <summary>
        /// 扩展类型
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public bool IsSystem { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

        public DbTxtExtendType() { }
  

    }
}
