using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class TextFormElementDataTests
    {
        [Fact]
        public void CheckValidEmptyFromParent()
        {
            var sut = new TextFormElementData();
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
        public void CheckValidFilledDouble()
        {
            var sut = new TextFormElementData();
            sut.Value = "test";
            sut.Validate();
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
        }
    }
}
