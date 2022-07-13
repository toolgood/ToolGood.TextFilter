using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.DFAs;
using ToolGood.TextFilter.Datas;

namespace ToolGood.TextFilter.DataBuilder2
{
    public partial class TxtCustomInfo
    {
        public DbTxtCustom TxtCustom { get; set; }
        public DbTxtContact TxtContact { get; set; }
        public int ContactId { get; set; }

        public string Text { get; set; }
        public bool IsRepeatWords { get; set; }
        public int Id { get; set; }

        public RootExp CurrExp { get; set; }

        public List<List<int>> SubIds { get; set; } = new List<List<int>>();
        /// <summary>
        /// 间隙  SubIds长度 - 1
        /// </summary>
        public List<int> IntervalWrods = new List<int>();

        public List<List<int>> GetSubIds(TxtCache txtCache)
        {
            var temp = txtCache.TxtKeywordInfo_OutIndex;
            var min = txtCache.Keyword34_Start;

            List<List<int>> result = new List<List<int>>();
            foreach (var ids in SubIds) {
                List<int> list = new List<int>();
                foreach (var id in ids) {
                    if (id >= min) {
                        var ts = temp[id];
                        foreach (var t in ts) {
                            if (list.Contains(t) == false) {
                                list.Add(t);
                            }
                        }
                    } else {
                        list.Add(id);
                    }
                }
                result.Add(list);
            }
            return result;
        }
        public List<List<int>> GetSubIds(Dictionary<int, List<int>> temp, int min)
        {
            List<List<int>> result = new List<List<int>>();
            foreach (var ids in SubIds) {
                List<int> list = new List<int>();
                foreach (var id in ids) {
                    if (id >= min) {
                        var ts = temp[id];
                        foreach (var t in ts) {
                            if (list.Contains(t) == false) {
                                list.Add(t);
                            }
                        }
                    } else {
                        list.Add(id);
                    }
                }
                result.Add(list);
            }
            return result;
        }


        public List<List<int>> GetSubIds(Dictionary<int, List<int>> temp)
        {
            List<List<int>> result = new List<List<int>>();
            foreach (var ids in SubIds) {
                List<int> list = new List<int>();
                foreach (var id in ids) {
                    var ts = temp[id];
                    foreach (var t in ts) {
                        if (list.Contains(t) == false) {
                            list.Add(t);
                        }
                    }
                }
                result.Add(list);
            }
            return result;
        }


    }
}
