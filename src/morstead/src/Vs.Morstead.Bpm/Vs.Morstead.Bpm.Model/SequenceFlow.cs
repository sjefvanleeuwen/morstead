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
        private IBpmnTask _currentTask;

        public SequenceFlow(JToken process, StartEvent startEvent, EndEvent endEvent)
        {
            _process = process;
            _startEvent = startEvent;
            _endEvent = endEvent;
            _currentFlowId = _startEvent.Outgoing;
        }

        public IBpmnTask GetCurrentTask()
        {
            return _currentTask;
        }

        public IBpmnTask Next()
        {
            var _current = _process["bpmn:sequenceFlow"].Single(p => p.Value<string>("@id") == _currentFlowId);
            var target = _current["@targetRef"].Value<string>();
            var task = new BpmnTask(_process, target);
            _currentFlowId = task.Outgoing;
            _currentTask = task;
            return _currentTask;
        }
    }
}
