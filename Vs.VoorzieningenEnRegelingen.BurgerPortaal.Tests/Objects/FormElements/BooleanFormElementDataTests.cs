using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class BooleanFormElementDataTests
    {
        //todo activate after texts have been restored
        //[Fact]
        public void ShouldDefineOptions()
        {
            var moqExecutionResult = InitMoqExecutionResult(1);

            var sut = new BooleanFormElementData();
            //sut.DefineOptions(moqExecutionResult);
            Assert.Equal(2, sut.Options.Count);
            Assert.Equal("test1", sut.Options.ToList()[0].Key);
            Assert.Equal("Test1", sut.Options.ToList()[0].Value);
            Assert.Equal("test2", sut.Options.ToList()[1].Key);
            Assert.Equal("Test2", sut.Options.ToList()[1].Value);
        }

        private IExecutionResult InitMoqExecutionResult(int type)
        {
            var moq = new Mock<IExecutionResult>();
            var moqParameterCollection = InitMoqParementerCollection(type);
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            return moq.Object;
        }

        private IParametersCollection InitMoqParementerCollection(int type)
        {
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { InitMoqParameter(1), InitMoqParameter(2) });
            return moq.Object;
        }

        private IParameter InitMoqParameter(int type)
        {
            var moq = new Mock<IParameter>();
            if (type == 1)
            {
                moq.Setup(m => m.Name).Returns("test1");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
            }
            if (type == 2)
            {
                moq.Setup(m => m.Name).Returns("test2");
                moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
            }
            return moq.Object;
        }
    }
}
