using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml.Linq;
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
            process = new BpmnProcess(TestFileLoader.Load(@"Bpmn20/simple-task.bpmn"));
        }

        public ITestOutputHelper Output { get; }

        private BpmnProcess process;

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

                // execution listener
                var ext = task["bpmn:extensionElements"];
                if (ext != null)
                {
                    var listener = ext["camunda:executionListener"];
                    if (listener != null)
                    {
                        var exp = listener["@delegateExpression"].Value<string>();
                        // triggered at start of task or end.
                        var evtType = listener["@event"].Value<string>();
                        var io = ext["camunda:inputOutput"];
                        if (io  != null)
                        {
                            var parameters = new System.Collections.Generic.Dictionary<string, string>();
                            foreach (var item in io["camunda:outputParameter"].Children())
                            {
                                var name = item["@name"].Value<string>();
                                var value = item["#text"].Value<string>();
                                parameters.Add(name, value);

                            }
                            Assert.Equal(4, parameters.Count);
                            Assert.Equal("UnitTestFrom@UnitTest.com", parameters["From"]);
                            Assert.Equal("UnitTestTo@UnitTest.com", parameters["To"]);
                            Assert.Equal("This a Test Email Topic", parameters["Topic"]);
                            Assert.Equal("This is the body of the email message", parameters["Content"]);
                        }
                    }
                }
                
                Output.WriteLine($"step executing task {target}: '{doc2}'. Next step {outgoing}.");
                step = outgoing;
            }
            Output.WriteLine($"process end event: {endEvent}.");
        }

        [Fact]
        public void CanReadProcessInfo()
        {
            Assert.Equal("Process_090fe9b2-1216-4737-abe9-16f42a3a18aa", process.Id);
            Assert.Equal("StartEvent_1", process.StartEvent.Id);
            Assert.Equal("Flow_1lx2iho", process.StartEvent.Outgoing);
            Assert.Equal("Event_0m6k39u", process.EndEvent.Id);
            Assert.Equal("Flow_1jjq12z", process.EndEvent.Incoming);
        }

        [Fact]
        public void CanGetProcessFlow()
        {
            var task = process.SequenceFlow.Next();
            Assert.Equal("Activity_1ch68uj", task.Id);
            Assert.Equal("Send Email", task.Name);
            Assert.Equal("Flow_1lx2iho", task.Incoming);
            Assert.Equal("Flow_1fntof2", task.Outgoing);

            task = process.SequenceFlow.Next();
            Assert.Equal("Activity_1ietc9u", task.Id);
            Assert.Equal("task 2", task.Name);
            Assert.Equal("Flow_1fntof2", task.Incoming);
            Assert.Equal("Flow_1jjq12z", task.Outgoing);
        }
    }
}
