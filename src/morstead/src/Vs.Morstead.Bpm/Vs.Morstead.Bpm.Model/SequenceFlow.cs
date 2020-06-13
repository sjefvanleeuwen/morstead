using Newtonsoft.Json.Linq;
using System.Linq;
using Vs.Morstead.Bpm.Model.Events;
using Vs.Morstead.Bpm.Model.Gateways;
using Vs.Morstead.Bpm.Model.Tasks;

namespace Vs.Morstead.Bpm.Model
{
    public class SequenceFlow
    {
        private readonly JToken _process;
        private readonly StartEvent _startEvent;
        private readonly EndEvent _endEvent;
        private string _currentFlowId;
        private ITarget _currentTarget { get; set; }

        public SequenceFlow(JToken process, StartEvent startEvent, EndEvent endEvent)
        {
            _process = process;
            _startEvent = startEvent;
            _endEvent = endEvent;
            _currentFlowId = _startEvent.Outgoing;
        }

        public ITarget GetCurrentTarget()
        {
            return _currentTarget;
        }

        public ITarget Next()
        {
            var _current = _process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == _currentFlowId);
            var target = _current["@targetRef"].Value<string>();
            var factory = new FlowTargetFactory(_process, target);
            switch (factory.Target)
            {
                case "bpmn:task":
                    _currentTarget = new BpmnTask(factory.Token);
                    break;
                case "bpmn:exclusiveGateway":
                    _currentTarget = new BpmExclusiveGateway(factory.Token);
                    break;
            }
            _currentFlowId = _currentTarget.Outgoing[0]; // TODO: support multiple outgoing.
            return _currentTarget;
        }
    }
}
