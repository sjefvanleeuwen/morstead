using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public class Markdown : IMarkupLanguage
    {
        public string Render(string content)
        {
            return Markdig.Markdown.ToHtml(content);
        }
    }
}