using System.Collections.Generic;

namespace ToolGood.TextFilter
{
    public class MultiWordsTreeNode
    {
        public int Index;
        public int Key;
        public SortedDictionary<int, MultiWordsTreeNode> Nodes;
        public List<int> Results;
        public MultiWordsTreeNode Parent;
        public bool End;
        public int Layer;


        public int ParentIndex;
        public int NextStart;
        public int NextEnd;
        public List<int> IntervalWrods = new List<int>();

        public MultiWordsTreeNode()
        {
            Nodes = new SortedDictionary<int, MultiWordsTreeNode>();
            Results = new List<int>();
        }

        public bool TryGetValue(int key, out MultiWordsTreeNode node)
        {
            if (Nodes.TryGetValue(key, out node)) {
                return true;
            }
            return false;
        }

        public void Add(int key, MultiWordsTreeNode node)
        {
            Nodes[key] = node;
            node.Parent = this;
        }

        public int GetResult()
        {
            if (Results.Count > 0) {
                return Results[0];
            }
            return 0;
        }

        public void SetLayer(int layer)
        {
            if (Layer < layer) {
                Layer = layer;
            }
            foreach (var item in Nodes) {
                item.Value.SetLayer(Layer + 1);
            }
        }


    }

}

