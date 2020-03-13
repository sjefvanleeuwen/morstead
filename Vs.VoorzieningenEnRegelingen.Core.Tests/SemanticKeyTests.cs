using Vs.VoorzieningenEnRegelingen.Core.Model.Content;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;
using YamlDotNet.Serialization;
using static Vs.VoorzieningenEnRegelingen.Core.TypeInference.InferenceResult;

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

            // TODO: put flat item list with nodes in a multibranch dependency tree...
            //var serializer = new YamlDotNet.Serialization.Serializer();
            //var yaml = serializer.Serialize(items);

            //List<string> tokens = new List<string>();

            //foreach (var item in items)
            //{
            //    tokens.Add(item.Name);
            //}
            //var tokenYaml = serializer.Serialize(tokens);

        }

        [Fact]
        public void CanDeserializeContentYaml()
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            var result = deserializer.Deserialize<ContentCollection>(YamlZorgtoeslag3Content.Body);
        }
    }
}
