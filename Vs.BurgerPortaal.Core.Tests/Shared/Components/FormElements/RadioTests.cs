using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vs.BurgerPortaal.Core.Areas.Shared.Components.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements;
using Vs.BurgerPortaal.Core.Objects.FormElements.Interfaces;
using Vs.BurgerPortaal.Core.Tests._Helper.Extensions;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components.FormElements
{
    public class RadioTests : BlazorTestBase
    {
        [Fact]
        public void RadioEmpty()
        {
            var data = new BooleanFormElementData() as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("input");
            var label = cut.Find("p");
            var labels = cut.FindAll("label");
            var hints = cut.FindAll(".mdc-text-field-helper-line");
            var errors = cut.FindAll("div.validation-message");
            Assert.Empty(inputs);
            Assert.Empty(label.InnerHtml);
            Assert.Empty(labels);
            Assert.Empty(hints);
            Assert.Empty(errors);
        }

        [Fact]
        public void RadioOptionsClass()
        {
            //only 2 lower than 10
            var data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                    { "A", "<than10" },
                    { "B", "and_only_2" }
                }
            } as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.Equal(2, cut.FindAll("div > div > input").Count);
            Assert.Empty(cut.FindAll("div > div > div > input"));//0 surrounded by extra div

            //lower than 10 in length, but 3
            data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                    { "A", "Thisisshor" },
                    { "B", "terthan10c" },
                    { "C", "haracters" }
                }
            } as IFormElementData;
            cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.Equal(3, cut.FindAll("div > div > input").Count);
            Assert.Equal(3, cut.FindAll("div > div > div > input").Count);//surrounded by extra div

            //longer than 10
            data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                     { "A", ">than10" },
                     { "B", "and_onlytwo" }
                }
            } as IFormElementData;
            cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.Equal(2, cut.FindAll("div > div > input").Count);
            Assert.Equal(2, cut.FindAll("div > div > div > input").Count);//surrounded by extra div
        }

        [Fact]
        public void RadioFilledTwoOptions()
        {
            var data = new BooleanFormElementData
            {
                IsDisabled = true,
                Options = new Dictionary<string, string> {
                    { "A", "ValueA" },
                    { "B", "ValueB" }
                },
                Name = "TheName",
                Label = "TheLabel"
            } as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.NotNull(cut.Find("div > div"));
            Assert.Equal(2, cut.FindAll("div > div > input").Count);
            var inputs = cut.FindAll("div > div > input");
            var input1 = inputs[0];
            Assert.Equal("TheName", input1.Attr("name"));
            Assert.Equal("A", input1.Attr("value"));
            Assert.Equal("label", input1.Parent.NextElement().NodeName.ToLower());
            Assert.Equal("ValueA", input1.Parent.NextElement().InnerHtml);
            Assert.True(input1.IsDisabled());
            Assert.False(input1.IsChecked());
            var input2 = inputs[1];
            Assert.Equal("TheName", input2.Attr("name"));
            Assert.Equal("B", input2.Attr("value"));
            Assert.Equal("label", input2.Parent.NextElement().NodeName.ToLower());
            Assert.Equal("ValueB", input2.Parent.NextElement().InnerHtml);
            Assert.True(input2.IsDisabled());
            Assert.False(input2.IsChecked());

            Assert.Equal("TheLabel", cut.Find("p").InnerHtml);
        }

        [Fact]
        public void RadioValueSet()
        {
            var data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                    { "A", "ValueA" },
                    { "B", "ValueB" }
                },
                Value = "B"
            } as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputs = cut.FindAll("div > div > input");
            var input1 = inputs[0];
            Assert.Equal("A", input1.Attr("value"));
            Assert.Equal("ValueA", input1.Parent.NextElement().InnerHtml);
            Assert.False(input1.IsChecked());
            var input2 = inputs[1];
            Assert.Equal("B", input2.Attr("value"));
            Assert.Equal("ValueB", input2.Parent.NextElement().InnerHtml);
            Assert.True(input2.IsChecked());
        }

        [Fact]
        public void RadioFilledMarkup()
        {
            var data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                    { "A", "Val<i>ue</i>A" },
                    { "B", "V<u>alueB</u>" }
                }
            } as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var labels = cut.FindAll("div > div > label");
            var label1 = labels[0];
            Assert.Equal("Val<i>ue</i>A", label1.InnerHtml);
            var i = cut.FindAll("div > div > label > i");
            Assert.Single(i);
            Assert.Equal("ue", i[0].InnerHtml);
            var label2 = labels[1];
            Assert.Equal("V<u>alueB</u>", label2.InnerHtml);
            var u = cut.FindAll("div > div > label > u");
            Assert.Single(u);
            Assert.Equal("alueB", u[0].InnerHtml);
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var data = new BooleanFormElementData
            {
                Options = new Dictionary<string, string> {
                    { "A", "ValueA" },
                    { "B", "ValueB" }
                }
            } as IFormElementData;
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            var inputA = cut.FindAll("div > div > input").ElementAt(0);
            Assert.False(inputA.IsChecked());
            var inputB = cut.FindAll("div > div > input").ElementAt(1);
            Assert.False(inputB.IsChecked());
            Assert.Empty(cut.Instance.Data.Value);

            inputB.Change("selected");

            inputA = cut.FindAll("div > div > input").ElementAt(0);
            Assert.False(inputA.IsChecked());
            inputB = cut.FindAll("div > div > input").ElementAt(1);
            Assert.True(inputB.IsChecked());
            Assert.Equal("B", cut.Instance.Data.Value);

            inputB.Change("selected");

            inputA = cut.FindAll("div > div > input").ElementAt(0);
            Assert.False(inputA.IsChecked());
            inputB = cut.FindAll("div > div > input").ElementAt(1);
            Assert.True(inputB.IsChecked());
            Assert.Equal("B", cut.Instance.Data.Value);

            inputA.Change("selected");

            inputA = cut.FindAll("div > div > input").ElementAt(0);
            Assert.True(inputA.IsChecked());
            inputB = cut.FindAll("div > div > input").ElementAt(1);
            Assert.False(inputB.IsChecked());
            Assert.Equal("A", cut.Instance.Data.Value);
        }

        [Fact]
        public void HasCorrectParts()
        {
            var data = new BooleanFormElementData
            {
                Label = "_L",
                HintText = "_H",
                Value = "",
                Options = new Dictionary<string, string> {
                    { "A", "ValueA" },
                    { "B", "ValueB" }
                }
            } as IFormElementData;
            data.CustomValidate();
            Validator.TryValidateObject(data, new ValidationContext(data), null);
            var cut = RenderComponent<Radio>(
                CascadingValue(data),
                CascadingValue(new EditContext(data)));

            Assert.NotEmpty(data.ErrorText);
            Assert.False(data.IsValid);

            var ps = cut.FindAll("p");
            Assert.Equal(3, ps.Count);
            //label
            Assert.Equal("_L", ps[0].InnerHtml);
            //hint
            Assert.Equal("_H", ps[1].InnerHtml);
            //error p
            //var errorContent = ps[2].FirstChild();
            //Assert.NotNull(errorContent);
            //Assert.Equal("validation-message", hint.ClassName);

            //check order
            var top = cut.Find("p");
            Assert.Equal("div", top.NextElement().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("input", top.NextElement().FirstChild().FirstChild().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().FirstChild().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().FirstChild().FirstChild().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().FirstChild().FirstChild().NextElement().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("label", top.NextElement().FirstChild().NextElement().NodeName.ToLower());

            Assert.Equal("div", top.NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("input", top.NextElement().NextElement().FirstChild().FirstChild().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().NextElement().FirstChild().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().NextElement().FirstChild().FirstChild().NextElement().FirstChild().NodeName.ToLower());
            Assert.Equal("div", top.NextElement().NextElement().FirstChild().FirstChild().NextElement().FirstChild().NextElement().NodeName.ToLower());
            Assert.Equal("label", top.NextElement().NextElement().FirstChild().NextElement().NodeName.ToLower());

            Assert.Equal("p", top.NextElement().NextElement().NextElement().NodeName.ToLower());
            Assert.Equal("p", top.NextElement().NextElement().NextElement().NextElement().NodeName.ToLower());
            //Assert.Equal("div", top.NextElement().NextElement().NextElement().NextElement().FirstChild().NodeName);
            //Assert.Equal("validation-message", top.NextElement().NextElement().NextElement().NextElement().ClassName);
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new Radio();
            Assert.True(sut.HasInput);
        }
    }
}