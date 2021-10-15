/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.Models
{
    public class KeywordEditRequest
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public byte Type { get; set; } = 255;

        public string Comment { get; set; }

    }
}
