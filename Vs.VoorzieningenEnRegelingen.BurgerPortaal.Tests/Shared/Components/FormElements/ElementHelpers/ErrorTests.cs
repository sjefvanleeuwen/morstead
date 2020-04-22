using Bunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements.ElementHelpers;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements.ElementHelpers
{
    public class ErrorTests : BlazorTestBase
    {
        [Fact]
        public void ErrorEmpty()
        {
            var cut = RenderComponent<Error>();
            Assert.Empty(cut.Nodes);
        }

        [Fact]
        public void ErrorExists()
        {
            var cut = RenderComponent<Error>(
               ChildContent("The error message"));
            var message = cut.FindAll("p");
            Assert.NotNull(message);
            Assert.Single(message);
            Assert.Equal("The error message", message[0].InnerHtml);
        }

        [Fact]
        public void ErrorExistsMarkup()
        {
            var cut = RenderComponent<Error>(
               ChildContent("The e<i>rror m</i>essage"));
            var message = cut.FindAll("p");
            Assert.NotNull(message);
            Assert.Single(message);
            Assert.Equal("The e<i>rror m</i>essage", message[0].InnerHtml);
            var innerMessage = cut.FindAll("i");
            Assert.NotNull(innerMessage);
            Assert.Single(innerMessage);
            Assert.Equal("rror m", innerMessage[0].InnerHtml);
        }
    }
}