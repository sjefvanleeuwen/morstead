using Flee.PublicTypes;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public class BpmnTask : IBpmnTask, ITarget
    {
        private readonly JToken o;

        public List<ExecutionListener> ExecutionListeners {get;set;}

        public string Id => o["@id"].Value<string>();
        public string Name => o["@name"].Value<string>();
        public List<string> Incoming { get; set; }
        public List<string> Outgoing { get; set; }

        public BpmnTask(JToken bpmnTask, ExpressionContext context)
        {
            this.o = bpmnTask;
            Incoming = new List<string> { o["bpmn:incoming"].Value<string>() };
            Outgoing = new List<string> { o["bpmn:outgoing"].Value<string>() };
            ExecutionListeners = new List<ExecutionListener>();
            // TODO: implement multiple execution listeners. (Sequential?) Ordered by Start End Event Type?
            var listener = new ExecutionListener(o, context);
            if (!string.IsNullOrEmpty(listener.DelegateExpression))
            {
                ExecutionListeners.Add(listener);
            }
        }
    }
}
