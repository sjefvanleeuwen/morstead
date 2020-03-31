using System.Collections.Generic;
using Vs.Core.Web.Extensions;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class FormElementHintTests : BlazorTestBase
    {
        [Fact]
        public void FormElementHintEmpty()
        {
            var component = _host.AddComponent<FormElementHint>();
            Assert.Empty(component.GetMarkup());
            var variables = new Dictionary<string, object> {
                { "Text", " " }
            };
            component = _host.AddComponent<FormElementHint>(variables);
            Assert.Empty(component.GetMarkup());
        }

        [Fact]
        public void FormElementHintExists()
        {
            var variables = new Dictionary<string, object> {
                { "Name", "DeNaam" },
                { "Text", "Hint text" }
            };
            var component = _host.AddComponent<FormElementHint>(variables);
            Assert.NotEmpty(component.GetMarkup());
            Assert.NotNull(component.Find("span"));
            Assert.Equal("hint_DeNaam", component.Find("span").Attr("id"));
            Assert.Equal("Hint text", component.Find("span").InnerHtml);
        }

        [Fact]
        public void FormElementHintExistsMarkup()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "Hint<i> text</i>" }
            };
            var component = _host.AddComponent<FormElementHint>(variables);
            Assert.NotEmpty(component.GetMarkup());
            Assert.NotNull(component.Find("span"));
            Assert.Equal(" text", component.Find("span > i").InnerHtml);
        }
    }
}