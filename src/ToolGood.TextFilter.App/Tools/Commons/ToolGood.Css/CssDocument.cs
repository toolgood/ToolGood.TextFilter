/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ToolGood.Css
{
    public class CssDocument
    {
        public List<CssElement> Elements;

        public CssElement this[string name] {
            get {
                for (int i = 0; i < Elements.Count; i++) {
                    if (((CssElement)Elements[i]).Name.Equals(name))
                        return (CssElement)Elements[i];
                }
                return null;
            }
        }

        public CssDocument()
        {
            Elements = new List<CssElement>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Load(string text)
        {
            CssParser parse = new CssParser();
            var eles = parse.Parse(text);
            Elements.AddRange(eles);
            parse = null;
        }

        public void GetCssNames(List<string> result, string key, string value)
        {
            for (int i = 0; i < Elements.Count; i++) {
                var ele = Elements[i];
                if (ele.Attributes.TryGetValue(key, out string val)) {
                    if (value.Equals(val)) {
                        var sp = ele.Name.Split(',');
                        foreach (var s in sp) {
                            result.Add(s.Trim());
                        }
                    }
                }
            }
        }
        public void GetCssNames(List<string> result, Func<Dictionary<string, string>, bool> func )
        {
            for (int i = 0; i < Elements.Count; i++) {
                var ele = Elements[i];
                if (func(ele.Attributes)) {
                    var sp = ele.Name.Split(',');
                    foreach (var s in sp) {
                        result.Add(s.Trim());
                    }
                }
            }
        }



    }


}
