using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.TextFilter.Datas
{
    [Table("TxtCustom")]
    [Index("TxtCustomTypeId")]
    public class DbTxtCustom
    {
        public int Id { get; set; }

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

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TxtCustomTypeId { get; set; }

        /// <summary>
        /// 风险等级：0）正常，1）触线 ，2）危险，3）违规，4）指向性违规
        /// </summary>
        public int RiskLevel { get; set; }

        /// <summary>
        /// 匹配类型： 0、普通匹配，1、匹配前缀，2、匹配整句，3、匹配后缀，
        /// </summary>
        public int MatchType { get; set; }

        /// <summary>
        /// 是否重复词
        /// </summary>
        public int IsRepeatWords { get; set; }

        /// <summary>
        /// 多组敏感词 最大 间隔
        /// </summary>
        public int IntervalWrods { get; set; }

        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }

    }
}
