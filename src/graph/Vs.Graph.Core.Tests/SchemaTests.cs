using Vs.Graph.Core.Data;
using Xunit;

namespace Vs.Graph.Core.Tests
{
    public class SchemaTests
    {
        [Fact]
        public void CreateCodeFromSchema()
        {
            var schemaController = new SchemaController(null);
            var nameSpace = "Vs.GraphData";
            var schema = schemaController.Deserialize(@"Name: codeTest
Attributes:
- Name: term
  Type: text
- Name: omschrijving
  Type: text
- Name: versie
  Type: int
- Name: periode
  Type: periode
");
            var code = schemaController.CreateCode<INode>(schema, nameSpace);
        }
        [Fact]
        public void CreateCompilableCodeFromSchema()
        {
            var schemaController = new SchemaController(null);
            var nameSpace = "Vs.GraphData";
            var schema = schemaController.Deserialize(@"Name: codeTest
Attributes:
- Name: id
  Type: int
- Name: term
  Type: text
- Name: omschrijving
  Type: text
- Name: versie
  Type: int
- Name: periode
  Type: periode
");
            var code = schemaController.CreateCode<INode>(schema, nameSpace);
            var assembly = schemaController.Compile(code);
            Assert.NotNull(assembly);
        }
    }
}
