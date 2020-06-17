using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Site.Model.Interfaces
{
    public interface INode
    {
        string Key { get; set; }
        string Name { get; set; }
        string Color { get; set; }
        IList<INode> SubNodes { get; }

        void AddSubNode(INode subNode);

        void AddSubNodes(IEnumerable<INode> subNodes);
    }
}
