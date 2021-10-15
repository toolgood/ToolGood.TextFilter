/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
#if Async
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ToolGood.TextFilter.App.Datas.Results;

namespace ToolGood.TextFilter.Application
{
    static public partial class FilterQueueApplication
    {
        private readonly static FilterQueue textQueue = new FilterQueue();

        #region SetFindAllAction
        public static void SetFindAllAction(Action<string, string, IllegalWordsFindAllResult, string, bool> findAllAction)
        {
            textQueue.FindAllAction = findAllAction;
        }
        #endregion

        #region SetReplaceAction
        public static void SetReplaceAction(Action<string, string, IllegalWordsReplaceResult, string, bool> replaceAction)
        {
            textQueue.ReplaceAction = replaceAction;
        }
        #endregion

        #region TextFilter
        public static void TextFilter(string requestId, string url, string txt, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(10, requestId, url, txt, (char)0, false, false, skipBidi, onlyPosition);
        }

        public static void JsonFilter(string requestId, string url, string txt, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(11, requestId, url, txt, (char)0, false, false, skipBidi, onlyPosition);
        }

        public static void HtmlFilter(string requestId, string url, string txt, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(12, requestId, url, txt, (char)0, false, false, skipBidi, onlyPosition);
        }

        public static void MarkdownFilter(string requestId, string url, string txt, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(13, requestId, url, txt, (char)0, false, false, skipBidi, onlyPosition);
        }
        #endregion

        #region TextReplace
        public static void TextReplace(string requestId, string url, string txt, char replaceChar, bool reviewReplace, bool contactReplace, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(20, requestId, url, txt, replaceChar, reviewReplace, contactReplace, skipBidi, onlyPosition);
        }
        public static void JsonReplace(string requestId, string url, string txt, char replaceChar, bool reviewReplace, bool contactReplace, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(21, requestId, url, txt, replaceChar, reviewReplace, contactReplace, skipBidi, onlyPosition);
        }
        public static void HtmlReplace(string requestId, string url, string txt, char replaceChar, bool reviewReplace, bool contactReplace, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(22, requestId, url, txt, replaceChar, reviewReplace, contactReplace, skipBidi, onlyPosition);
        }
        public static void MarkdownReplace(string requestId, string url, string txt, char replaceChar, bool reviewReplace, bool contactReplace, bool skipBidi, bool onlyPosition)
        {
            textQueue.EnqueueMessage(23, requestId, url, txt, replaceChar, reviewReplace, contactReplace, skipBidi, onlyPosition);
        }
        #endregion

    }
    sealed partial class FilterQueue
    {
        #region 属性
        private ConcurrentQueue<FilterQueueRequest> _writeQueue = new ConcurrentQueue<FilterQueueRequest>();
        private ConcurrentQueue<FilterQueueRequest> _readQueue = new ConcurrentQueue<FilterQueueRequest>();
        public Action<string, string, IllegalWordsFindAllResult, string, bool> FindAllAction;
        public Action<string, string, IllegalWordsReplaceResult, string, bool> ReplaceAction;

        private int _isInProcessMessage = 0;
        private const int _maxWriteCount = 1000;
        #endregion

        #region public EnqueueMessage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFilter">
        /// 10）文本检测；11) Json检测；12）Html检测；13）Markdown检测；
        /// 20) 文本替换；21) Json替换；22）Html替换；23）Markdown替换；
        /// </param>
        /// <param name="requestId"></param>
        /// <param name="data"></param>
        /// <param name="deleteTempPath"></param>
        public void EnqueueMessage(byte typeFilter, string requestId, string url, string data, char assistData, bool reviewReplace, bool contactReplace
            , bool skipBidi, bool onlyPosition)
        {
            _writeQueue.Enqueue(new FilterQueueRequest(typeFilter, requestId, url, data, assistData, reviewReplace, contactReplace, skipBidi, onlyPosition));
            if (_writeQueue.Count >= _maxWriteCount) Thread.Sleep(10);
            ProcessMessage();
        }

        private void ProcessMessage()
        {
            bool flag = Interlocked.CompareExchange(ref _isInProcessMessage, 1, 0) == 0;
            if (flag == false) return;

            Task.Factory.StartNew(() => {
                try {
                    if (!_writeQueue.IsEmpty) {
                        #region SwapWriteQueue
                        var tmp = _writeQueue;
                        _writeQueue = _readQueue;
                        _readQueue = tmp;
                        tmp = null;
                        #endregion

                        while (_writeQueue.TryDequeue(out FilterQueueRequest request)) {
                            if (request.TypeFilter <= 19) {
                                TextFilter(request);
                            } else if (request.TypeFilter <= 29) {
                                TextReplace(request);
                            }
                        }
                    }
                } finally {
                    Interlocked.Exchange(ref _isInProcessMessage, 0);
                    if (_writeQueue.Count > 0) {
                        ProcessMessage();
                    }
                }
            });
        }
        #endregion

        #region private TextFilter TextReplace
        private void TextFilter(FilterQueueRequest request)
        {
            try {
                if (FindAllAction != null) {
                    IllegalWordsFindAllResult result;
                    if (request.TypeFilter == 10) {
                        result = TextFilterApplication.FindAll(request.Data, request.SkipBidi);
                    } else if (request.TypeFilter == 11) {
                        result = JsonFilterApplication.FindAll(request.Data, request.SkipBidi);
                    } else if (request.TypeFilter == 12) {
                        result = HtmlFilterApplication.FindAll(request.Data, request.SkipBidi);
                    } else if (request.TypeFilter == 13) {
                        result = MarkdownFilterApplication.FindAll(request.Data, request.SkipBidi);
                    } else {
                        return;
                    }
                    FindAllAction(request.RequestId, request.Url, result, request.Data,request.OnlyPosition);
                }
            } catch { }
        }
        private void TextReplace(FilterQueueRequest request)
        {
            try {
                IllegalWordsReplaceResult result;
                if (ReplaceAction != null) {
                    if (request.TypeFilter == 20) {
                        result = TextFilterApplication.Replace(request.Data, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                    } else if (request.TypeFilter == 21) {
                        result = JsonFilterApplication.Replace(request.Data, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                    } else if (request.TypeFilter == 22) {
                        result = HtmlFilterApplication.Replace(request.Data, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                    } else if (request.TypeFilter == 23) {
                        result = MarkdownFilterApplication.Replace(request.Data, (char)request.ReplaceChar, request.ReviewReplace, request.ContactReplace, request.SkipBidi);
                    } else {
                        return;
                    }
                    ReplaceAction(request.RequestId, request.Url, result, request.Data, request.OnlyPosition);
                }
            } catch { }
        }
        #endregion
    }

    #region FilterQueueRequest
    public struct FilterQueueRequest
    {
        /// <summary>
        /// 过滤类型：
        /// 10）文本检测；11) Json检测；12）Html检测；13）Markdown检测；
        /// 20) 文本替换；21) Json替换；22）Html替换；23）Markdown替换；
        /// </summary>
        public byte TypeFilter { get; private set; }

        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestId { get; private set; }

        /// <summary>
        /// 返回网址
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 文本 或 路径
        /// </summary>
        public string Data { get; private set; }
        /// <summary>
        /// 替换符号
        /// </summary>
        public char ReplaceChar { get; private set; }

        public bool ReviewReplace { get; private set; }
        public bool ContactReplace { get; private set; }

        public bool SkipBidi { get; private set; }
        public bool OnlyPosition { get; private set; }


        public FilterQueueRequest(byte typeFilter, string requestId, string url, string data, char replaceChar, bool reviewReplace, bool contactReplace
            , bool skipBidi, bool onlyPosition)
        {
            TypeFilter = typeFilter;
            SkipBidi = skipBidi;
            RequestId = requestId;
            Url = url;
            Data = data;
            ReplaceChar = replaceChar;
            ReviewReplace = reviewReplace;
            ContactReplace = contactReplace;
            OnlyPosition = onlyPosition;
        }


    }

    #endregion
}

#endif