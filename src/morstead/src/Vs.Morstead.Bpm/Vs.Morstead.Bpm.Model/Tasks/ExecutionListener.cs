using Flee.PublicTypes;
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

        public ExecutionListener(JToken task, ExpressionContext context)
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
                }
            }
        }
    }
}
