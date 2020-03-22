using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class FormElementDataTests
    {
        [Fact]
        public void CheckValidEmpty()
        {
            var sut = new FormElementData();
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = string.Empty;
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = " ";
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = "\t";
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidFilled()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.Value = "test";
            sut.Validate();
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void CheckValidUnobtrusive()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.NotEmpty(sut.ErrorText);
            sut.Validate(true);
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
        }
    }
}
