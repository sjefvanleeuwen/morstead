using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    /// <summary>
    /// Unit tests to test the YamlScript Execution Engine.
    /// Note: Investigate other failed unit test classes before this one to get insights.
    /// </summary>
    public class ExecutionTests
    {
        /// <summary>
        /// Rekenvoorbeeld 1
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000. 
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("woonland","Nederland"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };
            var executionResult = controller.ExecuteWorkflow(ref parameters);
            Assert.True((double)parameters.GetParameter("zorgtoeslag").Value == 99.09);
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_GetInputParameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            var parameters = controller.GetInputParameters();
        }

    }
}
