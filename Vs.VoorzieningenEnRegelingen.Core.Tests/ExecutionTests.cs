using System;
using System.Linq;
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
        [Theory]
        [InlineData("true", true, false, true)]
        [InlineData("false", false, true, null)]
        public void Execution_Should_Stop(string rechtValue, bool rechtAssertionResult, 
            bool isStopExecutionAssertResult, bool? executionAssertResult)
        {
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
            var result = controller.Parse(
  $@"stuurinformatie:
  onderwerp: unit test
  organisatie: unit test
  type: unit test
  domein: unit test
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: unit test
berekening:
 - stap: 1
   omschrijving: unit test flow should stop
   formule: recht
 - stap: 2
   omschrijving: should not be executed
   formule: execution
formules:
 - recht:
     formule: {rechtValue}
 - execution:
     formule: true
");
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() { };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((bool)parameters.GetParameter("recht").Value == rechtAssertionResult);
            Assert.True(executionResult.Stacktrace.Where(s => s.Step.Name == "1").First().IsStopExecution == isStopExecutionAssertResult);
            Assert.True((bool?)parameters.GetParameter("execution")?.Value == executionAssertResult);
        }

        /// <summary>
        /// Rekenvoorbeeld 1
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000 en een vermogen van € 1.000
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
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("woonland","Nederland")
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((bool)parameters.GetParameter("recht")?.Value);
            Assert.True((double)parameters.GetParameter("zorgtoeslag").Value == 99.09);
        }

        /// <summary>
        /// Rekenvoorbeeld 1a
        /// Wouter is een alleenstaande man met een jaarinkomen van € 60.000 en een vermogen van € 1.000
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1a()
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
                new Parameter("toetsingsinkomen_aanvrager",(double)60000),
                new Parameter("vermogen_aanvrager",(double)1000)
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.False((bool)parameters.GetParameter("recht")?.Value);
            Assert.Null((double?)parameters.GetParameter("zorgtoeslag")?.Value);
        }

        /// <summary>
        /// Rekenvoorbeeld 1b
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000 en een vermogen van € 150.000
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1b()
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
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)150000)
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.False((bool)parameters.GetParameter("recht")?.Value);
            Assert.Null((double?)parameters.GetParameter("zorgtoeslag")?.Value);
        }

        /// <summary>
        /// Rekenvoorbeeld 1b
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000 en een vermogen van € 1.000.
        /// Hij woont in Malta.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1c()
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
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("woonland","Malta")
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((bool)parameters.GetParameter("recht")?.Value);
            Assert.True((double)parameters.GetParameter("zorgtoeslag").Value == Math.Round(99.09 * 0.3574, 2));
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1_WithQA_vermogen_aanvrager()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "vermogen_aanvrager");
                argsret = args;
            };
            var parameters = new ParametersCollection() {
                new Parameter("alleenstaande","ja"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                //new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("woonland","Nederland")
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "vermogen_aanvrager");
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
                /*new Parameter("toetsingsinkomen_aanvrager",(double)19000),*/
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("woonland","Nederland")
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
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
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                //new Parameter("woonland","Nederland")
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
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
        public void Execution_ZorgToeslag_2019_Scenario1_WithQA_Alleenstaande()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "alleenstaande");
                argsret = args;
            };
            var parameters = new ParametersCollection() {
                //new Parameter("alleenstaande","ja"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("woonland","Nederland")
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "alleenstaande");
        }

        /// <summary>
        /// Rekenvoorbeeld 2
        /// Wouter woont samen met zijn partner en zijn jaarinkomen is € 19.000. Dat van zijn partner is € 10.500. 
        /// Zijn vermogen is € 1.000 en dat van zijn partner is € 30.000.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario2()
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
                new Parameter("aanvrager_met_toeslagpartner","ja"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)10500),
                new Parameter("vermogen_toeslagpartner",(double)30000),
                new Parameter("woonland","Nederland"),
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((bool)parameters.GetParameter("recht")?.Value);
            Assert.True((double)parameters.GetParameter("zorgtoeslag").Value == 96.43);
        }

        /// <summary>
        /// Rekenvoorbeeld 2
        /// Wouter woont samen met zijn partner en zijn jaarinkomen is € 19.000. Dat van zijn partner is € 20.500. 
        /// Zijn vermogen is € 1.000 en dat van zijn partner is € 30.000.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario2a()
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
                new Parameter("aanvrager_met_toeslagpartner","ja"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)20500),
                new Parameter("vermogen_toeslagpartner",(double)30000),
                new Parameter("woonland","Nederland")
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.False((bool)parameters.GetParameter("recht")?.Value);
            Assert.Null((double?)parameters.GetParameter("zorgtoeslag")?.Value);
        }

        /// <summary>
        /// Rekenvoorbeeld 2b
        /// Wouter woont samen met zijn partner en zijn jaarinkomen is € 19.000. Dat van zijn partner is € 10.500. 
        /// Zijn vermogen is € 1.000 en dat van zijn partner is € 300.000.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario2b()
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
                new Parameter("aanvrager_met_toeslagpartner","ja"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)10500),
                new Parameter("vermogen_toeslagpartner",(double)300000),
                new Parameter("woonland","Nederland")
            };
            var executionResult = new ExecutionResult(ref parameters);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.False((bool)parameters.GetParameter("recht")?.Value);
            Assert.Null((double?)parameters.GetParameter("zorgtoeslag")?.Value);
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario2_WithQA_toetsingsinkomen_toeslagpartner()
        {
            var controller = new YamlScriptController();
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "toetsingsinkomen_toeslagpartner");
                argsret = args;
            };
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new Parameter("aanvrager_met_toeslagpartner","ja"),
                new Parameter("woonland","Nederland"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                //new Parameter("toetsingsinkomen_toeslagpartner",(double)10500),
                new Parameter("vermogen_toeslagpartner",(double)30000)
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "toetsingsinkomen_toeslagpartner");
        }

        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario2_WithQA_vermogen_toeslagpartner()
        {
            var controller = new YamlScriptController();
            QuestionArgs argsret = null;
            var isException = false;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "vermogen_toeslagpartner");
                argsret = args;
            };
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new Parameter("aanvrager_met_toeslagpartner","ja"),
                new Parameter("woonland","Nederland"),
                new Parameter("toetsingsinkomen_aanvrager",(double)19000),
                new Parameter("vermogen_aanvrager",(double)1000),
                new Parameter("toetsingsinkomen_toeslagpartner",(double)10500),
                //new Parameter("vermogen_toeslagpartner",(double)30000)
            };
            try
            {
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "vermogen_toeslagpartner");
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
                var executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
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
