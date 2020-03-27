using Microsoft.AspNetCore.Components.Testing;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class SelectTests : BlazorTestBase
    {
        [Fact]
        public void SelectEmpty()
        {
            var variables = new Dictionary<string, object> { { "Data", new ListFormElementData() } };
            var component = _host.AddComponent<Select>(variables);
            //no elementes
            var selects = component.FindAll("select");
            Assert.Single(selects);
            Assert.Empty(component.FindAll("option"));
            var select = selects.First();
            Assert.False(select.IsRequired());
            Assert.False(select.IsDisabled());
            Assert.Empty(select.Id);
            Assert.Equal("hint_", select.Attr("aria-describedby"));
            Assert.Equal("input__control input__control--select ", select.Attr("class"));
            Assert.Empty(select.Attr("value"));
        }

        [Fact]
        public void SelectFilled()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new ListFormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "ListOptionA" },
                            { "B", "ListOptionB" },
                            { "C", "ListOptionC" },
                            { "D", "ListOptionD" }
                        },
                        IsRequired = true,
                        IsDisabled = true,
                        Name = "TheName",
                        Size = FormElementSize.Large
                    }
                }
            };
            var component = _host.AddComponent<Select>(variables);
            //no elementes
            var selects = component.FindAll("select");
            Assert.Single(selects);
            var select = selects.First();
            Assert.True(select.IsRequired());
            Assert.True(select.IsDisabled());
            Assert.Equal("TheName", select.Id);
            Assert.Equal("hint_TheName", select.Attr("aria-describedby"));
            Assert.Equal("input__control input__control--select input__control--l", select.Attr("class"));

            var options = component.FindAll("option").ToList();
            Assert.Equal(4, options.Count());
            Assert.Equal(options[0].Attr("value"), component.Instance.Value);
            Assert.Equal(3, options.Count(o => !o.IsSelected()));
            Assert.Equal("A", options[0].Attr("value"));
            Assert.Equal("B", options[1].Attr("value"));
            Assert.Equal("C", options[2].Attr("value"));
            Assert.Equal("D", options[3].Attr("value"));
            Assert.Equal("ListOptionA", options[0].InnerHtml);
            Assert.Equal("ListOptionB", options[1].InnerHtml);
            Assert.Equal("ListOptionC", options[2].InnerHtml);
            Assert.Equal("ListOptionD", options[3].InnerHtml);
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new ListFormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "ListOptionA" },
                            { "B", "ListOptionB" },
                            { "C", "ListOptionC" },
                            { "D", "ListOptionD" }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Select>(variables);
            Assert.Equal("A", component.Instance.Value);
            Assert.Equal("ListOptionA", component.FindAll("option").FirstOrDefault(o => o.IsSelected()).InnerHtml);
            component.Find("select").Change("B");
            Assert.Equal("B", component.Instance.Value);
            Assert.Equal("ListOptionB", component.FindAll("option").FirstOrDefault(o => o.IsSelected()).InnerHtml);
            component.Find("select").Change("D");
            Assert.Equal("D", component.Instance.Value);
            Assert.Equal("ListOptionD", component.FindAll("option").FirstOrDefault(o => o.IsSelected()).InnerHtml);
            component.Find("select").Change("A");
            Assert.Equal("A", component.Instance.Value);
            Assert.Equal("ListOptionA", component.FindAll("option").FirstOrDefault(o => o.IsSelected()).InnerHtml);
        }

        [Fact]
        public void HasDressingElements()
        {
            //make sure elements are rendered
            var variables = new Dictionary<string, object> {
                {
                    "Data", new ListFormElementData() {
                        Label = "_",
                        HintText = "_",
                        ErrorTexts = new List<string> { "_" },
                        IsValid = false
                    }
                }
            };
            var component = _host.AddComponent<Select>(variables);
            Assert.NotNull(component.Find("div > select")); //it is contained in a wrapper
            Assert.Single(component.FindAll("div > select")); //only 1 input
            Assert.NotNull(component.Find("div > label")); //label present
            Assert.Equal("span", component.Find("div > label").NextElement().Name); //label followed by a hinttext
            Assert.Equal("div", component.Find("div > label + span").NextElement().Name); //label followed by error
            Assert.Equal("select", component.Find("div > label + span + div").NextElement().Name); //error followed by the one and only select
        }
    }
}