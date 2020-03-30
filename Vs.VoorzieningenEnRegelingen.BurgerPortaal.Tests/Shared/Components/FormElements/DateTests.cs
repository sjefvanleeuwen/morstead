using Microsoft.AspNetCore.Components.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Web;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class DateTests : BlazorTestBase
    {
        [Fact]
        public void DateEmpty()
        {
            var variables = new Dictionary<string, object> { { "Data", new DateFormElementData() } };
            var component = _host.AddComponent<Date>(variables);

            var labels = component.FindAll("div > div > label").ToList();
            var inputs = component.FindAll("input").ToList();
            var date = DateTime.Today;

            Assert.Equal("-d", labels[0].Attr("for"));
            Assert.Empty(labels[0].InnerHtml);
            Assert.Equal(date.Day.ToString(), inputs[0].Attr("value"));
            Assert.Empty(inputs[0].Attr("aria-label"));
            Assert.Empty(inputs[0].Attr("title"));
            Assert.Equal("-d", inputs[0].Id);
            Assert.False(inputs[0].IsRequired());
            Assert.False(inputs[0].IsDisabled());

            Assert.Equal("-m", labels[1].Attr("for"));
            Assert.Empty(labels[1].InnerHtml);
            Assert.Equal(date.Month.ToString(), inputs[1].Attr("value"));
            Assert.Empty(inputs[1].Attr("aria-label"));
            Assert.Empty(inputs[1].Attr("title"));
            Assert.Equal("-m", inputs[1].Id);
            Assert.False(inputs[1].IsRequired());
            Assert.False(inputs[1].IsDisabled());

            Assert.Equal("-y", labels[2].Attr("for"));
            Assert.Empty(labels[2].InnerHtml);
            Assert.Equal(date.Year.ToString(), inputs[2].Attr("value"));
            Assert.Empty(inputs[2].Attr("aria-label"));
            Assert.Empty(inputs[2].Attr("title"));
            Assert.Equal("-y", inputs[2].Id);
            Assert.False(inputs[2].IsRequired());
            Assert.False(inputs[2].IsDisabled());
        }

        [Fact]
        public void DateFilled()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateFormElementData() {
                        IsRequired = true,
                        IsDisabled = true,
                        Name = "TheName",
                        Value = "1979-03-08",
                        Labels = new Dictionary<string, FormElementLabel>
                        {
                            { "year", new FormElementLabel { Text = "yearText", Title = "yearTitle"} },
                            { "month", new FormElementLabel { Text = "monthText", Title = "monthTitle"} },
                            { "day", new FormElementLabel { Text = "dayText", Title = "dayTitle"} }
                        }
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);

            var labels = component.FindAll("div > div > label").ToList();
            var inputs = component.FindAll("input").ToList();

            Assert.Equal("TheName-d", labels[0].Attr("for"));
            Assert.Equal("dayText", labels[0].InnerHtml);
            Assert.Equal("8", inputs[0].Attr("value"));
            Assert.Equal("dayTitle", inputs[0].Attr("aria-label"));
            Assert.Equal("dayTitle", inputs[0].Attr("title"));
            Assert.Equal("TheName-d", inputs[0].Id);
            Assert.True(inputs[0].IsRequired());
            Assert.True(inputs[0].IsDisabled());

            Assert.Equal("TheName-m", labels[1].Attr("for"));
            Assert.Equal("monthText", labels[1].InnerHtml);
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("monthTitle", inputs[1].Attr("aria-label"));
            Assert.Equal("monthTitle", inputs[1].Attr("title"));
            Assert.Equal("TheName-m", inputs[1].Id);
            Assert.True(inputs[1].IsRequired());
            Assert.True(inputs[1].IsDisabled());

            Assert.Equal("TheName-y", labels[2].Attr("for"));
            Assert.Equal("yearText", labels[2].InnerHtml);
            Assert.Equal("1979", inputs[2].Attr("value"));
            Assert.Equal("yearTitle", inputs[2].Attr("aria-label"));
            Assert.Equal("yearTitle", inputs[2].Attr("title"));
            Assert.Equal("TheName-y", inputs[2].Id);
            Assert.True(inputs[2].IsRequired());
            Assert.True(inputs[2].IsDisabled());
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateFormElementData() {
                        Value = "1979-03-08"
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);

            //the ValueDate should not be changed before the get is called

            var inputs = component.FindAll("input").ToList();
            Assert.Equal("8", inputs[0].Attr("value"));
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("1979", inputs[2].Attr("value"));
            Assert.Equal("1979-03-08", component.Instance.Value);

            inputs[0].Change("01");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("1979", inputs[2].Attr("value"));
            Assert.Equal("1979-03-01", component.Instance.Value);

            inputs[1].Change("08");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1979", inputs[2].Attr("value"));
            Assert.Equal("1979-08-01", component.Instance.Value);

            inputs[2].Change("1949");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1949", inputs[2].Attr("value"));
            Assert.Equal("1949-08-01", component.Instance.Value);
        }

        /// <summary>
        /// Should never parse a value when changed
        /// </summary>
        [Fact]
        public void ShouldDoTwoWayBindingImmediatelyParse()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateFormElementData() {
                        Value = "1979-03-08"
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);

            //the ValueDate is always changed on valid input

            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("01");
            Assert.Equal("01", (component.Instance.Data as IDateFormElementData).Values["day"]);
            Assert.Equal(new DateTime(1979, 03, 01), (component.Instance.Data as IDateFormElementData).ValueDate);
            inputs = component.FindAll("input").ToList();
            inputs[1].Change("08");
            Assert.Equal("08", (component.Instance.Data as IDateFormElementData).Values["month"]);
            Assert.Equal(new DateTime(1979, 08, 01), (component.Instance.Data as IDateFormElementData).ValueDate);
            inputs = component.FindAll("input").ToList();
            inputs[2].Change("1949");
            Assert.Equal("1949", (component.Instance.Data as IDateFormElementData).Values["year"]);
            Assert.Equal(new DateTime(1949, 08, 01), (component.Instance.Data as IDateFormElementData).ValueDate);
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1949, 08, 01), (component.Instance.Data as IDateFormElementData).ValueDate);
        }

        [Fact]
        public void ShouldDoTwoWayBindingInvalidInput()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateFormElementData() {
                        Value = "1979-03-08"
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);

            //the ValueDate should not be changed when wrong data is entered

            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("Ma");
            Assert.Equal("Ma", (component.Instance.Data as IDateFormElementData).Values["day"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            inputs = component.FindAll("input").ToList();
            inputs[1].Change("ma");
            Assert.Equal("ma", (component.Instance.Data as IDateFormElementData).Values["month"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            inputs = component.FindAll("input").ToList();
            inputs[2].Change("love");
            Assert.Equal("love", (component.Instance.Data as IDateFormElementData).Values["year"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
        }

        [Fact]
        public void ShouldDoTwoWayBindingNoLeadingZeros()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateFormElementData() {
                        Value = "1979-03-08"
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);

            //when typing a value the value should not get a leading 0
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateFormElementData).ValueDate);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("1");
            Assert.Equal("1", (component.Instance.Data as IDateFormElementData).Values["day"]);
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1979, 03, 01), (component.Instance.Data as IDateFormElementData).ValueDate);
        }

        [Fact]
        public void HasDressingElements()
        {
            //make sure elements are rendered
            var variables = new Dictionary<string, object> {
                {
                    "Data", new DateFormElementData() {
                        Label = "_",
                        HintText = "_" ,
                        ErrorTexts = new List<string> { "_" },
                        IsValid = false
                    }
                }
            };
            var component = _host.AddComponent<Date>(variables);
            Assert.Equal(3, component.FindAll("div > div > div > input").Count); //3 input
            Assert.Equal(3, component.FindAll("div > div > div > label").Count); //3 label present
            Assert.Equal(4, component.FindAll("div > label").Count); //3 labels and 1 top label present
            Assert.Equal("span", component.Find("div > label").NextElement().Name); //label followed by a hinttext
            Assert.Equal("div", component.Find("div > label + span").NextElement().Name); //label followed by error
            Assert.Equal("div", component.Find("div > label + span + div").NextElement().Name); //error followed by the group div
            Assert.Equal("input__control-group", component.Find("div > label + span + div").NextElement().Attr("class")); //check div class
        }
    }
}