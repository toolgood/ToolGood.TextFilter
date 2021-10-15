using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.TextFilter.Api.Datas.Requests
{
    public class TextFilterRequest
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


        public TextFilterRequest()
        {
            SkipBidi = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt">需要检测的文本</param>
        /// <param name="skipBidi">是否跳过 Bidi 字符</param>
        /// <param name="onlyPosition">是否只显示位置，不显示匹配文字</param>
        public TextFilterRequest(string txt,bool skipBidi = false, bool onlyPosition = false)
        {
            Txt = txt;
            SkipBidi = skipBidi;
            OnlyPosition = onlyPosition;

        }

#if GRPC
        public TextFindAllGrpcRequest ToGrpcRequest()
        {
            return new TextFindAllGrpcRequest() {
                Txt = this.Txt,
                SkipBidi = this.SkipBidi,
                OnlyPosition = OnlyPosition,

            };
        }

#endif


    }


}
