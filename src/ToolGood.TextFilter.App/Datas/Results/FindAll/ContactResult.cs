﻿/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter.App.Datas.Results
{
    /// <summary>
    /// 联系人
    /// </summary>
    public struct ContactResult
    {
        /// <summary>
        /// 是否设置
        /// </summary>
        internal bool IsSet; 
        /// <summary>
        /// 联系方式类型： 0) 手机号, 1) QQ号, 2) 微信号, 3) 微博号，4）Q群号
        /// </summary>
        public int ContactType;

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start;

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End;

        public ContactResult(int contactType, int start, int end)
        {
            ContactType = contactType;
            Start = start;
            End = end;
            IsSet = true;
        }

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
            if (end > txt.Length) { end = txt.Length; }
            return txt.Substring(start, end - start + 1);
        }


    }
}
