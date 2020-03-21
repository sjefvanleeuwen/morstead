using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.Core.TestData.YamlScripts;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class StepFlowTests
    {
        [Fact]
        public void CanDoTwoStepsAndOneBack_1()
        {
            var currentStep = 0;
            var maxStep = 0;

            var sequence = new List<IExecutionResult>();
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
            var sequence = new Sequence()
            {
                Yaml = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml"
            };
            var sequenceController = new SequenceController(serviceController, sequence);

            //execute the first step
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(null);

            Assert.Equal(2, sequenceController.LastExecutionResult.Questions.Parameters.Count);
            Assert.Empty(sequenceController.LastExecutionResult.Parameters);
            Assert.Equal("alleenstaande", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal("aanvrager_met_toeslagpartner", sequenceController.LastExecutionResult.Questions.Parameters[1].Name);
            Assert.Equal(1, sequenceController.CurrentStep);
            Assert.Single(sequenceController.Sequence.Steps);

            //execute the second step
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() { new ClientParameter("alleenstaande", "ja") });

            Assert.Equal(2, sequenceController.LastExecutionResult.Parameters.Count); //standaardpremie is nu ook bekend
            Assert.Equal("alleenstaande", sequenceController.LastExecutionResult.Parameters[0].Name);
            Assert.True((bool)sequenceController.LastExecutionResult.Parameters[0].Value);

            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);

            Assert.Equal(2, sequenceController.LastExecutionResult.Questions.Parameters.Count);
            Assert.Empty(sequenceController.LastExecutionResult.Parameters);
            Assert.Equal("alleenstaande", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal("aanvrager_met_toeslagpartner", sequenceController.LastExecutionResult.Questions.Parameters[1].Name);
            Assert.Single(sequenceController.Sequence.Steps);
        }

        [Fact]
        public void CanDoThreeStepsAndTwoBackAnd1forward()
        {
            //step sequence: 1-2-3-2-1-2
            var serviceController = new ServiceController(null);
            var sequence = new Sequence()
            {
                Yaml = "https://raw.githubusercontent.com/sjefvanleeuwen/virtual-society-urukagina/master/doc/test-payloads/zorgtoeslag-2019.yml"
            };
            var sequenceController = new SequenceController(serviceController, sequence);

            //execute the first step 0 -> 1
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(null);

            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(sequenceController);
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters1(sequenceController);

            //go back one step (it does not work that way, so it should still be the first step)
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);

            //should be the same as after the previous step.
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(sequenceController);
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters1(sequenceController);

            //once again try to go back one step, but this time a value was entered
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() {
                new ClientParameter("alleenstaande", "nee"),
                new ClientParameter("aanvrager_met_toeslagpartner", "ja")
            });

            //should be the same as the previous step
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(sequenceController);
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters2a(sequenceController);

            //increase the step 1 -> 2
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() {
                new ClientParameter("alleenstaande", "ja"),
                new ClientParameter("aanvrager_met_toeslagpartner", "nee")
            });

            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2a(sequenceController);
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters2b(sequenceController);

            //increase the step 2 ->3
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() { new ClientParameter("toetsingsinkomen_aanvrager", "800") });

            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep3(sequenceController);
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(sequenceController);

            //decrease the step 3 -> 2
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);

            //should be the same as when step 2 was executed the first time
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2a(sequenceController);
            //but we still have the same parameters as last step
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(sequenceController);

            //decrease the step 2 -> 1
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);

            //should be the same as when step 1 was executed the first time
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(sequenceController);
            //but we still have the same parameters as last step
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(sequenceController);

            //decrease again (is not possible)
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);

            //should be the same as when step 1 was executed the first time
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(sequenceController);
            //but we still have the same parameters as last step
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(sequenceController);

            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() {
                new ClientParameter("alleenstaande", "ja"),
                new ClientParameter("aanvrager_met_toeslagpartner", "nee")
            });

            //should be the same as when step 1 was executed the first time
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2a(sequenceController);
            //but we still have the same parameters as last step
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(sequenceController);

            //decrease 2 -> 1
            sequenceController.DecreaseStep();
            sequenceController.ExecuteStep(null);
            //then increase with a different value 1 -> 2
            sequenceController.IncreaseStep();
            sequenceController.ExecuteStep(new ParametersCollection() {
                new ClientParameter("alleenstaande", "nee"),
                new ClientParameter("aanvrager_met_toeslagpartner", "ja")
            });
            //should be the same as when step 1 was executed the first time, but we get 1 different variable back
            CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2b(sequenceController);
            //but we should have all the paramaters that were submitted, but now one is changed and an other has a different value
            CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3b(sequenceController);
        }

        /// <summary>
        /// check what should be true after every step 1
        /// </summary>
        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckStep1(SequenceController sequenceController)
        {
            //we are getting 2 parameters in the question
            Assert.Equal(2, sequenceController.LastExecutionResult.Questions.Parameters.Count);
            //check that the prarmeters have the right name and type
            Assert.Equal("alleenstaande", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, sequenceController.LastExecutionResult.Questions.Parameters[0].Type);
            Assert.Equal("aanvrager_met_toeslagpartner", sequenceController.LastExecutionResult.Questions.Parameters[1].Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, sequenceController.LastExecutionResult.Questions.Parameters[1].Type);
            //no parameters known as a response after requesting step 1
            Assert.Empty(sequenceController.LastExecutionResult.Parameters);
            //we should be at step 1
            Assert.Equal(1, sequenceController.CurrentStep);
            //this step should have key 0
            Assert.Single(sequenceController.LastExecutionResult.Stacktrace);
            Assert.Equal(0, sequenceController.LastExecutionResult.Stacktrace.Last().Step.Key);
            //the sequence should only have 1 step
            Assert.Single(sequenceController.Sequence.Steps);
            //and that step should be key 0
            Assert.Equal(0, sequenceController.Sequence.Steps.Last().Key);
        }

        /// <summary>
        /// check what should be true after every step 2
        /// </summary>
        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2a(SequenceController sequenceController)
        {
            //we are getting 1 parameters in the question
            Assert.Single(sequenceController.LastExecutionResult.Questions.Parameters);
            //check that the prarmeters have the right name and type
            Assert.Equal("toetsingsinkomen_aanvrager", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, sequenceController.LastExecutionResult.Questions.Parameters[0].Type);
            //3 parameters known as a response after requesting step 1
            Assert.Equal(3, sequenceController.LastExecutionResult.Parameters.Count);
            //check that the parameters have the right name and type & value
            var var1 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.True((bool)var1.Value);
            var var2 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.False((bool)var2.Value);
            var var3 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "standaardpremie");
            Assert.NotNull(var3);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var3.Type);
            Assert.Equal(1609, (double)var3.Value);
            //we should be at step 2
            Assert.Equal(2, sequenceController.CurrentStep);
            //this step should have key 1
            Assert.Equal(2, sequenceController.LastExecutionResult.Stacktrace.Count);
            Assert.Equal(1, sequenceController.LastExecutionResult.Stacktrace.Last().Step.Key);
            //the sequence should have 2 steps
            Assert.Equal(2, sequenceController.Sequence.Steps.Count());
            //and the last step should be key 1
            Assert.Equal(1, sequenceController.Sequence.Steps.Last().Key);
        }

        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckStep2b(SequenceController sequenceController)
        {
            //we are getting 1 parameters in the question
            Assert.Single(sequenceController.LastExecutionResult.Questions.Parameters);
            //check that the prarmeters have the right name and type
            Assert.Equal("toetsingsinkomen_aanvrager", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, sequenceController.LastExecutionResult.Questions.Parameters[0].Type);
            //3 parameters known as a response after requesting step 1
            Assert.Equal(3, sequenceController.LastExecutionResult.Parameters.Count);
            //check that the parameters have the right name and type & value
            var var1 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.False((bool)var1.Value);
            var var2 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.True((bool)var2.Value);
            var var3 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "standaardpremie");
            Assert.NotNull(var3);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var3.Type);
            Assert.Equal(3218, (double)var3.Value);
            //we should be at step 2
            Assert.Equal(2, sequenceController.CurrentStep);
            //this step should have key 1
            Assert.Equal(2, sequenceController.LastExecutionResult.Stacktrace.Count);
            Assert.Equal(1, sequenceController.LastExecutionResult.Stacktrace.Last().Step.Key);
            //the sequence should have 2 steps
            Assert.Equal(2, sequenceController.Sequence.Steps.Count());
            //and the last step should be key 1
            Assert.Equal(1, sequenceController.Sequence.Steps.Last().Key);
        }

        /// <summary>
        /// check what should be true after every step 3
        /// </summary>
        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckStep3(SequenceController sequenceController)
        {
            //we are getting 1 parameters in the question
            Assert.Single(sequenceController.LastExecutionResult.Questions.Parameters);
            //check that the prarmeters have the right name and type
            Assert.Equal("toetsingsinkomen_toeslagpartner", sequenceController.LastExecutionResult.Questions.Parameters[0].Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, sequenceController.LastExecutionResult.Questions.Parameters[0].Type);
            //4 parameters known as a response after requesting step 3
            Assert.Equal(4, sequenceController.LastExecutionResult.Parameters.Count);
            //check that the parameters have the right name and type & value
            var var1 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.True((bool)var1.Value);
            var var2 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.False((bool)var2.Value);
            var var3 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "standaardpremie");
            Assert.NotNull(var3);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var3.Type);
            Assert.Equal(1609, (double)var3.Value);
            var var4 = sequenceController.LastExecutionResult.Parameters.FirstOrDefault(p => p.Name == "toetsingsinkomen_aanvrager");
            Assert.NotNull(var4);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var4.Type);
            Assert.Equal(800, (double)var4.Value);
            //we should be at step 3
            Assert.Equal(3, sequenceController.CurrentStep);
            //this step should have key 1 i.e. the same question requires 2 variable, thus now we have the same question
            Assert.Equal(2, sequenceController.LastExecutionResult.Stacktrace.Count);
            Assert.Equal(1, sequenceController.LastExecutionResult.Stacktrace.Last().Step.Key);
            //the sequence should have 3 steps
            Assert.Equal(3, sequenceController.Sequence.Steps.Count());
            //and the last step should be key 1
            Assert.Equal(1, sequenceController.Sequence.Steps.Last().Key);
        }

        /// <summary>
        /// check what should be true after the maximum step done was only 1
        /// </summary>
        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters1(SequenceController sequenceController)
        {
            //no parameters yet
            Assert.Empty(sequenceController.Sequence.Parameters);
        }

        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters2a(SequenceController sequenceController)
        {
            //we should have saved 2 variables in steps
            Assert.Equal(2, sequenceController.Sequence.Parameters.Count);
            //check these variables
            var var1 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.False((bool)var1.Value);
            Assert.Equal("false", var1.ValueAsString.ToLower());
            var var2 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.True((bool)var2.Value);
            Assert.Equal("true", var2.ValueAsString.ToLower());
        }

        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters2b(SequenceController sequenceController)
        {
            //we should have saved 1 variables in steps
            Assert.Equal(2, sequenceController.Sequence.Parameters.Count);
            //check these variables
            var var1 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.True((bool)var1.Value);
            Assert.Equal("true", var1.ValueAsString.ToLower());
            var var2 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.False((bool)var2.Value);
            Assert.Equal("false", var2.ValueAsString.ToLower());
        }

        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3a(SequenceController sequenceController)
        {
            //we should have saved 3 variables in steps
            Assert.Equal(3, sequenceController.Sequence.Parameters.Count);
            //check these variables
            var var1 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.True((bool)var1.Value);
            Assert.Equal("true", var1.ValueAsString.ToLower());
            var var2 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.False((bool)var2.Value);
            Assert.Equal("false", var2.ValueAsString.ToLower());
            var var3 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "toetsingsinkomen_aanvrager");
            Assert.NotNull(var3);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var3.Type);
            Assert.Equal(800, (double)var3.Value);
            Assert.Equal("800", var3.ValueAsString.ToLower());
        }

        private void CanDoThreeStepsAndTwoBackAnd1forward_CheckParameters3b(SequenceController sequenceController)
        {
            //we should have saved 2 variables in steps
            Assert.Equal(3, sequenceController.Sequence.Parameters.Count);
            //check these variables
            var var1 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "alleenstaande");
            Assert.NotNull(var1);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var1.Type);
            Assert.False((bool)var1.Value);
            Assert.Equal("false", var1.ValueAsString.ToLower());
            var var2 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "aanvrager_met_toeslagpartner");
            Assert.NotNull(var2);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, var2.Type);
            Assert.True((bool)var2.Value);
            Assert.Equal("true", var2.ValueAsString.ToLower());
            var var3 = sequenceController.Sequence.Parameters.FirstOrDefault(p => p.Name == "toetsingsinkomen_aanvrager");
            Assert.NotNull(var3);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, var3.Type);
            Assert.Equal(800, (double)var3.Value);
            Assert.Equal("800", var3.ValueAsString.ToLower());
        }
    }
}
