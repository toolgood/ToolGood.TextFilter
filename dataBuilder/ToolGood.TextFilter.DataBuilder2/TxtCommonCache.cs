using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    public class TxtCommonCache
    {
        private Regex tagRegex = new Regex(@"\{([\u3400-\u9fffa-zA-Z][\u3400-\u9fff0-9a-zA-Z\-_]*)\}", RegexOptions.Compiled);

        private Dictionary<string, string> TxtCommonTemp = new Dictionary<string, string>();
        private Dictionary<string, List<string>> TxtCommonDict2 = new Dictionary<string, List<string>>();

        public void AddTxtCommon(DbTxtCommonType type, List<DbTxtCommon> commons)
        {
            var name = "{" + type.Name + "}";
            if (name=="{电话}") {

            }
            var comStr = BuildExpString(commons);
            TxtCommonTemp[name] = comStr;

            var list2 = new List<string>();
            foreach (var item in commons) {
                list2.Add(BuildExpString(new List<DbTxtCommon> { item }));
            }
            TxtCommonDict2[name] = list2;
        }

        public List<string> GetTxtCommon(string name)
        {
            if (TxtCommonDict2.TryGetValue(name, out List<string> list)) {
                return list;
            }
            return new List<string>();
        }

        public string GetCommonString(string name)
        {
            return TxtCommonTemp[name];
        }


        private string BuildExpString(List<DbTxtCommon> txtCommons)
        {
            var singles = new List<string>();
            var memges = new List<string>();
            foreach (var common in txtCommons) {
                if (common.Text.Length == 1) {
                    singles.Add(common.Text);
                } else {
                    var text = common.Text;
                    if (tagRegex.IsMatch(text)) {
                        text = tagRegex.Replace(text, new MatchEvaluator((m) => {
                            return TxtCommonTemp[m.Value];
                        }));
                    }
                    memges.Add(text);
                }
            }
            StringBuilder sb = new StringBuilder();

            if (singles.Count > 1 && memges.Count > 0) {
                sb.Append("([");
                foreach (var s in singles) {
                    sb.Append(s);
                }
                sb.Append("]");
                foreach (var s in memges) {
                    sb.Append("|");
                    sb.Append(s);
                }
                sb.Append(")");
            } else if (singles.Count == 1 && memges.Count > 0) {
                sb.Append("(");
                foreach (var s in singles) {
                    sb.Append(s);
                }
                foreach (var s in memges) {
                    sb.Append("|");
                    sb.Append(s);
                }
                sb.Append(")");
            } else if (singles.Count == 1) {
                sb.Append(singles[0]);
            } else if (memges.Count == 1) {
                sb.Append(memges[0]);
            } else if (singles.Count > 1) {
                sb.Append("[");
                foreach (var s in singles) {
                    sb.Append(s);
                }
                sb.Append("]");
            } else {
                sb.Append("(");
                foreach (var s in memges) {
                    if (sb.Length > 1) {
                        sb.Append('|');
                    }
                    sb.Append(s);
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

    }

}
