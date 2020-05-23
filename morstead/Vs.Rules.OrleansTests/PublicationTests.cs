using Orleans.TestingHost;
using Vs.Publications.Grains.Interfaces;
using Xunit;

namespace Vs.Rules.OrleansTests
{
    public class PublicationTests
    {
        private readonly TestCluster cluster;

        public PublicationTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async void ShouldPersistPublication()
        {
            var grain = this.cluster.GrainFactory.GetGrain<IPublicationGrain>("did:vsoc:mstd:pub:Sp8WbN5r0E6MCEx54Is3oQ");
        }
    }
}
