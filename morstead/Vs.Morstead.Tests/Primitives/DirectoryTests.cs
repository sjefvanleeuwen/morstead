using Orleans.TestingHost;
using VirtualSociety.VirtualSocietyDid;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;
using Xunit;

namespace Vs.Morstead.Tests.Primitives
{
    public class DirectoryTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public DirectoryTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public void CanCreateNestedDirectoriesInState()
        {
            var state = new DirectoryState();
            state.Dir = new NestedDictionary<string, DirectoryItem>();
            var dir1 = state.CreateDir("//test1/test2/test3");
            var dir2 = state.CreateDir("test5//test2///test3");
            Assert.StartsWith("did:vsoc:mstd:dir", dir1.ItemsGrainId);
            Assert.StartsWith("did:vsoc:mstd:dir", dir2.ItemsGrainId);
            Assert.True(state.Dir["test1"].ContainsKey("test2"));
            Assert.True(state.Dir["test1"]["test2"].ContainsKey("test3"));
            Assert.True(state.Dir["test5"].ContainsKey("test2"));
            Assert.True(state.Dir["test5"]["test2"].ContainsKey("test3"));
        }

        [Fact]
        public async void CanReadNestedDirectoriesInGrain()
        {
            var did = new Did("mstd:dir").ToString();
            var dir = cluster.GrainFactory.GetGrain<IDirectoryGrain>(did);
            var dir1 = await dir.CreateDirectory("//test1/test2/test3");
            Assert.StartsWith("did:vsoc:mstd:dir", dir1.ItemsGrainId);
            var dir2 = await dir.GetDirectory("//test1/test2//test3");
            Assert.Equal(dir1.ItemsGrainId, dir2.ItemsGrainId);
        }
    }
}
