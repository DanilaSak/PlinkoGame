using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public class Match
    {
        private List<Node> _nodes = new List<Node>();

        public void Add(Node node)
        {
            if (node== null)return;
            _nodes.Add(node);
        }

        public void Use()
        {
            _nodes = _nodes.OrderBy(n => n._ball).ToList();
            Check((Ball)1);
            Check((Ball)2);
            Check((Ball)3);
            Check((Ball)4);
            Check((Ball)5);
            Check((Ball)6);
            Check((Ball)7);
            Check((Ball)8);
            Check((Ball)9);
            Check((Ball)10);
        }
        private void Check(Ball ball)
        {
            var t = _nodes.Where(n => n._ball == ball);
            if (t.Count()>=3)
            {
                foreach (var node in t)
                {
                    node.isSetToDestroy = true;
                }
            }
        }
    }
}