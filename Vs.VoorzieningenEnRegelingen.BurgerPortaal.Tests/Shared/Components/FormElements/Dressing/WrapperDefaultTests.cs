using System.Collections.Generic;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.Dressing;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class WrapperDefaultTests : BlazorTestBase
    {
        [Fact]
        public void WrapperDefaultEmpty()
        {
            var component = _host.AddComponent<WrapperDefault>();
            Assert.True(string.IsNullOrWhiteSpace(component.Find("div").InnerHtml));
            Assert.Equal("input input--invalid", component.Find("div").Attr("class"));
        }

        [Fact]
        public void WrapperDefaultSet()
        {
            var variables = new Dictionary<string, object> {
                { "IsValid", false }
            };
            var component = _host.AddComponent<WrapperDefault>(variables);
            Assert.Equal("input input--invalid", component.Find("div").Attr("class"));
            variables = new Dictionary<string, object> {
                { "IsValid", true }
            };
            component = _host.AddComponent<WrapperDefault>(variables);
            Assert.Equal("input ", component.Find("div").Attr("class"));
        }
    }
}