using Flee.PublicTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Vs.Morstead.Bpm.Core;
using Vs.Morstead.Bpm.Model.Events;

namespace Vs.Morstead.Bpm.Model
{
    public class BpmnProcess
    {
        JObject o { get; set; }
        public ExpressionContext Context { get; set; }
        private JToken process;
        public string Id { get; set; }
        public StartEvent StartEvent { get; }
        public EndEvent EndEvent { get; }
        public SequenceFlow SequenceFlow { get; }

        public BpmnProcess(string bpmn)
        {
            var doc = XDocument.Parse(bpmn);
            Context = new ExpressionContext();
            o = JObject.Parse(JsonConvert.SerializeXNode(doc));
            process = o["bpmn:definitions"]["bpmn:process"];
            Id = process["@id"].Value<string>();
            StartEvent = new StartEvent(process);
            EndEvent = new EndEvent(process);
            SequenceFlow = new SequenceFlow(process, StartEvent, EndEvent, Context);
        }
    }
}
