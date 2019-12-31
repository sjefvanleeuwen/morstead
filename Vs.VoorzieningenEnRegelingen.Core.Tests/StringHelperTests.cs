using System;
using System.Globalization;
using Xunit;

namespace Vs.VoorzieningenEnRegelingen.Core.Tests
{
    public class StringHelperTests
    {
        [Fact]
        public void StringHelper_Can_Infer_Double()
        {
            var t = "48.56";
            var y = t.Infer();
            Assert.True(t == Convert.ToString(y,new CultureInfo("en-US")));
        }
    }
}
