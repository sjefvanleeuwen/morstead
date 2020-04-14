using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class FormElementMultipleValueTests
    {
        [Fact]
        public void CheckValidEmpty()
        {
            var sut = new FormElementMultipleValueData();
            sut.CustomValidate();
            Assert.True(sut.IsValid);//no values to check
            Assert.Empty(sut.ErrorText);
            sut.Values = new Dictionary<string, string> { { "test", string.Empty } };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in voor alle waarden.", sut.ErrorText);
            sut.Values = new Dictionary<string, string> { { "test", " " } };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in voor alle waarden.", sut.ErrorText);
            sut.Values = new Dictionary<string, string> { { "test", "\t" } };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Vul een waarde in voor alle waarden.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidFilled()
        {
            var sut = new FormElementMultipleValueData();
            sut.Values = new Dictionary<string, string> { { "test", "test" } };
            sut.CustomValidate();
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void CheckValidUnobtrusive()
        {
            var sut = new FormElementMultipleValueData();
            sut.Values = new Dictionary<string, string> { { "test", string.Empty } };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.NotEmpty(sut.ErrorText);
            sut.CustomValidate(true);
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
        }
    }
}
