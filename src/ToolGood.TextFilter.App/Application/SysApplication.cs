/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using ToolGood.TextFilter.Commons;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.Application
{
    public static class SysApplication
    {
        #region 获取 异步网址
        public static string GetTextFilterNoticeUrl()
        {
            return MemoryCache.Instance.TextFilterNoticeUrl;
        }

        public static string GetTextReplaceNoticeUrl()
        {
            return MemoryCache.Instance.TextReplaceNoticeUrl;
        }

        #endregion

        #region 设置 异步网址
        public static void SetTextFilterNoticeUrl(string url)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='TextFilterNoticeUrl' ", url, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "TextFilterNoticeUrl",
                            Value = url,
                            ModifyTime = DateTime.Now
                        });
                    }
                    MemoryCache.Instance.TextFilterNoticeUrl = url;
                }
            }
        }

        public static void SetTextReplaceNoticeUrl(string url)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='TextReplaceNoticeUrl' ", url, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "TextReplaceNoticeUrl",
                            Value = url,
                            ModifyTime = DateTime.Now
                        });
                    }
                    MemoryCache.Instance.TextReplaceNoticeUrl = url;
                }
            }
        }

        #endregion

        #region GetSkipword SetSkipword
        public static string GetSkipword()
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var setting = helper.FirstOrDefault<DbSetting>("select * from Setting where Key='skipword'");
                    return setting.Value;
                }
            }
        }
        public static void SetSkipword(string skipword)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='skipword' ", skipword, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "skipword",
                            Value = skipword,
                            ModifyTime = DateTime.Now
                        });
                    }
                }
            }
        }
        #endregion



        #region LoadTextDataError LoadImageFilterError LoadImageClassifyError LoadBrowserFilterError LoadBrowserClassifyError
        public static bool LoadTextDataError()
        {
            return !MemoryCache.Instance.LoadTextFilterSuccess;
        }
        #endregion

        #region Licence
 
        public static bool HasGrpcLicence()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) {
                return info.GrpcLicence;
            }
            return false;
        }
        #endregion
 

        public static void Refresh()
        {
            MemoryCache.Refresh();
        }

        public static void Init()
        {
            MemoryCache.Init();
        }


    }
}
