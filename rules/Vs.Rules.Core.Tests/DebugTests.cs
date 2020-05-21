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
        [InlineData("HeaderIncomplete")]
        [InlineData("FlowNoDefinition")]
        [InlineData("StepUnknownProperty")]
        [InlineData("FlowEmptyDefinition")]
        [InlineData("StepAmbiguousInputs")]
        [InlineData("HeaderIncomplete")]
        [InlineData("HeaderUnknownProperty")]
        [InlineData("HeaderEmptyDefinition")]
        [InlineData("RootUnknownDefinitions")]
        public async void ShouldReturnDebugInformation(string yamlFile)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{yamlFile}.yaml"));
            Assert.True(result.IsError);
            Assert.NotNull(result.DebugInfo);
            Output.WriteLine(result.Message);
        }
    }
}
