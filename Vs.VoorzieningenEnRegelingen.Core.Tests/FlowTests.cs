using System;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    /// <summary>
    /// Tests simple conditional flow engine
    /// </summary>
    public class FlowTests
    {
        [Fact]
        public void Flow_Zorgtoeslag_Model_Flow_Deserialization()
        {
            var parser = new YamlParser(YamlZorgtoeslag.Body, null);
            var steps = parser.Flow();
            Assert.True(steps.Count == 3);
            Assert.True((from p in steps where p.Formula == "recht" select p).Single().Situation == "");
            Assert.True((from p in steps where p.Situation == "buitenland" select p).Single().Description == "bereken de zorgtoeslag wanner men in het buitenland woont");
            Assert.True((from p in steps where p.Name == "2" select p).First().Description == "bereken de zorgtoeslag wanneer men binnen nederland woont");
        }

        /// <summary>
        /// Rekenvoorbeeld 1
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000. 
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Flow_ZorgToeslag_2019_Scenario1_Condition_Branche_Test()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            var parameters = new ParametersCollection();
            parameters.Add(new ClientParameter("woonland", "Nederland"));
           //  var executionResult = controller.ExecuteWorkflow(parameters);
            // Tsjechië,            0.2412 
            // context.Variables.Add("woonland", "Tsjechië");
            // var formula = controller.GetFormula("woonlandfactor");
        }

        [Fact]
        public void Flow_Should_Be_StepException()
        {
            Exception ex1 = Assert.Throws<NotImplementedException>(() => new StepException());
            Exception ex2 = Assert.Throws<NotImplementedException>(() => new StepException("test"));
            Exception ex3 = Assert.Throws<NotImplementedException>(() => new StepException("",new Exception()));
        }

        [Fact]
        public void Flow_Shouldnt_Be_StepException()
        {
            StepException ex = new StepException("exception", new Step(1,"name", "description", "formula", "situation","break"));
            Assert.True(ex.Step.Key == 1);
            Assert.True(ex.Step.Name == "name");
            Assert.True(ex.Step.Description == "description");
            Assert.True(ex.Step.Formula == "formula");
            Assert.True(ex.Step.Situation == "situation");
            Assert.True(ex.Step.Break == "break");
        }
    }
}
