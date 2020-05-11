using Moq;
using System.Linq;
using Vs.Rules.Core.Exceptions;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Routing.Controllers.Interfaces;
using Vs.Rules.Routing.Model.Interfaces;
using Xunit;

namespace Vs.Rules.Core.Tests
{
    public class StepValueTests
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
- stap: toetsingsinkomen
  waarde: toetsingsinkomen
formules:
 - drempelinkomen:
     formule: 20941";



        [Fact]
        public void ShouldAcceptWaarde()
        {
            var controller = new YamlScriptController(InitMoqRoutingController());
            var parseResult = controller.Parse(_testYaml1);
            Assert.False(parseResult.IsError);
        }

        [Fact]
        public void ShouldStillReadDescription()
        {
            var controller = new YamlScriptController(InitMoqRoutingController());
            var parseResult = controller.Parse(_testYaml1);
            Assert.Single(parseResult.Model.Steps);
            var stepToTest = parseResult.Model.Steps.First();
            Assert.Equal("toetsingsinkomen", stepToTest.Name);
            Assert.Equal("toetsingsinkomen", stepToTest.Description);
        }

        [Fact]
        public void ShouldAskForValue()
        {
            var isException = false;

            //prepare the result
            var parameters = new ParametersCollection() as IParametersCollection;
            var executionResult = new ExecutionResult(ref parameters) as IExecutionResult;

            //prepare yaml definition
            var controller = new YamlScriptController(InitMoqRoutingController());
            //controller.Parse(_testYaml5);
            controller.Parse(_testYaml1);
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
            Assert.Equal("toetsingsinkomen", executionResult.Questions.Parameters[0].Name);
            Assert.Single(executionResult.Stacktrace);
            Assert.Equal("toetsingsinkomen", executionResult.Stacktrace.First().Step.Description);
        }

        private IRoutingController InitMoqRoutingController()
        {
            var moqRoutingController = new Mock<IRoutingController>();
            moqRoutingController.Setup(m => m.RoutingConfiguration).Returns(null as IRoutingConfiguration);
            return moqRoutingController.Object;
        }
    }
}
