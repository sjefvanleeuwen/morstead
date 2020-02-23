using Itenso.TimePeriod;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vs.Graph.Core.Data;
using Xunit;

namespace Vs.Graph.Core.Tests
{
    public class TestNode : INode
    {
        public int Id { get; set; }
        public TimeRange Periode { get; set; }

        public string Name { get; set; }
    }


    /// <summary>
    /// https://json-ld.org/
    /// </summary>
    public class JsonLdTests
    {

        [Fact]
        public void CreateJsonLdFromNode()
        {
            var node = new TestNode();
            JObject rss = JObject.FromObject(node);
            // Add namespace
            rss["@context"] = $"https://www.virtualsociety.nl/context/{node.GetType().Namespace}/{node.GetType().Name}.jsonld";
            rss["@id"] = $"https://www.virtualsociety.nl/resource/{node.GetType().Namespace}/{node.GetType().Name}/{node.Id}";
            //JsonConvert..SerializeObject(node);
        }


        /*
          {
          "@context": "https://json-ld.org/contexts/person.jsonld",
          "@id": "http://dbpedia.org/resource/John_Lennon",
          "name": "John Lennon",
          "born": "1940-10-09",
          "spouse": "http://dbpedia.org/resource/Cynthia_Lennon"
}
        */
        public void CreateJsonLdFromSchema()
        {
            var schemaController = new SchemaController(null);
            var nameSpace = "Vs.GraphData";
            var schema = schemaController.Deserialize(@"Name: codeTest
Attributes:
- Name: term
  Type: text
- Name: omschrijving
  Type: text
- Name: versie
  Type: int
- Name: periode
  Type: periode
");
            //var code = schemaController.CreateJsonLd<INode>(schema, nameSpace);
        }
    }
}
