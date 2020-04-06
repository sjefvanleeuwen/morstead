using Microsoft.AspNetCore.Components.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using Vs.Core.Web.Extensions;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Enum;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements.Interfaces;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Shared.Components.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Shared.Components.FormElements
{
    public class DateRangeTests : BlazorTestBase
    {
        [Fact]
        public void DateRangeEmpty()
        {
            var variables = new Dictionary<string, object> { { "Data", new DateRangeFormElementData() } };
            var component = _host.AddComponent<DateRange>(variables);

            var labels = component.FindAll("div > div > label").ToList();
            var inputs = component.FindAll("input").ToList();
            var date = DateTime.Today;

            Assert.Equal("-ds", labels[0].Attr("for"));
            Assert.Empty(labels[0].InnerHtml);
            Assert.Equal(date.Day.ToString(), inputs[0].Attr("value"));
            Assert.Empty(inputs[0].Attr("aria-label"));
            Assert.Empty(inputs[0].Attr("title"));
            Assert.Equal("-ds", inputs[0].Id);
            Assert.False(inputs[0].IsRequired());
            Assert.False(inputs[0].IsDisabled());

            Assert.Equal("-ms", labels[1].Attr("for"));
            Assert.Empty(labels[1].InnerHtml);
            Assert.Equal(date.Month.ToString(), inputs[1].Attr("value"));
            Assert.Empty(inputs[1].Attr("aria-label"));
            Assert.Empty(inputs[1].Attr("title"));
            Assert.Equal("-ms", inputs[1].Id);
            Assert.False(inputs[1].IsRequired());
            Assert.False(inputs[1].IsDisabled());

            Assert.Equal("-ys", labels[2].Attr("for"));
            Assert.Empty(labels[2].InnerHtml);
            Assert.Equal(date.Year.ToString(), inputs[2].Attr("value"));
            Assert.Empty(inputs[2].Attr("aria-label"));
            Assert.Empty(inputs[2].Attr("title"));
            Assert.Equal("-ys", inputs[2].Id);
            Assert.False(inputs[2].IsRequired());
            Assert.False(inputs[2].IsDisabled());

            Assert.Equal("-de", labels[3].Attr("for"));
            Assert.Empty(labels[3].InnerHtml);
            Assert.Equal(date.Day.ToString(), inputs[3].Attr("value"));
            Assert.Empty(inputs[3].Attr("aria-label"));
            Assert.Empty(inputs[3].Attr("title"));
            Assert.Equal("-de", inputs[3].Id);
            Assert.False(inputs[3].IsRequired());
            Assert.False(inputs[3].IsDisabled());

            Assert.Equal("-me", labels[4].Attr("for"));
            Assert.Empty(labels[4].InnerHtml);
            Assert.Equal(date.Month.ToString(), inputs[4].Attr("value"));
            Assert.Empty(inputs[4].Attr("aria-label"));
            Assert.Empty(inputs[4].Attr("title"));
            Assert.Equal("-me", inputs[4].Id);
            Assert.False(inputs[4].IsRequired());
            Assert.False(inputs[4].IsDisabled());

            Assert.Equal("-ye", labels[5].Attr("for"));
            Assert.Empty(labels[5].InnerHtml);
            Assert.Equal(date.Year.ToString(), inputs[5].Attr("value"));
            Assert.Empty(inputs[5].Attr("aria-label"));
            Assert.Empty(inputs[5].Attr("title"));
            Assert.Equal("-ye", inputs[5].Id);
            Assert.False(inputs[5].IsRequired());
            Assert.False(inputs[5].IsDisabled());
        }

        [Fact]
        public void DateFilled()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateRangeFormElementData() {
                        IsRequired = true,
                        IsDisabled = true,
                        Name = "TheName",
                        Value = "1979-03-08 - 1988-05-09",
                        Labels = new Dictionary<string, FormElementLabel>
                        {
                            { "yearStart", new FormElementLabel { Text = "yearSText", Title = "yearSTitle"} },
                            { "monthStart", new FormElementLabel { Text = "monthSText", Title = "monthSTitle"} },
                            { "dayStart", new FormElementLabel { Text = "daySText", Title = "daySTitle"} },
                            { "yearEnd", new FormElementLabel { Text = "yearEText", Title = "yearETitle"} },
                            { "monthEnd", new FormElementLabel { Text = "monthEText", Title = "monthETitle"} },
                            { "dayEnd", new FormElementLabel { Text = "dayEText", Title = "dayETitle"} }
                        }
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);

            var labels = component.FindAll("div > div > label").ToList();
            var inputs = component.FindAll("input").ToList();

            Assert.Equal("TheName-ds", labels[0].Attr("for"));
            Assert.Equal("daySText", labels[0].InnerHtml);
            Assert.Equal("8", inputs[0].Attr("value"));
            Assert.Equal("daySTitle", inputs[0].Attr("aria-label"));
            Assert.Equal("daySTitle", inputs[0].Attr("title"));
            Assert.Equal("TheName-ds", inputs[0].Id);
            Assert.True(inputs[0].IsRequired());
            Assert.True(inputs[0].IsDisabled());

            Assert.Equal("TheName-ms", labels[1].Attr("for"));
            Assert.Equal("monthSText", labels[1].InnerHtml);
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("monthSTitle", inputs[1].Attr("aria-label"));
            Assert.Equal("monthSTitle", inputs[1].Attr("title"));
            Assert.Equal("TheName-ms", inputs[1].Id);
            Assert.True(inputs[1].IsRequired());
            Assert.True(inputs[1].IsDisabled());

            Assert.Equal("TheName-ys", labels[2].Attr("for"));
            Assert.Equal("yearSText", labels[2].InnerHtml);
            Assert.Equal("1979", inputs[2].Attr("value"));
            Assert.Equal("yearSTitle", inputs[2].Attr("aria-label"));
            Assert.Equal("yearSTitle", inputs[2].Attr("title"));
            Assert.Equal("TheName-ys", inputs[2].Id);
            Assert.True(inputs[2].IsRequired());
            Assert.True(inputs[2].IsDisabled());

            Assert.Equal("TheName-de", labels[3].Attr("for"));
            Assert.Equal("dayEText", labels[3].InnerHtml);
            Assert.Equal("9", inputs[3].Attr("value"));
            Assert.Equal("dayETitle", inputs[3].Attr("aria-label"));
            Assert.Equal("dayETitle", inputs[3].Attr("title"));
            Assert.Equal("TheName-de", inputs[3].Id);
            Assert.True(inputs[3].IsRequired());
            Assert.True(inputs[3].IsDisabled());

            Assert.Equal("TheName-me", labels[4].Attr("for"));
            Assert.Equal("monthEText", labels[4].InnerHtml);
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("monthETitle", inputs[4].Attr("aria-label"));
            Assert.Equal("monthETitle", inputs[4].Attr("title"));
            Assert.Equal("TheName-me", inputs[4].Id);
            Assert.True(inputs[4].IsRequired());
            Assert.True(inputs[4].IsDisabled());

            Assert.Equal("TheName-ye", labels[5].Attr("for"));
            Assert.Equal("yearEText", labels[5].InnerHtml);
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.Equal("yearETitle", inputs[5].Attr("aria-label"));
            Assert.Equal("yearETitle", inputs[5].Attr("title"));
            Assert.Equal("TheName-ye", inputs[5].Id);
            Assert.True(inputs[5].IsRequired());
            Assert.True(inputs[5].IsDisabled());
        }

        [Fact]
        public void ShouldDoTwoWayBinding()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateRangeFormElementData() {
                        Value = "1944-03-11 - 1988-05-09"
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);

            //the ValueDate should not be changed before the get is called

            var inputs = component.FindAll("input").ToList();
            Assert.Equal("11", inputs[0].Attr("value"));
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("1944", inputs[2].Attr("value"));
            Assert.Equal("9", inputs[3].Attr("value"));
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("11-3-1944 - 9-5-1988", component.Instance.Value);

            inputs[0].Change("1");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("3", inputs[1].Attr("value"));
            Assert.Equal("1944", inputs[2].Attr("value"));
            Assert.Equal("9", inputs[3].Attr("value"));
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("1-3-1944 - 9-5-1988", component.Instance.Value);

            inputs[1].Change("8");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1944", inputs[2].Attr("value"));
            Assert.Equal("9", inputs[3].Attr("value"));
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("1-8-1944 - 9-5-1988", component.Instance.Value);

            inputs[2].Change("1949");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1949", inputs[2].Attr("value"));
            Assert.Equal("9", inputs[3].Attr("value"));
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("1-8-1949 - 9-5-1988", component.Instance.Value);

            inputs[3].Change("21");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1949", inputs[2].Attr("value"));
            Assert.Equal("21", inputs[3].Attr("value"));
            Assert.Equal("5", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("1-8-1949 - 21-5-1988", component.Instance.Value);

            inputs[4].Change("11");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1949", inputs[2].Attr("value"));
            Assert.Equal("21", inputs[3].Attr("value"));
            Assert.Equal("11", inputs[4].Attr("value"));
            Assert.Equal("1988", inputs[5].Attr("value"));
            Assert.StartsWith("1-8-1949 - 21-11-1988", component.Instance.Value);

            inputs[5].Change("1980");
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            Assert.Equal("8", inputs[1].Attr("value"));
            Assert.Equal("1949", inputs[2].Attr("value"));
            Assert.Equal("21", inputs[3].Attr("value"));
            Assert.Equal("11", inputs[4].Attr("value"));
            Assert.Equal("1980", inputs[5].Attr("value"));
            Assert.StartsWith("1-8-1949 - 21-11-1980", component.Instance.Value);
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
                    "Data", new DateRangeFormElementData() {
                        Value = "1979-03-08 - 1988-05-09"
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);

            //the ValueDate is always changed on valid input

            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("01");
            Assert.Equal("01", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "day"]);
            Assert.Equal(new DateTime(1979, 03, 01), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[1].Change("08");
            Assert.Equal("08", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "month"]);
            Assert.Equal(new DateTime(1979, 08, 01), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[2].Change("1949");
            Assert.Equal("1949", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "year"]);
            Assert.Equal(new DateTime(1949, 08, 01), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[3].Change("21");
            Assert.Equal("21", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "day"]);
            Assert.Equal(new DateTime(1988, 05, 21), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            inputs = component.FindAll("input").ToList();
            inputs[4].Change("11");
            Assert.Equal("11", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "month"]);
            Assert.Equal(new DateTime(1988, 11, 21), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            inputs = component.FindAll("input").ToList();
            inputs[5].Change("1980");
            Assert.Equal("1980", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "year"]);
            Assert.Equal(new DateTime(1980, 11, 21), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1949, 08, 01), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            Assert.Equal(new DateTime(1980, 11, 21), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
        }

        [Fact]
        public void ShouldDoTwoWayBindingInvalidInput()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateRangeFormElementData() {
                        Value = "1979-03-08 - 1988-05-09"
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);

            //the ValueDates should not be changed when wrong data is entered

            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            Assert.Equal(new DateTime(1988, 05, 09), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("Ma");
            Assert.Equal("Ma", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "day"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[1].Change("ma");
            Assert.Equal("ma", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "month"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[2].Change("love");
            Assert.Equal("love", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "year"]);
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            inputs = component.FindAll("input").ToList();
            inputs[3].Change("Pa");
            Assert.Equal("Pa", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "day"]);
            Assert.Equal(new DateTime(1988, 05, 09), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            inputs = component.FindAll("input").ToList();
            inputs[4].Change("ma");
            Assert.Equal("ma", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "month"]);
            Assert.Equal(new DateTime(1988, 05, 09), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            inputs = component.FindAll("input").ToList();
            inputs[5].Change("hart");
            Assert.Equal("hart", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "year"]);
            Assert.Equal(new DateTime(1988, 05, 09), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);

            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            Assert.Equal(new DateTime(1988, 05, 09), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
        }

        [Fact]
        public void ShouldDoTwoWayBindingNoLeadingZeros()
        {
            var variables = new Dictionary<string, object>
            {
                {
                    "Data", new DateRangeFormElementData() {
                        Value = "1979-03-08 - 1988-05-21"
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);

            //when typing a value the value should not get a leading 0
            Assert.Equal(new DateTime(1979, 03, 08), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);
            var inputs = component.FindAll("input").ToList();
            inputs[0].Change("1");
            Assert.Equal("1", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.Start + "day"]);
            inputs = component.FindAll("input").ToList();
            Assert.Equal("1", inputs[0].Attr("value"));
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1979, 03, 01), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.Start]);

            Assert.Equal(new DateTime(1988, 05, 21), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
            inputs = component.FindAll("input").ToList();
            inputs[3].Change("9");
            Assert.Equal("9", (component.Instance.Data as IDateRangeFormElementData).Values[DateRangeType.End + "day"]);
            inputs = component.FindAll("input").ToList();
            Assert.Equal("9", inputs[3].Attr("value"));
            component.Instance.Data.Validate();
            Assert.Equal(new DateTime(1988, 05, 9), (component.Instance.Data as IDateRangeFormElementData).ValueDates[DateRangeType.End]);
        }

        [Fact]
        public void HasDressingElements()
        {
            //make sure elements are rendered
            var variables = new Dictionary<string, object> {
                {
                    "Data", new DateRangeFormElementData() {
                        Label = "_",
                        HintText = "_" ,
                        ErrorTexts = new List<string> { "_" },
                        IsValid = false
                    }
                }
            };
            var component = _host.AddComponent<DateRange>(variables);
            Assert.Equal(6, component.FindAll("div > div > div > div > input").Count); //6 input
            Assert.Equal(6, component.FindAll("div > div > div > div > label").Count); //6 label present
            Assert.Equal(7, component.FindAll("div > label").Count); //6 labels and 1 top label present
            Assert.Equal("span", component.Find("div > label").NextElement().Name); //label followed by a hinttext
            Assert.Equal("div", component.Find("div > label + span").NextElement().Name); //label followed by error
            Assert.Equal("div", component.Find("div > label + span + div").NextElement().Name); //error followed by the group div
            Assert.Equal("input__control-group", component.Find("div > label + span + div").NextElement().Attr("class")); //check div class
        }

        [Fact]
        public void ShouldHaveInput()
        {
            var sut = new DateRange();
            Assert.True(sut.HasInput);
        }
    }
}