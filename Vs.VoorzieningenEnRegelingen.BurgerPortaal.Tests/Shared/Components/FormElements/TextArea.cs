using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class TextAreaTests : BlazorTestBase
    {
        //[Fact]
        //public void TextEmpty()
        //{
        //    var variables = new Dictionary<string, object> { { "Data", new TextFormElementData() } };
        //    var component = _host.AddComponent<TextArea>(variables);
        //    Assert.NotNull(component.Find("textarea"));
        //    Assert.False(component.Find("textarea").IsRequired());
        //    Assert.False(component.Find("textarea").IsDisabled());
        //    Assert.Empty(component.Find("textarea").Id);
        //    Assert.Equal("hint_", component.Find("textarea").Attr("aria-describedby"));
        //    Assert.Equal("input__control input__control--text ", component.Find("textarea").Attr("class"));
        //    Assert.Empty(component.Find("textarea").Attr("value"));
        //}

        //[Fact]
        //public void TextFilled()
        //{
        //    var variables = new Dictionary<string, object> {
        //        {
        //            "Data", new TextFormElementData() {
        //                IsRequired = true,
        //                IsDisabled = true,
        //                Name = "TheName",
        //                Size = FormElementSize.Large,
        //                Value = "123"
        //            }
        //        }
        //    };

        //    var component = _host.AddComponent<TextArea>(variables);
        //    Assert.NotNull(component.Find("textarea"));
        //    Assert.True(component.Find("textarea").IsRequired());
        //    Assert.True(component.Find("textarea").IsDisabled());
        //    Assert.Equal("TheName", component.Find("textarea").Id);
        //    Assert.Equal("hint_TheName", component.Find("textarea").Attr("aria-describedby"));
        //    Assert.Equal("input__control input__control--text input__control--l", component.Find("textarea").Attr("class"));
        //    Assert.Equal("123", component.Find("textarea").Attr("value"));
        //}

        //[Fact]
        //public void ShouldDoTwoWayBinding()
        //{
        //    var variables = new Dictionary<string, object> {
        //        {
        //            "Data", new TextFormElementData() {
        //                Name = "TheName",
        //                Size = FormElementSize.Large,
        //                Value = "123"
        //            }
        //        }
        //    };
        //    var component = _host.AddComponent<TextArea>(variables);
        //    Assert.Equal("123", component.Find("textarea").Attr("value"));

        //    var element = component.Find("textarea");
        //    element.Change("345");
        //    Assert.Equal("345", component.Find("textarea").Attr("value"));

        //    Assert.Equal("345", component.Instance.Value);
        //}

        //[Fact]
        //public void HasDressingElements()
        //{
        //    //make sure elements are rendered
        //    var variables = new Dictionary<string, object> {
        //        {
        //            "Data", new TextFormElementData() {
        //                Label = "_",
        //                HintText = "_" ,
        //                ErrorTexts = new List<string> { "_" },
        //                IsValid = false
        //            }
        //        }
        //    };
        //    var component = _host.AddComponent<TextArea>(variables);
        //    Assert.NotNull(component.Find("div > textarea")); //it is contained in a wrapper
        //    Assert.Single(component.FindAll("div > textarea")); //only 1 input
        //    Assert.NotNull(component.Find("div > label")); //label present
        //    Assert.Equal("span", component.Find("div > label").NextElement().Name); //label followed by a hinttext
        //    Assert.Equal("div", component.Find("div > label + span").NextElement().Name); //label followed by error
        //    Assert.Equal("textarea", component.Find("div > label + span + div").NextElement().Name); //error followed by the one and only input
        //}

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new Text();
            Assert.True(sut.HasInput);
        }
    }
}