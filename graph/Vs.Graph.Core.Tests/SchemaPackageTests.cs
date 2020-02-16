using System.Collections.Generic;
using Vs.DataProvider.MsSqlGraph;
using Vs.Graph.Core.SchemaPackageServices;
using Xunit;

namespace Vs.Graph.Core.Tests
{
    public class SchemaPackageTests
    {
        private readonly string migration = "20200215154400";

        [Fact]
        public void InMemoryCanLoadPackages()
        {
            var files = new Dictionary<string, string>();
            files.Add($"/schemas/migration_{migration}/openId.yaml", @"Name: openId
Attributes:
  - Name: gebruikersId
    Type: text
");
            files.Add($"/schemas/migration_{migration}/persoon.yaml", @"Name: persoon
Attributes:
- Name: voornamen
  Type: text
- Name: voorvoegsel
  Type: text
- Name: geslachtsnaam
  Type: text
Edges:
- Name: digitaleIdentiteit
  Constraints:
  - Name: openId
    Attributes:
    - Name: periode
      Type: periode
");
            files.Add($"/schemas/migration_{migration}/bestand.yaml", @"Name: bestand
Attributes:
- Name: content
  Type: text
- Name: type
  Type: text
- Name: versie
  Type: int
Edges:
- Name: versies
  Constraints:
  - Name: bestand
");
            files.Add($"/schemas/migration_{migration}/schema-package.yaml", @"SchemaSequence:
Schemas:
- Name: openId
- Name: persoon
- Name: bestand
");
            var storageService = new InMemorySchemaPackageService
            {
                Files = files
            };
            var schemaService = new MsSqlGraphSchemaService();
            var controller = new SchemaPackageController(storageService, schemaService);
            var sql = controller.CreateMigrationScript($"/schemas/migration_{migration}");
            Assert.True(sql== @"CREATE TABLE node.openId (
ID INTEGER PRIMARY KEY,
gebruikersId  NTEXT,
) AS NODE;

CREATE TABLE node.persoon (
ID INTEGER PRIMARY KEY,
voornamen  NTEXT,voorvoegsel  NTEXT,geslachtsnaam  NTEXT,
) AS NODE;
CREATE TABLE edge.digitaleIdentiteit (
CONSTRAINT EC_DIGITALEIDENTITEIT CONNECTION (
node.persoon TO node.openId
)
) AS EDGE;


CREATE TABLE node.bestand (
ID INTEGER PRIMARY KEY,
content  NTEXT,type  NTEXT,versie INTEGER,
) AS NODE;
CREATE TABLE edge.versies (
CONSTRAINT EC_VERSIES CONNECTION (
node.bestand TO node.bestand
)
) AS EDGE;


");
        }
    }
}
