/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Commons
{
    partial class MemoryCache
    {
        /// <summary>
        /// 异步网址
        /// </summary>
        public string TextFilterNoticeUrl { get; set; }

        /// <summary>
        /// 异步网址
        /// </summary>
        public string TextReplaceNoticeUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageFilterNoticeUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageClassifyNoticeUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageTempPath { get; set; }


        private void Setting_Dispose()
        {
            TextFilterNoticeUrl = null;
            TextReplaceNoticeUrl = null;
            ImageFilterNoticeUrl = null;
            ImageClassifyNoticeUrl = null;
            ImageTempPath = null;
        }

        private void LoadSettingData()
        {
            try {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var textFilterNoticeUrl = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='TextFilterNoticeUrl'");
                    var textReplaceNoticeUrl = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='TextReplaceNoticeUrl'");

                    var imageTempPathSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageTempPath'");
                    var imageFilterNoticeUrlSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageFilterNoticeUrl'");
                    var imageClassifyNoticeUrlSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageClassifyNoticeUrl'");

                    TextFilterNoticeUrl = textFilterNoticeUrl?.Value ?? "";
                    TextReplaceNoticeUrl = textReplaceNoticeUrl?.Value ?? "";
                    ImageTempPath = imageTempPathSetting?.Value ?? "";
                    ImageFilterNoticeUrl = imageFilterNoticeUrlSetting?.Value ?? "";
                    ImageClassifyNoticeUrl = imageClassifyNoticeUrlSetting?.Value ?? "";
                }
            } catch (Exception) { }
        }


        public void RefreshSettingData()
        {
            try {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var types = helper.Select<DbKeywordType>("select * from KeywordType ");
                    var keywordTypes = CustomKeywordType.Build(KeywordTypeInfos, types);

                    var skipwordSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='skipword'");
              

                    var textFilterNoticeUrl = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='TextFilterNoticeUrl'");
                    var textReplaceNoticeUrl = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='TextReplaceNoticeUrl'");

                    var imageTempPathSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageTempPath'");
                    var imageFilterNoticeUrlSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageFilterNoticeUrl'");
                    var imageClassifyNoticeUrlSetting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='ImageClassifyNoticeUrl'");

                    KeywordTypes = keywordTypes;

                    TextFilterNoticeUrl = textFilterNoticeUrl?.Value ?? "";
                    TextReplaceNoticeUrl = textReplaceNoticeUrl?.Value ?? "";
                    ImageTempPath = imageTempPathSetting?.Value ?? "";
                    ImageFilterNoticeUrl = imageFilterNoticeUrlSetting?.Value ?? "";
                    ImageClassifyNoticeUrl = imageClassifyNoticeUrlSetting?.Value ?? "";
                }
            } catch (Exception) { }
        }


    }
}
