using System.Collections.Generic;
using System.Threading.Tasks;
using Vs.Core.Layers.Enums;

namespace Vs.Core.Layers.Controllers.Interfaces
{
    public interface IYamlSourceController
    {
        Task SetYaml(YamlType yamlType, string yaml, Dictionary<string, object> filter = null);
        string GetYaml(YamlType yamlType, Dictionary<string, object> filter = null);
    }
}