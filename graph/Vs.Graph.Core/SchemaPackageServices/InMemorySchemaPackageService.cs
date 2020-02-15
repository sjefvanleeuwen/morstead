using System.Collections.Generic;
using System.Linq;

namespace Vs.Graph.Core.SchemaPackageServices
{
    public class InMemorySchemaPackageService : ISchemaPackageStorageService
    {
        public InMemorySchemaPackageService()
        {
            Files = new Dictionary<string, string>();
        }

        public Dictionary<string,string> Files { get; set; }

        public IEnumerable<KeyValuePair<string,string>> GetFiles(string migrationUrn) =>
            Files.Where(p => p.Key.StartsWith(migrationUrn));

        public string Load(string urn)
        {
            return Files[urn];
        }
    }
}
