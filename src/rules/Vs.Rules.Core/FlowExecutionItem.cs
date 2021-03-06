﻿using System;
using Vs.Rules.Core.Model;

namespace Vs.Rules.Core
{
    public class FlowExecutionItem
    {
        public FlowExecutionItem(IStep step, Exception exception = null)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
            Exception = exception;
        }

        public IStep Step { get; }
        public bool IsStopExecution { get; private set; }
        public Exception Exception { get; }

        public void StopExecution() => IsStopExecution = true;
    }
}
