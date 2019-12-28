using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    /// <summary>
    /// Tests simple conditional flow engine
    /// </summary>
    public class FlowTests
    {
        [Fact]
        public void Flow_Zorgtoeslag_Model_Flow_Deserialization()
        {
            var parser = new YamlParser(YamlZorgtoeslag.Body, null);
            var steps = parser.Flow();
            Assert.True(steps.Count == 5);
            Assert.True((from p in steps where p.Formula == "toetsingsinkomen" select p).Single().Situation == "");
            Assert.True((from p in steps where p.Situation == "buitenland" select p).Single().Description == "bereken de zorgtoeslag wanner men in het buitenland woont");
            Assert.True((from p in steps where p.Name == "4" select p).Single().Description == "bereken de zorgtoeslag wanneer men binnen nederland woont");

        }
    }
}
