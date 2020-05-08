using Moq;
using System.Linq;
using Vs.Core.Layers.Controllers.Interfaces;
using Vs.Core.Layers.Enums;
using Vs.Rules.Routing.Controllers;
using Xunit;

namespace Vs.Rules.Routing.Tests.Controllers
{
    public class RoutingControllerTests
    {
        [Fact]
        public void ShouldNotParseIfNothingAvailable()
        {
            var moqYamlSourceController = new Mock<IYamlSourceController>();
            moqYamlSourceController.Setup(m => m.GetYaml(YamlType.Routing, null)).Returns(null as string);
            var sut = new RoutingController(moqYamlSourceController.Object);
            Assert.Null(sut.RoutingConfiguration);
        }

        [Fact]
        public void ShouldParse()
        {
            var moqYamlSourceController = new Mock<IYamlSourceController>();
            moqYamlSourceController.Setup(m => m.GetYaml(YamlType.Routing, null)).Returns(@"# Zorgtoeslag routing for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 5.0
  status: ontwikkel
  jaar: 2019
parameters:
 - waarde: woonland
   locatie: woonlandfactorUrl");
            var sut = new RoutingController(moqYamlSourceController.Object);
            Assert.Single(sut.RoutingConfiguration.Parameters);
            Assert.Equal("woonland", sut.RoutingConfiguration.Parameters.ElementAt(0).Name);
            Assert.Equal("woonlandfactorUrl", sut.RoutingConfiguration.Parameters.ElementAt(0).Location);
        }
    }
}
