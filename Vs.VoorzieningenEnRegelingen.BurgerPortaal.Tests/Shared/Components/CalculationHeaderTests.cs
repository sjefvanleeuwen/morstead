namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components
{
    public class CalculationHeaderTests : BlazorTestBase
    {
        //[Fact]
        //public void CalculationHeaderEmpty()
        //{
        //    var component = _host.AddComponent<CalculationHeader>();
        //    Assert.Equal("strong", component.Find("h1").ChildNodes[0].Name);
        //    Assert.Empty(component.Find("h1 > strong").InnerHtml);
        //    Assert.Empty(component.Find("h2").InnerHtml);
        //    Assert.NotNull(component.Find("h3 > strong"));
        //    Assert.True(string.IsNullOrWhiteSpace(component.Find("h3 > strong").InnerHtml));
        //}

        //[Fact]
        //public void CalculationHeaderFilled()
        //{
        //    var variables = new Dictionary<string, object> {
        //        { "Title", "This is the Title" },
        //        { "SubTitle", "Sub sub Sub" },
        //        { "Number", 5 },
        //        { "Subject", "Sub ject Sub" }
        //    };
        //    var component = _host.AddComponent<CalculationHeader>(variables);
        //    Assert.Equal("strong", component.Find("h1").ChildNodes[0].Name);
        //    Assert.Equal("This is the Title", component.Find("h1 > strong").InnerHtml);
        //    Assert.Equal("Sub sub Sub", component.Find("h2").InnerHtml);
        //    Assert.Contains("strong", component.Find("h3").ChildNodes.Select(n => n.Name));
        //    Assert.NotNull(component.Find("h3 > strong"));
        //    Assert.Contains("Sub ject Sub", component.Find("h3 > strong").InnerHtml);
        //}

        //[Fact]
        //public void CalculationHeaderFilledWithMarkup()
        //{
        //    var variables = new Dictionary<string, object> {
        //        { "Title", "This is <u>the Title</u>" },
        //        { "SubTitle", "Sub<i> sub </i>Sub" },
        //        { "Subject", "Sub<span> ject</span> Sub" }
        //    };
        //    var component = _host.AddComponent<CalculationHeader>(variables);
        //    Assert.Equal("This is <u>the Title</u>", component.Find("h1 > strong").InnerHtml);
        //    Assert.Equal("the Title", component.Find("h1 > strong > u").InnerHtml);
        //    Assert.Equal("Sub<i> sub </i>Sub", component.Find("h2").InnerHtml);
        //    Assert.Equal(" sub ", component.Find("h2 > i").InnerHtml);
        //    Assert.NotNull(component.Find("h3 > strong > span"));
        //    Assert.Contains(" ject", component.Find("h3 > strong > span").InnerHtml);
        //}
    }
}
