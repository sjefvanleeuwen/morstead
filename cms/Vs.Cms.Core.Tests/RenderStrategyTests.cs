using Xunit;

namespace Vs.Cms.Core.Tests
{
    public class RenderStrategyTests
    {
        [Fact]
        public void CanRenderSimpleTemplate()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown());
            var result = renderer.Render(@"hello {{variable}}",  new { variable = "world" } );
            Assert.True(result == "<p>hello world</p>\n");
        }

        [Fact]
        public void CanRenderSimpleTemplateWithMarkdown()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown());
            var result = renderer.Render(@"**hello {{variable}}**", new { variable = "world" });
            Assert.True(result == "<p><strong>hello world</strong></p>\n");
        }
    }
}
