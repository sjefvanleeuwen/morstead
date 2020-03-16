using HtmlAgilityPack;

namespace Vs.Core.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static string Attr(this HtmlNode node, string attributeName)
        {
            return node.GetAttributeValue<string>(attributeName, "");
        }
    }
}
