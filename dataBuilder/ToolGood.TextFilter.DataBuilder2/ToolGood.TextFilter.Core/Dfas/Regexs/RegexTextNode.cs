using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolGood.DFAs
{
    public class RegexTextNode
    {
        /// <summary>
        /// 文本 
        /// 采用正则表达式
        /// </summary>
        public string RegexText;
        /// <summary>
        /// 添加
        /// </summary>
        public string AppendText;

        public List<RegexTextNode> Nodes;
        /// <summary>
        /// 是否并联
        /// </summary>
        public bool IsOrOperate;

        public int LabelIndex;


        #region RegexTextNode
        public RegexTextNode()
        {
            Nodes = new List<RegexTextNode>();
        }
        public RegexTextNode(char c)
        {
            RegexText = c.ToString();
        }
        public RegexTextNode(string t)
        {
            RegexText = t;
        }
        public RegexTextNode(params char[] cs)
        {
            RegexText = new string(cs);
        }
        public RegexTextNode this[int index] {
            get {
                return Nodes?[index];
            }
        }
        #endregion

        public int Count {
            get {
                return Nodes != null ? Nodes.Count : 0;
            }
        }

        #region GroupByOr
        public RegexTextNode GroupByOr(bool sort = false)
        {
            return GroupByOr(this, sort);
        }

        private RegexTextNode GroupByOr(RegexTextNode node, bool sort = false)
        {
            if (string.IsNullOrEmpty(node.RegexText)) {
                bool hasOr = false;
                foreach (var item in node.Nodes) {
                    GroupByOr(item, sort);
                    if (item.RegexText == "|") { hasOr = true; }
                }
                if (hasOr) {
                    var root = new List<RegexTextNode>();
                    var list = new RegexTextNode();
                    root.Add(list);
                    foreach (var item in node.Nodes) {
                        if (item.RegexText == "|") {
                            list = new RegexTextNode();
                            root.Add(list);
                        } else {
                            list.Nodes.Add(item);
                        }
                    }
                    if (sort) {
                        var ones = root.Where(q => q.Nodes.Count == 1).ToList();
                        var more = root.Where(q => q.Nodes.Count != 1).ToList();
                        root = new List<RegexTextNode>();
                        root.AddRange(ones);
                        root.AddRange(more);
                    }
                    node.IsOrOperate = true;
                    node.Nodes = root;

                }
            }
            return node;
        }

        #endregion




        #region Build

        public Exp Build()
        {
            var exp = Build(this);
            var root = new RootExp();
            root.InnerExp = exp;
            root.LabelIndex = this.LabelIndex;
            return root;
        }

        private Exp Build(RegexTextNode node)
        {
            Exp exp = null;
            if (node.RegexText == "(" || node.RegexText == ")") {
                return null;
            }
            if (node.RegexText == null) {
                if (node.Nodes[0].ToString() == "[") {
                    exp = Exp.CreateCharClass(node.ToString(false));
                } else {
                    var list = new List<Exp>();
                    foreach (var item in node.Nodes) {
                        list.Add(Build(item));
                    }
                    list.RemoveAll(q => q == null);
                    exp = list[0];
                    for (int i = 1; i < list.Count; i++) {
                        if (node.IsOrOperate) {
                            exp = exp.Union(list[i]);
                        } else {
                            exp = exp.Concat(list[i]);
                        }
                    }
                }
            } else {
                exp = Exp.CreateCharClass(node.RegexText);
            }
            if (node.AppendText == "?") {
                exp = exp.Optional();
            } else if (node.AppendText == "*") {
                exp = exp.Star();
            } else if (node.AppendText == "+") {
                exp = exp.Positive();
            } else if (node.AppendText != null && node.AppendText.StartsWith("{")) {
                var m = Regex.Match(node.AppendText, @"{(\d*)(,(\d*))?}");
                var min = 0;
                var max = int.MaxValue;
                if (m.Groups[1].Success && m.Groups[1].Value.Length > 0) {
                    min = int.Parse(m.Groups[1].Value);
                }
                if (m.Groups[2].Success == false) { max = min; } //不包含 逗号
                if (m.Groups[3].Success && m.Groups[3].Value.Length > 0) {
                    max = int.Parse(m.Groups[3].Value);
                }
                exp = exp.Repeat(min, max);
            }
            //exp.LabelIndex = node.LabelIndex;
            return exp;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToString(stringBuilder);
            return stringBuilder.ToString();
        }
        public string ToString(bool withAppendText)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ToString(stringBuilder, false);
            return stringBuilder.ToString();
        }
        private void ToString(StringBuilder stringBuilder, bool withAppendText = true)
        {
            if (string.IsNullOrEmpty(RegexText)) {
                for (int i = 0; i < Nodes.Count; i++) {
                    if (IsOrOperate && i > 0) { stringBuilder.Append("|"); }
                    Nodes[i].ToString(stringBuilder);
                }
            } else {
                stringBuilder.Append(RegexText);
            }

            if (string.IsNullOrEmpty(AppendText) == false && withAppendText) {
                stringBuilder.Append(AppendText);
            }
        }
        #endregion


    }

}
