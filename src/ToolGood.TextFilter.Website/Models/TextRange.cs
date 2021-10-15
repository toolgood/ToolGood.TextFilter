/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.Collections.Generic;
using System.Text;

namespace ToolGood.TextFilter.Models
{
    public class TextRange
    {
        private List<int> Starts = new List<int>();
        private List<int> Ends = new List<int>();
        private int Start;
        private int End;
        private bool First = true;

        public TextRange(int start, int end)
        {
            Start = start;
            End = end;
            First = false;
        }

        public TextRange()
        {
        }


        public void Add(int start, int end)
        {
            if (First) {
                Start = start;
                End = end;
                First = false;
            } else if (start > End + 1) {
                Starts.Add(Start);
                Ends.Add(End);
                Start = start;
                End = end;
            } else {
                End = end;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Starts.Count; i++) {
                var s = Starts[i];
                var e = Ends[i];
                if (sb.Length > 0) {
                    sb.Append(",");
                }
                sb.Append(s);
                if (s != e) {
                    sb.Append("-");
                    sb.Append(e);
                }
            }
            if (sb.Length > 0) {
                sb.Append(",");
            }
            sb.Append(Start);
            if (Start != End) {
                sb.Append("-");
                sb.Append(End);
            }
            return sb.ToString();
        }

    }

}
