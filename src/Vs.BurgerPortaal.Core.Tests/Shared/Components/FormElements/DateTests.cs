using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Tests._Helper.Extensions;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components.FormElements
{
    public class DateTests : BlazorTestBase
    {
        //[Fact]
        //public void DateEmpty()
        //{
        //    var data = new DateFormElementData() as IFormElementData;
        //    var cut = RenderComponent<Date>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));
        //    System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

        //    var inputs = cut.FindAll("input");
        //    var labels = cut.FindAll("label");
        //    var hints = cut.FindAll(".mdc-text-field-helper-line");
        //    var errors = cut.FindAll("div.validation-message");
        //    Assert.Equal(2, inputs.Count);
        //    Assert.Contains("mat-text-field-input", inputs.First().ClassName);
        //    var input = inputs[0];
        //    Assert.NotNull(input);
        //    Assert.Null(input.Id);
        //    Assert.Null(input.Attr("value"));
        //    Assert.Null(input.Attr("aria-label"));
        //    Assert.False(input.IsDisabled());
        //    Assert.Empty(labels);
        //    Assert.Empty(hints);
        //    Assert.Empty(errors);
        //}

        //[Fact]
        //public void DateFilled()
        //{
        //    var data = new DateFormElementData
        //    {
        //        IsDisabled = true,
        //        Name = "TheName",
        //        Value = "1979-03-08",
        //        Culture = new CultureInfo("nl-NL")
        //    } as IFormElementData;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;
        //    var cut = RenderComponent<Date>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var inputs = cut.FindAll("input");
        //    var labels = cut.FindAll("label");
        //    var hints = cut.FindAll("div.mdc-text-field-helper-text");
        //    var errors = cut.FindAll("div.validation-message");
        //    Assert.Equal(2, inputs.Count);
        //    Assert.Contains("mat-text-field-input", inputs[0].ClassName);
        //    var input = inputs[0];
        //    Assert.NotNull(input);
        //    Assert.Equal("TheName", input.Id);
        //    Assert.NotNull(input.Attr("value"));
        //    Assert.Equal("8-3-1979", input.Attr("value"));
        //    Assert.Null(input.Attr("aria-label"));
        //    Assert.True(input.IsDisabled());
        //    Assert.Empty(labels);
        //    Assert.Empty(hints);
        //    Assert.Empty(errors);
        //}

        //[Fact]
        //public void DateFilledUSCulture()
        //{
        //    var data = new DateFormElementData
        //    {
        //        Value = "1979-03-08",
        //        Culture = new CultureInfo("en-US")
        //    } as IFormElementData;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

        //    var cut = RenderComponent<Date>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var input = cut.Find("input");
        //    //This is an interface test. If you run this test on a server that has a changed format for en-US you will get a fail here.
        //    Assert.Equal("3/8/1979", input.Attr("value"));
        //}

        //[Fact]
        //public void ShouldDoTwoWayBinding()
        //{
        //    var data = new DateFormElementData
        //    {
        //        Value = "1979-03-08"
        //    } as IFormElementData;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;
        //    var cut = RenderComponent<Date>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    var input = cut.Find("input");
        //    Assert.Equal(new DateTime(1979, 3, 8), (cut.Instance.Data as IDateFormElementData).ValueDate);
        //    Assert.Equal("8-3-1979", input.Attr("value"));
        //    input.Change("9-5-1988");
        //    input = cut.Find("input");
        //    Assert.Equal(new DateTime(1988, 5, 9), (cut.Instance.Data as IDateFormElementData).ValueDate);
        //    Assert.Equal("9-5-1988", input.Attr("value"));
        //}

        //[Fact]
        //public void HasCorrectParts()
        //{
        //    var data = new DateFormElementData
        //    {
        //        Label = "TheLabel",
        //        HintText = "TheHint"
        //    } as IFormElementData;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

        //    data.CustomValidate();
        //    Validator.TryValidateObject(data, new ValidationContext(data), null);
        //    var cut = RenderComponent<Date>(
        //        CascadingValue(data),
        //        CascadingValue(new EditContext(data)));

        //    Assert.NotEmpty(data.ErrorText);
        //    Assert.False(data.IsValid);

        //    var input = cut.Find("input");
        //    var labels = cut.FindAll("label");
        //    var hints = cut.FindAll("div.mdc-text-field-helper-text");
        //    var errors = cut.FindAll("div.validation-message");

        //    Assert.NotNull(input.Attr("aria-label"));
        //    Assert.Equal("TheLabel", input.Attr("aria-label"));
        //    Assert.Single(labels);
        //    Assert.Equal("TheLabel", labels[0].InnerHtml);
        //    Assert.Single(hints);
        //    Assert.Equal("TheHint", hints[0].InnerHtml);
        //    //Assert.NotEmpty(errors);

        //    //check order
        //    var top = cut.Find("div");
        //    Assert.Equal("input", top.FirstChild().NodeName.ToLower());
        //    Assert.Equal("label", top.FirstChild().NextElement().NodeName.ToLower());
        //    Assert.Equal("div", top.FirstChild().NextElement().NextElement().NodeName.ToLower());
        //    Assert.Equal("div", top.FirstChild().NextElement().NextElement().NextElement().NodeName.ToLower());
        //    Assert.Equal("input", top.FirstChild().NextElement().NextElement().NextElement().FirstChild().NodeName.ToLower());
        //    Assert.Equal("button", top.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().NodeName.ToLower());
        //    Assert.Equal("i", top.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().FirstChild().NodeName.ToLower());
        //    Assert.Equal("div", top.NextElement().NodeName.ToLower());
        //    Assert.Equal("div", top.NextElement().FirstChild().NodeName.ToLower());
        //    Assert.Equal("p", top.NextElement().NextElement().NodeName.ToLower());
        //    //Assert.Equal("div", top.NextElement().NextElement().FirstChild().NodeName);
        //    //Assert.Equal("validation-message", top.NextElement().NextElement().FirstChild().ClassName);
        //}

        //[Fact]
        //public void ShouldHaveInput()
        //{
        //    var sut = new Date();
        //    Assert.True(sut.HasInput);
        //}
    }
}