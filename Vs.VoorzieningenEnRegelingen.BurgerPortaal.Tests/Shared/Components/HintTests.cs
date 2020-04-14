using System.Collections.Generic;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class HintTests : BlazorTestBase
    {
        //[Fact]
        //public void HintEmpty()
        //{
        //    var component = _host.AddComponent<Hint>();
        //    Assert.Empty(component.Find("aside > h4").InnerHtml);
        //    //alle elements are empty
        //    Assert.Empty(component.Find("aside").ChildNodes.Where(n => !string.IsNullOrWhiteSpace(n.InnerHtml)));
        //}

        //[Fact]
        //public void HintFilled()
        //{
        //    var variables = new Dictionary<string, object> {
        //        { "Title", "This is the Title" },
        //        { "Content", "The content" }
        //    };
        //    var component = _host.AddComponent<Hint>(variables);
        //    Assert.Equal("This is the Title", component.Find("aside > h4").InnerHtml);
        //    Assert.Contains("The content", component.GetMarkup());
        //}

        //[Fact]
        //public void HintFilledWithMarkup()
        //{
        //    var variables = new Dictionary<string, object> {
        //        { "Title", "<i>This is the </i>Title" },
        //        { "Content", "The <u>content</u>" }
        //    };
        //    var component = _host.AddComponent<Hint>(variables);
        //    Assert.Equal("<i>This is the </i>Title", component.Find("aside > h4").InnerHtml);
        //    Assert.Equal("This is the ", component.Find("aside > h4 > i").InnerHtml);
        //    Assert.Contains("The <u>content</u>", component.GetMarkup());
        //    Assert.Contains("content", component.Find("u").InnerHtml);
        //}
    }
}
