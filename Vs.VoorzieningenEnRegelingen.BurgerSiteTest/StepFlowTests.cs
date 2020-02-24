using System;
using Vs.VoorzieningenEnRegelingen.BurgerSite.Pages;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;
using System.Collections.Generic;

namespace Vs.VoorzieningenEnRegelingen.BurgerSiteTest
{
    public class StepFlowTests
    {
        [Fact]
        public void CanDoTwoSteps()
        {
            var currentStep = 0;

            var sequence = new List<ExecutionResult>();
            ServiceController controller = new ServiceController(null);
            var executeRequest = new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body
            };
            var result = controller.Execute(executeRequest);
            
            sequence.Add(result);
            Assert.Equal(2, sequence[currentStep].Questions.Parameters.Count);
            Assert.Empty(sequence[currentStep].Parameters);
            Assert.True(sequence[currentStep].Questions.Parameters[0].Name=="alleenstaande");
            Assert.True(sequence[currentStep].Questions.Parameters[1].Name == "aanvrager_met_toeslagpartner");
            var displaySequence = sequence[currentStep];
            //formElement(displaySequence)

            currentStep++;
            executeRequest = new ExecuteRequest()
            {
                Config = YamlZorgtoeslag.Body,
                Parameters = new ParametersCollection() {
                    new Parameter("alleenstaande","ja")
                }
            };

            result = controller.Execute(executeRequest);
            sequence.Add(result);
            Assert.True(sequence[currentStep].Parameters.Count == 1);
            Assert.True(sequence[currentStep].Parameters[0].Name == "alleenstaande");
            Assert.True((bool)sequence[currentStep].Parameters[0].Value);
            displaySequence = sequence[currentStep];
            //formElement(displaySequence)

            // stap terug
            currentStep--;
            //do not execute
            displaySequence = sequence[currentStep];
            //formElement(displaySequence)
            //displaySequence.Parameters = sequence[sequence.Count - 1].Parameters;
            Assert.True(sequence.Count > 1);


            // prefill form in sequence 0 with value from sequence 1
            
            var radiobuttonValue = sequence[1].Parameters[0];

        }
    }
}
