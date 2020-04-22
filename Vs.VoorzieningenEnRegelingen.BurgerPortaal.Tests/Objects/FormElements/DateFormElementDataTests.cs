using System;
using System.Globalization;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects.FormElements;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests.Objects.FormElements
{
    public class DateFormElementDataTests
    {
        [Fact]
        public void CheckValueNullWhenEmpty()
        {
            var sut = new DateFormElementData();
            Assert.Null(sut.ValueDate);
            Assert.Empty(sut.Value);
        }

        [Fact]
        public void CheckInvalidWhenEmpty()
        {
            var sut = new DateFormElementData();
            sut.CustomValidate();
            Assert.False(sut.IsValid);
        }

        [Fact]
        public void ShouldSetValue()
        {
            var sut = new DateFormElementData
            {
                Value = "1979-03-08"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        public void ShouldSetValueNoLeadingZeros()
        {
            var sut = new DateFormElementData
            {
                Value = "1979-3-8"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
        }

        [Fact]
        public void ShouldSetDateTimeForDefaultCulture()
        {
            var sut = new DateFormElementData
            {
                Value = "08-03-1979"
            };
            Assert.Equal(new DateTime(1979, 3, 8), sut.ValueDate);
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
        public void ShouldNotAcceptWrongDate()
        {
            Assert.Throws<ArgumentException>(() => new DateFormElementData { Value = "21-13-1979" });
        }

        [Fact]
        private void ShouldValidateDateTooLow()
        {
            var sut = new DateFormElementData
            {
                Value = "1979-03-08",
                MinimumAllowedDate = new DateTime(1988, 05, 09)
                
            };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("De datum is kleiner dan de minimaal toegestane datum: '9-5-1988'.", sut.ErrorText);
        }

        [Fact]
        private void ShouldValidateDateTooHigh()
        {
            var sut = new DateFormElementData
            {
                Value = "1949-08-01",
                MaximumAllowedDate = new DateTime(1944, 03, 11)
            };
            sut.CustomValidate();
            Assert.False(sut.IsValid);
            Assert.Equal("De datum is groter dan de maximaal toegestane datum: '11-3-1944'.", sut.ErrorText);
        }
    }
}
