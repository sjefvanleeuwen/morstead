using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class BooleanFormElementDataTests
    {
        [Fact]
        public void ShouldDefineOptions()
        {
            var moqExecutionResult = InitMoqExecutionResult(1);
            var moqContentController = new Mock<IContentController>();
            moqContentController.Setup(m => m.GetText("test1", FormElementContentType.Description, "test1")).Returns("Test1");
            moqContentController.Setup(m => m.GetText("test2", FormElementContentType.Description, "test2")).Returns("Test2");

            var sut = new BooleanFormElementData();
            sut.DefineOptions(moqExecutionResult, moqContentController.Object);
            Assert.Equal(2, sut.Options.Count);
            Assert.Equal("test1", sut.Options.ToList()[0].Key);
            Assert.Equal("Test1", sut.Options.ToList()[0].Value);
            Assert.Equal("test2", sut.Options.ToList()[1].Key);
            Assert.Equal("Test2", sut.Options.ToList()[1].Value);
        }

        private IExecutionResult InitMoqExecutionResult(int type)
        {
            var moq = new Mock<IExecutionResult>();
            moq.Setup(m => m.QuestionParameters).Returns(new List<IParameter> { InitMoqParameter(1), InitMoqParameter(2) });
            moq.Setup(m => m.GetParameterSemanticKey("test1")).Returns("test1");
            moq.Setup(m => m.GetParameterSemanticKey("test2")).Returns("test2");
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
