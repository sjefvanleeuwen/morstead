using Vs.DataProvider.MsSqlGraph;
using Vs.Graph.Core.Data;
using Xunit;

namespace Vs.Graph.Core.Tests
{
    public class DataTests
    {
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
            
            SchemaController controller = new SchemaController();
            var r = controller.Deserialize(yaml);




            NodeSchemaScript script = new NodeSchemaScript();
            var s = script.CreateScript(r);
            Assert.True(s== @"CREATE TABLE persoon (
ID INTEGER PRIMARY KEY,
BSN VARCHAR(10),periode_begin DATETIME,periode_eind  DATETIME 
) AS NODE;
CREATE TABLE partner (
periode_begin DATETIME,periode_eind  DATETIME CONSTRAINT EC_PARTNER CONNECTION (
persoon TO persoon
)
) AS EDGE;

CREATE TABLE kind (
CONSTRAINT EC_KIND CONNECTION (
persoon TO persoon
)
) AS EDGE;

CREATE TABLE ouder (
CONSTRAINT EC_OUDER CONNECTION (
persoon TO persoon
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
