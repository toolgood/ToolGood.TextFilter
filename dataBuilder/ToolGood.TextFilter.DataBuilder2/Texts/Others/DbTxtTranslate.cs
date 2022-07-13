using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{

    [Table("TxtTranslate")]
    public class DbTxtTranslate
    {
        public int Id { get; set; }

        /// <summary>
        /// 1）繁体转简单 正常，2） 英文数字转简体 正常 ，3）表情符号
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 原文本
        /// </summary>
        public string SrcTxt { get; set; }

        /// <summary>
        /// 目标文本
        /// </summary>
        public string TarTxt { get; set; }

        public string Comment { get; set; }


        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
