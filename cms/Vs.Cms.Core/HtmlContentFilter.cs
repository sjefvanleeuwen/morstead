using System.Text.RegularExpressions;
using Vs.Cms.Core.Interfaces;

namespace Vs.Cms.Core
{
    public class HtmlContentFilter : IContentFilter
    {
        public string Filter(string content)
        {
            return new Regex(@"\s+").Replace(new Regex(@"<p>|</p>|\n").Replace(content, " ").Trim()," ");
        }
    }
}
