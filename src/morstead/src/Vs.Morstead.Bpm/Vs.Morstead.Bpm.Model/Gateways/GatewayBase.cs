using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Vs.Morstead.Bpm.Core;
using Vs.Morstead.Bpm.Model.Tasks;

namespace Vs.Morstead.Bpm.Model.Gateways
{
    public abstract class GatewayBase : ITarget
    {
        public string Id { get; set; }

        public IBpmnProcessEngine Engine { get; set; }

        public string Name => throw new NotImplementedException();

        public List<string> Incoming { get; set; }
        public List<string> Outgoing { get; set; }

        protected GatewayBase(JToken gateway)
        {
            Id = gateway["@id"].Value<string>();
            Incoming = new List<string>();
            Outgoing = new List<string>();
            switch (gateway["bpmn:incoming"].Type)
            {
                case JTokenType.String:
                    Incoming.Add((string)gateway["bpmn:incoming"]);
                    break;
                case JTokenType.Array:
                    throw new NotImplementedException();
                default:
                    throw new Exception("Element 'bpm:incoming' expected a string or an array.");
            }

            switch (gateway["bpmn:outgoing"].Type)
            {
                case JTokenType.String:
                    Outgoing.Add((string)gateway["bpmn:outgoing"]);
                    break;
                case JTokenType.Array:
                    foreach (var item in gateway["bpmn:outgoing"].Children())
                    {
                        Outgoing.Add((string)item);
                    }
                    break;
                default:
                    throw new Exception("Element 'bpm:incoming' expected a string or an array.");
            }
        }
    }
}
