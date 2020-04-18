using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.Core.Tests
{
    public class ModelTests
    {
        [Fact]
        public void Model_Parse_Yaml_To_Model()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            Assert.True(controller.GetSituation("standaardpremie", "alleenstaande").Expression == "1609");
            Assert.True(controller.GetSituation("maximaalvermogen", "aanvrager_met_toeslagpartner").Expression == "145136");
        }

        [Fact]
        public void StuurInformatieTest()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            Assert.False(result.IsError);
            var header = controller.GetHeader();
            Assert.True(header.Onderwerp == "zorgtoeslag");
            Assert.True(header.Organisatie == "belastingdienst");
            Assert.True(header.Type == "toeslagen");
            Assert.True(header.Domein == "zorg");
            Assert.True(header.Versie == "1.0");
            Assert.True(header.Status == "ontwikkel");
            Assert.True(header.Jaar == "2019");
            Assert.True(header.Bron == "https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf");
        }

        [Fact]
        public void CanParseChoices()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag4.yaml"));
            Assert.False(result.IsError);
        }
    }
}
