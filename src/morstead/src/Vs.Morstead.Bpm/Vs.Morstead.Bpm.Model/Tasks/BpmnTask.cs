using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public class BpmnTask : IBpmnTask
    {
        private readonly JToken o;

        public List<ExecutionListener> ExecutionListeners {get;set;}

        public string Id => o["@id"].Value<string>();
        public string Name => o["@name"].Value<string>();
        public string Incoming => o["bpmn:incoming"].Value<string>();
        public string Outgoing => o["bpmn:outgoing"].Value<string>();

        public BpmnTask(JToken process, string id)
        {
            this.o = process["bpmn:task"].Where(p => p.Value<string>("@id") == id).Single();
            ExecutionListeners = new List<ExecutionListener>();
            // TODO: implement multiple execution listeners. (Sequential?) Ordered by Start End Event Type?
            var listener = new ExecutionListener(o);
            if (!string.IsNullOrEmpty(listener.DelegateExpression))
            {
                ExecutionListeners.Add(listener);
            }
        }
    }
}
