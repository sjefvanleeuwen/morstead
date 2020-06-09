using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.Core.Exceptions
{
    [Serializable]
    public class StepFormattingException : FormattingException
    {
        public StepFormattingException()
        {
            throw new NotImplementedException();
        }

        public StepFormattingException(string message, DebugInfo debugInfo) : base(message, debugInfo)
        {
        }
    }
}