using Orleans.TestingHost;
using Vs.Morstead.Grains.Interfaces.Security.User;
using Vs.Morstead.Grains.Interfaces.User;
using Xunit;

namespace Vs.Morstead.Tests.Security
{
    public class UserAccountTests
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
            await account.RegisterUser(new UserAccountState()
            {
                Email = "unittest@unittest.com",
                Name = "unittest",
                Locale = new System.Globalization.CultureInfo("nl-NL"),
                NickName = "unittestnick"
            });
            // For an advanced example usage see: PubSubTest, where a user is connected as an owner for a piece of content.
        }
    }
}
