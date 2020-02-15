using Vs.Graph.Core.Data;
using Vs.Graph.Core.Data.AttributeTypes;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.DataProvider.MsSqlGraph.Tests
{
    public class NodeTests
    {
        [Fact]
        public void Node_Create_Person()
        {
            NodeSchema n = new NodeSchema(name:"person");
            n.Attributes.Add(new Attribute(name: "FirstName", type: new AttributeText()));
            n.Attributes.Add(new Attribute(name: "LastName", type: new AttributeText()));
            n.Attributes.Add(new Attribute(name: "DateOfBirth", new AttributeDatum()));
            n.Edges.Add(new EdgeSchema(name: "likes"));
            n.Edges.Add(new EdgeSchema(name: "married"));
            n.Edges.Add(new EdgeSchema(name: "friend"));

            n.Edges[0].Constraints.Add(new Constraint(name: "person"));
            n.Edges[1].Constraints.Add(new Constraint(name: "person"));
            n.Edges[2].Constraints.Add(new Constraint(name: "person"));

            n.Edges[0].Attributes.Add(new Attribute(name: "rating", type: new AttributeEuro()));

            NodeSchemaScript script = new NodeSchemaScript();
            SchemaController controller = new SchemaController();
            var yaml = controller.Serialize(n);

            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            var r = deserializer.Deserialize<NodeSchema>(yaml);
            var sql = script.CreateScript(n);
        }

        [Fact]
        public void Node_Can_Deserialize_And_Create_SqlSchema()
        {
            var yaml = @"Version: 15.0.0.0
Name: person
Attributes:
- Name: FirstName
  Type: Text
- Name: LastName
  Type: Text
- Name: DateOfBirth
  Type: DateTime
Edges:
- Name: likes
  Constraints:
  - Name: person
  Attributes:
  - Name: rating
    Type: Integer
- Name: married
  Constraints:
  - Name: person
  Attributes: []
- Name: friend
  Constraints:
  - Name: person
  Attributes: []
";
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            var r = deserializer.Deserialize<NodeSchema>(yaml);
            NodeSchemaScript script = new NodeSchemaScript();
            var s = script.CreateScript(r);
        }
    }
}
