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
            Assert.Equal(DateTime.Today.Year.ToString("0000"), sut.Values["year"]);
            Assert.Equal(DateTime.Today.Month.ToString("00"), sut.Values["month"]);
            Assert.Equal(DateTime.Today.Day.ToString("00"), sut.Values["day"]);
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
            Assert.Equal("03", sut.Values["month"]);
            Assert.Equal("08", sut.Values["day"]);
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
            Assert.Equal("03", sut.Values["month"]);
            Assert.Equal("08", sut.Values["day"]);
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
            Assert.Equal("03", sut.Values["month"]);
            Assert.Equal("08", sut.Values["day"]);
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
            Assert.Equal("03", sut.Values["month"]);
            Assert.Equal("08", sut.Values["day"]);
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
            //does not thow then the dormat matches the culture
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
        private void ShouldGetDate()
        {
            var sut = new DateFormElementData
            {
                Value = "08-03-1979"
            };
            Assert.Equal("1979-03-08", sut.Value);
        }

        [Fact]
        private void ShouldThrowExceptionGetDateNoneSet()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "13" }
                }
            };
            Assert.Throws<ArgumentNullException>(() => sut.Value);
        }

        [Fact]
        private void ShouldThrowExceptionGetDateIllegalValue()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "13" },
                    { "day", "Eight" }
                }
            };
            Assert.Throws<ArgumentException>(() => sut.Value);
        }

        [Fact]
        private void ShouldThrowExceptionGetDateOutOfRange()
        {
            var sut = new DateFormElementData
            {
                Values = new Dictionary<string, string> {
                    { "year", "1979" },
                    { "month", "13" },
                    { "day", "08" }
                }
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Value);
        }

        [Fact]
        private void ShouldAcceptNoLeadingValues()
        {
            var sut = new DateFormElementData
            {
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
                Values = new Dictionary<string, string>()
            };
            sut.Validate();
            Assert.False(sut.IsValid);
        }

        [Fact]
        private void ShouldValidateDateInvalidDate()
        {
            var sut = new DateFormElementData
            {
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
