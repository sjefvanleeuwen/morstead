using Vs.Core.Serialization;

namespace Vs.Graph.Core.Data
{
    public interface INodeSchema : IIdentifiable, ISerialize
    {
        Attributes Attributes { get; }
        Edges Edges { get; }
    }
}