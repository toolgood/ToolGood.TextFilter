/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.App.Datas.Results
{
    public struct MultiWordsResultItem 
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start;

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End;

        public MultiWordsResultItem(int start, int end)
        {
            Start = start;
            End = end;
        }

        public string GetText(string txt)
        {
            return txt.Substring(Start, End - Start + 1);
        }

        public string GetText2(string txt)
        {
            var start = Start - 5;
            if (start < 0) { start = 0; }
            var end = End + 5;
            if (end > txt.Length) { end = txt.Length - 1; }
            return txt.Substring(start, end - start + 1);

        }
 
    }




}
