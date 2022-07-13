using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.TextFilter
{
    class TrieNode4
    {
        public int Index;
        public int Layer;
        public bool End;
        public char Char;
        public Dictionary<char, TrieNode4> m_values;
        public TrieNode4 Failure;
        public TrieNode4 Parent;
        public Dictionary<char, TrieNode4> dfa_values;
        public AcrTrieNode AcrTrieNode;

        public int Ptr = -1;
        public Int32 Next;
        public Int32 minflag = Int32.MaxValue;
        public Int32 maxflag = 0;
        public TrieNode4 SimplifyFailure;

        public TrieNode4()
        {
            m_values = new Dictionary<char, TrieNode4>();
            dfa_values = new Dictionary<char, TrieNode4>();
        }

        public static List<TrieNode4> ToTrieNode4(List<AcrTrieNode> list)
        {
            foreach (var item in list) {
                foreach (var (ch, val) in item.Nodes) {
                    if (val.Parent == null) {
                        val.Parent = item;
                    } else if (val.Parent == item) {
                    } else {
                    }
                }
            }

            var _root = list[0];
            HashSet<Tuple<char, int>> nodes = new HashSet<Tuple<char, int>>();
            foreach (var (_, value) in _root.Nodes) {
                value.Failure = _root;
                foreach (var (k, v) in value.Nodes) {
                    nodes.Add(Tuple.Create(k, v.Index));
                }
            }
            while (nodes.Count != 0) {
                HashSet<Tuple<char, int>> newNodes = new HashSet<Tuple<char, int>>();

                foreach (var (c, index) in nodes) {
                    var nd = list[index];
                    var r = nd.Parent.Failure;

                    while (r != null && !r.Nodes.ContainsKey(c)) r = r.Failure;
                    if (r == null)
                        nd.Failure = _root;
                    else {
                        nd.Failure = r.Nodes[c];
                        if (nd.Failure.End) { nd.End = true; }
                    }
                    foreach (var (k, v) in nd.Nodes) {
                        newNodes.Add(Tuple.Create(k, v.Index));
                    }
                }
                nodes = newNodes;
            }
            _root.Failure = _root;

            List<TrieNode4> result = new List<TrieNode4>();



            //TrieNode4 root = new TrieNode4();
            //root.AcrTrieNode = oldRoot;
            //result.Add(root);
            //Queue<Tuple<AcrTrieNode, TrieNode4>> queue = new Queue<Tuple<AcrTrieNode, TrieNode4>>();
            //queue.Enqueue(Tuple.Create(oldRoot, root));

            //while (queue.TryDequeue(out Tuple<AcrTrieNode, TrieNode4> tup)) {
            //    var oldNode = tup.Item1;
            //    var newNode = tup.Item2;

            //    for (int i = 0; i < oldNode.NodeChars.Count; i++) {
            //        var ch = oldNode.NodeChars[i];
            //        var value = oldNode.NodeValues[i];
            //        if (value == oldNode) {
            //            continue;
            //        }


            //        TrieNode4 node = new TrieNode4();
            //        node.AcrTrieNode = value;
            //        node.Char = ch;
            //        node.End = value.Results.Count > 0;
            //        node.Parent = newNode;

            //        newNode.m_values[ch] = node;
            //        result.Add(node);
            //        queue.Enqueue(Tuple.Create(value, node));
            //    }
            //}
            return result;
        }





    }
}
