using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Vs.Morstead.Bpm.Model.Gateways
{
    public class BpmExclusiveGateway
    {
        public string Id { get; set; }
        public List<string> Inputs { get; set; }
        public List<string> Outputs { get; set; }

        public BpmExclusiveGateway(JToken process, string id)
        {
            var o = process["bpmn:exclusiveGateway"];
            switch (o.Type)
            {
                case JTokenType.Object:
                    if (id != o["@id"].Value<string>())
                        throw new Exception($"The BPM process could not find a bpmn:exlusiveGateway with id {id}.");
                    break;
                case JTokenType.Array:
                    throw new NotImplementedException();
            }
                //.Where(p => p.Value<string>("@id") == id).Single();
            Id = id;
            Inputs = new List<string>();
            Outputs = new List<string>();
            switch (o["bpmn:incoming"].Type)
            {
                case JTokenType.String:
                    Inputs.Add((string)o["bpmn:incoming"]);
                    break;
                case JTokenType.Array:
                    throw new NotImplementedException();
                default:
                    throw new Exception("Element 'bpm:incoming' expected a string or an array.");
            }

            switch (o["bpmn:outgoing"].Type)
            {
                case JTokenType.String:
                    Inputs.Add((string)o["bpmn:outgoing"]);
                    break;
                case JTokenType.Array:
                    foreach (var item in o["bpmn:outgoing"].Children())
                    {
                        Outputs.Add((string)item);
                    }
                    break;
                default:
                    throw new Exception("Element 'bpm:incoming' expected a string or an array.");
            }
        }
    }
}
