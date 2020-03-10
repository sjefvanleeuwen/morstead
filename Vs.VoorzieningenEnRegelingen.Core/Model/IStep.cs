namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface IStep
    {
        string Break { get; }
        string Description { get; }
        string Formula { get; }
        bool IsSituational { get; }
        int Key { get; }
        string Name { get; }
        string Situation { get; }
    }
}