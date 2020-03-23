using Vs.Core.Collections.NodeTree;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ContentNode : TreeNodeBase<ContentNode>
    {
        public bool IsSituational { get; set; }
        public bool IsBreak { get; set; }
        public IParameter Parameter { get; set; }
        public string Situation { get; set; }

        public ContentNode(string name) : base(name)
        {

        }

        protected override ContentNode MySelf
        {
            get { return this; }
        }
    }
}


