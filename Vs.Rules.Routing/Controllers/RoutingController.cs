using Vs.Core.Formats.Yaml.Helper;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Rules.Routing.Model;
using YamlDotNet.Serialization;

namespace Vs.Rules.Routing.Controllers
{
    public class RoutingController
    {
        public RoutingConfiguration RoutingConfiguration { get; set; }

        private readonly IYamlSourceController _yamlSourceController;

        public RoutingController(IYamlSourceController yamlSourceController)
        {
            _yamlSourceController = yamlSourceController;
            string routerYaml = null;
            try
            {
                routerYaml = _yamlSourceController.GetYaml(YamlType.Routing);
            }
            catch
            {
                
            }
            if (routerYaml != null)
            {
                //convert http to yaml
                routerYaml = YamlParser.ParseHelper(routerYaml);

                var deserializer = new DeserializerBuilder().Build();
                RoutingConfiguration = deserializer.Deserialize<RoutingConfiguration>(routerYaml);
            }
        }
    }
}
