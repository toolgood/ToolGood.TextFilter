/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.App.Datas.Results
{
    /// <summary>
    /// 
    /// </summary>
    public struct SingleWordsResult 
    {

        public SingleWordsResult(int start, int end, int index)
        {
            Start = start;
            End = end;
            Code = "Custom";
            Index = index;
            TypeId = 0;
        }

        public SingleWordsResult(int typeid, int start, int end, string code, int index)
        {
            TypeId = typeid;
            Start = start;
            End = end;
            Code = code;
            Index = index;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Code;
        public int TypeId;

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start;

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End;

        /// <summary>
        /// 索引
        /// </summary>
        public int Index;


        public uint GetHashSet()
        {
            return ((uint)Start << 10) | ((uint)End & 0x3ff);
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
