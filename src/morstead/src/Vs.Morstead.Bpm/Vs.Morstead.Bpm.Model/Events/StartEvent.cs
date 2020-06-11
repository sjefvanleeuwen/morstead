using Newtonsoft.Json.Linq;
using System;

namespace Vs.Morstead.Bpm.Model.Events
{
    public class StartEvent
    {
        private JToken o;
        public string Id => o["@id"].Value<string>();
        public string Outgoing => o["bpmn:outgoing"].Value<string>();
        public StartEvent(JToken process)
        {
            o = process["bpmn:startEvent"];
        }
    }
}