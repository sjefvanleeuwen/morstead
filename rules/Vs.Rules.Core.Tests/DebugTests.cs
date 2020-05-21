using System;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.Core.Tests
{
    public class DebugTests
    {
        [Theory]
        [InlineData("HeaderIncomplete.yaml")]
        [InlineData("FlowNoDefinition.yaml")]
        [InlineData("FlowEmptyDefinition.yaml")]
        [InlineData("FlowAmbiguousInputs.yaml")]
        public async void ShouldReturnDebugInformation(string yamlFile)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{yamlFile}"));
            Assert.True(result.IsError);
            Assert.NotNull(result.DebugInfo);
        }
    }
}
