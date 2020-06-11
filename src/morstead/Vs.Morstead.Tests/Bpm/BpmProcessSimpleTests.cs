using Orleans.TestingHost;
using System;
using VirtualSociety.VirtualSocietyDid;
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
    }
}
