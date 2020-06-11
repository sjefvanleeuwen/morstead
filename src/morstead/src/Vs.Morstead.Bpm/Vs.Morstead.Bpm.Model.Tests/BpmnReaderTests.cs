using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Vs.Morstead.Bpm.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Vs.Morstead.Bpm.Model.Tests
{
    public class BpmnReaderTests
    {
        public BpmnReaderTests(ITestOutputHelper output)
        {
            Output = output;
        }

        public ITestOutputHelper Output { get; }

        [Fact]
        public void CanReadSomeSimpleTasks()
        {
            var doc = XDocument.Parse(TestFileLoader.Load(@"Bpmn20/simple-task.bpmn"));

            string json = JsonConvert.SerializeXNode(doc);
            JObject rss = JObject.Parse(json);
            var process = rss["bpmn:definitions"]["bpmn:process"];
            var startEvent = process["bpmn:startEvent"]["bpmn:outgoing"].Value<string>();

            Output.WriteLine($"process start event: {startEvent}.");

            var endEvent = process["bpmn:endEvent"]["bpmn:incoming"].Value<string>();
            // step through the sequence
            string step = startEvent;
            while (step != endEvent)
            {
                var sequence = process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == step);
                var target = sequence["@targetRef"].Value<string>();
                // find target ref in tasks.
                var task = process["bpmn:task"].Where(p => p.Value<string>("@id") == target).Single();
                var doc2 = task["@name"].Value<string>();
                var incoming = task["bpmn:incoming"].Value<string>();
                Assert.Equal(step, incoming);
                var outgoing = task["bpmn:outgoing"].Value<string>();
                Output.WriteLine($"step executing task {target}: '{doc2}'. Next step {outgoing}.");
                step = outgoing;
            }
            Output.WriteLine($"process end event: {endEvent}.");
        }
    }
}
