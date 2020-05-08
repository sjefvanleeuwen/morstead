using System.Collections.Generic;
using Vs.Core.Layers.Enums;

namespace Vs.Core.Layers.Controllers.Interfaces
{
    public interface IYamlSourceController
    {
        void SetYaml(YamlType yamlType, string yaml, Dictionary<string, object> filter = null);
        string GetYaml(YamlType yamlType, Dictionary<string, object> filter = null);
    }
}