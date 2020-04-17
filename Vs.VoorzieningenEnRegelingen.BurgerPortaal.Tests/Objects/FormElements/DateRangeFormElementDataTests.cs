using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class DateRangeFormElementDataTests
    {
        //[Fact]
        //public void CheckValueIsTodayWhenEmpty()
        //{
        //    var sut = new DateRangeFormElementData();
        //    Assert.Equal(DateTime.Today, sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal(DateTime.Today.Year.ToString(), sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal(DateTime.Today.Month.ToString(), sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal(DateTime.Today.Day.ToString(), sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(DateTime.Today, sut.ValueDates[DateRangeType.End]);
        //    Assert.Equal(DateTime.Today.Year.ToString(), sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal(DateTime.Today.Month.ToString(), sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal(DateTime.Today.Day.ToString(), sut.Values[DateRangeType.End + "day"]);
        //}

        [Fact]
        public void CheckValidWhenEmpty()
        {
            var sut = new DateRangeFormElementData();
            sut.CustomValidate();
            Assert.True(sut.IsValid);
        }

        //[Fact]
        //public void ShouldSetValue()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Value = "1979-03-08 00:00:00 - 2020-03-30 00:00:00"
        //    };
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal("1979", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal("8", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDates[DateRangeType.End]);
        //    Assert.Equal("2020", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal("30", sut.Values[DateRangeType.End + "day"]);
        //}

        //[Fact]
        //public void ShouldSetValueNoLeadingZeros()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Value = "1979-03-08 00:00:00 - 2020-03-30 00:00:00"
        //    };
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal("1979", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal("8", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDates[DateRangeType.End]);
        //    Assert.Equal("2020", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal("30", sut.Values[DateRangeType.End + "day"]);
        //}

        //[Fact]
        //public void ShouldSetDateTimeForDefaultCulture()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Value = "08-03-1979 00:00:00 - 30-03-2020 00:00:00"
        //    };
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal("1979", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal("8", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDates[DateRangeType.End]);
        //    Assert.Equal("2020", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal("30", sut.Values[DateRangeType.End + "day"]);
        //}

        //[Fact]
        //public void ShouldSetDateTimeForSetCulture()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Culture = new CultureInfo("en-US"),
        //        Value = "03/08/1979 00:00:00 - 03/30/2020 00:00:00"
        //    };
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal("1979", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal("8", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDates[DateRangeType.End]);
        //    Assert.Equal("2020", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal("3", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal("30", sut.Values[DateRangeType.End + "day"]);
        //}

        [Fact]
        public void ShouldThrowExceptionOnSetting()
        {
            Assert.Throws<ArgumentException>(
                () => new DateRangeFormElementData
                {
                    Culture = new CultureInfo("en-US"),
                    Value = "03/08/1979 00:00:00 - 30/03/2020 00:00:00"
                });
            //does not thow then the format matches the culture
            new DateRangeFormElementData
            {
                Culture = new CultureInfo("en-US"),
                Value = "03/08/1979 00:00:00 - 03/30/2020 00:00:00"
            };
            //wrong format
            Assert.Throws<ArgumentException>(
                () => new DateRangeFormElementData
                {
                    Value = "1979-3-8 00:00:00 - 2020-30-03 00:00:00"
                });
            Assert.Throws<ArgumentException>(
                () => new DateRangeFormElementData
                {
                    Value = "197-903-08 00:00:00 - 2020-03-30 00:00:00"
                });
            Assert.Throws<ArgumentException>(
                () => new DateRangeFormElementData
                {
                    Value = "1979-03-08 00:00:00 - 2020-30-30 00:00:00"
                });
        }

        [Fact]
        public void ShouldGetDateRange()
        {
            var sut = new DateRangeFormElementData
            {
                Value = "08-03-1979 00:00:00 - 03-30-2020 00:00:00"
            };

            Assert.StartsWith("8-3-1979 - 30-3-2020", sut.Value);
        }

        //[Fact]
        //public void ShouldThrowExceptionGetDateNoneSet()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "month", "13" },
        //            { DateRangeType.Start + "day", "80" },
        //            { DateRangeType.End + "year", "19790" },
        //            { DateRangeType.End + "month", "13" },
        //            { DateRangeType.End + "day", "80" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "19790" },
        //            { DateRangeType.Start + "day", "80" },
        //            { DateRangeType.End + "year", "19790" },
        //            { DateRangeType.End + "month", "13" },
        //            { DateRangeType.End + "day", "80" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "19790" },
        //            { DateRangeType.Start + "month", "13" },
        //            { DateRangeType.End + "year", "19790" },
        //            { DateRangeType.End + "month", "13" },
        //            { DateRangeType.End + "day", "80" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "19790" },
        //            { DateRangeType.Start + "month", "13" },
        //            { DateRangeType.Start + "day", "80" },
        //            { DateRangeType.End + "month", "13" },
        //            { DateRangeType.End + "day", "80" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "19790" },
        //            { DateRangeType.Start + "month", "13" },
        //            { DateRangeType.Start + "day", "80" },
        //            { DateRangeType.End + "year", "19790" },
        //            { DateRangeType.End + "day", "80" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "19790" },
        //            { DateRangeType.Start + "month", "13" },
        //            { DateRangeType.Start + "day", "80" },
        //            { DateRangeType.End + "year", "19790" },
        //            { DateRangeType.End + "month", "13" }
        //        }
        //    };
        //    Assert.Throws<KeyNotFoundException>(() => sut.Value);
        //}

        //[Fact]
        //public void ShouldThrowExceptionGetDateIllegalValue()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1979" },
        //            { DateRangeType.Start + "month", "3" },
        //            { DateRangeType.Start + "day", "8" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "Eight" }
        //        }
        //    };
        //    Assert.Throws<FormatException>(() => sut.Value);
        //}

        //[Fact]
        //public void ShouldGetDateItemsFromValueDate()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1944, 4, 11) },
        //            { DateRangeType.End, new DateTime(1949, 8, 1) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1979" },
        //            { DateRangeType.Start + "month", "3" },
        //            { DateRangeType.Start + "day", "8" },
        //            { DateRangeType.End + "year", "1980" },
        //            { DateRangeType.End + "month", "11" },
        //            { DateRangeType.End + "day", "21" }
        //        }
        //    };
        //    Assert.Equal(1944, sut.GetYear(DateRangeType.Start));
        //    Assert.Equal(1949, sut.GetYear(DateRangeType.End));
        //    Assert.Equal(4, sut.GetMonth(DateRangeType.Start));
        //    Assert.Equal(8, sut.GetMonth(DateRangeType.End));
        //    Assert.Equal(11, sut.GetDay(DateRangeType.Start));
        //    Assert.Equal(1, sut.GetDay(DateRangeType.End));
        //}

        //[Fact]
        //public void ShouldSetDateItem()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1944, 3, 11) },
        //            { DateRangeType.End, new DateTime(1949, 8, 1) }
        //        }
        //    };
        //    sut.SetYear(DateRangeType.Start, "1949");
        //    Assert.Equal("1949", sut.Values[DateRangeType.Start + "year"]);
        //    sut.SetYear(DateRangeType.End, "1950");
        //    Assert.Equal("1950", sut.Values[DateRangeType.End + "year"]);
        //    sut.SetMonth(DateRangeType.Start, "4");
        //    Assert.Equal("4", sut.Values[DateRangeType.Start + "month"]);
        //    sut.SetMonth(DateRangeType.End, "9");
        //    Assert.Equal("9", sut.Values[DateRangeType.End + "month"]);
        //    sut.SetDay(DateRangeType.Start, "12");
        //    Assert.Equal("12", sut.Values[DateRangeType.Start + "day"]);
        //    sut.SetDay(DateRangeType.End, "2");
        //    Assert.Equal("2", sut.Values[DateRangeType.End + "day"]);
        //}

        //[Fact]
        //public void ShouldNotAcceptWrongYearInDate()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        }
        //    };
        //    sut.SetYear(DateRangeType.Start, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetYear(DateRangeType.End, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetYear(DateRangeType.Start, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetYear(DateRangeType.End, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetYear(DateRangeType.Start, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetYear(DateRangeType.End, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetYear(DateRangeType.Start, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.Start + "year"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetYear(DateRangeType.End, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.End + "year"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //}

        //[Fact]
        //public void ShouldNotAcceptWrongMonthInDate()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        }
        //    };
        //    sut.SetMonth(DateRangeType.Start, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetMonth(DateRangeType.End, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetMonth(DateRangeType.Start, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetMonth(DateRangeType.End, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetMonth(DateRangeType.Start, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetMonth(DateRangeType.End, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetMonth(DateRangeType.Start, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.Start + "month"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetMonth(DateRangeType.End, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.End + "month"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //}

        //[Fact]
        //public void ShouldNotAcceptWrongDayInDate()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        }
        //    };
        //    sut.SetDay(DateRangeType.Start, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetDay(DateRangeType.End, "-10");
        //    Assert.Equal("-10", sut.Values[DateRangeType.End + "day"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetDay(DateRangeType.Start, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetDay(DateRangeType.End, "0");
        //    Assert.Equal("0", sut.Values[DateRangeType.End + "day"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetDay(DateRangeType.Start, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetDay(DateRangeType.End, "10000");
        //    Assert.Equal("10000", sut.Values[DateRangeType.End + "day"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetDay(DateRangeType.Start, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetDay(DateRangeType.End, "Foo");
        //    Assert.Equal("Foo", sut.Values[DateRangeType.End + "day"]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //    sut.SetMonth(DateRangeType.Start, "2");
        //    sut.SetDay(DateRangeType.Start, "29");
        //    Assert.Equal("29", sut.Values[DateRangeType.Start + "day"]);
        //    Assert.Equal(new DateTime(1949, 2, 1), sut.ValueDates[DateRangeType.Start]);
        //    sut.SetMonth(DateRangeType.End, "2");
        //    sut.SetDay(DateRangeType.End, "29");
        //    Assert.Equal("29", sut.Values[DateRangeType.End + "day"]);
        //    Assert.Equal(new DateTime(1979, 2, 8), sut.ValueDates[DateRangeType.End]);
        //}

        //[Fact]
        //private void ShouldThrowExceptionWhenValueAndDateMismatch()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "Eight" }
        //        }
        //    };
        //    Assert.Throws<FormatException>(() => sut.Value);
        //    sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "One" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        }
        //    };
        //    Assert.Throws<FormatException>(() => sut.Value);
        //}

        //[Fact]
        //private void ShouldAcceptLeadingZeros()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "08" },
        //            { DateRangeType.Start + "day", "01" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "03" },
        //            { DateRangeType.End + "day", "08" }
        //        }
        //    };
        //    Assert.StartsWith("1-8-1949 - 8-3-1979", sut.Value);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //}

        //[Fact]
        //private void ShouldAcceptNoLeadingZeros()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        }
        //    };
        //    Assert.StartsWith("1-8-1949 - 8-3-1979", sut.Value);
        //    Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDates[DateRangeType.Start]);
        //    Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDates[DateRangeType.End]);
        //}

        //[Fact]
        //private void ShouldValidateBase()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", string.Empty },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        }
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //}

        //[Fact]
        //private void ShouldValidateDateInvalidDate()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "13" },
        //            { DateRangeType.End + "day", "8" }
        //        }
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //    Assert.Equal("De waarden ingegeven vormen samen geen geldige datum range.", sut.ErrorText);
        //    sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "18" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        }
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //    Assert.Equal("De waarden ingegeven vormen samen geen geldige datum range.", sut.ErrorText);
        //}

        //[Fact]
        //private void ShouldValidateStartBiggerThanEnd()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1979, 3, 8) },
        //            { DateRangeType.End, new DateTime(1949, 8, 1) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1979" },
        //            { DateRangeType.Start + "month", "3" },
        //            { DateRangeType.Start + "day", "8" },
        //            { DateRangeType.End + "year", "1949" },
        //            { DateRangeType.End + "month", "8" },
        //            { DateRangeType.End + "day", "1" }
        //        }
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //    Assert.Equal("De startdatum is groter dan de einddatum.", sut.ErrorText);
        //}

        //[Fact]
        //private void ShouldValidateDateTooLow()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        },
        //        MinimumAllowedDate = new DateTime(2020, 3, 27)
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //    Assert.Equal("De startdatum is kleiner dan de minimaal toegestane datum: '27-3-2020'.", sut.ErrorText);
        //}

        //[Fact]
        //private void ShouldValidateDateTooHigh()
        //{
        //    var sut = new DateRangeFormElementData
        //    {
        //        ValueDates = new Dictionary<DateRangeType, DateTime>
        //        {
        //            { DateRangeType.Start, new DateTime(1949, 8, 1) },
        //            { DateRangeType.End, new DateTime(1979, 3, 8) }
        //        },
        //        Values = new Dictionary<string, string> {
        //            { DateRangeType.Start + "year", "1949" },
        //            { DateRangeType.Start + "month", "8" },
        //            { DateRangeType.Start + "day", "1" },
        //            { DateRangeType.End + "year", "1979" },
        //            { DateRangeType.End + "month", "3" },
        //            { DateRangeType.End + "day", "8" }
        //        },
        //        MaximumAllowedDate = new DateTime(1944, 3, 11)
        //    };
        //    sut.CustomValidate();
        //    Assert.False(sut.IsValid);
        //    Assert.Equal("De einddatum is groter dan de maximaal toegestane datum: '11-3-1944'.", sut.ErrorText);
        //}
    }
}
