using System.Threading.Tasks;
using Orleans.TestingHost;
using Vs.Rules.Core;
using Vs.Rules.Grains.Interfaces;
using Xunit;

namespace Vs.Rules.OrleansTests
{

    public class HelloGrainTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public HelloGrainTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async Task ShouldReceiveAQuestion()
        {
            var ruleWorker = this.cluster.GrainFactory.GetGrain<IRuleWorker>(0);
            var executionResult = await ruleWorker.Execute(Zorgtoeslag.Body, new ParametersCollection());
            Assert.NotNull(executionResult.Questions);
        }
    }
}