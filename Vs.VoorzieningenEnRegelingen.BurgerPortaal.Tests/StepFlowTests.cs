using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class StepFlowTests
    {
        [Fact]
        public void CanDoTwoStepsAndOneBack_1()
        {
            var currentStep = 0;
            var maxStep = 0;

            var sequence = new List<ExecutionResult>();
            ServiceController controller = new ServiceController(null);
            var executeRequest = new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body
            };
            var result = controller.Execute(executeRequest);
            
            sequence.Add(result);
            var displaySequence = new DisplayExecutionResult(sequence[currentStep], sequence[maxStep].Parameters);
            Assert.Equal(2, displaySequence.Questions.Parameters.Count);
            Assert.Empty(displaySequence.Parameters);
            Assert.Equal("alleenstaande", displaySequence.Questions.Parameters[0].Name);
            Assert.Equal("aanvrager_met_toeslagpartner", displaySequence.Questions.Parameters[1].Name);

            //next step            
            currentStep++;
            maxStep++;
            executeRequest = new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body,
                Parameters = new ParametersCollection() {
                    new ClientParameter("alleenstaande","ja")
                }
            };

            result = controller.Execute(executeRequest);
            sequence.Add(result);
            displaySequence = new DisplayExecutionResult(sequence[currentStep], sequence[maxStep].Parameters);
            Assert.Single(displaySequence.Parameters);
            Assert.Equal("alleenstaande", displaySequence.Parameters[0].Name);
            Assert.True((bool)displaySequence.Parameters[0].Value);
            Assert.Empty(sequence[0].Parameters);

            // stap back
            currentStep--;
            //do not execute

            displaySequence = new DisplayExecutionResult(sequence[currentStep], sequence[maxStep].Parameters);
            Assert.Equal(2, displaySequence.Questions.Parameters.Count);
            Assert.NotEmpty(displaySequence.Parameters);
            Assert.Single(displaySequence.Parameters);
            Assert.Equal("alleenstaande", displaySequence.Questions.Parameters[0].Name);
            Assert.Equal("aanvrager_met_toeslagpartner", displaySequence.Questions.Parameters[1].Name);
            Assert.True(sequence.Count > 1);
        }

        [Fact]
        public void CanDoTwoStepsAndOneBack_2()
        {
            var serviceController = new ServiceController(null);
            var sequenceController = new SequenceController(serviceController);
            //execute the first step
            sequenceController.RequestStep++;
            sequenceController.ExecuteStep(null);

            Assert.Equal(1, sequenceController.CurrentStep);
            Assert.Single(sequenceController.Sequence.Steps);
            //.Questions.Parameters.Count);
            //Assert.Empty(displaySequence.Parameters);
            //Assert.Equal("alleenstaande", displaySequence.Questions.Parameters[0].Name);
            //Assert.Equal("aanvrager_met_toeslagpartner", displaySequence.Questions.Parameters[1].Name);

            //    new Parameter("alleenstaande", "ja")

            //step 1 is done
            //Assert.Empty(calculation.)
        }
    }
}
