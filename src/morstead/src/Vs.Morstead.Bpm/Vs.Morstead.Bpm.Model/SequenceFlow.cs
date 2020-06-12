using Newtonsoft.Json.Linq;
using System.Linq;
using Vs.Morstead.Bpm.Model.Events;
using Vs.Morstead.Bpm.Model.Tasks;

namespace Vs.Morstead.Bpm.Model
{
    public class SequenceFlow
    {
        private readonly JToken _process;
        private readonly StartEvent _startEvent;
        private readonly EndEvent _endEvent;
        private string _currentFlowId;
        private JToken _current;

        public SequenceFlow(JToken process, StartEvent startEvent, EndEvent endEvent)
        {
            _process = process;
            _startEvent = startEvent;
            _endEvent = endEvent;
            _currentFlowId = _startEvent.Outgoing;
        }

        public JToken Current()
        {
            return _current;
        }

        public IBpmnTask Next()
        {
            _current = _process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == _currentFlowId);
            var target = _current["@targetRef"].Value<string>();
            var task = new BpmnTask(_process, target);
            _currentFlowId = task.Outgoing;
            return task;
        }
    }
}
