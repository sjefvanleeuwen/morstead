using System.Threading.Tasks;
using Orleans.TestingHost;
using Vs.Orleans.Tests;
using Vs.OrleansTests.rules;
using Vs.Rules.Core;
using Vs.Rules.Grains.Interfaces;
using Xunit;

namespace Vs.Orleans.Tests.rules
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
            var ruleWorker = cluster.GrainFactory.GetGrain<IRuleWorker>(0);
            var executionResult = await ruleWorker.Execute(Zorgtoeslag.Body, new ParametersCollection());
            Assert.NotNull(executionResult.Questions);
        }

        [Fact]
        public async Task ShouldPersistState()
        {
            var ruleWorker = cluster.GrainFactory.GetGrain<IPersistentRuleWorker>("did:vsoc:mstd:rule:TKzHGE7UpE2aXnEEZP0BXQ");
            var executionResult = await ruleWorker.Execute(Zorgtoeslag.Body, new ParametersCollection() { new Rules.Core.Model.Parameter() { Name = "woonland", Value = "Nederland" } });
            Assert.NotNull(executionResult.Questions);

            var ruleWorker2 = cluster.GrainFactory.GetGrain<IPersistentRuleWorker>("did:vsoc:mstd:rule:TKzHGE7UpE2aXnEEZP0BXQ");
            var state = await ruleWorker2.GetState();
            Assert.NotEmpty(state);
        }
    }
}