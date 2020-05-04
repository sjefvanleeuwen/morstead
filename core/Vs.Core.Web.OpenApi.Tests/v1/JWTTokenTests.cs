using Vs.Core.IntegrationTesting.OpenApi;
using Vs.Core.Web.OpenApi.Tests.v1.TestData;
using Xunit;

namespace Vs.Core.Web.OpenApi.Tests.v1
{
    public class JWTTokenTests : IClassFixture<TestFixture<Startup>>
    {
        private JwtTokenClient Client;

        public JWTTokenTests(TestFixture<Startup> fixture)
        {
            Client = new JwtTokenClient(fixture.Client);
        }

        [Fact]
        public async void CanCreateSignedTokenViaApi()
        {
            // sign using an RSA256 Key
            var response = await Client.CreateSignedTokenAsync(new CreateSignedTokenRequest()
            {
                Authority = "ACME Authority",
                Issuer = "ACME Issuer",
                Subject = "Token for ACME API'S",
                PrivateKey = JWTTestData.PrivateKeyHS256,
                Ttl = 86400, // valid for one day
                Roles = new[] { new Role() { Name = "TNT" }, new Role { Name = "Roadrunner" } }
            });
        }


        //[Fact]
        public void CanDiscoverRoleCapabilities()
        {
            Assert.True(true);
        }
    }
}
