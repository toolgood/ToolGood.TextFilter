namespace ToolGood.TextFilter.Api.Datas.Requests
{
    public class TextFilterAsyncRequest
    {
        /// <summary>
        /// 需要检测的文本
        /// </summary>
        public string Txt { get; set; }

        /// <summary>
        /// 是否跳过 Bidi 字符，默认 false
        /// </summary>
        public bool SkipBidi { get; set; }
        /// <summary>
        /// 只显示位置，不显示匹配文字
        /// </summary>
        public bool OnlyPosition { get; set; }


        /// <summary>
        /// 请求标识，为空时会自动生成
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 异步回调地址 可空
        /// </summary>
        public string Url { get; set; }
        public TextFilterAsyncRequest()
        {
            SkipBidi = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt">需要检测的文本</param>
        /// <param name="skipBidi">是否跳过 Bidi 字符</param>
        /// <param name="onlyPosition">是否只显示位置，不显示匹配文字</param>
        /// <param name="requestId">请求标识，为空时会自动生成</param>
        /// <param name="url">异步回调地址 可空</param>
        public TextFilterAsyncRequest(string txt, bool skipBidi = false, bool onlyPosition = false, string requestId = null, string url = null)
        {
            Txt = txt;
            SkipBidi = skipBidi;
            OnlyPosition = onlyPosition;
            RequestId = requestId;
            Url = url;
        }


#if Async
#if GRPC
        public TextFindAllAsyncGrpcRequest ToGrpcRequest()
        {
            return new TextFindAllAsyncGrpcRequest() {
                Txt = this.Txt,
                SkipBidi = this.SkipBidi,
                OnlyPosition = OnlyPosition,
                Url = this.Url,
                RequestId = this.RequestId
            };
        }
#endif
#endif

    }


}
