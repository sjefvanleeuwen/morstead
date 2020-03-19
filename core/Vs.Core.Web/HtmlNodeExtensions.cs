using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Vs.Core.Web
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

        public static HtmlNode NextElement(this HtmlNode node)
        {
            HtmlNode result = null;
            while (result == null)
            {
                var sibling = node.NextSibling;
                if (sibling == null)
                {
                    break;
                }
                if (sibling.Name != "#text")
                {
                    result = sibling;
                }
                node = sibling;
            }
            return result;
        }

        public static HtmlNode NextElement(this HtmlNode node, string selector)
        {
            HtmlNode result = null;
            while (result == null)
            {
                var sibling = node.NextSibling;
                if (sibling == null)
                {
                    break;
                }
                if (sibling.Name == selector)
                {
                    result = sibling;
                }
                node = sibling;
            }
            return result;
        }

        public static bool IsEmpty(this HtmlNode node)
        {
            return string.IsNullOrWhiteSpace(node.InnerHtml);
        }

        public static bool IsChecked(this HtmlNode node)
        {
            return node.GetAttributes().Any(a => a.Name.ToLower() == "checked");
        }

        public static bool IsDisabled(this HtmlNode node)
        {
            return node.GetAttributes().Any(a => a.Name.ToLower() == "disabled");
        }

        public static bool IsRequired(this HtmlNode node)
        {
            return node.GetAttributes().Any(a => a.Name.ToLower() == "required");
        }

        public static bool IsSelected(this HtmlNode node)
        {
            return node.GetAttributes().Any(a => a.Name.ToLower() == "selected");
        }
    }
}
