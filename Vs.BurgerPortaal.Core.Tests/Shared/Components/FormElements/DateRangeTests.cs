using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Vs.BurgerPortaal.Core.Tests._Helper.Extensions;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components.FormElements
{
    public class DateRangeTests : BlazorTestBase
    {
        [Fact]
        public void DateRangeEmpty()
        {
            var data = new DateRangeFormElementData() as IFormElementData;
            var cut = RenderComponent<DateRange>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("input");
            var labels = cut.FindAll("label");
            var hints = cut.FindAll(".mdc-text-field-helper-line");
            var errors = cut.FindAll("div.validation-message");
            Assert.Equal(4, inputs.Count);
            Assert.Contains("mat-text-field-input", inputs.First().ClassName);
            var input1 = inputs[0];
            Assert.NotNull(input1);
            Assert.NotNull(input1.Id);
            Assert.Equal("_start", input1.Id);
            Assert.Null(input1.Attr("value"));
            Assert.Null(input1.Attr("aria-label"));
            Assert.False(input1.IsDisabled());
            var input2 = inputs[2];
            Assert.NotNull(input2);
            Assert.NotNull(input2.Id);
            Assert.Equal("_end", input2.Id);
            Assert.Null(input2.Attr("value"));
            Assert.Null(input2.Attr("aria-label"));
            Assert.False(input2.IsDisabled());

            Assert.Empty(labels);
            Assert.Empty(hints);
            Assert.Empty(errors);
        }

        [Fact]
        public void DateRangeFilled()
        {
            var data = new DateRangeFormElementData
            {
                IsDisabled = true,
                Name = "TheName",
                Value = "1979-03-08 - 1988-05-09",
                Culture = new CultureInfo("nl-NL")
            } as IFormElementData;
            System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

            var cut = RenderComponent<DateRange>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));
            var inputs = cut.FindAll("input");
            var labels = cut.FindAll("label");
            var hints = cut.FindAll("div.mdc-text-field-helper-text");
            var errors = cut.FindAll("div.validation-message");
            Assert.Equal(4, inputs.Count);
            Assert.Contains("mat-text-field-input", inputs[0].ClassName);
            var input1 = inputs[0];
            Assert.NotNull(input1);
            Assert.Equal("TheName_start", input1.Id);
            Assert.NotNull(input1.Attr("value"));
            Assert.Equal("8-3-1979", input1.Attr("value"));
            Assert.Null(input1.Attr("aria-label"));
            Assert.True(input1.IsDisabled());

            var input2 = inputs[2];
            Assert.NotNull(input2);
            Assert.Equal("TheName_end", input2.Id);
            Assert.NotNull(input2.Attr("value"));
            Assert.Equal("9-5-1988", input2.Attr("value"));
            Assert.Null(input2.Attr("aria-label"));
            Assert.True(input2.IsDisabled());

            Assert.Empty(labels);
            Assert.Empty(hints);
            Assert.Empty(errors);
        }

        [Fact]
        public void DateRangeFilledUSCulture()
        {
            var data = new DateRangeFormElementData
            {
                Value = "1979-03-08 - 1988-05-09",
                Culture = new CultureInfo("en-US")
            } as IFormElementData;
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var cut = RenderComponent<DateRange>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("input");
            var input1 = inputs[0];
            //This is an interface test. If you run this test on a server that has a changed format for en-US you will get a fail here.
            Assert.Equal("3/8/1979", input1.Attr("value"));
            var input2 = inputs[2];
            Assert.Equal("5/9/1988", input2.Attr("value"));
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var data = new DateRangeFormElementData
            {
                Value = "1979-03-08 - 1988-05-09",
            } as IFormElementData;
            System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

            var cut = RenderComponent<DateRange>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.Equal(new DateTime(1979, 3, 8), (cut.Instance.Data as IDateRangeFormElementData).ValueDateStart);
            Assert.Equal(new DateTime(1988, 5, 9), (cut.Instance.Data as IDateRangeFormElementData).ValueDateEnd);
            Assert.Equal("8-3-1979", cut.FindAll("input")[0].Attr("value"));
            Assert.Equal("9-5-1988", cut.FindAll("input")[2].Attr("value"));

            cut.FindAll("input")[0].Change("11-3-1944");

            Assert.Equal(new DateTime(1944, 3, 11), (cut.Instance.Data as IDateRangeFormElementData).ValueDateStart);
            Assert.Equal(new DateTime(1988, 5, 9), (cut.Instance.Data as IDateRangeFormElementData).ValueDateEnd);
            Assert.Equal("11-3-1944", cut.FindAll("input")[0].Attr("value"));
            Assert.Equal("9-5-1988", cut.FindAll("input")[2].Attr("value"));

            cut.FindAll("input")[2].Change("1-8-1949");

            Assert.Equal(new DateTime(1944, 3, 11), (cut.Instance.Data as IDateRangeFormElementData).ValueDateStart);
            Assert.Equal(new DateTime(1949, 8, 1), (cut.Instance.Data as IDateRangeFormElementData).ValueDateEnd);
            Assert.Equal("11-3-1944", cut.FindAll("input")[0].Attr("value"));
            Assert.Equal("1-8-1949", cut.FindAll("input")[2].Attr("value"));
        }

        [Fact]
        public void HasCorrectParts()
        {
            var data = new DateRangeFormElementData
            {
                LabelStart = "TheLabelStart",
                LabelEnd = "TheLabelEnd",
                HintText = "TheHint"
            } as IFormElementData;
            System.Threading.Thread.CurrentThread.CurrentCulture = data.Culture;

            data.CustomValidate();
            Validator.TryValidateObject(data, new ValidationContext(data), null);
            var cut = RenderComponent<DateRange>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.NotEmpty(data.ErrorText);
            Assert.False(data.IsValid);

            var input1 = cut.FindAll("input")[0];
            var input2 = cut.FindAll("input")[2];
            var labels = cut.FindAll("label");
            var hints = cut.FindAll("div.mdc-text-field-helper-text");
            var errors = cut.FindAll("div.validation-message");

            Assert.NotNull(input1.Attr("aria-label"));
            Assert.Equal("TheLabelStart", input1.Attr("aria-label"));
            Assert.NotNull(input2.Attr("aria-label"));
            Assert.Equal("TheLabelEnd", input2.Attr("aria-label"));
            Assert.Equal(2, labels.Count);
            Assert.Equal("TheLabelStart", labels[0].InnerHtml);
            Assert.Equal("TheLabelEnd", labels[1].InnerHtml);
            Assert.Equal(2, hints.Count);
            Assert.Equal("TheHint", hints[0].InnerHtml);
            Assert.Equal("TheHint", hints[1].InnerHtml);
            //Assert.NotEmpty(errors);

            //check order
            var top = cut.Find("div");
            Assert.Equal("div", top.FirstChild().NodeName.ToLower());

            var date_start = top.FirstChild();
            Assert.Equal("input", date_start.FirstChild().NodeName.ToLower());
            Assert.Equal("label", date_start.FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("div", date_start.FirstChild().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("div", date_start.FirstChild().NextElement().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("input", date_start.FirstChild().NextElement().NextElement().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("button", date_start.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("i", date_start.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("div", date_start.NextElement().NodeName.ToLower());
            Assert.Equal("div", date_start.NextElement().FirstChild().NodeName.ToLower());

            Assert.Equal("div", top.NextElement().FirstChild().NodeName.ToLower());

            var date_end = top.NextElement().FirstChild();
            Assert.Equal("input", date_end.FirstChild().NodeName.ToLower());
            Assert.Equal("label", date_end.FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("div", date_end.FirstChild().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("div", date_end.FirstChild().NextElement().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("input", date_end.FirstChild().NextElement().NextElement().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("button", date_end.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("i", date_end.FirstChild().NextElement().NextElement().NextElement().FirstChild().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("div", date_end.NextElement().NodeName.ToLower());
            Assert.Equal("div", date_end.NextElement().FirstChild().NodeName.ToLower());

            Assert.Equal("p", top.NextElement().NextElement().NodeName.ToLower());
            //Assert.Equal("div", top2.NextElement().NextElement().FirstChild().NodeName);
            //Assert.Equal("validation-message", top2.NextElement().NextElement().FirstChild().ClassName);
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new DateRange();
            Assert.True(sut.HasInput);
        }
    }
}