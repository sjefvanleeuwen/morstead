using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Vs.Core.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static string Attr(this HtmlNode node, string attributeName)
        {
            return node.GetAttributeValue<string>(attributeName, "");
        }

        public static IList<HtmlNode> Elements(this HtmlNode node)
        {
            return node.ChildNodes.Where(n => n.Name != "#text").ToList();
        }

        public static IList<HtmlNode> Elements(this HtmlNode node, string selector)
        {
            return node.ChildNodes.Where(n => n.Name == selector).ToList();
        }

        public static bool IsEmpty(this HtmlNode node)
        {
            return string.IsNullOrWhiteSpace(node.InnerHtml);
        }
        
    }
}
