/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolGood.Css
{
    public class CssParser
    {
        private string _source;
        private int _idx;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EatWhiteSpace()
        {
            while (_idx < _source.Length) {
                var ch = GetCurrentChar();
                if ("\t\n\r ".IndexOf(ch) == -1)
                    return;
                _idx++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ParseElementName()
        {
            StringBuilder element = new StringBuilder();
            EatWhiteSpace();
            while (_idx < _source.Length) {
                if (GetCurrentChar() == '{') {
                    _idx++;
                    break;
                }
                element.Append(GetCurrentChar());
                _idx++;
            }
            EatWhiteSpace();
            return element.ToString().Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ParseAttributeName()
        {
            StringBuilder attribute = new StringBuilder();
            EatWhiteSpace();
            while (_idx < _source.Length) {
                var ch = GetCurrentChar();
                if (ch == ':') {
                    _idx++;
                    break;
                }
                attribute.Append(ch);
                _idx++;
            }

            EatWhiteSpace();
            return attribute.ToString().Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ParseAttributeValue()
        {
            StringBuilder attribute = new StringBuilder();
            EatWhiteSpace();
            while (_idx < _source.Length) {
                var ch = GetCurrentChar();
                if (ch == ';') {
                    _idx++;
                    break;
                } else if (ch == '}') {
                    break;
                }
                attribute.Append(ch);
                _idx++;
            }
            EatWhiteSpace();
            return attribute.ToString().Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char GetCurrentChar()
        {
            return _source[_idx];
        }


        static Regex reg = new Regex(@"(/\*.*?\*/|[ \r\n\t])+", RegexOptions.Compiled);
        static Regex reg2 = new Regex(@"([^\{\}]+)\{([^\{\}]+\{)|(\} ?\})", RegexOptions.Compiled);
        public List<CssElement> Parse(string text)
        {
            var css1 = reg.Replace(text, " ");
            _source = reg2.Replace(css1, new MatchEvaluator(m => { return m.Groups[2].Success ? m.Groups[2].Value : "}"; }));
            _idx = 0;

            List<CssElement> elements = new List<CssElement>();
            while (_idx < _source.Length) {
                string elementName = ParseElementName();
                if (string.IsNullOrEmpty(elementName)) break;

                CssElement element = new CssElement(elementName);
                string name = ParseAttributeName();
                string value = ParseAttributeValue();

                while (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(value) == false) {
                    element.Add(name, value);
                    EatWhiteSpace();
                    if (GetCurrentChar() == '}') {
                        _idx++;
                        break;
                    }
                    name = ParseAttributeName();
                    value = ParseAttributeValue();
                }
                elements.Add(element);
            }
            return elements;
        }

        public Dictionary<string,string> ParseStyle(string text)
        {
            _source = reg.Replace(text, " ");
            _idx = 0;

            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            while (_idx < _source.Length) {
                string name = ParseAttributeName();
                string value = ParseAttributeValue();
                while (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(value) == false) {
                    result[name] = value;
                    name = ParseAttributeName();
                    value = ParseAttributeValue();
                }
            }
            return result;
        }

    }

}
