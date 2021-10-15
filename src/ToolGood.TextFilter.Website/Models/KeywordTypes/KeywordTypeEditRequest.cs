/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.Models
{
    public class KeywordTypeEditRequest
    {
        public int TypeId { get; set; }

        public byte Level_1_UseType { get; set; }
        public byte Level_2_UseType { get; set; }
        public byte Level_3_UseType { get; set; }

        public bool UseTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }


        public KeywordTypeEditRequest()
        {
            Level_1_UseType = 255;
            Level_2_UseType = 255;
            Level_3_UseType = 255;
        }


    }
}
