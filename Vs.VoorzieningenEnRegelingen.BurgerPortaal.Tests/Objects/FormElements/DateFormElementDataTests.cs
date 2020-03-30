using System;
using System.Collections.Generic;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class DateFormElementDataTests
    {
        [Fact]
        public void CheckValueIsTodayWhenEmpty()
        {
            var sut = new DateFormElementData();
            Assert.Equal(DateTime.Today, sut.ValueDate);
            Assert.Equal(DateTime.Today.Year.ToString(), sut.Values["year"]);
            Assert.Equal(DateTime.Today.Month.ToString(), sut.Values["month"]);
            Assert.Equal(DateTime.Today.Day.ToString(), sut.Values["day"]);
        }

        [Fact]
        public void CheckValidWhenEmpty()
        {
            var sut = new DateFormElementData();
            sut.Validate();
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void ShouldSetValue()
        {
            var sut = new DateFormElementData
            {
                Value = "1979-03-08"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            Assert.Equal("1979", sut.Values["year"]);
            Assert.Equal("3", sut.Values["month"]);
            Assert.Equal("8", sut.Values["day"]);
        }

        [Fact]
        public void ShouldSetValueNoLeadingZeros()
        {
            var sut = new DateFormElementData
            {
                Value = "1979-3-8"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            Assert.Equal("1979", sut.Values["year"]);
            Assert.Equal("3", sut.Values["month"]);
            Assert.Equal("8", sut.Values["day"]);
        }

        [Fact]
        public void ShouldSetDateTimeForDefaultCulture()
        {
            var sut = new DateFormElementData
            {
                Value = "08-03-1979"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            Assert.Equal("1979", sut.Values["year"]);
            Assert.Equal("3", sut.Values["month"]);
            Assert.Equal("8", sut.Values["day"]);
        }

        [Fact]
        public void ShouldSetDateTimeForSetCulture()
        {
            var sut = new DateFormElementData
            {
                Culture = new CultureInfo("en-US"),
                Value = "03/08/1979"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            Assert.Equal("1979", sut.Values["year"]);
            Assert.Equal("3", sut.Values["month"]);
            Assert.Equal("8", sut.Values["day"]);
        }

        [Fact]
        public void ShouldThrowExceptionOnSetting()
        {
            Assert.Throws<ArgumentException>(
                () => new DateFormElementData
                {
                    Culture = new CultureInfo("en-US"),
                    Value = "29/03/1979"
                });
            //does not thow then the format matches the culture
            new DateFormElementData
            {
                Culture = new CultureInfo("en-US"),
                Value = "03/29/1979"
            };
            //wrong format
            Assert.Throws<ArgumentException>(
                () => new DateFormElementData
                {
                    Value = "1979-29-3"
                });
            Assert.Throws<ArgumentException>(
                () => new DateFormElementData
                {
                    Value = "197-329-3"
                });
            Assert.Throws<ArgumentException>(
                () => new DateFormElementData
                {
                    Value = "1979-29-29"
                });
        }

        [Fact]
        public void ShouldGetDate()
        {
            var sut = new DateFormElementData
            {
                Value = "08-03-1979"
            };
            Assert.Equal("1979-03-08", sut.Value);
        }

        [Fact]
        public void ShouldThrowExceptionGetDateNoneSet()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "month", "13" },
                    { "day", "80" },
                }
            };
            Assert.Throws<KeyNotFoundException>(() => sut.Value);
            sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "19790" },
                    { "day", "80" },
                }
            };
            Assert.Throws<KeyNotFoundException>(() => sut.Value);
            sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "19790" },
                    { "month", "13" },
                }
            };
            Assert.Throws<KeyNotFoundException>(() => sut.Value);
        }

        [Fact]
        public void ShouldThrowExceptionGetDateIllegalValue()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "13" },
                    { "day", "Eight" }
                }
            };
            Assert.Throws<FormatException>(() => sut.Value);
        }

        [Fact]
        public void ShouldGetDateItemsFromValueDate()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1949" },
                    { "month", "8" },
                    { "day", "1" }
                }
            };
            Assert.Equal(1979, sut.GetYear());
            Assert.Equal(3, sut.GetMonth());
            Assert.Equal(8, sut.GetDay());
        }

        [Fact]
        public void ShouldSetDateItem()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8)
            };
            sut.SetYear("1949");
            Assert.Equal("1949", sut.Values["year"]);
            Assert.Equal(new DateTime(1949, 3, 8), sut.ValueDate);
            sut.SetMonth("8");
            Assert.Equal("8", sut.Values["month"]);
            Assert.Equal(new DateTime(1949, 8, 8), sut.ValueDate);
            sut.SetDay("1");
            Assert.Equal("1", sut.Values["day"]);
            Assert.Equal(new DateTime(1949, 8, 1), sut.ValueDate);
        }

        [Fact]
        public void ShouldNotAcceptWrongYearInDate()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8)
            };
            sut.SetYear("-10");
            Assert.Equal("-10", sut.Values["year"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetYear("0");
            Assert.Equal("0", sut.Values["year"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetYear("10000");
            Assert.Equal("10000", sut.Values["year"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetYear("Foo");
            Assert.Equal("Foo", sut.Values["year"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        public void ShouldNotAcceptWrongMonthInDate()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8)
            };
            sut.SetMonth("-10");
            Assert.Equal("-10", sut.Values["month"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetMonth("0");
            Assert.Equal("0", sut.Values["month"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetMonth("13");
            Assert.Equal("13", sut.Values["month"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetMonth("Foo");
            Assert.Equal("Foo", sut.Values["month"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        public void ShouldNotAcceptWrongDayInDate()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8)
            };
            sut.SetDay("-10");
            Assert.Equal("-10", sut.Values["day"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetDay("0");
            Assert.Equal("0", sut.Values["day"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetDay("32");
            Assert.Equal("32", sut.Values["day"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetDay("Foo");
            Assert.Equal("Foo", sut.Values["day"]);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
            sut.SetMonth("2");
            sut.SetDay("29");
            Assert.Equal("29", sut.Values["day"]);
            Assert.Equal(new DateTime(1979, 2, 8), sut.ValueDate);
        }

        [Fact]
        private void ShouldThrowExceptionWhenValueAndDateMismatch()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 7),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "3" },
                    { "day", "Seven" }
                }
            };
            Assert.Throws<FormatException>(() => sut.Value);
        }

        [Fact]
        private void ShouldAcceptLeadingZeros()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "03" },
                    { "day", "08" }
                }
            };
            Assert.Equal("1979-03-08", sut.Value);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        private void ShouldAcceptNoLeadingZeros()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "3" },
                    { "day", "8" }
                }
            };
            Assert.Equal("1979-03-08", sut.Value);
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        private void ShouldValidateBase()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", string.Empty },
                    { "month", string.Empty },
                    { "day", string.Empty }
                }
            };
            sut.Validate();
            Assert.False(sut.IsValid);
        }

        [Fact]
        private void ShouldValidateDateInvalidDate()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "13" },
                    { "day", "08" }
                }
            };
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("De waarden ingegeven vormen samen geen geldige datum.", sut.ErrorText);
        }

        [Fact]
        private void ShouldValidateDateTooLow()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "03" },
                    { "day", "08" }
                },
                MinimumAllowedDate = new DateTime(2020, 3, 27)
            };
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("De datum is kleiner dan de minimaal toegestane datum: '27-3-2020'.", sut.ErrorText);
        }

        [Fact]
        private void ShouldValidateDateTooHigh()
        {
            var sut = new DateFormElementData
            {
                ValueDate = new DateTime(1979, 3, 8),
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "03" },
                    { "day", "08" }
                },
                MaximumAllowedDate = new DateTime(1944, 3, 11)
            };
            sut.Validate();
            Assert.False(sut.IsValid);
            Assert.Equal("De datum is groter dan de maximaal toegestane datum: '11-3-1944'.", sut.ErrorText);
        }
    }
}
