﻿using Nager.Country;
using System.Linq;
using Vs.Core.Layers.Controllers;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Core.Layers.Tests.Controllers
{
    public class LayerControllerTests
    {
        [Fact]
        public async void ShouldInitialize()
        {
            var yaml = YamlTestFileLoader.Load(@"Zorgtoeslag5.layers.yaml");
            var sut = new LayerController();
            await sut.Initialize(yaml);
            Assert.Equal(sut.LayerConfiguration.Start.Line, sut.LayerConfiguration.End.Line);
            Assert.Equal(sut.LayerConfiguration.Start.Col, sut.LayerConfiguration.End.Col);
            Assert.Equal(sut.LayerConfiguration.Start.Index, sut.LayerConfiguration.End.Index);
            Assert.Equal("1.0", sut.LayerConfiguration.Version);
            Assert.Equal(3, sut.LayerConfiguration.Layers.ToList().Count());
            Assert.Single(sut.LayerConfiguration.Layers.ElementAt(0).Contexts);
            Assert.Equal(3, sut.LayerConfiguration.Layers.ElementAt(1).Contexts.Count());
            Assert.Null(sut.LayerConfiguration.Layers.ElementAt(1).Contexts.ElementAt(0).Language);
            Assert.Equal(LanguageCode.EN, sut.LayerConfiguration.Layers.ElementAt(1).Contexts.ElementAt(1).Language);
            Assert.Equal(LanguageCode.AR, sut.LayerConfiguration.Layers.ElementAt(1).Contexts.ElementAt(2).Language);
            Assert.Single(sut.LayerConfiguration.Layers.ElementAt(2).Contexts);
        }
    }
}
