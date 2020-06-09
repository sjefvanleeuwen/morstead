using Vs.BurgerPortaal.Core.Objects.FormElements;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Objects.FormElements
{
    public class NumericFormElementDataTests
    {
        [Fact]
        public void CheckValidEmptyFromParent()
        {
            var sut = new NumericFormElementData();
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
        public void CheckValidFilledDouble()
        {
            var sut = new NumericFormElementData();
            sut.Value = "123";
            sut.CustomValidate();
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
            sut.Value = "123,45";
            sut.CustomValidate();
            Assert.True(sut.IsValid);
            Assert.Empty(sut.ErrorText);
            sut.Value = "123.45";
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
            sut.Value = "123,4";
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Typ twee cijfers achter de komma.", sut.ErrorText);
            sut.Value = "123,4,4";
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
        }
    }
}
