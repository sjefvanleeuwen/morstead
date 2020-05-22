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
        [InlineData("FlowNoDefinition")]
        [InlineData("StepUnknownProperty")]
        [InlineData("FlowEmptyDefinition")]
        [InlineData("StepAmbiguousInputs")]
        [InlineData("HeaderIncomplete")]
        [InlineData("HeaderUnknownProperty")]
        [InlineData("HeaderEmptyDefinition")]
        public async void ShouldReturnDebugInformation(string yamlFile)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{yamlFile}.yaml"));
            Assert.True(result.IsError);
            Assert.NotNull(result.Exceptions);
            Assert.NotNull(result.Message);
            Assert.True(result.Exceptions.Exceptions.Count == 1);
            Assert.NotNull(result.Exceptions.Exceptions[0].DebugInfo);
            Output.WriteLine($"message: {result.Message} {result.Exceptions.Exceptions[0].DebugInfo}");
        }

        [Theory]
        [InlineData("RootUnknownDefinitions")]
        public async void ShouldReturnDebugCollectionInformation(string yamlFile)
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{yamlFile}.yaml"));
            Assert.True(result.IsError);
            Assert.NotNull(result.Exceptions);
            Assert.NotNull(result.Message);
            Output.WriteLine(result.Message);
            foreach (var exception in result.Exceptions.Exceptions)
            {
                Assert.NotNull(exception.Message);
                Assert.NotNull(exception.DebugInfo);
                Output.WriteLine($"message: {exception.Message} {exception.DebugInfo}");

            }
        }
    }
}
