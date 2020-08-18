using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.CitizenPortal.Logic.Controllers;
using Vs.CitizenPortal.Logic.Objects.Interfaces;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.Logic.Controllers.Interfaces;
using Xunit;

namespace Vs.CitizenPortal.Logic.Tests.Controllers
{
    public class SequenceControllerTests
    {
        [Fact]
        public void ShouldInit()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);

            Assert.Equal(0, sut.CurrentStep);
            Assert.Equal(0, sut.RequestStep);
        }

        [Fact]
        public void ShouldGetExecuteRequest()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var result = sut.GetExecuteRequest();
            Assert.Equal("YamlFileUrl", result.Config);
            Assert.Null(result.Parameters);

            result = sut.GetExecuteRequest(GetDummyParameterCollection());
            Assert.Single(result.Parameters.GetAll());
            Assert.Equal("TestParam", result.Parameters.GetAll().ElementAt(0).Name);
        }

        [Fact]
        public void ShouldGetGetParseResult()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var result = sut.GetParseResult();

            moqServiceController.Verify(m => m.Parse(It.IsAny<IParseRequest>()), Times.Once());
        }

        [Fact]
        public void ShouldGetGetParseRequest()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var result = sut.GetParseRequest();

            Assert.Equal("YamlFileUrl", result.Config);
        }

        [Fact]
        public void FirstStepShouldAlwaysResultInOne()
        {
            ShouldIncreaseAndDecreraseSteps();
            ShouldDecreraseAndIncreaseSteps();
        }

        [Fact]
        public void ShouldIncreaseAndDecreraseSteps()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            sut.IncreaseStep();
            Assert.Equal(1, sut.RequestStep);//first step, it should always be 1 after
            sut.IncreaseStep();
            Assert.Equal(2, sut.RequestStep);
            sut.DecreaseStep();
            Assert.Equal(1, sut.RequestStep);
            sut.DecreaseStep();
            Assert.Equal(1, sut.RequestStep);//decrease but ca't be lower than 1
        }

        [Fact]
        public void ShouldDecreraseAndIncreaseSteps()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            sut.DecreaseStep();
            Assert.Equal(1, sut.RequestStep);//decrease but ca't be lower than 1
            sut.IncreaseStep();
            Assert.Equal(2, sut.RequestStep);
        }

        [Fact]
        public async void ShouldExecuteStep()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var moqParametersCollection = It.IsAny<IParametersCollection>();

            sut.IncreaseStep();
            await sut.ExecuteStep(moqParametersCollection);

            moqSequence.Verify(m => m.GetParametersToSend(1), Times.Once());
            moqSequence.Verify(m => m.UpdateParametersCollection(It.IsAny<IParametersCollection>()), Times.Once());
            moqSequence.Verify(m => m.AddStep(1, It.IsAny<IExecutionResult>()), Times.Once());
            moqSequence.Verify(m => m.UpdateParametersCollection(It.IsAny<IParametersCollection>()), Times.Once());
            Assert.Equal(1, sut.CurrentStep);
        }

        [Fact]
        private async void ShouldNotGetSavedValueNoExecutionResult()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var result = sut.GetSavedValue();
            Assert.Null(result);
        }

        [Fact]
        private async void ShouldNotGetSavedValueNoQuestion()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.Null(result);
        }

        [Fact]
        private async void ShouldNotGetSavedValueNoStep()
        {
            var moqServiceController = InitMoqServiceController(true);
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.Null(result);
        }

        [Fact]
        private async void ShouldNotGetSavedValueNoStepMatch()
        {
            var moqServiceController = InitMoqServiceController(true);
            var moqSequence = InitMoqSequence(true);
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.Null(result);
        }

        [Fact]
        private async void ShouldGetSavedValueString()
        {
            var moqServiceController = InitMoqServiceController(true);
            var moqSequence = InitMoqSequence(true, true,
                new List<IClientParameter> {
                    new ClientParameter("Name", "TestValue", TypeInference.InferenceResult.TypeEnum.String, "Dummy")
                });
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.NotNull(result);
            Assert.Equal("TestValue", result);
        }

        [Fact]
        private async void ShouldGetSavedValueBoolean()
        {
            var moqServiceController = InitMoqServiceController(true, TypeInference.InferenceResult.TypeEnum.Boolean);
            var moqSequence = InitMoqSequence(true, true,
                new List<IClientParameter> {
                    new ClientParameter("Name1", true, TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy"),
                    new ClientParameter("Name2", false, TypeInference.InferenceResult.TypeEnum.Boolean, "Dummy")
                });
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.NotNull(result);
            Assert.Equal("Name1", result);
        }

        [Fact]
        private async void ShouldGetSavedValueNumber()
        {
            var moqServiceController = InitMoqServiceController(true, TypeInference.InferenceResult.TypeEnum.Double);
            var moqSequence = InitMoqSequence(true, true,
                new List<IClientParameter> {
                    new ClientParameter("TheName", "1234.23", TypeInference.InferenceResult.TypeEnum.Double, "Dummy"),
                });
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            await sut.ExecuteStep(It.IsAny<IParametersCollection>());
            var result = sut.GetSavedValue();
            Assert.NotNull(result);
            Assert.Equal("1234,23", result);
        }

        private IParametersCollection GetDummyParameterCollection()
        {
            var moqParam = new Mock<IParameter>();
            moqParam.Setup(m => m.Name).Returns("TestParam");
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { moqParam.Object });
            return moq.Object;
        }

        private Mock<ISequence> InitMoqSequence(bool shouldHaveSteps = false, bool stepShouldMatch = false, IEnumerable<IParameter> returnCollection = null)
        {
            var moq = new Mock<ISequence>();
            moq.Setup(m => m.Yaml).Returns("YamlFileUrl");
            if (shouldHaveSteps)
            {
                var moqStep = InitMoqStep(stepShouldMatch);
                moq.Setup(m => m.Steps).Returns(new List<ISequenceStep> { moqStep.Object });
            }
            var moqParameterCollection = new Mock<IParametersCollection>();
            moqParameterCollection.Setup(m => m.GetAll()).Returns(returnCollection);
            moq.Setup(m => m.Parameters).Returns(moqParameterCollection.Object);
            return moq;
        }

        private Mock<ISequenceStep> InitMoqStep(bool stepShouldMatch)
        {
            var moq = new Mock<ISequenceStep>();
            moq.Setup(m => m.IsMatch(It.IsAny<IParameter>())).Returns(stepShouldMatch);
            return moq;
        }

        private Mock<IServiceController> InitMoqServiceController(bool hasQuestion = false,
            TypeInference.InferenceResult.TypeEnum inferedType = TypeInference.InferenceResult.TypeEnum.String)
        {
            var moq = new Mock<IServiceController>();
            var moqExecutionResult = InitMoqExecutionResult(hasQuestion, inferedType);
            moq.Setup(m => m.Execute(It.IsAny<IExecuteRequest>())).ReturnsAsync(moqExecutionResult.Object);
            moq.Setup(m => m.Parse(It.IsAny<IParseRequest>())).ReturnsAsync(It.IsAny<IParseResult>());
            return moq;
        }

        private Mock<IExecutionResult> InitMoqExecutionResult(bool hasQuestion, TypeInference.InferenceResult.TypeEnum inferedType)
        {
            var moq = new Mock<IExecutionResult>();
            var moqParameterCollection = new Mock<IParametersCollection>();
            if (hasQuestion)
            {
                var moqQuestionArgs = new Mock<IQuestionArgs>();
                moq.Setup(m => m.Questions).Returns(moqQuestionArgs.Object);
                moq.Setup(m => m.InferedType).Returns(inferedType);
            }
            return moq;
        }
    }
}
