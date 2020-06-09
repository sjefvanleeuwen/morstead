using System;
using System.Linq;
using System.Text;
using Vs.Graph.Core.Data;
using Vs.Graph.Core.SchemaPackageServices;

namespace Vs.Graph.Core
{
    public class SchemaPackageController
    {
        private readonly ISchemaPackageStorageService _schemaPackageStorageService;
        private readonly IGraphSchemaService _graphSchemaService;

        public SchemaPackageController(ISchemaPackageStorageService schemaPackageStorageService, IGraphSchemaService graphSchemaService)
        {
            _schemaPackageStorageService = schemaPackageStorageService;
            _graphSchemaService = graphSchemaService;
        }

        public string CreateMigrationScript(string migrationUrn)
        {
            StringBuilder sb = new StringBuilder();
            SchemaController controller = new SchemaController(_graphSchemaService);
            var files = _schemaPackageStorageService.GetFiles(migrationUrn);
            var f = (from p in files
                     where p.Key.EndsWith("schema-package.yaml")
                     select p).SingleOrDefault();
            if (string.IsNullOrEmpty(f.Value))
                throw new Exception($"schema-package.yaml was not found at migration urn. {migrationUrn}");
            // get sequence of schemas from package to be converted.
            var sequence = controller.SchemaSequence(f.Value);
            NodeSchema nodeSchema = null;
            foreach (var nodeSequence in sequence.Schemas)
            {
                var schemaFile = (from p in files
                                  where p.Key.EndsWith($"{nodeSequence.Name}.yaml")
                                  select p).SingleOrDefault();
                if (string.IsNullOrEmpty(schemaFile.Value))
                    throw new Exception($"{nodeSequence.Name}.yaml wat not found at migration urn {migrationUrn}");
                try
                {
                    nodeSchema = controller.Deserialize(schemaFile.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception($"can't deserialize schema ${schemaFile.Key}", ex);
                }

                sb.AppendLine(_graphSchemaService.CreateScript(nodeSchema));
            }
            return sb.ToString();
        }
    }
}
