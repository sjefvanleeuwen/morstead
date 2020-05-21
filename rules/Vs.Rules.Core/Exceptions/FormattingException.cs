using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Exceptions
{
    public class FormattingException : Exception
    {
        public DebugInfo DebugInfo { get; internal set; }
        public FormattingException()
        {
            throw new NotImplementedException();
        }

        public FormattingException(string message, DebugInfo debugInfo) : base(message)
        {
            DebugInfo = debugInfo;
        }
    }
}
