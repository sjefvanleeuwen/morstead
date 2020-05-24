using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Exceptions
{
    [Serializable]
    public class RootFormattingException : FormattingException
    {
        public RootFormattingException()
        {
            throw new NotImplementedException();
        }

        public RootFormattingException(string message, DebugInfo debugInfo) : base(message, debugInfo)
        {
        }
    }
}