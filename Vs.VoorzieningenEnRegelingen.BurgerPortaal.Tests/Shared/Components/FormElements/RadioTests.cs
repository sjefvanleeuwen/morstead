using System.Collections.Generic;
using Xunit;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Microsoft.AspNetCore.Components.Testing;
using System.Linq;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class RadioTests : BlazorTestBase
    {
        [Fact]
        public void RadioEmpty()
        {
            var variables = new Dictionary<string, object> { { "Data", new FormElementData() } };
            var component = _host.AddComponent<Radio>(variables);
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
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "<than10" },
                            { "B", "and_only_2" }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.Equal("input__group-horizontal", component.Find("div").Attr("class"));

            //lower than 10 in length, but 3
            variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "Thisisshor" },
                            { "B", "terthan10c" },
                            { "C", "haracters" }
                        }
                    }
                }
            };
            component = _host.AddComponent<Radio>(variables);
            Assert.Empty(component.Find("div").Attr("class"));

            //longer than 10
            variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", ">than10" },
                            { "B", "and_onlytwo" }
                        }
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
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "ValueA" },
                            { "B", "ValueB" }
                        },
                        Name = "TheName",
                        Size = FormElementSize.Large
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.NotNull(component.Find("div > div"));
            Assert.Equal(2, component.FindAll("div > div").Count);
            var radios = component.FindAll("div > div").ToList();
            var opt1 = radios[0];
            Assert.Equal(2, opt1.Elements().Count);
            var input1 = opt1.Elements()[0];
            Assert.Equal("input", input1.Name);
            Assert.Equal("TheName", input1.Attr("Name"));
            Assert.Equal("TheName-radio_1", input1.Id);
            Assert.Equal("input__control input__control--radio input__control--l", input1.Attr("class"));
            Assert.Equal("", input1.Attr("checked"));
            Assert.Equal("A", input1.Attr("value"));
            var label1 = opt1.Elements()[1];
            Assert.Equal("label", label1.Name);
            Assert.Equal("TheName-radio_1", label1.Attr("for"));
            Assert.Equal("ValueA", label1.InnerHtml);

            var opt2 = radios[1];
            Assert.Equal(2, opt2.Elements().Count);
            var input2 = opt2.Elements()[0];
            Assert.Equal("input", input2.Name);
            Assert.Equal("TheName", input2.Attr("Name"));
            Assert.Equal("TheName-radio_2", input2.Id);
            Assert.Equal("input__control input__control--radio input__control--l", input2.Attr("class"));
            Assert.Equal("", input2.Attr("checked"));
            Assert.Equal("B", input2.Attr("value"));
            var label2 = opt2.Elements()[1];
            Assert.Equal("label", label2.Name);
            Assert.Equal("TheName-radio_2", label2.Attr("for"));
            Assert.Equal("ValueB", label2.InnerHtml);
        }

        [Fact]
        public void RadioValueSet()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "ValueA" },
                            { "B", "ValueB" }
                        },
                        Value = "A"
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            var inputs = component.FindAll("div > div > input").ToList();
            Assert.True(inputs[0].IsChecked());
            Assert.False(inputs[1].IsChecked());
        }

        [Fact]
        public void RadioNotSizeSet()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                            { "A", "ValueA" }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.Equal("input__control input__control--radio ", component.Find("div > div > input").Attr("class"));
        }

        [Fact]
        public void RadioFilledMarkup()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                             { "A", "Val<i>ue</i>A" },
                             { "B", "V<u>alueB</u>" }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            var labels = component.FindAll("label").ToList();
            Assert.Equal("Val<i>ue</i>A", labels[0].InnerHtml);
            Assert.Single(labels[0].Elements());
            Assert.Equal("i", labels[0].Elements()[0].Name);
            Assert.Equal("ue", labels[0].Elements()[0].InnerHtml);
            Assert.Equal("V<u>alueB</u>", labels[1].InnerHtml);
            Assert.Single(labels[1].Elements());
            Assert.Equal("u", labels[1].Elements()[0].Name);
            Assert.Equal("alueB", labels[1].Elements()[0].InnerHtml);
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                             { "A", "ValueA" },
                             { "B", "ValueB" }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Radio>(variables);
            Assert.False(component.FindAll("div > div > input").ElementAt(0).IsChecked());
            Assert.False(component.FindAll("div > div > input").ElementAt(1).IsChecked());
            Assert.Empty(component.Instance.Value);

            //click the first one
            component.FindAll("div > div > input").ElementAt(0).Click();
            Assert.True(component.FindAll("div > div > input").ElementAt(0).IsChecked());
            Assert.False(component.FindAll("div > div > input").ElementAt(1).IsChecked());
            Assert.Equal("A", component.Instance.Value);

            //click the first one again
            component.FindAll("div > div > input").ElementAt(0).Click();
            Assert.True(component.FindAll("div > div > input").ElementAt(0).IsChecked());
            Assert.False(component.FindAll("div > div > input").ElementAt(1).IsChecked());
            Assert.Equal("A", component.Instance.Value);

            //click the second one
            component.FindAll("div > div > input").ElementAt(1).Click();
            Assert.False(component.FindAll("div > div > input").ElementAt(0).IsChecked());
            Assert.True(component.FindAll("div > div > input").ElementAt(1).IsChecked());
            Assert.Equal("B", component.Instance.Value);

            //click the first one again
            component.FindAll("div > div > input").ElementAt(0).Click();
            Assert.True(component.FindAll("div > div > input").ElementAt(0).IsChecked());
            Assert.False(component.FindAll("div > div > input").ElementAt(1).IsChecked());
            Assert.Equal("A", component.Instance.Value);
        }

        [Fact]
        public void HasDressingElements()
        {
            var variables = new Dictionary<string, object> {
                {
                    "Data", new FormElementData() {
                        Options = new Dictionary<string, string> {
                             { "A", "ValueA" }
                        },
                        Label = "_",
                        HintText = "_",
                        ErrorText = "_"
                    }
                }
            };
            //make sure elements are rendered
            var component = _host.AddComponent<Radio>(variables);
            Assert.NotNull(component.Find("fieldset > div > div > input")); //it is contained in a wrapper
            Assert.Single(component.FindAll("fieldset > div > div > input")); //only 1 input
            Assert.NotNull(component.Find("fieldset > legend")); //label present
            Assert.Equal("span", component.Find("fieldset > legend").NextElement().Name); //label followed by a hinttext
            Assert.Equal("div", component.Find("fieldset > legend + span").NextElement().Name); //label followed by error
            Assert.Equal("div", component.Find("fieldset > legend + span + div").NextElement().Name); //error followed by the element
            Assert.Equal("div", component.Find("fieldset > legend + span + div + div").Elements()[0].Name); //element has a div
            Assert.Equal("input", component.Find("fieldset > legend + span + div + div").Elements()[0].Elements()[0].Name); //div has an input
        }
    }
}