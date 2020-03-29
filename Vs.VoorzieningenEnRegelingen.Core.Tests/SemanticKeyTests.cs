using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class SemanticKeyTests
    {
        [Fact]
        public void CanDiscoverAllSemanticKeysAndBindToParameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag4.Body);

            Assert.False(result.IsError);
            Assert.True(controller.ContentNodes.Count == 16);
            List<string> keys = new List<string>();
            foreach (var item in controller.ContentNodes)
            {
                Assert.NotNull(item.Parameter);
                Assert.NotNull(item.Parameter.SemanticKey);
                Assert.True(item.Parameter.SemanticKey == item.Name);
                keys.Add(item.Name);
            }
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void CanDeserializeContentYaml()
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }
    }
}
