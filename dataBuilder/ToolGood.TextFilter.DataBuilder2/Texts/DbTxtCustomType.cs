using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtCustomType")]
    public class DbTxtCustomType
    {
        public int Id { get; set; }
        //public string UUID { get; set; }

        public DbTxtCustomType[] Types { get; set; }
        /// <summary>
        /// 上一级
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 英文
        /// </summary>
        public string NameEn { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 末端
        /// </summary>
        public bool IsLeaf { get; set; }
 
        /// <summary>
        /// 冻结不使用
        /// </summary>
        public bool IsFrozen { get; set; }

        public bool IsCheckTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }



        public DbTxtCustomType()
        {
            //UUID = Guid.NewGuid().ToString();
        }

        public DbTxtCustomType(string name, string nameEn = null, DbTxtCustomType[] types = null, bool isLeaf = false, string commonTypes = null, string comment = null)
        {
            //UUID = Guid.NewGuid().ToString();
            Name = name;
            NameEn = nameEn;
            Types = types;
            IsLeaf = isLeaf;
            Comment = comment;
            CreateTime = DateTime.Now;
        }


    }
}
