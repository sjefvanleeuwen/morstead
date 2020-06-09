using Vs.Core.Serialization;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public interface INodeSchema : IIdentifiable, IYamlConvertible
    {
        Attributes Attributes { get; }
        Edges Edges { get; }
    }
}