using Itenso.TimePeriod;
using System;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class TimePeriodTests
    {
        [Fact]
        public void TimePeriod_Can_Serialize()
        {
            var range = new Itenso.TimePeriod.TimeRange();
            // range.m

            // "01/01/0001 00:00:00 - 31/12/9999 23:59:59 | 3652058.23:59"
            var s = range.ToString();

            var dateTimeStrings = s.Split('|')[0].Split('-');

            var l = new TimeRange(DateTime.Parse(dateTimeStrings[0]), DateTime.Parse(dateTimeStrings[1]));
        }
    }
}
