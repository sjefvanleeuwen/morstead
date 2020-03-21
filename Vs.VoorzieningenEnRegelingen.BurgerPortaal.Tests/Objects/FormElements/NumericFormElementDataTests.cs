using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class NumericFormElementDataTests
    {
        [Fact]
        public void CheckValidEmptyFromParent()
        {
            var sut = new NumericFormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
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
            var sut = new NumericFormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.Double;
            sut.Value = "123";
            sut.Validate();
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
            sut.Value = "123,45";
            sut.Validate();
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
            sut.Value = "123.45";
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
            sut.Value = "123,4";
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Typ twee cijfers achter de komma.", sut.ErrorText);
            sut.Value = "123,4,4";
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
        }
    }
}
