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
            //var variables = new Dictionary<string, object> { { "Number", number }, { "GoUp", translateY } };
            var component = _host.AddComponent<Number>();
            Assert.NotNull(component.Find("input"));
            Assert.False(component.Instance.IsRequired);
            Assert.False(component.Instance.IsDisabled);
            Assert.Empty(component.Find("input").Attr("required"));
            Assert.Empty(component.Find("input").Attr("disabled"));
            Assert.Empty(component.Find("input").Attr("id"));
            Assert.Equal("hint_", component.Find("input").Attr("aria-describedby"));
            Assert.Equal("input__control input__control--text ", component.Find("input").Attr("class"));
            Assert.Empty(component.Find("input").Attr("value"));
        }

        [Fact]
        public void NumberFilled()
        {
            var variables = new Dictionary<string, object> {
                { "IsRequired", true },
                { "IsDisabled", true },
                { "Name", "TheName" },
                { "Size", FormElementSize.Large },
                { "Value", "123" }
            };
            var component = _host.AddComponent<Number>(variables);
            Assert.NotNull(component.Find("input"));
            Assert.True(component.Instance.IsRequired);
            Assert.True(component.Instance.IsDisabled);
            //there is no difference in the component at this pont regarding 
            //the rendering of required="@Required" and disabled="@Disabled" True vs False
            Assert.Empty(component.Find("input").Attr("required"));
            Assert.Empty(component.Find("input").Attr("disabled"));
            Assert.Equal("TheName", component.Find("input").Attr("id"));
            Assert.Equal("hint_TheName", component.Find("input").Attr("aria-describedby"));
            Assert.Equal("input__control input__control--text input__control--l", component.Find("input").Attr("class"));
            Assert.Equal("123", component.Find("input").Attr("value"));
        }

        [Fact]
        public void NumberTwoWayBindTest()
        {
            var variables = new Dictionary<string, object> {
                { "Name", "TheName" },
                { "Size", FormElementSize.Large },
                { "Value", "123" }
            };
            var component = _host.AddComponent<Number>(variables);
            Assert.Equal("123", component.Find("input").Attr("value"));

            var element = component.Find("input");
            element.Change("345");
            Assert.Equal("345", component.Find("input").Attr("value"));

            Assert.Equal("345", component.Instance.Value);
        }
    }
}