using Vs.DataProvider.MsSqlGraph;
using Xunit;

namespace Vs.Graph.Core.Tests
{
    public class DataTests
    {
        [Fact]
        public void CanReadSchemaPackage()
        {
            var schemaPackage = @"SchemaSequence:
Schemas:
- Name: openId
- Name: persoon
- Name: bestand
";
            SchemaController controller = new SchemaController(new MsSqlGraphSchemaService());
            var package = controller.SchemaSequence(schemaPackage);
            Assert.NotNull(package.Schemas);
            Assert.True(package.Schemas.Count == 3);
            Assert.True(package.Schemas[0].Name != string.Empty);
        }

        [Fact]
        public void CreateEntityFromYamlWithCustomType()
        {
            // create some entities through built-in data provider
            var yaml = @"Version: 15.0.0.0
Name: persoon
Attributes:
- Name: BSN
  Type: elfproef
- Name: periode
  Type: periode
Edges:
- Name: partner
  Constraints:
  - Name: persoon
  Attributes:
  - Name: periode
    Type: periode
- Name: kind
  Constraints:
  - Name: persoon
- Name: ouder
  Constraints:
  - Name: persoon
";

            SchemaController controller = new SchemaController(new MsSqlGraphSchemaService());
            var r = controller.Deserialize(yaml);
            var s = controller.Service.CreateScript(r);
            Assert.True(s == @"CREATE TABLE node.persoon (
ID INTEGER PRIMARY KEY,
BSN VARCHAR(10),periode_begin DATETIME,periode_eind  DATETIME,
) AS NODE;
CREATE TABLE edge.partner (
periode_begin DATETIME,periode_eind  DATETIME,CONSTRAINT EC_PARTNER CONNECTION (
node.persoon TO node.persoon
)
) AS EDGE;

CREATE TABLE edge.kind (
CONSTRAINT EC_KIND CONNECTION (
node.persoon TO node.persoon
)
) AS EDGE;

CREATE TABLE edge.ouder (
CONSTRAINT EC_OUDER CONNECTION (
node.persoon TO node.persoon
)
) AS EDGE;

");
            /*

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=graph;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(s, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            */
        }
    }
}
