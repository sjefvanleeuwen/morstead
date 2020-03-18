using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class StepKeuzeTests
    {
        private string _testYaml1 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Test A of B
   omschrijving: test
   keuze:
   - situatie: A
   - situatie: B
   recht: A
formules:
 - drempelinkomen:
     formule: 20941";

        private string _testYaml2 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Test A of B
   keuze:
   - situatie: A
   - situatie: B
   recht: A
formules:
 - drempelinkomen:
     formule: 20941";

        private string _testYaml3 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Test A of B
   recht: A
formules:
 - drempelinkomen:
     formule: 20941";

        private string _testYaml4 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: Wat is uw woonsituatie?
   formule: standaardpremie
formules:
 - standaardpremie:
   - situatie: alleenstaande
     formule: 1609
   - situatie: aanvrager_met_toeslagpartner
     formule: 3218";

        private string _testYaml5 = @"# Zorgtoeslag for burger site demo
stuurinformatie:
  onderwerp: zorgtoeslag
  organisatie: belastingdienst
  type: toeslagen
  domein: zorg
  versie: 1.0
  status: ontwikkel
  jaar: 2019
  bron: https://download.belastingdienst.nl/toeslagen/docs/berekening_zorgtoeslag_2019_tg0821z91fd.pdf
berekening:
 - stap: woonland
   formule: woonlandfactor
   recht: woonlandfactor > 0
formules:
 - woonlandfactor:
   formule: lookup('woonlandfactoren',woonland,'woonland','factor', 0)
tabellen:
  - naam: woonlandfactoren
    woonland, factor:
      - [ Nederland,           1.0    ]
      - [ België,              0.7392 ]
      - [ Bosnië-Herzegovina,  0.0672 ]
      - [ Bulgarije,           0.0735 ]
      - [ Cyprus,              0.1363 ]
      - [ Denemarken,          0.9951 ]";

        [Fact]
        public void ShouldAcceptKeuze()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml1);
            Assert.False(parseResult.IsError);
        }

        [Fact]
        public void ShouldStillReadDescription()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml1);
            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal("Test A of B", stepToTest.Name);
            Assert.Equal("test", stepToTest.Description);
        }

        [Fact]
        public void ShouldNotNeedDescription()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml2);

            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal("Test A of B", stepToTest.Name);
            Assert.Equal("Test A of B", stepToTest.Description);
        }

        [Fact]
        public void ShouldHaveTwoChoices()
        {
            //List<ParametersCollection> parameters = new List<ParametersCollection>();
            var controller = new YamlScriptController();
            var parseResult = controller.Parse(_testYaml2);

            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal(2, stepToTest.Choices.Count());
            Assert.Equal("A", stepToTest.Choices.ElementAt(0).Situation);
            Assert.Equal("B", stepToTest.Choices.ElementAt(1).Situation);
        }

        //does not work, it is caught, should be refactored
        //[Fact]
        //public void ShouldThrowExceptionIfNoFormulaORChoiceProvided()
        //{
        //    //List<ParametersCollection> parameters = new List<ParametersCollection>();
        //    var controller = new YamlScriptController();
        //    var parseResult = controller.Parse(_testYaml3);
        //    //generate empty call
        //    var parameters = new ParametersCollection() as IParametersCollection;
        //    var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;
        //    Assert.Throws<StepException>(() => controller.ExecuteWorkflow(ref parameters, ref executionResult));
        //}

        [Fact]
        public void ShouldReturnBooleanAsFirstQuestionOldSituation()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection() as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController();
            controller.Parse(_testYaml4);
            //set the callback
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }

            Assert.True(isException);
            Assert.Empty(executionResult.Parameters);
            Assert.Equal(2, executionResult.Questions.Parameters.Count);
            Assert.Equal("alleenstaande", executionResult.Questions.Parameters[0].Name);
            Assert.Equal("aanvrager_met_toeslagpartner", executionResult.Questions.Parameters[1].Name);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("Wat is uw woonsituatie?", executionResult.Stacktrace.First().Step.Description);
        }

        [Fact]
        public void ShouldReturnBooleanAsFirstQuestionOldSituation2()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection() as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController();
            //controller.Parse(_testYaml5);
            controller.Parse(YamlZorgtoeslag4.Body);
            //set the callback
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }

            Assert.True(isException);
            Assert.Empty(executionResult.Parameters);
            Assert.Single(executionResult.Questions.Parameters);
            Assert.Equal("woonland", executionResult.Questions.Parameters[0].Name);
            Assert.Equal(41, ((List<object>)executionResult.Questions.Parameters[0].Value).Count);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("woonland", executionResult.Stacktrace.First().Step.Description);
        }

        [Fact]
        public void ShouldReturnBooleanAsFirstQuestionNewSituation()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection() as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController();
            controller.Parse(_testYaml2);
            //set the callback
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }

            Assert.True(isException);
            Assert.Empty(executionResult.Parameters);
            Assert.Equal(2, executionResult.Questions.Parameters.Count);
            Assert.Equal("A", executionResult.Questions.Parameters[0].Name);
            Assert.Equal("B", executionResult.Questions.Parameters[1].Name);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("Test A of B", executionResult.Stacktrace.First().Step.Description);
        }

        [Fact]
        public void ShouldReturnRight()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection
            {
                new ClientParameter("A","ja"),
                new ClientParameter("B","nee")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController();
            controller.Parse(_testYaml2);
            //set the callback
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }

            Assert.False(isException);
            Assert.Equal(3, executionResult.Parameters.Count);
            Assert.Equal("A", executionResult.Parameters[0].Name);
            Assert.True((bool)executionResult.Parameters[0].Value);
            Assert.Equal("B", executionResult.Parameters[1].Name);
            Assert.False((bool)executionResult.Parameters[1].Value);
            Assert.Equal("recht", executionResult.Parameters[2].Name);
            Assert.True((bool)executionResult.Parameters[2].Value);
            Assert.Null(executionResult.Questions);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("Test A of B", executionResult.Stacktrace.First().Step.Description);
        }

        [Fact]
        public void ShouldReturnNoRight()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection
            {
                new ClientParameter("A","nee"),
                new ClientParameter("B","ja")
            } as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController();
            controller.Parse(_testYaml2);
            //set the callback
            controller.QuestionCallback = (FormulaExpressionContext sender, QuestionArgs args) =>
            {
                executionResult.Questions = args;
            };

            try
            {
                controller.ExecuteWorkflow(ref parameters, ref executionResult);
            }
            catch (UnresolvedException)
            {
                isException = true;
            }

            Assert.False(isException);
            Assert.Equal(3, executionResult.Parameters.Count);
            Assert.Equal("A", executionResult.Parameters[0].Name);
            Assert.False((bool)executionResult.Parameters[0].Value);
            Assert.Equal("B", executionResult.Parameters[1].Name);
            Assert.True((bool)executionResult.Parameters[1].Value);
            Assert.Equal("recht", executionResult.Parameters[2].Name);
            Assert.False((bool)executionResult.Parameters[2].Value);
            Assert.Null(executionResult.Questions);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("Test A of B", executionResult.Stacktrace.First().Step.Description);
        }
    }
}
