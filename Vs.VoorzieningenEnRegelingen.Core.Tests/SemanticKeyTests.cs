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
            var result = controller.Parse(YamlZorgtoeslag3.Body);

            Assert.False(result.IsError);
            Assert.True(controller.ContentNodes.Count == 28);
        }

        [Fact]
        public void CanDeserializeContentYaml()
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            //var result = deserializer.Deserialize<ContentCollection>(YamlZorgtoeslag3Content.Body);
        }
    }
}
