using Mapster;
using System.Linq;
using Vs.Rules.Core.Model;
using Vs.Rules.Network.Semantic.Entities;
using Xunit;

namespace Vs.Rules.Network.Semantic.Tests
{
    public class EntityTests
    {
        private Model _model;

        public EntityTests()
        {
            _model = TestHelpers.GetDefaultTestModel();
        }

        [Fact]
        public void ShouldMapStepEntity()
        {
            var entity = _model.Steps[2].Adapt<StepEntity>();
            Assert.NotNull(entity);
            Assert.Equal(2, entity.Key);
            Assert.Equal("alleenstaande, aanvrager_met_toeslagpartner", entity.Situation);
            Assert.Equal("stap.vermogensdrempel.situatie.aanvrager_met_toeslagpartner", entity.SemanticKey);
        }

        [Fact]
        public void ShouldMapChoiceEntity()
        {
            var entity = _model.Steps[1].Choices.First().Adapt<ChoiceEntity>();
            Assert.NotNull(entity);
            Assert.Equal("alleenstaande", entity.Situation);
        }
    }
}
