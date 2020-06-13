using Orleans.TestingHost;
using System;
using System.Linq;
using VirtualSociety.VirtualSocietyDid;
using Vs.Morstead.Bpm.Model;
using Vs.Morstead.Bpm.Model.Tasks;
using Vs.Morstead.Grains.Interfaces.Bpm;
using Xunit;

namespace Vs.Morstead.Tests.Bpm
{
    public class BpmProcessSimpleTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public BpmProcessSimpleTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async void ExpectCorrectStateAndExceptions()
        {
            var did = new Did("mstd:bpm").ToString();
            var sut = cluster.GrainFactory.GetGrain<IBpmProcessGrain>(did);
            Assert.Equal(BpmProcessExecutionTypes.Uninitialized, sut.GetProcessStatus().Result);
            var ex = Assert.Throws<AggregateException>(() => sut.StartProcess().Wait());
            Assert.EndsWith($"started.", ex.InnerException.Message);
            ex = Assert.Throws<AggregateException>(() => sut.PauseProcess().Wait());
            Assert.EndsWith($"paused.", ex.InnerException.Message);
            ex = Assert.Throws<AggregateException>(() => sut.StopProcess().Wait());
            Assert.EndsWith($"stopped.", ex.InnerException.Message);
            await sut.LoadProcess(TestFileLoader.Load(@"Bpmn20/simple-task.bpmn"));
            Assert.Equal(BpmProcessExecutionTypes.Initialized, sut.GetProcessStatus().Result);
            await sut.StartProcess();
            Assert.Equal(BpmProcessExecutionTypes.Started, sut.GetProcessStatus().Result);
            await sut.PauseProcess();
            Assert.Equal(BpmProcessExecutionTypes.Paused, sut.GetProcessStatus().Result);
            await sut.StopProcess();
            Assert.Equal(BpmProcessExecutionTypes.Stopped, sut.GetProcessStatus().Result);
        }

        [Fact]
        public void CanInstantiateGrainAndMethodFromDelegateExpression()
        {
            var process = new BpmnProcess(TestFileLoader.Load(@"Bpmn20/simple-task.bpmn"));
            var task = process.SequenceFlow.Next() as BpmnTask;
            var listener = task.ExecutionListeners[0];
            var l = typeof(IBpmProcessGrain).Assembly.DefinedTypes.First(p => p.Name == listener.DelegateInterface).AsType();
            var m = l.GetMethod(listener.DelegateMethod);
            var p = m.GetParameters();
            var o = new System.Collections.Generic.List<object>();
            foreach (var item in m.GetParameters())
            {
                if (!listener.OutputParameters.ContainsKey(item.Name.ToLower()))
                {
                    throw new Exception($"Execution Listener for delegate {listener.DelegateInterface} method {listener.DelegateMethod} does not contain the expected parameter {item.Name}.");
                }
                if (item.ParameterType.IsArray)
                {
                    
                    o.Add(new string[] { listener.OutputParameters[item.Name.ToLower()] });
                }
                else
                {
                    o.Add(listener.OutputParameters[item.Name.ToLower()]);
                }
            }

            var grain = cluster.GrainFactory.GetGrain(l, 0);
            m.Invoke(grain, o.ToArray());
        }

        [Fact]
        public async void CanExecuteBpmAndDelegate()
        {
            var did = new Did("mstd:bpm").ToString();
            var sut = cluster.GrainFactory.GetGrain<IBpmProcessGrain>(did);
            await sut.LoadProcess(TestFileLoader.Load(@"Bpmn20/simple-task.bpmn"));
            sut.StartProcess();
        }
    }
}
