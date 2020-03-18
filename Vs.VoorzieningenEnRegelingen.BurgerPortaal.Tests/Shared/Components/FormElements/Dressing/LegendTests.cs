using System.Collections.Generic;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class LegendTests : BlazorTestBase
    {
        [Fact]
        public void LegendEmpty()
        {
            var component = _host.AddComponent<Legend>();
            Assert.True(string.IsNullOrWhiteSpace(component.Find("legend").InnerHtml));
            var variables = new Dictionary<string, object> {
                { "TagText", " " }
            };
            component = _host.AddComponent<Legend>(variables);
            Assert.True(string.IsNullOrWhiteSpace(component.Find("legend").InnerHtml));
        }

        [Fact]
        public void LegendExists()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "Legend text" },
                { "TagText", "Tag text" }
            };
            var component = _host.AddComponent<Legend>(variables);
            Assert.Equal("Legend text", component.Find("legend").Elements("#text").First().InnerHtml.Trim());
            Assert.NotNull(component.Find("legend > span"));
            Assert.Equal("Tag text", component.Find("legend > span").InnerHtml);
        }

        [Fact]
        public void LegendExistsMarkup()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "Le<i>gend te</i>xt" },
                { "TagText", "T<b>ag </b>text" }
            };
            var component = _host.AddComponent<Legend>(variables);
            Assert.Equal("gend te", component.Find("legend > i").InnerHtml);
            Assert.Equal("ag ", component.Find("legend > span > b").InnerHtml);
        }
    }
}