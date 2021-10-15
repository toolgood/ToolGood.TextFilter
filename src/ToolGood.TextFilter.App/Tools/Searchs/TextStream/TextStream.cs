/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter
{
    public class TextStream : ReadStreamBase
    {
        public TextStream(string source) : base(source)
        {
        }
        public TextStream(char[] source) : base(source)
        {
        }



    }
}
