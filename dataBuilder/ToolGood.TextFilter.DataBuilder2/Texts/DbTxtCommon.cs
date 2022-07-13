using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtCommon")]
    [Index("TxtCommonTypeId")]
    public class DbTxtCommon
    {
        public int Id { get; set; }
        /// <summary>
        /// 通用ID
        /// </summary>
        public int TxtCommonTypeId { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 自动备注，如用了什么扩展
        /// </summary>
        public string AutoComment { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
