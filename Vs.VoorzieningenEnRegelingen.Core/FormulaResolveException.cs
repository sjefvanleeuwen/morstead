using System;
using System.Runtime.Serialization;
using Vs.VoorzieningenEnRegelingen.Core.Model;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    [Serializable]
    internal class StepException : Exception
    {
        public StepException(string message, Step step) : base(message)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
        }

        public Step Step { get; }
    }
}
