using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Exceptions
{
    [Serializable]
    public class FlowFormattingException : FormattingException
    {
        public FlowFormattingException()
        {
            throw new NotImplementedException();
        }

        public FlowFormattingException(string message, DebugInfo debugInfo) : base(message, debugInfo)
        {
        }
    }
}