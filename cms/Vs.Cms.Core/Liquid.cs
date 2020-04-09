using Scriban;
using Scriban.Syntax;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<string> GetExpressionNames(string template)
        {
            var result = new List<string>();

            var t = Template.Parse(template);
            var expressions = t.Page.Body.Statements.Where(p => p is ScriptExpressionStatement);
            foreach (var expression in expressions)
            {
                result.Add((expression as ScriptExpressionStatement).Expression.ToString());
            }
            return result;
        }
    }
}