using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var parameters = new ParametersCollection() as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)60000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)150000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Malta")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((bool)parameters.GetParameter("recht")?.Value);
            Assert.True((double)parameters.GetParameter("zorgtoeslag").Value == 35.42);
        }

        /// <summary>
        /// Rekenvoorbeeld 1b
        /// Wouter is een alleenstaande man met een jaarinkomen van € 19.000 en een vermogen van € 1.000.
        /// Hij woont in Rusland.
        /// bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
        /// </summary>
        [Fact]
        public void Execution_ZorgToeslag_2019_Scenario1d()
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Rusland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.False((bool)parameters.GetParameter("recht")?.Value);
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                //new Parameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                /*new Parameter("toetsingsinkomen_aanvrager",(double)19000),*/
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                //new Parameter("woonland","Nederland")
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("aanvrager_met_toeslagpartner","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("toetsingsinkomen_toeslagpartner",(double)10500),
                new ClientParameter("vermogen_toeslagpartner",(double)30000),
                new ClientParameter("woonland","Nederland"),
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("aanvrager_met_toeslagpartner","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("toetsingsinkomen_toeslagpartner",(double)20500),
                new ClientParameter("vermogen_toeslagpartner",(double)30000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("aanvrager_met_toeslagpartner","ja"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("toetsingsinkomen_toeslagpartner",(double)10500),
                new ClientParameter("vermogen_toeslagpartner",(double)300000),
                new ClientParameter("woonland","Nederland")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("aanvrager_met_toeslagpartner","ja"),
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("vermogen_toeslagpartner",(double)30000)
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
                new ClientParameter("aanvrager_met_toeslagpartner","ja"),
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("toetsingsinkomen_aanvrager",(double)19000),
                new ClientParameter("vermogen_aanvrager",(double)1000),
                new ClientParameter("toetsingsinkomen_toeslagpartner",(double)10500),
                //new Parameter("vermogen_toeslagpartner",(double)30000)
            } as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            Assert.NotNull(argsret);
            Assert.True(argsret.Parameters[0].Name == "vermogen_toeslagpartner");
            Assert.True(argsret.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Double);
            Assert.True((double)argsret.Parameters[0].Value == 0);
            Assert.True(argsret.Parameters[0].ValueAsString == "0");
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
                Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Boolean);
                Assert.True(args.Parameters[0].ValueAsString == "False");
                Assert.False((bool)args.Parameters[0].Value);
                Assert.True(args.Parameters[1].Name == "aanvrager_met_toeslagpartner");
                Assert.True(args.Parameters[1].Type == TypeInference.InferenceResult.TypeEnum.Boolean);
                Assert.True(args.Parameters[1].ValueAsString == "False");
                Assert.False((bool)args.Parameters[1].Value);
                argsret = args;
            };
            var parameters = new ParametersCollection() as IParametersCollection;
            try
            {
                var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
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
        }

        [Fact]
        void Execution_ZorgToeslag_2019_Full_Scenario1()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag.Body);
            Assert.False(result.IsError);
            bool isException = false;
            var executionResult = null as IExecutionResult;
            var parameters = new ParametersCollection() as IParametersCollection;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                switch (args.Parameters[0].Name) {
                    case "alleenstaande":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Boolean);
                        parameters.Add(new ClientParameter("alleenstaande", true));
                        break;
                    case "woonland":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.List);
                        parameters.Add(new ClientParameter("woonland", "Finland"));
                        break;
                    case "vermogen_aanvrager":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Double);
                        parameters.Add(new ClientParameter("vermogen_aanvrager",15000));
                        break;
                    case "toetsingsinkomen_aanvrager":
                        Assert.True(args.Parameters[0].Type == TypeInference.InferenceResult.TypeEnum.Double);
                        parameters.Add(new ClientParameter("toetsingsinkomen_aanvrager", 1500));
                        break;
                }
            };

            try
            {
                executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            isException = false;
            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.False(isException);
        }

        [Fact]
        public void Execution_YamlVermogensgrens_WithQA()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlVermogensgrens.Body);
            Assert.False(result.IsError);
            bool isException = false;
            QuestionArgs argsret = null;
            var executionResult = null as IExecutionResult;
            var parameters = new ParametersCollection() as IParametersCollection;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "keuze_boven_vermogensgrens");
                parameters.Add(new ClientParameter("keuze_boven_vermogensgrens", true));
            };

            try
            {
                executionResult = new ExecutionResult(ref parameters);
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }
            Assert.True(isException);
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True(executionResult.Stacktrace.Last().IsStopExecution);
        }

        [Fact]
        public void HashExecutionTest()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlHashExecutionTests.Body);
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // Should not be called.
                Assert.False(true);
            };
            Assert.True(result.Model.Steps.Count == 3);
            Assert.True(result.Model.Steps[0].Key == 0);
            Assert.True(result.Model.Steps[1].Key == 1);
            Assert.True(result.Model.Steps[2].Key == 2);
            Assert.False(result.IsError);
            var executionResult = null as IExecutionResult;
            var parameters = new ParametersCollection() as IParametersCollection;
            var stepSequence = new List<string>();
            // Scenario 1: User fill in value 1 in first step.
            parameters.Add(new ClientParameter("formule1_waarde", "1"));
            executionResult = new ExecutionResult(ref parameters);
            var executionResult1 = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((double)parameters[4].Value == 1000);
            StringBuilder sb = new StringBuilder();
            stepSequence.Add(string.Join(",", executionResult1.Stacktrace.Select(p => p.Step.Key).ToArray()));
            parameters.Clear();
            // Scenario 2: User fills in value 2 (in first step).
            parameters.Add(new ClientParameter("formule1_waarde", "-1"));
            executionResult = new ExecutionResult(ref parameters);
            var executionResult2 = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            //string.Join(",", integerArray.Select(i => i.ToString()).ToArray());
            Assert.True((double)parameters[3].Value == -100);
            stepSequence.Add(string.Join(",", executionResult2.Stacktrace.Select(p => p.Step.Key).ToArray()));
            // Get differences between sequence flows
            Assert.True(stepSequence[0] == "0,2");
            Assert.True(stepSequence[1] == "0,1");

            var delta = executionResult1.Stacktrace.Except(executionResult2.Stacktrace);
        }

        [Fact]
        public void FormulaResolvesToCorrectSituationalFunctionV3()
        {
            var controller = new YamlScriptController();
            var result = controller.Parse(YamlZorgtoeslag3.Body);
            Assert.False(result.IsError);
            bool isException = false;
            var executionResult = null as IExecutionResult;

            var parameters = new ParametersCollection() {
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("alleenstaande",true),
                new ClientParameter("aanvrager_met_toeslagpartner",false),
                new ClientParameter("hoger_dan_de_inkomensdrempel",false),
                new ClientParameter("lager_dan_de_inkomensdrempel",true),
                new ClientParameter("lager_dan_de_vermogensdrempel",true),
                new ClientParameter("hoger_dan_de_vermogensdrempel",false),
                new ClientParameter("toetsingsinkomen_aanvrager",15000),
            } as IParametersCollection;
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                Assert.True(args.Parameters[0].Name == "keuze_boven_vermogensgrens");
                parameters.Add(new ClientParameter("keuze_boven_vermogensgrens", true));
            };
            executionResult = new ExecutionResult(ref parameters);
            executionResult = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((double)executionResult.Parameters.First(p => p.Name == "zorgtoeslag").Value == 99.09);
            parameters = new ParametersCollection() {
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("alleenstaande",false),
                new ClientParameter("aanvrager_met_toeslagpartner",true),
                new ClientParameter("hoger_dan_de_inkomensdrempel",false),
                new ClientParameter("lager_dan_de_inkomensdrempel",true),
                new ClientParameter("lager_dan_de_vermogensdrempel",true),
                new ClientParameter("hoger_dan_de_vermogensdrempel",false),
                new ClientParameter("toetsingsinkomen_gezamenlijk",15000),
            } as IParametersCollection;
            executionResult = new ExecutionResult(ref parameters);
            executionResult = controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.True((double)executionResult.Parameters.First(p => p.Name == "zorgtoeslag").Value == 233.18);
        }

        [Fact]
        public void FormulaResolvesToCorrectSituationalFunctionV4_1()
        {
            //based on version 4 of the yaml
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
            var result = controller.Parse(YamlZorgtoeslag4.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("aanvrager_met_toeslagpartner","nee"),
                new ClientParameter("hoger_dan_vermogensdrempel","nee"),
                new ClientParameter("lager_dan_vermogensdrempel","ja"),
                new ClientParameter("hoger_dan_inkomensdrempel","nee"),
                new ClientParameter("lager_dan_inkomensdrempel","ja"),
                new ClientParameter("toetsingsinkomen", (double)19000),
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.Equal(99.09, (double)parameters.GetParameter("zorgtoeslag").Value);
        }

        [Fact]
        public void FormulaResolvesToCorrectSituationalFunctionV4_2()
        {
            //based on version 4 of the yaml
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
            var result = controller.Parse(YamlZorgtoeslag4.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("alleenstaande","ja"),
                new ClientParameter("aanvrager_met_toeslagpartner","nee"),
                new ClientParameter("hoger_dan_vermogensdrempel","nee"),
                new ClientParameter("lager_dan_vermogensdrempel","ja"),
                new ClientParameter("hoger_dan_inkomensdrempel","nee"),
                new ClientParameter("lager_dan_inkomensdrempel","ja"),
                new ClientParameter("toetsingsinkomen", (double)27000),
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.Equal(35.00, (double)parameters.GetParameter("zorgtoeslag").Value);
        }

        [Fact]
        [Trait("Category", "Unfinished")]
        public void FormulaResolvesToCorrectSituationalFunctionV4_3()
        {
            //based on version 4 of the yaml
            var controller = new YamlScriptController();
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                // should not be called.
                throw new Exception("Questioncallback should not be called.");
            };
            var result = controller.Parse(YamlZorgtoeslag4.Body);
            Assert.False(result.IsError);
            var parameters = new ParametersCollection() {
                new ClientParameter("woonland","Nederland"),
                new ClientParameter("alleenstaande",false),
                new ClientParameter("aanvrager_met_toeslagpartner",true),
                new ClientParameter("hoger_dan_vermogensdrempel",false),
                new ClientParameter("lager_dan_vermogensdrempel",true),
                new ClientParameter("hoger_dan_inkomensdrempel",false),
                new ClientParameter("lager_dan_inkomensdrempel",true),
                new ClientParameter("toetsingsinkomen", (double)19000),
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
            controller.ExecuteWorkflow(ref parameters, ref executionResult);
            Assert.Equal(233.18, (double)parameters.GetParameter("zorgtoeslag").Value);
        }
    }
}
