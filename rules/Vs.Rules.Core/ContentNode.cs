using System.Collections.Generic;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Core
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


