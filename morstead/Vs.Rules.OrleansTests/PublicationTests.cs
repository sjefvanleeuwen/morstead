using Orleans.TestingHost;
using System;
using System.Net.Mime;
using System.Text;
using Vs.Publications.Grains.Interfaces;
using Xunit;

namespace Vs.Rules.OrleansTests
{
    public class PublicationTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public PublicationTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async void ShouldPersistPublication()
        {
            string target = "# Empty Yaml";
            var grain = this.cluster.GrainFactory.GetGrain<IPublicationGrain>("did:vsoc:mstd:pub:Sp8WbN5r0E6MCEx54Is3oQ");
            var d = await grain.Get();
            await grain.Create(new ContentType("application/yaml"), Encoding.UTF8, Encoding.UTF8.GetBytes("# Empty Yaml"));
            var grain2 = this.cluster.GrainFactory.GetGrain<IPublicationGrain>("did:vsoc:mstd:pub:Sp8WbN5r0E6MCEx54Is3oQ");
            var document = await grain2.Get();
            var contents = document.Encoding.GetString(document.Content);
            Assert.Equal(contents, target);
        }
    }
}
