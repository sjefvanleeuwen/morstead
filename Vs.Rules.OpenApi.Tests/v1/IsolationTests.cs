using Vs.Core.IntegrationTesting.OpenApi;
using Xunit;

namespace Vs.Rules.OpenApi.Tests.v1
{
    public class IsolationTests : IClassFixture<TestFixture<Startup>>
    {
        private RulesClient Client;

        public IsolationTests(TestFixture<Startup> fixture)
        {
            Client = new RulesClient(fixture.Client);
        }

        /*
        [Fact]
        async void PingShouldProvidePong()
        {
            var s = await Client.PingAsync();
            Assert.True(s == "Pong from v1");
        }
        */
    }
}
