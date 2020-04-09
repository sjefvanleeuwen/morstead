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
            Assert.Equal("hello world", result);
        }

        [Fact]
        public void CanRenderSimpleTemplateWithMarkdown()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown(), new HtmlContentFilter());
            var result = renderer.Render(@"**hello {{variable}}**", new { variable = "world" });
            Assert.Equal("<strong>hello world</strong>", result);
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
            Assert.Equal("<strong>hello</strong> € 4.25 world!", result);
        }

        [Fact]
        public void CanRenderTemplateWithNumberFormatting()
        {
            var renderer = new RenderStrategy(new Liquid(), new Markdown(), new HtmlContentFilter());
            var result = renderer.Render(@"hello {{variable | string.to_double | math.format ""N"" ""nl-NL""}}", new { variable = "1345" });
            Assert.Equal("hello 1.345,00", result);
            result = renderer.Render(@"hello {{variable | string.to_double | math.format ""N"" ""nl-NL""}}", new { variable = "1345.1" });
            Assert.Equal("hello 1.345,10", result);
        }
    }
}
