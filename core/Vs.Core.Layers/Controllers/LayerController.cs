using System;
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

        public void Initialize(Uri layerUri)
        {
            Initialize(layerUri.ToString());
        }

        public void Initialize(string layerYaml)
        {
            var yaml = YamlParser.ParseHelper(layerYaml);

            var deserializer = new DeserializerBuilder().Build();
            LayerConfiguration = deserializer.Deserialize<LayerConfiguration>(yaml);
        }
    }
}
