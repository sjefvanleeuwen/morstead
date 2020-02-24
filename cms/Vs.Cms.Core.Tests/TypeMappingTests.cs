using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Vs.DataProvider.MsSqlGraph;
using Vs.Graph.Core;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Vs.Cms.Core.Tests
{
    [TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
    public class TypeMappingTests
    {
        private readonly GraphController controller;
        private readonly SchemaController schemaController;

        public TypeMappingTests()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddYamlFile("config.yaml", optional: false).Build();
            Global.ConnectionString = configuration["Cms:SqlConnection"];
            controller = new GraphController(Global.ConnectionString);
            schemaController = new SchemaController(new MsSqlGraphSchemaService());
        }

        [Fact, Order(1)]
        [Trait("Category", "Integration")]
        public void CreateTestTable()
        {
            var schema = schemaController.Deserialize(@"Name: typeMappingTest
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
            var tSql = schemaController.Service.CreateScript(schema);

            using (var connection = new SqlConnection(Global.ConnectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(@"IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.schemata
WHERE   schema_name = 'node' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA node'
END");
                affectedRows = connection.Execute($"drop table if exists node.{schema.Name}");
                connection.Execute(tSql);
            }
        }
    }
}
