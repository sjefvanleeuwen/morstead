using System.Data;
using System.IO;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    /// <summary></summary>
    public class YamlTests
    {
        private string Order(string s)
        {
            return string.Join(",", s.Trim(',').Split(',').OrderBy(i => i));
        }

        /// <summary>
        /// Determines whether this instance [can deserialize yaml].
        /// </summary>
        [Fact]
        public void Yaml_Can_Deserialize_Root_Nodes()
        {
            var map = YamlParser.Map(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            // Load the stream
            var s = "";
            foreach (var entry in map.Children)
            {
                s += (((YamlScalarNode)entry.Key).Value) + ",";
            }
            Assert.True("berekening,formules,stuurinformatie,tabellen" == Order(s));
        }

        [Fact]
        public void Yaml_Passes_AttributeNaming_Stuurinformatie()
        {
            var map = YamlParser.Map(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            var s = "";
            foreach (var entry in (YamlMappingNode)map.Children[new YamlScalarNode("stuurinformatie")])
            {
                s += (((YamlScalarNode)entry.Key).Value) + ",";
            }
            Assert.True("bron,domein,jaar,onderwerp,organisatie,status,type,versie" == Order(s));
        }
        [Fact]
        public void Yaml_Passes_AttributeValues_Stuurinformatie()
        {
            var map = YamlParser.Map(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"));
            var s = "";
            var entries = (YamlMappingNode)map.Children[new YamlScalarNode("stuurinformatie")];
            foreach (var entry in entries)
            {
                s += entry.Value + ",";
            }
            Assert.True("1.0,2019,belastingdienst,https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf,ontwikkel,toeslagen,zorg,zorgtoeslag" == Order(s));
        }

        [Fact]
        public void Yaml_Can_Convert_To_Json()
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(new StringReader(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml")));

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            var json = serializer.Serialize(yamlObject);
        }

        [Fact]
        public void Yaml_Can_Parse_Formulas()
        {
            var yamlParser = new YamlParser(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"), null);
            var functions = yamlParser.Formulas();
            Assert.True(functions.Count() == 11);
            Assert.True(functions.ElementAt(1).Name == "maximaalvermogen");
            Assert.True(functions.ElementAt(1).IsSituational == true);
        }

        [Fact]
        public void Yaml_Can_Deserialize_Tables()
        {
            var yamlParser = new YamlParser(YamlTestFileLoader.Load(@"Rijksoverheid/Zorgtoeslag.yaml"), null);
            var tabellen = yamlParser.Tabellen();
            Assert.True(tabellen.Count() == 1);
            var tabel = tabellen.Single();
            Assert.True(tabel.Name == "woonlandfactoren");
            Assert.True(tabel.ColumnTypes.Count == 2);
            Assert.True(tabel.ColumnTypes[0].Name == "woonland");
            Assert.True(tabel.ColumnTypes[1].Name == "factor");
        }
    }
}
