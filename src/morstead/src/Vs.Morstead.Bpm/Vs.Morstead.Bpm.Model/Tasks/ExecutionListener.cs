using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
                        foreach (var item in io["camunda:outputParameter"].Children())
                        {
                            var name = item["@name"].Value<string>().ToLower();
                            var value = item["#text"].Value<string>();
                            OutputParameters.Add(name, value);
                        }
                    }
                }
            }
        }
    }
}
