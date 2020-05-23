using Mapster;
using System.Linq;
using Vs.Rules.Core.Model;
using Vs.Rules.Network.Semantic.Entities;
using Xunit;

namespace Vs.Rules.Network.Semantic.Tests
{
    public class IndexingTests
    {
        private Model _model;
        private RuleDatabase _db;

        public IndexingTests()
        {
            _model = TestHelpers.GetDefaultTestModel();
            _db = new RuleDatabase();

            foreach (var step in _model.Steps)
            {
                _db.Steps.Insert(step.Adapt<StepEntity>());
                if (step.Choices == null)
                    continue;
                foreach (var choice in step.Choices)
                {
                    var entity = choice.Adapt<ChoiceEntity>();
                    entity.PkStep = _db.Steps.Last().Pk;
                    _db.Choices.Insert(entity);
                }
            }
        }

        [Fact]
        public void ShouldContainEntities()
        {
            Assert.NotEmpty(_db.Steps);
            Assert.NotEmpty(_db.Choices);
        }

        [Fact]
        public void FindRelationShipBetweenStepAndChoices()
        {
            var query = from p in _db.Choices join g in _db.Steps on p.PkStep equals g.Pk select new { Situation = p.Situation, Step = g.Name };
            // there should be six relationships of choices found that connect to the steps.
            Assert.Equal(6, query.Count());
        }
    }
}
