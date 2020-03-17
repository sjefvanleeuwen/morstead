using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Vs.Core.Extensions;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class ErrorTests : BlazorTestBase
    {
        [Fact]
        public void ErrorEmpty()
        {
            var component = _host.AddComponent<Error>();
            Assert.Empty(component.GetMarkup());
        }

        [Fact]
        public void ErrorExists()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "Error text" }
            };
            var component = _host.AddComponent<Error>(variables);
            Assert.NotEmpty(component.GetMarkup());
            Assert.NotNull(component.Find("div"));
            Assert.Equal("Error text", component.Find("div").InnerHtml);
        }

        [Fact]
        public void ErrorExistsMarkup()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "Error<i> text</i>" }
            };
            var component = _host.AddComponent<Error>(variables);
            Assert.NotEmpty(component.GetMarkup());
            Assert.NotNull(component.Find("div"));
            Assert.Equal(" text", component.Find("div > i").InnerHtml);
        }
    }
}