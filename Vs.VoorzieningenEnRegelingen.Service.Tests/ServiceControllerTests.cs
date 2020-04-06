using Newtonsoft.Json;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Service.Tests
{
    public class ServiceControllerTests
    {
        [Fact]
        public void Service_Parse_Yaml_Successfull()
        {
            ServiceController controller = new ServiceController(null);
            var s = new ParseRequest() { Config = YamlZorgtoeslag.Body };
            var o = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            var result = controller.Parse(s);
            Assert.False(result.IsError);
        }

        [Fact]
        public void Service_Parse_Yaml_Unsuccessfull()
        {
            ServiceController controller = new ServiceController(null);
            var result = controller.Parse(new ParseRequest() { Config = "--- Garbage In ---" });
            Assert.True(result.IsError);
        }

        [Fact]
        public void Service_Execute_Zorgtoeslag_From_Url()
        {
            ServiceController controller = new ServiceController(null);
            var executeRequest = new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body,
                Parameters = new ParametersCollection() {
                    new ClientParameter("alleenstaande", "ja", TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy")
                }
            };

            var payload = JsonConvert.SerializeObject(executeRequest);
            var result = controller.Execute(executeRequest);
            Assert.True(result.Questions.Parameters.Count == 1);
            Assert.True(result.Questions.Parameters[0].Name == "woonland");
        }
    }
}
