using Vs.Graph.Core.Data;

namespace Vs.Graph.Core
{
    public interface IGraphSchemaService
    {
        string CreateScript(NodeSchema nodeSchema);
    }
}
