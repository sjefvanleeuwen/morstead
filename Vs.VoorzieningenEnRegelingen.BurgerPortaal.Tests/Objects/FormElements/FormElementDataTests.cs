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
    }
}
