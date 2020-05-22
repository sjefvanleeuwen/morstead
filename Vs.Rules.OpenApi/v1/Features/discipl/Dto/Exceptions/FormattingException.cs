using System;
using Vs.Core.Diagnostics;

namespace Vs.Rules.OpenApi.v1.Features.discipl.Dto.Exceptions
{
    public class FormattingException
    {
        public DebugInfo DebugInfo { get; set; }
        public string Message { get; set; }
        public FormattingException()
        {
            
        }

        public FormattingException(string message, DebugInfo debugInfo)
        {
            Message = message;
            DebugInfo = debugInfo;
        }
    }
}
