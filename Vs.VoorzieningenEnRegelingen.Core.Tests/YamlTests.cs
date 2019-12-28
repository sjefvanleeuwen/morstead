using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Xunit;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    /// <summary></summary>
    public class YamlTests
    {
        private Dictionary<string, YamlMappingNode> Maps = new Dictionary<string, YamlMappingNode>();

        private YamlMappingNode Map(string body)
        {
            if (Maps.ContainsKey(body))
                return Maps[body];
            var input = new StringReader(body);
            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(input);
            // Examine the stream
            Maps.Add(body, (YamlMappingNode)yaml.Documents[0].RootNode);
            return (YamlMappingNode)yaml.Documents[0].RootNode;
        }

        private string Order(string s)
        {
            return String.Join(",", s.Trim(',').Split(',').OrderBy(i => i));
        }

        /// <summary>
        /// Determines whether this instance [can deserialize yaml].
        /// </summary>
        [Fact]
        public void Yaml_Can_Deserialize_Root_Nodes()
        {
            var map = Map(YamlScripts.YamlZorgtoeslag.Body);
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
            var map = Map(YamlScripts.YamlZorgtoeslag.Body);
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
            var map = Map(YamlScripts.YamlZorgtoeslag.Body);
            var s = "";
            var entries = (YamlMappingNode)map.Children[new YamlScalarNode("stuurinformatie")];
            foreach (var entry in entries)
            {
                s += entry.Value + ",";
            }
            Assert.True("1.0,2019,belastingdienst,https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf,productie,toeslagen,zorg,zorgtoeslag" == Order(s));
        }

        [Fact]
        public void Yaml_Can_Convert_To_Json()
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(new StringReader(YamlScripts.YamlZorgtoeslag.Body));

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            var json = serializer.Serialize(yamlObject);
        }

        [Fact]
        public void Yaml_Can_Parse_Formulas()
        {
            var yamlParser = new YamlParser(YamlScripts.YamlZorgtoeslag.Body, null);
            var functions = yamlParser.Formulas();
            Assert.True(functions.Count == 8);
            Assert.True(functions[1].Name == "maximaalvermogen");
            Assert.True(functions[1].IsSituational == true);
        }

        [Fact]
        public void Yaml_Can_Deserialize_Tables()
        {
            var yamlParser = new YamlParser(YamlScripts.YamlZorgtoeslag.Body, null);
            var tabellen = yamlParser.Tabellen();
            Assert.True(tabellen.Count == 2);
            Assert.True(tabellen[1].Name == "woonlandfactoren");
            Assert.True(tabellen[1].ColumnTypes.Count == 2);
            Assert.True(tabellen[1].ColumnTypes[0].Name == "woonland");
            Assert.True(tabellen[1].ColumnTypes[1].Name == "factor");
        }
    }
}
