using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter.DataBuilder2
{
    public class TxtKeywordInfo
    {
        public int Id { get; set; }

        public byte KeywordLength { get; set; }


        public List<TxtCustomInfo> CustomInfos { get; set; } = new List<TxtCustomInfo>();


        public class TxtCustomInfo
        {
            public int TxtCustomId { get; set; }

            public string Text { get; set; }

            /// <summary>
            /// 类型ID
            /// </summary>
            public int TxtCustomTypeId { get; set; }

            /// <summary>
            /// 风险等级：  0）正常，1）触线 ，2）危险，3）违规，4）指向性违规转成   255 默认
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

            public bool IsTime { get; set; }

            public string TimeName { get; set; }

            public List<List<int>> TxtCacheIds { get; set; }
        }

        public TxtKeywordInfoOut ToOut()
        {
            TxtKeywordInfoOut outInfo = new TxtKeywordInfoOut() {
                Id = this.Id,
                KeywordLength = this.KeywordLength,
                RiskLevel = 255,// 默认 无意
            };
            if (CustomInfos.Count > 0) {
                TxtCustomInfo inf = null;
                foreach (var info in CustomInfos) {
                    if (info.Text=="bb") {

                    }
                    if (inf == null) {
                        inf = info;
                    } else if (inf.RiskLevel > info.RiskLevel) {
                        inf = info;
                    } else if (inf.RiskLevel == info.RiskLevel) {
                        if (inf.MatchType != 0 && info.MatchType == 1) {
                            inf = info;
                        } else if (inf.MatchType == 2 && info.MatchType != 2) {
                            inf = info;
                        } else if (inf.IsTime && info.IsTime == false) {
                            inf = info;
                        }
                    }
                }
                outInfo.TxtCustomId = inf.TxtCustomId;
                outInfo.TxtCustomTypeId = inf.TxtCustomTypeId;
                outInfo.RiskLevel = inf.RiskLevel;
                outInfo.MatchType = inf.MatchType;
            }
            return outInfo;
        }

        public TxtKeywordInfo Copy(int id)
        {
            TxtKeywordInfo info = new TxtKeywordInfo();
            info.Id = id;
            info.KeywordLength = KeywordLength;
            foreach (var customInfo in CustomInfos) {
                info.CustomInfos.Add(customInfo);
            }
            return info;
        }

    }

    public class TxtKeywordInfoOut
    {
        public int Id { get; set; }
        public int TxtCustomId { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TxtCustomTypeId { get; set; }

        /// <summary>
        /// 风险等级：  0）正常，1）触线 ，2）危险，3）违规，4）指向性违规转成   255）默认
        /// </summary>
        public int RiskLevel { get; set; }

        /// <summary>
        /// 匹配类型： 0、普通匹配，1、匹配前缀，2、匹配整句，3、匹配后缀，    255）默认
        /// </summary>
        public int MatchType { get; set; }

        public byte KeywordLength { get; set; }


    }

}
