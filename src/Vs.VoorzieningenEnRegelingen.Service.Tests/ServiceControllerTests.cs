using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Vs.Rules.Core;
using Vs.Rules.Core.Model;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.Rules.Routing.Model.Interfaces;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Service.Tests
{
    public class ServiceControllerTests
    {
        [Fact]
        public async void Service_Parse_Yaml_Successfull()
        {
            APIServiceController controller = new APIServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var s = new ParseRequest() { Config = YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml") };
            var result = await controller.Parse(s);
            Assert.False(result.IsError);
        }

        [Fact]
        public async void Service_Parse_Yaml_Unsuccessfull()
        {
            APIServiceController controller = new APIServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var result = await controller.Parse(new ParseRequest() { Config = "--- Garbage In ---" });
            Assert.True(result.IsError);
        }

        [Fact]
        public void Service_Execute_Zorgtoeslag_From_Url()
        {
            APIServiceController controller = new APIServiceController(InitMoqLogger(), new YamlScriptController(), InitMoqRoutingController());
            var executeRequest = new ExecuteRequest()
            {
                Config = YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"),
                Parameters = new ParametersCollection() {
                    new ClientParameter("alleenstaande", "ja", TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy")
                }
            };

            var payload = JsonConvert.SerializeObject(executeRequest);
            var result = controller.Execute(executeRequest).Result;
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
