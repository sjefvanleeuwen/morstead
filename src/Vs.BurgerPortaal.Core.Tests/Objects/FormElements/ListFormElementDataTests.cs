using Moq;
using System.Collections.Generic;
using System.Linq;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.Cms.Core.Controllers.Interfaces;
using Vs.Core.Enums;
using Vs.Rules.Core.Interfaces;
using Vs.Rules.Core.Model;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Objects.FormElements
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
            Assert.Equal("Optie1", sut.Options.ToList()[0].Value);
            Assert.Equal("optie2", sut.Options.ToList()[1].Key);
            Assert.Equal("Optie2", sut.Options.ToList()[1].Value);
            Assert.Equal("optie3", sut.Options.ToList()[2].Key);
            Assert.Equal("Optie3", sut.Options.ToList()[2].Value);
        }

        private Mock<IExecutionResult> InitMoqExecutionResult()
        {
            var moq = new Mock<IExecutionResult>();
            var moqParameter = new Mock<IParameter>();
            moqParameter.Setup(m => m.Value).Returns(new List<string> { "optie1", "optie2", "optie3" });
            moq.Setup(m => m.QuestionFirstParameter).Returns(moqParameter.Object);
            moq.Setup(m => m.Step.SemanticKey).Returns("semKey");
            return moq;
        }
        private Mock<IContentController> InitMoqContentController()
        {
            var moq = new Mock<IContentController>();
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Description, "Optie1")).Returns("TestOpt1");
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Description, "Optie2")).Returns("TestOpt2");
            moq.Setup(m => m.GetText("semKey", FormElementContentType.Description, "Optie3")).Returns("Optie3");
            return moq;
        }
    }
}
