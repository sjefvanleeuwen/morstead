using System.Collections.Generic;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.Site.Model
{
    public class Node : INode
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public IList<INode> SubNodes { get; } = new List<INode>();

        public void AddSubNode(INode subNode)
        {
            SubNodes.Add(subNode);
        }

        public void AddSubNodes(IEnumerable<INode> subNodes)
        {
            subNodes.ToList().ForEach(n => AddSubNode(n));
        }
    }
}
