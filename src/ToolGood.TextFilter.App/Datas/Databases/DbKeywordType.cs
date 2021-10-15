/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using ToolGood.ReadyGo3.Attributes;
using ToolGood.TextFilter.App.Datas.TextFilters;

namespace ToolGood.TextFilter.Datas
{
    [Table("KeywordType")]
    public class DbKeywordType 
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("TypeId")]
        public int TypeId { get; set; }

        [Ignore]
        public string Code { get; set; }

        [Ignore]
        public int ParentId { get; set; }

        [Ignore]
        public string Name { get; set; }

        /// <summary>
        /// 类型：0）必删,会被替换， 1）后台审核，2）通过 
        /// </summary>
        [Column("Level_1_UseType")]
        public byte? Level_1_UseType { get; set; }
        /// <summary>
        /// 类型：0）必删,会被替换， 1）后台审核，2）通过 
        /// </summary>
        [Column("Level_2_UseType")]
        public byte? Level_2_UseType { get; set; }
        /// <summary>
        /// 类型：0）必删,会被替换， 1）后台审核，2）通过 
        /// </summary>
        [Column("Level_3_UseType")]
        public byte? Level_3_UseType { get; set; }

        [Column("UseTime")]
        public bool UseTime { get; set; }

        [Column("StartTime")]
        public DateTime? StartTime { get; set; }

        [Column("EndTime")]
        public DateTime? EndTime { get; set; }


        [Column("AddingTime")]
        public DateTime AddingTime { get; set; }
        [Column("ModifyTime")]
        public DateTime? ModifyTime { get; set; }

        public DbKeywordType()
        {
        }

        public DbKeywordType(KeywordTypeInfo info)
        {
            TypeId = info.Id;
            ParentId = info.ParentId;
            Code = info.Code;
            Name = info.Name;

            Level_1_UseType = 1;
            Level_2_UseType = 0;
            Level_3_UseType = 0;

            UseTime = info.UseTime;
            StartTime = info.StartTime;
            EndTime = info.EndTime;
        }

        //public void Dispose()
        //{
        //    Code = null;
        //    Name = null;
        //    Level_0_UseType = null;
        //    Level_1_UseType = null;
        //    Level_2_UseType = null;
        //    Level_3_UseType = null;
        //    ModifyTime = null;
        //}
    }
}
