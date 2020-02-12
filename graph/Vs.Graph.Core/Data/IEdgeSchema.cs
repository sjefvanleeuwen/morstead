namespace Vs.Graph.Core.Data
{
    public interface IEdgeSchema : IIdentifiable, ISerialize
    {
        Constraints Constraints { get; }
        Attributes Attributes { get; }
    }
}
