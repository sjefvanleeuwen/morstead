using Mapster;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Vs.Rules.Core;
using Vs.Rules.Core.Model;
using Vs.Rules.Network.Semantic.Entities;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;

namespace Vs.Rules.Network.Semantic.Tests
{
    public class IndexingTests
    {
        [Fact]
        public void ShouldIndexModel()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            Assert.False(result.IsError);

            RuleDatabase db = new RuleDatabase();

            foreach (var step in result.Model.Steps)
            {
                db.Steps.Insert(step.Adapt<StepEntity>());
                if (step.Choices == null)
                    continue;
                foreach (var choice in step.Choices)
                {
                    db.Choices.Insert(choice.Adapt<ChoiceEntity>());
                }
            }

            Assert.NotEmpty(db.Steps);
            Assert.NotEmpty(db.Choices);
        }
 
    }
}
