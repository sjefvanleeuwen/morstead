using Moq;
using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Helpers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.Core;
using Vs.VoorzieningenEnRegelingen.Core.Model;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Helpers
{

    public class FormElementHelperTests
    {
        [Fact]
        public void ShouldParseExecutionResult()
        {
            var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResultEmpty);
            Assert.Null(formElement.Name);
            
            var moqExecutionResult = InitMoqExecutionResult(1);
            formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult);
            Assert.Equal("woonland", formElement.Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.Boolean, formElement.InferedType);
            Assert.Equal(string.Empty, formElement.Label);
            Assert.NotNull(formElement.Options["woonland"]);
            Assert.Equal("Woonland", formElement.Options["woonland"]);
            Assert.Equal(string.Empty, formElement.TagText);
            Assert.Equal("Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.", formElement.HintText);
        }

        [Fact]
        public void ShouldParseExecutionResultList()
        {
            var moqExecutionResultEmpty = new Mock<IExecutionResult>().Object;
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResultEmpty);
            Assert.Null(formElement.Name);

            var moqExecutionResult = InitMoqExecutionResult(2);
            formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult);
            Assert.Equal("woonland", formElement.Name);
            Assert.Equal(TypeInference.InferenceResult.TypeEnum.List, formElement.InferedType);
            Assert.Equal(string.Empty, formElement.Label);
            Assert.NotNull(formElement.Options["optie1"]);
            Assert.Equal("optie1", formElement.Options["optie1"]);
            Assert.NotNull(formElement.Options["optie2"]);
            Assert.Equal("optie2", formElement.Options["optie2"]);
            Assert.Equal(string.Empty, formElement.TagText);
            Assert.Equal("Selecteer \"Anders\" wanneer het uw woonland niet in de lijst staat.", formElement.HintText);
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeBoolean()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.Boolean,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is Radio);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeDouble()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.Double,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is Number);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeList()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.List,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is Select);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeTimeSpan()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.TimeSpan,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is FormElement);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeDateTime()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.DateTime,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is FormElement);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypeString()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.String,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is FormElement);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        [Fact]
        public void ShouldGetCorrectFormElementTypePeriod()
        {
            var ParameterList = new List<IParameter> { new ClientParameter()
                {
                    Type = TypeInference.InferenceResult.TypeEnum.Period,
                    Name = "test"
                }
            } as IEnumerable<IParameter>;
            var moqParameters = new Mock<IParametersCollection>();
            moqParameters.Setup(m => m.GetAll()).Returns(ParameterList);
            var moqQuestion = new Mock<IQuestionArgs>();
            moqQuestion.Setup(m => m.Parameters).Returns(moqParameters.Object);
            var moqExecutionResult = new Mock<IExecutionResult>();
            moqExecutionResult.Setup(m => m.Questions).Returns(moqQuestion.Object);
            var formElement = FormElementHelper.ParseExecutionResult(moqExecutionResult.Object);
            Assert.True(formElement is FormElement);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElement)));
        }

        private IExecutionResult InitMoqExecutionResult(int type)
        {
            var moq = new Mock<IExecutionResult>();
            var moqCoreStep = InitMoqCoreStep();
            var moqParameterCollection = InitMoqParementerCollection(type);
            moq.Setup(m => m.Questions).Returns(new QuestionArgs(string.Empty, moqParameterCollection));
            moq.Setup(m => m.Stacktrace).Returns(new List<FlowExecutionItem> { null, new FlowExecutionItem(moqCoreStep) });
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
            //moq.Setup(m => m.GetEnumerator()).Returns(new List<IParameter> { InitMoqParameter(type) }.GetEnumerator());
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
