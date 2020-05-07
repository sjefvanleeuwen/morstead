namespace Vs.Rules.Core.Model
{
    public interface IClientParameter : IParameter
    {
        bool IsCalculated { get; }
    }
}