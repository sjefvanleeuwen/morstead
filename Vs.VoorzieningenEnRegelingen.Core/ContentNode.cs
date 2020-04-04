using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.Interface;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class ContentNode : IContentNode
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


