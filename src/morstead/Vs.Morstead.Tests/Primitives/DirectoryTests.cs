using EasyCompressor;
using Microsoft.AspNetCore.Connections;
using Orleans.TestingHost;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using VirtualSociety.VirtualSocietyDid;
using Vs.Morstead.Grains.Interfaces.Content;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var did = new Did("mstd:dir").ToString();
            var dir = cluster.GrainFactory.GetGrain<IDirectoryGrain>(did);
            Assert.False(await dir.DirectoryExists("//test1/test2/test3"));
            var dir1 = await dir.CreateDirectory("//test1/test2/test3");
            Assert.StartsWith("did:vsoc:mstd:dir", dir1.ItemsGrainId);
            var dir2 = await dir.GetDirectory("//test1/test2//test3");
            Assert.Equal(dir1.ItemsGrainId, dir2.ItemsGrainId);
            // Add an item to dir2
            var contents = cluster.GrainFactory.GetGrain<IDirectoryContentsGrain>(dir2.ItemsGrainId);
            var contentDid = new Did("mstd:pub").ToString();
            var content = cluster.GrainFactory.GetGrain<IContentPersistentGrain>(contentDid);
            await content.Save(new System.Net.Mime.ContentType("text/html"), Encoding.UTF8, "Unit Test, Hello World");
            await contents.AddItem(new DirectoryContentsItem()
            {
                GrainId = contentDid,
                Interface = typeof(IContentPersistentGrain)
            });

            var contents2 = cluster.GrainFactory.GetGrain<IDirectoryContentsGrain>(dir2.ItemsGrainId);
            var list = await contents2.ListItems();
            Assert.Single(list.Items);
            Assert.Equal(list.Items.ElementAt(0).Value.GrainId, contentDid);
            // tested in another unit test, but to be thorough
            var content2 = cluster.GrainFactory.GetGrain<IContentPersistentGrain>(contentDid);
            var content2state = await content2.Load();
            var contentsDocument = content2state.Encoding.GetString(content2state.Content);
            Assert.Equal("Unit Test, Hello World", contentsDocument);
            stopwatch.Stop();
        }
    }
}
