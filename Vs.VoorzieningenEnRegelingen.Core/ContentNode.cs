using System.Collections.Generic;
using Vs.Core.Collections.NodeTree;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ContentNode
    {
        public bool IsSituational { get; set; }
        public bool IsBreak { get; set; }
        public IParameter Parameter { get; set; }
        public string Situation { get; set; }
        public string Name { get; private set; }
        Dictionary<string, string> SituationParameterValues { get; set; }
        public ContentNode(string name)
        {
            Name = name;
        }
    }
}


