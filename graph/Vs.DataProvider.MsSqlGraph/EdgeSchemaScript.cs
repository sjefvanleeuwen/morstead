using System.Text;
using Vs.Graph.Core.Data;

namespace Vs.DataProvider.MsSqlGraph
{
    public class EdgeSchemaScript : IEdgeSchemaScript
    {
        public INodeSchema Parent => _parent;

        public INodeSchema _parent;

        public EdgeSchemaScript(INodeSchema parent)
        {
            _parent = parent;
        }

        public string CreateScript(IEdgeSchema edge)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"CREATE TABLE {edge.Name} (");
            sb.Append(new AttributeSchemaScript().CreateScript(edge.Attributes));
            if (edge.Constraints!= null && edge.Constraints.Count > 0) {
                sb.AppendLine($"CONSTRAINT EC_{edge.Name.ToUpper()} CONNECTION (");
                for (int i=0;i<edge.Constraints.Count;i++)
                {
                    sb.Append($"{Parent.Name} TO {edge.Constraints[i].Name}");
                    if (i < edge.Constraints.Count-1)
                        sb.Append(",");
                    sb.AppendLine();
                }

                sb.AppendLine(")");
            }
            sb.AppendLine($") AS EDGE;");
            return sb.ToString();
        }
    }
}
