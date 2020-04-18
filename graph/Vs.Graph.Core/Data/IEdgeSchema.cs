using Vs.Core.Serialization;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public interface IEdgeSchema : IIdentifiable, IYamlConvertible
    {

        Constraints Constraints { get; }
        Attributes Attributes { get; }
    }
}
