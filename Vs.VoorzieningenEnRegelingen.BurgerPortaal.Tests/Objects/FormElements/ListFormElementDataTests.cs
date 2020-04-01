using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Cms.Core.Enums;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class ListFormElementDataTests
    {
        [Fact]
        public void ShouldDefineOptions()
        {
            var moqExecutionResult = InitMoqExecutionResult();
            var moqContentController = InitMoqContentController();

            var sut = new ListFormElementData();
            sut.DefineOptions(moqExecutionResult.Object, moqContentController.Object);

            Assert.Equal(3, sut.Options.Count);
            Assert.Equal("optie1", sut.Options.ToList()[0].Key);
            Assert.Equal("TestOpt1", sut.Options.ToList()[0].Value);
            Assert.Equal("optie2", sut.Options.ToList()[1].Key);
            Assert.Equal("TestOpt2", sut.Options.ToList()[1].Value);
            Assert.Equal("optie3", sut.Options.ToList()[2].Key);
            Assert.Equal("Optie3", sut.Options.ToList()[2].Value);
        }

        private Mock<IExecutionResult> InitMoqExecutionResult()
        {
            var moq = new Mock<IExecutionResult>();
            var moqParameter = new Mock<IParameter>();
            moqParameter.Setup(m => m.Value).Returns(new List<string> { "optie1", "optie2", "optie3" });
            moq.Setup(m => m.QuestionFirstParameter).Returns(moqParameter.Object);
            moq.Setup(m => m.SemanticKey).Returns("semKey");
            return moq;
        }
        private Mock<IContentController> InitMoqContentController()
        {
            var moq = new Mock<IContentController>();
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Option, null, "Optie1")).Returns("TestOpt1");
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Option, null, "Optie2")).Returns("TestOpt2");
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Option, null, "Optie3")).Returns("Optie3");
            return moq;
        }
    }
}
