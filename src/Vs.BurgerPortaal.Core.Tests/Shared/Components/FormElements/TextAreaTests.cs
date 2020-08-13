using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements;
using Vs.CitizenPortal.DataModel.Model.FormElements;
using Vs.CitizenPortal.DataModel.Model.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Tests._Helper.Extensions;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components.FormElements
{
    public class TextAreaTests : BlazorTestBase
    {
        //[Fact]
        //public void TextEmpty()
        //{
        //    var data = new TextFormElementData() as IFormElementData;
        //    var cut = RenderComponent<TextArea>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var textareas = cut.FindAll("textarea");
        //    var labels = cut.FindAll("label");
        //    var hints = cut.FindAll(".mdc-text-field-helper-line");
        //    var errors = cut.FindAll("div.validation-message");
        //    Assert.Single(textareas);
        //    var textarea = textareas[0];
        //    Assert.NotNull(textarea);
        //    Assert.Null(textarea.Id);
        //    Assert.Empty(textarea.Attr("value"));
        //    Assert.Null(textarea.Attr("aria-label"));
        //    Assert.False(textarea.IsDisabled());
        //    Assert.Empty(labels);
        //    Assert.Empty(hints);
        //    Assert.Empty(errors);
        //}

        //[Fact]
        //public void TextFilled()
        //{
        //    var data = new TextFormElementData
        //    {
        //        IsDisabled = true,
        //        Name = "TheName",
        //        Value = "TheValue",
        //        Label = "TheLabel",
        //        HintText = "TheHint"
        //    } as IFormElementData;
        //    var cut = RenderComponent<TextArea>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var textareas = cut.FindAll("textarea");
        //    var labels = cut.FindAll("label");
        //    var hints = cut.FindAll(".mdc-text-field-helper-line > div.mdc-text-field-helper-text");
        //    Assert.Single(textareas);
        //    var textarea = textareas[0];
        //    Assert.NotNull(textarea);
        //    Assert.Equal("TheName", textarea.Id);
        //    Assert.Equal("TheValue", textarea.Attr("value"));
        //    Assert.NotNull(textarea.Attr("aria-label"));
        //    Assert.True(textarea.IsDisabled());
        //    Assert.Single(labels);
        //    Assert.Equal("TheLabel", labels[0].InnerHtml);
        //    Assert.Single(hints);
        //    Assert.Equal("TheHint", hints[0].InnerHtml);
        //}

        //[Fact]
        //public void ShouldDoTwoWayBinding()
        //{
        //    var data = new TextFormElementData
        //    {
        //        Value = "TheValue",
        //    } as IFormElementData;
        //    var cut = RenderComponent<TextArea>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var textarea = cut.Find("textarea");
        //    Assert.Equal("TheValue", cut.Instance.Data.Value);
        //    Assert.Equal("TheValue", textarea.Attr("value"));
        //    textarea.Change("TheNewValue");
        //    Assert.Equal("TheNewValue", cut.Instance.Data.Value);
        //    textarea = cut.Find("textarea");
        //    Assert.Equal("TheNewValue", textarea.Attr("value"));
        //}

        //[Fact]
        //public void HasCorrectParts()
        //{
        //    var data = new TextFormElementData
        //    {
        //        Label = "_",
        //        HintText = "_",
        //        Value = ""
        //    } as IFormElementData;
        //    data.CustomValidate();
        //    Validator.TryValidateObject(data, new ValidationContext(data), null);
        //    var cut = RenderComponent<TextArea>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    Assert.NotEmpty(data.ErrorText);
        //    Assert.False(data.IsValid);

        //    var div = cut.Find("div");
        //    Assert.NotNull(div);
        //    var textarea = cut.Find("div > textarea");
        //    Assert.NotNull(textarea);
        //    var label = cut.Find("div > label");
        //    Assert.NotNull(label);
        //    Assert.Equal("mdc-floating-label", label.ClassName);
        //    var hint = cut.Find("div:nth-of-type(2) > div");
        //    Assert.NotNull(hint);
        //    Assert.Equal("mdc-text-field-helper-text", hint.ClassName);
        //    var ps = cut.FindAll("p");
        //    Assert.NotNull(ps);
        //    Assert.Single(ps);
        //    Assert.Equal("mdc-text-field-helper-text mdc-text-field-helper-text--persistent", ps.First().ClassName);
        //    //var errorContent = cut.Find("p > div");
        //    //Assert.NotNull(errorContent);
        //    //Assert.Equal("validation-message", hint.ClassName);

        //    //check order
        //    var top = cut.Find("div");
        //    Assert.Equal("textarea", top.FirstChild().NodeName.ToLower());
        //    Assert.Equal("label", top.FirstChild().NextElement().NodeName.ToLower());
        //    Assert.Equal("div", top.NextElement().NodeName.ToLower());
        //    Assert.Equal("mdc-text-field-helper-line", top.NextElement().ClassName);
        //    Assert.Equal("div", top.NextElement().FirstChild().NodeName.ToLower());
        //    Assert.Equal("mdc-text-field-helper-text", top.NextElement().FirstChild().ClassName);
        //    Assert.Equal("p", top.NextElement().NextElement().NodeName.ToLower());
        //    Assert.Equal("mdc-text-field-helper-text mdc-text-field-helper-text--persistent", top.NextElement().NextElement().ClassName);
        //    //Assert.Equal("div", top.NextElement().NextElement().FirstChild().NodeName);
        //    //Assert.Equal("validation-message", top.NextElement().NextElement().FirstChild().ClassName);
        //}

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new TextArea();
            Assert.True(sut.HasInput);
        }
    }
}