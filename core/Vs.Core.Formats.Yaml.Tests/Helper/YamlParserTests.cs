using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Tests.TestYaml;
using Vs.Core.Formats.Yaml.Helper;
using Xunit;

namespace Vs.Cms.Core.Tests.Helper
{
    public class YamlParserTests
    {
        [Fact]
        public void ShouldRenderContentYamlToObject()
        {
            var root = YamlParser.RenderYamlToObject(ContentYamlTest1.Body);
            Assert.Single(root);
            Assert.Equal("content", root.First().Key);
            Assert.Equal(typeof(List<object>), root.First().Value.GetType());

            var content = root.First().Value as List<object>;
            Assert.Equal(4, content.Count);

            Assert.Equal(typeof(Dictionary<string, object>), content[0].GetType());
            var item1 = content[0] as Dictionary<string, object>;
            Assert.Equal(7, item1.Count);
            Assert.Equal("Waar bent u woonachtig?", item1["vraag"]);
            Assert.Equal("Selecteer uw woonland.", item1["titel"]);
            Assert.Equal("Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.", item1["tekst"]);
            Assert.Empty(item1["label"].ToString());
            Assert.Empty(item1["tag"].ToString());
            Assert.Equal("Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.", item1["hint"]);

            Assert.Equal(typeof(Dictionary<string, object>), content[1].GetType());
            var item2 = content[1] as Dictionary<string, object>;
            Assert.Equal(6, item2.Count);

            Assert.Equal(typeof(Dictionary<string, object>), content[2].GetType());
            var item3 = content[2] as Dictionary<string, object>;
            Assert.Equal(2, item3.Count);

            Assert.Equal(typeof(Dictionary<string, object>), content[3].GetType());
            var item4 = content[3] as Dictionary<string, object>;
            Assert.Equal(2, item4.Count);
        }
    }
}
