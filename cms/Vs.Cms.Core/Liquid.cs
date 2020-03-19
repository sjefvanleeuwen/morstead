using Scriban;
using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public class Liquid : ITemplateEngine
    {
        public string Render(string template, dynamic model)
        {
            var t = Template.ParseLiquid(template);
            return t.Render(model);
        }
    }
}