/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.Models
{
    public class SystemRequest
    {
        public string TextFilterNoticeUrl { get; set; }
        public string TextReplaceNoticeUrl { get; set; }
        public string ImageFilterNoticeUrl { get; set; }
        public string ImageClassifyNoticeUrl { get; set; }
        public string ImageTempPath { get; set; }

        public string Skipword { get; set; }


    }
}
