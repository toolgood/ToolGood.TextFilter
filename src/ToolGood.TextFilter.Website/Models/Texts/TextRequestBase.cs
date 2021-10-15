/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
namespace ToolGood.TextFilter.Models
{
    public class TextRequestBase : ITextRequest
    {
        public string Txt { get; set; }
        public bool OnlyPosition { get; set; }

        public TextRequestBase(string txt, bool onlyPostion)
        {
            Txt = txt;
            OnlyPosition = onlyPostion;
        }
    }




}
