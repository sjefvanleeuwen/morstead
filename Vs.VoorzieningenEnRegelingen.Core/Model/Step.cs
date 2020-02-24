namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Step
    {
        public Step(string name, string description, string formula, string situation, string @break)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentException("message", nameof(description));
            }

            Name = name;
            Description = description;
            Formula = formula;
            Situation = situation;
            Break = @break;
        }

        public string Name { get; }
        public string Description { get; }
        public string Formula { get; }
        public string Situation { get; }
        /// <summary>
        /// Break is also a formula, but allows a condition to break out of a flow.
        /// For example, if someone has rights (recht) to a service.
        /// </summary>
        public string Break { get; }

        public bool IsSituational => !string.IsNullOrEmpty(Situation);
    }
}