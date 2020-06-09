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
        [InlineData("FormulaEmptyDefinition")]
        [InlineData("FlowNoDefinition")]
        [InlineData("StepUnknownProperty")]
        [InlineData("FlowEmptyDefinition")]
        [InlineData("StepAmbiguousInputs")]
        [InlineData("HeaderIncomplete")]
        [InlineData("HeaderUnknownProperty")]
        [InlineData("HeaderEmptyDefinition")]
        [InlineData("YamlGarbage1")]
        [InlineData("YamlGarbage2")]
        [InlineData("YamlMalformed1")]
        public async void ShouldReturnDebugInformation(string yamlFile)
        {
            // IMPORTANT please the nl-NL language at the end of the collection, as other
            // tests are primarily tested under nl-NL yaml test files.
            foreach (var language in new[] { "nl-NL","en-US" }) {
                Globalization.SetKeywordResourceCulture(new System.Globalization.CultureInfo(language));
                Globalization.SetFormattingExceptionResourceCulture(new System.Globalization.CultureInfo(language));
                var controller = new YamlScriptController();
                var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{language}/{yamlFile}.yaml"));
                Assert.True(result.IsError);
                Assert.NotNull(result.Exceptions);
                Assert.NotNull(result.Message);
                Assert.True(result.Exceptions.Exceptions.Count == 1);
                Assert.NotNull(result.Exceptions.Exceptions[0].DebugInfo);
                Output.WriteLine($"{language} message: {result.Message} {result.Exceptions.Exceptions[0].DebugInfo}");
            }
        }

        [Theory]
        [InlineData("RootUnknownDefinitions")]
        public async void ShouldReturnDebugCollectionInformation(string yamlFile)
        {
            // IMPORTANT please the nl-NL language at the end of the collection, as other
            // tests are primarily tested under nl-NL yaml test files.
            foreach (var language in new[] { "en-US", "nl-NL" })
            {
                Globalization.SetKeywordResourceCulture(new System.Globalization.CultureInfo(language));
                Globalization.SetFormattingExceptionResourceCulture(new System.Globalization.CultureInfo(language));
                var controller = new YamlScriptController();
                var result = controller.Parse(YamlTestFileLoader.Load($"Malformed/{language}/{yamlFile}.yaml"));
                Assert.True(result.IsError);
                Assert.NotNull(result.Exceptions);
                Assert.NotNull(result.Message);
                Output.WriteLine(result.Message);
                foreach (var exception in result.Exceptions.Exceptions)
                {
                    Assert.NotNull(exception.Message);
                    Assert.NotNull(exception.DebugInfo);
                    Output.WriteLine($"{language}: {exception.Message} {exception.DebugInfo}");

                }
            }
        }
    }
}
