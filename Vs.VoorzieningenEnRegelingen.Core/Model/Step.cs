using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Step : IStep
    {
        public Step(int key, string name, string description, string formula, string situation, string @break, IEnumerable<string> choices)
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

            Key = key;
            Name = name;
            Description = description;
            Formula = formula;
            Situation = situation;
            Break = @break;
            Choices = choices;
        }

        public int Key { get; }
        public string Name { get; }
        public string Description { get; }
        public string Formula { get; }
        public string Situation { get; }
        /// <summary>
        /// Choices is a number of situations
        /// This will lead to a question which of the situations should be taken
        /// </summary>
        public IEnumerable<string> Choices { get; }
        /// <summary>
        /// Break is also a formula, but allows a condition to break out of a flow.
        /// For example, if someone has rights (recht) to a service.
        /// </summary>
        public string Break { get; }
        

        public bool IsSituational => !string.IsNullOrEmpty(Situation);
    }
}