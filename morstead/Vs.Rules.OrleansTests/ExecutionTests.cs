using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Orleans.TestingHost;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Grains.Interfaces;
using Xunit;

namespace Vs.Rules.OrleansTests
{
    [Collection(ClusterCollection.Name)]
    public class HelloGrainTests
    {
        private readonly TestCluster cluster;

        public HelloGrainTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async Task SaysHelloCorrectly()
        {
            var ruleWorker = this.cluster.GrainFactory.GetGrain<IRuleWorker>(0);
            var executionResult = await ruleWorker.Execute(Zorgtoeslag.Body, new ParametersCollection());

            cluster.StopAllSilos();

            Assert.NotNull(executionResult.Questions);
        }
    }
}