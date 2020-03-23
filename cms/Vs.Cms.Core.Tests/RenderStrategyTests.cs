using System.Collections.Generic;
using Xunit;

namespace Vs.Cms.Core.Tests
{
    public class RenderStrategyTests
    {
        [Fact]
        public void CanRenderSimpleTemplate()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown());
            var result = renderer.Render(@"hello {{variable}}", new { variable = "world" });
            Assert.True(result == "<p>hello world</p>\n");
        }

        [Fact]
        public void CanRenderSimpleTemplateWithMarkdown()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown());
            var result = renderer.Render(@"**hello {{variable}}**", new { variable = "world" });
            Assert.True(result == "<p><strong>hello world</strong></p>\n");
        }

        [Fact]
        public void CanRenderTemplateUsingDictionary()
        {
            var fields = new Dictionary<string, object>();
            fields.Add("boolean", true);
            fields.Add("double", 4.25);
            fields.Add("hello", "hello");
            var renderer = new RenderStrategy(new Liquid(), new Markdown());
            var result = renderer.Render(@"**{{hello}}**
€ {{double}} 
{% if boolean %}
  world!
{% endif %}
", fields);
            Assert.True(result == "<p><strong>hello</strong>\n€ 4.25</p>\n<p>world!</p>\n");
        }
    }
}
