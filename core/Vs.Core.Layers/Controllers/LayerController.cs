using System;
using Vs.Core.Formats.Yaml.Helper;
using Vs.Core.Layers.Model;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Controllers
{
    public class LayerController
    {
        public LayerConfiguration LayerConfiguration { get; set; }

        public LayerController(Uri layerUri)
        {
            Initialize(layerUri.ToString());
        }

        public LayerController(string layerYaml)
        {
            Initialize(layerYaml);
        }

        private void Initialize(string layerYaml)
        {
            var yaml = YamlParser.ParseHelper(layerYaml);

            var deserializer = new DeserializerBuilder().Build();
            LayerConfiguration = deserializer.Deserialize<LayerConfiguration>(yaml);
        }
    }
}
