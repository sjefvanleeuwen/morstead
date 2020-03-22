using System.Collections.Generic;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class BadgeTests : BlazorTestBase
    {
        [Fact]
        public void BadgeEmpty()
        {
            var component = _host.AddComponent<Badge>();
            Assert.Null(component.Find("span"));
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(null, false)]
        [InlineData(1, true)]
        public void BadgeWorksShown(int? value, bool shown)
        {
            var variables = new Dictionary<string, object> { { "Number", value } };
            var component = _host.AddComponent<Badge>(variables);
            Assert.Equal(shown, component.Find("span") != null);
        }

        [Theory]
        [InlineData(5, "5", 5, "-5")]
        [InlineData(-5, "-5", 0, "0")]
        [InlineData(18, "18", -30, "30")]
        public void BadgeShowsCorrectly(int number, string numberValue, int translateY, string translateYValue)
        {
            var variables = new Dictionary<string, object> { { "Number", number }, { "GoUp", translateY } };
            var component = _host.AddComponent<Badge>(variables);
            Assert.Equal(numberValue, component.Find("span").InnerText);
            Assert.Equal($"transform: translateY({translateYValue}px);", component.Find("span").Attr("style"));
        }
    }
}