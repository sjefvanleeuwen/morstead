using SmartFormat;
using System.Dynamic;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Vs.Rules.Core.Tests
{
    /// <summary>
    /// Unit tests for globalization
    /// </summary>
    public class GlobalizationTests
    {
        [Theory]
        [InlineData("nl-NL")] // IMPORTANT please keep this at top as other unit tests by default use the nl-NL test files as their default culture.
        [InlineData("en-US")]
        public void ShouldReturnCorrectCultureForKeywords(string culture)
        {
            Globalization.SetKeywordResourceCulture(new CultureInfo(culture));
            YamlRuleParser parser = new YamlRuleParser(YamlTestFileLoader.Load($"Globalization/rule.{culture}.yaml"), null);
            Assert.NotEmpty(parser.Flow());
            Assert.NotNull(parser.Header());
        }

        public void ShouldSmartFormatFieldsFromDifferentOjects()
        {
            dynamic keywords = new ExpandoObject();
            keywords.header = "stuurinformatie";
            var field = "a field";
            var result = Smart.Format("Hello {header} {field}.", new { keywords.header, field });
            Assert.Equal(result, "Hello stuurinformatie a field.");
        }
    }
}
