using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.TextFilter.Api.Datas.Requests
{
    public class TextReplaceRequest
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

        public TextReplaceRequest()
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
        public TextReplaceRequest(string txt, char replaceChar = '*', bool reviewReplace = true, bool contactReplace = true, bool skipBidi = false, bool onlyPosition = false)
        {
            Txt = txt;
            SkipBidi = skipBidi;
            ReplaceChar = replaceChar;
            ReviewReplace = reviewReplace;
            ContactReplace = contactReplace;
            OnlyPosition = onlyPosition;
        }


#if GRPC
        public TextReplaceGrpcRequest ToGrpcRequest()
        {
            return new TextReplaceGrpcRequest() {
                Txt = this.Txt,
                SkipBidi = this.SkipBidi,
                ReplaceChar = this.ReplaceChar,
                ReviewReplace = this.ReviewReplace,
                ContactReplace = this.ContactReplace,
                OnlyPosition = OnlyPosition,

            };
        }
#endif


    }


}
