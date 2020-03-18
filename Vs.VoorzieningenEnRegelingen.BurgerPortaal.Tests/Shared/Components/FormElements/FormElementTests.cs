using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class FormElementTests
    {
        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "We want to assign values here for testing")]
        public void CheckValidEmpty()
        {
            var sut = new FormElement();
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "We want to assign values here for testing")]
        public void CheckValidFilled()
        {
            var sut = new FormElement();
            sut.InferedType = Core.TypeInference.InferenceResult.TypeEnum.String;
            sut.Value = "test";
            Assert.True(sut.Validate());
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "We want to assign values here for testing")]
        public void CheckValidFilledDouble()
        {
            var sut = new FormElement();
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
            var sut = new FormElement();
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
