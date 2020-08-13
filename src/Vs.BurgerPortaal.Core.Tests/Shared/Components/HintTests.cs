using Bunit;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Shared.Components
{
    public class HintTests : BlazorTestBase
    {
        [Fact]
        public void HintEmpty()
        {
            var cut = RenderComponent<Hint>();
            Assert.Empty(cut.Nodes);
            Assert.Empty(cut.FindAll("div"));
        }

        [Fact]
        public void HintFilledTitle()
        {
            var cut = RenderComponent<Hint>(
                (nameof(Hint.Title), "Title"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Single(cut.FindAll("div"));
            Assert.Single(cut.FindAll("h4"));
            Assert.Empty(cut.FindAll("p"));
        }

        [Fact]
        public void HintFilledContent()
        {
            var cut = RenderComponent<Hint>(
                (nameof(Hint.Content), "Content"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Single(cut.FindAll("div"));
            Assert.Empty(cut.FindAll("h4"));
            Assert.Single(cut.FindAll("p"));
        }

        [Fact]
        public void HintFilled()
        {
            var cut = RenderComponent<Hint>(
                (nameof(Hint.Title), "Title"),
                (nameof(Hint.Content), "Content"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Single(cut.FindAll("div"));
            Assert.Single(cut.FindAll("h4"));
            Assert.Single(cut.FindAll("p"));
            Assert.Equal("Title", cut.FindAll("h4")[0].InnerHtml);
            Assert.Equal("Content", cut.FindAll("p")[0].InnerHtml);
        }

        [Fact]
        public void HintFilledWithMarkup()
        {
            var cut = RenderComponent<Hint>(
                (nameof(Hint.Title), "Ti<u>tl</u>e"),
                (nameof(Hint.Content), "C<i>onten</i>t"));
            Assert.NotEmpty(cut.Nodes);
            Assert.Single(cut.FindAll("u"));
            Assert.Single(cut.FindAll("i"));
            Assert.Equal("tl", cut.FindAll("u")[0].InnerHtml);
            Assert.Equal("onten", cut.FindAll("i")[0].InnerHtml);
        }
    }
}
