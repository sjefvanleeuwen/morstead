using System.Linq;
using System.Threading.Tasks;
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
        private readonly IYamlSourceController _yamlSourceController;

        private IRoutingConfiguration RoutingConfiguration;
        private bool IsRoutingConfigurationSet;

        public RoutingController(IYamlSourceController yamlSourceController)
        {
            _yamlSourceController = yamlSourceController;
        }

        public async Task Initialize(string routerYaml = null)
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
                routerYaml = await YamlParser.ParseHelper(routerYaml);

                var deserializer = new DeserializerBuilder().Build();
                RoutingConfiguration = deserializer.Deserialize<RoutingConfiguration>(routerYaml);
            }
            IsRoutingConfigurationSet = true;
        }

        public async Task<string> GetParameterValue(string missingParameterName)
        {
            var url = RoutingConfiguration.Parameters.FirstOrDefault()?.Location;
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            var api = new ApiCalls.ACMEApiAnswerClient(new System.Net.Http.HttpClient(), url);
            try
            {
                var result = await api.GetByParameterNameAsync(missingParameterName);
                var value = result?.Parameters?.First()?.Value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }
                return value;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IRoutingConfiguration> GetRoutingConfiguration()
        {
            if (!IsRoutingConfigurationSet)
            {
                await Initialize();
            }
            return RoutingConfiguration;
        }
    }
}
