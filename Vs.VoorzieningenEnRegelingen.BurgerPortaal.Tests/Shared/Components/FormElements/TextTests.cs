using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests._Helper.Extensions;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class TextTests : BlazorTestBase
    {
        [Fact]
        public void TextEmpty()
        {
            var data = new TextFormElementData() as IFormElementData;
            var cut = RenderComponent<Text>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("input");
            var labels = cut.FindAll("label");
            var hints = cut.FindAll(".mdc-text-field-helper-line");
            var errors = cut.FindAll("div.validation-message");
            Assert.Single(inputs);
            var input = inputs[0];
            Assert.NotNull(input);
            Assert.Null(input.Id);
            Assert.Empty(input.Attr("value"));
            Assert.Null(input.Attr("aria-label"));
            Assert.False(input.IsDisabled());
            Assert.Empty(labels);
            Assert.Empty(hints);
            Assert.Empty(errors);
        }

        [Fact]
        public void TextFilled()
        {
            var data = new TextFormElementData
            {
                IsDisabled = true,
                Name = "TheName",
                Value = "TheValue",
                Label = "TheLabel",
                HintText = "TheHint"
            } as IFormElementData;
            var cut = RenderComponent<Text>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("input");
            var labels = cut.FindAll("label");
            var hints = cut.FindAll(".mdc-text-field-helper-line > div.mdc-text-field-helper-text");
            Assert.Single(inputs);
            var input = inputs[0];
            Assert.NotNull(input);
            Assert.Equal("TheName", input.Id);
            Assert.Equal("TheValue", input.Attr("value"));
            Assert.NotNull(input.Attr("aria-label"));
            Assert.True(input.IsDisabled());
            Assert.Single(labels);
            Assert.Equal("TheLabel", labels[0].InnerHtml);
            Assert.Single(hints);
            Assert.Equal("TheHint", hints[0].InnerHtml);
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var data = new TextFormElementData
            {
                Value = "TheValue",
            } as IFormElementData;
            var cut = RenderComponent<Text>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var input = cut.Find("input");
            Assert.Equal("TheValue", cut.Instance.Data.Value);
            Assert.Equal("TheValue", input.Attr("value"));
            input.Change("TheNewValue");
            Assert.Equal("TheNewValue", cut.Instance.Data.Value);
            input = cut.Find("input");
            Assert.Equal("TheNewValue", input.Attr("value"));
        }

        [Fact]
        public void HasCorrectParts()
        {
            var data = new TextFormElementData
            {
                Label = "_",
                HintText = "_",
                Value = ""
            } as IFormElementData;
            data.CustomValidate();
            Validator.TryValidateObject(data, new ValidationContext(data), null);
            var cut = RenderComponent<Text>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.NotEmpty(data.ErrorText);
            Assert.False(data.IsValid);

            var div = cut.Find("div");
            Assert.NotNull(div);
            var input = cut.Find("div > input");
            Assert.NotNull(input);
            var label = cut.Find("div > label");
            Assert.NotNull(label);
            Assert.Equal("mdc-floating-label", label.ClassName);
            var hint = cut.Find("div:nth-of-type(2) > div");
            Assert.NotNull(hint);
            Assert.Equal("mdc-text-field-helper-text", hint.ClassName);
            var ps = cut.FindAll("p");
            Assert.NotNull(ps);
            Assert.Single(ps);
            Assert.Equal("mdc-text-field-helper-text mdc-text-field-helper-text--persistent", ps.First().ClassName);
            //var errorContent = cut.Find("p > div");
            //Assert.NotNull(errorContent);
            //Assert.Equal("validation-message", hint.ClassName);

            //check order
            var top = cut.Find("div");
            Assert.Equal("input", top.FirstChild().NodeName.ToLower());
            Assert.Equal("label", top.FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().NodeName.ToLower());
            Assert.Equal("mdc-text-field-helper-line", top.NextElement().ClassName);
            Assert.Equal("div", top.NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("mdc-text-field-helper-text", top.NextElement().FirstChild().ClassName);
            Assert.Equal("p", top.NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("mdc-text-field-helper-text mdc-text-field-helper-text--persistent", top.NextElement().NextElement().ClassName);
            //Assert.Equal("div", div.NextElement().NextElement().FirstChild().NodeName);
            //Assert.Equal("validation-message", div.NextElement().NextElement().FirstChild().ClassName);
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new Text();
            Assert.True(sut.HasInput);
        }
    }
}