using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class SemanticKeyTests
    {
        [Fact]
        public void CanDiscoverAllSemanticKeysAndBindToParameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag5.Body);

            Assert.False(result.IsError);
            Assert.True(controller.ContentNodes.Count == 35);
            List<string> keys = new List<string>();
            foreach (var item in controller.ContentNodes)
            {
                if (item.Name != "end")
                {
                    Assert.NotNull(item.Parameter);
                    Assert.NotNull(item.Parameter.SemanticKey);
                    Assert.True(item.Parameter.SemanticKey == item.Name);
                }
                keys.Add(item.Name);

            }
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void CanDeserializeContentYaml()
        {
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }

        [Fact]
        public void GetContentNodesFromExecutionResult()
        {
            QuestionArgs argsret = null;
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) => {
                argsret = args;
            };
            var result = controller.Parse(YamlZorgtoeslag5.Body);
            var parameters = new ParametersCollection() { new ClientParameter("woonland", "Nederland") } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            try 
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException ex)
            {
            }
            Assert.Equal(35, executionResult.ContentNodes.Count());
            Assert.Equal("stap.woonsituatie.keuze.alleenstaande",
                executionResult.ContentNodes.FirstOrDefault(c => c.Parameter.Name == argsret.Parameters.ToList()[0].Name).Parameter.SemanticKey);
            Assert.Equal("stap.woonsituatie.keuze.aanvrager_met_toeslagpartner",
                executionResult.ContentNodes.FirstOrDefault(c => c.Parameter.Name == argsret.Parameters.ToList()[1].Name).Parameter.SemanticKey);
        }
    }
}
