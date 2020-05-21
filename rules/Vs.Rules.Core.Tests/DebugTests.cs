using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Vs.Rules.Core.Tests
{
    public class DebugTests
    {
        public ITestOutputHelper Output { get; }

        public DebugTests(ITestOutputHelper output)
        {
            Output = output;
        }

        [Theory]
        [InlineData("HeaderIncomplete.yaml")]
        [InlineData("FlowNoDefinition.yaml")]
        [InlineData("FlowEmptyDefinition.yaml")]
        [InlineData("FlowAmbiguousInputs.yaml")]
        [InlineData("HeaderIncomplete.yaml")]
        [InlineData("HeaderEmptyDefinition.yaml")]
        public async void ShouldReturnDebugInformation(string yamlFile)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{yamlFile}"));
            Assert.True(result.IsError);
            Assert.NotNull(result.DebugInfo);
            Output.WriteLine(result.Message);
        }
    }
}
