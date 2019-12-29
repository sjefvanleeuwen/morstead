using Itenso.TimePeriod;
using System;
using System.Globalization;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class TimePeriodTests
    {
        [Fact]
        public void TimePeriod_Can_Serialize()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            var range = new Itenso.TimePeriod.TimeRange();
            // range.m

            // "01/01/0001 00:00:00 - 31/12/9999 23:59:59 | 3652058.23:59"
            var s = range.ToString();

            var dateTimeStrings = s.Split('|')[0].Split(" - ");

            var l = new TimeRange(DateTime.Parse(dateTimeStrings[0],new CultureInfo("nl-NL")), DateTime.Parse(dateTimeStrings[1],new CultureInfo("nl-NL")));
        }
    }
}
