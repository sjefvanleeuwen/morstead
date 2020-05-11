using Vs.Core.Formats.Yaml.Helper;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.Rules.Routing.Model;
using Vs.Rules.Routing.Model.Interfaces;
using YamlDotNet.Serialization;

namespace Vs.Rules.Routing.Controllers
{
    public class RoutingController : IRoutingController
    {
        private IRoutingConfiguration routingConfiguration;
        private bool routingConfigurationSet;
        public IRoutingConfiguration RoutingConfiguration
        {
            get
            {
                if (!routingConfigurationSet)
                {
                    Initialize();
                }
                return routingConfiguration;
            }
        }

        private readonly IYamlSourceController _yamlSourceController;

        public RoutingController(IYamlSourceController yamlSourceController)
        {
            _yamlSourceController = yamlSourceController;
        }

        public void Initialize(string routerYaml = null)
        {
            if (routerYaml == null)
            {
                try
                {
                    routerYaml = _yamlSourceController.GetYaml(YamlType.Routing);
                }
                catch
                {

                }
            }
            if (routerYaml != null)
            {
                //convert http to yaml
                routerYaml = YamlParser.ParseHelper(routerYaml);

                var deserializer = new DeserializerBuilder().Build();
                routingConfiguration = deserializer.Deserialize<RoutingConfiguration>(routerYaml);
            }
            routingConfigurationSet = true;
        }

        public string GetParameterValue(string missingParameterName)
        {
            return "Nederland";
        }
    }
}
