using System.Collections.Generic;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Vs.Core.Extensions;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class LabelTests : BlazorTestBase
    {
        [Fact]
        public void LabelEmpty()
        {
            var component = _host.AddComponent<Label>();
            Assert.True(string.IsNullOrWhiteSpace(component.Find("label").InnerHtml));
            var variables = new Dictionary<string, object> {
                { "TagText", " " }
            };
            component = _host.AddComponent<Label>(variables);
            Assert.True(string.IsNullOrWhiteSpace(component.Find("label").InnerHtml));
        }

        [Fact]
        public void LabelExists()
        {
            var variables = new Dictionary<string, object> {
                { "Name", "TagName" },
                { "Text", "Label text" },
                { "TagText", "Tag text" }
            };
            var component = _host.AddComponent<Label>(variables);
            Assert.Equal("TagName", component.Find("label").Attr("for"));
            Assert.Equal("Label text", component.Find("label").Elements("#text").First().InnerHtml.Trim());
            Assert.NotNull(component.Find("label > span"));
            Assert.Equal("Tag text", component.Find("label > span").InnerHtml);
        }

        [Fact]
        public void LabelExistsMarkup()
        {
            var variables = new Dictionary<string, object> {
                { "Text", "La<i>bel te</i>xt" },
                { "TagText", "T<b>ag </b>text" }
            };
            var component = _host.AddComponent<Label>(variables);
            Assert.Equal("bel te", component.Find("label > i").InnerHtml);
            Assert.Equal("ag ", component.Find("label > span > b").InnerHtml);
        }
    }
}