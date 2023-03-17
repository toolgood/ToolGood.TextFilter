/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;

namespace ToolGood.TextFilter.Commons
{
    public partial class MemoryCache : IDisposable
    {
        private static MemoryCache _memoryCache;
        public static string DataFile = "";
        public static MemoryCache Instance {
            get
            {
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
                cache.LoadTextFilter();

                if (_memoryCache != null) {
                    _memoryCache.Dispose();
                    _memoryCache = null;
                }
                _memoryCache = cache;
                GC.Collect(2);
            }
        }



        public void Dispose()
        {
            TextFilterData_Dispose();

        }
    }
}
