using Newtonsoft.Json.Linq;
using System.Linq;

namespace Vs.Morstead.Bpm.Model.Tasks
{
    public class Task : ITask
    {
        private readonly JToken o;

        public string Id => o["@id"].Value<string>();
        public string Name => o["@name"].Value<string>();
        public string Incoming => o["bpmn:incoming"].Value<string>();
        public string Outgoing => o["bpmn:outgoing"].Value<string>();

        public Task(JToken process, string id)
        {
            this.o = process["bpmn:task"].Where(p => p.Value<string>("@id") == id).Single();
        }
    }
}
