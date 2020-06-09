using System;
using System.Runtime.Serialization;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Exceptions
{
    [Serializable]
    public class HeaderFormattingException : FormattingException
    {
        public HeaderFormattingException()
        {
            throw new NotImplementedException();
        }

        public HeaderFormattingException(string message, DebugInfo debugInfo) : base(message, debugInfo)
        {
        }
    }
}