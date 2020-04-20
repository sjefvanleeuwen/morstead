using AngleSharp.Dom;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests._Helper.Extensions
{
    public static class AngleSharpDomIElementExtensions
    {
        public static string Attr(this IElement element, string attributeName)
        {
            return element.Attributes.GetNamedItem(attributeName)?.Value;
        }

        //public static IList<HtmlNode> Elements(this IElement node)
        //{
        //    return node.ChildNodes.Where(n => n. .Name != "#text").ToList();
        //}

        //public static IList<HtmlNode> Elements(this IElement node, string selector)
        //{
        //    return node.ChildNodes.Where(n => n.Name == selector).ToList();
        //}

        public static IElement NextElement(this INode element)
        {
            var sibling = element.NextSibling;
            if (sibling == null)
            {
                return null;
            }
            if (sibling.NodeName != "#text")
            {
                //null if not an element, and will continue
                return sibling as IElement;
            }
            return sibling.NextElement();
        }

        public static IElement FirstChild(this INode element)
        {
            var children = element.ChildNodes.Where(n => n.NodeName != "#text" && n is IElement);
            return children.FirstOrDefault() as IElement;
        }

        //public static HtmlNode NextElement(this IElement node, string selector)
        //{
        //    HtmlNode result = null;
        //    while (result == null)
        //    {
        //        var sibling = node.NextSibling;
        //        if (sibling == null)
        //        {
        //            break;
        //        }
        //        if (sibling.Name == selector)
        //        {
        //            result = sibling;
        //        }
        //        node = sibling;
        //    }
        //    return result;
        //}

        //public static bool IsEmpty(this IElement node)
        //{
        //    return string.IsNullOrWhiteSpace(node.InnerHtml);
        //}

        //public static bool IsChecked(this IElement node)
        //{
        //    return node.GetAttributes().Any(a => a.Name.ToLower() == "checked");
        //}

        //public static bool IsDisabled(this IElement node)
        //{
        //    return node.Attributes.Any(a => a.Name.ToLower() == "disabled");
        //}

        //public static bool IsRequired(this IElement node)
        //{
        //    return node.GetAttributes().Any(a => a.Name.ToLower() == "required");
        //}

        //public static bool IsSelected(this IElement node)
        //{
        //    return node.GetAttributes().Any(a => a.Name.ToLower() == "selected");
        //}
    }
}
