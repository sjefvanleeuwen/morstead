using System;
using System.Globalization;
using Vs.CitizenPortal.DataModel.Model.FormElements;
using Xunit;

namespace Vs.BurgerPortaal.Core.Tests.Objects.FormElements
{
    public class DateRangeFormElementDataTests
    {
        [Fact]
        public void CheckValueIsNullWhenEmpty()
        {
            var sut = new DateRangeFormElementData();
            Assert.Null(sut.ValueDateStart);
            Assert.Null(sut.ValueDateEnd);
        }

        [Fact]
        public void CheckInvalidWhenEmpty()
        {
            var sut = new DateRangeFormElementData();
            sut.CustomValidate();
            Assert.False(sut.IsValid);
        }

        [Fact]
        public void ShouldSetValue()
        {
            var sut = new DateRangeFormElementData
            {
                Value = "1979-03-08 00:00:00 - 2020-03-30 00:00:00"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDateStart);
            Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDateEnd);
        }

        [Fact]
        public void ShouldSetValueNoLeadingZeros()
        {
            var sut = new DateRangeFormElementData
            {
                Value = "1979-3-8 00:00:00 - 2020-3-30 00:00:00"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDateStart);
            Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDateEnd);
        }

        [Fact]
        public void ShouldSetDateTimeForDefaultCulture()
        {
            var sut = new DateRangeFormElementData
            {
                Value = "08-03-1979 00:00:00 - 30-03-2020 00:00:00"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDateStart);
            Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDateEnd);
        }

        [Fact]
        public void ShouldSetDateTimeForSetCulture()
        {
            var sut = new DateRangeFormElementData
            {
                Culture = new CultureInfo("en-US"),
                Value = "03/08/1979 00:00:00 - 03/30/2020 00:00:00"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDateStart);
            Assert.Equal(new DateTime(2020, 3, 30), sut.ValueDateEnd);
        }

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

        [Fact]
        public void ShouldThrowExceptionGetDateIllegalValue()
        {
            Assert.Throws<ArgumentException>(() => new DateRangeFormElementData { Value = "31-13-1979 00:00:00 - 31-13-2020 00:00:00" });
        }

        [Fact]
        private void ShouldValidateStartBiggerThanEnd()
        {
            var sut = new DateRangeFormElementData
            {
                ValueDateStart = new DateTime(1979, 3, 8),
                ValueDateEnd = new DateTime(1949, 8, 1)
            };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("De startdatum is groter dan de einddatum.", sut.ErrorText);
        }

        [Fact]
        private void ShouldValidateDateTooLow()
        {
            var sut = new DateRangeFormElementData
            {
                ValueDateStart = new DateTime(1949, 8, 1),
                ValueDateEnd = new DateTime(1979, 3, 8),
                MinimumAllowedDate = new DateTime(2020, 3, 27)
            };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("De startdatum is kleiner dan de minimaal toegestane datum: '27-3-2020'.", sut.ErrorText);
        }

        [Fact]
        private void ShouldValidateDateTooHigh()
        {
            var sut = new DateRangeFormElementData
            {
                ValueDateStart = new DateTime(1949, 8, 1),
                ValueDateEnd = new DateTime(1979, 3, 8),
                MaximumAllowedDate = new DateTime(1944, 3, 11)
            };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("De einddatum is groter dan de maximaal toegestane datum: '11-3-1944'.", sut.ErrorText);
        }
    }
}
