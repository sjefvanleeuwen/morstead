using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Morstead.Bpm.Model.Events;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public class ExecutionListener
    {
        public string DelegateExpression { get; set; }
        public string DelegateInterface => DelegateExpression.Split('.')[0];
        public string DelegateMethod => DelegateExpression.Split('.')[1];

        public EventTypes EventType { get; set; }

        public Dictionary<string, string> OutputParameters { get; set; }

        public ExecutionListener(JToken task)
        {
            var ext = task["bpmn:extensionElements"];
            if (ext != null)
            {
                var listener = ext["camunda:executionListener"];
                if (listener != null)
                {
                    DelegateExpression = listener["@delegateExpression"].Value<string>();
                    // triggered at start of task or end.
                    EventType = (EventTypes)Enum.Parse(typeof(EventTypes), listener["@event"].Value<string>());
                    var io = ext["camunda:inputOutput"];
                    if (io != null)
                    {
                        OutputParameters = new Dictionary<string, string>();
                        string name, value;
                        switch (io["camunda:outputParameter"].Type)
                        {
                            case JTokenType.Object:
                                name = io["camunda:outputParameter"]["@name"].Value<string>().ToLower();
                                value = io["camunda:outputParameter"]["#text"].Value<string>();
                                OutputParameters.Add(name, value);
                                break;
                            case JTokenType.Array:
                                foreach (var item in io["camunda:outputParameter"].Children())
                                {
                                    name = item["@name"].Value<string>().ToLower();
                                    value = item["#text"].Value<string>();
                                    OutputParameters.Add(name, value);
                                }
                                break;
                            default:
                                throw new Exception("Element 'camunda:outputParameter' expected a single item or an array.");
                        }
                    }
                }
            }
        }
    }
}
