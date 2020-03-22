using System.Collections.Generic;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class WrapperFieldsetTests : BlazorTestBase
    {
        [Fact]
        public void WrapperFieldsetEmpty()
        {
            var component = _host.AddComponent<WrapperFieldset>();
            Assert.True(string.IsNullOrWhiteSpace(component.Find("fieldset").InnerHtml));
            Assert.Equal("input input--invalid", component.Find("fieldset").Attr("class"));
        }

        [Fact]
        public void WrapperFieldsetSet()
        {
            var variables = new Dictionary<string, object> {
                { "IsValid", false }
            };
            var component = _host.AddComponent<WrapperFieldset>(variables);
            Assert.Equal("input input--invalid", component.Find("fieldset").Attr("class"));
            variables = new Dictionary<string, object> {
                { "IsValid", true }
            };
            component = _host.AddComponent<WrapperFieldset>(variables);
            Assert.Equal("input ", component.Find("fieldset").Attr("class"));
        }
    }
}