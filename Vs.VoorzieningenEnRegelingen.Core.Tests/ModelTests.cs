using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class ModelTests
    {
        [Fact]
        public void Model_Parse_Yaml_To_Model()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.True(controller.GetSituation("standaardpremie", "alleenstaande").Expression == "1609");
            Assert.True(controller.GetSituation("maximaalvermogen", "aanvrager_met_toeslagpartner").Expression == "145136");
        }
    }
}
