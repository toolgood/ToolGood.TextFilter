using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    /// <summary>
    /// 通用类型
    /// </summary>
    [Table("TxtCommonType")]
    public class DbTxtCommonType
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

        /// <summary>
        /// 分支
        /// </summary>
        public string SubTypes { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

        public DbTxtCommonType() { }
        public DbTxtCommonType(string name, string comment = null)
        {
            Name = name;
            Comment = comment;
            CreateTime = DateTime.Now;
        }


    }
}
