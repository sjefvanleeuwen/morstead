using Vs.Core.Layers.Controllers;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Core.Layers.Tests.Controllers
{
    public class LayerControllerTests
    {
        [Fact]
        public void ShouldBeConstructed()
        {
            var yaml = YamlTestFileLoader.Load(@"Zorgtoeslag5.rules.yaml");
            var sut = new LayerController(yaml);
            Assert.True(true);
        }
    }
}
