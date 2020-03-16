namespace Vs.VoorzieningenEnRegelingen.Core.Model
{
    public interface IClientParameter : IParameter
    {
        bool IsCalculated { get; }
    }
}