using Moq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Interface;
using Vs.VoorzieningenEnRegelingen.Core;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class FormElementBaseTests
    {
        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Testing purposes")]
        public void CascadedDataOverData()
        {
            var sut = new FormElementBase();
            //sets the private data
            sut.Data = new NumericFormElementData { Name = "test1" };
            Assert.Equal(typeof(NumericFormElementData), sut.Data.GetType());
            Assert.Equal("test1", sut.Data.Name);
            sut.Data.Name = "test1_1";
            Assert.Equal("test1_1", sut.Data.Name);
            //castcaded is set so data should return the castcaded one
            sut.CascadedData = new OptionsFormElementData { Name = "test2" };
            Assert.Equal(typeof(OptionsFormElementData), sut.Data.GetType());
            Assert.Equal("test2", sut.Data.Name);
            sut.Data.Name = "test2_1";
            Assert.Equal("test2_1", sut.Data.Name);
            //once cascaded data is set the value of data is completely ifnored
            sut.Data = new NumericFormElementData { Name = "test3" };
            Assert.Equal(typeof(OptionsFormElementData), sut.CascadedData.GetType());
            Assert.Equal("test2_1", sut.CascadedData.Name);
            Assert.Equal("test2_1", sut.Data.Name);
            sut.Data.Name = "test3_1";
            Assert.Equal("test3_1", sut.CascadedData.Name);
            Assert.Equal("test3_1", sut.Data.Name);
        }

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
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(FormElementBase), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.Period);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(FormElementBase), formElement.GetType());

            moq.Setup(m => m.InferedType).Returns(TypeInference.InferenceResult.TypeEnum.String);
            formElement = sut.GetFormElement(moq.Object);
            Assert.True(formElement is IFormElementBase);
            Assert.False(formElement.GetType().IsSubclassOf(typeof(FormElementBase)));
            Assert.Equal(typeof(FormElementBase), formElement.GetType());

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
    }
}
