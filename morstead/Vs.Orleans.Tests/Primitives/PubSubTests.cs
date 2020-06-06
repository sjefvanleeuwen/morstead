using Orleans;
using Orleans.TestingHost;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using VirtualSociety.VirtualSocietyDid;
using Vs.Rules.Grains.Interfaces.Content;
using Vs.Rules.Grains.Interfaces.Primitives.PubSub;
using Vs.Rules.Grains.Interfaces.User;
using Vs.Rules.Grains.User;
using Xunit;

namespace Vs.Orleans.Tests.Primitives
{
    public class PubSubTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public PubSubTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async Task ShouldInitializePubSub()
        {
            var contentDid = new Did("mstd:content").ToString();
            //var pubSubDid = new Did("mstd:pubsub").ToString();
            var accountDid = new Did("mstd:person").ToString();
            var userContentDid = new Did("mstd:rel").ToString();
            //var pubSub = cluster.GrainFactory.GetGrain<IPubSubGrain>(pubSubDid);
            var content = cluster.GrainFactory.GetGrain<IContentPersistentGrain>(contentDid);
            var userContent = cluster.GrainFactory.GetGrain<IUserContent>(userContentDid);
            var account = cluster.GrainFactory.GetGrain<IUserAccountPersistentGrain>(accountDid);
            // Register A User Account
            await account.RegisterUser(new UserAccountState()
            {
                Email = "unittest@unittest.com",
                Name = "unittest",
                Locale = new System.Globalization.CultureInfo("nl-NL"),
                NickName = "unittestnick"
            });
            // Set an owner for the user content that is going to be saved.
            await userContent.SetOwner(account);
            // Attach the content for the publishing grain
            //await pubSub.SetPublishingGrain(typeof(IContentPersistentGrain), contentDid);
            // Subscribe the User Content
            await content.GetPubSub().Result.Subscribe(
                new PubSubSubscriber()
                {
                    Interface = typeof(IUserContent),
                    GrainId = userContent.GetPrimaryKeyString(),
                    GrainCallbackMethod = nameof(userContent.ContentSaved)
                });
            // Create Some Content
            await content.Save(new ContentType("text/html"), Encoding.UTF8,"Hello World!");
        }
    }
}
