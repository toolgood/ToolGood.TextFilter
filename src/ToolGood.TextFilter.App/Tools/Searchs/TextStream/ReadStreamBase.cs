/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter
{

    public abstract class ReadStreamBase 
    {
        /// <summary>
        /// 源文本字符集
        /// </summary>
        protected internal char[] Source;
        /// <summary>
        /// 保存TestingText映射到Source的开始位置
        /// </summary>
        protected internal int[] Start;
        /// <summary>
        /// 保存TestingText映射到Source的结束位置
        /// </summary>
        protected internal int[] End;
        /// <summary>
        /// 检测的字符集
        /// </summary>
        protected internal char[] TestingText;

        public ReadStreamBase(string source)
        {
            Source = source.ToCharArray();
        }

        public ReadStreamBase(char[] source)
        {
            Source = source;
        }


    }



}
