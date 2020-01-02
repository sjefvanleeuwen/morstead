using System;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class SessionRepositoryTests
    {

        [Fact]
        public void Persistence_Load_Configuration()
        {
            var sessionRepository = new SessionRepository(new TimeSpan(0, 15, 0));
            var sessionId = Guid.NewGuid();
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("woonland","Nederland"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };

            sessionRepository.Persist(sessionId, parameters);
            ParametersCollection result = sessionRepository.Retrieve(sessionId);
            Assert.False(result == null);
            Assert.True(result.Count == 4);
        }
    }
}
