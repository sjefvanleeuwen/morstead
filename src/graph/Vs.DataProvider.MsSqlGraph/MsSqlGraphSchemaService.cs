using Vs.Graph.Core;
using Vs.Graph.Core.Data;

namespace Vs.DataProvider.MsSqlGraph
{
    public class MsSqlGraphSchemaService : IGraphSchemaService
    {
        public string CreateScript(NodeSchema nodeSchema)
        {
            NodeSchemaScript script = new NodeSchemaScript();
            return script.CreateScript(nodeSchema);
        }
    }
}
