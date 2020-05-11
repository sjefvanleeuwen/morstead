using Moq;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.Rules.Routing.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.Core.Tests
{
    public class ContentTests
    {
        [Fact]
        public void ShouldGenerateAValidContentYamlTemplate()
        {
            var controller = new YamlScriptController(InitMoqRoutingController());

            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            Assert.False(result.IsError);
            var template = controller.CreateYamlContentTemplate();
            Assert.False(string.IsNullOrEmpty(template));
        }

        private IRoutingController InitMoqRoutingController()
        {
            var moqRoutingController = new Mock<IRoutingController>();
            moqRoutingController.Setup(m => m.RoutingConfiguration).Returns(null as IRoutingConfiguration);
            return moqRoutingController.Object;
        }
    }
}
