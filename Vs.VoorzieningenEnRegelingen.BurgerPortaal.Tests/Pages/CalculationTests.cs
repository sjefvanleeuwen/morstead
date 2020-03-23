using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Pages;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Pages
{
    public class CalculationTests : BlazorTestBase
    {
        //todo activate after texts have been restored
        //[Fact]
        public void HasElements()
        {
            //make sure elements are rendered
            //right now it is redered occording to the zorgtoeslagyaml 4
            var component = _host.AddComponent<Calculation>();
            //Assert.NotNull(component.Find("div.content-background")); //find the outer layout item
            Assert.Equal("#document", component.Find("div").ParentNode.Name); //find the wrapper
            var wrapper = component.Find("div");
            Assert.Equal(5, wrapper.Elements().ToList().Count);
            //check the CalculationHeader is the first 3 elements
            Assert.Equal("h1", wrapper.Elements().ToList()[0].Name);
            Assert.Equal("h2", wrapper.Elements().ToList()[1].Name);
            Assert.Equal("h3", wrapper.Elements().ToList()[2].Name);
            Assert.Equal("aside", wrapper.Elements().ToList()[3].Name); //check the hint is the 4th
            Assert.Equal("form", wrapper.Elements().ToList()[4].Name); //check the form is the last

            Assert.NotNull(component.Find("div > form > div")); //should have a div inside
            Assert.NotNull(component.Find("div > form > div select")); //div should have a select somewhere
            Assert.True(component.FindAll("div > form > div select > option").Count > 0); //div should have multiple options somewhere
            Assert.NotNull(component.FindAll("div > form > div nav")); //div should have navigation inside
        }
    }
}
