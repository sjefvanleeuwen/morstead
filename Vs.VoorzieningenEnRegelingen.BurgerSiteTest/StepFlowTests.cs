using System;
using Vs.VoorzieningenEnRegelingen.BurgerSite.Pages;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Core.Tests.YamlScripts;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerSite.Library.Object;

namespace Vs.VoorzieningenEnRegelingen.BurgerSiteTest
{
    public class StepFlowTests
    {
        [Fact]
        public void CanDoTwoStepsAndOneBack()
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
                    new Parameter("alleenstaande","ja")
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
    }
}
