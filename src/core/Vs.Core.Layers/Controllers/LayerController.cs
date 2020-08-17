using System;
using System.Threading.Tasks;
using Vs.Core.Formats.Yaml.Helper;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Model;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Controllers
{
    public class LayerController : ILayerController
    {
        public LayerConfiguration LayerConfiguration { get; set; }

        public LayerController()
        {

        }

        public async Task Initialize(Uri layerUri)
        {
            await Initialize(layerUri.ToString());
        }

        public async Task Initialize(string layerYaml)
        {
            var yaml = await YamlParser.ParseHelper(layerYaml);

            var deserializer = new DeserializerBuilder().Build();
            LayerConfiguration = deserializer.Deserialize<LayerConfiguration>(yaml);
        }
    }
}
