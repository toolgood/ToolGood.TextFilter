/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */

namespace ToolGood.TextFilter
{

    public abstract class ReadStreamBase 
    {
        protected internal char[] Source;
        protected internal int[] Start;
        protected internal int[] End;
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
