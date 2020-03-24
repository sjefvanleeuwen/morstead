using System.Collections.Generic;
using Xunit;

namespace Vs.Cms.Core.Tests
{
    public class RenderStrategyTests
    {
        [Fact]
        public void CanRenderSimpleTemplate()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown(), new HtmlContentFilter());
            var result = renderer.Render(@"hello {{variable}}", new { variable = "world" });
            Assert.True(result == "hello world");
        }

        [Fact]
        public void CanRenderSimpleTemplateWithMarkdown()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown(), new HtmlContentFilter());
            var result = renderer.Render(@"**hello {{variable}}**", new { variable = "world" });
            Assert.True(result == "<strong>hello world</strong>");
        }

        [Fact]
        public void CanRenderTemplateUsingDictionary()
        {
            var fields = new Dictionary<string, object>();
            fields.Add("boolean", true);
            fields.Add("double", 4.25);
            fields.Add("hello", "hello");
            var renderer = new RenderStrategy(new Liquid(), new Markdown(), new HtmlContentFilter());
            var result = renderer.Render(@"**{{hello}}**
€ {{double}} 
{% if boolean %}
  world!
{% endif %}
", fields);
            Assert.True(result == "<strong>hello</strong> € 4.25 world!");
        }
    }
}
