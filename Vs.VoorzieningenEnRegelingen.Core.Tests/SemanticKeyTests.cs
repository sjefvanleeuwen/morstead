using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class SemanticKeyTests
    {
        [Fact]
        [Trait("Category", "Unfinished")]
        public void BuildTreeForContent()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag4.Body);

            Assert.False(result.IsError);
            Assert.True(controller.ContentNodes.Count == 16);
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void CanDeserializeContentYaml()
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }
    }
}
