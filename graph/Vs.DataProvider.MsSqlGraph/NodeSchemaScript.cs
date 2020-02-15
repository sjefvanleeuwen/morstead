using System.Text;
using Vs.Graph.Core.Data;

namespace Vs.DataProvider.MsSqlGraph
{
    public class NodeSchemaScript : INodeSchemaScript
    {
        public string CreateScript(INodeSchema node)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE node.{node.Name} (");
            sb.AppendLine($"ID INTEGER PRIMARY KEY,");
            sb.AppendLine(new AttributeSchemaScript().CreateScript(node.Attributes));
            sb.AppendLine(") AS NODE;");
            var s = new EdgeSchemaScript(node);
            foreach (var edge in node.Edges)
            {
                sb.AppendLine(s.CreateScript(edge));
            }
            return sb.ToString();
        }
    }
}
