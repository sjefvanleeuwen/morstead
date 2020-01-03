using System;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
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
            var result = controller.Execute(new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body,
                Parameters = new ParametersCollection() {
                    new Parameter("alleenstaande","ja")
                }
            });
            Assert.True(result.Questions.Parameters.Count == 1);
            Assert.True(result.Questions.Parameters[0].Name == "toetsingsinkomen_aanvrager");
        }
    }
}
