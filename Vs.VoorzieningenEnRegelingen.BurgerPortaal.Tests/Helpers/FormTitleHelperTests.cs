using Moq;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Helpers
{
    public class FormTitleHelperTests
    {
        [Fact]
        public void ShouldGetQuestionNumber()
        {
            var moqSequence = InitMoqSequence();
            var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;

            var number = FormTitleHelper.GetQuestionNumber(moqSequence, moqExecutionResultEmpty);
            Assert.Equal(0, number);

            var moqExecutionResult = InitMoqExecutionResult();
            number = FormTitleHelper.GetQuestionNumber(moqSequence, moqExecutionResult);
            Assert.Equal(2, number);
        }

        [Fact]
        public void ShouldGetQuestion()
        {
            var moqExecutionResult = InitMoqExecutionResult();
            var question = FormTitleHelper.GetQuestion(moqExecutionResult);
            Assert.Equal("This is a test question", question);
        }

        [Fact]
        public void ShouldGetQuestionTitle()
        {
            var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
            var questionTitle = FormTitleHelper.GetQuestionTitle(moqExecutionResultEmpty);
            Assert.Equal("Resultaat", questionTitle);

            var moqExecutionResult = InitMoqExecutionResult();
            questionTitle = FormTitleHelper.GetQuestionTitle(moqExecutionResult);
            Assert.Equal("Selecteer uw woonland.", questionTitle);
        }

        [Fact]
        public void ShouldGetQuestionDescription()
        {
            var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
            var questionTitle = FormTitleHelper.GetQuestionDescription(moqExecutionResultEmpty);
            Assert.Equal("Uw zorgtoeslag is berekend. Hieronder staat het bedrag in euro's waar u volgens de berekening maandelijks recht op hebt.<br />" +
                    "Let op: dit is een proefberekening, nadat u uw zorgtoeslag hebt aangevraagd bij de Belastingdienst wordt de definitieve toeslag bekend.", questionTitle);

            var moqExecutionResult = InitMoqExecutionResult();
            questionTitle = FormTitleHelper.GetQuestionDescription(moqExecutionResult);
            Assert.Equal("Indien u niet zeker weet wat uw woonland is, kijk dan op de website van de Belastingdienst.", questionTitle);
        }

        private ISequence InitMoqSequence()
        {
            var moq = new Mock<ISequence>();
            moq.Setup(m => m.Steps).Returns(new List<Objects.IStep> { new Objects.Step(), new Objects.Step() });
            return moq.Object;
        }

        private IExecutionResult InitMoqExecutionResult()
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep();
            var moqParameterCollection = InitMoqParementerCollection();
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection ));
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { null, new FlowExecutionItem(moqCoreStep) });
            return moq.Object;
        }

        private Core.Model.IStep InitMoqCoreStep()
        {
            var moq = new Mock<Core.Model.IStep>();
            moq.Setup(m => m.Description).Returns("This is a test question");
            return moq.Object;
        }

        private IParametersCollection InitMoqParementerCollection()
        {
            var moqParameter = InitMoqParameter();
            var moq = new Mock<IParametersCollection>();
            moq.Setup(m => m.GetEnumerator()).Returns(new List<IParameter> { moqParameter }.GetEnumerator());
            return moq.Object;
        }

        private IParameter InitMoqParameter()
        {
            var moq = new Mock<IParameter>();
            moq.Setup(m => m.Name).Returns("woonland");
            return moq.Object;
        }
    }
}
