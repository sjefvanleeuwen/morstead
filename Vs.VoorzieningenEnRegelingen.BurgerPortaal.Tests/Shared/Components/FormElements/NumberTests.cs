using System.Collections.Generic;
using Vs.Core.Extensions;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Microsoft.AspNetCore.Components.Testing;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class NumberTests : BlazorTestBase
    {
        [Fact]
        public void NumberEmpty()
        {
            var variables = new Dictionary<string, object> { { "Data", new FormElementData() } };
            var component = _host.AddComponent<Number>(variables);
            Assert.NotNull(component.Find("input"));
            Assert.False(component.Find("input").IsRequired());
            Assert.False(component.Find("input").IsDisabled());
            Assert.Empty(component.Find("input").Attr("required"));
            Assert.Empty(component.Find("input").Attr("disabled"));
            Assert.Empty(component.Find("input").Id);
            Assert.Equal("hint_", component.Find("input").Attr("aria-describedby"));
            Assert.Equal("input__control input__control--text ", component.Find("input").Attr("class"));
            Assert.Empty(component.Find("input").Attr("value"));
        }

        [Fact]
        public void NumberFilled()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        IsRequired = true,
                        IsDisabled = true,
                        Name = "TheName",
                        Size = FormElementSize.Large ,
                        Value = "123"
                    }
                }
            };

            var component = _host.AddComponent<Number>(variables);
            Assert.NotNull(component.Find("input"));
            Assert.True(component.Find("input").IsRequired());
            Assert.True(component.Find("input").IsDisabled());
            Assert.Equal("TheName", component.Find("input").Id);
            Assert.Equal("hint_TheName", component.Find("input").Attr("aria-describedby"));
            Assert.Equal("input__control input__control--text input__control--l", component.Find("input").Attr("class"));
            Assert.Equal("123", component.Find("input").Attr("value"));
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Name = "TheName",
                        Size = FormElementSize.Large ,
                        Value = "123"
                    }
                }
            };
            var component = _host.AddComponent<Number>(variables);
            Assert.Equal("123", component.Find("input").Attr("value"));

            var element = component.Find("input");
            element.Change("345");
            Assert.Equal("345", component.Find("input").Attr("value"));

            Assert.Equal("345", component.Instance.Value);
        }

        [Fact]
        public void HasDressingElements()
        {
            //make sure elements are rendered
            var variables = new Dictionary<string, object> {
                { 
                    "Data", new FormElementData() {
                        Label = "_",
                        HintText = "_" ,
                        ErrorText = "_"
                    }
                }
            };
            var component = _host.AddComponent<Number>(variables);
            Assert.NotNull(component.Find("div > input")); //it is contained in a wrapper
            Assert.Single(component.FindAll("div > input")); //only 1 input
            Assert.NotNull(component.Find("div > label")); //label present
            Assert.Equal("span", component.Find("div > label").NextElement().Name); //label followed by a hinttext
            Assert.Equal("div", component.Find("div > label + span").NextElement().Name); //label followed by error
            Assert.Equal("input", component.Find("div > label + span + div").NextElement().Name); //error followed by the one and only input
        }
    }
}