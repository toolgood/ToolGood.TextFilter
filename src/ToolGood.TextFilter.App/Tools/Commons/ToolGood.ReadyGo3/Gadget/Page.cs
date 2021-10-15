using System;
using System.Collections.Generic;

namespace ToolGood.ReadyGo3
{
    /// <summary>
    /// 页
    /// </summary>
    [Serializable]
    public abstract class Page
    {
        private bool _hasComplete;
        private int _pageStart = -1;
        private int _pageEnd = -1;
        private int _currentPage = 0;
        private int _totalItems = 0;
        private int _pageSize = 0;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get => _currentPage; set { _hasComplete = false; _currentPage = value; } }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalItems { get => _totalItems; set { _hasComplete = false; _totalItems = value; } }

        /// <summary>
        /// 每一页数量
        /// </summary>
        public int PageSize { get => _pageSize; set { _hasComplete = false; _pageSize = value; } }

        /// <summary>
        /// 从1开始
        /// </summary>
        public int PageStart { get { if (_hasComplete == false) { SetShowPage(7); } return _pageStart; } }

        /// <summary>
        /// 结束 包含TotalPages
        /// </summary>
        public int PageEnd { get { if (_hasComplete == false) { SetShowPage(7); } return _pageEnd; } }

        /// <summary>
        /// 用于上下文传输
        /// </summary>
        public object Context { get; set; }

        /// <summary>
        /// 设置显示页
        /// </summary>
        /// <param name="tagSize"></param>
        public void SetShowPage(int tagSize)
        {
            int mod2 = tagSize / 2;
            var end = Math.Min(TotalPages, CurrentPage + mod2);
            var start = Math.Max(1, end - tagSize);
            end = Math.Min(TotalPages, start + tagSize);
            _pageStart = start;
            _pageEnd = end;
            _hasComplete = true;
        }
    }
    /// <summary>
    /// 页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Page<T> : Page
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<T> Items { get; set; }
    }
}