using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Vs.Rules.Core;
using Vs.Rules.Core.Model;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.Rules.Routing.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Service.Tests
{
    public class ServiceControllerTests
    {
        [Fact]
        public void Service_Parse_Yaml_Successfull()
        {
            ServiceController controller = new ServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var s = new ParseRequest() { Config = YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml") };
            var result = controller.Parse(s);
            Assert.False(result.IsError);
        }

        [Fact]
        public void Service_Parse_Yaml_Unsuccessfull()
        {
            ServiceController controller = new ServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var result = controller.Parse(new ParseRequest() { Config = "--- Garbage In ---" });
            Assert.True(result.IsError);
        }

        [Fact]
        public void Service_Execute_Zorgtoeslag_From_Url()
        {
            ServiceController controller = new ServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var executeRequest = new ExecuteRequest()
            {
                Config = YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"),
                Parameters = new ParametersCollection() {
                    new ClientParameter("alleenstaande", "ja", TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy")
                }
            };

            var payload = JsonConvert.SerializeObject(executeRequest);
            var result = controller.Execute(executeRequest);
            Assert.True(result.Questions.Parameters.Count == 1);
            Assert.True(result.Questions.Parameters[0].Name == "woonland");
        }

        private ILogger<ServiceController> InitMoqLogger()
        {
            var moqLogger = new Mock<ILogger<ServiceController>>();
            return moqLogger.Object;
        }

        private IRoutingController InitMoqRoutingController()
        {
            var moqRoutingController = new Mock<IRoutingController>();
            moqRoutingController.Setup(m => m.RoutingConfiguration).Returns(null as IRoutingConfiguration);
            return moqRoutingController.Object;
        }
    }
}
