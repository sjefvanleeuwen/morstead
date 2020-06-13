using Flee.PublicTypes;
using Newtonsoft.Json.Linq;
using System;
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
        private readonly ExpressionContext _context;
        private string _currentFlowId;
        private ITarget _currentTarget { get; set; }

        public SequenceFlow(JToken process, StartEvent startEvent, EndEvent endEvent, ExpressionContext context)
        {
            _process = process;
            _startEvent = startEvent;
            _endEvent = endEvent;
            _context = context;
            _currentFlowId = _startEvent.Outgoing;
        }

        public ITarget GetCurrentTarget()
        {
            return _currentTarget;
        }

        private void Evaluate()
        {
            if (_currentTarget != null)
            {
                if (_currentTarget.GetType() == typeof(BpmExclusiveGateway))
                {
                    // evaluate the outgoing flows until one of them is true.
                    foreach (var flowId in _currentTarget.Outgoing)
                    {
                        var expression = _process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == flowId)
                            ["bpmn:conditionExpression"]["#text"];
                        IGenericExpression<bool> eDynamic = _context.CompileGeneric<bool>(((string)expression).ToLower());
                        var result = eDynamic.Evaluate();
                        if (result)
                        {
                            // proceed to the target ref.
                            _currentFlowId = flowId;
                            return;
                        }
                    }
                    throw new Exception("None of the conditional sequence flows are true.");
                }
            }
        }


        public ITarget Next()
        {
            Evaluate();
            var _current = _process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == _currentFlowId);
            var target = _current["@targetRef"].Value<string>();
            if (target == _endEvent.Id)
                return null;
            var factory = new FlowTargetFactory(_process, target);
            switch (factory.Target)
            {
                case "bpmn:task":
                    _currentTarget = new BpmnTask(factory.Token,_context);
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
