using Flee.PublicTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public class BpmnTask : IBpmnTask, ITarget
    {
        private readonly ExpressionContext _context;
        private readonly JToken o;

        public List<ExecutionListener> ExecutionListeners {get;set;}

        public string Id => o["@id"].Value<string>();
        public string Name => o["@name"].Value<string>();
        public List<string> Incoming { get; set; }
        public List<string> Outgoing { get; set; }

        //public Dictionary<string,object> InputParameters { get; set; }
        public Dictionary<string,object> OutputParameters { get; set; }

        private void LoadParameters()
        {
            var io = o["bpmn:extensionElements"];
            if (io == null)
                return;
            io = io["camunda:inputOutput"];
            if (io != null)
            {
                OutputParameters = new Dictionary<string, object>();
                string name, value;
                switch (io["camunda:outputParameter"].Type)
                {
                    case JTokenType.Object:
                        name = io["camunda:outputParameter"]["@name"].Value<string>().ToLower();
                        value = io["camunda:outputParameter"]["#text"].Value<string>();
                        OutputParameters.Add(name, value);
                        // Todo Infer, reuse inference from vs.core (rules)?
                        // Todo Infer, reuse inference from vs.core (rules)?
                        try
                        {
                            _context.Variables.Add(name, double.Parse(value));
                        }
                        catch (Exception ex)
                        {
                            _context.Variables.Add(name, value);
                        }
                        break;
                    case JTokenType.Array:
                        foreach (var item in io["camunda:outputParameter"].Children())
                        {
                            name = item["@name"].Value<string>().ToLower();
                            value = item["#text"].Value<string>();
                            OutputParameters.Add(name, value);
                            // Todo Infer, reuse inference from vs.core (rules)?
                            try
                            {
                                _context.Variables.Add(name, double.Parse(value));
                            }
                            catch (Exception ex)
                            {
                                _context.Variables.Add(name, value);
                            }
                        }
                        break;
                    default:
                        throw new Exception("Element 'camunda:outputParameter' expected a single item or an array.");
                }
            }
        }

        public BpmnTask(JToken bpmnTask, ExpressionContext context)
        {
            _context = context;
            this.o = bpmnTask;
            Incoming = new List<string> { o["bpmn:incoming"].Value<string>() };
            Outgoing = new List<string> { o["bpmn:outgoing"].Value<string>() };
            LoadParameters();
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
