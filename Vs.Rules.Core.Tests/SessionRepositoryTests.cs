using System;
using Vs.Rules.Core;
using Vs.Rules.Core.Model;
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
                new ClientParameter("alleenstaande", "ja", TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy"),
                new ClientParameter("woonland", "Nederland", TypeInference.InferenceResult.TypeEnum.List, "Dummy"),
                new ClientParameter("toetsingsinkomen_aanvrager", (double)19000, TypeInference.InferenceResult.TypeEnum.Double, "Dummy"),
                new ClientParameter("toetsingsinkomen_toeslagpartner", (double)0, TypeInference.InferenceResult.TypeEnum.Double, "Dummy")
            };

            sessionRepository.Persist(sessionId, parameters);
            ParametersCollection result = sessionRepository.Retrieve(sessionId);
            Assert.False(result == null);
            Assert.True(result.Count == 4);
        }
    }
}
