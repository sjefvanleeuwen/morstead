using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Vs.Morstead.Bpm.Model
{
    public class FlowTargetFactory
    {
        private static string[] targets => "bpmn:task,bpmn:exclusiveGateway,bpmn:inclusiveGateway".Split(',');

        public string Target { get; set; }

        public JToken Token { get; set; }

        public FlowTargetFactory(JToken process, string id)
        {
            foreach (var target in targets)
            {
                if (process[target] == null) continue;
                foreach (var item in process[target])
                {
                    switch (item.Type)
                    {
                        case JTokenType.Object:
                            if (item["@id"].Value<string>() == id) {
                                Token = item;
                                Target = target;
                                return;
                            }
                            break;
                        case JTokenType.Property:
                            if (item.Parent["@id"].Value<string>() == id)
                            {
                                Token = item.Parent;
                                Target = target;
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            throw new Exception($"Flow target not found, current supported are: {string.Join(",", targets)}");
        }
    }
}
