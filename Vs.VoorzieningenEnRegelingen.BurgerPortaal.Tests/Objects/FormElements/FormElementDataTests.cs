using System;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class FormElementDataTests
    {
        [Fact]
        public void ShouldGetErrorText()
        {
            var sut = new FormElementData();
            Assert.Empty(sut.ErrorText);
            sut.ErrorTexts.Add("_");
            Assert.Empty(sut.ErrorText);
            sut.IsValid = false;
            Assert.Equal("_", sut.ErrorText);
            sut.ErrorTexts.Add("_");
            Assert.Equal($"_{Environment.NewLine}_", sut.ErrorText);
        }

        [Fact]
        public void CheckValidEmpty()
        {
            var sut = new FormElementData();
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = string.Empty;
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = " ";
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = "\t";
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidFilled()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.Value = "test";
            sut.CustomValidate();
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void CheckValidUnobtrusive()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.NotEmpty(sut.ErrorText);
            sut.CustomValidate(true);
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
        }
    }
}
