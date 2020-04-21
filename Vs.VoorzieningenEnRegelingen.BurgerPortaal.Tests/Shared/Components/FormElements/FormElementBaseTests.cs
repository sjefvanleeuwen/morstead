using Moq;
using Vs.Rules.Core;
using Vs.Rules.Core.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interfaces;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class FormElementBaseTests
    {
        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Testing purposes")]
        public void ValueTest()
        {
            //Vsalue equal to Data.Value
            var sut = new FormElementBase();
            sut.Data = new NumericFormElementData { Value = "test1" };
            Assert.Equal("test1", sut.Data.Value);
            Assert.Equal("test1", sut.Value);
            sut.Data.Value = "test2";
            Assert.Equal("test2", sut.Data.Value);
            Assert.Equal("test2", sut.Value);
            sut.Value = "test3";
            Assert.Equal("test3", sut.Data.Value);
            Assert.Equal("test3", sut.Value);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Testing purposes")]
        public void ShowElementTest()
        {
            //Vsalue equal to Data.Value
            var sut = new FormElementBase();
            Assert.False(sut.ShowElement);
            sut.Data = new NumericFormElementData { Name = null };
            Assert.False(sut.ShowElement);
            sut.Data.Name = string.Empty;
            Assert.False(sut.ShowElement);
            sut.Data.Name = " ";
            Assert.False(sut.ShowElement);
            sut.Data.Name = "_";
            Assert.True(sut.ShowElement);
        }

        [Fact]
        public void GetFormElementOfCorrectType()
        {
            var sut = new FormElementBase();

            var moq = new Mock<IExecutionResult>();
            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.Boolean);
            var formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(Radio), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.Double);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(Number), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.List);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(Select), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.DateTime);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(Date), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.Period);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(DateRange), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.String);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.True(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(Text), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.TimeSpan);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(FormElementBase), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.Unknown);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(FormElementBase), formElement.GetType());
        }

        [Fact]
        public void ShouldNotHaveInput()
        {
            var sut = new FormElementBase();
            Assert.False(sut.HasInput);
        }
    }
}
