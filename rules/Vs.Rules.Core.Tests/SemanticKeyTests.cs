using System.Collections.Generic;
using System.Linq;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.TestData;
using Xunit;
using YamlDotNet.Serialization;

namespace Vs.Rules.Core.Tests
{
    public class SemanticKeyTests
    {
        [Fact]
        public void CanDiscoverAllSemanticKeysAndBindToParameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));

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
            var s = string.Join('\n', keys);
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
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                argsret = args;
            };
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            var parameters = new ParametersCollection() {
                new ClientParameter("woonland", "Nederland", TypeInference.InferenceResult.TypeEnum.List, "Dummy")
            } as IParametersCollection;
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

        [Fact]
        public void ExecutionResultHasCorrectSemanticKeyOnGeenRecht()
        {
            QuestionArgs argsret = null;
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                argsret = args;
            };
            var result = controller.Parse(YamlTestFileLoader.Load(@"Zorgtoeslag5.yaml"));
            var parameters = new ParametersCollection() { new ClientParameter("woonland", "Anders", TypeInference.InferenceResult.TypeEnum.List, "Dummy") } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException ex)
            {
            }
            Assert.Equal("stap.woonland", executionResult.Step.SemanticKey);
            Assert.Equal("stap.woonland.geen_recht", executionResult.Step.Break.SemanticKey);
            Assert.True(executionResult?.Parameters?.Any(p => p.Name == "recht" && !(bool)p.Value));
        }
    }
}
