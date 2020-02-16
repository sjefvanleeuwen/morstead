using Vs.Graph.Core;
using Vs.Graph.Core.Data;
using Vs.Graph.Core.Data.AttributeTypes;
using Xunit;

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

            SchemaController controller = new SchemaController(new MsSqlGraphSchemaService());
            var yaml = controller.Serialize(n);
            var r = controller.Deserialize(yaml);
            var sql = controller.Service.CreateScript(r);

            Assert.True(sql== @"CREATE TABLE node.person (
ID INTEGER PRIMARY KEY,
FirstName  NTEXT,LastName  NTEXT,DateOfBirth DATETIME,
) AS NODE;
CREATE TABLE edge.likes (
rating DECIMAL,CONSTRAINT EC_LIKES CONNECTION (
node.person TO node.person
)
) AS EDGE;

CREATE TABLE edge.married (
CONSTRAINT EC_MARRIED CONNECTION (
node.person TO node.person
)
) AS EDGE;

CREATE TABLE edge.friend (
CONSTRAINT EC_FRIEND CONNECTION (
node.person TO node.person
)
) AS EDGE;

");
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
    Type: int
- Name: married
  Constraints:
  - Name: person
  Attributes: []
- Name: friend
  Constraints:
  - Name: person
  Attributes: []
";
            SchemaController controller = new SchemaController(new MsSqlGraphSchemaService());
            var r = controller.Deserialize(yaml);
            var sql = controller.Service.CreateScript(r);
            Assert.True(sql== @"CREATE TABLE node.person (
ID INTEGER PRIMARY KEY,
FirstName  NTEXT,LastName  NTEXT,
) AS NODE;
CREATE TABLE edge.likes (
rating INTEGER,CONSTRAINT EC_LIKES CONNECTION (
node.person TO node.person
)
) AS EDGE;

CREATE TABLE edge.married (
CONSTRAINT EC_MARRIED CONNECTION (
node.person TO node.person
)
) AS EDGE;

CREATE TABLE edge.friend (
CONSTRAINT EC_FRIEND CONNECTION (
node.person TO node.person
)
) AS EDGE;

");
        }
    }
}
