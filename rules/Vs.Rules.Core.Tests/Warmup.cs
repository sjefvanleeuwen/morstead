using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("Vs.Rules.Core.Tests.Warmup", "Vs.Rules.Core.Tests")]
namespace Vs.Rules.Core.Tests
{

    public class Warmup : XunitTestFramework
    {
        public Warmup(IMessageSink messageSink)
        : base(messageSink)
        {
            // Place initialization code here
            Globalization.SetFormattingExceptionResourceCulture(new CultureInfo("nl-NL"));
            Globalization.SetKeywordResourceCulture(new CultureInfo("nl-NL"));
        }

        public new void Dispose()
        {
            // Place tear down code here
            base.Dispose();
        }
    }
}
