using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtExtend")]
    [Index("TxtExtendTypeId")]
    public class DbTxtExtend
    {
        public int Id { get; set; }

        public int TxtExtendTypeId { get; set; }

        public string SrcTxt { get; set; }

        public string TarTxt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }



        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }


        public List<string> GetChars()
        {
            return TarTxt.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

    }
}
