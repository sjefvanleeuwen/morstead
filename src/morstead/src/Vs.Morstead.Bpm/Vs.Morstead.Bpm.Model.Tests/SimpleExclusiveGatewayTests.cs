using Vs.Morstead.Bpm.Model.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]
namespace Vs.Morstead.Bpm.Model.Tests
{
    public class SimpleExclusiveGatewayTests : IClassFixture<BpmnTestFixture>
    {
        BpmnTestFixture _fixture;

        public ITestOutputHelper Output { get; }

        public SimpleExclusiveGatewayTests(ITestOutputHelper output, BpmnTestFixture fixture)
        {
            _fixture = fixture;
            Output = output;
        }

        [Fact, Order(0)]
        public void LoadBpmnProcess()
        {
            _fixture.LoadBpmn(@"Bpmn20/simple-exclusive-gateway.bpmn");
            Output.WriteLine("Process Loaded.");
        }

        [Fact, Order(1)] public void ExecutesStartEventAndExpectsTaskA()
        {
            var task = _fixture.Process.SequenceFlow.Next();
            Assert.Equal(task, _fixture.Process.SequenceFlow.GetCurrentTarget());
            Assert.True(_fixture.Process.SequenceFlow.GetCurrentTarget().Name == "A");
        }

        [Fact, Order(2)] public void ProcessesTaskAAndExpectsVariableAToBeSet()
        {
            var task = _fixture.Process.SequenceFlow.GetCurrentTarget() as BpmnTask;
            Assert.Equal("Flee", task.ExecutionListeners[0].DelegateInterface);
            Assert.Equal("Calc", task.ExecutionListeners[0].DelegateMethod);
            Assert.Equal("1", task.ExecutionListeners[0].OutputParameters["a"]);
        }

        [Fact, Order(3)] public void FlowExpectsAnExclusiveGateway()
        {
            var task = _fixture.Process.SequenceFlow.Next();
        }

        [Fact, Order(4)] public void ChoosesTaskB()
        {

        }

        [Fact, Order(5)] public void ArrivesAtEndEvent()
        {

        }
    }
}
