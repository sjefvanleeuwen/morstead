using System.Collections.Generic;

namespace Vs.Graph.Core.SchemaPackageServices
{
    public interface ISchemaPackageStorageService
    {
        string Load(string uri);
        IEnumerable<KeyValuePair<string, string>> GetFiles(string migrationUrn);
    }
}