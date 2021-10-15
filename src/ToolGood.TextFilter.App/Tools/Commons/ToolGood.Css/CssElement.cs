/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ToolGood.Css
{
    public class CssElement
    {
        public string Name;
        public Dictionary<string,string> Attributes;

        public CssElement(string name)
        {
            Name = name;
            Attributes = new Dictionary<string, string>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(string attribute, string value)
        {
            Attributes[attribute] = value;
        }
    }
}
