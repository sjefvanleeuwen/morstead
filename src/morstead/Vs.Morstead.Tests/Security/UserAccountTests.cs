using Orleans.Runtime;
using Orleans.TestingHost;
using System.Threading.Tasks;
using Vs.Morstead.Grains.Interfaces.Security.User;
using Vs.Morstead.Grains.Interfaces.User;
using Xunit;

namespace Vs.Morstead.Tests.Security
{
    public class UserAccountTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public UserAccountTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async void CanCreateAccount()
        {
            // Create a user under that user hash account that resides on for example Auth0.
            var account = cluster.GrainFactory.GetGrain<IUserAccountPersistentGrain>("live|1234567890");
            // Register A User Account
            var user = new UserAccountState()
            {
                Email = "unittest@unittest.com",
                Name = "unittest",
                Locale = new System.Globalization.CultureInfo("nl-NL"),
                NickName = "unittestnick",
                Picture = new System.Uri("https://somegravatar.com/4438439")
            };
            await account.RegisterUser(user);
            // For an advanced example usage see: PubSubTest, where a user is connected as an owner for a piece of content.
            var accountRetrieval = cluster.GrainFactory.GetGrain<IUserAccountPersistentGrain>("live|1234567890");
            Assert.Equal(user.Email, accountRetrieval.GetUserAccountInfo().Result.Email);
        }
    }
}
