namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public class Situation : ISituation
    {
        public string Name { get; }

        public Situation(string name)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
        }
    }
}