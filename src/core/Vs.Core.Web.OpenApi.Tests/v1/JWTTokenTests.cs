using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var response = await Client.CreateSignedTokenAsync(new CreateSignedTokenRequest()
            {
                Authority = "ACME Authority",
                Issuer = "ACME Issuer",
                Subject = "Token for ACME API'S",
                PrivateKey = JWTTestData.PrivateKeyHS256,
                Ttl = 86400, // valid for one day
                Roles = new[] { new Role { Name = "TNT" }, new Role { Name = "Roadrunner" }, new Role { Name = "dev" } }
            });
        }

        [Fact]
        public async void CanDiscoverRoleCapabilities()
        {
            var response = await Client.AvailableRolesAsync(new AvailableRolesRequest());
            Assert.True(response.RolesControllers.DistinctRoles.Count > 0);
            Assert.True(response.RolesControllers.Controllers.Count > 0);
            // no further asserts because of changing api.
        }
    }
}
