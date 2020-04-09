using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class ContentTests
    {
        [Fact]
        public void ShouldGenerateAValidContentYamlTemplate()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag5.Body);
            Assert.False(result.IsError);
            var template = controller.CreateYamlContentTemplate();
            Assert.False(string.IsNullOrEmpty(template));
        }
    }
}
