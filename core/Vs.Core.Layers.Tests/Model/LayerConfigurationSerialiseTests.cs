using Nager.Country;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Layers.Model;
using Vs.Core.Layers.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.Core.Layers.Tests.Model
{
    public class LayerConfigurationSerialiseTests
    {
        [Fact]
        public void ShouldSerialize()
        {
            var layerConfiguration = new LayerConfiguration()
            {
                Version = "1.0",
                Layers = new List<Layer>
                {
                    {
                        new Layer { Name = "rules", Contexts = new List<Context> { new Context { Endpoint = new System.Uri("https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.routing.yaml") } } }
                    },
                    {
                         new Layer { Name = "uxcontent", Contexts = new List<Context> {
                            new Context { Endpoint = new System.Uri("https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.nl.yaml") },
                            new Context { Endpoint = new System.Uri("https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.en.yaml"),
                                          Language = LanguageCode.EN },
                            new Context { Endpoint = new System.Uri("https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.ar.yaml"),
                                          Language = LanguageCode.AR } } }
                    },
                    {
                        new Layer { Name = "routing", Contexts = new List<Context> { new Context { Endpoint = new System.Uri("https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.routing.yaml") } } }
                    }
                }
            };
            var serializer = new Serializer();
            var result = serializer.Serialize(layerConfiguration);
            Assert.Equal(@"version: 1.0.0
layers:
  rules:
  - context: https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.routing.yaml
    language: 
  uxcontent:
  - context: https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.nl.yaml
    language: 
  - context: https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.en.yaml
    language: EN
  - context: https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.uxcontent.ar.yaml
    language: AR
  routing:
  - context: https://virtualsociety.nl/zorgtoeslag/v1.0/zorgtoeslag.routing.yaml
    language: 
", result);
        }

        [Fact]
        public void ShouldDeserialize()
        {
            var yaml = YamlTestFileLoader.Load(@"Zorgtoeslag5.layers.yaml");
            var deserializer = new DeserializerBuilder().Build();
            var result = deserializer.Deserialize<LayerConfiguration>(yaml);
            Assert.Equal("1.0", result.Version);
            Assert.Equal(3, result.Layers.ToList().Count());
            Assert.Single(result.Layers.ElementAt(0).Contexts);
            Assert.Equal(3, result.Layers.ElementAt(1).Contexts.Count());
            Assert.Null(result.Layers.ElementAt(1).Contexts.ElementAt(0).Language);
            Assert.Equal(LanguageCode.EN, result.Layers.ElementAt(1).Contexts.ElementAt(1).Language);
            Assert.Equal(LanguageCode.AR, result.Layers.ElementAt(1).Contexts.ElementAt(2).Language);
            Assert.Single(result.Layers.ElementAt(2).Contexts);
            Assert.True(true);
        }
    }
}
