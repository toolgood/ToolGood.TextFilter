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

        public static string GetImageFilterNoticeUrl()
        {
            return MemoryCache.Instance.ImageFilterNoticeUrl;
        }

        public static string GetImageClassifyNoticeUrl()
        {
            return MemoryCache.Instance.ImageClassifyNoticeUrl;
        }

        public static string GetImageTempPath()
        {
            return MemoryCache.Instance.ImageTempPath;
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

        public static void SetImageFilterNoticeUrl(string url)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='ImageFilterNoticeUrl' ", url, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "ImageFilterNoticeUrl",
                            Value = url,
                            ModifyTime = DateTime.Now
                        });
                    }
                    MemoryCache.Instance.ImageFilterNoticeUrl = url;
                }
            }
        }

        public static void SetImageClassifyNoticeUrl(string url)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='ImageClassifyNoticeUrl' ", url, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "ImageClassifyNoticeUrl",
                            Value = url,
                            ModifyTime = DateTime.Now
                        });
                    }
                    MemoryCache.Instance.ImageClassifyNoticeUrl = url;
                }
            }
        }

        public static void SetImageTempPath(string path)
        {
            lock (MemoryCache.lockObj) {
                using (var helper = ConfigUtil.GetSqlHelper()) {
                    var count = helper.Execute("update Setting set Value=@0,ModifyTime=@1 where Key='ImageTempPath' ", path, DateTime.Now);
                    if (count == 0) {
                        helper.Insert(new DbSetting() {
                            Key = "ImageTempPath",
                            Value = path,
                            ModifyTime = DateTime.Now
                        });
                    }
                    MemoryCache.Instance.ImageTempPath = path;
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
#if image
        public static bool LoadImageFilterError()
        {
            return !MemoryCache.Instance.LoadTextFilterSuccess;
        }
#if browser
        public static bool LoadImageClassifyError()
        {
            return !MemoryCache.Instance.LoadImageClassifySuccess;
        }
        public static bool LoadBrowserFilterError()
        {
            return !MemoryCache.Instance.LoadBrowserFilterSuccess;
        }
        public static bool LoadBrowserClassifyError()
        {
            return !MemoryCache.Instance.LoadBrowserClassifySuccess;
        }
#endif
#endif
        #endregion

        #region Licence

        public static bool IsRegister()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            return info != null;
        }
        public static string GetRegister()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) { return info.Phone; }
            return "";
        }
        public static DateTime? ServiceStart()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) { return info.RegisterTime; }
            return null;
        }
        public static DateTime? ServiceEnd()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) { return info.ServiceEnd; }
            return null;
        }
        public static string GetLicenceTxt()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) { return info.LicenceTxt; }
            return null;
        }
        public static bool UpdateLicence(string txt)
        {
            return MemoryCache.Instance.UpdateLicence(txt);
        }

        public static bool HasImageLicence()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) {
                return info.ImageLicence;
            }
            return false;
        }
        public static bool HasBrowserLicence()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) {
                return info.BrowserLicence;
            }
            return false;
        }
        public static bool HasGrpcLicence()
        {
            var info = MemoryCache.Instance.LicenceInfo;
            if (info != null) {
                return info.GrpcLicence;
            }
            return false;
        }
        #endregion


        public static string GetMachineCode()
        {
            return MachineCodeUtil.GetMachineCode();
        }

        public static void Refresh()
        {
            MemoryCache.Refresh();
        }

        public static void Init()
        {
            MemoryCache.Init();
        }

        public static string GetVersion()
        {
            return MemoryCache.Version;
        }



    }
}
