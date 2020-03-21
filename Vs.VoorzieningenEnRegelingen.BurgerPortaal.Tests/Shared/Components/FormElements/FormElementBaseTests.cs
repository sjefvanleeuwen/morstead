using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class FormElementBaseTests
    {
        [Fact]
        public void CheckValidEmpty()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            Assert.False(sut.Validate());
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = string.Empty;
            Assert.False(sut.Validate());
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = " ";
            Assert.False(sut.Validate());
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
            sut.Value = "\t";
            Assert.False(sut.Validate());
            Assert.Equal("Vul een waarde in.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidFilled()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.Value = "test";
            Assert.True(sut.Validate());
        }

        [Fact]
        public void CheckValidFilledDouble()
        {
            var sut = new NumericFormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.Double;
            sut.Value = "123";
            Assert.True(sut.Validate());
            Assert.Empty(sut.ErrorText);
            sut.Value = "123,45";
            Assert.True(sut.Validate());
            Assert.Empty(sut.ErrorText);
            sut.Value = "123.45";
            Assert.False(sut.Validate());
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
            sut.Value = "123,4";
            Assert.False(sut.Validate());
            Assert.Equal("Typ twee cijfers achter de komma.", sut.ErrorText);
            sut.Value = "123,4,4";
            Assert.False(sut.Validate());
            Assert.Equal("Er zijn ongeldige tekens ingegeven. Een getal bestaat uit nummers en maximaal één komma met daarachter twee cijfers.", sut.ErrorText);
        }

        [Fact]
        public void CheckValidUnobtrusive()
        {
            var sut = new FormElementData();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            Assert.False(sut.Validate());
            Assert.NotEmpty(sut.ErrorText);
            Assert.False(sut.IsValid);
            Assert.False(sut.Validate(true));
            Assert.Null(sut.ErrorText);
            Assert.True(sut.IsValid);
        }
    }
}
