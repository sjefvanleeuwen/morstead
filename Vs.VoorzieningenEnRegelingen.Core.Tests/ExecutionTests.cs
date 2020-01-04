using System;
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
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
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
        public void Execution_ZorgToeslag_2019_Scenario1_WithQA_toetsingsinkomen_aanvrager()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "toetsingsinkomen_aanvrager");
                argsret = args;
            };
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("woonland","Nederland"),
                /*new Parameter("toetsingsinkomen_aanvrager",(double)19000),*/
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };
            try
            {
                var executionResult = controller.ExecuteWorkflow(ref parameters);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "toetsingsinkomen_aanvrager");
        }
        
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1_WithQA_Woonland()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "woonland");
                argsret = args;
            };
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                /*new Parameter("woonland","Nederland"),*/
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)0)
            };
            try
            {
                var executionResult = controller.ExecuteWorkflow(ref parameters);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "woonland");
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_Without_Initial_Parameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "alleenstaande");
                Assert.True(args.Parameters[0].Type == "UnresolvedType");
                Assert.True(args.Parameters[0].Value.ToString() == "Situation");
                Assert.True(args.Parameters[1].Name == "aanvrager_met_toeslagpartner");
                Assert.True(args.Parameters[1].Type == "UnresolvedType");
                Assert.True(args.Parameters[1].Value.ToString() == "Situation");
                argsret = args;
            };
            var parameters = new ParametersCollection();
            try
            {
                var executionResult = controller.ExecuteWorkflow(ref parameters);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "alleenstaande");
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_GetInputParameters()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
           // var parameters = controller.GetInputParameters();
        }

        [Fact]
        public void Execution_Should_Be_Not_Implemented_Exception()
        {
            Exception ex1 = Assert.Throws<NotImplementedException>(() => new UnresolvedException());
            Exception ex3 = Assert.Throws<NotImplementedException>(() => new UnresolvedException("", new Exception()));
        }
    }
}
