/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.Models
{
    public class KeywordListRequest
    {
        public string Text { get; set; }
        public int Type { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public KeywordListRequest()
        {
            Type = -1;
        }


    }
}
