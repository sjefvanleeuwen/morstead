using Vs.Core.Serialization;
using YamlDotNet.Serialization;

namespace Vs.Graph.Core.Data
{
    public interface IConstraintSchema : IIdentifiable, IYamlConvertible
    {
    }
}
