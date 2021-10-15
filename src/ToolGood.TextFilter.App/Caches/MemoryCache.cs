/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;

namespace ToolGood.TextFilter.Commons
{
    sealed partial class MemoryCache : IDisposable
    {
        private static MemoryCache _memoryCache;
        public static MemoryCache Instance {
            get {
                if (_memoryCache == null) {
                    Init();
                }
                return _memoryCache;
            }
        }

        public static object lockObj = new object();

        public const string Version = "2021.07.18";


        public static void Init()
        {
            lock (lockObj) {
                MemoryCache cache = new MemoryCache();
                cache.InitLicence();
                if (cache.LicenceInfo != null) {
                    cache.InitTextFilter(cache.LicenceInfo);
                    if (cache.LicenceInfo.ImageLicence) {
#if image
                        cache.InitImageFilter(cache.LicenceInfo);
#endif
                    }
                    cache.LoadSettingData();
                }

                if (_memoryCache != null) {
                    _memoryCache.Dispose();
                    _memoryCache = null;
                }
                cache.LicenceInfo.ClearTemp();
                _memoryCache = cache;
                GC.Collect(2);
            }

        }


        public static void Refresh()
        {
            if (Instance != null) {
                lock (lockObj) {
                    Instance.LoadDatabase();
                    Instance.RefreshSettingData();
                }
            }
        }

        public void Dispose()
        {
            Licence_Dispose();
            TextFilterData_Dispose();
            Setting_Dispose();

        }
    }
}
