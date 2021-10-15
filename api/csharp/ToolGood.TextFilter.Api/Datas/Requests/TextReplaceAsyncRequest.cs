namespace ToolGood.TextFilter.Api.Datas.Requests
{
    public class TextReplaceAsyncRequest
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
        /// 替换符号, 默认 星号
        /// </summary>
        public char ReplaceChar { get; set; }
        /// <summary>
        /// 人工审核替换，默认true
        /// </summary>
        public bool ReviewReplace { get; set; }
        /// <summary>
        /// 联系字符串替换，默认true
        /// </summary>
        public bool ContactReplace { get; set; }
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


        public TextReplaceAsyncRequest()
        {
            ReviewReplace = true;
            ContactReplace = true;
            SkipBidi = false;
            ReplaceChar = '*';
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt">需要检测的文本</param>
        /// <param name="replaceChar">替换符号, 默认 星号</param>
        /// <param name="reviewReplace">人工审核替换，默认true</param>
        /// <param name="contactReplace">联系字符串替换，默认true</param>
        /// <param name="skipBidi">是否跳过 Bidi 字符</param>
        /// <param name="onlyPosition">只显示位置，不显示匹配文字</param>
        /// <param name="requestId">请求标识，为空时会自动生成</param>
        /// <param name="url">异步回调地址 可空</param>
        public TextReplaceAsyncRequest(string txt, char replaceChar = '*', bool reviewReplace = true, bool contactReplace = true, bool skipBidi = false, bool onlyPosition = false, string requestId = null, string url = null)
        {
            Txt = txt;
            SkipBidi = skipBidi;
            ReplaceChar = replaceChar;
            ReviewReplace = reviewReplace;
            ContactReplace = contactReplace;
            OnlyPosition = OnlyPosition;
            RequestId = requestId;
            Url = url;
        }



#if Async
#if GRPC
        public TextReplaceAsyncGrpcRequest ToGrpcRequest()
        {
            return new TextReplaceAsyncGrpcRequest() {
                Txt = this.Txt,
                SkipBidi = this.SkipBidi,
                ReplaceChar = this.ReplaceChar,
                ReviewReplace = this.ReviewReplace,
                ContactReplace = this.ContactReplace,
                OnlyPosition = OnlyPosition,
                Url = this.Url,
                RequestId = this.RequestId
            };
        }
#endif
#endif

    }


}
