using AngleSharp.Dom;
using System.Linq;

namespace Vs.BurgerPortaal.Core.Tests._Helper.Extensions
{
    public static class AngleSharpDomIElementExtensions
    {
        public static string Attr(this IElement element, string attributeName)
        {
            return element?.Attributes?.GetNamedItem(attributeName)?.Value;
        }

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
    }
}
