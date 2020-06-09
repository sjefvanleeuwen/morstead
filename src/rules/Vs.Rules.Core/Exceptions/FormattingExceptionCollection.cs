using System;
using System.Collections.Generic;

namespace Vs.Rules.Core.Exceptions
{
    public class FormattingExceptionCollection : Exception
    {
        public FormattingExceptionCollection(string message, List<FormattingException> exceptions) : base(message)
        {
            Exceptions = exceptions;
        }

        public List<FormattingException> Exceptions { get; }
    }
}
