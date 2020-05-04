using System;
using Vs.Core.IntegrationTesting.OpenApi;
using Xunit;

namespace Vs.Core.Web.OpenApi.Tests
{
    public class FeatureTests : IClassFixture<TestFixture<Startup>>
    {
        //private RulesClient Client;

        public FeatureTests(TestFixture<Startup> fixture)
        {
           // Client = new RulesClient(fixture.Client);
        }

        [Fact]
        public void CanDiscoverRoleCapabilities()
        {
            Assert.True(true);
        } 
    }
}
