using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Vs.VoorzieningenEnRegelingen.Service.Controllers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Controllers
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
        public void ShouldExecuteStep()
        {
            var moqServiceController = InitMoqServiceController();
            var moqSequence = InitMoqSequence();
            var sut = new SequenceController(moqServiceController.Object, moqSequence.Object);
            var moqParametersCollection = It.IsAny<IParametersCollection>();

            sut.IncreaseStep();
            sut.ExecuteStep(moqParametersCollection);
           
            moqSequence.Verify(m => m.GetParametersToSend(1), Times.Once());
            moqSequence.Verify(m => m.UpdateParametersCollection(It.IsAny<IParametersCollection>()), Times.Once());
            moqSequence.Verify(m => m.AddStep(1, It.IsAny<IExecutionResult>()), Times.Once());
            moqSequence.Verify(m => m.UpdateParametersCollection(It.IsAny<IParametersCollection>()), Times.Once());
            Assert.Equal(1, sut.CurrentStep);
        }

        private IParametersCollection GetDummyParameterCollection()
        {
            var moqParam = new Mock<IParameter>();
            moqParam.Setup(m => m.Name).Returns("TestParam");
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { moqParam.Object });
            return moq.Object;
        }

        private Mock<ISequence> InitMoqSequence()
        {
            var moq = new Mock<ISequence>();
            moq.Setup(m => m.Yaml).Returns("YamlFileUrl");
            return moq;
        }

        private Mock<IServiceController> InitMoqServiceController()
        {
            var moq = new Mock<IServiceController>();
            var moqExecutionResult = InitExecutionResult();
            moq.Setup(m => m.Execute(It.IsAny<IExecuteRequest>())).Returns(moqExecutionResult.Object);
            moq.Setup(m => m.Parse(It.IsAny<IParseRequest>())).Returns(It.IsAny<IParseResult>());
            return moq;
        }

        private Mock<IExecutionResult> InitExecutionResult()
        {
            var moq = new Mock<IExecutionResult>();
            moq.Setup(m => m.Parameters).Returns(It.IsAny<IParametersCollection>());
            return moq;
        }
    }
}
