using Newtonsoft.Json.Linq;

namespace Vs.Morstead.Bpm.Model.Events
{
    public class EndEvent
    {
        private JToken o;
        public string Id => o["@id"].Value<string>();
        public string Incoming => o["bpmn:incoming"].Value<string>();
        public EndEvent(JToken process)
        {
            o = process["bpmn:endEvent"];
        }
    }
}
