using Bunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class MaterialWrapperTests : BlazorTestBase
    {
        [Fact]
        public void MaterialWrapperEmpty()
        {
            var cut = RenderComponent<MaterialWrapper>();
            Assert.Equal(3, cut.FindAll("div").Count);
        }

        [Fact]
        public void MaterialWrapperFilled()
        {
            var cut = RenderComponent<MaterialWrapper>(
               ChildContent("The error message"));
            Assert.Equal("The error message", cut.Find("div > div > div").InnerHtml.Trim());
        }

        [Fact]
        public void MaterialWrapperFilledMarkup()
        {
            var cut = RenderComponent<MaterialWrapper>(
               ChildContent("The e<i>rror m</i>essage"));
            var message = cut.FindAll("div > div > div");
            Assert.NotNull(message);
            Assert.Single(message);
            Assert.Equal("The e<i>rror m</i>essage", message[0].InnerHtml.Trim());
            var innerMessage = cut.FindAll("i");
            Assert.NotNull(innerMessage);
            Assert.Single(innerMessage);
            Assert.Equal("rror m", innerMessage[0].InnerHtml);
        }
    }
}