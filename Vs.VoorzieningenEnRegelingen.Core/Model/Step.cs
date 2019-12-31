namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Step
    {
        public Step(string name, string description, string formula, string situation)
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
        }

        public string Name { get; }
        public string Description { get; }
        public string Formula { get; }
        public string Situation { get; }
        public bool IsSituational => !string.IsNullOrEmpty(Situation);
    }
}