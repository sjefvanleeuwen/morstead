using System;
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
    }
}
