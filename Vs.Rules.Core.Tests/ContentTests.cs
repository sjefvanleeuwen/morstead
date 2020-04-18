using Vs.Rules.Core;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class ContentTests
    {
        [Fact]
        public void ShouldGenerateAValidContentYamlTemplate()
        {
            var controller = new YamlScriptController();

            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            Assert.False(result.IsError);
            var template = controller.CreateYamlContentTemplate();
            Assert.False(string.IsNullOrEmpty(template));
        }
    }
}
