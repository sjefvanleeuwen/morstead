using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Tests._Helper.Extensions;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components.FormElements
{
    public class SelectTests : BlazorTestBase
    {
        [Fact]
        public void SelectEmpty()
        {
            var data = new ListFormElementData() as IFormElementData;
            var cut = RenderComponent<Select>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var selects = cut.FindAll("div.mdc-select");
            var selected = cut.FindAll("div.mdc-select > div > div.mdc-select__selected-text");
            var options = cut.FindAll("li");
            var labels = cut.FindAll("span.mdc-floating-label");
            var helpertexts = cut.FindAll("p.mdc-text-field-helper-text");
            var errors = cut.FindAll("div.validation-message");
            Assert.Single(selects);
            var select = selects[0];
            Assert.NotNull(select);
            Assert.Null(select.Id);
            Assert.Single(selected);
            Assert.Empty(options);
            Assert.Null(select.Attr("aria-label"));
            Assert.DoesNotContain("mdc-select--disabled", select.ClassName);
            Assert.Empty(labels);
            Assert.Single(helpertexts);//not the hint
            Assert.Empty(errors);
        }

        [Fact]
        public void SelectFilled()
        {
            var data = new ListFormElementData
            {
                IsDisabled = true,
                Name = "TheName",
                Label = "TheLabel",
                HintText = "TheHint",
                Options = new Dictionary<string, string>
                {
                    { "A", "ListOptionA" },
                    { "B", "ListOptionB" },
                    { "C", "ListOptionC" },
                    { "D", "ListOptionD" }
                }
            } as IFormElementData;
            var cut = RenderComponent<Select>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var selects = cut.FindAll("div.mdc-select");
            var selected = cut.FindAll("div.mdc-select > div > div.mdc-select__selected-text");
            var options = cut.FindAll("li");
            var labels = cut.FindAll("span.mdc-floating-label");
            var helpertexts = cut.FindAll("p.mdc-text-field-helper-text");
            Assert.Single(selects);
            var select = selects[0];
            Assert.NotNull(select);
            Assert.Equal("TheName", select.Id);
            Assert.Single(selected);
            //selecting the value doesn't work since that is a javascript triggered event
            Assert.Equal(4, options.Count);
            Assert.Equal("ListOptionA", options[0].InnerHtml.Trim());
            Assert.Equal("A", options[0].Attr("data-value"));
            Assert.Equal("ListOptionB", options[1].InnerHtml.Trim());
            Assert.Equal("B", options[1].Attr("data-value"));
            Assert.Equal("ListOptionC", options[2].InnerHtml.Trim());
            Assert.Equal("C", options[2].Attr("data-value"));
            Assert.Equal("ListOptionD", options[3].InnerHtml.Trim());
            Assert.Equal("D", options[3].Attr("data-value"));
            Assert.Contains("mdc-select--disabled", select.ClassName);
            Assert.Single(labels);
            Assert.Equal("TheLabel", labels[0].InnerHtml);
            Assert.Equal(2, helpertexts.Count);
            Assert.Equal("TheHint", helpertexts[0].InnerHtml);
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var data = new ListFormElementData
            {
                Options = new Dictionary<string, string>
                {
                    { "A", "ListOptionA" },
                    { "B", "ListOptionB" },
                    { "C", "ListOptionC" },
                    { "D", "ListOptionD" }
                }
            } as IFormElementData;
            var cut = RenderComponent<Select>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var select = cut.Find("div.mdc-select");
            //selecting the value doesn't work since that is a javascript triggered event
            //select.Change("A");
            //Assert.Equal("A", cut.Instance.Data.Value);
            //select.Change("C");
            //Assert.Equal("C", cut.Instance.Data.Value);
        }

        [Fact]
        public void HasCorrectParts()
        {
            var data = new ListFormElementData
            {
                Label = "_",
                HintText = "_",
                Value = "",
                Options = new Dictionary<string, string>
                {
                    { "", "" },
                    { "A", "ListOptionA" },
                    { "B", "ListOptionB" },
                    { "C", "ListOptionC" },
                    { "D", "ListOptionD" }
                }
            } as IFormElementData;
            data.CustomValidate();
            Validator.TryValidateObject(data, new ValidationContext(data), null);
            var cut = RenderComponent<Select>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.NotEmpty(data.ErrorText);
            Assert.False(data.IsValid);

            var select = cut.FindAll("div.mdc-select");
            Assert.NotNull(select);
            var options = cut.FindAll("li");
            Assert.Equal(5, options.Count);
            var label = cut.Find("div > div > span.mdc-floating-label");
            Assert.NotNull(label);
            var helpertexts = cut.FindAll("p.mdc-text-field-helper-text");
            Assert.Equal(2, helpertexts.Count);
            //var errorContent = cut.Find("p > div");
            //Assert.NotNull(errorContent);
            //Assert.Equal("validation-message", hint.ClassName);

            //check order
            var div = cut.Find("div.mdc-select");
            Assert.Equal("div", div.FirstChild().NodeName.ToLower());
            Assert.Equal("i", div.FirstChild().FirstChild().NodeName.ToLower());
            Assert.Equal("div", div.FirstChild().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("span", div.FirstChild().FirstChild().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("div", div.FirstChild().FirstChild().NextElement().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("mdc-floating-label", div.FirstChild().FirstChild().NextElement().NextElement().ClassName);
            Assert.Equal("div", div.FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("ul", div.FirstChild().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("li", div.FirstChild().NextElement().FirstChild().FirstChild().NodeName.ToLower());
            Assert.Equal("p", div.NextElement().NodeName.ToLower());
            Assert.Equal("p", div.NextElement().NextElement().NodeName.ToLower());
            //Assert.Equal("div", top.NextElement().NextElement().FirstChild().NodeName);
            //Assert.Equal("validation-message", top.NextElement().NextElement().FirstChild().ClassName);
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new Select();
            Assert.True(sut.HasInput);
        }
    }
}