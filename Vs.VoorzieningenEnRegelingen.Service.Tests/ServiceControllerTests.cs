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
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
        }

        [Fact]
        public void Service_Parse_Yaml_Unsuccessfull()
        {
            ServiceController controller = new ServiceController(null);
            var result = controller.Parse("-- garbage in ---");
            Assert.True(result.IsError);
        }
    }
}
