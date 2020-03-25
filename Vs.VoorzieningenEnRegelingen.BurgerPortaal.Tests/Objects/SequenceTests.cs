using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects
{
    public class SequenceTests
    {
        [Fact]
        public void ShouldUpdateParametersCollection()
        {
            var sut = new Sequence();
            sut.UpdateParametersCollection(InitMoqClientParameterCollection());

            Assert.Equal(3, sut.Parameters.GetAll().Count());
            Assert.Equal("boolean_test1", sut.Parameters.GetAll().ElementAt(0).Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, sut.Parameters.GetAll().ElementAt(0).Type);
            Assert.True((bool)sut.Parameters.GetAll().ElementAt(0).Value);
            Assert.Equal("double_test3", sut.Parameters.GetAll().ElementAt(1).Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.String, sut.Parameters.GetAll().ElementAt(1).Type);
            Assert.Equal("test2_waarde", (string)sut.Parameters.GetAll().ElementAt(1).Value);
            Assert.Equal("string_test4", sut.Parameters.GetAll().ElementAt(2).Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Double, sut.Parameters.GetAll().ElementAt(2).Type);
            Assert.Equal(125.01, (double)sut.Parameters.GetAll().ElementAt(2).Value);
        }

        [Fact]
        public void GetParametersToSend()
        {
            var sut = new Sequence();
            sut.UpdateParametersCollection(InitMoqClientParameterCollection());
            sut.AddStep(1, InitMoqExecutionResultsWithBoolean());
            sut.AddStep(2, InitMoqExecutionResultsWithDouble());
            sut.AddStep(3, InitMoqExecutionResultsWithString());

            //get for first question. should be nothing
            var result = sut.GetParametersToSend(1);
            Assert.Empty(result.GetAll());

            //get for 2nd question, should only be the 1st value
            result = sut.GetParametersToSend(2);
            Assert.Single(result.GetAll());
            Assert.Equal("boolean_test1", result.GetAll().ElementAt(0).Name);

            //get for the newest question, should be everything
            result = sut.GetParametersToSend(4);
            Assert.Equal(3, result.GetAll().Count());
            Assert.Equal("boolean_test1", result.GetAll().ElementAt(0).Name);
            Assert.Equal("double_test3", result.GetAll().ElementAt(1).Name);
            Assert.Equal("string_test4", result.GetAll().ElementAt(2).Name);

            //get for a high question. Should be everything
            result = sut.GetParametersToSend(100);
            Assert.Equal(3, result.GetAll().Count());
            Assert.Equal("boolean_test1", result.GetAll().ElementAt(0).Name);
            Assert.Equal("double_test3", result.GetAll().ElementAt(1).Name);
            Assert.Equal("string_test4", result.GetAll().ElementAt(2).Name);
        }

        [Fact]
        public void ShouldAddStepBoolean()
        {
            var sut = new Sequence();
            sut.AddStep(1, InitMoqExecutionResultsWithBoolean());
            Assert.Single(sut.Steps);
            Assert.NotNull(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.Null(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal(2, sut.Steps.ElementAt(0).ValidParameterNames.Count());
            Assert.Equal("boolean_test1", sut.Steps.ElementAt(0).ValidParameterNames.First());
            Assert.Equal("boolean_test2", sut.Steps.ElementAt(0).ValidParameterNames.Last());
        }

        [Fact]
        public void ShouldAddStepDouble()
        {
            var sut = new Sequence();
            sut.AddStep(1, InitMoqExecutionResultsWithDouble());
            Assert.Single(sut.Steps);
            Assert.Null(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.NotNull(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal("double_test3", sut.Steps.ElementAt(0).ParameterName);
        }

        [Fact]
        public void ShouldAddStepString()
        {
            var sut = new Sequence();
            sut.AddStep(1, InitMoqExecutionResultsWithString());
            Assert.Single(sut.Steps);
            Assert.Null(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.NotNull(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal("string_test4", sut.Steps.ElementAt(0).ParameterName);
        }

        [Fact]
        public void ShouldAddStepMultipleScenarios()
        {
            var sut = new Sequence();

            sut.AddStep(1, InitMoqExecutionResultsWithBoolean());

            //already covered in an other test

            sut.AddStep(2, InitMoqExecutionResultsWithDouble());

            Assert.Equal(2, sut.Steps.Count());

            Assert.NotNull(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.Null(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal(2, sut.Steps.ElementAt(0).ValidParameterNames.Count());
            Assert.Equal("boolean_test1", sut.Steps.ElementAt(0).ValidParameterNames.First());
            Assert.Equal("boolean_test2", sut.Steps.ElementAt(0).ValidParameterNames.Last());

            Assert.Null(sut.Steps.ElementAt(1).ValidParameterNames);
            Assert.NotNull(sut.Steps.ElementAt(1).ParameterName);
            Assert.Equal("double_test3", sut.Steps.ElementAt(1).ParameterName);

            //now add step 2 again with new values (3rd executionresult)
            //only the 1rst one is changed
            sut.AddStep(2, InitMoqExecutionResultsWithString());

            Assert.Equal(2, sut.Steps.Count());

            Assert.NotNull(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.Null(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal(2, sut.Steps.ElementAt(0).ValidParameterNames.Count());
            Assert.Equal("boolean_test1", sut.Steps.ElementAt(0).ValidParameterNames.First());
            Assert.Equal("boolean_test2", sut.Steps.ElementAt(0).ValidParameterNames.Last());

            Assert.Null(sut.Steps.ElementAt(1).ValidParameterNames);
            Assert.NotNull(sut.Steps.ElementAt(1).ParameterName);
            Assert.Equal("string_test4", sut.Steps.ElementAt(1).ParameterName);

            //add step at 1; everything after step 1 should be gone
            sut.AddStep(1, InitMoqExecutionResultsWithString());

            Assert.Single(sut.Steps);
            Assert.Null(sut.Steps.ElementAt(0).ValidParameterNames);
            Assert.NotNull(sut.Steps.ElementAt(0).ParameterName);
            Assert.Equal("string_test4", sut.Steps.ElementAt(0).ParameterName);
        }

        /// <summary>
        /// Initiates a parametercollection with the parameters corresponding to the numbers provided
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private IParametersCollection InitMoqClientParameterCollection()
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IClientParameter> {
                InitMoqClientParameter(1),
                InitMoqClientParameter(2),
                InitMoqClientParameter(3) });
            return moq.Object;
        }

        /// <summary>
        /// Initiates a client parameter corresponding with the number asked
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private IClientParameter InitMoqClientParameter(int i)
        {
            var moq = new Mock<IClientParameter>();
            if (i == 1)
            {
                moq.Setup(m => m.Name).Returns("boolean_test1");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
                moq.Setup(m => m.Value).Returns(true);
            }
            if (i == 2)
            {
                moq.Setup(m => m.Name).Returns("double_test3");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.String);
                moq.Setup(m => m.Value).Returns("test2_waarde");
            }
            if (i == 3)
            {
                moq.Setup(m => m.Name).Returns("string_test4");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Double);
                moq.Setup(m => m.Value).Returns(125.01);
            }

            return moq.Object;
        }

        private IExecutionResult InitMoqExecutionResultsWithBoolean()
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep(1); //step 1
            var moqParameterCollection = InitMoqBooleanParameterCollection();
            var moqParameters = InitMoqBooleanParameters();
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            moq.Setup(m => m.QuestionParameters).Returns(moqParameters);
            moq.Setup(m => m.QuestionFirstParameter).Returns(moqParameters.First());
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { new FlowExecutionItem(moqCoreStep) });
            return moq.Object;
        }

        private IExecutionResult InitMoqExecutionResultsWithDouble()
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep(2); //step 2
            var moqParameterCollection = InitMoqDoubleParameterCollection();
            var moqParameters = InitMoqDoubleParameters();
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            moq.Setup(m => m.QuestionParameters).Returns(moqParameters);
            moq.Setup(m => m.QuestionFirstParameter).Returns(moqParameters.First());
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { new FlowExecutionItem(moqCoreStep) });
            return moq.Object;
        }

        private IExecutionResult InitMoqExecutionResultsWithString()
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep(3); //step 3
            var moqParameterCollection = InitMoqStringParameterCollection();
            var moqParameters = InitMoqStringParameters();
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            moq.Setup(m => m.QuestionParameters).Returns(moqParameters);
            moq.Setup(m => m.QuestionFirstParameter).Returns(moqParameters.First());
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { new FlowExecutionItem(moqCoreStep) });
            return moq.Object;
        }

        private IEnumerable<IParameter> InitMoqBooleanParameters()
        {
            return new List<IParameter> {
                InitMoqBooleanParameter(1),
                InitMoqBooleanParameter(2)
            };
        }

        private IParametersCollection InitMoqBooleanParameterCollection()
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> {
                InitMoqBooleanParameter(1),
                InitMoqBooleanParameter(2)
            });

            return moq.Object;
        }

        private IEnumerable<IParameter> InitMoqDoubleParameters()
        {
            return new List<IParameter> {
                InitMoqDoubleParameter()
            };
        }

        private IParametersCollection InitMoqDoubleParameterCollection()
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> {
                InitMoqDoubleParameter()
            });

            return moq.Object;
        }

        private IEnumerable<IParameter> InitMoqStringParameters()
        {
            return new List<IParameter> {
                InitMoqStringParameter()
            };
        }

        private IParametersCollection InitMoqStringParameterCollection()
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> {
                InitMoqStringParameter()
            });

            return moq.Object;
        }

        private IParameter InitMoqBooleanParameter(int i)
        {
            var moq = new Mock<IParameter>();
            if (i == 1)
            {
                moq.Setup(m => m.Name).Returns("boolean_test1");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
                moq.Setup(m => m.Value).Returns(true);
            }
            if (i == 2)
            {
                moq.Setup(m => m.Name).Returns("boolean_test2");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
                moq.Setup(m => m.Value).Returns(false);
            }

            return moq.Object;
        }

        private IParameter InitMoqDoubleParameter()
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Name).Returns("double_test3");
            moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Double);
            moq.Setup(m => m.Value).Returns(125.01);
            return moq.Object;
        }

        private IParameter InitMoqStringParameter()
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Name).Returns("string_test4");
            moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.String);
            moq.Setup(m => m.Value).Returns("string_value");
            return moq.Object;
        }

        private IStep InitMoqCoreStep(int i)
        {
            var moq = new Mock<IStep>();
            if (i == 1)
            {
                moq.Setup(m => m.Key).Returns(1);
                moq.Setup(m => m.SemanticKey).Returns("test_key1");
            }
            if (i == 2)
            {
                moq.Setup(m => m.Key).Returns(2);
                moq.Setup(m => m.SemanticKey).Returns("test_key2");
            }
            if (i == 3)
            {
                moq.Setup(m => m.Key).Returns(3);
                moq.Setup(m => m.SemanticKey).Returns("test_key3");
            }

            return moq.Object;
        }
    }
}