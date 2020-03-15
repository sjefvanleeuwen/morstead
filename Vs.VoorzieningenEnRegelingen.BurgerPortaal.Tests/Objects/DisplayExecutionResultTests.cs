using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;
using IStep = Vs.VoorzieningenEnRegelingen.Core.Model.IStep;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects
{
    public class DisplayExecutionResultTests
    {
        [Fact]
        public void ShouldInitiateCorrectly() {
            var moqExecutionResult = InitMoqExecutionResult();
            var moqParameterCollection = InitMoqParementerCollection(2);
            var sut = new DisplayExecutionResult(moqExecutionResult, moqParameterCollection);

            Assert.True(sut.IsError);
            Assert.Equal("This is a test message", sut.Message);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.List, sut.Parameters.GetAll().First().Type);
            Assert.Equal("optie2", ((List<object>)sut.Parameters.GetAll().First().Value).Last());
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, sut.Questions.Parameters.GetAll().First().Type);
            Assert.Equal("This is a test question", sut.Stacktrace.First().Step.Description);
        }

        private IExecutionResult InitMoqExecutionResult()
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep();
            var moqParameterCollection = InitMoqParementerCollection(1);
            moq.Setup(m => m.IsError).Returns(true);
            moq.Setup(m => m.Message).Returns("This is a test message");
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { new FlowExecutionItem(moqCoreStep) });
            return moq.Object;
        }

        private IStep InitMoqCoreStep()
        {
            var moq = new Mock<IStep>();
            moq.Setup(m => m.Description).Returns("This is a test question");
            return moq.Object;
        }

        private IParametersCollection InitMoqParementerCollection(int type)
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { InitMoqParameter(type) });
            return moq.Object;
        }

        private IParameter InitMoqParameter(int type)
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Name).Returns("woonland");
            if (type == 1)
            {
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
            }
            if (type == 2)
            {
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.List);
                moq.Setup(m => m.Value).Returns(new List<object> { "optie1", "optie2" });
            }
            return moq.Object;
        }
    }
}
