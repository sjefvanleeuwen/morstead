using Vs.Morstead.Bpm.Model.Gateways;
using Vs.Morstead.Bpm.Model.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace Vs.Morstead.Bpm.Model.Tests
{
    public class SimpleInclusiveGatewayTests : IClassFixture<BpmnTestFixture>
    {
        BpmnTestFixture _fixture;

        public ITestOutputHelper Output { get; }

        public SimpleInclusiveGatewayTests(ITestOutputHelper output, BpmnTestFixture fixture)
        {
            _fixture = fixture;
            Output = output;
        }

        [Fact, Order(0)]
        public void LoadBpmnProcess()
        {
            _fixture.LoadBpmn(@"Bpmn20/simple-inclusive-gateway.bpmn");
            Output.WriteLine("Process Loaded.");
        }

        [Fact, Order(1)]
        public void ExecutesStartEventAndExpectsTaskSetVariables()
        {
            var task = _fixture.Process.SequenceFlow.Next();
            Assert.True(task.Id == "TaskSetVariables");
        }

        [Fact, Order(2)]
        public void ProcessesTaskAAndExpectsVariableAAndBToBeSet()
        {
            var task = _fixture.Process.SequenceFlow.GetCurrentTarget() as BpmnTask;
            Assert.Equal(true, bool.Parse((string)task.OutputParameters["a"].ToString()));
            Assert.Equal(true, bool.Parse((string)task.OutputParameters["b"].ToString()));
        }


        [Fact, Order(3)]
        public void FlowExpectsAnInclusiveGateway()
        {
            var task = _fixture.Process.SequenceFlow.Next() as BpmInclusiveGateway;
            Assert.NotEmpty(task.Outgoing);
            Assert.NotEmpty(task.Incoming);
            Assert.Single(task.Incoming);
            Assert.Equal(2, task.Outgoing.Count);
        }

        [Fact, Order(4)]
        public void ForkToTaskAAndB()
        {

        }
    }
}
