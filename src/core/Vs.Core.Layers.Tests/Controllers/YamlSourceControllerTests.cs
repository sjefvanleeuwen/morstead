using Moq;
using Nager.Country;
using System;
using System.Collections.Generic;
using Vs.Core.Layers.Controllers;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Core.Layers.Exceptions;
using Vs.Core.Layers.Model;
using Vs.Core.Layers.Model.Interfaces;
using Xunit;

namespace Vs.Core.Layers.Tests.Controllers
{
    public class YamlSourceControllerTests
    {
        [Fact]
        public async void ShouldSetYaml()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Rules, "TestYamlString");
            Assert.Equal("TestYamlString", sut.GetYaml(YamlType.Rules));
        }

        [Fact]
        public async void ShouldOverwriteSetYaml()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Rules, "TestYamlString");
            Assert.Equal("TestYamlString", sut.GetYaml(YamlType.Rules));
            await sut.SetYaml(YamlType.Rules, "TestYamlString2");
            Assert.Equal("TestYamlString2", sut.GetYaml(YamlType.Rules));
        }

        [Fact]
        public async void ShouldSetYamlUrl()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Rules, "http://yamlurl");
            Assert.Equal("http://yamlurl/", sut.GetYaml(YamlType.Rules));
        }

        [Fact]
        public async void ShouldSetYamlLayerUrl()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "rules",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml")
                            }
                        }
                    }
                }
            });
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Equal("http://testurlyaml/", sut.GetYaml(YamlType.Rules));
        }

        [Fact]
        public async void ShouldGetYamlByPriority()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "rules",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml")
                            }
                        }
                    }
                }
            });

            var sut = new YamlSourceController(moqLayerController.Object);

            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Equal("http://testurlyaml/", sut.GetYaml(YamlType.Rules));

            //set by url
            await sut.SetYaml(YamlType.Rules, "http://yamlurl");
            Assert.Equal("http://yamlurl/", sut.GetYaml(YamlType.Rules));

            //set the layer again -> no changes because url overrules
            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Equal("http://yamlurl/", sut.GetYaml(YamlType.Rules));

            //override with yaml (no url)
            await sut.SetYaml(YamlType.Rules, "TestYamlString");
            Assert.Equal("TestYamlString", sut.GetYaml(YamlType.Rules));
        }

        [Fact]
        public async void ShouldSetFilterAndGetByFilterYaml()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Uxcontent, "dummy", new Dictionary<string, object> { { "Language", LanguageCode.RU } });
            await sut.SetYaml(YamlType.Uxcontent, "dummy2", new Dictionary<string, object> { { "Language", "testString" } });
            await sut.SetYaml(YamlType.Uxcontent, "dummy3");
            Assert.Equal("dummy3", sut.GetYaml(YamlType.Uxcontent));
            Assert.Equal("dummy", sut.GetYaml(YamlType.Uxcontent, new Dictionary<string, object> { { "Language", LanguageCode.RU } }));
            Assert.Equal("dummy2", sut.GetYaml(YamlType.Uxcontent, new Dictionary<string, object> { { "Language", "testString" } }));
        }

        [Fact]
        public async void ShouldSetFilterAndGetByFilterFromLayer()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "rules",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml"),
                                Language = LanguageCode.RU
                            }
                        }
                    }
                }
            });
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Equal("http://testurlyaml/", sut.GetYaml(YamlType.Rules, new Dictionary<string, object> { { "Language", LanguageCode.RU } }));
        }

        [Fact]
        public async void ShouldThowExceptionWhenFilterRequestedDoesNotExist()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "rules",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml"),
                                Language = LanguageCode.RU
                            }
                        }
                    }
                }
            });
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Throws<LayersYamlException>(() => sut.GetYaml(YamlType.Rules, new Dictionary<string, object> { { "Language", LanguageCode.NL } }));
        }

        [Fact]
        public async void ShouldThrowExceptionWhenFilterRequestedIsOfWrongType()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "rules",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml"),
                                Language = LanguageCode.RU
                            }
                        }
                    }
                }
            });
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Layer, "dummy");
            Assert.Throws<LayersYamlException>(() => sut.GetYaml(YamlType.Rules, new Dictionary<string, object> { { "Language", "RU" } }));
        }

        [Fact]
        public void ShouldThrowErrorWhenNotFound()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            Assert.Throws<LayersYamlException>(() => sut.GetYaml(YamlType.Routing));
        }

        [Fact]
        public async void ShouldThrowExceptionWhenFilterDoesNotMatch()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            await sut.SetYaml(YamlType.Uxcontent, "dummy", new Dictionary<string, object> { { "Language", LanguageCode.RU } });
            Assert.Throws<LayersYamlException>(() => sut.GetYaml(YamlType.Uxcontent, new Dictionary<string, object> { { "Language", LanguageCode.EN } }));
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldThrowExceptionWhenUnknowYamlTypeIsProvided()
        {
            var moqLayerController = new Mock<ILayerController>();
            moqLayerController.Setup(m => m.LayerConfiguration).Returns(new LayerConfiguration
            {
                Layers = new List<Layer>
                {
                    new Layer
                    {
                        Name = "unknownYamlType",
                        Contexts = new List<Context>
                        {
                            new Context
                            {
                                Endpoint = new Uri("http://testurlyaml")
                            }
                        }
                    }
                }
            });
            var sut = new YamlSourceController(moqLayerController.Object);
            await Assert.ThrowsAsync<LayersYamlException>(() => sut.SetYaml(YamlType.Layer, "dummy"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenInvalidUriIsProvided()
        {
            var moqLayerController = new Mock<ILayerController>();
            var sut = new YamlSourceController(moqLayerController.Object);
            Assert.ThrowsAsync<LayersYamlException>(() => sut.SetYaml(YamlType.Rules, "httpNotAUri"));
        }
    }
}
