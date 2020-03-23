using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class ListFormElementDataTests
    {
        //todo MPS activate after texts have been restored
        //[Fact]
        public void ShouldDefineOptions()
        {
            var moqExecutionResult = InitMoqExecutionResult(1);

            var sut = new ListFormElementData();
            sut.DefineOptions(moqExecutionResult);
            Assert.Equal(2, sut.Options.Count);
            Assert.Equal("optie1", sut.Options.ToList()[0].Key);
            Assert.Equal("optie1", sut.Options.ToList()[0].Value);
            Assert.Equal("optie2", sut.Options.ToList()[1].Key);
            Assert.Equal("optie2", sut.Options.ToList()[1].Value);
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
            moq.Setup(m => m.GetAll()).Returns(new List<IParameter> { InitMoqParameter(type) });
            return moq.Object;
        }

        private IParameter InitMoqParameter(int type)
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Type).Returns(TypeInference.InferenceResult.TypeEnum.List);
            moq.Setup(m => m.Value).Returns(new List<object> { "optie1", "optie2" });
            return moq.Object;
        }
    }
}
