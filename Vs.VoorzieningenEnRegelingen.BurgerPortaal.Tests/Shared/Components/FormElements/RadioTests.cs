using System.Collections.Generic;
using Vs.Core.Extensions;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Microsoft.AspNetCore.Components.Testing;
using System.Linq;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class RadioTests : BlazorTestBase
    {
        [Fact]
        public void RadioEmpty()
        {
            var component = _host.AddComponent<Radio>();
            //no elementes
            Assert.Null(component.Find("div > div"));
            Assert.Single(component.FindAll("div")); ;
            Assert.Empty(component.Find("div").Attr("class"));
        }

        [Fact]
        public void RadioOptionsClass()
        {
            //only 2 lower than 10
            var variables = new Dictionary<string, object> {
                { "Options", new Dictionary<string, string>
                    {
                        { "A", "<than10" },
                        { "B", "and_only_2" }
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.Equal("input__group-horizontal", component.Find("div").Attr("class"));

            //lower than 10 in length, but 3
            variables = new Dictionary<string, object> {
                { "Options", new Dictionary<string, string>
                    {
                        { "A", "Thisisshor" },
                        { "B", "terthan10c" },
                        { "C", "haracters" }
                    }
                }
            };
            component = _host.AddComponent<Radio>(variables);
            Assert.Empty(component.Find("div").Attr("class"));


            variables = new Dictionary<string, object> {
                { "Options", new Dictionary<string, string>
                    {
                        { "A", ">than10" },
                        { "B", "and_onlytwo" }
                    }
                }
            };
            component = _host.AddComponent<Radio>(variables);
            Assert.Empty(component.Find("div").Attr("class"));
        }


        [Fact]
        public void RadioFilledTwoOptions()
        {
            var variables = new Dictionary<string, object> {
                { "Options", new Dictionary<string, string>
                    {
                        { "A", "ValueA" },
                        { "B", "ValueB" }
                    } },
                { "IsDisabled", true },
                { "Name", "TheName" },
                { "Size", FormElementSize.Large },
                { "Value", "123" }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.NotNull(component.Find("div > div"));
            Assert.Equal(2, component.FindAll("div > div").Count);
            var radios = component.FindAll("div > div").ToList();
            var opt1 = radios[0];
            Assert.Equal(2, opt1.Elements().Count);
            Assert.Equal("input", opt1.Elements()[0].Name);
            var input1 = opt1.Elements()[0];

            Assert.Equal("label", opt1.Elements()[1].Name);
            var label1 = opt1.Elements()[1];

            var opt2 = radios[1];



            Assert.NotNull(component.Find("input"));
            Assert.True(component.Instance.IsRequired);
            Assert.True(component.Instance.IsDisabled);
            //there is no difference in the component at this pont regarding 
            //the rendering of required="@Required" and disabled="@Disabled" True vs False
            Assert.Empty(component.Find("input").Attr("required"));
            Assert.Empty(component.Find("input").Attr("disabled"));
            Assert.Equal("TheName", component.Find("input").Attr("id"));
            Assert.Equal("hint_TheName", component.Find("input").Attr("aria-describedby"));
            Assert.Equal("input__control input__control--text input__control--l", component.Find("input").Attr("class"));
            Assert.Equal("123", component.Find("input").Attr("value"));
        }

        [Fact]
        public void RadioTwoWayBindTest()
        {
            var variables = new Dictionary<string, object> {
                { "Name", "TheName" },
                { "Size", FormElementSize.Large },
                { "Value", "123" }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.Equal("123", component.Find("input").Attr("value"));

            var element = component.Find("input");
            element.Change("345");
            Assert.Equal("345", component.Find("input").Attr("value"));

            Assert.Equal("345", component.Instance.Value);
        }
    }
}