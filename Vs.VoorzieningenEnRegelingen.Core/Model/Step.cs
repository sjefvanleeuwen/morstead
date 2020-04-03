using System.Collections.Generic;
using Vs.Core.Diagnostics;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Step : IStep
    {
        public Step(DebugInfo debugInfo, int key, string name, string description, string formula, string value, string situation, IBreak @break, IEnumerable<IChoice> choices)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                //description is not required
                description = name;
            }

            DebugInfo = debugInfo;
            Key = key;
            Name = name;
            Description = description;
            Formula = formula;
            Value = value;
            Situation = situation;
            Break = @break;
            Choices = choices;
        }

        public int Key { get; }
        public string Name { get; }
        public string Description { get; }
        public string Formula { get; }
        public string Value { get; }
        public string Situation { get; set; }
        /// <summary>
        /// Choices is a number of situations
        /// This will lead to a question which of the situations should be taken
        /// </summary>
        public IEnumerable<IChoice> Choices { get; }
        /// <summary>
        /// Break is also a formula, but allows a condition to break out of a flow.
        /// For example, if someone has rights (recht) to a service.
        /// </summary>
        public IBreak Break { get; }

        public bool IsSituational => !string.IsNullOrEmpty(Situation);

        public string SemanticKey { get; set; }

        public DebugInfo DebugInfo { get; }

        public IStep Clone()
        {
            return (Step)this.MemberwiseClone();
        }
    }
}